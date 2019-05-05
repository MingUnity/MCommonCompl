using Baidu.Aip.Face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MingUnity.FaceRec
{
    /// <summary>
    /// 人脸识别基类
    /// </summary>
    public abstract class FaceRecBase
    {
        private static bool _serviceInit = false;

        /// <summary>
        /// 人脸服务
        /// </summary>
        protected Face _faceClient;

        public FaceRecBase(FaceAppData appData)
        {
            if (!_serviceInit)
            {
                ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidationCallback;

                ServicePointManager.DefaultConnectionLimit = 200;

                _serviceInit = true;
            }

            _faceClient = new Face(appData.apiKey, appData.secretKey)
            {
                Timeout = 60000
            };
        }

        private bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            //Return true if the server certificate is ok
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            bool acceptCertificate = true;
            string msg = "The server could not be validated for the following reason(s):\r\n";

            //The server did not present a certificate
            if ((sslPolicyErrors &
                 SslPolicyErrors.RemoteCertificateNotAvailable) == SslPolicyErrors.RemoteCertificateNotAvailable)
            {
                msg = msg + "\r\n    -The server did not present a certificate.\r\n";
                acceptCertificate = false;
            }
            else
            {
                //The certificate does not match the server name
                if ((sslPolicyErrors &
                     SslPolicyErrors.RemoteCertificateNameMismatch) == SslPolicyErrors.RemoteCertificateNameMismatch)
                {
                    msg = msg + "\r\n    -The certificate name does not match the authenticated name.\r\n";
                    acceptCertificate = false;
                }

                //There is some other problem with the certificate
                if ((sslPolicyErrors &
                     SslPolicyErrors.RemoteCertificateChainErrors) == SslPolicyErrors.RemoteCertificateChainErrors)
                {
                    foreach (X509ChainStatus item in chain.ChainStatus)
                    {
                        if (item.Status != X509ChainStatusFlags.RevocationStatusUnknown &&
                            item.Status != X509ChainStatusFlags.OfflineRevocation)
                            break;

                        if (item.Status != X509ChainStatusFlags.NoError)
                        {
                            msg = msg + "\r\n    -" + item.StatusInformation;
                            acceptCertificate = false;
                        }
                    }
                }
            }

            //If Validation failed, present message box
            if (acceptCertificate == false)
            {
                msg = msg + "\r\nDo you wish to override the security check?";
                //          if (MessageBox.Show(msg, "Security Alert: Server could not be validated",
                //                       MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                acceptCertificate = true;
            }

            return acceptCertificate;
        }
    }
}
