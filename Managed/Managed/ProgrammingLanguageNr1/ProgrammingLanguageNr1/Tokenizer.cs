using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000016 RID: 22
	public class Tokenizer
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00006CC0 File Offset: 0x00004EC0
		public Tokenizer(ErrorHandler errorHandler, bool stripOutComments)
		{
			this.m_errorHandler = errorHandler;
			this.m_stripOutComments = stripOutComments;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00006CF8 File Offset: 0x00004EF8
		public List<Token> process(TextReader textReader)
		{
			Debug.Assert(textReader != null);
			this.m_tokens = new List<Token>();
			this.m_textReader = textReader;
			this.m_endOfFile = false;
			this.readNextChar();
			this.m_currentLine = 1;
			this.m_currentPosition = 0;
			this.m_currentTokenStartPosition = 0;
			Token token;
			do
			{
				token = this.readNextToken();
				token.LineNr = this.m_currentLine;
				token.LinePosition = this.m_currentTokenStartPosition;
				this.m_currentTokenStartPosition = this.m_currentPosition;
				this.m_tokens.Add(token);
			}
			while (token.getTokenType() != Token.TokenType.EOF);
			this.m_textReader.Close();
			this.m_textReader.Dispose();
			return this.m_tokens;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00006DA4 File Offset: 0x00004FA4
		private Token readNextToken()
		{
			while (!this.m_endOfFile)
			{
				char currentChar = this.m_currentChar;
				switch (currentChar)
				{
				case ' ':
				case ';':
					break;
				case '!':
				case '&':
				case '*':
				case '+':
				case '-':
				case '/':
				case '<':
				case '=':
				case '>':
					goto IL_0105;
				case '"':
					return this.QUOTED_STRING(true);
				case '#':
					if (this.m_stripOutComments)
					{
						this.stripComment();
						continue;
					}
					return this.COMMENT();
				default:
					switch (currentChar)
					{
					case '[':
						return this.BRACKET_LEFT();
					default:
						if (currentChar != '\t')
						{
							if (currentChar == '\n')
							{
								return this.NEW_LINE();
							}
							if (currentChar == '\0')
							{
								this.m_endOfFile = true;
								continue;
							}
							if (currentChar == '|')
							{
								goto IL_0105;
							}
							if (this.isLETTER())
							{
								return this.NAME();
							}
							if (this.isDIGIT())
							{
								return this.NUMBER(false);
							}
							if (this.m_currentChar == Tokenizer.WINDOWS_LINE_ENDING_CRAP)
							{
								return this.NEW_LINE();
							}
							this.m_errorHandler.errorOccured(string.Concat(new object[]
							{
								"Can't understand this character: '",
								this.m_currentChar,
								"' (int code ",
								(int)this.m_currentChar,
								")"
							}), Error.ErrorType.SYNTAX, this.m_currentLine, this.m_currentPosition);
							this.readNextChar();
							continue;
						}
						break;
					case ']':
						return this.BRACKET_RIGHT();
					}
					break;
				case '\'':
					return this.QUOTED_STRING(false);
				case '(':
					return this.PARANTHESIS_LEFT();
				case ')':
					return this.PARANTHESIS_RIGHT();
				case ',':
					return this.COMMA();
				case '.':
					return this.DOT();
				}
				this.readNextChar();
				continue;
				IL_0105:
				return this.OPERATOR();
			}
			return new Token(Token.TokenType.EOF, "<EOF>");
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00006FAC File Offset: 0x000051AC
		private void stripComment()
		{
			while (this.m_currentChar != '\n' && this.m_currentChar != '\0')
			{
				this.readNextChar();
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00006FD4 File Offset: 0x000051D4
		private Token COMMENT()
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (this.m_currentChar != '\n' && this.m_currentChar != '\0')
			{
				stringBuilder.Append(this.m_currentChar);
				this.readNextChar();
			}
			return new Token(Token.TokenType.COMMENT, stringBuilder.ToString());
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007024 File Offset: 0x00005224
		private Token COMMA()
		{
			this.readNextChar();
			return new Token(Token.TokenType.COMMA, ",");
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00007038 File Offset: 0x00005238
		private Token DOT()
		{
			this.readNextChar();
			return new Token(Token.TokenType.DOT, ".");
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000704C File Offset: 0x0000524C
		private Token NOT()
		{
			return new Token(Token.TokenType.NOT, "!");
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000705C File Offset: 0x0000525C
		private Token NEW_LINE()
		{
			while (this.m_currentChar == '\n' || this.m_currentChar == Tokenizer.WINDOWS_LINE_ENDING_CRAP)
			{
				this.m_currentLine++;
				this.m_currentPosition = 0;
				this.readNextChar();
			}
			return new Token(Token.TokenType.NEW_LINE, "<NEW_LINE>");
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000070B4 File Offset: 0x000052B4
		private Token OPERATOR()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.m_currentChar);
			char currentChar = this.m_currentChar;
			this.readNextChar();
			if (currentChar == '-' && this.isDIGIT())
			{
				return this.NUMBER(true);
			}
			if ((currentChar == '<' || currentChar == '>') && this.m_currentChar == '=')
			{
				stringBuilder.Append('=');
				this.readNextChar();
			}
			else if (currentChar == '=')
			{
				if (this.m_currentChar != '=')
				{
					return this.ASSIGNMENT();
				}
				stringBuilder.Append('=');
				this.readNextChar();
			}
			else if (currentChar == '!' && this.m_currentChar == '=')
			{
				stringBuilder.Append('=');
				this.readNextChar();
			}
			else
			{
				if (currentChar == '!')
				{
					return this.NOT();
				}
				if (currentChar == '&' && this.m_currentChar == '&')
				{
					stringBuilder.Append('&');
					this.readNextChar();
				}
				else if (currentChar == '|' && this.m_currentChar == '|')
				{
					stringBuilder.Append('|');
					this.readNextChar();
				}
				else if (currentChar == '+' && this.m_currentChar == '+')
				{
					stringBuilder.Append('+');
					this.readNextChar();
				}
				else if (currentChar == '-' && this.m_currentChar == '-')
				{
					stringBuilder.Append('-');
					this.readNextChar();
				}
				else if (currentChar == '+' && this.m_currentChar == '=')
				{
					stringBuilder.Append('=');
					this.readNextChar();
				}
				else if (currentChar == '*' && this.m_currentChar == '=')
				{
					stringBuilder.Append('=');
					this.readNextChar();
				}
				else if (currentChar == '-' && this.m_currentChar == '=')
				{
					stringBuilder.Append('=');
					this.readNextChar();
				}
				else if (currentChar == '/' && this.m_currentChar == '=')
				{
					stringBuilder.Append('=');
					this.readNextChar();
				}
			}
			return new Token(Token.TokenType.OPERATOR, stringBuilder.ToString());
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000072EC File Offset: 0x000054EC
		private Token PARANTHESIS_LEFT()
		{
			this.readNextChar();
			return new Token(Token.TokenType.PARANTHESIS_LEFT, "(");
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00007300 File Offset: 0x00005500
		private Token PARANTHESIS_RIGHT()
		{
			this.readNextChar();
			return new Token(Token.TokenType.PARANTHESIS_RIGHT, ")");
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00007314 File Offset: 0x00005514
		private Token BRACKET_LEFT()
		{
			this.readNextChar();
			return new Token(Token.TokenType.BRACKET_LEFT, "[");
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00007328 File Offset: 0x00005528
		private Token BRACKET_RIGHT()
		{
			this.readNextChar();
			return new Token(Token.TokenType.BRACKET_RIGHT, "]");
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000733C File Offset: 0x0000553C
		private Token QUOTED_STRING(bool pDoubleQuoted)
		{
			char c = ((!pDoubleQuoted) ? '\'' : '"');
			StringBuilder stringBuilder = new StringBuilder();
			this.readNextChar();
			while (this.m_currentChar != c && this.m_currentChar != '\n' && this.m_currentChar != '\0')
			{
				stringBuilder.Append(this.m_currentChar);
				this.readNextChar();
			}
			this.readNextChar();
			return new Token(Token.TokenType.QUOTED_STRING, stringBuilder.ToString());
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000073B4 File Offset: 0x000055B4
		private Token ASSIGNMENT()
		{
			return new Token(Token.TokenType.ASSIGNMENT, "=");
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000073C4 File Offset: 0x000055C4
		private Token NAME()
		{
			StringBuilder stringBuilder = new StringBuilder();
			do
			{
				stringBuilder.Append(this.m_currentChar);
				this.readNextChar();
			}
			while (this.isLETTER() || this.isDIGIT());
			Token.TokenType tokenType = Token.TokenType.NAME;
			string text = stringBuilder.ToString();
			string text2 = text.ToLower();
			if (text2 == "if")
			{
				tokenType = Token.TokenType.IF;
			}
			else if (text2 == "else")
			{
				tokenType = Token.TokenType.ELSE;
			}
			else if (text2 == "return")
			{
				tokenType = Token.TokenType.RETURN;
			}
			else if (text2 == "void")
			{
				tokenType = Token.TokenType.BUILT_IN_TYPE_NAME;
			}
			else if (text2 == "number")
			{
				tokenType = Token.TokenType.BUILT_IN_TYPE_NAME;
			}
			else if (text2 == "string")
			{
				tokenType = Token.TokenType.BUILT_IN_TYPE_NAME;
			}
			else if (text2 == "bool")
			{
				tokenType = Token.TokenType.BUILT_IN_TYPE_NAME;
			}
			else if (text2 == "array")
			{
				tokenType = Token.TokenType.BUILT_IN_TYPE_NAME;
			}
			else if (text2 == "var")
			{
				tokenType = Token.TokenType.BUILT_IN_TYPE_NAME;
			}
			else if (text2 == "loop")
			{
				tokenType = Token.TokenType.LOOP;
			}
			else if (text2 == "in")
			{
				tokenType = Token.TokenType.IN;
			}
			else if (text == "break")
			{
				tokenType = Token.TokenType.BREAK;
			}
			else if (text2 == "from")
			{
				tokenType = Token.TokenType.FROM;
			}
			else if (text2 == "to")
			{
				tokenType = Token.TokenType.TO;
			}
			else if (text2 == "end")
			{
				tokenType = Token.TokenType.BLOCK_END;
			}
			else if (text2 == "and")
			{
				tokenType = Token.TokenType.OPERATOR;
				text = "&&";
			}
			else if (text2 == "or")
			{
				tokenType = Token.TokenType.OPERATOR;
				text = "||";
			}
			else if (text2 == "true" || text2 == "false")
			{
				tokenType = Token.TokenType.BOOLEAN_VALUE;
			}
			return new Token(tokenType, text);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000075DC File Offset: 0x000057DC
		private Token NUMBER(bool negative)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (negative)
			{
				stringBuilder.Append("-");
			}
			bool flag = false;
			for (;;)
			{
				if (this.m_currentChar == '.' && !flag)
				{
					stringBuilder.Append(".");
					flag = true;
					this.readNextChar();
				}
				else if (this.m_currentChar == '.')
				{
					break;
				}
				stringBuilder.Append(this.m_currentChar);
				this.readNextChar();
				if (!this.isDIGIT() && this.m_currentChar != '.')
				{
					goto IL_00A6;
				}
			}
			this.m_errorHandler.errorOccured("Can't have several period signs in a number!", Error.ErrorType.SYNTAX, this.m_currentLine, this.m_currentPosition);
			this.readNextChar();
			IL_00A6:
			return new Token(Token.TokenType.NUMBER, stringBuilder.ToString());
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000769C File Offset: 0x0000589C
		private bool isLETTER()
		{
			foreach (char c in Tokenizer.s_letters)
			{
				if (this.m_currentChar == c)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000076E0 File Offset: 0x000058E0
		private bool isDIGIT()
		{
			foreach (char c in Tokenizer.s_digits)
			{
				if (this.m_currentChar == c)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007724 File Offset: 0x00005924
		private void readNextChar()
		{
			int num = this.m_textReader.Read();
			if (num > 0)
			{
				this.m_currentChar = (char)num;
				this.m_currentPosition++;
			}
			else
			{
				this.m_currentChar = '\0';
				this.m_endOfFile = true;
			}
		}

		// Token: 0x04000071 RID: 113
		private static char WINDOWS_LINE_ENDING_CRAP = '\r';

		// Token: 0x04000072 RID: 114
		private List<Token> m_tokens;

		// Token: 0x04000073 RID: 115
		private TextReader m_textReader;

		// Token: 0x04000074 RID: 116
		private bool m_endOfFile;

		// Token: 0x04000075 RID: 117
		private char m_currentChar;

		// Token: 0x04000076 RID: 118
		private int m_currentLine;

		// Token: 0x04000077 RID: 119
		private int m_currentPosition;

		// Token: 0x04000078 RID: 120
		private int m_currentTokenStartPosition;

		// Token: 0x04000079 RID: 121
		private bool m_stripOutComments;

		// Token: 0x0400007A RID: 122
		private static string s_letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_@!?";

		// Token: 0x0400007B RID: 123
		private static string s_digits = "1234567890";

		// Token: 0x0400007C RID: 124
		private ErrorHandler m_errorHandler;
	}
}
