using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace Clinic.Modules
{
    public class dataJSON
    {
        public void writeLog(string regID = "", string status = "", string fileName = "", string des = "", string par="",string errCatch = "")
        {
            string path = @"D:\MyLog\DailyLog\Clinic";
            string desPath = @"D:\MyLog\AllLog\Clinic";
            string filePath = path + DateTime.Now.ToString("yy-MM-dd");
            if (!File.Exists(filePath))
            {
                moveFile(path, desPath);
            }
            StringBuilder log = new StringBuilder();
            try
            {
                string space = "    ";
                log.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss   "));
                log.Append("RegID:" + regID + space);
                log.Append("Status:" + status + space);
                log.Append("controller:" + fileName + space);
                log.Append("Description:" + des + space);
                log.Append("Par:" + par + space);
                log.Append("ErrorCatch:" + errCatch);
                using (var ws=new StreamWriter(filePath, true))
                {
                    ws.WriteLine(log);
                    ws.Close();
                }
            }
            catch{}
            finally
            {
                log.Clear();
            }
        }
        private void moveFile(string srcPath,string tagetPath)
        {
            string fileName = "", destFile = "";
            try
            {
                string[] files = Directory.GetFiles(srcPath);
                foreach(string item in files)
                {
                    fileName = Path.GetFileName(item);
                    destFile = Path.Combine(tagetPath, fileName);
                    if (!File.Exists(destFile))
                    {
                        File.Move(item, destFile);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                srcPath = tagetPath = "";
            }
        }
    }
}