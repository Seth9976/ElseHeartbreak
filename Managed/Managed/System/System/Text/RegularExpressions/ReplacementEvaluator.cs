using System;
using System.Text.RegularExpressions.Syntax;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000496 RID: 1174
	internal class ReplacementEvaluator
	{
		// Token: 0x06002A4F RID: 10831 RVA: 0x00091ACC File Offset: 0x0008FCCC
		public ReplacementEvaluator(Regex regex, string replacement)
		{
			this.regex = regex;
			this.replacement = replacement;
			this.pieces = null;
			this.n_pieces = 0;
			this.Compile();
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x00091B04 File Offset: 0x0008FD04
		public static string Evaluate(string replacement, Match match)
		{
			ReplacementEvaluator replacementEvaluator = new ReplacementEvaluator(match.Regex, replacement);
			return replacementEvaluator.Evaluate(match);
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x00091B28 File Offset: 0x0008FD28
		public string Evaluate(Match match)
		{
			if (this.n_pieces == 0)
			{
				return this.replacement;
			}
			StringBuilder stringBuilder = new StringBuilder();
			this.EvaluateAppend(match, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x00091B5C File Offset: 0x0008FD5C
		public void EvaluateAppend(Match match, StringBuilder sb)
		{
			if (this.n_pieces == 0)
			{
				sb.Append(this.replacement);
				return;
			}
			int i = 0;
			while (i < this.n_pieces)
			{
				int num = this.pieces[i++];
				if (num >= 0)
				{
					int num2 = this.pieces[i++];
					sb.Append(this.replacement, num, num2);
				}
				else if (num < -3)
				{
					global::System.Text.RegularExpressions.Group group = match.Groups[-(num + 4)];
					sb.Append(group.Text, group.Index, group.Length);
				}
				else if (num == -1)
				{
					sb.Append(match.Text);
				}
				else if (num == -2)
				{
					sb.Append(match.Text, 0, match.Index);
				}
				else
				{
					int num3 = match.Index + match.Length;
					sb.Append(match.Text, num3, match.Text.Length - num3);
				}
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06002A53 RID: 10835 RVA: 0x00091C68 File Offset: 0x0008FE68
		public bool NeedsGroupsOrCaptures
		{
			get
			{
				return this.n_pieces != 0;
			}
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x00091C78 File Offset: 0x0008FE78
		private void Ensure(int size)
		{
			if (this.pieces == null)
			{
				int num = 4;
				if (num < size)
				{
					num = size;
				}
				this.pieces = new int[num];
			}
			else if (size >= this.pieces.Length)
			{
				int num = this.pieces.Length + (this.pieces.Length >> 1);
				if (num < size)
				{
					num = size;
				}
				int[] array = new int[num];
				Array.Copy(this.pieces, array, this.n_pieces);
				this.pieces = array;
			}
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x00091CF8 File Offset: 0x0008FEF8
		private void AddFromReplacement(int start, int end)
		{
			if (start == end)
			{
				return;
			}
			this.Ensure(this.n_pieces + 2);
			this.pieces[this.n_pieces++] = start;
			this.pieces[this.n_pieces++] = end - start;
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x00091D50 File Offset: 0x0008FF50
		private void AddInt(int i)
		{
			this.Ensure(this.n_pieces + 1);
			this.pieces[this.n_pieces++] = i;
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x00091D84 File Offset: 0x0008FF84
		private void Compile()
		{
			int num = 0;
			int i = 0;
			while (i < this.replacement.Length)
			{
				char c = this.replacement[i++];
				if (c == '$')
				{
					if (i == this.replacement.Length)
					{
						break;
					}
					if (this.replacement[i] == '$')
					{
						this.AddFromReplacement(num, i);
						i = (num = i + 1);
					}
					else
					{
						int num2 = i - 1;
						int num3 = this.CompileTerm(ref i);
						if (num3 < 0)
						{
							this.AddFromReplacement(num, num2);
							this.AddInt(num3);
							num = i;
						}
					}
				}
			}
			if (num != 0)
			{
				this.AddFromReplacement(num, i);
			}
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x00091E40 File Offset: 0x00090040
		private int CompileTerm(ref int ptr)
		{
			char c = this.replacement[ptr];
			if (char.IsDigit(c))
			{
				int num = global::System.Text.RegularExpressions.Syntax.Parser.ParseDecimal(this.replacement, ref ptr);
				if (num < 0 || num > this.regex.GroupCount)
				{
					return 0;
				}
				return -num - 4;
			}
			else
			{
				ptr++;
				char c2 = c;
				switch (c2)
				{
				case '&':
					return -4;
				case '\'':
					return -3;
				default:
				{
					if (c2 == '_')
					{
						return -1;
					}
					if (c2 == '`')
					{
						return -2;
					}
					if (c2 != '{')
					{
						return 0;
					}
					int num2 = -1;
					string text;
					try
					{
						if (char.IsDigit(this.replacement[ptr]))
						{
							num2 = global::System.Text.RegularExpressions.Syntax.Parser.ParseDecimal(this.replacement, ref ptr);
							text = string.Empty;
						}
						else
						{
							text = global::System.Text.RegularExpressions.Syntax.Parser.ParseName(this.replacement, ref ptr);
						}
					}
					catch (IndexOutOfRangeException)
					{
						ptr = this.replacement.Length;
						return 0;
					}
					if (ptr == this.replacement.Length || this.replacement[ptr] != '}' || text == null)
					{
						return 0;
					}
					ptr++;
					if (text != string.Empty)
					{
						num2 = this.regex.GroupNumberFromName(text);
					}
					if (num2 < 0 || num2 > this.regex.GroupCount)
					{
						return 0;
					}
					return -num2 - 4;
				}
				case '+':
					return -this.regex.GroupCount - 4;
				}
			}
		}

		// Token: 0x04001AF8 RID: 6904
		private Regex regex;

		// Token: 0x04001AF9 RID: 6905
		private int n_pieces;

		// Token: 0x04001AFA RID: 6906
		private int[] pieces;

		// Token: 0x04001AFB RID: 6907
		private string replacement;
	}
}
