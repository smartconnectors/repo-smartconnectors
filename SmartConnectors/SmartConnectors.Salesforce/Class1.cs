using Salesforce.Common;
using Salesforce.Force;
using System;

namespace SmartConnectors.Salesforce
{
    public class Class1
    {
        public class Account
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public Class1()
        {
            var auth = new AuthenticationClient("v44.0");

            var account = new Account() { Name = "New Name", Description = "New Description" };

            var instanceUrl = auth.InstanceUrl;
            var accessToken = auth.AccessToken;
            var apiVersion = auth.ApiVersion;

            var client = new ForceClient(instanceUrl, accessToken, apiVersion);
            var bulkClient = new BulkForceClient(instanceUrl, accessToken, apiVersion);

            var id = await client.CreateAsync("Account", account);

            var accounts = await client.QueryAsync<Account>("SELECT id, name, description FROM Account");

            foreach (var account in accounts.records)
            {
                Console.WriteLine(account.Name);
            }
        }
    }
}
