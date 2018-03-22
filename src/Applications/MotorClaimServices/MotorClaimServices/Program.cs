using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;



using System.IO.Ports;
using System.Data;
using System.Xml;

namespace MotorClaimServices
{
    class Program
    {
        //private static void DataReceivedHandler(
        //               object sender,
        //               SerialDataReceivedEventArgs e)
        //{
        //    SerialPort sp = (SerialPort)sender;
        //    string indata = sp.ReadExisting();
        //    Console.WriteLine("Data Received:");
        //    Console.Write(indata);
        //}

        static void Main(string[] args)
        {
            string path = @"C:\WorkSpace\Applications\MotorClaimServices\MotorClaimServices\data.txt";

            // Open the file to read from.
            string readText = File.ReadAllText(path);

            DataSet ds = ConvertXMLToDataSet(readText);

            int items = ds.Tables["ClaimTransaction_Data"].Rows.Count;


            //SerialPort mySerialPort = new SerialPort("COM1");

            //mySerialPort.BaudRate = 9600;
            //mySerialPort.Parity = Parity.None;
            //mySerialPort.StopBits = StopBits.One;
            //mySerialPort.DataBits = 8;
            //mySerialPort.Handshake = Handshake.None;
            //mySerialPort.RtsEnable = true;

            //mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            //mySerialPort.Open();

            //Console.WriteLine("Press any key to continue...");
            //Console.WriteLine();
            //Console.ReadKey();
            //mySerialPort.Close();

            using (wsMC.MotorClaimWebService ws = new wsMC.MotorClaimWebService())
            {
                ws.Url = @"http://61.47.16.89/MotorClaimDemo/MotorClaimWebService.asmx";

                List<wsMC.ClaimTransaction_Data> ItemList = new List<wsMC.ClaimTransaction_Data>();

                foreach (DataRow dr in ds.Tables["ClaimTransaction_Data"].Rows)
                {
                    wsMC.ClaimTransaction_Data item = new wsMC.ClaimTransaction_Data();

                    item.ClaimStatus = dr["ClaimStatus"].ToString();
                    item.TempPolicy = dr["TempPolicy"].ToString();
                    item.RefNo = dr["RefNo"].ToString();
                    item.Version = Convert.ToInt32(dr["Version"]);
                    item.PolicyNo = dr["PolicyNo"].ToString();
                    item.PolicyYear = Convert.ToInt32(dr["PolicyYear"]);
                    item.ClaimNo = dr["ClaimNo"].ToString();
                    item.TransactionDate = DateTime.Now.ToString("yyyy-MM-dd"); //dr["TransactionDate"].ToString();
                    item.Unwriter = dr["Unwriter"].ToString();
                    item.InsuredName = dr["InsuredName"].ToString();
                    item.EffectiveDate = dr["EffectiveDate"].ToString();
                    item.ExpiryDate = dr["ExpiryDate"].ToString();
                    item.Beneficiary = dr["Beneficiary"].ToString();
                    item.CarBrand = dr["CarBrand"].ToString();
                    item.CarModel = dr["CarModel"].ToString();
                    item.CarLicense = dr["CarLicense"].ToString();
                    item.CarYear = dr["CarYear"].ToString();
                    item.ChassisNo = dr["ChassisNo"].ToString();
                    item.ShowRoomName = dr["ShowRoomName"].ToString();
                    item.ShowRoomCode = dr["ShowRoomCode"].ToString();
                    item.ClaimNoticeDate = dr["ClaimNoticeDate"].ToString();
                    item.ClaimNoticeTime = dr["ClaimNoticeTime"].ToString();
                    item.ClaimDetails = dr["ClaimDetails"].ToString();
                    item.ClaimType = Convert.ToInt32(dr["ClaimType"]);
                    item.ClaimResult = Convert.ToInt32(dr["ClaimResult"]);
                    item.ClaimDamageDetails = dr["ClaimDamageDetails"].ToString();
                    item.CallCenter = dr["CallCenter"].ToString();
                    item.AccidentDate = dr["AccidentDate"].ToString();
                    item.AccidentTime = dr["AccidentTime"].ToString();
                    item.AccidentPlace = dr["AccidentPlace"].ToString();
                    item.AccidentTumbon = dr["AccidentTumbon"].ToString();
                    item.AccidentAmphur = dr["AccidentAmphur"].ToString();
                    item.AccidentProvince = dr["AccidentProvince"].ToString();
                    item.AccidentZipcode = dr["AccidentZipcode"].ToString();
                    item.DriverName = dr["DriverName"].ToString();
                    item.DriverTel = dr["DriverTel"].ToString();
                    item.NoticeName = dr["NoticeName"].ToString();
                    item.NoticeTel = dr["NoticeTel"].ToString();
                    item.GarageType = Convert.ToInt32(dr["GarageType"]);
                    item.GarageCode = dr["GarageCode"].ToString();
                    item.GarageName = dr["GarageName"].ToString();
                    item.GaragePlace = dr["GaragePlace"].ToString();
                    item.GarageTumbon = dr["GarageTumbon"].ToString();
                    item.GarageAmphur = dr["GarageAmphur"].ToString();
                    item.GarageProvince = dr["GarageProvince"].ToString();
                    item.GarageZipcode = dr["GarageZipcode"].ToString();
                    item.CarRepairDate = dr["CarRepairDate"].ToString();
                    item.CarReceiveDate = dr["CarReceiveDate"].ToString();
                    item.ConsentFormNo = dr["ConsentFormNo"].ToString();
                    item.PartsDealerName = dr["PartsDealerName"].ToString();
                    item.PaymentDetails = dr["PaymentDetails"].ToString();
                    item.Amount1 = Convert.ToDouble(dr["Amount1"]);
                    item.Amount2 = Convert.ToDouble(dr["Amount2"]);
                    item.Amount3 = Convert.ToDouble(dr["Amount3"]);
                    item.Remark = dr["Remark"].ToString();

                    ItemList.Add(item);

                }



                var _result = ws.SendMotorClaim("U00117", "CA435F7D-C52F-4742-BE7C-59972D039412", ItemList.ToArray());
                //Display Message
                foreach (var _result1 in _result.ToList())
                {
                    if (!_result1.ResultStatus)
                    {
                        foreach (var _result2 in _result1.ResultMessage.ToList())
                        {
                            Console.WriteLine(String.Format("{0} - {1}, Result ({2} : {3})", _result1.TRNo, _result1.ResultNo, _result2.ResultCode, _result2.ResultMessage));
                        }
                    }
                }


                ////================= Claim Data ================================
                //wsMC.ClaimTransaction_Data item = new wsMC.ClaimTransaction_Data();
                //item.ClaimStatus = "00";
                //item.TempPolicy = "หมายเลขแจ้งงานของ LWT";
                //item.RefNo = "เลขที่รับแจ้งหรือเลขเคลม";
                //item.Version = 0;
                //item.PolicyNo = "เลขที่กรมธรรมธ์";
                //item.PolicyYear = 0; // ปีต่ออายุ
                //item.ClaimNo = "เลขที่เคลม";
                //item.TransactionDate = "2016-03-23";
                //item.Unwriter = "รหัสประกันภัย";
                //item.InsuredName = "ชื่อผู้เอาประกัน";
                //item.EffectiveDate = "2015-08-09";
                //item.ExpiryDate = "2016-08-09";
                //item.Beneficiary = "ผู้รับผลประโยชน์";
                //item.CarBrand = "ยี่ห้อ";
                //item.CarModel = "รุ่นรถ";
                //item.CarLicense = "ทะเบียนรถ";
                //item.CarYear = "ปีจดทะเบียน";
                //item.ChassisNo = "เลข chassis";
                //item.ShowRoomName = "ชื่อ showroom";
                //item.ShowRoomCode = "9120801";//'รหัส showroom;
                //item.ClaimNoticeDate = "2016-03-23";
                //item.ClaimNoticeTime = "10:30:00";
                //item.ClaimDetails = "การเกิดเหตุ  ตย";
                //item.ClaimType = 1; //'1=เคลมสด, 2=แห้ง
                //item.ClaimResult = 2; // ' 1=ถูก, 2=ผิด, 3=ประมาทร่วม
                //item.ClaimDamageDetails = "-";//"รายละเอียดความเสียหาย ตยitem. 1item.กันชนหน้าขวาครูด ,2item.บังโคลนหน้าขวาครูด"

                //item.CallCenter = "1171";// 'รหัส call center
                //item.AccidentDate = "2016-03-23";
                //item.AccidentTime = "10:30:00";
                //item.AccidentPlace = "สถานที่เกิดเหตุ";
                //item.AccidentTumbon = "ตำบลที่เกิดเหตุ";
                //item.AccidentAmphur = "อำเภอที่เกิดเหตุ";
                //item.AccidentProvince = "จังหวัดที่เกิดเหตุ";
                //item.AccidentZipcode = "รหัสไปรษณีย์ที่เกิดเหตุ";
                //item.DriverName = "ชื่อผู้ขับขี่";
                //item.DriverTel = "เบอร์โทรผู้ขับขี่";
                //item.NoticeName = "ชื่อผู้แจ้งหรือผู้ติดต่อ";
                //item.NoticeTel = "เบอร์ผู้แจ้งหรือผู้ติดต่อ";

                //item.GarageType = 1;// ' 1=BP Shop, 2=Authorized garage,3=Insurance garage, 4=Other
                //item.GarageCode = "9120801";// ' รหัสอู่
                //item.GarageName = "ชื่อศูนย์ซ่อมที่นำรถเข้าซ่อมจริง";
                //item.GaragePlace = "ที่อยู่(อู่นอก)";
                //item.GarageTumbon = "ตำบล/แขวง(อู่นอก)";
                //item.GarageAmphur = "อำเภอ/เขต(อู่นอก)";
                //item.GarageProvince = "จังหวัด(อู่นอก)";
                //item.GarageZipcode = "รหัสไปรษณีย์(อู่นอก)";
                //item.CarRepairDate = "2016-03-23";
                //item.CarReceiveDate = "2016-03-23";
                //item.ConsentFormNo = "ConsentFormNo";// 'เลข consent Form
                //item.PartsDealerName = "ชื่อดีลเลอร์ที่สั่งอะไหล่มา";
                //item.PaymentDetails = "รายการค่าแรงค่าอะไหล่ ตยitem. 1item.ค่าแรงเคาะพ่นสีกันชนหน้า, 2item.ค่าแรงเคาะพ่นสีบังโคลนหน้าขวา";

                //item.Amount1 = 0;// item.0 'ค่าอะไหล่
                //item.Amount2 = 0;// item.0 'ค่าแรง
                //item.Amount3 = 0;// item.0 'ค่ากระจก, ค่าอื่นๆ

                //item.Remark = "หมายเหตุ";
                //ItemList.Add(item);





                //var _result = ws.SendMotorClaim("U00097", "15E15A55-FB60-4015-9407-8F0B6AF08E93", ItemList.ToArray());
                ////Display Message
                //foreach (var _result1 in _result.ToList())
                //{
                //    if (!_result1.ResultStatus)
                //    {
                //        foreach (var _result2 in _result1.ResultMessage.ToList())
                //        {
                //            Console.WriteLine(String.Format("{0} - {1}, Result ({2} : {3})", _result1.TRNo, _result1.ResultNo, _result2.ResultCode, _result2.ResultMessage));
                //        }
                //    }
                //}

                ////================= Consent Form ================================
                //List<wsMC.ClaimTransaction_ConsentForm> cfList = new List<wsMC.ClaimTransaction_ConsentForm>();
                //wsMC.ClaimTransaction_ConsentForm cf = new wsMC.ClaimTransaction_ConsentForm();
                //cf.ConsentFormNo = "F0-59-04/4082";
                //cf.ConsentFormFileType = "docx";
                //cf.ConsentFormData = File.ReadAllBytes(@"C:\Users\dusit\Desktop\MotorClaim\WebImport.docx");
                //cfList.Add(cf);
                //var _result2 = ws.SendConsentForm("UserName", "Password", cfList.ToArray());


                ////Display Message
                //foreach (var _result1 in _result2.ToList())
                //{
                //    if (!_result1.ResultStatus)
                //    {
                //        foreach (var _result3 in _result1.ResultMessage.ToList())
                //        {
                //            Console.WriteLine(String.Format("{0} - {1}, Result ({2} : {3})", _result1.TRNo, _result1.ResultNo, _result3.ResultCode, _result3.ResultMessage));
                //        }
                //    }
                //}

                Console.ReadLine();

            }


        }





        // Function to convert passed XML data to dataset
        static DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                // Load the XmlTextReader from the stream
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }// Use this function to get XML string from a dataset

        // Function to convert passed dataset to XML data
        static string ConvertDataSetToXML(DataSet xmlDS)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;
            try
            {
                stream = new MemoryStream();
                // Load the XmlTextReader from the stream
                writer = new XmlTextWriter(stream, Encoding.Unicode);
                // Write to the file with the WriteXml method.
                xmlDS.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);
                UnicodeEncoding utf = new UnicodeEncoding();
                return utf.GetString(arr).Trim();
            }
            catch
            {
                return String.Empty;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }
    }
}
