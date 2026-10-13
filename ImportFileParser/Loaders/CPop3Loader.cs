using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net.Security;
using System.Text.RegularExpressions;
using System.Globalization;
using ImportFileParser.Managers;
using ImportFileParser.Common;
using ImportFileParser.RowObjects;

namespace ImportFileParser.Loaders
{
    enum EnumConnState
    {
        UPDATE,
        AUTHORIZATION,
        TRANSACTION
    }

    public class CPop3Loader: CBaseLoader
    {
        
        public string m_Error = string.Empty;

        private TcpClient m_Server;
        private string m_ServerAddress;
        private int m_ServerPort;
        private string m_User;
        private string m_Password;
        private SslStream m_TcpStream;
        private StreamReader m_TcpReader;
        private string m_Data;
        private byte[] m_DataSize;
        private const string m_CRLF = "\r\n";

        private EnumConnState m_ConnectionState = EnumConnState.UPDATE;

        public event TransRecEventHandler OnAddTransRec;

        public CPop3Loader(string p_BankAccount, string p_Server, int p_ServerPort, string p_User, string p_Password): base(p_BankAccount)
        {
            m_ServerAddress = p_Server;
            m_ServerPort = p_ServerPort;
            m_User = p_User;
            m_Password = p_Password;            
        }

        private bool Connect(string p_Server, int p_Port)
        {
            

            //Initialize the connection.  This code snipped "borrowed"                       
            m_Server = new TcpClient(p_Server, p_Port);

            try
            {
                // initialization
                m_TcpStream = new SslStream(m_Server.GetStream());
                m_TcpStream.AuthenticateAsClient(p_Server);
                m_TcpReader = new StreamReader(m_TcpStream);

                //The pop session is now in the AUTHORIZATION state               
                m_ConnectionState = EnumConnState.AUTHORIZATION;
                return true;
            }
            catch (InvalidOperationException err)
            {
                m_Error = ("Error: " + err.ToString());
                return false;
            }

        }

        private string USER(string p_Login)
        {
            string temp;

            if (m_ConnectionState != EnumConnState.AUTHORIZATION)
            {
                //DELE is only valid when the pop session is in the TRANSACTION STATE
                temp = "Connection state not = TRANSACTION";
            }
            else
            {
                issue_command("USER " + p_Login);
                temp = read_single_line_response();
            }
            return (temp);
        }

        private string PASS(string p_Pwd)
        {
            string temp;

            if (m_ConnectionState != EnumConnState.AUTHORIZATION)
            {
                //DELE is only valid when the pop session is in the TRANSACTION STATE
                temp = "Connection state not = TRANSACTION";
            }
            else
            {
                issue_command("PASS " + p_Pwd);
                temp = read_single_line_response();
            }
            return (temp);
        }

        private string STAT()
        {
            string temp;

            if (m_ConnectionState != EnumConnState.TRANSACTION)
            {
                //DELE is only valid when the pop session is in the TRANSACTION STATE
                temp = "Connection state not = TRANSACTION";
            }
            else
            {
                issue_command("STAT");
                temp = read_single_line_response();
            }
            return (temp);
        }

        private string QUIT()
        {
            string temp;

            if (m_ConnectionState != EnumConnState.TRANSACTION)
            {
                //DELE is only valid when the pop session is in the TRANSACTION STATE
                temp = "Connection state not = TRANSACTION";
            }
            else
            {
                issue_command("QUIT");
                temp = read_single_line_response();
            }
            return (temp);
        }

        private List<string> RETR(int p_MsgId)
        {            

            if (m_ConnectionState != EnumConnState.TRANSACTION)
            {
                //DELE is only valid when the pop session is in the TRANSACTION STATE
                m_Error = "Connection state not = TRANSACTION";
                return null;
            }
            
            issue_command("RETR " + p_MsgId.ToString());            
            return read_multi_line_response();
        }

        private void issue_command(string command)
        {
            //send the command to the pop server.  This code snipped "borrowed"            
            m_Data = command + m_CRLF;
            m_DataSize = Encoding.ASCII.GetBytes(m_Data.ToCharArray());
            m_TcpStream.Write(m_DataSize, 0, m_DataSize.Length);

        }

        private string read_single_line_response()
        {
            //read the response of the pop server.  This code snipped "borrowed"            
            string temp;
            try
            {
                temp = m_TcpReader.ReadLine();
                was_pop_error(temp);
                return (temp);
            }
            catch (InvalidOperationException err)
            {
                return ("Error in read_single_line_response(): " + err.ToString());
            }

        }

        private List<string> read_multi_line_response()
        {
            //read the response of the pop server.  This code snipped "borrowed"            
            List<string> v_Result = new List<string>();
            try
            {
                string v_Temp = m_TcpReader.ReadLine();
                while (v_Temp != null)
                {
                    if (v_Temp == ".")
                    {
                        break;
                    }
                    if (v_Temp.IndexOf("-ERR") != -1)
                    {
                        break;
                    }
                    v_Result.Add(v_Temp);
                    was_pop_error(v_Temp);

                    v_Temp = m_TcpReader.ReadLine();
                }
                
                return (v_Result);
            }
            catch (InvalidOperationException err)
            {
                m_Error = "Error in read_single_line_response(): " + err.ToString();
                return null;
            }

        }

        private void was_pop_error(string response)
        {
            //detect if the pop server that issued the response believes that
            //an error has occured.

            if (string.IsNullOrEmpty(response)) return;

            if (response.StartsWith("-"))
            {
                //if the first character of the response is "-" then the 
                //pop server has encountered an error executing the last 
                //command send by the client
                m_Error = response;
            }            
        }

        private string DecodeBase64(string str)
        {
            byte[] buff = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(buff);
        }

        private List<string> m_EmailLines = null;

        public void Load()
        {
            if (!this.Connect(m_ServerAddress, m_ServerPort))
            {
                System.Windows.Forms.MessageBox.Show(this.m_Error);
                return;
            }

            try
            {
                string v_Resp = this.USER(m_User);
                v_Resp = this.PASS(m_Password);
                this.m_ConnectionState = EnumConnState.TRANSACTION;

                // Get total count of emails
                v_Resp = this.STAT();
                v_Resp = this.STAT();
                
                string[] v_ParsedResp = v_Resp.Split(' ');
                int v_LastEmail = Int32.Parse(v_ParsedResp[1]);

                for (int v_MsgId = v_LastEmail; v_MsgId > Math.Max(0, (v_LastEmail - 50)); --v_MsgId)
                {
                    m_EmailLines = this.RETR(v_MsgId);

                    // Check if this email was sent from me to myself
                    int v_FoundIndex = m_EmailLines.FindIndex(x => x.StartsWith("From:"));
                    if (v_FoundIndex < 0) continue;
                    if (!m_EmailLines[v_FoundIndex].Contains("magicdranton")) continue;                        

                    // Get Date of the email
                    v_FoundIndex = m_EmailLines.FindIndex(x => x.StartsWith("Date:"));
                    if (v_FoundIndex < 0) continue;

                    string v_Date = m_EmailLines[v_FoundIndex].Replace("Date:", "");

                    // Get contents of the email
                    v_FoundIndex = m_EmailLines.FindIndex(x => x.Contains("Content-Transfer-Encoding: base64"));
                    if (v_FoundIndex < 0) continue;

                    string v_MailBody = this.DecodeBase64(m_EmailLines[v_FoundIndex + 2]).ToLower();
                    v_MailBody = v_MailBody.Replace(".", "").Replace("руб", "").Replace("\r\n","");

                    string v_StrAmount = Regex.Match(v_MailBody, @"\d+").Value;

                    if (!string.IsNullOrEmpty(v_StrAmount))
                    {
                        v_MailBody = v_MailBody.Replace(v_StrAmount, "");
                    }

                    int v_Amount = 0;
                    Int32.TryParse(v_StrAmount, NumberStyles.Any, CConfigManager.Manager.RussianCulture, out v_Amount);

                    bool v_IsCredit = true;
                    string v_ContragentAccount = Constants.CONST_EMPTY_VALUE;

                    // Ignore saldo emails
                    if (v_MailBody.Contains("сальдо")) continue;

                    // Add new Trans to common list
                    CTransRec v_NewTrans = this.CreateTransRec
                    (
                        v_ContragentAccount,
                        DateTime.Parse(v_Date, CConfigManager.Manager.RussianCulture),
                        v_Amount,
                        v_MailBody,
                        null,
                        null,
                        v_IsCredit
                    );

                    using (CTransRecEventParams v_Params = new CTransRecEventParams(v_NewTrans, this.SkipDuplicates))
                    {
                        OnAddTransRec(v_Params);
                    }
                }                
            }
            finally
            {
                this.QUIT();
                this.QUIT();
                this.m_ConnectionState = EnumConnState.UPDATE;
            }
        }
    }


}
