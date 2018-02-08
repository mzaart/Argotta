using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.WebUtilities;
using Multilang.Services.ConfigurationServices;
using Multilang.Utils;
using System.Text;

namespace Multilang.Services.AuthTokenServices
{
    public class JwtService<T> : IAuthTokenService<T>
    {
        private string key;

        public JwtService(IConfigService config) {
            this.key = config.GetJwtKey();
        }

        string IAuthTokenService<T>.Issue(T data)
        {
            var head = new  { typ = "JWT", alg = "HS256" };
            string headEncoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(head)));
            string bodyEncoded =  WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(data)));   

            byte[] signature = Hash.HMACSHA256(Hash.HexToByte(key), 
                Encoding.UTF8.GetBytes(headEncoded + "." + bodyEncoded));    
            string signatureEncoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(signature)));      
            
            return headEncoded + "." + bodyEncoded + "." + signatureEncoded;
        }

        bool IAuthTokenService<T>.IsValid(string jwtTokenBase64)
        {
            if (jwtTokenBase64 == null)
            {
                return false;
            }
            
            // check if format is valid
            string regex = @"^[^\.]+\.[^\.]+\.[^\.]+$";
            if(!(new Regex(regex).IsMatch(jwtTokenBase64))) 
            {
                return false;
            }

            // check signature
            string[] jwt = jwtTokenBase64.Split('.');
            byte[] signature = Hash.HMACSHA256(Hash.HexToByte(key), 
                Encoding.UTF8.GetBytes(jwt[0] + "." + jwt[1]));
            
            return WebEncoders.Base64UrlEncode(signature) == jwt[2];
        }

        T IAuthTokenService<T>.GetData(string jwtTokenBase64)
        {
            string[] jwt = jwtTokenBase64.Split('.');
            string json = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(jwt[1]));
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}