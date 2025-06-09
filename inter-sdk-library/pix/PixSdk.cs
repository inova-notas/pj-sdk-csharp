namespace inter_sdk_library;

public class PixSdk
{
    private readonly Config _config;
    private readonly DueBillingClient _dueBillingClient = new DueBillingClient();
    private readonly DueBillingBatchClient _dueBillingBatchClient = new DueBillingBatchClient();
    private readonly ImmediateBillingClient _immediateBillingClient = new ImmediateBillingClient();
    private readonly LocationClient _locationClient = new LocationClient();
    private readonly PixClient _pixClient = new PixClient();
    private readonly PixWebhookSdk _pixWebhookSdk = new PixWebhookSdk();

    public PixSdk(Config config) 
    {
        _config = config;
    }

    /// <summary>
    /// Includes a due billing entry for a PIX transaction.
    /// </summary>
    /// <param name="txid">The transaction ID associated with the due billing.</param>
    /// <param name="billing">The DueBilling object containing the billing details to be included.</param>
    /// <returns>A GeneratedDueBilling object containing the details of the included due billing.</returns>
    /// <exception cref="SdkException">If an error occurs during the inclusion process.</exception>
    public GeneratedDueBilling IncludeDuePixBilling(string txid, DueBilling billing)
    {
        return _dueBillingClient.IncludeDueBilling(_config, txid, billing);
    }

    /// <summary>
    /// Retrieves the detailed due billing information for a specific PIX transaction.
    /// </summary>
    /// <param name="txid">The transaction ID associated with the due billing to be retrieved.</param>
    /// <returns>A DetailedDuePixBilling object containing the details of the retrieved due billing.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public DetailedDuePixBilling RetrieveDuePixBilling(string txid)
    {
        return _dueBillingClient.RetrieveDueBilling(_config, txid);
    }

    /// <summary>
    /// Retrieves a list of detailed due billing entries for a specified period, applying optional filters.
    /// </summary>
    /// <param name="initialDate">The starting date for the billing collection retrieval. Format: YYYY-MM-DD.</param>
    /// <param name="finalDate">The ending date for the billing collection retrieval. Format: YYYY-MM-DD.</param>
    /// <param name="filter">Optional filter criteria to refine the billing collection retrieval.</param>
    /// <returns>A list of DetailedDuePixBilling objects containing the retrieved billing information.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public List<DetailedDuePixBilling> RetrieveBillingCollectionInRange(string initialDate, string finalDate, RetrieveDueBillingFilter filter)
    {
        return _dueBillingClient.RetrieveDueBillingInRange(_config, initialDate, finalDate, filter);
    }

    /// <summary>
    /// Retrieves a paginated collection of due billing entries for a specified period, applying optional filters.
    /// </summary>
    /// <param name="initialDate">The starting date for the billing collection retrieval. Format: YYYY-MM-DD.</param>
    /// <param name="finalDate">The ending date for the billing collection retrieval. Format: YYYY-MM-DD.</param>
    /// <param name="page">The page number for pagination.</param>
    /// <param name="pageSize">The number of items per page. If null, a default size will be used.</param>
    /// <param name="filter">Optional filter criteria to refine the billing collection retrieval.</param>
    /// <returns>A DueBillingPage object containing the paginated list of retrieved due billing entries.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public DueBillingPage RetrieveDueBillingCollectionPage(string initialDate, string finalDate, int page, int? pageSize, RetrieveDueBillingFilter filter)
    {
        return _dueBillingClient.RetrieveDueBillingPage(_config, initialDate, finalDate, page, pageSize, filter);
    }

    /// <summary>
    /// Reviews a due billing entry for a PIX transaction.
    /// </summary>
    /// <param name="txid">The transaction ID associated with the due billing to be reviewed.</param>
    /// <param name="billing">The DueBilling object containing the billing details to be reviewed.</param>
    /// <returns>A GeneratedDueBilling object containing the details of the reviewed due billing.</returns>
    /// <exception cref="SdkException">If an error occurs during the review process.</exception>
    public GeneratedDueBilling ReviewDuePixBilling(string txid, DueBilling billing)
    {
        return _dueBillingClient.ReviewDueBilling(_config, txid, billing);
    }

    /// <summary>
    /// Includes a batch of due billing entries for a specific PIX transaction.
    /// </summary>
    /// <param name="txid">The transaction ID associated with the due billing batch.</param>
    /// <param name="batchRequest">The IncludeDueBillingBatchRequest object containing the details of the billing batch to be included.</param>
    /// <exception cref="SdkException">If an error occurs during the inclusion process.</exception>
    public void IncludeDueBillingBatch(string txid, IncludeDueBillingBatchRequest batchRequest)
    {
        _dueBillingBatchClient.IncludeDueBillingBatch(_config, txid, batchRequest);
    }

    /// <summary>
    /// Retrieves a due billing batch by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the billing batch to be retrieved.</param>
    /// <returns>A DueBillingBatch object containing the details of the retrieved billing batch.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public DueBillingBatch RetrieveDueBillingBatch(string id)
    {
        return _dueBillingBatchClient.RetrieveDueBillingBatch(_config, id);
    }

    /// <summary>
    /// Retrieves a paginated collection of due billing batches for a specified period.
    /// </summary>
    /// <param name="initialDate">The starting date for the billing batch collection retrieval. Format: YYYY-MM-DD.</param>
    /// <param name="finalDate">The ending date for the billing batch collection retrieval. Format: YYYY-MM-DD.</param>
    /// <param name="page">The page number for pagination.</param>
    /// <param name="pageSize">The number of items per page. If null, a default size will be used.</param>
    /// <returns>A DueBillingBatchPage object containing the paginated list of retrieved due billing batches.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public DueBillingBatchPage RetrieveDueBillingBatchCollectionPage(string initialDate, string finalDate, int page, int? pageSize)
    {
        return _dueBillingBatchClient.RetrieveDueBillingBatchesPage(_config, initialDate, finalDate, page, pageSize);
    }

    /// <summary>
    /// Retrieves a list of due billing batches for a specified period.
    /// </summary>
    /// <param name="initialDate">The starting date for the billing batch collection retrieval. Format: YYYY-MM-DD.</param>
    /// <param name="finalDate">The ending date for the billing batch collection retrieval. Format: YYYY-MM-DD.</param>
    /// <returns>A list of DueBillingBatch objects containing the retrieved billing batches.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public List<DueBillingBatch> RetrieveDueBillingBatchCollectionInRange(string initialDate, string finalDate)
    {
        return _dueBillingBatchClient.RetrieveDueBillingBatchesInRange(_config, initialDate, finalDate);
    }

    /// <summary>
    /// Retrieves the situation of a specific due billing batch by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the billing batch whose situation is to be retrieved.</param>
    /// <param name="situation">The specific situation to filter the results.</param>
    /// <returns>A DueBillingBatch object containing the details of the retrieved billing batch situation.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public DueBillingBatch RetrieveDueBillingBatchBySituation(string id, string situation)
    {
        return _dueBillingBatchClient.RetrieveDueBillingBatchBySituation(_config, id, situation);
    }

    /// <summary>
    /// Retrieves the summary of a specific due billing batch by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the billing batch whose summary is to be retrieved.</param>
    /// <returns>A DueBillingBatchSummary object containing the summary details of the retrieved billing batch.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public DueBillingBatchSummary RetrieveDueBillingBatchSummary(string id)
    {
        return _dueBillingBatchClient.RetrieveDueBillingBatchSummary(_config, id);
    }

    /// <summary>
    /// Reviews a due billing batch identified by its ID.
    /// </summary>
    /// <param name="id">The identifier of the billing batch to be reviewed.</param>
    /// <param name="request">The IncludeDueBillingBatchRequest object containing details for the review process.</param>
    /// <exception cref="SdkException">If an error occurs during the review process.</exception>
    public void ReviewDueBillingBatch(string id, IncludeDueBillingBatchRequest request)
    {
        _dueBillingBatchClient.ReviewDueBillingBatch(_config, id, request);
    }

    /// <summary>
    /// Includes an immediate billing entry for a PIX transaction.
    /// </summary>
    /// <param name="billing">The PixBilling object containing the details of the immediate billing to be included.</param>
    /// <returns>A GeneratedImmediateBilling object containing the details of the included immediate billing.</returns>
    /// <exception cref="SdkException">If an error occurs during the inclusion process.</exception>
    public GeneratedImmediateBilling IncludeImmediateBilling(PixBilling billing)
    {
        return _immediateBillingClient.IncludeImmediateBilling(_config, billing);
    }

    /// <summary>
    /// Retrieves the details of an immediate billing entry by its transaction ID.
    /// </summary>
    /// <param name="txid">The transaction ID associated with the immediate billing to be retrieved.</param>
    /// <returns>A DetailedImmediatePixBilling object containing the details of the retrieved immediate billing.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public DetailedImmediatePixBilling RetrieveImmediateBilling(string txid)
    {
        return _immediateBillingClient.RetrieveImmediateBilling(_config, txid);
    }

    /// <summary>
    /// Retrieves a list of detailed immediate billing entries for a specified period, optionally filtered.
    /// </summary>
    /// <param name="initialDate">The starting date for the retrieval of immediate billings. Format: YYYY-MM-DD.</param>
    /// <param name="finalDate">The ending date for the retrieval of immediate billings. Format: YYYY-MM-DD.</param>
    /// <param name="filter">The filter criteria for retrieving the immediate billings.</param>
    /// <returns>A list of DetailedImmediatePixBilling objects containing the details of the retrieved immediate billings.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public List<DetailedImmediatePixBilling> RetrieveImmediateBillingCollectionInRange(string initialDate, string finalDate, RetrieveImmediateBillingsFilter filter)
    {
        return _immediateBillingClient.RetrieveImmediateBillingInRange(_config, initialDate, finalDate, filter);
    }

    /// <summary>
    /// Retrieves a paginated list of immediate billing entries for a specified period, optionally filtered.
    /// </summary>
    /// <param name="initialDate">The starting date for the retrieval of immediate billings. Format: YYYY-MM-DD.</param>
    /// <param name="finalDate">The ending date for the retrieval of immediate billings. Format: YYYY-MM-DD.</param>
    /// <param name="page">The page number for pagination.</param>
    /// <param name="pageSize">The number of items per page. If null, a default size will be used.</param>
    /// <param name="filter">The filter criteria for retrieving the immediate billings.</param>
    /// <returns>A BillingPage object containing the paginated list of retrieved immediate billings.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public PixBillingPage RetrieveImmediateBillingCollectionPage(string initialDate, string finalDate, int page, int? pageSize, RetrieveImmediateBillingsFilter filter)
    {
        return _immediateBillingClient.RetrieveImmediateBillingPage(_config, initialDate, finalDate, page, pageSize, filter);
    }

    /// <summary>
    /// Reviews an immediate billing entry for a PIX transaction.
    /// </summary>
    /// <param name="billing">The PixBilling object containing the details of the immediate billing to be reviewed.</param>
    /// <returns>A GeneratedImmediateBilling object containing the details of the reviewed immediate billing.</returns>
    /// <exception cref="SdkException">If an error occurs during the review process.</exception>
    public GeneratedImmediateBilling ReviewImmediateBilling(PixBilling billing)
    {
        return _immediateBillingClient.ReviewImmediateBilling(_config, billing);
    }

    /// <summary>
    /// Includes a location associated with an immediate billing type.
    /// </summary>
    /// <param name="immediateBillingType">The ImmediateBillingType object containing the details of the location to be included.</param>
    /// <returns>A Location object containing the details of the included location.</returns>
    /// <exception cref="SdkException">If an error occurs during the inclusion process.</exception>
    public Location IncludeLocation(string immediateBillingType)
    {
        return _locationClient.IncludeLocation(_config, immediateBillingType);
    }

    /// <summary>
    /// Retrieves a location by its identifier.
    /// </summary>
    /// <param name="locationId">The identifier of the location to be retrieved.</param>
    /// <returns>A Location object containing the details of the retrieved location.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public Location RetrieveLocation(string locationId)
    {
        return _locationClient.RetrieveLocation(_config, locationId);
    }

    /// <summary>
    /// Retrieves a list of locations for a specified period, optionally filtered.
    /// </summary>
    /// <param name="initialDate">The starting date for the retrieval of locations. Format: YYYY-MM-DD.</param>
    /// <param name="finalDate">The ending date for the retrieval of locations. Format: YYYY-MM-DD.</param>
    /// <param name="filter">The filter criteria for retrieving the locations.</param>
    /// <returns>A list of Location objects containing the details of the retrieved locations.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public List<Location> RetrieveLocationsListInRange(string initialDate, string finalDate, RetrieveLocationFilter filter)
    {
        return _locationClient.RetrieveLocationInRange(_config, initialDate, finalDate, filter);
    }

    /// <summary>
    /// Retrieves a paginated list of locations for a specified period, optionally filtered.
    /// </summary>
    /// <param name="initialDate">The starting date for the retrieval of locations. Format: YYYY-MM-DD.</param>
    /// <param name="finalDate">The ending date for the retrieval of locations. Format: YYYY-MM-DD.</param>
    /// <param name="page">The page number for pagination.</param>
    /// <param name="pageSize">The number of items per page. If null, a default size will be used.</param>
    /// <param name="filter">The filter criteria for retrieving the locations.</param>
    /// <returns>A LocationPage object containing the paginated list of retrieved locations.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public LocationPage RetrieveLocationsListPage(string initialDate, string finalDate, int page, int? pageSize, RetrieveLocationFilter filter)
    {
        return _locationClient.RetrieveLocationPage(_config, initialDate, finalDate, page, pageSize, filter);
    }

    /// <summary>
    /// Unlinks a location by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the location to be unlinked.</param>
    /// <returns>A Location object containing the details of the unlinked location.</returns>
    /// <exception cref="SdkException">If an error occurs during the unlinking process.</exception>
    public Location UnlinkLocation(string id)
    {
        return _locationClient.UnlinkLocation(_config, id);
    }

    /// <summary>
    /// Requests a devolution for a specific transaction.
    /// </summary>
    /// <param name="e2eId">The end-to-end identifier for the transaction.</param>
    /// <param name="id">The identifier of the devolution request.</param>
    /// <param name="devolutionRequestBody">The body containing the details for the devolution request.</param>
    /// <returns>A DetailedDevolution object containing the details of the requested devolution.</returns>
    /// <exception cref="SdkException">If an error occurs during the request process.</exception>
    public DetailedDevolution RequestDevolution(string e2eId, string id, DevolutionRequestBody devolutionRequestBody)
    {
        return _pixClient.RequestDevolution(_config, e2eId, id, devolutionRequestBody);
    }

    /// <summary>
    /// Retrieves the details of a specific devolution by its identifiers.
    /// </summary>
    /// <param name="e2eId">The end-to-end identifier for the transaction.</param>
    /// <param name="id">The identifier of the devolution to be retrieved.</param>
    /// <returns>A DetailedDevolution object containing the details of the retrieved devolution.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public DetailedDevolution RetrieveDevolution(string e2eId, string id)
    {
        return _pixClient.RetrieveDevolution(_config, e2eId, id);
    }

    /// <summary>
    /// Retrieves a list of PIX transactions for a specified period, optionally filtered.
    /// </summary>
    /// <param name="initialDate">The starting date for the retrieval of PIX transactions. Format: YYYY-MM-DD.</param>
    /// <param name="finalDate">The ending date for the retrieval of PIX transactions. Format: YYYY-MM-DD.</param>
    /// <param name="filter">The filter criteria for retrieving the PIX transactions.</param>
    /// <returns>A list of Pix objects containing the details of the retrieved PIX transactions.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public List<Pix> RetrievePixListInRange(string initialDate, string finalDate, RetrievedPixFilter filter)
    {
        return _pixClient.RetrievePixInRange(_config, initialDate, finalDate, filter);
    }

    /// <summary>
    /// Retrieves a paginated list of PIX transactions for a specified period, optionally filtered.
    /// </summary>
    /// <param name="initialDate">The starting date for the retrieval of PIX transactions. Format: YYYY-MM-DD.</param>
    /// <param name="finalDate">The ending date for the retrieval of PIX transactions. Format: YYYY-MM-DD.</param>
    /// <param name="page">The page number for pagination.</param>
    /// <param name="pageSize">The number of items per page. If null, a default size will be used.</param>
    /// <param name="filter">The filter criteria for retrieving the PIX transactions.</param>
    /// <returns>A PixPage object containing the paginated list of retrieved PIX transactions.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public PixPage RetrievePixListPage(string initialDate, string finalDate, int page, int? pageSize, RetrievedPixFilter filter)
    {
        return _pixClient.RetrievePixPage(_config, initialDate, finalDate, page, pageSize, filter);
    }

    /// <summary>
    /// Retrieves the details of a specific PIX transaction by its end-to-end identifier.
    /// </summary>
    /// <param name="e2eId">The end-to-end identifier for the PIX transaction.</param>
    /// <returns>A Pix object containing the details of the retrieved PIX transaction.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public Pix RetrievePix(string e2eId)
    {
        return _pixClient.RetrievePix(_config, e2eId);
    }

    /// <summary>
    /// Retrieves a list of callback responses for a specified period, optionally filtered.
    /// </summary>
    /// <param name="initialDateHour">The starting date and hour for the retrieval of callbacks. Format: YYYY-MM-DD HH:mm.</param>
    /// <param name="finalDateHour">The ending date and hour for the retrieval of callbacks. Format: YYYY-MM-DD HH:mm.</param>
    /// <param name="filter">The filter criteria for retrieving the callback responses.</param>
    /// <returns>A list of RetrieveCallbackResponse objects containing the details of the retrieved callbacks.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public List<RetrieveCallbackResponse> RetrieveCallbacksInRange(string initialDateHour, string finalDateHour, PixCallbackRetrieveFilter filter)
    {
        return _pixWebhookSdk.RetrieveCallbackInRange(_config, initialDateHour, finalDateHour, filter);
    }

    /// <summary>
    /// Retrieves a paginated list of callback responses for a specified period, optionally filtered.
    /// </summary>
    /// <param name="initialDateHour">The starting date and hour for the retrieval of callbacks. Format: YYYY-MM-DD HH:mm.</param>
    /// <param name="finalDateHour">The ending date and hour for the retrieval of callbacks. Format: YYYY-MM-DD HH:mm.</param>
    /// <param name="page">The page number for pagination.</param>
    /// <param name="pageSize">The number of items per page. If null, a default size will be used.</param>
    /// <param name="filter">The filter criteria for retrieving the callback responses.</param>
    /// <returns>A CallbackPage object containing the paginated list of retrieved callbacks.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public PixCallbackPage RetrieveCallbacksPage(string initialDateHour, string finalDateHour, int page, int? pageSize, PixCallbackRetrieveFilter filter)
    {
        return _pixWebhookSdk.RetrieveCallbackPage(_config, initialDateHour, finalDateHour, page, pageSize, filter);
    }

    /// <summary>
    /// Includes a new webhook for a specified key.
    /// </summary>
    /// <param name="key">The identifier key for which the webhook is being included.</param>
    /// <param name="webhookUrl">The URL of the webhook to be included.</param>
    /// <exception cref="SdkException">If an error occurs during the inclusion of the webhook.</exception>
    public void IncludeWebhook(string key, string webhookUrl)
    {
        _pixWebhookSdk.IncludeWebhook(_config, key, webhookUrl);
    }

    /// <summary>
    /// Retrieves the details of a specific webhook by its identifier key.
    /// </summary>
    /// <param name="key">The identifier key for the webhook to be retrieved.</param>
    /// <returns>A Webhook object containing the details of the retrieved webhook.</returns>
    /// <exception cref="SdkException">If an error occurs during the retrieval process.</exception>
    public Webhook RetrieveWebhook(string key)
    {
        return _pixWebhookSdk.RetrieveWebhook(_config, key);
    }

    /// <summary>
    /// Deletes a specific webhook identified by its key.
    /// </summary>
    /// <param name="key">The identifier key for the webhook to be deleted.</param>
    /// <exception cref="SdkException">If an error occurs during the deletion process.</exception>
    public void DeleteWebhook(string key)
    {
        _pixWebhookSdk.DeleteWebhook(_config, key);
    }
}
