using System;
using System.Collections.Generic;
using System.Globalization;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000012 RID: 18
	public class Parser
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00004B0C File Offset: 0x00002D0C
		public Parser(List<Token> tokens, ErrorHandler errorHandler)
		{
			this.m_tokens = tokens;
			this.m_errorHandler = errorHandler;
			this.m_nextTokenIndex = 0;
			this.m_lookahead = new Token[this.k];
			this.m_lookaheadIndex = 0;
			for (int i = 0; i < this.k; i++)
			{
				this.consumeCurrentToken();
			}
			this.m_programAST = new AST(new Token(Token.TokenType.PROGRAM_ROOT, "<PROGRAM_ROOT>"));
			this.m_isInsideFunctionDefinition = false;
			this.m_processed = false;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004BA4 File Offset: 0x00002DA4
		public void process()
		{
			if (this.m_processed)
			{
				throw new InvalidOperationException("Has already processed tokens!");
			}
			if (this.m_tokens.Count == 0)
			{
				throw new InvalidOperationException("No tokens to process!");
			}
			this.program();
			this.m_processed = true;
			if (this.m_isInsideFunctionDefinition)
			{
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004BFC File Offset: 0x00002DFC
		private void program()
		{
			this.m_functionList = new AST(new Token(Token.TokenType.STATEMENT_LIST, "<FUNCTION_LIST>"));
			AST ast = this.statementList(true);
			ast.addChildFirst(new AST(new Token(Token.TokenType.STATEMENT_LIST, "<GLOBAL_VARIABLE_DEFINITIONS_LIST>")));
			this.m_programAST.addChild(ast);
			this.m_programAST.addChild(this.m_functionList);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004C5C File Offset: 0x00002E5C
		private AST statementList(bool isInMainScope)
		{
			AST ast = new AST(new Token(Token.TokenType.STATEMENT_LIST, "<STATEMENT_LIST>"));
			while (this.lookAheadType(1) != Token.TokenType.EOF && this.lookAheadType(1) != Token.TokenType.ELSE)
			{
				if (this.lookAheadType(1) == Token.TokenType.BLOCK_END)
				{
					if (isInMainScope)
					{
						this.m_errorHandler.errorOccured("Found the word 'end' where it makes no sense", Error.ErrorType.SYNTAX, this.lookAhead(1).LineNr, this.lookAhead(1).LinePosition);
					}
					break;
				}
				AST ast2 = this.statement();
				if (ast2 != null)
				{
					ast.addChild(ast2);
				}
			}
			return ast;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004CF8 File Offset: 0x00002EF8
		private AST statement()
		{
			AST ast = null;
			try
			{
				ast = this.figureOutStatementType();
			}
			catch (Error error)
			{
				this.m_errorHandler.errorOccured(error);
				this.skipStuffUntilNextLine();
			}
			return ast;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004D48 File Offset: 0x00002F48
		private AST figureOutStatementType()
		{
			AST ast = null;
			if (this.lookAheadType(1) == Token.TokenType.NAME && this.lookAheadType(2) == Token.TokenType.NAME)
			{
				throw new Error("Can't understand what the word '" + this.lookAhead(1).getTokenString() + "' means here", Error.ErrorType.SYNTAX, this.lookAhead(1).LineNr, this.lookAhead(1).LinePosition);
			}
			if (this.lookAheadType(1) == Token.TokenType.BUILT_IN_TYPE_NAME && this.lookAheadType(2) == Token.TokenType.NAME && this.lookAheadType(3) == Token.TokenType.PARANTHESIS_LEFT)
			{
				this.m_functionList.addChild(this.functionDeclaration());
				this.checkThatItsTheEndOfTheLine();
			}
			else if (this.lookAheadType(1) == Token.TokenType.LOOP)
			{
				ast = this.loop();
			}
			else if (this.lookAheadType(1) == Token.TokenType.BUILT_IN_TYPE_NAME && this.lookAheadType(2) == Token.TokenType.NAME)
			{
				if (this.lookAheadType(3) == Token.TokenType.ASSIGNMENT)
				{
					ast = this.declarationAndAssignment();
					this.checkThatItsTheEndOfTheLine();
				}
				else
				{
					ast = this.declaration();
					this.checkThatItsTheEndOfTheLine();
				}
			}
			else
			{
				if (this.lookAhead(1).getTokenString() == "@" && this.lookAheadType(2) == Token.TokenType.ASSIGNMENT)
				{
					throw new Error("Can't assign to @", Error.ErrorType.SYNTAX, this.lookAhead(1).LineNr, this.lookAhead(1).LinePosition);
				}
				if (this.lookAheadType(1) == Token.TokenType.NAME)
				{
					if (this.lookAheadType(2) == Token.TokenType.ASSIGNMENT)
					{
						ast = this.assignment();
						this.checkThatItsTheEndOfTheLine();
					}
					else if (this.lookAheadType(2) == Token.TokenType.OPERATOR && (this.lookAhead(2).getTokenString() == "++" || this.lookAhead(2).getTokenString() == "--"))
					{
						ast = this.plusplusOrMinusminus();
					}
					else if (this.lookAheadType(2) == Token.TokenType.BRACKET_LEFT)
					{
						ast = this.assignmentToArray();
					}
					else if (this.lookAhead(2).getTokenString() == "+=" || this.lookAhead(2).getTokenString() == "*=" || this.lookAhead(2).getTokenString() == "-=" || this.lookAhead(2).getTokenString() == "/=")
					{
						ast = this.assignmentAndOperator();
					}
					else
					{
						ast = this.expression();
						this.checkThatItsTheEndOfTheLine();
					}
				}
				else if (this.lookAheadType(1) == Token.TokenType.NUMBER || this.lookAheadType(1) == Token.TokenType.PARANTHESIS_LEFT)
				{
					ast = this.expression();
					this.checkThatItsTheEndOfTheLine();
				}
				else if (this.lookAheadType(1) == Token.TokenType.IF)
				{
					ast = this.ifThenElse();
				}
				else if (this.lookAheadType(1) == Token.TokenType.RETURN)
				{
					ast = this.returnFromFunction();
				}
				else if (this.lookAheadType(1) == Token.TokenType.BREAK)
				{
					ast = this.breakStatement();
				}
				else
				{
					if (this.lookAheadType(1) != Token.TokenType.NEW_LINE)
					{
						throw new Error("Can't understand what the word '" + this.lookAhead(1).getTokenString() + "' means here", Error.ErrorType.SYNTAX, this.lookAhead(1).LineNr, this.lookAhead(1).LinePosition);
					}
					this.match(Token.TokenType.NEW_LINE);
				}
			}
			return ast;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00005094 File Offset: 0x00003294
		private void checkThatItsTheEndOfTheLine()
		{
			if (this.lookAheadType(1) == Token.TokenType.EOF)
			{
				this.match(Token.TokenType.EOF);
			}
			else
			{
				if (this.lookAheadType(1) != Token.TokenType.NEW_LINE)
				{
					throw new Error("Can't understand the words at the end of this line", Error.ErrorType.SYNTAX, this.lookAhead(1).LineNr, this.lookAhead(1).LinePosition);
				}
				this.match(Token.TokenType.NEW_LINE);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000050FC File Offset: 0x000032FC
		private void skipStuffUntilNextLine()
		{
			while (this.lookAheadType(1) != Token.TokenType.NEW_LINE && this.lookAheadType(1) != Token.TokenType.EOF)
			{
				this.consumeCurrentToken();
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005124 File Offset: 0x00003324
		private AST expression()
		{
			return this.booleanExpression();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000513C File Offset: 0x0000333C
		private AST booleanExpression()
		{
			AST ast = this.comparisonExpression();
			if (this.lookAheadType(1) == Token.TokenType.NOT)
			{
				return this.notStatement();
			}
			if (this.lookAhead(1).getTokenString() == "&&" || this.lookAhead(1).getTokenString() == "||")
			{
				Token token = this.match(Token.TokenType.OPERATOR);
				AST ast2 = this.booleanExpression();
				this.checkLeftHandSide(ast, token);
				this.checkRightHandSide(ast2, token);
				AST ast3 = new AST(token);
				ast3.addChild(ast);
				ast3.addChild(ast2);
				return ast3;
			}
			return ast;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000051D8 File Offset: 0x000033D8
		private AST comparisonExpression()
		{
			AST ast = this.plusOrMinusExpression();
			if (this.lookAhead(1).getTokenString() == "<" || this.lookAhead(1).getTokenString() == ">" || this.lookAhead(1).getTokenString() == "<=" || this.lookAhead(1).getTokenString() == ">=" || this.lookAhead(1).getTokenString() == "!=" || this.lookAhead(1).getTokenString() == "==")
			{
				Token token = this.match(Token.TokenType.OPERATOR);
				AST ast2 = this.plusOrMinusExpression();
				this.checkLeftHandSide(ast, token);
				this.checkRightHandSide(ast2, token);
				AST ast3 = new AST(token);
				ast3.addChild(ast);
				ast3.addChild(ast2);
				return ast3;
			}
			return ast;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000052C8 File Offset: 0x000034C8
		private AST plusOrMinusExpression()
		{
			AST ast = this.multiplicationExpression();
			if (this.lookAhead(1).getTokenString() == "+" || this.lookAhead(1).getTokenString() == "-")
			{
				Token token = this.match(Token.TokenType.OPERATOR);
				AST ast2 = this.plusOrMinusExpression();
				this.checkLeftHandSide(ast, token);
				this.checkRightHandSide(ast2, token);
				AST ast3 = new AST(token);
				ast3.addChild(ast);
				ast3.addChild(ast2);
				return ast3;
			}
			return ast;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000534C File Offset: 0x0000354C
		private AST multiplicationExpression()
		{
			AST ast = this.dotNotationExpression();
			if (this.lookAhead(1).getTokenString() == "*" || this.lookAhead(1).getTokenString() == "/")
			{
				Token token = this.match(Token.TokenType.OPERATOR);
				AST ast2 = this.multiplicationExpression();
				this.checkLeftHandSide(ast, token);
				this.checkRightHandSide(ast2, token);
				AST ast3 = new AST(token);
				ast3.addChild(ast);
				ast3.addChild(ast2);
				return ast3;
			}
			return ast;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000053D0 File Offset: 0x000035D0
		private AST dotNotationExpression()
		{
			AST ast = this.parenthesisExpression();
			if (this.lookAhead(1).getTokenType() == Token.TokenType.DOT)
			{
				this.match(Token.TokenType.DOT);
				Token token = this.match(Token.TokenType.NAME);
				AST ast2 = new AST_FunctionCall(new Token(Token.TokenType.FUNCTION_CALL, "RemoteFunctionCall", token.LineNr, token.LinePosition));
				AST ast3 = this.FunctionArgumentList();
				AST ast4 = new AST(new Token(Token.TokenType.NODE_GROUP, "<ARGUMENT_LIST>"));
				ast4.addChild(ast);
				Token token2 = new TokenWithValue(Token.TokenType.QUOTED_STRING, token.getTokenString(), token.LineNr, token.LinePosition, token.getTokenString());
				ast4.addChild(new AST(token2));
				AST_ArrayEndSignal ast_ArrayEndSignal = new AST_ArrayEndSignal(new Token(Token.TokenType.ARRAY_END_SIGNAL, "<ARRAY>"));
				foreach (AST ast5 in ast3.getChildren())
				{
					ast_ArrayEndSignal.addChild(ast5);
				}
				ast_ArrayEndSignal.ArraySize = ast3.getChildren().Count;
				ast4.addChild(ast_ArrayEndSignal);
				ast2.addChild(ast4);
				return ast2;
			}
			return ast;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005510 File Offset: 0x00003710
		private AST parenthesisExpression()
		{
			if (this.lookAheadType(1) != Token.TokenType.PARANTHESIS_LEFT)
			{
				return this.operand();
			}
			this.match(Token.TokenType.PARANTHESIS_LEFT);
			AST ast = this.expression();
			this.match(Token.TokenType.PARANTHESIS_RIGHT);
			if (this.lookAheadType(1) == Token.TokenType.OPERATOR)
			{
				Token token = this.match(Token.TokenType.OPERATOR);
				AST ast2 = this.expression();
				AST ast3 = new AST(token);
				ast3.addChild(ast);
				ast3.addChild(ast2);
				return ast3;
			}
			return ast;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005584 File Offset: 0x00003784
		private void checkLeftHandSide(AST lhs, Token operatorToken)
		{
			if (lhs == null)
			{
				throw new Error("No expression on the left side of '" + operatorToken.getTokenString() + "'", Error.ErrorType.SYNTAX, operatorToken.LineNr, operatorToken.LinePosition - 1);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000055C4 File Offset: 0x000037C4
		private void checkRightHandSide(AST rhs, Token operatorToken)
		{
			if (rhs == null)
			{
				throw new Error("No expression on the right side of '" + operatorToken.getTokenString() + "'", Error.ErrorType.SYNTAX, operatorToken.LineNr, operatorToken.LinePosition + 2);
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005604 File Offset: 0x00003804
		private AST operand()
		{
			AST ast = null;
			if (this.lookAheadType(1) == Token.TokenType.NAME && this.lookAheadType(2) == Token.TokenType.PARANTHESIS_LEFT)
			{
				ast = this.functionCall();
			}
			else if (this.lookAheadType(1) == Token.TokenType.NAME && this.lookAheadType(2) == Token.TokenType.BRACKET_LEFT)
			{
				ast = this.arrayLookup();
			}
			else if (this.lookAheadType(1) == Token.TokenType.FROM)
			{
				ast = this.fromMinToMaxArrayCreation();
			}
			else if (this.lookAheadType(1) == Token.TokenType.NAME)
			{
				Token token = this.match(Token.TokenType.NAME);
				ast = new AST(token);
			}
			else if (this.lookAheadType(1) == Token.TokenType.NUMBER)
			{
				Token token2 = this.match(Token.TokenType.NUMBER);
				float num = (float)Convert.ToDouble(token2.getTokenString(), CultureInfo.InvariantCulture);
				ast = new AST(new TokenWithValue(token2.getTokenType(), token2.getTokenString(), num));
			}
			else if (this.lookAheadType(1) == Token.TokenType.QUOTED_STRING)
			{
				Token token3 = this.match(Token.TokenType.QUOTED_STRING);
				ast = new AST(new TokenWithValue(token3.getTokenType(), token3.getTokenString(), token3.getTokenString()));
			}
			else if (this.lookAheadType(1) == Token.TokenType.BOOLEAN_VALUE)
			{
				Token token4 = this.match(Token.TokenType.BOOLEAN_VALUE);
				bool flag = token4.getTokenString().ToLower() == "true";
				ast = new AST(new TokenWithValue(token4.getTokenType(), token4.getTokenString(), flag));
			}
			else if (this.lookAheadType(1) == Token.TokenType.OPERATOR && this.lookAhead(1).getTokenString() == "-")
			{
				ast = this.negativeExpression();
			}
			else if (this.lookAheadType(1) == Token.TokenType.OPERATOR && this.lookAhead(1).getTokenString() == "+")
			{
				this.match(Token.TokenType.OPERATOR);
				ast = this.operand();
			}
			else if (this.lookAheadType(1) == Token.TokenType.BRACKET_LEFT)
			{
				ast = this.arrayCreation();
			}
			return ast;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005808 File Offset: 0x00003A08
		private AST arrayCreation()
		{
			this.match(Token.TokenType.BRACKET_LEFT);
			AST_ArrayEndSignal ast_ArrayEndSignal = new AST_ArrayEndSignal(new Token(Token.TokenType.ARRAY_END_SIGNAL, "<ARRAY>"));
			int num = 0;
			if (this.lookAheadType(1) != Token.TokenType.BRACKET_RIGHT)
			{
				for (;;)
				{
					AST ast = this.expression();
					if (ast == null)
					{
						break;
					}
					ast_ArrayEndSignal.addChild(ast);
					num++;
					if (this.lookAheadType(1) == Token.TokenType.BRACKET_RIGHT)
					{
						goto Block_2;
					}
					if (this.lookAheadType(1) == Token.TokenType.COMMA)
					{
						this.match(Token.TokenType.COMMA);
					}
				}
				Token token = this.lookAhead(1);
				throw new Error("Problem with an expression in the array", Error.ErrorType.SYNTAX, token.LineNr, token.LinePosition);
				Block_2:
				ast_ArrayEndSignal.ArraySize = num;
			}
			else
			{
				ast_ArrayEndSignal.ArraySize = 0;
			}
			this.match(Token.TokenType.BRACKET_RIGHT);
			return ast_ArrayEndSignal;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000058C8 File Offset: 0x00003AC8
		private AST arrayLookup()
		{
			AST ast = new AST(this.match(Token.TokenType.NAME));
			AST ast2 = new AST(new Token(Token.TokenType.ARRAY_LOOKUP, ast.getTokenString()));
			this.match(Token.TokenType.BRACKET_LEFT);
			AST ast3 = this.expression();
			this.match(Token.TokenType.BRACKET_RIGHT);
			ast2.addChild(ast3);
			return ast2;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005918 File Offset: 0x00003B18
		private AST plusplusOrMinusminus()
		{
			Token token = this.match(Token.TokenType.NAME);
			Token token2 = this.match(Token.TokenType.OPERATOR);
			AST ast;
			if (token2.getTokenString() == "++")
			{
				ast = new AST(new Token(Token.TokenType.OPERATOR, "+"));
			}
			else
			{
				if (!(token2.getTokenString() == "--"))
				{
					throw new Error("Invalid operator token");
				}
				ast = new AST(new Token(Token.TokenType.OPERATOR, "-"));
			}
			ast.addChild(new AST(token));
			ast.addChild(new AST(new TokenWithValue(Token.TokenType.NUMBER, "1", 1f)));
			AST ast2 = new AST_Assignment(new Token(Token.TokenType.ASSIGNMENT, "="), token.getTokenString());
			ast2.addChild(ast);
			return ast2;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000059E4 File Offset: 0x00003BE4
		private AST negativeExpression()
		{
			this.match(Token.TokenType.OPERATOR);
			AST ast = new AST(new Token(Token.TokenType.OPERATOR, "*"));
			AST ast2 = new AST(new TokenWithValue(Token.TokenType.NUMBER, "-1", this.lookAhead(1).LineNr, this.lookAhead(1).LinePosition, -1f));
			AST ast3 = this.parenthesisExpression();
			ast.addChild(ast2);
			ast.addChild(ast3);
			return ast;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005A54 File Offset: 0x00003C54
		private AST quotedString()
		{
			Token token = this.match(Token.TokenType.QUOTED_STRING);
			return new AST(token);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005A70 File Offset: 0x00003C70
		private AST functionCall()
		{
			Token token = this.match(Token.TokenType.NAME);
			AST ast = new AST_FunctionCall(new Token(Token.TokenType.FUNCTION_CALL, token.getTokenString(), token.LineNr, token.LinePosition));
			AST ast2 = this.FunctionArgumentList();
			ast.getToken().LineNr = token.LineNr;
			ast.getToken().LinePosition = token.LinePosition;
			ast.addChild(ast2);
			return ast;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005AD8 File Offset: 0x00003CD8
		private AST FunctionArgumentList()
		{
			this.match(Token.TokenType.PARANTHESIS_LEFT);
			AST ast = new AST(new Token(Token.TokenType.NODE_GROUP, "<ARGUMENT_LIST>"));
			if (this.lookAheadType(1) != Token.TokenType.PARANTHESIS_RIGHT)
			{
				for (;;)
				{
					AST ast2 = this.expression();
					if (ast2 == null)
					{
						break;
					}
					ast.addChild(ast2);
					if (this.lookAheadType(1) == Token.TokenType.COMMA)
					{
						this.match(Token.TokenType.COMMA);
					}
					else
					{
						if (this.lookAheadType(1) == Token.TokenType.NEW_LINE || this.lookAheadType(1) == Token.TokenType.EOF)
						{
							goto IL_009A;
						}
						if (this.lookAheadType(1) != Token.TokenType.NAME && this.lookAheadType(1) != Token.TokenType.QUOTED_STRING && this.lookAheadType(1) != Token.TokenType.NUMBER)
						{
							goto IL_00EA;
						}
					}
				}
				throw new Error("Something is wrong with the argument list", Error.ErrorType.SYNTAX, this.lookAhead(1).LineNr, this.lookAhead(1).LinePosition);
				IL_009A:
				throw new Error("Ending parenthesis is missing in function call", Error.ErrorType.SYNTAX, this.lookAhead(1).LineNr, this.lookAhead(1).LinePosition);
				IL_00EA:;
			}
			this.match(Token.TokenType.PARANTHESIS_RIGHT);
			return ast;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005BE4 File Offset: 0x00003DE4
		private AST ifThenElse()
		{
			AST ast = null;
			AST ast2;
			AST ast3;
			try
			{
				Token token = this.match(Token.TokenType.IF);
				ast2 = this.expression();
				if (ast2 == null)
				{
					throw new Error("The if statement is missing an expression after the 'if'", Error.ErrorType.SYNTAX, token.LineNr, token.LinePosition);
				}
				if (this.lookAheadType(1) != Token.TokenType.NEW_LINE)
				{
					throw new Error("Found assignment (=) in if statement. Use == instead?", Error.ErrorType.SYNTAX, token.LineNr, token.LinePosition);
				}
				this.match(Token.TokenType.NEW_LINE);
				ast3 = this.statementList(false);
				if (this.lookAheadType(1) == Token.TokenType.ELSE && this.lookAheadType(2) == Token.TokenType.IF)
				{
					this.match(Token.TokenType.ELSE);
					AST ast4 = this.statement();
					ast = new AST(new Token(Token.TokenType.STATEMENT_LIST, "<STATEMENT_LIST>"));
					if (ast4 != null)
					{
						ast.addChild(ast4);
					}
				}
				else if (this.lookAheadType(1) == Token.TokenType.ELSE)
				{
					this.match(Token.TokenType.ELSE);
					if (this.lookAhead(1).getTokenType() != Token.TokenType.NEW_LINE)
					{
						throw new Error("The else statement is missing a line break after it", Error.ErrorType.SYNTAX, token.LineNr, token.LinePosition);
					}
					this.match(Token.TokenType.NEW_LINE);
					ast = this.statementList(false);
					if (this.lookAhead(1).getTokenType() != Token.TokenType.BLOCK_END)
					{
						throw new Error("The if statement is missing a following 'end'", Error.ErrorType.SYNTAX, token.LineNr, token.LinePosition);
					}
					this.match(Token.TokenType.BLOCK_END);
				}
				else
				{
					if (this.lookAhead(1).getTokenType() != Token.TokenType.BLOCK_END)
					{
						throw new Error("The if statement is missing a following 'end'", Error.ErrorType.SYNTAX, token.LineNr, token.LinePosition);
					}
					this.match(Token.TokenType.BLOCK_END);
				}
			}
			catch (Error error)
			{
				throw error;
			}
			AST ast5 = new AST_IfNode(new Token(Token.TokenType.IF, "if", this.lookAhead(1).LineNr, this.lookAhead(1).LinePosition));
			ast5.addChild(ast2);
			ast5.addChild(ast3);
			if (ast != null)
			{
				ast5.addChild(ast);
			}
			return ast5;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005DF8 File Offset: 0x00003FF8
		private void allowLineBreak()
		{
			if (this.lookAheadType(1) == Token.TokenType.NEW_LINE)
			{
				this.match(Token.TokenType.NEW_LINE);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005E10 File Offset: 0x00004010
		private AST returnFromFunction()
		{
			AST ast = new AST(this.match(Token.TokenType.RETURN));
			AST ast2 = this.expression();
			if (ast2 != null)
			{
				ast.addChild(ast2);
			}
			return ast;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005E40 File Offset: 0x00004040
		private AST_VariableDeclaration declaration()
		{
			Token token = this.match(Token.TokenType.BUILT_IN_TYPE_NAME);
			Token token2 = this.match(Token.TokenType.NAME);
			return new AST_VariableDeclaration(new Token(Token.TokenType.VAR_DECLARATION, "<VAR_DECL>", this.lookAhead(1).LineNr, this.lookAhead(1).LinePosition), ExternalFunctionCreator.GetReturnTypeFromString(token.getTokenString()), token2.getTokenString());
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005E9C File Offset: 0x0000409C
		private AST assignment()
		{
			Token token = this.match(Token.TokenType.NAME);
			Token token2 = this.match(Token.TokenType.ASSIGNMENT);
			AST ast = this.expression();
			if (ast != null)
			{
				AST_Assignment ast_Assignment = new AST_Assignment(token2, token.getTokenString());
				ast_Assignment.addChild(ast);
				return ast_Assignment;
			}
			throw new Error("The expression after = makes no sense", Error.ErrorType.SYNTAX, token2.LineNr, token2.LinePosition);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005EF4 File Offset: 0x000040F4
		private AST assignmentAndOperator()
		{
			Token token = this.match(Token.TokenType.NAME);
			Token token2 = this.match(Token.TokenType.OPERATOR);
			AST ast = this.expression();
			if (ast != null)
			{
				AST_Assignment ast_Assignment = new AST_Assignment(new Token(Token.TokenType.ASSIGNMENT, "="), token.getTokenString());
				AST ast2;
				if (token2.getTokenString() == "+=")
				{
					ast2 = new AST(new Token(Token.TokenType.OPERATOR, "+"));
				}
				else if (token2.getTokenString() == "*=")
				{
					ast2 = new AST(new Token(Token.TokenType.OPERATOR, "*"));
				}
				else if (token2.getTokenString() == "-=")
				{
					ast2 = new AST(new Token(Token.TokenType.OPERATOR, "-"));
				}
				else
				{
					if (!(token2.getTokenString() == "/="))
					{
						throw new Error("Can't handle the operator '" + token2.getTokenString() + "'", Error.ErrorType.SYNTAX, ast_Assignment.getToken().LineNr, ast_Assignment.getToken().LinePosition);
					}
					ast2 = new AST(new Token(Token.TokenType.OPERATOR, "/"));
				}
				ast2.addChild(token);
				ast2.addChild(ast);
				ast_Assignment.addChild(ast2);
				return ast_Assignment;
			}
			throw new Error("The expression after " + token2.getTokenString() + " makes no sense", Error.ErrorType.SYNTAX, token2.LineNr, token2.LinePosition);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000605C File Offset: 0x0000425C
		private AST assignmentToArray()
		{
			Token token = this.match(Token.TokenType.NAME);
			this.match(Token.TokenType.BRACKET_LEFT);
			AST ast = this.expression();
			this.match(Token.TokenType.BRACKET_RIGHT);
			if (this.lookAheadType(1) == Token.TokenType.NEW_LINE || this.lookAheadType(1) == Token.TokenType.EOF)
			{
				return ast;
			}
			Token token2 = this.match(Token.TokenType.ASSIGNMENT);
			AST ast2 = this.expression();
			if (ast2 != null)
			{
				Token token3 = new Token(Token.TokenType.ASSIGNMENT_TO_ARRAY, "=", token2.LineNr, token2.LinePosition);
				AST_Assignment ast_Assignment = new AST_Assignment(token3, token.getTokenString());
				ast_Assignment.addChild(ast);
				ast_Assignment.addChild(ast2);
				return ast_Assignment;
			}
			throw new Error("The expression after = makes no sense", Error.ErrorType.SYNTAX, token2.LineNr, token2.LinePosition);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006114 File Offset: 0x00004314
		private AST declarationAndAssignment()
		{
			AST_VariableDeclaration ast_VariableDeclaration = this.declaration();
			Token token = this.match(Token.TokenType.ASSIGNMENT);
			AST ast = this.expression();
			if (ast != null)
			{
				AST_Assignment ast_Assignment = new AST_Assignment(token, ast_VariableDeclaration.Name);
				ast_Assignment.addChild(ast);
				AST ast2 = new AST(new Token(Token.TokenType.STATEMENT_LIST, "<DECLARATION_AND_ASSIGNMENT>", ast_VariableDeclaration.getToken().LineNr, ast_VariableDeclaration.getToken().LinePosition));
				ast2.addChild(ast_VariableDeclaration);
				ast2.addChild(ast_Assignment);
				return ast2;
			}
			throw new Error("The expression after = makes no sense", Error.ErrorType.SYNTAX, this.lookAhead(1).LineNr, this.lookAhead(1).LinePosition);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000061B4 File Offset: 0x000043B4
		private AST functionDeclaration()
		{
			if (this.m_isInsideFunctionDefinition)
			{
				throw new Error("Trying to define a function inside a function (are you missing the END word?)", Error.ErrorType.SYNTAX, this.lookAhead(1).LineNr, this.lookAhead(1).LinePosition);
			}
			this.m_isInsideFunctionDefinition = true;
			AST_FunctionDefinitionNode ast_FunctionDefinitionNode = new AST_FunctionDefinitionNode(new Token(Token.TokenType.FUNC_DECLARATION, "<FUNC_DECL>", this.lookAhead(1).LineNr, this.lookAhead(1).LinePosition));
			ast_FunctionDefinitionNode.addChild(this.match(Token.TokenType.BUILT_IN_TYPE_NAME));
			ast_FunctionDefinitionNode.addChild(this.match(Token.TokenType.NAME));
			this.match(Token.TokenType.PARANTHESIS_LEFT);
			ast_FunctionDefinitionNode.addChild(this.parameterList());
			this.match(Token.TokenType.PARANTHESIS_RIGHT);
			this.allowLineBreak();
			ast_FunctionDefinitionNode.addChild(this.statementList(false));
			this.match(Token.TokenType.BLOCK_END);
			this.m_isInsideFunctionDefinition = false;
			return ast_FunctionDefinitionNode;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00006280 File Offset: 0x00004480
		private AST parameterList()
		{
			AST ast = new AST(new Token(Token.TokenType.NODE_GROUP, "<PARAMETER_LIST>", this.lookAhead(1).LineNr, this.lookAhead(1).LinePosition));
			if (this.lookAheadType(1) != Token.TokenType.PARANTHESIS_RIGHT)
			{
				for (;;)
				{
					AST ast2 = this.parameter();
					ast.addChild(ast2);
					if (this.lookAheadType(1) != Token.TokenType.COMMA)
					{
						break;
					}
					this.match(Token.TokenType.COMMA);
				}
			}
			return ast;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000062F8 File Offset: 0x000044F8
		private AST parameter()
		{
			AST ast = new AST(new Token(Token.TokenType.PARAMETER, "<PARAMETER>", this.lookAhead(1).LineNr, this.lookAhead(1).LinePosition));
			AST ast2;
			if (this.lookAheadType(1) == Token.TokenType.BUILT_IN_TYPE_NAME)
			{
				ast2 = new AST(this.match(Token.TokenType.BUILT_IN_TYPE_NAME));
			}
			else
			{
				ast2 = new AST(new Token(Token.TokenType.BUILT_IN_TYPE_NAME, "var"));
			}
			AST ast3 = new AST(this.match(Token.TokenType.NAME));
			AST ast4 = new AST_VariableDeclaration(new Token(Token.TokenType.VAR_DECLARATION, "<PARAMETER_DECLARATION>"), ExternalFunctionCreator.GetReturnTypeFromString(ast2.getTokenString()), ast3.getTokenString());
			AST ast5 = new AST_Assignment(new Token(Token.TokenType.ASSIGNMENT, "="), ast3.getTokenString());
			ast.addChild(ast4);
			ast.addChild(ast5);
			return ast;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000063C0 File Offset: 0x000045C0
		private AST fromMinToMaxArrayCreation()
		{
			AST ast4;
			try
			{
				Token token = this.match(Token.TokenType.FROM);
				AST ast = this.expression();
				if (ast == null)
				{
					throw new Error("Missing expression after 'from'", Error.ErrorType.SYNTAX, token.LineNr, token.LinePosition + 5);
				}
				this.match(Token.TokenType.TO);
				AST ast2 = this.expression();
				if (ast2 == null)
				{
					throw new Error("Missing expression after 'to'", Error.ErrorType.SYNTAX, token.LineNr, token.LinePosition + 3);
				}
				AST_FunctionCall ast_FunctionCall = new AST_FunctionCall(new Token(Token.TokenType.FUNCTION_CALL, "Range"));
				AST ast3 = new AST(new Token(Token.TokenType.NODE_GROUP, "<ARGUMENT_LIST>"));
				ast3.addChild(ast);
				ast3.addChild(ast2);
				ast_FunctionCall.addChild(ast3);
				ast4 = ast_FunctionCall;
			}
			catch (Error error)
			{
				this.m_errorHandler.errorOccured(error);
				ast4 = null;
			}
			return ast4;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000064B4 File Offset: 0x000046B4
		private AST loop()
		{
			AST ast = new AST(new Token(Token.TokenType.STATEMENT_LIST, "<LOOP_BLOCK_STATEMENTS>"));
			AST_LoopNode ast_LoopNode = new AST_LoopNode(this.match(Token.TokenType.LOOP));
			string text = "@";
			bool flag = false;
			if (this.lookAheadType(1) != Token.TokenType.NEW_LINE)
			{
				flag = true;
				AST_VariableDeclaration ast_VariableDeclaration = new AST_VariableDeclaration(new Token(Token.TokenType.VAR_DECLARATION, "<VAR_DECL>"), ReturnValueType.NUMBER, "__index__");
				ast.addChild(ast_VariableDeclaration);
				AST_Assignment ast_Assignment = new AST_Assignment(new Token(Token.TokenType.ASSIGNMENT, "="), "__index__");
				ast_Assignment.addChild(new AST(new TokenWithValue(Token.TokenType.NUMBER, "-1", -1f)));
				ast.addChild(ast_Assignment);
				Token token = this.lookAhead(1);
				AST ast2 = null;
				if ((this.lookAheadType(1) == Token.TokenType.NAME && this.lookAheadType(2) == Token.TokenType.IN) || (this.lookAheadType(1) == Token.TokenType.NAME && this.lookAheadType(2) == Token.TokenType.FROM))
				{
					Token token2 = this.match(Token.TokenType.NAME);
					if (this.lookAheadType(1) == Token.TokenType.IN)
					{
						this.match(Token.TokenType.IN);
					}
					else if (this.lookAheadType(1) == Token.TokenType.FROM)
					{
					}
					try
					{
						ast2 = this.expression();
						if (ast2 == null)
						{
							this.backtrackToToken(token);
						}
						else
						{
							text = token2.getTokenString();
						}
					}
					catch (Error error)
					{
						this.backtrackToToken(token);
					}
				}
				if (ast2 == null)
				{
					ast2 = this.expression();
				}
				if (ast2 == null)
				{
					throw new Error("Failed to match the expression after 'loop'", Error.ErrorType.SYNTAX, ast_LoopNode.getToken().LineNr, ast_LoopNode.getToken().LinePosition);
				}
				AST_VariableDeclaration ast_VariableDeclaration2 = new AST_VariableDeclaration(new Token(Token.TokenType.VAR_DECLARATION, "<VAR_DECL>"), ReturnValueType.UNKNOWN_TYPE, "__array__");
				ast.addChild(ast_VariableDeclaration2);
				AST_Assignment ast_Assignment2 = new AST_Assignment(new Token(Token.TokenType.ASSIGNMENT, "="), "__array__");
				if (ast2 == null)
				{
					throw new Error("Can't understand array expression in loop", Error.ErrorType.SYNTAX, ast_Assignment2.getToken().LineNr, ast_Assignment2.getToken().LinePosition);
				}
				ast_Assignment2.addChild(ast2);
				ast.addChild(ast_Assignment2);
				AST_VariableDeclaration ast_VariableDeclaration3 = new AST_VariableDeclaration(new Token(Token.TokenType.VAR_DECLARATION, "<VAR_DECL>"), ReturnValueType.UNKNOWN_TYPE, "__indexes__");
				ast.addChild(ast_VariableDeclaration3);
				AST_FunctionCall ast_FunctionCall = new AST_FunctionCall(new Token(Token.TokenType.FUNCTION_CALL, "GetIndexes"));
				AST ast3 = new AST(new Token(Token.TokenType.NODE_GROUP, "<ARGUMENT_LIST>"));
				ast3.addChild(new Token(Token.TokenType.NAME, "__array__"));
				ast_FunctionCall.addChild(ast3);
				AST_Assignment ast_Assignment3 = new AST_Assignment(new Token(Token.TokenType.ASSIGNMENT, "="), "__indexes__");
				ast_Assignment3.addChild(ast_FunctionCall);
				ast.addChild(ast_Assignment3);
			}
			this.allowLineBreak();
			AST ast4 = this.statementList(false);
			ast4.addChild(new AST(new Token(Token.TokenType.GOTO_BEGINNING_OF_LOOP, "<GOTO_BEGINNING_OF_LOOP>")));
			this.allowLineBreak();
			this.match(Token.TokenType.BLOCK_END);
			if (flag)
			{
				ast4.addChildFirst(this.foreachStuff(text));
			}
			ast_LoopNode.addChild(ast4);
			ast.addChild(ast_LoopNode);
			AST_LoopBlockNode ast_LoopBlockNode = new AST_LoopBlockNode(new Token(Token.TokenType.LOOP_BLOCK, "<LOOP_BLOCK>"));
			ast_LoopBlockNode.addChild(ast);
			return ast_LoopBlockNode;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000067E0 File Offset: 0x000049E0
		private AST foreachStuff(string pLoopVariableName)
		{
			AST ast = new AST(new Token(Token.TokenType.STATEMENT_LIST, "<FOREACH_STATEMENTS>"));
			AST ast2 = new AST(new Token(Token.TokenType.OPERATOR, "+"));
			ast2.addChild(new AST(new Token(Token.TokenType.NAME, "__index__")));
			ast2.addChild(new AST(new TokenWithValue(Token.TokenType.NUMBER, "1", 1f)));
			AST_Assignment ast_Assignment = new AST_Assignment(new Token(Token.TokenType.ASSIGNMENT, "="), "__index__");
			ast_Assignment.addChild(ast2);
			ast.addChild(ast_Assignment);
			AST_FunctionCall ast_FunctionCall = new AST_FunctionCall(new Token(Token.TokenType.FUNCTION_CALL, "Count"));
			AST ast3 = new AST(new Token(Token.TokenType.NODE_GROUP, "<ARGUMENT_LIST>"));
			ast3.addChild(new Token(Token.TokenType.NAME, "__indexes__"));
			ast_FunctionCall.addChild(ast3);
			AST ast4 = new AST_IfNode(new Token(Token.TokenType.IF, "IF"));
			AST ast5 = new AST(new Token(Token.TokenType.OPERATOR, ">="));
			ast5.addChild(new Token(Token.TokenType.NAME, "__index__"));
			ast5.addChild(ast_FunctionCall);
			ast4.addChild(ast5);
			ast4.addChild(new Token(Token.TokenType.BREAK, "break"));
			ast.addChild(ast4);
			AST_VariableDeclaration ast_VariableDeclaration = new AST_VariableDeclaration(new Token(Token.TokenType.VAR_DECLARATION, "<VAR_DECL>"), ReturnValueType.UNKNOWN_TYPE, pLoopVariableName);
			ast.addChild(ast_VariableDeclaration);
			AST ast6 = new AST(new Token(Token.TokenType.ARRAY_LOOKUP, "__indexes__"));
			ast6.addChild(new AST(new Token(Token.TokenType.NAME, "__index__")));
			AST ast7 = new AST(new Token(Token.TokenType.ARRAY_LOOKUP, "__array__"));
			ast7.addChild(ast6);
			AST_Assignment ast_Assignment2 = new AST_Assignment(new Token(Token.TokenType.ASSIGNMENT, "="), pLoopVariableName);
			ast_Assignment2.addChild(ast7);
			ast.addChild(ast_Assignment2);
			return ast;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006994 File Offset: 0x00004B94
		private AST breakStatement()
		{
			return new AST(this.match(Token.TokenType.BREAK));
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000069A4 File Offset: 0x00004BA4
		private AST notStatement()
		{
			AST ast = new AST(this.match(Token.TokenType.NOT));
			AST ast2 = this.expression();
			ast.addChild(ast2);
			return ast;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000069D0 File Offset: 0x00004BD0
		public virtual Token match(Token.TokenType expectedTokenType)
		{
			Token token = this.lookAhead(1);
			if (this.lookAheadType(1) == expectedTokenType)
			{
				this.consumeCurrentToken();
				return token;
			}
			throw new Error(string.Concat(new object[]
			{
				"The code word '",
				this.lookAhead(1).getTokenString(),
				"' does not compute. Expected ",
				expectedTokenType
			}), Error.ErrorType.SYNTAX, this.lookAhead(1).LineNr, this.lookAhead(1).LinePosition);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006A50 File Offset: 0x00004C50
		public void consumeCurrentToken()
		{
			Token token;
			if (this.m_nextTokenIndex < this.m_tokens.Count)
			{
				token = this.m_tokens[this.m_nextTokenIndex];
				this.m_nextTokenIndex++;
			}
			else
			{
				token = new Token(Token.TokenType.EOF, "<EOF>");
			}
			this.m_lookahead[this.m_lookaheadIndex] = token;
			this.m_lookaheadIndex = (this.m_lookaheadIndex + 1) % this.k;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00006AC8 File Offset: 0x00004CC8
		public void backtrackToToken(Token pToken)
		{
			this.m_nextTokenIndex = 0;
			this.m_lookaheadIndex = 0;
			for (int i = 0; i < this.k; i++)
			{
				this.consumeCurrentToken();
			}
			while (this.lookAhead(1) != pToken)
			{
				this.consumeCurrentToken();
			}
			Console.WriteLine("Found token");
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00006B24 File Offset: 0x00004D24
		public Token lookAhead(int i)
		{
			return this.m_lookahead[(this.m_lookaheadIndex + i - 1) % this.k];
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00006B40 File Offset: 0x00004D40
		public Token.TokenType lookAheadType(int i)
		{
			return this.lookAhead(i).getTokenType();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006B50 File Offset: 0x00004D50
		public AST getAST()
		{
			if (this.m_programAST == null)
			{
				throw new Exception("AST is null, this probably means that you haven't called process() on Parser");
			}
			return this.m_programAST;
		}

		// Token: 0x04000036 RID: 54
		private bool m_processed = false;

		// Token: 0x04000037 RID: 55
		private List<Token> m_tokens;

		// Token: 0x04000038 RID: 56
		private int m_nextTokenIndex;

		// Token: 0x04000039 RID: 57
		private Token[] m_lookahead;

		// Token: 0x0400003A RID: 58
		private int k = 4;

		// Token: 0x0400003B RID: 59
		private int m_lookaheadIndex = 0;

		// Token: 0x0400003C RID: 60
		private AST m_programAST;

		// Token: 0x0400003D RID: 61
		private ErrorHandler m_errorHandler;

		// Token: 0x0400003E RID: 62
		private bool m_isInsideFunctionDefinition;

		// Token: 0x0400003F RID: 63
		private AST m_functionList;
	}
}
