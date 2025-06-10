namespace inter_sdk_library;

using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

public class TokenUtils {
    private static Dictionary<string, GetTokenResponse> map = new Dictionary<string, GetTokenResponse>();
    private const int SECONDS_TO_RENEW = 60;

    public static string GetToken(Config config, string scopes, X509Certificate2Collection certificates) {
        GetTokenResponse tokenInfo;
        map.TryGetValue(scopes, out tokenInfo);
        bool isTokenValid = ValidateToken(tokenInfo);

        if (!isTokenValid) {
            tokenInfo = makeTokenRequest(config, scopes, certificates);
            map[scopes] = tokenInfo;
        }
        
        return tokenInfo.AccessToken;
    }

    private static bool ValidateToken(GetTokenResponse getTokenResponse) {
        if (getTokenResponse == null) {
            return false;
        }

        long? expirationDate = getTokenResponse.CreatedAt + getTokenResponse.ExpiresIn;
        long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return (now + SECONDS_TO_RENEW) <= expirationDate;
    }

    public static GetTokenResponse makeTokenRequest(Config config, string scope, X509Certificate2Collection certificates) {
        string url = UrlUtils.BuildUrl(config, Constants.URL_TOKEN);

        InterSdk.LogInfo("POST {0}", url);
        using (var handler = new HttpClientHandler()) {
            handler.ClientCertificates.AddRange(certificates);
            using (var client = new HttpClient(handler)) {
                var postData = new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>("client_id", config.ClientId),
                    new KeyValuePair<string, string>("client_secret", config.ClientSecret),
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("scope", scope)
                };
                var content = new FormUrlEncodedContent(postData);
                HttpResponseMessage resp;
                try {
                    resp = client.PostAsync(url, content).GetAwaiter().GetResult();
                } catch (HttpRequestException e) {
                    if (e.StatusCode == HttpStatusCode.TooManyRequests && config.RateLimitControl) {
                        Thread.Sleep(60000);
                        return makeTokenRequest(config, scope, certificates);
                    }
                    throw;
                }

                GetTokenResponse token = null;
                InterSdk.LogInfo("HTTPSTATUS {0}", resp.StatusCode.ToString());
                string jsonResponse = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                int code = (int)resp.StatusCode;
                if (config.Debug && code >= 400) {
                    InterSdk.LogInfo("RESPONSE {0}", jsonResponse);
                }
                token = JsonSerializer.Deserialize<GetTokenResponse>(jsonResponse)!;
                token.CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                return token;
            }
        }
    }
}
