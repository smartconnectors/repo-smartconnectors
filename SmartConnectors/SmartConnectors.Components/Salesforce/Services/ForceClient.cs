using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using SmartConnectors.Components.Salesforce.Model.Json;
using SmartConnectors.Components.Salesforce.Common;
using SmartConnectors.Components.Salesforce.ViewModels;

namespace SmartConnectors.Components.Salesforce.Services
{
    public class ForceClient
    {
        private string _apiVersion { get; set; }
        private string _instanceUrl { get; set; }
        private string _accessToken { get; set; }
        private string _clientName { get; set; }
        private Uri _uri { get; set; }

        public ForceClient(QueryRequest req)
        {
            _accessToken = req.Token;
            _apiVersion = req.ApiVersion;
            _clientName = req.ClientName;
            _instanceUrl = req.InstanceUrl;
            
            _uri = UriFormatter.DescribeGlobal(_instanceUrl, _apiVersion);
        }

        /// <summary>
        /// Retrieve (basic) metadata for an object.
        /// <para>Use the SObject Basic Information resource to retrieve metadata for an object.</para>
        /// </summary>
        /// <param name="objectTypeName">SObject name, e.g. Account</param>
        /// <returns>Returns SObjectMetadataBasic with basic object metadata and a list of recently created items.</returns>
        public async Task<SObjectBasicInfo> GetObjectBasicInfo(string objectTypeName)
        {
            try
            {
                Dictionary<string, string> headers = HeaderFormatter.SforceCallOptions(_clientName);

                //need to get the token from user_authentication
                using (var httpClient = new HttpClient())
                {
                    var client = new JsonClient(_accessToken, httpClient);
                    return await client.HttpGetAsync<SObjectBasicInfo>(_uri, headers);
                }
            }
            catch (Exception ex)
            { 
                return null; 
            }
        }

        /// <summary>
        /// Get a List of Objects
        /// <para>Use the Describe Global resource to list the objects available in your org and available to the logged-in user. This resource also returns the org encoding, as well as maximum batch size permitted in queries.</para>
        /// </summary>
        /// <returns>Returns DescribeGlobal object with a SObjectDescribe collection</returns>
        public async Task<DescribeGlobal> DescribeGlobal()
        {
            try
            {

                var uri = UriFormatter.DescribeGlobal(_instanceUrl, _apiVersion);
                using (var httpClient = new HttpClient())
                {
                    JsonClient client = new JsonClient(_accessToken, httpClient);

                    return await client.HttpGetAsync<DescribeGlobal>(uri);
                }
            }
            catch (Exception ex)
            { return null; }
        }

        /// <summary>
        /// Get field and other metadata for an object.
        /// <para>Use the SObject Describe resource to retrieve all the metadata for an object, including information about each field, URLs, and child relationships.</para>
        /// </summary>
        /// <param name="objectTypeName">SObject name, e.g. Account</param>
        /// <returns>Returns SObjectMetadataAll with full object meta including field metadata</returns>
        public async Task<SObjectDescribeFull> GetObjectDescribe(string objectTypeName)
        {
            var uri = UriFormatter.SObjectDescribe(_instanceUrl, _apiVersion, objectTypeName);

            using (var httpClient = new HttpClient())
            {
                JsonClient client = new JsonClient(_accessToken, httpClient);

                return await client.HttpGetAsync<SObjectDescribeFull>(uri);
            }
        }
        /// <summary>
        /// Get current user's info via Identity URL
        /// <para>https://developer.salesforce.com/docs/atlas.en-us.mobile_sdk.meta/mobile_sdk/oauth_using_identity_urls.htm</para>
        /// </summary>
        /// <param name="identityUrl"></param>
        /// <returns>UserInfo</returns>
        public async Task<UserInfo> GetUserInfo(string identityUrl)
        {
            using (var httpClient = new HttpClient())
            {
                JsonClient client = new JsonClient(_accessToken, httpClient);

                return await client.HttpGetAsync<UserInfo>(new Uri(identityUrl));
            }
        }
        /// <summary>
        /// Lists information about limits in your org.
        /// <para>This resource is available in REST API version 29.0 and later for API users with the View Setup and Configuration permission.</para>
        /// </summary>
        public async Task<OrganizationLimits> GetOrganizationLimits()
        {
            var uri = UriFormatter.Limits(_instanceUrl, _apiVersion);

            using (var httpClient = new HttpClient())
            {
                JsonClient client = new JsonClient(_accessToken, httpClient);

                return await client.HttpGetAsync<OrganizationLimits>(uri);
            }
        }
        //soql
        /// <summary>
        /// Retrieve records using a SOQL query.
        /// <para>Will automatically retrieve the complete result set if split into batches. If you want to limit results, use the LIMIT operator in your query.</para>
        /// </summary>
        /// <param name="queryString">SOQL query string, without any URL escaping/encoding</param>
        /// <param name="queryAll">True if deleted records are to be included</param>
        /// <returns>List{T} of results</returns>
        public async Task<List<T>> Query<T>(string queryString, bool queryAll = false)
        {
#if DEBUG
            Stopwatch sw = new Stopwatch();
            sw.Start();
#endif
            try
            {
                Dictionary<string, string> headers = HeaderFormatter.SforceCallOptions(_clientName);
                var queryUri = UriFormatter.Query(_instanceUrl, _apiVersion, queryString, queryAll);

                using (var httpClient = new HttpClient())
                {
                    JsonClient client = new JsonClient(_accessToken, httpClient);

                    List<T> results = new List<T>();

                    bool done = false;
                    string nextRecordsUrl = string.Empty;

                    //larger result sets will be split into batches (sized according to system and account settings)
                    //if additional batches are indicated retrieve the rest and append to the result set.
                    do
                    {
                        if (!string.IsNullOrEmpty(nextRecordsUrl))
                        {
                            queryUri = new Uri(new Uri(_instanceUrl), nextRecordsUrl);
                        }

                        QueryResult<T> qr = await client.HttpGetAsync<QueryResult<T>>(queryUri, headers);

#if DEBUG
                        Debug.WriteLine(string.Format("Got query resuts, {0} totalSize, {1} in this batch, final batch: {2}",
                            qr.TotalSize, qr.Records.Count.ToString(), qr.Done.ToString()));
#endif

                        results.AddRange(qr.Records);

                        done = qr.Done;

                        nextRecordsUrl = qr.NextRecordsUrl;

                        if (!done && string.IsNullOrEmpty(nextRecordsUrl))
                        {
                            //Normally if query has remaining batches, NextRecordsUrl will have a value, and Done will be false.
                            //In case of some unforseen error, flag the result as done if we're missing the NextRecordsUrl
                            //In this situation we'll just get the previous set again and be stuck in a loop.
                            done = true;
                        }

                    } while (!done);

#if DEBUG
                    sw.Stop();
                    Debug.WriteLine(string.Format("Query results retrieved in {0}ms", sw.ElapsedMilliseconds.ToString()));
#endif

                    return results;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error querying: " + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// Get a basic SOQL COUNT() query result
        /// <para>The query must start with SELECT COUNT() FROM, with no named field in the count clause. COUNT() must be the only element in the SELECT list.</para>
        /// </summary>
        /// <param name="queryString">SOQL query string starting with SELECT COUNT() FROM</param>
        /// <param name="queryAll">True if deleted records are to be included</param>
        /// <returns>The <see cref="Task{Int}"/> returning the count</returns>
        public async Task<int> CountQuery(string queryString, bool queryAll = false)
        {
            // https://developer.salesforce.com/docs/atlas.en-us.soql_sosl.meta/soql_sosl/sforce_api_calls_soql_select_count.htm
            // COUNT() must be the only element in the SELECT list.

            Dictionary<string, string> headers = HeaderFormatter.SforceCallOptions(_clientName);

            if (!queryString.Replace(" ", "").ToLower().StartsWith("selectcount()from"))
            {
                throw new ForceApiException("CountQueryAsync may only be used with a query starting with SELECT COUNT() FROM");
            }
            using (var httpClient = new HttpClient())
            {

                var jsonClient = new JsonClient(_accessToken, httpClient);
                var uri = UriFormatter.Query(_instanceUrl, _apiVersion, queryString);
                var qr = await jsonClient.HttpGetAsync<QueryResult<object>>(uri, headers);

                return qr.TotalSize;
            }
        }
        /// <summary>
        /// Get SObject by ID
        /// </summary>
        /// <param name="sObjectTypeName">SObject name, e.g. "Account"</param>
        /// <param name="objectId">SObject ID</param>
        /// <param name="fields">(optional) List of fields to retrieve, if not supplied, all fields are retrieved.</param>
        public async Task<T> GetObjectById<T>(string sObjectTypeName, string objectId, List<string> fields = null)
        {
            Dictionary<string, string> headers = HeaderFormatter.SforceCallOptions(_clientName);
            var uri = UriFormatter.SObjectRows(_instanceUrl, _apiVersion, sObjectTypeName, objectId, fields);
            using (var httpClient = new HttpClient())
            {
                JsonClient client = new JsonClient(_accessToken, httpClient);

                return await client.HttpGetAsync<T>(uri, headers);
            }
        }
        // <summary>
        /// Create a new record
        /// </summary>
        /// <param name="sObjectTypeName">SObject name, e.g. "Account"</param>
        /// <param name="sObject">Object to create</param>
        /// <param name="customHeaders">Custom headers to include in request (Optional). await The HeaderFormatter helper class can be used to generate the custom header as needed.</param>
        /// <returns>CreateResponse object, includes new object's ID</returns>
        /// <exception cref="ForceApiException">Thrown when creation fails</exception>

        public async Task<CreateResponse> CreateRecord<T>(string sObjectTypeName, T sObject, Dictionary<string, string> customHeaders = null)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            //Add call options
            Dictionary<string, string> callOptions = HeaderFormatter.SforceCallOptions(_clientName);
            headers.AddRange(callOptions);

            //Add custom headers if specified
            if (customHeaders != null)
            {
                headers.AddRange(customHeaders);
            }

            var uri = UriFormatter.SObjectBasicInformation(_instanceUrl, _apiVersion, sObjectTypeName);
            using (var httpClient = new HttpClient())
            {
                JsonClient client = new JsonClient(_accessToken, httpClient);

                return await client.HttpPostAsync<CreateResponse>(sObject, uri, headers);
            }
        }
        /// <summary>
        /// Updates
        /// </summary>
        /// <param name="sObjectTypeName">SObject name, e.g. "Account"</param>
        /// <param name="objectId">Id of Object to update</param>
        /// <param name="sObject">Object to update</param>
        /// <param name="customHeaders">Custom headers to include in request (Optional). await The HeaderFormatter helper class can be used to generate the custom header as needed.</param>
        /// <returns>void, API returns 204/NoContent</returns>
        /// <exception cref="ForceApiException">Thrown when update fails</exception>
        public async Task UpdateRecord<T>(string sObjectTypeName, string objectId, T sObject, Dictionary<string, string> customHeaders = null)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            //Add call options
            Dictionary<string, string> callOptions = HeaderFormatter.SforceCallOptions(_clientName);
            headers.AddRange(callOptions);

            //Add custom headers if specified
            if (customHeaders != null)
            {
                headers.AddRange(customHeaders);
            }

            var uri = UriFormatter.SObjectRows(_instanceUrl, _apiVersion, sObjectTypeName, objectId);
            using (var httpClient = new HttpClient())
            {
                JsonClient client = new JsonClient(_accessToken, httpClient);

                await client.HttpPatchAsync<object>(sObject, uri, headers);

                return;
            }
        }
        /// <summary>
        /// Inserts or Updates a records based on external id
        /// </summary>
        /// <param name="sObjectTypeName">SObject name, e.g. "Account"</param>
        /// <param name="fieldName">External ID field name</param>
        /// <param name="fieldValue">External ID field value</param>
        /// <param name="sObject">Object to update</param>
        /// <param name="customHeaders">Custom headers to include in request (Optional). await The HeaderFormatter helper class can be used to generate the custom header as needed.</param>
        /// <returns>CreateResponse object, includes new object's ID if record was created and no value if object was updated</returns>
        /// <exception cref="ForceApiException">Thrown when request fails</exception>
        public async Task<CreateResponse> InsertOrUpdateRecord<T>(string sObjectTypeName, string fieldName, string fieldValue, T sObject, Dictionary<string, string> customHeaders = null)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            //Add call options
            Dictionary<string, string> callOptions = HeaderFormatter.SforceCallOptions(_clientName);
            headers.AddRange(callOptions);

            //Add custom headers if specified
            if (customHeaders != null)
            {
                headers.AddRange(customHeaders);
            }

            var uri = UriFormatter.SObjectRowsByExternalId(_instanceUrl, _apiVersion, sObjectTypeName, fieldName, fieldValue);
            using (var httpClient = new HttpClient())
            {
                JsonClient client = new JsonClient(_accessToken, httpClient);

                return await client.HttpPatchAsync<CreateResponse>(sObject, uri, headers);
            }
        }
        /// <summary>
        /// Delete record
        /// </summary>
        /// <param name="sObjectTypeName">SObject name, e.g. "Account"</param>
        /// <param name="objectId">Id of Object to update</param>
        /// <returns>void, API returns 204/NoContent</returns>
        /// <exception cref="ForceApiException">Thrown when update fails</exception>
        public async Task DeleteRecord(string sObjectTypeName, string objectId)
        {
            Dictionary<string, string> headers = HeaderFormatter.SforceCallOptions(_clientName);
            var uri = UriFormatter.SObjectRows(_instanceUrl, _apiVersion, sObjectTypeName, objectId);
            using (var httpClient = new HttpClient())
            {
                JsonClient client = new JsonClient(_accessToken, httpClient);

                await client.HttpDeleteAsync<object>(uri, headers);

                return;
            }
        }

    }

}