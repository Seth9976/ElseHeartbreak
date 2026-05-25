using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000C1 RID: 193
	internal static class StringUtils
	{
		// Token: 0x060008CD RID: 2253 RVA: 0x00020442 File Offset: 0x0001E642
		public static string FormatWith(this string format, IFormatProvider provider, params object[] args)
		{
			ValidationUtils.ArgumentNotNull(format, "format");
			return string.Format(provider, format, args);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00020458 File Offset: 0x0001E658
		public static bool ContainsWhiteSpace(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			for (int i = 0; i < s.Length; i++)
			{
				if (char.IsWhiteSpace(s[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00020498 File Offset: 0x0001E698
		public static bool IsWhiteSpace(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < s.Length; i++)
			{
				if (!char.IsWhiteSpace(s[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000204E0 File Offset: 0x0001E6E0
		public static string EnsureEndsWith(string target, string value)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (target.Length >= value.Length)
			{
				if (string.Compare(target, target.Length - value.Length, value, 0, value.Length, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return target;
				}
				string text = target.TrimEnd(null);
				if (string.Compare(text, text.Length - value.Length, value, 0, value.Length, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return target;
				}
			}
			return target + value;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00020566 File Offset: 0x0001E766
		public static bool IsNullOrEmptyOrWhiteSpace(string s)
		{
			return string.IsNullOrEmpty(s) || StringUtils.IsWhiteSpace(s);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002057D File Offset: 0x0001E77D
		public static void IfNotNullOrEmpty(string value, Action<string> action)
		{
			StringUtils.IfNotNullOrEmpty(value, action, null);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00020587 File Offset: 0x0001E787
		private static void IfNotNullOrEmpty(string value, Action<string> trueAction, Action<string> falseAction)
		{
			if (!string.IsNullOrEmpty(value))
			{
				if (trueAction != null)
				{
					trueAction(value);
					return;
				}
			}
			else if (falseAction != null)
			{
				falseAction(value);
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x000205A6 File Offset: 0x0001E7A6
		public static string Indent(string s, int indentation)
		{
			return StringUtils.Indent(s, indentation, ' ');
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x000205DC File Offset: 0x0001E7DC
		public static string Indent(string s, int indentation, char indentChar)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (indentation <= 0)
			{
				throw new ArgumentException("Must be greater than zero.", "indentation");
			}
			StringReader stringReader = new StringReader(s);
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			StringUtils.ActionTextReaderLine(stringReader, stringWriter, delegate(TextWriter tw, string line)
			{
				tw.Write(new string(indentChar, indentation));
				tw.Write(line);
			});
			return stringWriter.ToString();
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00020650 File Offset: 0x0001E850
		private static void ActionTextReaderLine(TextReader textReader, TextWriter textWriter, StringUtils.ActionLine lineAction)
		{
			bool flag = true;
			string text;
			while ((text = textReader.ReadLine()) != null)
			{
				if (!flag)
				{
					textWriter.WriteLine();
				}
				else
				{
					flag = false;
				}
				lineAction(textWriter, text);
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x000206C8 File Offset: 0x0001E8C8
		public static string NumberLines(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			StringReader stringReader = new StringReader(s);
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			int lineNumber = 1;
			StringUtils.ActionTextReaderLine(stringReader, stringWriter, delegate(TextWriter tw, string line)
			{
				tw.Write(lineNumber.ToString(CultureInfo.InvariantCulture).PadLeft(4));
				tw.Write(". ");
				tw.Write(line);
				lineNumber++;
			});
			return stringWriter.ToString();
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0002071B File Offset: 0x0001E91B
		public static string NullEmptyString(string s)
		{
			if (!string.IsNullOrEmpty(s))
			{
				return s;
			}
			return null;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00020728 File Offset: 0x0001E928
		public static string ReplaceNewLines(string s, string replacement)
		{
			StringReader stringReader = new StringReader(s);
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			string text;
			while ((text = stringReader.ReadLine()) != null)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(replacement);
				}
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002076D File Offset: 0x0001E96D
		public static string Truncate(string s, int maximumLength)
		{
			return StringUtils.Truncate(s, maximumLength, "...");
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0002077C File Offset: 0x0001E97C
		public static string Truncate(string s, int maximumLength, string suffix)
		{
			if (suffix == null)
			{
				throw new ArgumentNullException("suffix");
			}
			if (maximumLength <= 0)
			{
				throw new ArgumentException("Maximum length must be greater than zero.", "maximumLength");
			}
			int num = maximumLength - suffix.Length;
			if (num <= 0)
			{
				throw new ArgumentException("Length of suffix string is greater or equal to maximumLength");
			}
			if (s != null && s.Length > maximumLength)
			{
				string text = s.Substring(0, num);
				text = text.Trim();
				return text + suffix;
			}
			return s;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x000207EC File Offset: 0x0001E9EC
		public static StringWriter CreateStringWriter(int capacity)
		{
			StringBuilder stringBuilder = new StringBuilder(capacity);
			return new StringWriter(stringBuilder, CultureInfo.InvariantCulture);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00020810 File Offset: 0x0001EA10
		public static int? GetLength(string value)
		{
			if (value == null)
			{
				return null;
			}
			return new int?(value.Length);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00020838 File Offset: 0x0001EA38
		public static string ToCharAsUnicode(char c)
		{
			char c2 = MathUtils.IntToHex((int)((c >> 12) & '\u000f'));
			char c3 = MathUtils.IntToHex((int)((c >> 8) & '\u000f'));
			char c4 = MathUtils.IntToHex((int)((c >> 4) & '\u000f'));
			char c5 = MathUtils.IntToHex((int)(c & '\u000f'));
			return new string(new char[] { '\\', 'u', c2, c3, c4, c5 });
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000208A4 File Offset: 0x0001EAA4
		public static void WriteCharAsUnicode(TextWriter writer, char c)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			char c2 = MathUtils.IntToHex((int)((c >> 12) & '\u000f'));
			char c3 = MathUtils.IntToHex((int)((c >> 8) & '\u000f'));
			char c4 = MathUtils.IntToHex((int)((c >> 4) & '\u000f'));
			char c5 = MathUtils.IntToHex((int)(c & '\u000f'));
			writer.Write('\\');
			writer.Write('u');
			writer.Write(c2);
			writer.Write(c3);
			writer.Write(c4);
			writer.Write(c5);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0002095C File Offset: 0x0001EB5C
		public static TSource ForgivingCaseSensitiveFind<TSource>(this IEnumerable<TSource> source, Func<TSource, string> valueSelector, string testValue)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (valueSelector == null)
			{
				throw new ArgumentNullException("valueSelector");
			}
			IEnumerable<TSource> enumerable = source.Where((TSource s) => string.Compare(valueSelector(s), testValue, StringComparison.OrdinalIgnoreCase) == 0);
			if (enumerable.Count<TSource>() <= 1)
			{
				return enumerable.SingleOrDefault<TSource>();
			}
			IEnumerable<TSource> enumerable2 = source.Where((TSource s) => string.Compare(valueSelector(s), testValue, StringComparison.Ordinal) == 0);
			return enumerable2.SingleOrDefault<TSource>();
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x000209E4 File Offset: 0x0001EBE4
		public static string ToCamelCase(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return s;
			}
			if (!char.IsUpper(s[0]))
			{
				return s;
			}
			string text = char.ToLower(s[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
			if (s.Length > 1)
			{
				text += s.Substring(1);
			}
			return text;
		}

		// Token: 0x040002A5 RID: 677
		public const string CarriageReturnLineFeed = "\r\n";

		// Token: 0x040002A6 RID: 678
		public const string Empty = "";

		// Token: 0x040002A7 RID: 679
		public const char CarriageReturn = '\r';

		// Token: 0x040002A8 RID: 680
		public const char LineFeed = '\n';

		// Token: 0x040002A9 RID: 681
		public const char Tab = '\t';

		// Token: 0x020000C2 RID: 194
		// (Invoke) Token: 0x060008E3 RID: 2275
		private delegate void ActionLine(TextWriter textWriter, string line);
	}
}
