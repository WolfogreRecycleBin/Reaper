using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wolfogre.Tool;
using System.IO;

namespace TestReaper
{
	class Program
	{
		static void Main(string[] args)
		{
			Reaper reaper = new Reaper((new StreamReader("03,073.html")).ReadToEnd());
			StreamWriter outputFile = new StreamWriter("output.txt");
			List<string> partResult =
				reaper
				.RemainBeforeFirst("<table width=\"100%\" summary=\"table used for formatting\"><tr><td>")
				.ReapByProfix("<hr><big><b><i>")
				.GetResult();
			foreach(var part in partResult)
			{
				string partName = (new Reaper(part)).RemainBeforeFirst(":</i></b></big>").GetResult()[0];
				//ShowStrings((new Reaper(part)).RemainBeforeFirst(":</i></b></big>").GetResult());
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
						ShowStrings((new Reaper(line)).RemainBeforeFirst("</td>").GetResult());
						outputFile.WriteLine("LINE:" + lineName + " " + ++lineCount);
						List<string> dataResult =
												(new Reaper(line))
												.ReapByProfix("<td align=\"center\" nowrap>")
												.RemainBeforeFirst("</td>")
												.GetResult();
						foreach(var data in dataResult)
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
