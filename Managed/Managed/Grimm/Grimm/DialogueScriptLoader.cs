using System;
using System.Collections.Generic;
using System.IO;
using GameTypes;

namespace GrimmLib
{
	// Token: 0x02000014 RID: 20
	public class DialogueScriptLoader
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00004A10 File Offset: 0x00002C10
		public DialogueScriptLoader(DialogueRunner pDialogueRunner)
		{
			D.isNull(pDialogueRunner);
			this._dialogueRunner = pDialogueRunner;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004A4C File Offset: 0x00002C4C
		public void CreateDialogueNodesFromString(string pString, string pConversation)
		{
			using (StringReader stringReader = new StringReader(pString))
			{
				this.CreateDialogueNodes(stringReader, pConversation);
				stringReader.Close();
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004A9C File Offset: 0x00002C9C
		public void LoadDialogueNodesFromFile(string pFilepath)
		{
			string conversationNameFromFilepath = DialogueScriptLoader.GetConversationNameFromFilepath(pFilepath);
			using (StreamReader streamReader = File.OpenText(pFilepath))
			{
				this.CreateDialogueNodes(streamReader, conversationNameFromFilepath);
				streamReader.Close();
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004AF4 File Offset: 0x00002CF4
		private void CreateDialogueNodes(TextReader pTextReader, string pConversation)
		{
			this._conversationName = pConversation;
			Tokenizer tokenizer = new Tokenizer();
			this._tokens = tokenizer.process(pTextReader);
			this._loopStack = new Stack<DialogueNode>();
			this._nextTokenIndex = 0;
			this._lookaheadIndex = 0;
			this._lookahead = new Token[this.k];
			for (int i = 0; i < this.k; i++)
			{
				this.ConsumeCurrentToken();
			}
			this.Languages();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004B68 File Offset: 0x00002D68
		public static string GetConversationNameFromFilepath(string pFilepath)
		{
			if (pFilepath == null || pFilepath == "")
			{
				throw new GrimmException("Filepath is empty!");
			}
			int num = pFilepath.LastIndexOf("/");
			int num2 = pFilepath.LastIndexOf("\\");
			if (num2 > num)
			{
				num = num2;
			}
			string text = pFilepath.Substring(num + 1);
			string text2 = text;
			int num3 = text.LastIndexOf(".");
			if (num3 > -1)
			{
				text2 = text.Substring(0, num3);
			}
			return text2;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004BE4 File Offset: 0x00002DE4
		private void Languages()
		{
			this._language = Language.SWEDISH;
			while (this.lookAheadType(1) != Token.TokenType.EOF)
			{
				if (this.lookAheadType(1) == Token.TokenType.LANGUAGE)
				{
					this.match(Token.TokenType.LANGUAGE);
					Token token = this.match(Token.TokenType.NAME);
					string tokenString = token.getTokenString();
					string text = tokenString.ToLower();
					if (text != null)
					{
						if (DialogueScriptLoader.<>f__switch$map0 == null)
						{
							DialogueScriptLoader.<>f__switch$map0 = new Dictionary<string, int>(2)
							{
								{ "swedish", 0 },
								{ "english", 1 }
							};
						}
						int num;
						if (DialogueScriptLoader.<>f__switch$map0.TryGetValue(text, out num))
						{
							if (num == 0)
							{
								this._language = Language.SWEDISH;
								goto IL_00C2;
							}
							if (num == 1)
							{
								this._language = Language.ENGLISH;
								goto IL_00C2;
							}
						}
					}
					throw new GrimmException("Can't handle language '" + tokenString + "'");
				}
				IL_00C2:
				this.CreateTreeOfDialogueNodes();
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004CC8 File Offset: 0x00002EC8
		private void CreateTreeOfDialogueNodes()
		{
			this._nodeCounter = 0;
			ConversationStartDialogueNode conversationStartDialogueNode = this._dialogueRunner.Create<ConversationStartDialogueNode>(this._conversationName, this._language, "__Start__");
			ConversationEndDialogueNode conversationEndDialogueNode = this._dialogueRunner.Create<ConversationEndDialogueNode>(this._conversationName, this._language, "__End__");
			this.Nodes(conversationStartDialogueNode, conversationEndDialogueNode);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004D20 File Offset: 0x00002F20
		private void Nodes(DialogueNode pPrevious, DialogueNode pScopeEndNode)
		{
			while (this.lookAheadType(1) != Token.TokenType.EOF && this.lookAheadType(1) != Token.TokenType.BLOCK_END && this.lookAheadType(1) != Token.TokenType.QUOTED_STRING && this.lookAheadType(1) != Token.TokenType.LANGUAGE)
			{
				DialogueNode dialogueNode = this.Statement(pPrevious);
				if (dialogueNode != null)
				{
					pPrevious = dialogueNode;
				}
			}
			this.AddLinkFromPreviousNode(pPrevious, pScopeEndNode);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004D84 File Offset: 0x00002F84
		private DialogueNode Statement(DialogueNode pPrevious)
		{
			if (this.lookAheadType(1) == Token.TokenType.NEW_LINE)
			{
				this.match(Token.TokenType.NEW_LINE);
			}
			else if (this.lookAheadType(1) == Token.TokenType.EOF)
			{
				this.match(Token.TokenType.EOF);
			}
			else
			{
				if (this.lookAheadType(1) == Token.TokenType.NAME && this.lookAheadType(2) == Token.TokenType.QUOTED_STRING)
				{
					return this.VisitTimedDialogueNode(pPrevious);
				}
				if (this.lookAheadType(1) == Token.TokenType.NAME && this.lookAheadType(2) == Token.TokenType.PARANTHESIS_LEFT)
				{
					return this.VisitFunctionDialogueNode(pPrevious);
				}
				if (this.lookAheadType(1) == Token.TokenType.NAME && this.lookAheadType(2) == Token.TokenType.DOT)
				{
					return this.VisitFunctionDialogueNode(pPrevious);
				}
				if (this.lookAheadType(1) == Token.TokenType.GOTO)
				{
					return this.VisitGotoDialogueNode(pPrevious);
				}
				if (this.lookAheadType(1) == Token.TokenType.START)
				{
					return this.VisitStartCommandoDialogueNode(pPrevious);
				}
				if (this.lookAheadType(1) == Token.TokenType.INTERRUPT)
				{
					return this.VisitInterruptDialogueNode(pPrevious);
				}
				if (this.lookAheadType(1) == Token.TokenType.IF)
				{
					return this.VisitIfDialogueNode(pPrevious);
				}
				if (this.lookAheadType(1) == Token.TokenType.ASSERT)
				{
					return this.VisitAssertDialogueNode(pPrevious);
				}
				if (this.lookAheadType(1) == Token.TokenType.STOP)
				{
					return this.VisitStopDialogueNode(pPrevious);
				}
				if (this.lookAheadType(1) == Token.TokenType.BROADCAST)
				{
					return this.VisitBroadcastDialogueNode(pPrevious);
				}
				if (this.lookAheadType(1) == Token.TokenType.LISTEN)
				{
					return this.VisitListeningDialogueNode(pPrevious);
				}
				if (this.lookAheadType(1) == Token.TokenType.CANCEL)
				{
					return this.VisitCancelDialogueNode(pPrevious);
				}
				if (this.lookAheadType(1) == Token.TokenType.WAIT)
				{
					if (this.lookAheadType(2) == Token.TokenType.NUMBER)
					{
						return this.VisitTimedWaitDialogueNode(pPrevious);
					}
					return this.VisitWaitDialogueNode(pPrevious);
				}
				else
				{
					if (this.lookAheadType(1) == Token.TokenType.FOCUS)
					{
						return this.VisitFocusDialogueNode(pPrevious);
					}
					if (this.lookAheadType(1) == Token.TokenType.DEFOCUS)
					{
						return this.VisitDefocusDialogueNode(pPrevious);
					}
					if (this.lookAheadType(1) == Token.TokenType.LOOP)
					{
						return this.VisitLoopDialogueNode(pPrevious);
					}
					if (this.lookAheadType(1) == Token.TokenType.BREAK)
					{
						return this.VisitBreakDialogueNode(pPrevious);
					}
					if (this.lookAheadType(1) == Token.TokenType.BLOCK_BEGIN)
					{
						return this.VisitBranchingDialogueNode(pPrevious);
					}
					if (this.lookAheadType(1) == Token.TokenType.BRACKET_LEFT)
					{
						return this.VisitEmptyNodeWithName(pPrevious);
					}
					if (this.lookAheadType(1) == Token.TokenType.CHOICE)
					{
						return this.VisitBranchingDialogueNode(pPrevious);
					}
					throw new GrimmException(string.Concat(new object[]
					{
						"Can't figure out statement type of token ",
						this.lookAheadType(1),
						" with string ",
						this.lookAhead(1).getTokenString(),
						" on line ",
						this.lookAhead(1).LineNr,
						" and position",
						this.lookAhead(1).LinePosition,
						" in conversation ",
						this._conversationName
					}));
				}
			}
			return null;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005044 File Offset: 0x00003244
		private TimedDialogueNode VisitTimedDialogueNode(DialogueNode pPrevious)
		{
			Token token = this.match(Token.TokenType.NAME);
			string tokenString = token.getTokenString();
			Token token2 = this.match(Token.TokenType.QUOTED_STRING);
			string tokenString2 = token2.getTokenString();
			TimedDialogueNode timedDialogueNode = this._dialogueRunner.Create<TimedDialogueNode>(this._conversationName, this._language, this._nodeCounter++.ToString() + "_line_" + token2.LineNr);
			timedDialogueNode.speaker = tokenString;
			timedDialogueNode.line = tokenString2;
			timedDialogueNode.CalculateAndSetTimeBasedOnLineLength(false);
			if (this.lookAheadType(1) == Token.TokenType.BRACKET_LEFT)
			{
				this.match(Token.TokenType.BRACKET_LEFT);
				string tokenString3 = this.match(Token.TokenType.NAME).getTokenString();
				timedDialogueNode.name = tokenString3;
				this.match(Token.TokenType.BRACKET_RIGHT);
			}
			this.AddLinkFromPreviousNode(pPrevious, timedDialogueNode);
			return timedDialogueNode;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005114 File Offset: 0x00003314
		private DialogueNode VisitCancelDialogueNode(DialogueNode pPrevious)
		{
			this.match(Token.TokenType.CANCEL);
			Token token = this.match(Token.TokenType.NAME);
			CancelDialogueNode cancelDialogueNode = this._dialogueRunner.Create<CancelDialogueNode>(this._conversationName, this._language, this._nodeCounter++.ToString() + "(cancel)");
			cancelDialogueNode.handle = token.getTokenString();
			this.AddLinkFromPreviousNode(pPrevious, cancelDialogueNode);
			return cancelDialogueNode;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005184 File Offset: 0x00003384
		private DialogueNode VisitBroadcastDialogueNode(DialogueNode pPrevious)
		{
			this.match(Token.TokenType.BROADCAST);
			string astringFromNextToken = this.GetAStringFromNextToken(false, false);
			BroadcastDialogueNode broadcastDialogueNode = this._dialogueRunner.Create<BroadcastDialogueNode>(this._conversationName, this._language, this._nodeCounter++.ToString() + "(broadcaster)");
			broadcastDialogueNode.eventName = astringFromNextToken;
			this.AddLinkFromPreviousNode(pPrevious, broadcastDialogueNode);
			return broadcastDialogueNode;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000051F0 File Offset: 0x000033F0
		private string GetAStringFromNextToken(bool pQuotedStringsAreOK, bool pNumbersAreOK)
		{
			Token token;
			if (this.lookAheadType(1) == Token.TokenType.NUMBER && pNumbersAreOK)
			{
				token = this.match(Token.TokenType.NUMBER);
			}
			else if (this.lookAheadType(1) == Token.TokenType.QUOTED_STRING && pQuotedStringsAreOK)
			{
				token = this.match(Token.TokenType.QUOTED_STRING);
			}
			else
			{
				token = this.match(Token.TokenType.NAME);
			}
			return token.getTokenString();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000524C File Offset: 0x0000344C
		private DialogueNode VisitListeningDialogueNode(DialogueNode pPrevious)
		{
			this.match(Token.TokenType.LISTEN);
			string astringFromNextToken = this.GetAStringFromNextToken(false, false);
			string text = "";
			if (this.lookAheadType(1) == Token.TokenType.BRACKET_LEFT)
			{
				this.match(Token.TokenType.BRACKET_LEFT);
				Token token = this.match(Token.TokenType.NAME);
				text = token.getTokenString();
				this.match(Token.TokenType.BRACKET_RIGHT);
			}
			else if (this.lookAheadType(1) != Token.TokenType.EOF && this.lookAheadType(1) != Token.TokenType.NEW_LINE && this.lookAheadType(1) != Token.TokenType.BLOCK_BEGIN)
			{
				throw new GrimmException(string.Concat(new object[]
				{
					"Can't follow LISTEN statement with token of type ",
					this.lookAheadType(1),
					" at line ",
					this.lookAhead(1).LineNr,
					" and position ",
					this.lookAhead(1).LinePosition,
					" in ",
					this._conversationName
				}));
			}
			ListeningDialogueNode listeningDialogueNode = this._dialogueRunner.Create<ListeningDialogueNode>(this._conversationName, this._language, this._nodeCounter++.ToString() + "(event listener)");
			listeningDialogueNode.eventName = astringFromNextToken;
			listeningDialogueNode.handle = text;
			if (this._loopStack.Count > 0)
			{
				listeningDialogueNode.scopeNode = this._loopStack.Peek().name;
			}
			SilentDialogueNode silentDialogueNode = this._dialogueRunner.Create<SilentDialogueNode>(this._conversationName, this._language, this._nodeCounter++.ToString() + "(silent stop node)");
			this.AllowLineBreak();
			if (this.lookAheadType(1) == Token.TokenType.BLOCK_BEGIN)
			{
				this._loopStack.Push(listeningDialogueNode);
				ImmediateNode immediateNode = this._dialogueRunner.Create<ImmediateNode>(this._conversationName, this._language, this._nodeCounter++.ToString() + "(eventBranchStartNode)");
				listeningDialogueNode.branchNode = immediateNode.name;
				listeningDialogueNode.hasBranch = true;
				this.match(Token.TokenType.BLOCK_BEGIN);
				this.Nodes(immediateNode, silentDialogueNode);
				this.match(Token.TokenType.BLOCK_END);
				this._loopStack.Pop();
			}
			this.AddLinkFromPreviousNode(pPrevious, listeningDialogueNode);
			return listeningDialogueNode;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000549C File Offset: 0x0000369C
		private CallFunctionDialogueNode VisitFunctionDialogueNode(DialogueNode pPrevious)
		{
			string text = "";
			string[] array = this.VisitFunctionCall(out text);
			CallFunctionDialogueNode callFunctionDialogueNode = this._dialogueRunner.Create<CallFunctionDialogueNode>(this._conversationName, this._language, this._nodeCounter++.ToString() + "_" + text);
			callFunctionDialogueNode.function = text;
			callFunctionDialogueNode.args = array;
			if (!this._dialogueRunner.HasFunction(text))
			{
			}
			this.AddLinkFromPreviousNode(pPrevious, callFunctionDialogueNode);
			return callFunctionDialogueNode;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000551C File Offset: 0x0000371C
		private string[] VisitFunctionCall(out string pFunctionName)
		{
			List<string> list = new List<string>();
			if (this.lookAheadType(2) == Token.TokenType.DOT)
			{
				Token token = this.match(Token.TokenType.NAME);
				string tokenString = token.getTokenString();
				list.Add(tokenString);
				this.match(Token.TokenType.DOT);
			}
			Token token2 = this.match(Token.TokenType.NAME);
			pFunctionName = token2.getTokenString();
			this.match(Token.TokenType.PARANTHESIS_LEFT);
			while (this.lookAheadType(1) != Token.TokenType.PARANTHESIS_RIGHT)
			{
				if (this.lookAheadType(1) == Token.TokenType.NEW_LINE)
				{
					this.match(Token.TokenType.NEW_LINE);
				}
				else
				{
					string astringFromNextToken = this.GetAStringFromNextToken(true, true);
					list.Add(astringFromNextToken);
					if (this.lookAheadType(1) != Token.TokenType.COMMA)
					{
						IL_00AF:
						this.match(Token.TokenType.PARANTHESIS_RIGHT);
						return list.ToArray();
					}
					this.match(Token.TokenType.COMMA);
				}
			}
			goto IL_00AF;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000055E8 File Offset: 0x000037E8
		private DialogueNode VisitEmptyNodeWithName(DialogueNode pPreviousNode)
		{
			this.match(Token.TokenType.BRACKET_LEFT);
			string tokenString = this.match(Token.TokenType.NAME).getTokenString();
			ImmediateNode immediateNode = this._dialogueRunner.Create<ImmediateNode>(this._conversationName, this._language, this._nodeCounter++.ToString());
			immediateNode.name = tokenString;
			this.match(Token.TokenType.BRACKET_RIGHT);
			this.AddLinkFromPreviousNode(pPreviousNode, immediateNode);
			return immediateNode;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005658 File Offset: 0x00003858
		private void AddLinkFromPreviousNode(DialogueNode pPreviousNode, DialogueNode pNewNode)
		{
			D.isNull(pPreviousNode);
			D.isNull(pNewNode);
			pPreviousNode.nextNode = pNewNode.name;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005674 File Offset: 0x00003874
		private int CalculateTimeout(string pLine)
		{
			int length = pLine.Length;
			return (int)(30f + (float)length * 1f);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005698 File Offset: 0x00003898
		private GotoDialogueNode VisitGotoDialogueNode(DialogueNode pPrevious)
		{
			this.match(Token.TokenType.GOTO);
			Token token = this.match(Token.TokenType.NAME);
			GotoDialogueNode gotoDialogueNode = this._dialogueRunner.Create<GotoDialogueNode>(this._conversationName, this._language, this._nodeCounter++ + " (goto)");
			gotoDialogueNode.linkedNode = token.getTokenString();
			this.AddLinkFromPreviousNode(pPrevious, gotoDialogueNode);
			return gotoDialogueNode;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005704 File Offset: 0x00003904
		private DialogueNode VisitStopDialogueNode(DialogueNode pPrevious)
		{
			this.match(Token.TokenType.STOP);
			string text;
			if (this.lookAheadType(1) == Token.TokenType.NAME || this.lookAheadType(1) == Token.TokenType.QUOTED_STRING)
			{
				text = this.GetAStringFromNextToken(true, false);
			}
			else
			{
				text = this._conversationName;
			}
			StopDialogueNode stopDialogueNode = this._dialogueRunner.Create<StopDialogueNode>(this._conversationName, this._language, this._nodeCounter++ + " (stop)");
			stopDialogueNode.conversationToStop = text;
			this.AddLinkFromPreviousNode(pPrevious, stopDialogueNode);
			return stopDialogueNode;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000579C File Offset: 0x0000399C
		private StartCommandoDialogueNode VisitStartCommandoDialogueNode(DialogueNode pPrevious)
		{
			this.match(Token.TokenType.START);
			string astringFromNextToken = this.GetAStringFromNextToken(false, false);
			StartCommandoDialogueNode startCommandoDialogueNode = this._dialogueRunner.Create<StartCommandoDialogueNode>(this._conversationName, this._language, this._nodeCounter++ + " (start commando)");
			startCommandoDialogueNode.commando = astringFromNextToken;
			this.AddLinkFromPreviousNode(pPrevious, startCommandoDialogueNode);
			return startCommandoDialogueNode;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005804 File Offset: 0x00003A04
		private InterruptDialogueNode VisitInterruptDialogueNode(DialogueNode pPrevious)
		{
			this.match(Token.TokenType.INTERRUPT);
			string astringFromNextToken = this.GetAStringFromNextToken(false, false);
			InterruptDialogueNode interruptDialogueNode = this._dialogueRunner.Create<InterruptDialogueNode>(this._conversationName, this._language, this._nodeCounter++ + " (interrupt commando)");
			interruptDialogueNode.interruptingConversation = astringFromNextToken;
			this.AddLinkFromPreviousNode(pPrevious, interruptDialogueNode);
			return interruptDialogueNode;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000586C File Offset: 0x00003A6C
		private TimedWaitDialogueNode VisitTimedWaitDialogueNode(DialogueNode pPrevious)
		{
			this.match(Token.TokenType.WAIT);
			Token token = this.match(Token.TokenType.NUMBER);
			TimedWaitDialogueNode timedWaitDialogueNode = this._dialogueRunner.Create<TimedWaitDialogueNode>(this._conversationName, this._language, this._nodeCounter++ + " (timed wait node)");
			TimedWaitDialogueNode timedWaitDialogueNode2 = timedWaitDialogueNode;
			float num = Convert.ToSingle(token.getTokenString());
			timedWaitDialogueNode.timerStartValue = num;
			timedWaitDialogueNode2.timer = num;
			this.AddLinkFromPreviousNode(pPrevious, timedWaitDialogueNode);
			return timedWaitDialogueNode;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000058E8 File Offset: 0x00003AE8
		private WaitDialogueNode VisitWaitDialogueNode(DialogueNode pPrevious)
		{
			this.match(Token.TokenType.WAIT);
			WaitDialogueNode waitDialogueNode = this._dialogueRunner.Create<WaitDialogueNode>(this._conversationName, this._language, this._nodeCounter++ + " (start commando)");
			List<ExpressionDialogueNode> list = new List<ExpressionDialogueNode>();
			bool flag = false;
			for (;;)
			{
				if (this.lookAheadType(1) == Token.TokenType.NAME)
				{
					string text = "";
					string[] array = this.VisitFunctionCall(out text);
					ExpressionDialogueNode expressionDialogueNode = this._dialogueRunner.Create<ExpressionDialogueNode>(this._conversationName, this._language, this._nodeCounter++ + " (expression)");
					expressionDialogueNode.expression = text;
					expressionDialogueNode.args = array;
					list.Add(expressionDialogueNode);
				}
				else if (this.lookAheadType(1) == Token.TokenType.AND)
				{
					this.ConsumeCurrentToken();
				}
				else
				{
					if (this.lookAheadType(1) != Token.TokenType.LISTEN)
					{
						goto IL_0132;
					}
					if (flag)
					{
						break;
					}
					this.ConsumeCurrentToken();
					waitDialogueNode.eventName = this.match(Token.TokenType.NAME).getTokenString();
					flag = true;
				}
			}
			throw new GrimmException(this._conversationName + " already has a event listener attached to the wait statement on line " + this.lookAhead(1).LineNr);
			IL_0132:
			waitDialogueNode.expressions = list.ToArray();
			string text2 = "";
			if (this.lookAheadType(1) == Token.TokenType.BRACKET_LEFT)
			{
				this.match(Token.TokenType.BRACKET_LEFT);
				Token token = this.match(Token.TokenType.NAME);
				text2 = token.getTokenString();
				this.match(Token.TokenType.BRACKET_RIGHT);
			}
			waitDialogueNode.handle = text2;
			if (this._loopStack.Count > 0)
			{
				waitDialogueNode.scopeNode = this._loopStack.Peek().name;
			}
			SilentDialogueNode silentDialogueNode = this._dialogueRunner.Create<SilentDialogueNode>(this._conversationName, this._language, this._nodeCounter++.ToString() + "(silent stop node)");
			this.AllowLineBreak();
			if (this.lookAheadType(1) == Token.TokenType.BLOCK_BEGIN)
			{
				this._loopStack.Push(waitDialogueNode);
				ImmediateNode immediateNode = this._dialogueRunner.Create<ImmediateNode>(this._conversationName, this._language, this._nodeCounter++.ToString() + "(waitBranchStartNode)");
				waitDialogueNode.branchNode = immediateNode.name;
				waitDialogueNode.hasBranch = true;
				this.match(Token.TokenType.BLOCK_BEGIN);
				this.Nodes(immediateNode, silentDialogueNode);
				this.match(Token.TokenType.BLOCK_END);
				this._loopStack.Pop();
			}
			this.AddLinkFromPreviousNode(pPrevious, waitDialogueNode);
			return waitDialogueNode;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005B8C File Offset: 0x00003D8C
		private FocusDialogueNode VisitFocusDialogueNode(DialogueNode pPrevious)
		{
			this.match(Token.TokenType.FOCUS);
			FocusDialogueNode focusDialogueNode = this._dialogueRunner.Create<FocusDialogueNode>(this._conversationName, this._language, this._nodeCounter++ + " (focus)");
			this.AddLinkFromPreviousNode(pPrevious, focusDialogueNode);
			return focusDialogueNode;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005BE4 File Offset: 0x00003DE4
		private DefocusDialogueNode VisitDefocusDialogueNode(DialogueNode pPrevious)
		{
			this.match(Token.TokenType.DEFOCUS);
			DefocusDialogueNode defocusDialogueNode = this._dialogueRunner.Create<DefocusDialogueNode>(this._conversationName, this._language, this._nodeCounter++ + " (defocus)");
			this.AddLinkFromPreviousNode(pPrevious, defocusDialogueNode);
			return defocusDialogueNode;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005C3C File Offset: 0x00003E3C
		private void AllowLineBreak()
		{
			if (this.lookAheadType(1) == Token.TokenType.NEW_LINE)
			{
				this.match(Token.TokenType.NEW_LINE);
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005C54 File Offset: 0x00003E54
		private DialogueNode VisitLoopDialogueNode(DialogueNode pPrevious)
		{
			this.match(Token.TokenType.LOOP);
			LoopDialogueNode loopDialogueNode = this._dialogueRunner.Create<LoopDialogueNode>(this._conversationName, this._language, this._nodeCounter++.ToString() + " (loop)");
			this.AddLinkFromPreviousNode(pPrevious, loopDialogueNode);
			this.AllowLineBreak();
			this._loopStack.Push(loopDialogueNode);
			this.match(Token.TokenType.BLOCK_BEGIN);
			ImmediateNode immediateNode = this._dialogueRunner.Create<ImmediateNode>(this._conversationName, this._language, this._nodeCounter++.ToString() + " (loop branch node)");
			loopDialogueNode.branchNode = immediateNode.name;
			SilentDialogueNode silentDialogueNode = this._dialogueRunner.Create<SilentDialogueNode>(this._conversationName, this._language, this._nodeCounter++.ToString() + " (unified end node for loop)");
			this.Nodes(immediateNode, silentDialogueNode);
			this.match(Token.TokenType.BLOCK_END);
			this._loopStack.Pop();
			return loopDialogueNode;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005D70 File Offset: 0x00003F70
		private DialogueNode VisitBreakDialogueNode(DialogueNode pPrevious)
		{
			Token token = this.match(Token.TokenType.BREAK);
			BreakDialogueNode breakDialogueNode = this._dialogueRunner.Create<BreakDialogueNode>(this._conversationName, this._language, this._nodeCounter++.ToString() + "(break)");
			if (this._loopStack.Count > 0)
			{
				breakDialogueNode.breakTargetLoop = this._loopStack.Peek().name;
				this.AddLinkFromPreviousNode(pPrevious, breakDialogueNode);
				return breakDialogueNode;
			}
			throw new GrimmException(string.Concat(new object[] { "Trying to break at weird position? Line: ", token.LineNr, " in conversation '", this._conversationName, "'" }));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005E38 File Offset: 0x00004038
		private DialogueNode VisitAssertDialogueNode(DialogueNode pPrevious)
		{
			this.match(Token.TokenType.ASSERT);
			string text = "";
			string[] array = this.VisitFunctionCall(out text);
			AssertDialogueNode assertDialogueNode = this._dialogueRunner.Create<AssertDialogueNode>(this._conversationName, this._language, this._nodeCounter++ + " (assert)");
			assertDialogueNode.expression = text;
			assertDialogueNode.args = array;
			if (!this._dialogueRunner.HasExpression(text))
			{
				throw new GrimmException("There is no '" + text + "' expression registered in the dialogue runner");
			}
			this.AddLinkFromPreviousNode(pPrevious, assertDialogueNode);
			return assertDialogueNode;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005ED4 File Offset: 0x000040D4
		private DialogueNode VisitIfDialogueNode(DialogueNode pPrevious)
		{
			this.match(Token.TokenType.IF);
			string text = "";
			string[] array = this.VisitFunctionCall(out text);
			this.AllowLineBreak();
			this.match(Token.TokenType.BLOCK_BEGIN);
			UnifiedEndNodeForScope unifiedEndNodeForScope = this._dialogueRunner.Create<UnifiedEndNodeForScope>(this._conversationName, this._language, this._nodeCounter++ + " (unified end of if)");
			ExpressionDialogueNode expressionDialogueNode = this._dialogueRunner.Create<ExpressionDialogueNode>(this._conversationName, this._language, this._nodeCounter++ + " (if true)");
			this.Nodes(expressionDialogueNode, unifiedEndNodeForScope);
			expressionDialogueNode.expression = text;
			expressionDialogueNode.args = array;
			this.match(Token.TokenType.BLOCK_END);
			this.AllowLineBreak();
			ImmediateNode immediateNode = null;
			List<ExpressionDialogueNode> list = new List<ExpressionDialogueNode>();
			while (this.lookAheadType(1) == Token.TokenType.ELIF)
			{
				this.match(Token.TokenType.ELIF);
				string text2 = "";
				string[] array2 = this.VisitFunctionCall(out text2);
				this.AllowLineBreak();
				this.match(Token.TokenType.BLOCK_BEGIN);
				ExpressionDialogueNode expressionDialogueNode2 = this._dialogueRunner.Create<ExpressionDialogueNode>(this._conversationName, this._language, this._nodeCounter++ + " (elif)");
				this.Nodes(expressionDialogueNode2, unifiedEndNodeForScope);
				expressionDialogueNode2.expression = text2;
				expressionDialogueNode2.args = array2;
				list.Add(expressionDialogueNode2);
				this.match(Token.TokenType.BLOCK_END);
				this.AllowLineBreak();
			}
			if (this.lookAheadType(1) == Token.TokenType.ELSE)
			{
				this.match(Token.TokenType.ELSE);
				this.AllowLineBreak();
				this.match(Token.TokenType.BLOCK_BEGIN);
				immediateNode = this._dialogueRunner.Create<ImmediateNode>(this._conversationName, this._language, this._nodeCounter++ + " (if false)");
				this.Nodes(immediateNode, unifiedEndNodeForScope);
				this.match(Token.TokenType.BLOCK_END);
			}
			IfDialogueNode ifDialogueNode = this._dialogueRunner.Create<IfDialogueNode>(this._conversationName, this._language, this._nodeCounter++ + " (if)");
			this.AddLinkFromPreviousNode(pPrevious, ifDialogueNode);
			ifDialogueNode.nextNode = unifiedEndNodeForScope.name;
			ifDialogueNode.ifTrueNode = expressionDialogueNode;
			ifDialogueNode.elifNodes = list.ToArray();
			if (immediateNode != null)
			{
				ifDialogueNode.ifFalseNode = immediateNode;
			}
			else
			{
				ifDialogueNode.ifFalseNode = null;
			}
			if (!this._dialogueRunner.HasExpression(text))
			{
			}
			return unifiedEndNodeForScope;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000615C File Offset: 0x0000435C
		private DialogueNode VisitBranchingDialogueNode(DialogueNode pPrevious)
		{
			if (this.lookAheadType(1) == Token.TokenType.CHOICE)
			{
				this.match(Token.TokenType.CHOICE);
			}
			bool flag = this.lookAheadType(1) == Token.TokenType.ETERNAL;
			if (flag)
			{
				this.match(Token.TokenType.ETERNAL);
			}
			this.match(Token.TokenType.BLOCK_BEGIN);
			BranchingDialogueNode branchingDialogueNode = this._dialogueRunner.Create<BranchingDialogueNode>(this._conversationName, this._language, this._nodeCounter++.ToString() + " (branching node)");
			pPrevious.nextNode = branchingDialogueNode.name;
			UnifiedEndNodeForScope unifiedEndNodeForScope = this._dialogueRunner.Create<UnifiedEndNodeForScope>(this._conversationName, this._language, this._nodeCounter++ + " (unified end of options)");
			branchingDialogueNode.unifiedEndNodeForScope = unifiedEndNodeForScope.name;
			List<string> list = new List<string>();
			while (this.lookAheadType(1) != Token.TokenType.EOF && this.lookAheadType(1) != Token.TokenType.BLOCK_END)
			{
				DialogueNode dialogueNode = this.FigureOutOptionStatement(unifiedEndNodeForScope);
				if (dialogueNode != null)
				{
					list.Add(dialogueNode.name);
				}
			}
			branchingDialogueNode.nextNodes = list.ToArray();
			branchingDialogueNode.eternal = flag;
			if (branchingDialogueNode.nextNodes.Length < 2)
			{
				Console.WriteLine(string.Concat(new object[]
				{
					"\nWarning! Branching node ",
					branchingDialogueNode.name,
					" with only ",
					branchingDialogueNode.nextNodes.Length,
					" nodes in ",
					this._conversationName
				}));
			}
			this.match(Token.TokenType.BLOCK_END);
			return unifiedEndNodeForScope;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000062EC File Offset: 0x000044EC
		private DialogueNode FigureOutOptionStatement(DialogueNode pScopeEndNode)
		{
			if (this.lookAheadType(1) == Token.TokenType.NEW_LINE)
			{
				this.match(Token.TokenType.NEW_LINE);
			}
			else if (this.lookAheadType(1) == Token.TokenType.EOF)
			{
				this.match(Token.TokenType.EOF);
			}
			else
			{
				if (this.lookAheadType(1) == Token.TokenType.QUOTED_STRING && this.lookAheadType(2) == Token.TokenType.COLON)
				{
					return this.VisitOption(pScopeEndNode);
				}
				throw new GrimmException(string.Concat(new object[]
				{
					"Can't figure out player option statement type of token ",
					this.lookAheadType(1),
					" with string ",
					this.lookAhead(1).getTokenString(),
					" on line ",
					this.lookAhead(1).LineNr,
					" and position",
					this.lookAhead(1).LinePosition,
					" in conversation ",
					this._conversationName
				}));
			}
			return null;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000063DC File Offset: 0x000045DC
		private DialogueNode VisitOption(DialogueNode pScopeEndNode)
		{
			Token token = this.match(Token.TokenType.QUOTED_STRING);
			this.match(Token.TokenType.COLON);
			TimedDialogueNode timedDialogueNode = this._dialogueRunner.Create<TimedDialogueNode>(this._conversationName, this._language, this._nodeCounter++.ToString());
			timedDialogueNode.line = token.getTokenString();
			timedDialogueNode.speaker = this._playerCharacterName;
			timedDialogueNode.CalculateAndSetTimeBasedOnLineLength(true);
			this.Nodes(timedDialogueNode, pScopeEndNode);
			return timedDialogueNode;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006454 File Offset: 0x00004654
		private Token match(Token.TokenType expectedTokenType)
		{
			Token token = this.lookAhead(1);
			if (this.lookAheadType(1) == expectedTokenType)
			{
				this.ConsumeCurrentToken();
				return token;
			}
			throw new GrimmException(string.Concat(new object[]
			{
				"The code word \"",
				this.lookAhead(1).getTokenString(),
				"\" doesn't match the expected (",
				expectedTokenType,
				"). at line ",
				this.lookAhead(1).LineNr,
				" and position ",
				this.lookAhead(1).LinePosition,
				" in conversation '",
				this._conversationName,
				"'"
			}));
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006510 File Offset: 0x00004710
		private void ConsumeCurrentToken()
		{
			Token token;
			if (this._nextTokenIndex < this._tokens.Count)
			{
				token = this._tokens[this._nextTokenIndex];
				this._nextTokenIndex++;
			}
			else
			{
				token = new Token(Token.TokenType.EOF, "<EOF>");
			}
			this._lookahead[this._lookaheadIndex] = token;
			this._lookaheadIndex = (this._lookaheadIndex + 1) % this.k;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006588 File Offset: 0x00004788
		private Token lookAhead(int i)
		{
			return this._lookahead[(this._lookaheadIndex + i - 1) % this.k];
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000065A4 File Offset: 0x000047A4
		private Token.TokenType lookAheadType(int i)
		{
			return this.lookAhead(i).getTokenType();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000065B4 File Offset: 0x000047B4
		private void SkipStuffUntilNextLine()
		{
			while (this.lookAheadType(1) != Token.TokenType.NEW_LINE)
			{
				this.ConsumeCurrentToken();
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000065D0 File Offset: 0x000047D0
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x000065D8 File Offset: 0x000047D8
		public string playerCharacterName
		{
			get
			{
				return this._playerCharacterName;
			}
			set
			{
				this._playerCharacterName = value;
			}
		}

		// Token: 0x0400005D RID: 93
		private const string NAME_OF_START_NODE = "__Start__";

		// Token: 0x0400005E RID: 94
		private const string NAME_OF_END_NODE = "__End__";

		// Token: 0x0400005F RID: 95
		private DialogueRunner _dialogueRunner;

		// Token: 0x04000060 RID: 96
		private string _playerCharacterName = "Sebastian";

		// Token: 0x04000061 RID: 97
		private string _conversationName;

		// Token: 0x04000062 RID: 98
		private Language _language;

		// Token: 0x04000063 RID: 99
		private List<Token> _tokens;

		// Token: 0x04000064 RID: 100
		private Token[] _lookahead;

		// Token: 0x04000065 RID: 101
		private int _lookaheadIndex = 0;

		// Token: 0x04000066 RID: 102
		private int _nextTokenIndex;

		// Token: 0x04000067 RID: 103
		private int k = 2;

		// Token: 0x04000068 RID: 104
		private int _nodeCounter;

		// Token: 0x04000069 RID: 105
		private Stack<DialogueNode> _loopStack;
	}
}
