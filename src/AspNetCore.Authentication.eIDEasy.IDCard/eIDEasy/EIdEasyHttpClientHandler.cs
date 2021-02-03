﻿using System;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace AspNetCore.Authentication.eIDEasy.IDCard.eIDEasy
{
    public class EIdEasyHttpClientHandler : HttpClientHandler
    {
        private readonly string _serverCertificatePublicKey;

        public EIdEasyHttpClientHandler(string serverCertificatePublicKey)
        {
            _serverCertificatePublicKey = serverCertificatePublicKey;
            ServerCertificateCustomValidationCallback = ServerCertificateValidationCallback;
        }

        private bool ServerCertificateValidationCallback(HttpRequestMessage httpRequestMessage,
            X509Certificate2 x509Certificate2, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors != SslPolicyErrors.None)
                return false;

            var publicKey = x509Certificate2.GetPublicKeyString();

            return _serverCertificatePublicKey.Equals(publicKey, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}