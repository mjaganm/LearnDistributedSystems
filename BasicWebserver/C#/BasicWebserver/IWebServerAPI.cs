/**
 * 
 * MIT License
 * 
 * Copyright (c) 2018 Jagan Mohan Maddukuri
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 * 
 * */

namespace mjaganm.LearnDistributedSystems.BasicWebserver
{
    using System;
    using System.Globalization;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Security;
    using System.ServiceModel.Web;
    using System.Threading;

    [ServiceContract]
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    internal class WCFServiceApi : IDisposable
    {
        private WebServiceHost serviceHost;

        private BasicWebserver WebServer { get; set; }

        public WCFServiceApi(BasicWebserver basicwebserver)
        {
            WebServer = basicwebserver;
        }

        [OperationContract]
        [WebGet(UriTemplate = "IsAlive",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        public string GetIsAlive()
        {
            return Constants.ResponseAlive;
        }

        [OperationContract]
        [WebGet(UriTemplate = "GetRandomString",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        public string GetRandomString()
        {
            Console.WriteLine("GetRandomString: IpAddress: {0}");

            return WebServer.GetRandomString();
        }

        [OperationContract]
        [WebGet(UriTemplate = "GetRandomString?length={intlength}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        public string GetRandomStringLength(string intlength)
        {
            Console.WriteLine("GetRandomString: IpAddress: {0} int-length: {1}", RetrieveClientIP(), intlength);

            return WebServer.GetRandomString(intlength);
        }

        [OperationContract]
        [WebGet(UriTemplate = "GetRandomInt?start={intstart}&end={intend}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        public int GetRandomInt(string intstart, string intend)
        {
            Console.WriteLine("GetRandomInt: IpAddress: {0} int-start: {1} int-end: {2}", RetrieveClientIP(), intstart, intend);
            return WebServer.GetRandomInt(intstart, intend);
        }

        [OperationContract]
        [WebGet(UriTemplate = "GetSHA256Hash?content={stringcontent}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        public string GetSHA256Hash(string stringcontent)
        {
            Console.WriteLine("GetSHA256Hash: IpAddress: {0} stringcontent: {1}", RetrieveClientIP(), stringcontent);
            return WebServer.GetSHA256Hash(stringcontent);
        }

        public void Dispose()
        {
            serviceHost.Close();
        }

        public string RetrieveClientIP()
        {
            var props = OperationContext.Current.IncomingMessageProperties;
            var endpointProperty = props[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;

            if (endpointProperty == null)
            {
                Console.WriteLine("Fatal error retrieving client IP");
                return string.Empty;
            }

            Console.WriteLine("client IP:{0}", endpointProperty.Address);
            return endpointProperty.Address;
        }

        public bool StartWebService(int servicePort)
        {
            string httpEndpoint = GetBaseAddress(servicePort);

            try
            {
                serviceHost = new WebServiceHost(this, new Uri(httpEndpoint));

                serviceHost.AddServiceEndpoint(
                    typeof(WCFServiceApi),
                    GetHttpBinding(),
                    string.Empty);

                serviceHost.Open();

                // Debug
                Console.WriteLine("Http WebService running");

                return true;
            }
            catch (AddressAccessDeniedException accessex)
            {
                Console.WriteLine("This process needs to be initialized with ELEVATED Command Prompt. Exiting now...");
                serviceHost.Abort();

                return false;
            }
            catch (AddressAlreadyInUseException addressex)
            {
                Console.WriteLine("Port {0} is already registered by another process.", servicePort);
                serviceHost.Abort();

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Fatal exception encountered during Http Webservice initialization. Exception: {0}", e);
                serviceHost.Abort();

                return false;
            }
        }

        private WebHttpBinding GetHttpBinding()
        {
            WebHttpBinding binding = new WebHttpBinding();
            binding.Security.Mode = WebHttpSecurityMode.None;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

            return binding;
        }

        private string GetBaseAddress(int servicePort)
        {
            string baseAddress = string.Format(
                CultureInfo.InvariantCulture,
                "http://{0}:{1}/BasicWebserver/",
                Environment.MachineName,
                servicePort);

            Console.WriteLine("WebServer BaseAddress:{0}", baseAddress);
            return baseAddress;
        }
    }
}
