using System;
using System.Text;
using GameTypes;

namespace GrimmLib
{
	// Token: 0x02000018 RID: 24
	public class DialogueScriptPrinter
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x0000690C File Offset: 0x00004B0C
		public DialogueScriptPrinter(DialogueRunner pDialogueRunner)
		{
			D.isNull(pDialogueRunner);
			this._dialogueRunner = pDialogueRunner;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006924 File Offset: 0x00004B24
		public void PrintConversation(string pConversation)
		{
			this._conversation = pConversation;
			this._indentationLevel = 0;
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(pConversation, "__Start__");
			this._output = new StringBuilder();
			this.SwitchOnNode(dialogueNode);
			Console.WriteLine("Printing conversation '" + pConversation + "':");
			Console.WriteLine(this._output.ToString());
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006988 File Offset: 0x00004B88
		private void SwitchOnNode(DialogueNode pDialogueNode)
		{
			D.isNull(pDialogueNode);
			if (pDialogueNode.isOn)
			{
				this._output.Append("ON ---> ");
			}
			if (pDialogueNode is BranchingDialogueNode)
			{
				this.PrintBranchingDialogueNode(pDialogueNode as BranchingDialogueNode);
			}
			else if (pDialogueNode is ConversationEndDialogueNode)
			{
				this.PrintConversationEndDialogueNode(pDialogueNode as ConversationEndDialogueNode);
			}
			else if (pDialogueNode is ConversationStartDialogueNode)
			{
				this.PrintConversationStartDialogueNode(pDialogueNode as ConversationStartDialogueNode);
			}
			else if (pDialogueNode is TimedDialogueNode)
			{
				this.PrintTimedDialogueNode(pDialogueNode as TimedDialogueNode);
			}
			else if (pDialogueNode is UnifiedEndNodeForScope)
			{
				this.PrintUnifiedEndNodeForScope(pDialogueNode as UnifiedEndNodeForScope);
			}
			else if (pDialogueNode is GotoDialogueNode)
			{
				this.PrintGotoNode(pDialogueNode as GotoDialogueNode);
			}
			else if (pDialogueNode is IfDialogueNode)
			{
				this.PrintIfNode(pDialogueNode as IfDialogueNode);
			}
			else if (pDialogueNode is ImmediateNode)
			{
				this.PrintImmediateNode(pDialogueNode as ImmediateNode);
			}
			else if (pDialogueNode is StartCommandoDialogueNode)
			{
				this.PrintStartCommandoDialogueNode(pDialogueNode as StartCommandoDialogueNode);
			}
			else if (pDialogueNode is StopDialogueNode)
			{
				this.PrintStopCommandoDialogueNode(pDialogueNode as StopDialogueNode);
			}
			else if (pDialogueNode is InterruptDialogueNode)
			{
				this.PrintInterruptDialogueNode(pDialogueNode as InterruptDialogueNode);
			}
			else if (pDialogueNode is WaitDialogueNode)
			{
				this.PrintWaitDialogueNode(pDialogueNode as WaitDialogueNode);
			}
			else if (pDialogueNode is CallFunctionDialogueNode)
			{
				this.PrintCallFunctionDialogueNode(pDialogueNode as CallFunctionDialogueNode);
			}
			else if (pDialogueNode is AssertDialogueNode)
			{
				this.PrintAssertDialogueNode(pDialogueNode as AssertDialogueNode);
			}
			else if (pDialogueNode is ListeningDialogueNode)
			{
				this.PrintListeningDialogueNode(pDialogueNode as ListeningDialogueNode);
			}
			else if (pDialogueNode is SilentDialogueNode)
			{
				this.PrintSilentDialogueNode(pDialogueNode as SilentDialogueNode);
			}
			else if (pDialogueNode is BroadcastDialogueNode)
			{
				this.PrintBroadcastDialogueNode(pDialogueNode as BroadcastDialogueNode);
			}
			else if (pDialogueNode is CancelDialogueNode)
			{
				this.PrintCancelDialogueNode(pDialogueNode as CancelDialogueNode);
			}
			else if (pDialogueNode is FocusDialogueNode)
			{
				this.PrintFocusNode(pDialogueNode as FocusDialogueNode);
			}
			else if (pDialogueNode is DefocusDialogueNode)
			{
				this.PrintDefocusNode(pDialogueNode as FocusDialogueNode);
			}
			else if (pDialogueNode is LoopDialogueNode)
			{
				this.PrintLoopDialogueNode(pDialogueNode as LoopDialogueNode);
			}
			else if (pDialogueNode is BreakDialogueNode)
			{
				this.PrintBreakDialogueNode(pDialogueNode as BreakDialogueNode);
			}
			else if (pDialogueNode is ExpressionDialogueNode)
			{
				this.PrintExpressionDialogueNode(pDialogueNode as ExpressionDialogueNode);
			}
			else
			{
				if (!(pDialogueNode is TimedWaitDialogueNode))
				{
					throw new GrimmException("Don't understand node type " + pDialogueNode.GetType());
				}
				this.PrintTimedWaitDialogueNode(pDialogueNode as TimedWaitDialogueNode);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006C70 File Offset: 0x00004E70
		private void PrintTimedDialogueNode(TimedDialogueNode pTimedDialogueNode)
		{
			this.Indentation();
			this._output.Append(pTimedDialogueNode.speaker + ": \"" + pTimedDialogueNode.line + "\"");
			this._output.Append("\n");
			if (pTimedDialogueNode.nextNode != "")
			{
				DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pTimedDialogueNode.nextNode);
				this.SwitchOnNode(dialogueNode);
				return;
			}
			throw new GrimmException("TimedDialogueNode with name '" + pTimedDialogueNode.name + "' doesn't have a next node");
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00006D10 File Offset: 0x00004F10
		private void PrintImmediateNode(ImmediateNode pImmediateNode)
		{
			this.Indentation();
			this._output.Append(pImmediateNode.name + "\n");
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pImmediateNode.nextNode);
			this.SwitchOnNode(dialogueNode);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006D60 File Offset: 0x00004F60
		private void PrintGotoNode(GotoDialogueNode pGotoDialogueNode)
		{
			this.Indentation();
			this._output.Append("GOTO " + pGotoDialogueNode.linkedNode + "\n");
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pGotoDialogueNode.nextNode);
			this.SwitchOnNode(dialogueNode);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006DB4 File Offset: 0x00004FB4
		private void PrintSilentDialogueNode(SilentDialogueNode par1)
		{
			this.Indentation();
			this._output.Append("SILENT NODE (won't continue from here) \n");
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006DD0 File Offset: 0x00004FD0
		private void PrintStartCommandoDialogueNode(StartCommandoDialogueNode pStartCommandoNode)
		{
			this.Indentation();
			this._output.Append("START " + pStartCommandoNode.commando + "\n");
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pStartCommandoNode.nextNode);
			this.SwitchOnNode(dialogueNode);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006E24 File Offset: 0x00005024
		private void PrintInterruptDialogueNode(InterruptDialogueNode pInterruptNode)
		{
			this.Indentation();
			this._output.Append("INTERRUPT " + pInterruptNode.interruptingConversation + "\n");
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pInterruptNode.nextNode);
			this.SwitchOnNode(dialogueNode);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006E78 File Offset: 0x00005078
		private void PrintStopCommandoDialogueNode(StopDialogueNode pStopNode)
		{
			this.Indentation();
			this._output.Append("STOP " + pStopNode.conversationToStop + "\n");
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pStopNode.nextNode);
			this.SwitchOnNode(dialogueNode);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006ECC File Offset: 0x000050CC
		private void PrintCancelDialogueNode(CancelDialogueNode pCancelNode)
		{
			this.Indentation();
			this._output.Append("CANCEL " + pCancelNode.handle + "\n");
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pCancelNode.nextNode);
			this.SwitchOnNode(dialogueNode);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006F20 File Offset: 0x00005120
		private void PrintWaitDialogueNode(WaitDialogueNode pWaitNode)
		{
			this.Indentation();
			this._output.Append("WAIT_UNTIL expressions: ");
			foreach (ExpressionDialogueNode expressionDialogueNode in pWaitNode.expressions)
			{
				this._output.Append(expressionDialogueNode.expression + ", ");
			}
			if (pWaitNode.eventName != "")
			{
				this._output.Append("LISTEN for event: " + pWaitNode.eventName);
			}
			this._output.Append("\n");
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pWaitNode.nextNode);
			this.SwitchOnNode(dialogueNode);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006FE0 File Offset: 0x000051E0
		private void PrintListeningDialogueNode(ListeningDialogueNode pListeningNode)
		{
			this.Indentation();
			this._output.Append(string.Concat(new string[] { "LISTEN_FOR ", pListeningNode.eventName, " (scope: ", pListeningNode.scopeNode, ")", pListeningNode.handle, " {\n" }));
			if (pListeningNode.hasBranch)
			{
				this._indentationLevel++;
				DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pListeningNode.branchNode);
				this.SwitchOnNode(dialogueNode);
				this._indentationLevel--;
			}
			this._output.Append("}\n");
			DialogueNode dialogueNode2 = this._dialogueRunner.GetDialogueNode(this._conversation, pListeningNode.nextNode);
			this.SwitchOnNode(dialogueNode2);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000070BC File Offset: 0x000052BC
		private void PrintBroadcastDialogueNode(BroadcastDialogueNode pBroadcastNode)
		{
			this.Indentation();
			this._output.Append("BROADCAST " + pBroadcastNode.eventName + "\n");
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pBroadcastNode.nextNode);
			this.SwitchOnNode(dialogueNode);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00007110 File Offset: 0x00005310
		private void PrintFocusNode(FocusDialogueNode pNode)
		{
			this.Indentation();
			this._output.Append("FOCUS\n");
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pNode.nextNode);
			this.SwitchOnNode(dialogueNode);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00007154 File Offset: 0x00005354
		private void PrintDefocusNode(FocusDialogueNode pNode)
		{
			this.Indentation();
			this._output.Append("DEFOCUS\n");
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pNode.nextNode);
			this.SwitchOnNode(dialogueNode);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00007198 File Offset: 0x00005398
		private void PrintCallFunctionDialogueNode(CallFunctionDialogueNode pFunctionNode)
		{
			this.Indentation();
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (string text in pFunctionNode.args)
			{
				stringBuilder.Append(text);
				num++;
				if (num < pFunctionNode.args.Length)
				{
					stringBuilder.Append(", ");
				}
			}
			this._output.Append(string.Concat(new string[]
			{
				"CALL FUNCTION ",
				pFunctionNode.function,
				"(",
				stringBuilder.ToString(),
				")\n"
			}));
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pFunctionNode.nextNode);
			this.SwitchOnNode(dialogueNode);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000725C File Offset: 0x0000545C
		private void PrintIfNode(IfDialogueNode pIfDialogueNode)
		{
			this.Indentation();
			this._output.Append("IF");
			this._indentationLevel++;
			this.SwitchOnNode(pIfDialogueNode.ifTrueNode);
			this._indentationLevel--;
			foreach (ExpressionDialogueNode expressionDialogueNode in pIfDialogueNode.elifNodes)
			{
				this._output.Append("ELIF");
				this._indentationLevel++;
				this.SwitchOnNode(expressionDialogueNode);
				this._indentationLevel--;
			}
			if (pIfDialogueNode.ifFalseNode != null)
			{
				this._output.Append("ELSE\n");
				this._indentationLevel++;
				this.SwitchOnNode(pIfDialogueNode.ifFalseNode);
				this._indentationLevel--;
			}
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pIfDialogueNode.nextNode);
			this.SwitchOnNode(dialogueNode);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00007360 File Offset: 0x00005560
		private void PrintAssertDialogueNode(AssertDialogueNode pAssertNode)
		{
			this.Indentation();
			this._output.Append("ASSERT " + pAssertNode.expression + "\n");
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pAssertNode.nextNode);
			this.SwitchOnNode(dialogueNode);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000073B4 File Offset: 0x000055B4
		private void PrintLoopDialogueNode(LoopDialogueNode pLoopNode)
		{
			this.Indentation();
			this._output.Append("LOOP{\n");
			this._indentationLevel++;
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pLoopNode.branchNode);
			this.SwitchOnNode(dialogueNode);
			this._indentationLevel--;
			this._output.Append("}\n");
			DialogueNode dialogueNode2 = this._dialogueRunner.GetDialogueNode(this._conversation, pLoopNode.nextNode);
			this.SwitchOnNode(dialogueNode2);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00007444 File Offset: 0x00005644
		private void PrintBreakDialogueNode(BreakDialogueNode pBreakNode)
		{
			this.Indentation();
			this._output.Append("BREAK\n");
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pBreakNode.nextNode);
			this.SwitchOnNode(dialogueNode);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00007488 File Offset: 0x00005688
		private void PrintExpressionDialogueNode(ExpressionDialogueNode pExpressionNode)
		{
			this.Indentation();
			this._output.Append("Expression " + pExpressionNode.expression + "\n");
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pExpressionNode.nextNode);
			this.SwitchOnNode(dialogueNode);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000074DC File Offset: 0x000056DC
		private void PrintTimedWaitDialogueNode(TimedWaitDialogueNode pTimedWaitDialogueNode)
		{
			this.Indentation();
			this._output.Append("TimedWaitDialogueNode\n");
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pTimedWaitDialogueNode.nextNode);
			this.SwitchOnNode(dialogueNode);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00007520 File Offset: 0x00005720
		private void PrintBranchingDialogueNode(BranchingDialogueNode pBranchingDialogueNode)
		{
			D.isNull(pBranchingDialogueNode);
			this.Indentation();
			this._output.Append("{\n");
			this._indentationLevel++;
			int num = 1;
			foreach (string text in pBranchingDialogueNode.nextNodes)
			{
				TimedDialogueNode timedDialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, text) as TimedDialogueNode;
				D.isNull(timedDialogueNode);
				this.Indentation();
				this._output.Append(string.Concat(new object[]
				{
					num++,
					". \"",
					timedDialogueNode.line,
					"\":\n"
				}));
				D.assert(timedDialogueNode.nextNode != "");
				DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, timedDialogueNode.nextNode);
				D.isNull(dialogueNode);
				this._indentationLevel++;
				this.SwitchOnNode(dialogueNode);
				this._indentationLevel--;
			}
			this._indentationLevel--;
			this.Indentation();
			this._output.Append("}\n");
			D.assert(pBranchingDialogueNode.name != "");
			UnifiedEndNodeForScope unifiedEndNodeForScope = this._dialogueRunner.GetDialogueNode(this._conversation, pBranchingDialogueNode.unifiedEndNodeForScope) as UnifiedEndNodeForScope;
			D.isNull(unifiedEndNodeForScope);
			D.assert(unifiedEndNodeForScope.name != "");
			DialogueNode dialogueNode2 = this._dialogueRunner.GetDialogueNode(this._conversation, unifiedEndNodeForScope.nextNode);
			D.isNull(dialogueNode2);
			this.SwitchOnNode(dialogueNode2);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000076D8 File Offset: 0x000058D8
		private void PrintUnifiedEndNodeForScope(UnifiedEndNodeForScope pUnifiedEndNodeForScope)
		{
			D.isNull(pUnifiedEndNodeForScope);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000076E0 File Offset: 0x000058E0
		private void PrintConversationStartDialogueNode(ConversationStartDialogueNode pConversationStartDialogueNode)
		{
			this._output.Append("Start -> \n");
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this._conversation, pConversationStartDialogueNode.nextNode);
			this.SwitchOnNode(dialogueNode);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00007720 File Offset: 0x00005920
		private void PrintConversationEndDialogueNode(ConversationEndDialogueNode pConversationEndDialogueNode)
		{
			this.Indentation();
			this._output.Append("-> End\n");
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000773C File Offset: 0x0000593C
		private void Indentation()
		{
			for (int i = 0; i < this._indentationLevel; i++)
			{
				this._output.Append("\t");
			}
		}

		// Token: 0x0400006F RID: 111
		private DialogueRunner _dialogueRunner;

		// Token: 0x04000070 RID: 112
		private StringBuilder _output;

		// Token: 0x04000071 RID: 113
		private string _conversation;

		// Token: 0x04000072 RID: 114
		private int _indentationLevel;
	}
}
