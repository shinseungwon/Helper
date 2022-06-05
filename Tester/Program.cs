using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Helper;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {            
            //string input = "";

            //ConfigHelper ch = new ConfigHelper("Config.txt");
            //Console.WriteLine(ch.GetConfig("test"));

            //string ip = "175.121.130.13,14333";
            //string port = "14333";
            //string catalog = "SSWARE";
            //string id = "sa";
            //string password = "sungwon530";

            //string connectionString = "Data Source=" + ip + "," + port + "; Initial Catalog=" + catalog
            //    + "; User id=" + id + "; Password=" + password + ";";

            //LogHelper l = new LogHelper();
            //DbHelper dh = new DbHelper(connectionString);
            //dh.SetLogger(l);

            //WebHelper wh = new WebHelper();
            //wh.SetLogger(l);

            //Console.WriteLine(wh.Request("https://www.naver.com"));

            //l.WriteText("TestLog");
            //l.WriteFile("TestLog", "mytest.xml");

            //DbHelper
            //DataSet ds = new DataSet();
            //dh.CallQuery("select * from information_schema.tables(nolock)", ref ds);
            //string res = GridHelper.GetJson(ds.Tables[0]);
            //Console.WriteLine(res);
            //dh.CallQuery("create table test ( id int not null, qty int )", ref ds);
            //Console.WriteLine(dh.latestError);
            //foreach(DataRow r in ds.Tables[0].Rows)
            //{
            //    foreach(DataColumn c in ds.Tables[0].Columns)
            //    {
            //        Console.Write(r[c] + " ");
            //    }
            //    Console.WriteLine();
            //}

            ////ERDHelper
            //ERDHelper eh = new ERDHelper(dh);
            //eh.SetLogger(l);

            ////WebHelper
            //WebHelper wh = new WebHelper();
            //wh.SetLogger(l);
            //wh.logToDB = true;
            //string res = wh.Request("https://www.naver.com");
            //Console.WriteLine(res);

            ////FileHelper
            //FileHelper fh = new FileHelper("ftp://localhost/Root", "testftpuser", "1111", dh, l);
            //string sourceUploadFilePath = Directory.GetCurrentDirectory() + @"\Files\Test.txt";
            //fh.Upload(sourceUploadFilePath, "TestDest.txt");
            //string targetDownloadFilePath = Directory.GetCurrentDirectory() + @"\Files\Test2.txt";
            //fh.Download(targetDownloadFilePath, "TestDest.txt");

            ////MailHelper
            //sswaresmtp@gmail.com/sungwon530
            //MailHelper mh = new MailHelper("smtp.gmail.com", 587, "sswaresmtp@gmail.com", "sungwon530");
            //mh.SetLogger(l);
            //mh.Send("MyTest", "TestBody xxx", new string[] { "ssw900528@gmail.com" });
        }
    }
}