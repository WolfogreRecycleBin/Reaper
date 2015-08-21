using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wolfogre.Tool;
using System.IO;
using System.Threading;

namespace TestReaper
{
	class Program
	{
		static void Main(string[] args)
		{
			test6();
		}

		static void test6()
		{
			int countTimes = 0;
			while (countTimes < 10000)
			{
				test4();
				Console.WriteLine(++countTimes);
			}
		}

		static void test5()
		{
			Thread[] ths = new Thread[10];
			int countTimes = 0;
			while (countTimes < 10000)
			{
				for (int i = 0; i < 10; ++i)
				{
					if (ths[i] == null || ths[i].ThreadState == ThreadState.Stopped)
					{
						ths[i] = new Thread(new ThreadStart(test4));
						ths[i].Start();
						Console.WriteLine(++countTimes);
					}
				}
			}	
		}

		static void test4()
		{
			string originString = (new StreamReader("03,073.html")).ReadToEnd();
			DateTime dt = DateTime.Now;
			Reaper reaper = new Reaper(originString);
			foreach (Reaper part in reaper.RemainBeforeFirst("<table width=\"100%\" summary=\"table used for formatting\"><tr><td>").ReapByProfix("<hr><big><b><i>"))
			{
				string partName = part.RemainBeforeFirst(":</i></b></big>").GetResult()[0];
				foreach (Reaper table in part.ReapByProfix("<div align=\"center\"><table border=1 summary=\""))
				{
					string tableName = table.RemainBeforeFirst("\" width=\"95%\">").GetResult()[0];

					foreach (Reaper line in table.ReapByProfix("<tr><td>").GiveUpContain("<td>Jan</td><td>Feb</td><td>Mar</td>"))
					{
						string lineName = line.RemainBeforeFirst("</td>").GetResult()[0];
						/////////
						foreach (Reaper data in line.ReapByProfix("<td align=\"center\" nowrap>").RemainBeforeFirst("</td>"))
						{
							/////////
						}
						/////////////
					}
				}
			}
			////////////
			TimeSpan ts = DateTime.Now - dt;
			Console.WriteLine(ts);
		}

		static void test3()
		{
			Reaper reaper = new Reaper((new StreamReader("03,073.html")).ReadToEnd());
			StreamWriter outputFile = new StreamWriter("test3_output.txt");
			foreach (Reaper part in reaper.RemainBeforeFirst("<table width=\"100%\" summary=\"table used for formatting\"><tr><td>").ReapByProfix("<hr><big><b><i>"))
			{
				string partName = part.RemainBeforeFirst(":</i></b></big>").GetResult()[0];
				ShowStrings(part.RemainBeforeFirst(":</i></b></big>").GetResult());
				foreach (Reaper table in part.ReapByProfix("<div align=\"center\"><table border=1 summary=\""))
				{
					string tableName = table.RemainBeforeFirst("\" width=\"95%\">").GetResult()[0];

					int lineCount = 0;
					foreach (Reaper line in table.ReapByProfix("<tr><td>").GiveUpContain("<td>Jan</td><td>Feb</td><td>Mar</td>"))
					{
						string lineName = line.RemainBeforeFirst("</td>").GetResult()[0];

						outputFile.WriteLine("PART:" + partName);
						outputFile.WriteLine("TABLE:" + tableName);
						outputFile.WriteLine("LINE:" + lineName);
						outputFile.WriteLine("NO.:" + ++lineCount);
						foreach (Reaper data in line.ReapByProfix("<td align=\"center\" nowrap>").RemainBeforeFirst("</td>"))
						{
							outputFile.Write(data.GetResult()[0] + " ");
						}
						outputFile.WriteLine();
					}
				}
			}
			outputFile.Close();
		}


		static void test2()
		{
			Reaper reaper = new Reaper((new StreamReader("03,073.html")).ReadToEnd());
			StreamWriter outputFile = new StreamWriter("test2_output.txt");
			foreach (Reaper part in reaper.RemainBeforeFirst("<table width=\"100%\" summary=\"table used for formatting\"><tr><td>").ReapByProfix("<hr><big><b><i>"))
			{
				string partName = part.RemainBeforeFirst(":</i></b></big>").GetResult()[0];
				ShowStrings(part.RemainBeforeFirst(":</i></b></big>").GetResult());
				outputFile.WriteLine("----------PART:" + partName);
				foreach (Reaper table in part.ReapByProfix("<div align=\"center\"><table border=1 summary=\""))
				{
					string tableName = table.RemainBeforeFirst("\" width=\"95%\">").GetResult()[0];
					ShowStrings(table.RemainBeforeFirst("\" width=\"95%\">").GetResult());
					outputFile.WriteLine("-----TABLE:" + tableName);

					int lineCount = 0;
					foreach (Reaper line in table.ReapByProfix("<tr><td>").GiveUpContain("<td>Jan</td><td>Feb</td><td>Mar</td>"))
					{
						string lineName = line.RemainBeforeFirst("</td>").GetResult()[0];
						ShowStrings(line.RemainBeforeFirst("</td>").GetResult());
						outputFile.WriteLine("LINE:" + lineName + " " + ++lineCount);
						foreach (Reaper data in line.ReapByProfix("<td align=\"center\" nowrap>").RemainBeforeFirst("</td>"))
						{
							outputFile.Write(data.GetResult()[0] + " ");
						}
						outputFile.WriteLine();
					}
				}
			}
			outputFile.Close();
		}




		static void test1()
		{
			Reaper reaper = new Reaper((new StreamReader("03,073.html")).ReadToEnd());
			StreamWriter outputFile = new StreamWriter("test1_output.txt");
			List<string> partResult =
				reaper
				.RemainBeforeFirst("<table width=\"100%\" summary=\"table used for formatting\"><tr><td>")
				.ReapByProfix("<hr><big><b><i>")
				.GetResult();
			foreach (var part in partResult)
			{
				string partName = (new Reaper(part)).RemainBeforeFirst(":</i></b></big>").GetResult()[0];
				ShowStrings((new Reaper(part)).RemainBeforeFirst(":</i></b></big>").GetResult());
				outputFile.WriteLine("----------PART:" + partName);
				List<string> tableResult =
											(new Reaper(part))
											.ReapByProfix("<div align=\"center\"><table border=1 summary=\"")
											.GetResult();
				foreach (var table in tableResult)
				{
					string tableName = (new Reaper(table)).RemainBeforeFirst("\" width=\"95%\">").GetResult()[0];
					//ShowStrings((new Reaper(table)).RemainBeforeFirst("\" width=\"95%\">").GetResult());
					outputFile.WriteLine("-----TABLE:" + tableName);
					List<string> lineResult =
												(new Reaper(table))
												.ReapByProfix("<tr><td>")
												.GiveUpContain("<td>Jan</td><td>Feb</td><td>Mar</td>")
												.GetResult();
					int lineCount = 0;
					foreach (var line in lineResult)
					{
						string lineName = (new Reaper(line)).RemainBeforeFirst("</td>").GetResult()[0];
						//ShowStrings((new Reaper(line)).RemainBeforeFirst("</td>").GetResult());
						outputFile.WriteLine("LINE:" + lineName + " " + ++lineCount);
						List<string> dataResult =
												(new Reaper(line))
												.ReapByProfix("<td align=\"center\" nowrap>")
												.RemainBeforeFirst("</td>")
												.GetResult();
						foreach (var data in dataResult)
						{
							outputFile.Write(data + " ");
						}
						outputFile.WriteLine();
					}
				}
			}
			outputFile.Close();
		}




		static int count = 0;
		static private bool ResetCount()
		{
			count = 0;
			return true;
		}
		static private bool ShowStrings(List<string> strings)
		{
			if (strings == null)
				return false;
			foreach (var str in strings)
			{
				Console.Write(++count + ":");
				Console.WriteLine("-------------------------------------------------------------------------");
				Console.WriteLine(str);
			}
			return true;
		}
	}
}
