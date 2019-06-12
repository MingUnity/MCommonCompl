using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Ming.Speech
{
    /// <summary>
    /// 语音基类
    /// </summary>
    public class SpeechBase
    {
        private static bool _serviceInit = false;

        public SpeechBase()
        {
            if (!_serviceInit)
            {
                ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidationCallback;

                ServicePointManager.DefaultConnectionLimit = 200;

                _serviceInit = true;
            }
        }

        private bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            //Return true if the server certificate is ok
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            bool acceptCertificate = true;

            //The server did not present a certificate
            if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNotAvailable) == SslPolicyErrors.RemoteCertificateNotAvailable)
            {
                acceptCertificate = false;
            }
            else
            {
                //The certificate does not match the server name
                if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) == SslPolicyErrors.RemoteCertificateNameMismatch)
                {
                    acceptCertificate = false;
                }

                //There is some other problem with the certificate
                if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) == SslPolicyErrors.RemoteCertificateChainErrors)
                {
                    foreach (X509ChainStatus item in chain.ChainStatus)
                    {
                        if (item.Status != X509ChainStatusFlags.RevocationStatusUnknown &&
                            item.Status != X509ChainStatusFlags.OfflineRevocation)
                            break;

                        if (item.Status != X509ChainStatusFlags.NoError)
                        {
                            acceptCertificate = false;
                        }
                    }
                }
            }

            //If Validation failed, present message box
            if (acceptCertificate == false)
            {
                acceptCertificate = true;
            }

            return acceptCertificate;
        }
    }

    /// <summary>
    /// 语音格式
    /// </summary>
    public enum SpeechForamt
    {
        WAV = 0,

        PCM,

        AMR,

        MP3
    }
}
