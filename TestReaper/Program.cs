using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wolfogre.Tool;

namespace TestReaper
{
	class Program
	{
		static void Main(string[] args)
		{
			Reaper reaper = new Reaper((new System.IO.StreamReader("03,073.html")).ReadToEnd());
			List<string> result =
				reaper
				//.DeleteAfterLast("<a href=\"/cgi-bin/sse/sse.cgi?\"><img src=\"/sse/images/back.jpg\"")
				//.ReapByProfix("<div align=\"center\"><table border=1 summary=\"")
				//.ReapByProfix("<tr><td>")
				.ReapByProfix("<td align=\"center\" nowrap>")
				.DeleteAfterFirst("</td>")
				.GetResult();
			ShowStrings(result);
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
