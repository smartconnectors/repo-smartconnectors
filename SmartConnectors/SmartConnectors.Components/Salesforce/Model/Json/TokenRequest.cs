using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
namespace SmartConnectors.Components.Salesforce.Model.Json
{
    public class TokenRequest
    {
        [JsonProperty(PropertyName = "grant_type")]
        public string GrantType { get; set; }

         

        [JsonProperty(PropertyName = "securityToken")]
        public string SecurityToken { get; set; }

        [JsonProperty(PropertyName = "redirect_uri")]
        public string RedirectUri { get; set; }

        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        public FormUrlEncodedContent GetUrlEncoded()
        {
            return new System.Net.Http.FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", GrantType),
                     new KeyValuePair<string, string>("securityToken", SecurityToken ),
                    new KeyValuePair<string, string>("redirect_uri", RedirectUri),
                    new KeyValuePair<string, string>("code", Code)
                });
        }
    }
}
 