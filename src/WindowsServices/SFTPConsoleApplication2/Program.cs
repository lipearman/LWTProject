
using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SFTPConsoleApplication2
{


    public static class StringExtensions
    {

        public static string RemoveLineBreaks(this string lines)
        {
            return lines.Replace("\r", "").Replace("\n", "");
        }

        public static string ReplaceLineBreaks(this string lines, string replacement)
        {
            return lines.Replace("\r\n", replacement)
                        .Replace("\r", replacement)
                        .Replace("\n", replacement);
        }

        public static string Left(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }

    }

    class Program
    {
        static void WriteToFile(string text)
        {
            string path = @"E:\ftproot\ftpuser\www\ssh\OpenSSH\data\ServiceLog.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(text + " - " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                writer.Close();
            }
        }

        static void Main(string[] args)
        {

            //test();

            sftp_gen();

            //genfile();
        }
        static void sftp_gen()
        {


            //var file_m = DateTime.Now.AddDays(-1).ToString("yyyyMMddHHmmss");
            var file_m = DateTime.Now.ToString("yyyyMMddHHmmss");
            var filename = String.Format(@"E:\ftproot\ftpuser\www\ssh\OpenSSH\data\NPPRSI001_{0}.DAT", file_m);

            using (var dc = new DataClasses_afwnltDataContext())
            {
                var arrResults = dc.ExecuteQuery<vw_RetailSalesJP>("select distinct * FROM vw_RetailSalesJP where CONVERT(varchar, ClosingDate,112) = '" + file_m.Left(8) + "' or CONVERT(varchar, AFWMakeDate,112)='" + file_m.Left(8) + "'").ToList();
                if (arrResults.Count > 0)
                {

                    Console.WriteLine("Data :" + arrResults.Count.ToString());
                    WriteToFile("Data :" + arrResults.Count.ToString());

                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        Console.WriteLine("Create File...");
                        WriteToFile("Create File...");

                        foreach (var s in arrResults)
                        {

                            var _TITLE = (!string.IsNullOrEmpty(s.TITLE)) ? s.TITLE.ToString() : "-";
                            var _FIRST_NAME = (!string.IsNullOrEmpty(s.FIRST_NAME)) ? s.FIRST_NAME.ToString() : "-";
                            var _LAST_NAME = (!string.IsNullOrEmpty(s.LAST_NAME)) ? s.LAST_NAME.ToString() : "-";
                            var _CUSTOMER_TYPE = (!string.IsNullOrEmpty(s.CUSTOMER_TYPE)) ? s.CUSTOMER_TYPE.ToString() : "-";
                            var _ID_CARD = (!string.IsNullOrEmpty(s.ID_CARD)) ? s.ID_CARD.ToString() : "-";
                            var _TAX_ID_NO = (!string.IsNullOrEmpty(s.TAX_ID_NO)) ? s.TAX_ID_NO.ToString() : "-";
                            var _MOBILE_NO = (!string.IsNullOrEmpty(s.MOBILE_NO)) ? s.MOBILE_NO.ToString() : "-";
                            var _HOME_NO = (!string.IsNullOrEmpty(s.HOME_NO)) ? s.HOME_NO.ToString() : "-";
                            var _OFFICE_NO = (!string.IsNullOrEmpty(s.OFFICE_NO)) ? s.OFFICE_NO.ToString() : "-";
                            var _EMAIL = (!string.IsNullOrEmpty(s.Email)) ? s.Email.ToString() : "-";
                            var _CONTACT_PERSON = (!string.IsNullOrEmpty(s.CONTACT_PERSON)) ? s.CONTACT_PERSON.ToString() : "-";
                            var _CONTACT_ADDRESS = (!string.IsNullOrEmpty(s.CONTACT_ADDRESS)) ? s.CONTACT_ADDRESS.ToString() : "-";
                            var _POSTAL_ADDRESS = (!string.IsNullOrEmpty(s.POSTAL_ADDRESS)) ? s.POSTAL_ADDRESS.ToString() : "-";
                            var _VIN = (!string.IsNullOrEmpty(s.VIN)) ? s.VIN.ToString() : "-";
                            var _CLIENT_CODE = (!string.IsNullOrEmpty(s.CLIENT_CODE)) ? s.CLIENT_CODE.ToString() : "-";
                            //var _CLOSING_DATE = s.ClosingDate.Value.ToString("yyyyMMdd");
                            var _CLOSING_DATE = (!string.IsNullOrEmpty(s.CLOSING_DATE)) ? s.CLOSING_DATE.ToString() : "-";


                            //1	TITLE	VARCHAR2		15	
                            //2	FIRST_NAME	VARCHAR2		100	
                            //3	LAST_NAME	VARCHAR2		100	กรณีเป็นบริษัท ให้ระบุเป็น -
                            //4	CUSTOMER_TYPE	VARCHAR2		1	1=นิติบุคคล, 2=บริษัท, 3=หน่วยงานภาครัฐ
                            //5	ID_CARD	VARCHAR2		20	รวม Passport No.
                            //6	TAX_ID_NO	VARCHAR2		20	
                            //7	MOBILE_NO	VARCHAR2		10	
                            //8	HOME_NO	VARCHAR2		20	
                            //9	OFFICE_NO	VARCHAR2		20	
                            //10	EMAIL	VARCHAR2		40	
                            //11	CONTACT_PERSON	VARCHAR2		100	กรณีเป็นหน่วยงานภาครัฐหรือบริษัท
                            //12	CONTACT_ADDRESS	VARCHAR2		255	ที่อยู่ทั้งหมด
                            //13	POSTAL_ADDRESS	VARCHAR2		255	
                            //14	VIN	VARCHAR2		30	Chassis No.
                            //15	CLIENT_CODE	VARCHAR2		100	
                            //16	CLOSING_DATE	VARCHAR2		100	วันที่ส่งประกัน

                            StringBuilder sb = new StringBuilder();

                            sb.Append(_TITLE.Trim().Left(15));
                            sb.Append("|" + _FIRST_NAME.Replace(Environment.NewLine, "").Trim().Left(100));
                            sb.Append("|" + _LAST_NAME.Replace(Environment.NewLine, "").Trim().Left(100));
                            sb.Append("|" + _CUSTOMER_TYPE.Replace(Environment.NewLine, "").Trim().Left(1));
                            sb.Append("|" + _ID_CARD.Replace(Environment.NewLine, "").Trim().Left(20));
                            sb.Append("|" + _TAX_ID_NO.Replace(Environment.NewLine, "").Trim().Left(20));
                            sb.Append("|" + _MOBILE_NO.Replace(Environment.NewLine, "").Trim().Left(10));
                            sb.Append("|" + _HOME_NO.Replace(Environment.NewLine, "").Trim().Left(20));
                            sb.Append("|" + _OFFICE_NO.Replace(Environment.NewLine, "").Trim().Left(20));
                            sb.Append("|" + _EMAIL.Replace(Environment.NewLine, "").Trim().Left(40));
                            sb.Append("|" + _CONTACT_PERSON.Replace(Environment.NewLine, "").Trim().Left(100));
                            sb.Append("|" + _CONTACT_ADDRESS.Replace(Environment.NewLine, "").Trim().Left(255));
                            sb.Append("|" + _POSTAL_ADDRESS.Replace(Environment.NewLine, "").Trim().Left(255));
                            sb.Append("|" + _VIN.Replace(Environment.NewLine, "").Trim().Left(30));
                            sb.Append("|" + _CLIENT_CODE.Replace(Environment.NewLine, "").Trim().Left(100));
                            sb.Append("|" + _CLOSING_DATE.Replace(Environment.NewLine, "").Trim().Left(100));

                            sw.WriteLine(sb.ToString());


                        }
                    }

                    //===============================  #Console1  ================================================
                    Process p = Process.Start(@"E:\ftproot\ftpuser\www\ssh\OpenSSH\ssh.exe", @"-L 60001:sfxsv901.jp.nissan.biz:22 -i E:\ftproot\ftpuser\www\ssh\id_rsa_20160203 NMTCRMFTP@150.63.64.101");
                    Console.WriteLine("Connecting Gateway...");
                    WriteToFile("Connecting Gateway...");
                    System.Threading.Thread.Sleep(10000);


                    //===============================  #Console2  ================================================
                    var keyFile = new PrivateKeyFile(@"E:\ftproot\ftpuser\www\ssh\id_rsa_20160203");
                    var keyFiles = new[] { keyFile };
                    var methods_client = new List<AuthenticationMethod>();
                    methods_client.Add(new PrivateKeyAuthenticationMethod("NMTCRMFTP04", keyFiles));
                    var con_client = new ConnectionInfo("localhost", 60001, "NMTCRMFTP04", methods_client.ToArray());
                    using (var client = new SftpClient(con_client))
                    {
                        try
                        {
                            client.Connect();
                            if (client.IsConnected)
                            {
                                Console.WriteLine("Client Connected");
                                WriteToFile("Client Connected");
                                System.Threading.Thread.Sleep(1000);


                                Console.WriteLine("Upload File...");
                                WriteToFile("Upload File...");
                                System.Threading.Thread.Sleep(1000);

                                FileInfo f = new FileInfo(filename);
                                string uploadfile = f.FullName;
                                using (FileStream fileStream = new FileStream(uploadfile, FileMode.Open))
                                {
                                    if (fileStream != null)
                                    {
                                        client.BufferSize = 4 * 1024;
                                        client.UploadFile(fileStream, "/export/ftpdata1/ESB01/nmtcrmftp04/" + f.Name, null);
                                    }
                                }
                                System.Threading.Thread.Sleep(5000);


                                //=============== Send Mail ==========
                                Console.WriteLine("Send Mail...");
                                WriteToFile("Send Mail...");

                                System.Threading.Thread.Sleep(1000);
                                try
                                {
                                    string smtp = System.Configuration.ConfigurationManager.AppSettings["smtp"];
                                    string mail_from = System.Configuration.ConfigurationManager.AppSettings["mailfrom"];
                                    string mail_to = System.Configuration.ConfigurationManager.AppSettings["mailto"];
                                    string mail_cc = System.Configuration.ConfigurationManager.AppSettings["mailcc"];
                                    string mail_bcc = System.Configuration.ConfigurationManager.AppSettings["mailbcc"];


                                    MailMessage mail = new MailMessage();
                                    //SmtpClient SmtpServer = new SmtpClient("172.16.40.244");//172.16.40.135
                                    //SmtpClient SmtpServer = new SmtpClient("172.16.40.135");
                                    SmtpClient SmtpServer = new SmtpClient(smtp);

                                    mail.IsBodyHtml = true;
                                    mail.From = new MailAddress(mail_from);

                                    //mail.To.Add(mail_to);
                                    //mail.CC.Add(mail_cc);
                                    //mail.Bcc.Add(mail_bcc);

                                    string[] _MailTo = mail_to.Split(';');
                                    foreach (var item in _MailTo)
                                    {
                                        if (!string.IsNullOrEmpty(item.Trim()))
                                            mail.To.Add(item);
                                    }
                                    string[] _MailCc = mail_cc.Split(';');
                                    foreach (var item in _MailCc)
                                    {
                                        if (!string.IsNullOrEmpty(item.Trim()))
                                            mail.CC.Add(item);
                                    }
                                    string[] _MailBcc = mail_bcc.Split(';');
                                    foreach (var item in _MailBcc)
                                    {
                                        if (!string.IsNullOrEmpty(item.Trim()))
                                            mail.Bcc.Add(item);
                                    }


                                    mail.Subject = String.Format("Request NPP data as of {0} (Daily)", DateTime.Now.ToString("dd MMM yyyy"));

                                    StringBuilder sb = new StringBuilder();
                                    sb.Append("Dear K.Pojdej,<br><br>");
                                    sb.AppendFormat("As your requested, please find attached files the NPP data for NMT as {0} for your consideration.<br><br>", DateTime.Now.ToString("dd MMM yyyy"));
                                    sb.Append("Best regards,<br><br>");

                                    sb.Append("<b><span lang=EN-NZ style='font-size:10.0pt;font-family:\"Tahoma\",\"sans-serif\";color:black;mso-ansi-language:EN-NZ'>Dusit Prasertsilp</span></b><br>");
                                    sb.Append("<b><span lang=EN-NZ style='font-size:10.0pt;font-family:\"Tahoma\",\"sans-serif\";color:black;mso-ansi-language:EN-NZ'>Division Manager</span></b><br>");
                                    sb.Append("<b><span lang=EN-NZ style='font-size:10.0pt;font-family:\"Tahoma\",\"sans-serif\";color:black;mso-ansi-language:EN-NZ'>Information Technology</span></b><br>");
                                    sb.Append("<b><span lang=EN-NZ style='font-size:10.0pt;font-family:\"Tahoma\",\"sans-serif\";color:black;mso-ansi-language:EN-NZ'>Lockton Wattana Insurance Brokers (Thailand) Ltd.</span></b><br>");

                                    sb.Append("<span style='font-size:10.0pt;font-family:\"Tahoma\",\"sans-serif\";color:#999999'>4th Floor, United Center Building,</span><br>");
                                    sb.Append("<span style='font-size:10.0pt;font-family:\"Tahoma\",\"sans-serif\";color:#999999'>323 Silom Road, Khet Bangrak, Bangkok 10500</span><br>");
                                    sb.Append("<span style='font-size:10.0pt;font-family:\"Tahoma\",\"sans-serif\";color:#999999'>Tel: +66 (0) 2 353 7000 (Ext. 2911)</span><br>");
                                    sb.Append("<span style='font-size:10.0pt;font-family:\"Tahoma\",\"sans-serif\";color:#999999'>Fax: +66 (0)2 353 7001-2</span><br>");
                                    sb.Append("<span style='font-size:10.0pt;font-family:\"Tahoma\",\"sans-serif\";color:#999999'>Mobile: +66 (8) 4 752 0536</span><br>");
                                    sb.Append("<span style='font-size:10.0pt;font-family:\"Tahoma\",\"sans-serif\";color:#999999'>E-mail: dusit@asia.lockton.com</span><br>");
                                    sb.Append("<span style='font-size:10.0pt;font-family:\"Tahoma\",\"sans-serif\";color:#999999'>Website: www.locktonwattana.co.th</span><br>");
                                    sb.Append("<span style='font-size:10.0pt;font-family:\"Tahoma\",\"sans-serif\";color:#999999'>Company Registration No.: 0105527011324</span><br>");


                                    //mail.Body = sb.ToString();


                                    //'===========================================================
                                    var bodyHTML = "<html><body>" + sb.ToString() + "<br><img src='cid:LOGO_IMAGE1' alt='Logo' /><br><span style='font-size:16.0pt;font-family:Angsana New,serif;color:green'><img src='cid:LOGO_IMAGE2' alt='Logo' /><i>Please consider the environment before printing this e-mail.</i></span></body></html>";
                                    AlternateView alternateView = AlternateView.CreateAlternateViewFromString(bodyHTML, null, "text/html");

                                    var path_to_the_image_file1 = String.Format(@"E:\ftproot\ftpuser\www\ssh\OpenSSH\logo\{0}", "image02.jpg");
                                    var path_to_the_image_file2 = String.Format(@"E:\ftproot\ftpuser\www\ssh\OpenSSH\logo\{0}", "image002.jpg");

                                    //Create the LinkedResource here
                                    LinkedResource logo1 = new LinkedResource(path_to_the_image_file1, "image/jpeg");//  'Content Type is set as image/jpeg
                                    logo1.ContentId = "LOGO_IMAGE1";
                                    logo1.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                                    alternateView.LinkedResources.Add(logo1);

                                    LinkedResource logo2 = new LinkedResource(path_to_the_image_file2, "image/jpeg");  //'Content Type is set as image/jpeg
                                    logo2.ContentId = "LOGO_IMAGE2";
                                    logo2.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                                    alternateView.LinkedResources.Add(logo2);

                                    mail.AlternateViews.Add(alternateView);
                                    //'===========================================================


                                    System.Net.Mail.Attachment attachment;
                                    attachment = new System.Net.Mail.Attachment(filename);
                                    mail.Attachments.Add(attachment);

                                    //SmtpServer.Port = 587;
                                    //SmtpServer.Credentials = new System.Net.NetworkCredential("your mail@gmail.com", "your password");
                                    //SmtpServer.EnableSsl = true;

                                    SmtpServer.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

                                    SmtpServer.Send(mail);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    WriteToFile(ex.Message);
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            WriteToFile(ex.Message);
                        }
                        finally
                        {
                            Console.WriteLine("Client Exit.");
                            WriteToFile("Client Exit.");
                            client.Disconnect();
                            client.Dispose();
                        }
                    }

                    p.CloseMainWindow();
                    System.Threading.Thread.Sleep(5000);

                }
            }

        }

        static void genfile()
        {

            var file_m = DateTime.Now.ToString("yyyyMMddHHmmss");
            var filename = String.Format(@"c:\temp\NPPRSI001_{0}.DAT", file_m);

            using (var dc = new DataClasses_afwnltDataContext())
            {
                //var arrResults = dc.ExecuteQuery<vw_RetailSalesJP>("select distinct * FROM vw_RetailSalesJP where CONVERT(varchar, ClosingDate,112) between '20171001' and '20170430'").ToList();
                var arrResults = dc.ExecuteQuery<vw_RetailSalesJP>("select distinct * FROM vw_RetailSalesJP where (CONVERT(varchar, ClosingDate,112) >= '20171001' or CONVERT(varchar, AFWMakeDate,112) >= '20171001') and MOBILE_NO <> '0000000000'").ToList();


                //select distinct * FROM vw_RetailSalesJP where CONVERT(varchar, ClosingDate,112) >= '20170701' and MOBILE_NO <> '0000000000'
                Console.WriteLine("Data :" + arrResults.Count.ToString());
                //WriteToFile("Data :" + arrResults.Count.ToString());

                using (StreamWriter sw = new StreamWriter(filename))
                {
                    Console.WriteLine("Create File...");
                    //WriteToFile("Create File...");

                    foreach (var s in arrResults)
                    {

                        var _TITLE = (!string.IsNullOrEmpty(s.TITLE)) ? s.TITLE.ToString() : "-";
                        var _FIRST_NAME = (!string.IsNullOrEmpty(s.FIRST_NAME)) ? s.FIRST_NAME.ToString() : "-";
                        var _LAST_NAME = (!string.IsNullOrEmpty(s.LAST_NAME)) ? s.LAST_NAME.ToString() : "-";
                        var _CUSTOMER_TYPE = (!string.IsNullOrEmpty(s.CUSTOMER_TYPE)) ? s.CUSTOMER_TYPE.ToString() : "-";
                        var _ID_CARD = (!string.IsNullOrEmpty(s.ID_CARD)) ? s.ID_CARD.ToString() : "-";
                        var _TAX_ID_NO = (!string.IsNullOrEmpty(s.TAX_ID_NO)) ? s.TAX_ID_NO.ToString() : "-";
                        var _MOBILE_NO = (!string.IsNullOrEmpty(s.MOBILE_NO)) ? s.MOBILE_NO.ToString() : "-";
                        var _HOME_NO = (!string.IsNullOrEmpty(s.HOME_NO)) ? s.HOME_NO.ToString() : "-";
                        var _OFFICE_NO = (!string.IsNullOrEmpty(s.OFFICE_NO)) ? s.OFFICE_NO.ToString() : "-";
                        var _EMAIL = (!string.IsNullOrEmpty(s.Email)) ? s.Email.ToString() : "-";
                        var _CONTACT_PERSON = (!string.IsNullOrEmpty(s.CONTACT_PERSON)) ? s.CONTACT_PERSON.ToString() : "-";
                        var _CONTACT_ADDRESS = (!string.IsNullOrEmpty(s.CONTACT_ADDRESS)) ? s.CONTACT_ADDRESS.ToString() : "-";
                        var _POSTAL_ADDRESS = (!string.IsNullOrEmpty(s.POSTAL_ADDRESS)) ? s.POSTAL_ADDRESS.ToString() : "-";
                        var _VIN = (!string.IsNullOrEmpty(s.VIN)) ? s.VIN.ToString() : "-";
                        var _CLIENT_CODE = (!string.IsNullOrEmpty(s.CLIENT_CODE)) ? s.CLIENT_CODE.ToString() : "-";
                        //var _CLOSING_DATE = s.ClosingDate.Value.ToString("yyyyMMdd");
                        var _CLOSING_DATE = (!string.IsNullOrEmpty(s.CLOSING_DATE)) ? s.CLOSING_DATE.ToString() : "-";


                        //1	TITLE	VARCHAR2		15	
                        //2	FIRST_NAME	VARCHAR2		100	
                        //3	LAST_NAME	VARCHAR2		100	กรณีเป็นบริษัท ให้ระบุเป็น -
                        //4	CUSTOMER_TYPE	VARCHAR2		1	1=นิติบุคคล, 2=บริษัท, 3=หน่วยงานภาครัฐ
                        //5	ID_CARD	VARCHAR2		20	รวม Passport No.
                        //6	TAX_ID_NO	VARCHAR2		20	
                        //7	MOBILE_NO	VARCHAR2		10	
                        //8	HOME_NO	VARCHAR2		20	
                        //9	OFFICE_NO	VARCHAR2		20	
                        //10	EMAIL	VARCHAR2		40	
                        //11	CONTACT_PERSON	VARCHAR2		100	กรณีเป็นหน่วยงานภาครัฐหรือบริษัท
                        //12	CONTACT_ADDRESS	VARCHAR2		255	ที่อยู่ทั้งหมด
                        //13	POSTAL_ADDRESS	VARCHAR2		255	
                        //14	VIN	VARCHAR2		30	Chassis No.
                        //15	CLIENT_CODE	VARCHAR2		100	
                        //16	CLOSING_DATE	VARCHAR2		100	วันที่ส่งประกัน

                        StringBuilder sb = new StringBuilder();

                        sb.Append(_TITLE.Trim().Left(15));
                        sb.Append("|" + _FIRST_NAME.Replace(Environment.NewLine, "").Trim().Left(100));
                        sb.Append("|" + _LAST_NAME.Replace(Environment.NewLine, "").Trim().Left(100));
                        sb.Append("|" + _CUSTOMER_TYPE.Replace(Environment.NewLine, "").Trim().Left(1));
                        sb.Append("|" + _ID_CARD.Replace(Environment.NewLine, "").Trim().Left(20));
                        sb.Append("|" + _TAX_ID_NO.Replace(Environment.NewLine, "").Trim().Left(20));
                        sb.Append("|" + _MOBILE_NO.Replace(Environment.NewLine, "").Trim().Left(10));
                        sb.Append("|" + _HOME_NO.Replace(Environment.NewLine, "").Trim().Left(20));
                        sb.Append("|" + _OFFICE_NO.Replace(Environment.NewLine, "").Trim().Left(20));
                        sb.Append("|" + _EMAIL.Replace(Environment.NewLine, "").Trim().Left(40));
                        sb.Append("|" + _CONTACT_PERSON.Replace(Environment.NewLine, "").Trim().Left(100));
                        sb.Append("|" + _CONTACT_ADDRESS.Replace(Environment.NewLine, "").Trim().Left(255));
                        sb.Append("|" + _POSTAL_ADDRESS.Replace(Environment.NewLine, "").Trim().Left(255));
                        sb.Append("|" + _VIN.Replace(Environment.NewLine, "").Trim().Left(30));
                        sb.Append("|" + _CLIENT_CODE.Replace(Environment.NewLine, "").Trim().Left(100));
                        sb.Append("|" + _CLOSING_DATE.Replace(Environment.NewLine, "").Trim().Left(100));

                        sw.WriteLine(sb.ToString());


                    }
                }

            }

            Console.WriteLine("End..");
        }

        static void test()
        {

            PrivateKeyFile rsa = new PrivateKeyFile(@"E:\ftproot\ftpuser\www\ssh\OpenSSH\id_rsa_20160203");

            using (var client = new SshClient("150.63.64.101", "NMTCRMFTP", rsa))
            {
                client.Connect();
                Console.WriteLine("client.Connect()");
                var port = new ForwardedPortLocal("localhost", 60001, "150.63.64.101", 22);
                port.Exception += delegate(object sender, ExceptionEventArgs e)
                {
                    Console.WriteLine(e.Exception.ToString());
                };
                port.RequestReceived += delegate(object sender, PortForwardEventArgs e)
                {
                    Console.WriteLine(string.Format("Port request recieved from {0} : {1}", e.OriginatorHost, e.OriginatorPort));
                };

                client.AddForwardedPort(port);

                //port.Exception += delegate(object sender, ExceptionEventArgs e)
                //{
                //    Console.WriteLine(e.Exception.ToString());
                //};
                port.Start();
                Console.WriteLine("port.Start()");
                // ... hold the port open ... //

                //// create ssh forwarded client on Server2
                //var client2 = new SshClient("localhost:60001", "NMTCRMFTP04", rsa);
                ////client2.ErrorOccurred += _sshclient_ErrorOccurred;
                ////client2.HostKeyReceived += _sshclient_HostKeyReceived;

                //client2.Connect();

                //// Setup Credentials and Server Information
                //ConnectionInfo ConnNfo = new ConnectionInfo("localhost", 60001, "NMTCRMFTP04",
                //    new AuthenticationMethod[]{


                //// Key Based Authentication (using keys in OpenSSH Format)
                //new PrivateKeyAuthenticationMethod("username",rsa)
                //}
                //);

                //// Upload A File
                //using (var sftp = new SftpClient(client.ConnectionInfo))
                //{

                //    //var uploadfn = @"E:\ftproot\ftpuser\www\ssh\OpenSSH\Debug\Debug\NPPRSI001_20171020112153.DAT";


                //    sftp.Connect();
                //    Console.WriteLine("sftp.Connect()");

                //    Console.WriteLine(sftp.WorkingDirectory.ToArray());


                //    sftp.ChangeDirectory("/export/ftpdata1/ESB01/nmtcrmftp04");
                //    Console.WriteLine("sftp.ChangeDirectory()");

                //    //using (var uplfileStream = System.IO.File.OpenRead(uploadfn))
                //    //{
                //    //    sftp.UploadFile(uplfileStream, uploadfn, true);
                //    //    Console.WriteLine("sftp.UploadFile()");
                //    //}

                //    sftp.Disconnect();
                //    Console.WriteLine("sftp.Disconnect()");
                //}

                port.Stop();
                Console.WriteLine("port.Stop()");
                client.Disconnect();
                Console.WriteLine("port.Disconnect()");
            }




            //using (var client = new SshClient("150.63.64.101",22, "NMTCRMFTP", rsa))
            //{
            //    client.Connect();

            //    // Forward the SSH-Port of Device via Server1 on the Desktop-Machine
            //    var port1 = new ForwardedPortLocal("localhost", 60001, "localhost", 22);
            //    port1.Exception += delegate(object sender, ExceptionEventArgs e)
            //    {
            //        Console.WriteLine(e.Exception.ToString());
            //    };
            //    port1.RequestReceived += delegate(object sender, PortForwardEventArgs e)
            //    {
            //        Console.WriteLine(string.Format("Port request recieved from {0} : {1}", e.OriginatorHost, e.OriginatorPort));


            //    };
            //    client.AddForwardedPort(port1);


            //    port1.Start();

            //    // create ssh forwarded client on Server2
            //    var client2 = new SshClient("localhost:60001","NMTCRMFTP04", rsa);
            //    //client2.ErrorOccurred += _sshclient_ErrorOccurred;
            //    //client2.HostKeyReceived += _sshclient_HostKeyReceived;

            //    client2.Connect();



            //}
           

            Console.ReadLine();

        }
    }
}
