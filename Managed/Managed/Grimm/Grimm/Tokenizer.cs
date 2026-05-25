using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GameTypes;

namespace GrimmLib
{
	// Token: 0x0200000D RID: 13
	public class Tokenizer
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00003AF8 File Offset: 0x00001CF8
		public List<Token> process(TextReader pTextReader)
		{
			D.isNull(pTextReader);
			this._tokens = new List<Token>();
			this._textReader = pTextReader;
			this._endOfFile = false;
			this.readNextChar();
			this._currentLine = 1;
			this._currentPosition = 0;
			this._currentTokenStartPosition = 0;
			Token token;
			do
			{
				token = this.readNextToken();
				token.LineNr = this._currentLine;
				token.LinePosition = this._currentTokenStartPosition;
				this._currentTokenStartPosition = this._currentPosition;
				this._tokens.Add(token);
			}
			while (token.getTokenType() != Token.TokenType.EOF);
			this._textReader.Close();
			this._textReader.Dispose();
			return this._tokens;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003BA0 File Offset: 0x00001DA0
		private Token readNextToken()
		{
			while (!this._endOfFile)
			{
				char currentChar = this._currentChar;
				switch (currentChar)
				{
				case '(':
					return this.PARANTHESIS_LEFT();
				case ')':
					return this.PARANTHESIS_RIGHT();
				default:
					switch (currentChar)
					{
					case ' ':
						break;
					default:
						switch (currentChar)
						{
						case '[':
							return this.BRACKET_LEFT();
						default:
							switch (currentChar)
							{
							case '{':
								return this.BLOCK_BEGIN();
							default:
								if (currentChar != '\t')
								{
									if (currentChar == '\n')
									{
										return this.NEW_LINE();
									}
									if (currentChar == '\0')
									{
										this._endOfFile = true;
										continue;
									}
									if (currentChar == ':')
									{
										return this.COLON();
									}
									if (this.isLETTER())
									{
										return this.NAME();
									}
									if (this.isDIGIT())
									{
										return this.NUMBER(false);
									}
									throw new Exception(string.Concat(new object[] { "Unrecognized character found: '", this._currentChar, " on line ", this._currentLine, " and position", this._currentPosition }));
								}
								break;
							case '}':
								return this.BLOCK_END();
							}
							break;
						case ']':
							return this.BRACKET_RIGHT();
						}
						break;
					case '"':
						return this.QUOTED_STRING();
					case '#':
						this.stripComment();
						continue;
					}
					this.readNextChar();
					break;
				case ',':
					return this.COMMA();
				case '.':
					return this.DOT();
				}
			}
			return new Token(Token.TokenType.EOF, "<EOF>");
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003D44 File Offset: 0x00001F44
		private void stripComment()
		{
			while (this._currentChar != '\n' && this._currentChar != '\0')
			{
				this.readNextChar();
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003D6C File Offset: 0x00001F6C
		private Token COLON()
		{
			this.readNextChar();
			return new Token(Token.TokenType.COLON, ":");
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003D80 File Offset: 0x00001F80
		private Token DOT()
		{
			this.readNextChar();
			return new Token(Token.TokenType.DOT, ".");
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003D94 File Offset: 0x00001F94
		private Token COMMA()
		{
			this.readNextChar();
			return new Token(Token.TokenType.COMMA, ",");
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003DA8 File Offset: 0x00001FA8
		private Token NEW_LINE()
		{
			while (this._currentChar == '\n')
			{
				this._currentLine++;
				this._currentPosition = 0;
				this.readNextChar();
			}
			return new Token(Token.TokenType.NEW_LINE, "<NEW_LINE>");
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003DF0 File Offset: 0x00001FF0
		private Token PARANTHESIS_LEFT()
		{
			this.readNextChar();
			return new Token(Token.TokenType.PARANTHESIS_LEFT, "(");
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003E04 File Offset: 0x00002004
		private Token PARANTHESIS_RIGHT()
		{
			this.readNextChar();
			return new Token(Token.TokenType.PARANTHESIS_RIGHT, ")");
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003E18 File Offset: 0x00002018
		private Token BRACKET_LEFT()
		{
			this.readNextChar();
			return new Token(Token.TokenType.BRACKET_LEFT, "[");
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003E2C File Offset: 0x0000202C
		private Token BRACKET_RIGHT()
		{
			this.readNextChar();
			return new Token(Token.TokenType.BRACKET_RIGHT, "]");
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003E40 File Offset: 0x00002040
		private Token BLOCK_BEGIN()
		{
			this.readNextChar();
			return new Token(Token.TokenType.BLOCK_BEGIN, "{");
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003E54 File Offset: 0x00002054
		private Token BLOCK_END()
		{
			this.readNextChar();
			return new Token(Token.TokenType.BLOCK_END, "}");
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003E68 File Offset: 0x00002068
		private Token QUOTED_STRING()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.readNextChar();
			while (this._currentChar != '"' && this._currentChar != '\n' && this._currentChar != '\0')
			{
				stringBuilder.Append(this._currentChar);
				this.readNextChar();
			}
			this.readNextChar();
			return new Token(Token.TokenType.QUOTED_STRING, stringBuilder.ToString());
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003ED0 File Offset: 0x000020D0
		private Token NAME()
		{
			StringBuilder stringBuilder = new StringBuilder();
			do
			{
				stringBuilder.Append(this._currentChar);
				this.readNextChar();
			}
			while (this.isLETTER() || this.isDIGIT());
			Token.TokenType tokenType = Token.TokenType.NAME;
			if (stringBuilder.ToString() == "GOTO")
			{
				tokenType = Token.TokenType.GOTO;
			}
			else if (stringBuilder.ToString() == "IF")
			{
				tokenType = Token.TokenType.IF;
			}
			else if (stringBuilder.ToString() == "ELSE")
			{
				tokenType = Token.TokenType.ELSE;
			}
			else if (stringBuilder.ToString() == "ELIF")
			{
				tokenType = Token.TokenType.ELIF;
			}
			else if (stringBuilder.ToString() == "LANGUAGE")
			{
				tokenType = Token.TokenType.LANGUAGE;
			}
			else if (stringBuilder.ToString() == "START")
			{
				tokenType = Token.TokenType.START;
			}
			else if (stringBuilder.ToString() == "WAIT")
			{
				tokenType = Token.TokenType.WAIT;
			}
			else if (stringBuilder.ToString() == "ASSERT")
			{
				tokenType = Token.TokenType.ASSERT;
			}
			else if (stringBuilder.ToString() == "LOOP")
			{
				tokenType = Token.TokenType.LOOP;
			}
			else if (stringBuilder.ToString() == "STOP")
			{
				tokenType = Token.TokenType.STOP;
			}
			else if (stringBuilder.ToString() == "BREAK")
			{
				tokenType = Token.TokenType.BREAK;
			}
			else if (stringBuilder.ToString() == "LISTEN")
			{
				tokenType = Token.TokenType.LISTEN;
			}
			else if (stringBuilder.ToString() == "BROADCAST")
			{
				tokenType = Token.TokenType.BROADCAST;
			}
			else if (stringBuilder.ToString() == "CANCEL")
			{
				tokenType = Token.TokenType.CANCEL;
			}
			else if (stringBuilder.ToString() == "FOCUS")
			{
				tokenType = Token.TokenType.FOCUS;
			}
			else if (stringBuilder.ToString() == "DEFOCUS")
			{
				tokenType = Token.TokenType.DEFOCUS;
			}
			else if (stringBuilder.ToString() == "AND")
			{
				tokenType = Token.TokenType.AND;
			}
			else if (stringBuilder.ToString() == "INTERRUPT")
			{
				tokenType = Token.TokenType.INTERRUPT;
			}
			else if (stringBuilder.ToString() == "CHOICE")
			{
				tokenType = Token.TokenType.CHOICE;
			}
			else if (stringBuilder.ToString() == "ETERNAL")
			{
				tokenType = Token.TokenType.ETERNAL;
			}
			return new Token(tokenType, stringBuilder.ToString());
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000415C File Offset: 0x0000235C
		private Token NUMBER(bool pNegative)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (pNegative)
			{
				stringBuilder.Append("-");
			}
			bool flag = false;
			for (;;)
			{
				if (this._currentChar == '.' && !flag)
				{
					stringBuilder.Append(".");
					this.readNextChar();
				}
				else if (this._currentChar == '.' && flag)
				{
					break;
				}
				stringBuilder.Append(this._currentChar);
				this.readNextChar();
				if (!this.isDIGIT() && this._currentChar != '.')
				{
					goto Block_7;
				}
			}
			throw new Exception("Can't have several period signs in a number!");
			Block_7:
			return new Token(Token.TokenType.NUMBER, stringBuilder.ToString());
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004204 File Offset: 0x00002404
		private bool isLETTER()
		{
			foreach (char c in "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖabcdefghijklmnopqrstuvwxyzåäö_$£@!?")
			{
				if (this._currentChar == c)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004248 File Offset: 0x00002448
		private bool isDIGIT()
		{
			foreach (char c in "-1234567890")
			{
				if (this._currentChar == c)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000428C File Offset: 0x0000248C
		private void readNextChar()
		{
			int num = this._textReader.Read();
			if (num > 0)
			{
				this._currentChar = (char)num;
				this._currentPosition++;
			}
			else
			{
				this._currentChar = '\0';
				this._endOfFile = true;
			}
		}

		// Token: 0x04000043 RID: 67
		private const string s_lettersThatWorksInNames = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖabcdefghijklmnopqrstuvwxyzåäö_$£@!?";

		// Token: 0x04000044 RID: 68
		private const string s_digits = "-1234567890";

		// Token: 0x04000045 RID: 69
		private List<Token> _tokens;

		// Token: 0x04000046 RID: 70
		private TextReader _textReader;

		// Token: 0x04000047 RID: 71
		private bool _endOfFile;

		// Token: 0x04000048 RID: 72
		private char _currentChar;

		// Token: 0x04000049 RID: 73
		private int _currentLine;

		// Token: 0x0400004A RID: 74
		private int _currentPosition;

		// Token: 0x0400004B RID: 75
		private int _currentTokenStartPosition;
	}
}
