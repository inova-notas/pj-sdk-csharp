namespace inter_sdk_library;

using System.IO;
using System.Text;
using System.Net;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

public class HttpUtils {
    public static string LastUrl;
    public static string LastRequest;

    public static string CallVerb(Config config, string url, string scope, string message, string body, string verb) {
        LastUrl = url;
        LastRequest = body;
        X509Certificate2Collection certificates = new X509Certificate2Collection();

        var cert = X509CertificateLoader.LoadPkcs12FromFile(config.Certificate, config.Password,
            X509KeyStorageFlags.PersistKeySet);
        certificates.Add(cert);
        
        string token = TokenUtils.GetToken(config, scope, certificates);

        InterSdk.LogInfo("{0} {1}", verb, url);
        using (var handler = new HttpClientHandler()) {
            handler.ClientCertificates.AddRange(certificates);
            using (var client = new HttpClient(handler)) {
                var request = new HttpRequestMessage(new HttpMethod(verb), url);
                request.Headers.Add("Authorization", "Bearer " + token);
                if (config.Account != null && !config.Account.Equals("")) {
                    request.Headers.Add("x-conta-corrente", config.Account);
                }
                request.Headers.Add("x-inter-sdk", "csharp");
                request.Headers.Add("x-inter-sdk-version", "1.0.0");
                if (body != null && !body.Equals("")) {
                    if (config.Debug) {
                        InterSdk.LogInfo("REQUEST {0}", body);
                    }
                    request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                }
                string jsonResponse = "";
                string err = null;
                HttpResponseMessage resp = null;
                try {
                    resp = client.SendAsync(request).GetAwaiter().GetResult();
                    jsonResponse = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (config.Debug) {
                        InterSdk.LogInfo("RESPONSE {0}", jsonResponse);
                    }
                    if ((int)resp.StatusCode == 429 && config.RateLimitControl) {
                        Thread.Sleep(60000);
                        return CallVerb(config, url, scope, message, body, verb);
                    }
                    if (!resp.IsSuccessStatusCode) {
                        err = resp.ReasonPhrase;
                    }
                } catch (HttpRequestException e) {
                    err = e.Message;
                }
                if (err != null) {
                    Error error = BuildError(err, jsonResponse);
                    throw new SdkException(err, error);
                }
                return jsonResponse;
            }
        }
    }

    static Error BuildError(string err, string jsonResponse) {
        if (!string.IsNullOrEmpty(jsonResponse)) {
            return JsonSerializer.Deserialize<Error>(jsonResponse)!;
        } else {
            return new Error{
                Title = "Error",
                Detail = err,
            };
        }
    }
    public static string CallGet(Config config, string url, string scope, string message) {
        return CallVerb(config, url, scope, message, null, "GET");
    }

    public static string CallPost(Config config, string url, string scope, string message, string body) {
        return CallVerb(config, url, scope, message, body, "POST");
    }

    public static string CallPut(Config config, string url, string scope, string message, string body) {
        return CallVerb(config, url, scope, message, body, "PUT");
    }

    public static string CallPatch(Config config, string url, string scope, string message, string body) {
        return CallVerb(config, url, scope, message, body, "PATCH");
    }

    public static string CallDelete(Config config, string url, string scope, string message) {
        return CallVerb(config, url, scope, message, null, "DELETE");
    }

}
