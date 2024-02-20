using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
// The generated classes are defined in this namespace.
namespace SAPB1
{
    // ServiceLayer is defined as a partial class in the generated classes.
    // Here we add our own implementation.
    public partial class ServiceLayer
    {
        // Save the cookie in the client side.
        private string m_strCookie = "";
        // An empty implementation for certificate verification.
        private static bool TLSCertificateValidate(object sender,
        X509Certificate cert,
        X509Chain chain,
        SslPolicyErrors ssl)
        {
            return true;
        }
        // Override this function to customize some logic to for the ServiceLayer context.
        partial void OnContextCreated()
        {
            InitializeContext();
        }
        private void InitializeContext()
        {
            // Get the cookie if the response header containing Set-Cookie.
            this.ReceivingResponse += (sender, eventArgs) =>
            {
                string cookie = eventArgs.ResponseMessage.GetHeader("Set-Cookie");
                if (!string.IsNullOrEmpty(cookie))
                {
                    if (eventArgs.ResponseMessage.StatusCode ==
                   (int)HttpStatusCode.OK)
                    {
                        m_strCookie = cookie;
                    }
                }
            };
            // Set the cookie for each request.
            this.BuildingRequest += (sender, eventArgs) =>
            {
                if (m_strCookie != null)
                {
                    eventArgs.Headers.Remove("cookie");
                    eventArgs.Headers.Add("cookie", m_strCookie);
                }
            };
            // Ignore the SSL/TLS certificate check for the HTTPS connection to Service Layer.
            ServicePointManager.ServerCertificateValidationCallback = TLSCertificateValidate;
        }
    }
}
