using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000025 RID: 37
	internal class JPath
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000062A5 File Offset: 0x000044A5
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000062AD File Offset: 0x000044AD
		public List<object> Parts { get; private set; }

		// Token: 0x06000141 RID: 321 RVA: 0x000062B6 File Offset: 0x000044B6
		public JPath(string expression)
		{
			ValidationUtils.ArgumentNotNull(expression, "expression");
			this._expression = expression;
			this.Parts = new List<object>();
			this.ParseMain();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000062E4 File Offset: 0x000044E4
		private void ParseMain()
		{
			int num = this._currentIndex;
			bool flag = false;
			while (this._currentIndex < this._expression.Length)
			{
				char c = this._expression[this._currentIndex];
				char c2 = c;
				switch (c2)
				{
				case '(':
					goto IL_0056;
				case ')':
					goto IL_0094;
				default:
					if (c2 != '.')
					{
						switch (c2)
						{
						case '[':
							goto IL_0056;
						case ']':
							goto IL_0094;
						}
						if (flag)
						{
							throw new Exception("Unexpected character following indexer: " + c);
						}
					}
					else
					{
						if (this._currentIndex > num)
						{
							string text = this._expression.Substring(num, this._currentIndex - num);
							this.Parts.Add(text);
						}
						num = this._currentIndex + 1;
						flag = false;
					}
					break;
				}
				IL_00FC:
				this._currentIndex++;
				continue;
				IL_0056:
				if (this._currentIndex > num)
				{
					string text2 = this._expression.Substring(num, this._currentIndex - num);
					this.Parts.Add(text2);
				}
				this.ParseIndexer(c);
				num = this._currentIndex + 1;
				flag = true;
				goto IL_00FC;
				IL_0094:
				throw new Exception("Unexpected character while parsing path: " + c);
			}
			if (this._currentIndex > num)
			{
				string text3 = this._expression.Substring(num, this._currentIndex - num);
				this.Parts.Add(text3);
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006440 File Offset: 0x00004640
		private void ParseIndexer(char indexerOpenChar)
		{
			this._currentIndex++;
			char c = ((indexerOpenChar == '[') ? ']' : ')');
			int currentIndex = this._currentIndex;
			int num = 0;
			bool flag = false;
			while (this._currentIndex < this._expression.Length)
			{
				char c2 = this._expression[this._currentIndex];
				if (char.IsDigit(c2))
				{
					num++;
					this._currentIndex++;
				}
				else
				{
					if (c2 == c)
					{
						flag = true;
						break;
					}
					throw new Exception("Unexpected character while parsing path indexer: " + c2);
				}
			}
			if (!flag)
			{
				throw new Exception("Path ended with open indexer. Expected " + c);
			}
			if (num == 0)
			{
				throw new Exception("Empty path indexer.");
			}
			string text = this._expression.Substring(currentIndex, num);
			this.Parts.Add(Convert.ToInt32(text, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000652C File Offset: 0x0000472C
		internal JToken Evaluate(JToken root, bool errorWhenNoMatch)
		{
			JToken jtoken = root;
			foreach (object obj in this.Parts)
			{
				string text = obj as string;
				if (text != null)
				{
					JObject jobject = jtoken as JObject;
					if (jobject != null)
					{
						jtoken = jobject[text];
						if (jtoken == null && errorWhenNoMatch)
						{
							throw new Exception("Property '{0}' does not exist on JObject.".FormatWith(CultureInfo.InvariantCulture, new object[] { text }));
						}
					}
					else
					{
						if (errorWhenNoMatch)
						{
							throw new Exception("Property '{0}' not valid on {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
							{
								text,
								jtoken.GetType().Name
							}));
						}
						return null;
					}
				}
				else
				{
					int num = (int)obj;
					JArray jarray = jtoken as JArray;
					if (jarray != null)
					{
						if (jarray.Count <= num)
						{
							if (errorWhenNoMatch)
							{
								throw new IndexOutOfRangeException("Index {0} outside the bounds of JArray.".FormatWith(CultureInfo.InvariantCulture, new object[] { num }));
							}
							return null;
						}
						else
						{
							jtoken = jarray[num];
						}
					}
					else
					{
						if (errorWhenNoMatch)
						{
							throw new Exception("Index {0} not valid on {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
							{
								num,
								jtoken.GetType().Name
							}));
						}
						return null;
					}
				}
			}
			return jtoken;
		}

		// Token: 0x04000082 RID: 130
		private readonly string _expression;

		// Token: 0x04000083 RID: 131
		private int _currentIndex;
	}
}
