using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayPal.Api;

namespace CloudKids.Web.Models
{
    public static class PaypalConfiguration
    {
        //Variables for storing the clientID and clientSecret key
        public readonly static string ClientId;
        public readonly static string ClientSecret;

        //Constructor
        static PaypalConfiguration()
        {
            var config = GetConfig();
            //ClientId = config["clientId"];
            //ClientSecret = config["clientSecret"];

            ClientId = "ASOZiqSe3YbvcIeLmqRuezrsLrBSk3ceE1eeKq-YXv2hfSRuZuHcY8j2S1MkZ1c4npwHet39JgYtG1nb";
            ClientSecret = "EHGw1kL8Uhr5pGS7z5naVlxw4jj0wpuYdGCTYNRAwsxSyLQK1yFWLtsgzHB8xFIlIsyTKMMtm4zVbei2";

        }

        // getting properties from the web.config
        public static Dictionary<string, string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            // getting accesstocken from paypal               
            string accessToken = new OAuthTokenCredential
        (ClientId, ClientSecret, GetConfig()).GetAccessToken();

            return accessToken;
        }

        public static APIContext GetAPIContext()
        {
            // return apicontext object by invoking it with the accesstoken
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }
    }
}