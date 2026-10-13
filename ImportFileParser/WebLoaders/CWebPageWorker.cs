using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

/* ANTON: Below is experimental part on auto-login into Banck Web-Clients using HTML parsing (w/o APIs) */ 

namespace ImportFileParser
{   
    public class CWebPageWorker
    {
        private class CWebClient : System.Net.WebClient
        {
            public int Timeout { get; set; }

            protected override WebRequest GetWebRequest(Uri uri)
            {
                WebRequest lWebRequest = base.GetWebRequest(uri);
                lWebRequest.Timeout = Timeout;
                ((HttpWebRequest)lWebRequest).ReadWriteTimeout = Timeout;
                return lWebRequest;
            }
        }

        private CWebClient m_WebClient = null;

        public CWebPageWorker()
        {
            /*
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.DefaultConnectionLimit = 9999;
            */
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            m_WebClient = new CWebClient();
            m_WebClient.Timeout = 10000;

            m_WebClient.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
            m_WebClient.Headers.Add(HttpRequestHeader.Host, "online.akbars.ru");
            m_WebClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 73.0.3683.86 Safari / 537.36");            
        }

        private HttpWebResponse GetRequest(string p_Url)
        {
            var logger = new TraceSource("LowLevelDesign");

            //try
            //{
            //    HttpWebRequest v_WebRequest = (HttpWebRequest)WebRequest.Create(p_Url);
            //    v_WebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3";
            //    v_WebRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            //    v_WebRequest.Headers.Add("Accept-Language", "en-US,en;q=0.9");
            //    v_WebRequest.AllowAutoRedirect = true;
            //    v_WebRequest.Method = "GET";                
            //    v_WebRequest.Timeout = 20000;
            //    v_WebRequest.UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 73.0.3683.86 Safari / 537.36";
            //    v_WebRequest.Host = "click.alfabank.ru";
            //    v_WebRequest.Referer = p_Url;                

            //    return (HttpWebResponse)v_WebRequest.GetResponse();
            //}
            //try
            //{
            //    using (WebClient v_client = new WebClient())
            //    {
            //        ToLog("debug", "before connection");

            //        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            //        v_client.Timeout = v_TimeOutMin * 60 * 1000;

            //        v_client.Headers.Add(HttpRequestHeader.Accept, v_HTTP_Accept);
            //        v_client.Headers.Add(HttpRequestHeader.Authorization, "basic " + v_AuthEncoded);

            //        byte[] v_result_bytes = null;

            //        ToLog("debug", "GET");
            //        v_result_bytes = v_client.DownloadData(v_URL);

            //        ToLog("debug", "After connection");

            //        if (v_result_bytes == null)
            //        {
            //            string v_RequestIsNull = "WebRequest to Caliber API: Response result is null.";
            //            SetRequestError(v_RequestIsNull, v_OrgData);
            //            Dts.TaskResult = (int)ScriptResults.Failure;
            //            return;
            //        }

            //        v_result = System.Text.Encoding.UTF8.GetString(v_result_bytes);
            //    }
            //}            
            //catch (Exception ex)
            //{
            //     logger.TraceEvent(TraceEventType.Information, 0, "Exception: {0}", ex);
            //}
            return null;
        }

        private string SubmitForm(string p_Url, params string[] p_FormParams)
        {
            //setup some variables
            string p_AllParams = string.Join("&", p_FormParams);
            var v_DataBytes = Encoding.UTF8.GetBytes(p_AllParams);            
            
            HttpWebRequest v_WebRequest = (HttpWebRequest)WebRequest.Create(p_Url);
            v_WebRequest.Method = "POST";
            v_WebRequest.ContentLength = v_DataBytes.Length;
            v_WebRequest.Accept = "application/json, text/plain, */*";
            v_WebRequest.ContentType = "application/json; charset=utf-8";
            v_WebRequest.Referer = "https://online.akbars.ru/";
            v_WebRequest.Headers.Add("Origin", "https://online.akbars.ru");
            //v_WebRequest.Timeout = 7000;            

            Stream v_Stream = null;

            try
            {
                v_Stream = v_WebRequest.GetRequestStream();
                v_Stream.Write(v_DataBytes, 0, v_DataBytes.Length);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                v_Stream?.Close();
            }

            using (HttpWebResponse v_WebResponse = (HttpWebResponse)v_WebRequest.GetResponse())
            {
                string v_Result = null;

                using (StreamReader sr =
                   new StreamReader(v_WebResponse.GetResponseStream()))
                {
                    v_Result = sr.ReadToEnd();

                    // Close and clean up the StreamReader
                    sr.Close();
                }
                return v_Result;
            }
        }

        public void Login_Alfabank()
        {
            string v_RequestId = "3543036109420014564";
            using (HttpWebResponse v_Response = GetRequest("https://click.alfabank.ru:443/login/"))
            {
                string v_JSessionID = v_Response?.Cookies["JSESSIONID"].Value;
            }

            //reqIdExpiresAt
            //JSESSIONID

            // Initial login request using UsrName & pwd
            //
            
            string v_LoginResponse = SubmitForm(
                "https://click.alfabank.ru/oam/server/auth_cred_submit",
                "username=some_user",
                "password=some_pwd",
                "request_id=" + v_RequestId
                );                        

            //https://click.alfabank.ru/login/otp-check?authMethods=SMSPWD%2CPSHPWD&referenceId=P1125021907l93r1&request_id=780283174922187791&CREDENTIAL_CONTEXT_DATA=USER_ACTION_COMMAND%2CUSER_ACTION_COMMAND%2Cnull%2Chidden%3Botp_param%2Cotp_param%2Cotp_param%2Cpassword%3B
            //authMethods=SMSPWD%2CPSHPWD
            //referenceId=P1125021907l93r1
            //request_id=780283174922187791
            //CREDENTIAL_CONTEXT_DATA=USER_ACTION_COMMAND%2CUSER_ACTION_COMMAND%2Cnull%2Chidden%3Botp_param%2Cotp_param%2Cotp_param%2Cpassword%3B


            // Enter code from SMS
            v_LoginResponse = SubmitForm(
                "https://click.alfabank.ru/oam/server/auth_cred_submit",
                "otp_param=sms",                
                "request_id=" + v_RequestId
                );

            // Extract JSESSIONID from v_LOginResponse and do submit final login form
            //https://click.alfabank.ru/ALFAIBSR/;CLICK_JSESSIONID=SWgFOyw7UNXGR7taxpRply6lJs0-_i8ajoxYOUdX32bkbL4XbW0C!-2141808010?_afrLoop=20490372474577516&_afrWindowMode=0&_afrWindowId=null
        }        
    }
}
