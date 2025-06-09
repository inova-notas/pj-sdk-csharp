namespace inter_sdk_library;

public class BankingSdk {
    private readonly Config _config;

    private readonly BalanceClient _balanceClient = new();
    private readonly BankStatementClient _bankStatementClient = new();
    private readonly BankingPaymentClient _bankingPaymentClient = new();
    private readonly BankingPixClient _bankingPixClient = new();
    private readonly BankingWebhookClient _bankingWebhookClient = new();
    
    public BankingSdk(Config config) {
        _config = config;
    }

    /// <summary>
    /// Retrieves the statement for a specific period. The maximum period between the dates is 90 days.
    /// </summary>
    /// <param name="initialDate"> Starting date for the statement query. Format: YYYY-MM-DD. </param>
    /// <param name="finalDate"> Ending date for the statement query. Format: YYYY-MM-DD. </param>
    /// <returns> List of transactions. </returns>
    /// <see href="https://developers.inter.co/references/banking#tag/Extrato/operation/Extrato">Consult Statement</see>
    /// <exception cref="SdkException"> If there is an error during the retrieval process. </exception>
    public BankStatement RetrieveStatement(string initialDate, string finalDate)
    {
        return _bankStatementClient.RetrieveStatement(_config, initialDate, finalDate);
    }

    /// <summary>
    /// Retrieves the statement in PDF format for a specific period. The maximum period between the dates is 90 days.
    /// </summary>
    /// <param name="initialDate"> Starting date for the statement export. Format: YYYY-MM-DD. </param>
    /// <param name="finalDate"> Ending date for the statement export. Format: YYYY-MM-DD. </param>
    /// <param name="file"> PDF file that will be saved. </param>
    /// <see href="https://developers.inter.co/references/banking#tag/Extrato/operation/ExtratoExport">Retrieve Statement in PDF</see>
    /// <exception cref="SdkException"> If there is an error during the export process. </exception>
    public void RetrieveStatementInPdf(string initialDate, string finalDate, string file)
    {
        _bankStatementClient.RetrieveStatementInPdf(_config, initialDate, finalDate, file);
    }

    /// <summary>
    /// Retrieves enriched statements within a date range using the specified filters.
    /// </summary>
    /// <param name="initialDate"> Starting date for the query. Format: YYYY-MM-DD. </param>
    /// <param name="finalDate"> Ending date for the query. Format: YYYY-MM-DD. </param>
    /// <param name="filter"> Filters for the query (optional, can be null). </param>
    /// <returns> List of enriched transactions. </returns>
    /// <see href="https://developers.inter.co/references/banking#tag/Extrato/operation/ExtratoComplete">Query Enriched Statement</see>
    /// <exception cref="SdkException"> If there is an error during the retrieval process. </exception>
    public List<EnrichedTransaction> RetrieveEnrichedStatementInRange(string initialDate, string finalDate, FilterRetrieveEnrichedStatement filter)
    {
        return _bankStatementClient.RetrieveStatementInRange(_config, initialDate, finalDate, filter);
    }

    /// <summary>
    /// Retrieves enriched statements with detailed information about each transaction for a specific period. The maximum period between the dates is 90 days.
    /// </summary>
    /// <param name="initialDate"> Starting date for the statement export. Format: YYYY-MM-DD. </param>
    /// <param name="finalDate"> Ending date for the statement export. Format: YYYY-MM-DD. </param>
    /// <param name="filter"> Filters for the query (optional, can be null). </param>
    /// <param name="page"> Page number starting from 0. </param>
    /// <returns> List of enriched transactions. </returns>
    /// <see href="https://developers.inter.co/references/banking#tag/Extrato/operation/ExtratoComplete">Query Enriched Statement</see>
    /// <exception cref="SdkException"> If there is an error during the retrieval process. </exception>
    public EnrichedBankStatementPage RetrieveEnrichedStatementPage(string initialDate, string finalDate, FilterRetrieveEnrichedStatement filter, int page)
    {
        return _bankStatementClient.RetrieveEnrichedStatementPage(_config, initialDate, finalDate, page, null, filter);
    }

    /// <summary>
    /// Retrieves enriched statements with detailed information about each transaction for a specific period. The maximum period between the dates is 90 days.
    /// </summary>
    /// <param name="initialDate"> Starting date for the statement export. Format: YYYY-MM-DD. </param>
    /// <param name="finalDate"> Ending date for the statement export. Format: YYYY-MM-DD. </param>
    /// <param name="filter"> Filters for the query (optional, can be null). </param>
    /// <param name="page"> Page number starting from 0. </param>
    /// <param name="pageSize"> Size of the page, default = 50. </param>
    /// <returns> List of enriched transactions. </returns>
    /// <see href="https://developers.inter.co/references/banking#tag/Extrato/operation/ExtratoComplete">Query Enriched Statement</see>
    /// <exception cref="SdkException"> If there is an error during the retrieval process. </exception>
    public EnrichedBankStatementPage RetrieveEnrichedStatement(string initialDate, string finalDate, FilterRetrieveEnrichedStatement filter, int page, int pageSize)
    {
        return _bankStatementClient.RetrieveEnrichedStatementPage(_config, initialDate, finalDate, page, pageSize, filter);
    }

    /// <summary>
    /// Retrieves the balance for a specific period.
    /// </summary>
    /// <param name="balanceDate"> Date for querying the positional balance. Format: YYYY-MM-DD. </param>
    /// <returns> Object containing the account balances as of the specified date. </returns>
    /// <see href="https://developers.inter.co/references/banking#tag/Saldo/operation/Saldo">Query Balance</see>
    /// <exception cref="SdkException"> If there is an error during the retrieval process. </exception>
    public Balance RetrieveBalance(string balanceDate)
    {
        return _balanceClient.RetrieveBalance(_config, balanceDate);
    }

    /// <summary>
    /// Method for including an immediate payment or scheduling the payment of a billet, agreement, or tax with a barcode.
    /// </summary>
    /// <param name="payment"> Payment data </param>
    /// <returns> Object containing quantity of approvers, payment status, transaction code, etc. </returns>
    /// <see href="https://developers.inter.co/references/banking#tag/Pagamento/operation/pagarBoleto">Include Payment with Barcode</see>
    /// <exception cref="SdkException"> If there is an error during the payment process. </exception>
    public IncludePaymentResponse IncludePayment(BilletPayment payment)
    {
        return _bankingPaymentClient.IncludeBilletPayment(_config, payment);
    }

    /// <summary>
    /// Retrieves information about billets payments.
    /// </summary>
    /// <param name="initialDate"> Starting date, according to the "filterDateBy" field. Accepted format: YYYY-MM-DD. </param>
    /// <param name="finalDate"> Ending date, according to the "filterDateBy" field. Accepted format: YYYY-MM-DD. </param>
    /// <param name="filter"> Filters for the query (optional, can be null). </param>
    /// <returns> List of payments. </returns>
    /// <see href="https://developers.inter.co/references/banking#tag/Pagamento/operation/buscarInformacoesPagamentos">Retrieve Payments</see>
    /// <exception cref="SdkException"> If there is an error during the retrieval process. </exception>
    public List<Payment> RetrievePaymentsInRange(string initialDate, string finalDate, PaymentSearchFilter filter)
    {
        return _bankingPaymentClient.RetrieveBilletPaymentList(_config, initialDate, finalDate, filter);
    }

    /// <summary>
    /// Method for including an immediate DARF payment without a barcode.
    /// </summary>
    /// <param name="payment"> Payment data </param>
    /// <returns> Object containing authentication, operation number, return type, transaction code, etc. </returns>
    /// <see href="https://developers.inter.co/references/banking#tag/Pagamento/operation/pagamentosDarf">Include DARF Payment</see>
    /// <exception cref="SdkException"> If there is an error during the payment process. </exception>
    public IncludeDarfPaymentResponse IncludeDarfPayment(DarfPayment payment)
    {
        return _bankingPaymentClient.IncludeDarfPayment(_config, payment);
    }

    /// <summary>
    /// Retrieves information about DARF payments.
    /// </summary>
    /// <param name="initialDate"> Starting date, according to the "filterDateBy" field. Accepted format: YYYY-MM-DD. </param>
    /// <param name="finalDate"> Ending date, according to the "filterDateBy" field. Accepted format: YYYY-MM-DD. </param>
    /// <param name="filter"> Filters for the query (optional, can be null). </param>
    /// <returns> List of payments. </returns>
    /// <see href="https://developers.inter.co/references/banking#tag/Pagamento/operation/buscarInformacoesPagamentoDarf">Retrieve DARF Payments</see>
    /// <exception cref="SdkException"> If there is an error during the retrieval process. </exception>
    public List<DarfPaymentResponse> RetrieveDarfPaymentsInRange(string initialDate, string finalDate, DarfPaymentSearchFilter filter)
    {
        return _bankingPaymentClient.RetrieveDarfPaymentList(_config, initialDate, finalDate, filter);
    }

    /// <summary>
    /// Inclusion of a batch of payments entered by the client.
    /// </summary>
    /// <param name="myIdentifier"> Identifier for the batch for the client. </param>
    /// <param name="payments"> Payments to be processed. </param>
    /// <returns> Information regarding the batch processing. </returns>
    /// <see href="https://developers.inter.co/references/banking#tag/Pagamento/operation/pagamentosLote">Include Batch Payments</see>
    /// <exception cref="SdkException"> If there is an error during the batch processing. </exception>
    public IncludeBatchPaymentResponse IncludeBatchPayment(string myIdentifier, List<BatchItem> payments)
    {
        return _bankingPaymentClient.IncludePaymentInBatch(_config, myIdentifier, payments);
    }

    /// <summary>
    /// Retrieves a batch of payments entered by the client.
    /// </summary>
    /// <param name="batchId"> Identifier for the batch. </param>
    /// <returns> Information regarding the batch processing. </returns>
    /// <see href="https://developers.inter.co/references/banking#tag/Pagamento/operation/buscarInformacoesPagamentoLote">Retrieve Batch Payments</see>
    /// <exception cref="SdkException"> If there is an error during the retrieval process. </exception>
    public BatchProcessing RetrievePaymentBatch(string batchId)
    {
        return _bankingPaymentClient.RetrieveBatch(_config, batchId);
    }

    /// <summary>
    /// Method for including a Pix payment/transfer using banking data or a key.
    /// </summary>
    /// <param name="pix"> Pix data </param>
    /// <returns> Object containing endToEndId, etc. </returns>
    /// <see href="https://developers.inter.co/references/banking#tag/Pix-Pagamento/operation/realizarPagamentoPix">Include Pix</see>
    /// <exception cref="SdkException"> If there is an error during the Pix payment process. </exception>
    public IncludePixResponse IncludePix(BankingPix pix)
    {
        return _bankingPixClient.IncludePixPayment(_config, pix);
    }

    /// <summary>
    /// Method for retrieving a Pix payment/transfer.
    /// </summary>
    /// <param name="requestCode"> Pix data </param>
    /// <returns> Object containing endToEndId, etc. </returns>
    /// <see href="https://developers.inter.co/references/banking#tag/Pix-Pagamento/operation/consultarPagamentoPix">Include Pix</see>
    /// <exception cref="SdkException"> If there is an error during the retrieval process. </exception>
    public RetrievePixResponse RetrievePix(string requestCode)
    {
        return _bankingPixClient.RetrievePixPayment(_config, requestCode);
    }

    /// <summary>
    /// Method intended to create a webhook to receive notifications for confirmation of Pix payments (callbacks).
    /// </summary>
    /// <param name="webhookType"> The type of webhook. </param>
    /// <param name="webhookUrl"> The client's HTTPS server URL. </param>
    /// <see href="https://developers.inter.co/references/banking#tag/Webhook/operation/putWebhookBanking">Create Webhook</see>
    /// <exception cref="SdkException"> If there is an error during the webhook creation process. </exception>
    public void IncludeWebhook(string webhookType, string webhookUrl)
    {
        _bankingWebhookClient.IncludeWebhook(_config, webhookType, webhookUrl);
    }

    /// <summary>
    /// Retrieve the registered webhook.
    /// </summary>
    /// <param name="webhookType"> The type of webhook. </param>
    /// <returns> Webhook object. </returns>
    /// <see href="https://developers.inter.co/references/banking#tag/Webhook/operation/getWebhookBanking">Retrieve Registered Webhook</see>
    /// <exception cref="SdkException"> If there is an error during the retrieval process. </exception>
    public Webhook RetrieveWebhook(string webhookType)
    {
        return _bankingWebhookClient.RetrieveWebhook(_config, webhookType);
    }

    /// <summary>
    /// Deletes the webhook.
    /// </summary>
    /// <param name="webhookType"> The type of webhook to delete. </param>
    /// <see href="https://developers.inter.co/references/banking#tag/Webhook/operation/deleteWebhookBanking">Delete Webhook</see>
    /// <exception cref="SdkException"> If there is an error during the deletion process. </exception>
    public void DeleteWebhook(string webhookType)
    {
        _bankingWebhookClient.DeleteWebhook(_config, webhookType);
    }

    /// <summary>
    /// Retrieves a collection of callbacks for a specific period, according to the provided parameters, without pagination.
    /// </summary>
    /// <param name="webhookType"> The type of webhook. </param>
    /// <param name="initialDateHour"> Starting date, according to the "filterDateBy" field. Accepted format: YYYY-MM-DD. </param>
    /// <param name="finalDateHour"> Ending date, according to the "filterDateBy" field. Accepted format: YYYY-MM-DD. </param>
    /// <param name="filter"> Filters for the query (optional, can be null). </param>
    /// <returns> List of callbacks. </returns>
    /// <see href="https://developers.inter.co/references/banking#tag/Webhook/operation/callbacksFilter">Retrieve Collection of billets</see>
    /// <exception cref="SdkException"> If there is an error during the retrieval process. </exception>
    public List<RetrieveCallbackResponse> RetrieveCallbackInRange(string webhookType, string initialDateHour, string finalDateHour, CallbackRetrieveFilter filter)
    {
        return _bankingWebhookClient.RetrieveCallbacksInRange(_config, webhookType, initialDateHour, finalDateHour, filter);
    }

    /// <summary>
    /// Retrieves a collection of billets for a specific period, according to the provided parameters, with pagination.
    /// </summary>
    /// <param name="webhookType"> The type of webhook. </param>
    /// <param name="initialDateHour"> Starting date, according to the "filterDateBy" field. Accepted format: YYYY-MM-DD. </param>
    /// <param name="finalDateHour"> Ending date, according to the "filterDateBy" field. Accepted format: YYYY-MM-DD. </param>
    /// <param name="filter"> Filters for the query (optional, can be null). </param>
    /// <param name="page"> Page number for pagination. </param>
    /// <param name="pageSize"> Size of the page. </param>
    /// <returns> Page with a list of billets. </returns>
    /// <see href="https://developers.inter.co/references/banking#tag/Webhook/operation/callbacksFilter">Retrieve Collection of billets</see>
    /// <exception cref="SdkException"> If there is an error during the retrieval process. </exception>
    public CallbackPage RetrieveCallbacksPage(string webhookType, string initialDateHour, string finalDateHour, CallbackRetrieveFilter filter, int page, int pageSize)
    {
        return _bankingWebhookClient.RetrieveCallbackPage(_config, webhookType, initialDateHour, finalDateHour, page, null, filter);
    }

    /// <summary>
    /// Cancels the scheduling of a payment.
    /// </summary>
    /// <param name="transactionCode"> Unique transaction code. </param>
    /// <exception cref="SdkException"> If there is an error during the cancellation process. </exception>
    public void PaymentSchedulingCancel(string transactionCode)
    {
        _bankingPaymentClient.CancelPayment(_config, transactionCode);
    }
}
