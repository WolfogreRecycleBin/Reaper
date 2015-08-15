using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolfogre.Tool
{
	class Reaper
	{
		List<string> _baseStrs;

		public Reaper(string baseStr)
		{
			if (baseStr == null || baseStr == "")
				throw new ArgumentNullException("baseStr", "can not be null or empty.");
			_baseStrs = new List<string>();
			_baseStrs.Add(baseStr);
		}

		public Reaper(List<string> baseStrs)
		{
			if (baseStrs == null)
				throw new ArgumentNullException("baseStrs", "can not be null.");
			_baseStrs = new List<string>(baseStrs);
		}

		private List<int> FindIndexOf(string mainStr,string subStr)
		{
			if (subStr == null || subStr == "")
				throw new ArgumentNullException("subStr", "can not be null or empty.");
			List<int> result = new List<int>();
			int startIndex = 0;
			while (mainStr.IndexOf(subStr,startIndex) != -1)
			{
				result.Add(mainStr.IndexOf(subStr, startIndex));
				startIndex = mainStr.IndexOf(subStr, startIndex) + subStr.Length;
			}
			return result;
		}

		public Reaper GetBefore(string subStr)
		{
			if (subStr == null || subStr == "")
				throw new ArgumentNullException("subStr", "can not be null or empty.");
			List<string> result = new List<string>();
			foreach (var str in _baseStrs)
			{
				List<int> indexs = FindIndexOf(str, subStr);
				if (indexs.Count == 0)
					continue;
				for (int i = 0; i < indexs.Count; ++i)
				{
					if (i > 0)
						result.Add(str.Substring(indexs[i - 1] + subStr.Length, indexs[i] - indexs[i - 1] - subStr.Length));
					else
						result.Add(str.Substring(0, indexs[i] - 0));
				}
			}
			return new Reaper(result);
		}

		public Reaper GetAfter(string subStr)
		{
			if (subStr == null || subStr == "")
				throw new ArgumentNullException("subStr", "can not be null or empty.");
			List<string> result = new List<string>();
			foreach (var str in _baseStrs)
			{
				List<int> indexs = FindIndexOf(str,subStr);
				if(indexs.Count == 0)
					continue;
				for(int i = 0; i < indexs.Count; ++i)
				{
					if(i + 1 < indexs.Count)
						result.Add(str.Substring(indexs[i] + subStr.Length, indexs[i + 1] - indexs[i] - subStr.Length));
					else
						result.Add(str.Substring(indexs[i] + subStr.Length, str.Length - indexs[i] - subStr.Length));
				}
			}
			return new Reaper(result);
		}

		public Reaper GetBeforeFirst(string subStr)
		{
			if (subStr == null || subStr == "")
				throw new ArgumentNullException("baseStr", "can not be null.");
			List<string> result = new List<string>();
			foreach (var str in _baseStrs)
			{
				List<int> indexs = FindIndexOf(str, subStr);
				if (indexs.Count == 0)
					continue;
				else
					result.Add(str.Substring(0, indexs[0] - 0));
			}
			return new Reaper(result);
		}

		public Reaper GetAfterFirst(string subStr)
		{
			if (subStr == null || subStr == "")
				throw new ArgumentNullException("baseStr", "can not be null.");
			List<string> result = new List<string>();
			foreach (var str in _baseStrs)
			{
				List<int> indexs = FindIndexOf(str, subStr);
				if (indexs.Count == 0)
					continue;
				else
					result.Add(str.Substring(indexs[0] + subStr.Length, str.Length - indexs[0] - subStr.Length));
			}
			return new Reaper(result);
		}

		public List<string> GetResult()
		{
			return new List<string>(_baseStrs);
		}

		public Reaper Clone()
		{
			return new Reaper(_baseStrs);
		}
	}
}
