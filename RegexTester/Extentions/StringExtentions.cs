using RegexTester;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System
{
	public static class StringExtentions
	{
		public static string ShowSpecialSymbols(this string text)
		{
			return Regex.Escape(text);
		}

		public static string DontShowSpecialSymbols(this string text)
		{
			return Regex.Unescape(text);
		}
	}
}
