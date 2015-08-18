using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using Domain;

namespace Broker
{
    internal class AutenticationProvider
    {
        internal AutenticationProvider()
        {
            tokenDictionary = new ConcurrentDictionary<string, AutenticationToken>();
        }

        internal ConcurrentDictionary<String, AutenticationToken> tokenDictionary { get; private set; }

        internal string AutenticateUser(Credential credential)
        {
            /* Token de teste */
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            var tokenData = new byte[64];
            rng.GetBytes(tokenData);
            string s = Convert.ToBase64String(tokenData);
            var token = new AutenticationToken(credential);
            tokenDictionary.TryAdd(s, token);
            return s;
        }

        internal AutenticationToken GetAutenticationToken(string token)
        {
            AutenticationToken authtoken = null;
            tokenDictionary.TryGetValue(token, out authtoken);
            return authtoken;
        }

        internal bool RemoveAuthToken(string authToken)
        {
            AutenticationToken token;
            return tokenDictionary.TryRemove(authToken, out token);
        }
    }
}