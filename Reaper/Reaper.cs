﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;

namespace Wolfogre.Tool
{
	public class Reaper : IEnumerable, IEnumerator
	{
		List<string> _baseStrs;

		public Reaper(string baseStr)
		{
			if (baseStr == null)
				throw new ArgumentNullException("baseStr", "Can not be null.");
			if (baseStr == String.Empty)
				throw new ArgumentNullException("baseStr", "Can not be empty.");
			_baseStrs = new List<string>();
			_baseStrs.Add(baseStr);
		}

		public Reaper(List<string> baseStrs)
		{
			if (baseStrs == null)
				throw new ArgumentNullException("baseStrs", "Can not be null.");
			if (baseStrs.Count == 0)
				throw new ArgumentNullException("baseStrs", "Can not be empty.");
			_baseStrs = new List<string>();
			foreach (var str in baseStrs)
			{
				if (str != String.Empty)
					_baseStrs.Add(str);
			}
			if (baseStrs.Count == 0)
			{
				throw new ArgumentNullException("baseStrs", "Can not contain only empty strings.");
			}
		}

		public List<string> GetResult()
		{
			List<string> result = new List<string>();
			foreach (var str in _baseStrs)
			{
				if (str != String.Empty)
					result.Add(str);
			}
			return new List<string>(_baseStrs);
		}

		public Reaper Clone()
		{
			return new Reaper(_baseStrs);
		}

		public Reaper ReapBySuffix(string suffix)
		{
			if (suffix == null)
				throw new ArgumentNullException("profix", "Can not be null.");
			if (suffix == String.Empty)
				throw new ArgumentNullException("profix", "Can not be empty.");
			List<string> result = new List<string>();
			foreach (var str in _baseStrs)
			{
				List<int> indexs = FindIndexOf(str, suffix);
				if (indexs.Count == 0)
					continue;
				for (int i = 0; i < indexs.Count; ++i)
				{
					if (i > 0)
						result.Add(str.Substring(indexs[i - 1] + suffix.Length, indexs[i] - indexs[i - 1] - suffix.Length));
					else
						result.Add(str.Substring(0, indexs[i] - 0));
				}
			}
			return new Reaper(result);
		}

		public Reaper ReapByProfix(string profix)
		{
			if (profix == null)
				throw new ArgumentNullException("profix", "Can not be null.");
			if (profix == String.Empty)
				throw new ArgumentNullException("profix", "Can not be empty.");
			List<string> result = new List<string>();
			foreach (var str in _baseStrs)
			{
				List<int> indexs = FindIndexOf(str, profix);
				if (indexs.Count == 0)
					continue;
				for (int i = 0; i < indexs.Count; ++i)
				{
					if (i + 1 < indexs.Count)
						result.Add(str.Substring(indexs[i] + profix.Length, indexs[i + 1] - indexs[i] - profix.Length));
					else
						result.Add(str.Substring(indexs[i] + profix.Length, str.Length - indexs[i] - profix.Length));
				}
			}
			return new Reaper(result);
		}

		public Reaper RemainBeforeFirst(string subStr)
		{
			if (subStr == null)
				throw new ArgumentNullException("subStr", "Can not be null.");
			if (subStr == String.Empty)
				throw new ArgumentNullException("subStr", "Can not be empty.");
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

		public Reaper RemainAfterFirst(string subStr)
		{
			if (subStr == null)
				throw new ArgumentNullException("subStr", "Can not be null.");
			if (subStr == String.Empty)
				throw new ArgumentNullException("subStr", "Can not be empty.");
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

		public Reaper RemainBeforeLast(string subStr)
		{
			if (subStr == null)
				throw new ArgumentNullException("subStr", "Can not be null.");
			if (subStr == String.Empty)
				throw new ArgumentNullException("subStr", "Can not be empty.");
			List<string> result = new List<string>();
			foreach (var str in _baseStrs)
			{
				List<int> indexs = FindIndexOf(str, subStr);
				if (indexs.Count == 0)
					continue;
				else
					result.Add(str.Substring(0, indexs[indexs.Count - 1] - 0));
			}
			return new Reaper(result);
		}

		public Reaper RemainAfterLast(string subStr)
		{
			if (subStr == null)
				throw new ArgumentNullException("subStr", "Can not be null.");
			if (subStr == String.Empty)
				throw new ArgumentNullException("subStr", "Can not be empty.");
			List<string> result = new List<string>();
			foreach (var str in _baseStrs)
			{
				List<int> indexs = FindIndexOf(str, subStr);
				if (indexs.Count == 0)
					continue;
				else
					result.Add(str.Substring(indexs[indexs.Count - 1] + subStr.Length, str.Length - indexs[indexs.Count - 1] - subStr.Length));
			}
			return new Reaper(result);
		}

		public Reaper DeleteBeforeFirst(string subStr)
		{
			if (subStr == null)
				throw new ArgumentNullException("subStr", "Can not be null.");
			if (subStr == String.Empty)
				throw new ArgumentNullException("subStr", "Can not be empty.");
			List<string> result = new List<string>(_baseStrs);
			for (int i = 0; i < result.Count; ++i)
			{
				List<int> indexs = FindIndexOf(result[i], subStr);
				if (indexs.Count == 0)
					continue;
				else
					result[i] = result[i].Substring(indexs[0], result[i].Length - indexs[0]);
			}
			return new Reaper(result);
		}

		public Reaper DeleteAfterFirst(string subStr)
		{
			if (subStr == null)
				throw new ArgumentNullException("subStr", "Can not be null.");
			if (subStr == String.Empty)
				throw new ArgumentNullException("subStr", "Can not be empty.");
			List<string> result = new List<string>(_baseStrs);
			for (int i = 0; i < result.Count; ++i)
			{
				List<int> indexs = FindIndexOf(result[i], subStr);
				if (indexs.Count == 0)
					continue;
				else
					result[i] = result[i].Substring(0, indexs[0] + subStr.Length);
			}
			return new Reaper(result);
		}

		public Reaper DeleteBeforeLast(string subStr)
		{
			if (subStr == null)
				throw new ArgumentNullException("subStr", "Can not be null.");
			if (subStr == String.Empty)
				throw new ArgumentNullException("subStr", "Can not be empty.");
			List<string> result = new List<string>(_baseStrs);
			for (int i = 0; i < result.Count; ++i)
			{
				List<int> indexs = FindIndexOf(result[i], subStr);
				if (indexs.Count == 0)
					continue;
				else
					result[i] = result[i].Substring(indexs[indexs.Count - 1], result[i].Length - indexs[indexs.Count - 1]);
			}
			return new Reaper(result);
		}

		public Reaper DeleteAfterLast(string subStr)
		{
			if (subStr == null)
				throw new ArgumentNullException("subStr", "Can not be null.");
			if (subStr == String.Empty)
				throw new ArgumentNullException("subStr", "Can not be empty.");
			List<string> result = new List<string>(_baseStrs);
			for (int i = 0; i < result.Count; ++i)
			{
				List<int> indexs = FindIndexOf(result[i], subStr);
				if (indexs.Count == 0)
					continue;
				else
					result[i] = result[i].Substring(0, indexs[indexs.Count - 1] + subStr.Length);
			}
			return new Reaper(result);
		}

		public Reaper GiveUpContain(string subStr)
		{
			if (subStr == null)
				throw new ArgumentNullException("subStr", "Can not be null.");
			if (subStr == String.Empty)
				throw new ArgumentNullException("subStr", "Can not be empty.");
			List<string> result = new List<string>(_baseStrs);
			for (int i = 0; i < result.Count; ++i)
			{
				if (result[i].IndexOf(subStr) != -1)
					result[i] = "";
				//直接删除会造成result的索引失效，故置空，构造Reaper或返回Result的时候会被忽略
			}
			return new Reaper(result);
		}

		public Reaper GiveUpNotContain(string subStr)
		{
			if (subStr == null)
				throw new ArgumentNullException("subStr", "Can not be null.");
			if (subStr == String.Empty)
				throw new ArgumentNullException("subStr", "Can not be empty.");
			List<string> result = new List<string>(_baseStrs);
			for (int i = 0; i < result.Count; ++i)
			{
				if (result[i].IndexOf(subStr) == -1)
					result[i] = "";
				//直接删除会造成result的索引失效，故置空，构造Reaper或返回Result的时候会被忽略
			}
			return new Reaper(result);
		}

		private List<int> FindIndexOf(string mainStr, string subStr)
		{
			if (subStr == null || subStr == "")
				throw new ArgumentNullException("profix", "Can not be null or empty.");
			List<int> result = new List<int>();
			int startIndex = 0;
			while (mainStr.IndexOf(subStr, startIndex) != -1)
			{
				result.Add(mainStr.IndexOf(subStr, startIndex));
				startIndex = mainStr.IndexOf(subStr, startIndex) + subStr.Length;
			}
			return result;
		}

		//实现迭代器接口
		int index = -1;

		public IEnumerator GetEnumerator()
		{
			return this;
		}

		public void Reset()
		{
			index = -1;
		}

		public object Current
		{
			get
			{
				return new Reaper(GetResult()[index]);
			}
		}

		public bool MoveNext()
		{
			++index;
			if (index >= GetResult().Count)
			{
				index = -1;
				return false;
			}
			else
				return true;
		}
	}
}
