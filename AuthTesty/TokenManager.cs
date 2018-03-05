using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;


namespace AuthTesty
{
    public class TokenManager
    {
        private const string Secret = "Api457887Secret122Cod4875eExamples963Samples";
        public string IssueToken(IDictionary<string,object> parametrs)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(parametrs, Secret);
            return token;
        }

        public IDictionary<string,string> DecodedToken(string token)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider dataProvider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, dataProvider);
            IBase64UrlEncoder encoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, encoder);
            var decoded = decoder.DecodeToObject<IDictionary<string, string>>(token, Secret, true);
            return decoded;
        }
    }
}