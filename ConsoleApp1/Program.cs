using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterUsers();

            Console.ReadKey();
        }

        public static string GenerateSignature(string key, string message)
        {
            HMAC hmac = HMAC.Create("HMACSHA256");
            hmac.Key = Encoding.UTF8.GetBytes(key);
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            return BitConverter.ToString(hash).Replace("-", "").ToUpperInvariant();
        }

        public static void RegisterUsers()
        {
            string url = "https://api.oddsgames.com/api/auth/token";

            string result = string.Empty;

            string ClientId = "liga-stavok-web";

            string Nonce = "1";

            //string Email = "liga-stavok-web@sandbox.today";
            string Email = "liga-stavok-web@oddsgames.com";

            //string message = ClientId + Nonce + Email;
            string message = $"{ClientId}{Email}{Nonce}";

            //string key = "E93E8A55-EFF1-40DE-BC00-54614E8BE2F8";
            string key = "7CFA6C73-04F1-411E-9EEF-54A094FBF7C1";

            string signature = GenerateSignature(key, message);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.UseDefaultCredentials = false;
            request.Credentials = new NetworkCredential("api-liga-stavok-apiaccess", "Api_Li20Smart00$");
            request.Headers["ClientId"] = ClientId;
            request.Headers["Nonce"] = Nonce;
            request.Headers["Signature"] = signature;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            dynamic jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(result);

            string token = (string)jsonResult["token"];

            //get session token
            //------------------------

            url = "https://api.oddsgames.com/api/session/create";

            string data =
                "{\"deviceIdentifier\": \"sessionCreator\",\"userAgent\": \"sessionCreator\",\"ipAddress\": \"207.77.104.20\",\"appId\": \"00000000-0000-0000-0000-000000000000\"}";

            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(url);
            request2.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request2.ContentLength = dataBytes.Length;
            request2.ContentType = "application/json";
            request2.Method = "POST";

            request2.UseDefaultCredentials = false;
            request2.Credentials = new NetworkCredential("api-liga-stavok-apiaccess", "Api_Li20Smart00$");
            request2.Headers["Authorization"] = string.Format("Bearer {0}", token);
            request2.Headers["Session"] = "test";

            using (Stream requestBody = request2.GetRequestStream())
            {
                requestBody.Write(dataBytes, 0, dataBytes.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)request2.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            dynamic jsonResult2 = Newtonsoft.Json.JsonConvert.DeserializeObject(result);

            string sessionToken = (string)jsonResult2["sessionToken"];

            //----------------------------

            url = "https://api.oddsgames.com/api/data/user/register";

            string userNameStart = "newUserNameGTeGot";

            for (int i = 942; i < 20000; i++)
            {
                string userName = userNameStart + i;
                
                string data3 =
                    "{" +
                    "\"password\": \"password123\"," +
                    "\"bonusAcceptance\": 1," +
                    "\"externalIdentifier\": \"strin23ggfhUsser\"," +
                    "\"logonName\": \"" + userName + "\"," +
                    "\"branchId\": 1," +
                    "\"currencyId\": 126," +
                    "\"promotionCode\": \"string\"," +
                    "\"profile\": {" +
                    "\"id\": 0," +
                    "\"title\": \"string222\"," +
                    "\"gender\": 2," +
                    "\"firstName\": \"string222\"," +
                    "\"lastName\": \"string888\"," +
                    "\"birthday\": \"1980-09-11\"," +
                    "\"birthPlace\": \"string\"," +
                    "\"birthName\": \"string\"," +
                    "\"languageId\": 1," +
                    "\"nationalityCountryId\": 1," +
                    "\"phoneNumbers\": [" +
                    "{" +
                    "\"id\": 1," +
                    "\"name\": \"string\"," +
                    "\"type\": 1" +
                    "}" +
                    "]," +
                    "\"nationalIdentityId\": \"string\"" +
                    "}," +
                    "\"primaryAddress\": {" +
                    "\"poBox\": \"string\"," +
                    "\"streetAddress\": \"string\"," +
                    "\"zipCode\": \"string\"," +
                    "\"region\": \"string\"," +
                    "\"countryId\": 1," +
                    "\"city\": \"string\"" +
                    "}," +
                    "\"secondaryAddress\": {" +
                    "\"poBox\": \"string\"," +
                    "\"streetAddress\": \"string\"," +
                    "\"zipCode\": \"string\"," +
                    "\"region\": \"string\"," +
                    "\"countryId\": 1," +
                    "\"city\": \"string\"" +
                    "}," +
                    "\"eMail\": {" +
                    "\"name\": \"" + userName + "@email.com\"," +
                    "\"protocol\": 1" +
                    "}," +
                    "\"emailAddresses\": [" +
                    "\"" + userName + "@email.com\"" +
                    "]," +
                    "\"passwordRecoveryQuestionId\": 1," +
                    "\"passwordRecoveryAnswer\": \"string\"," +
                    "\"languageId\": 1," +
                    "\"timeZoneId\": 1," +
                    "\"cultureInfoIdentifier\": \"string\"," +
                    "\"sendNewsLetterEmail\": false," +
                    "\"betPin\": \"string\"," +
                    "\"cardNumber\": \"string\"," +
                    "\"phonePin\": \"string\"," +
                    "\"rfidNumber\": \"" + userName + "\"" +
                    "}";


                byte[] dataBytes3 = Encoding.UTF8.GetBytes(data3);

                HttpWebRequest request3 = (HttpWebRequest)WebRequest.Create(url);
                request3.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request3.ContentLength = dataBytes3.Length;
                request3.ContentType = "application/json";
                request3.Method = "POST";

                request3.UseDefaultCredentials = false;
                request3.Credentials = new NetworkCredential("api-liga-stavok-apiaccess", "Api_Li20Smart00$");
                request3.Headers["Authorization"] = string.Format("Bearer {0}", token);
                request3.Headers["Session"] = sessionToken;

                using (Stream requestBody = request3.GetRequestStream())
                {
                    requestBody.Write(dataBytes3, 0, dataBytes3.Length);
                }

                try
                {
                    using (HttpWebResponse response = (HttpWebResponse) request3.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        result = reader.ReadToEnd();
                    }

                    dynamic jsonResult3 = Newtonsoft.Json.JsonConvert.DeserializeObject(result);

                    string registrationErrors = (string) jsonResult3["errors"];

                    if (registrationErrors == "0")
                    {
                        Console.WriteLine($"{i}. Register user {userName}.");
                    }
                }
                catch
                {
                    i--;
                }

                //System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
