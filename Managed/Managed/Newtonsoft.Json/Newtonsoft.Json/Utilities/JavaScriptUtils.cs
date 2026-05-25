using System;
using System.IO;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000B5 RID: 181
	internal static class JavaScriptUtils
	{
		// Token: 0x06000831 RID: 2097 RVA: 0x0001DA24 File Offset: 0x0001BC24
		public static void WriteEscapedJavaScriptString(TextWriter writer, string value, char delimiter, bool appendDelimiters)
		{
			if (appendDelimiters)
			{
				writer.Write(delimiter);
			}
			if (value != null)
			{
				int num = 0;
				int num2 = 0;
				char[] array = null;
				int i = 0;
				while (i < value.Length)
				{
					char c = value[i];
					char c2 = c;
					string text;
					if (c2 <= '\'')
					{
						switch (c2)
						{
						case '\b':
							text = "\\b";
							break;
						case '\t':
							text = "\\t";
							break;
						case '\n':
							text = "\\n";
							break;
						case '\v':
							goto IL_00FE;
						case '\f':
							text = "\\f";
							break;
						case '\r':
							text = "\\r";
							break;
						default:
							if (c2 != '"')
							{
								if (c2 != '\'')
								{
									goto IL_00FE;
								}
								text = ((delimiter == '\'') ? "\\'" : null);
							}
							else
							{
								text = ((delimiter == '"') ? "\\\"" : null);
							}
							break;
						}
					}
					else if (c2 != '\\')
					{
						if (c2 != '\u0085')
						{
							switch (c2)
							{
							case '\u2028':
								text = "\\u2028";
								break;
							case '\u2029':
								text = "\\u2029";
								break;
							default:
								goto IL_00FE;
							}
						}
						else
						{
							text = "\\u0085";
						}
					}
					else
					{
						text = "\\\\";
					}
					IL_0110:
					if (text != null)
					{
						if (array == null)
						{
							array = value.ToCharArray();
						}
						if (num2 > 0)
						{
							writer.Write(array, num, num2);
							num2 = 0;
						}
						writer.Write(text);
						num = i + 1;
					}
					else
					{
						num2++;
					}
					i++;
					continue;
					IL_00FE:
					text = ((c <= '\u001f') ? StringUtils.ToCharAsUnicode(c) : null);
					goto IL_0110;
				}
				if (num2 > 0)
				{
					if (num == 0)
					{
						writer.Write(value);
					}
					else
					{
						writer.Write(array, num, num2);
					}
				}
			}
			if (appendDelimiters)
			{
				writer.Write(delimiter);
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001DBA3 File Offset: 0x0001BDA3
		public static string ToEscapedJavaScriptString(string value)
		{
			return JavaScriptUtils.ToEscapedJavaScriptString(value, '"', true);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001DBB0 File Offset: 0x0001BDB0
		public static string ToEscapedJavaScriptString(string value, char delimiter, bool appendDelimiters)
		{
			string text;
			using (StringWriter stringWriter = StringUtils.CreateStringWriter(StringUtils.GetLength(value) ?? 16))
			{
				JavaScriptUtils.WriteEscapedJavaScriptString(stringWriter, value, delimiter, appendDelimiters);
				text = stringWriter.ToString();
			}
			return text;
		}
	}
}
