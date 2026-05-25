using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using GameTypes;
using RelayLib;

namespace GrimmLib
{
	// Token: 0x02000004 RID: 4
	public class DialogueRunner
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002104 File Offset: 0x00000304
		public DialogueRunner(RelayTwo pRelay, Language pLanguage)
		{
			D.isNull(pRelay);
			this._dialogueTable = pRelay.GetTable("Dialogues");
			this._language = pLanguage;
			this._dialogueNodes = InstantiatorTwo.Process<DialogueNode>(this._dialogueTable);
			foreach (DialogueNode dialogueNode in this._dialogueNodes)
			{
				dialogueNode.SetRunner(this);
				if (dialogueNode is IRegisteredDialogueNode)
				{
					IRegisteredDialogueNode registeredDialogueNode = dialogueNode as IRegisteredDialogueNode;
					this._registeredDialogueNodes.Add(registeredDialogueNode);
				}
				if (dialogueNode.isOn)
				{
					this._nodesThatAreOn.Add(dialogueNode);
				}
				this.AddNodeToNodeForConversationDictionary(dialogueNode.conversation, dialogueNode);
			}
			this.RegisterBuiltInAPIExpressions();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000004 RID: 4 RVA: 0x00002244 File Offset: 0x00000444
		// (remove) Token: 0x06000005 RID: 5 RVA: 0x00002280 File Offset: 0x00000480
		private event DialogueRunner.OnSomeoneSaidSomething _onSomeoneSaidSomething
		{
			add
			{
				DialogueRunner.OnSomeoneSaidSomething onSomeoneSaidSomething = this._onSomeoneSaidSomething;
				DialogueRunner.OnSomeoneSaidSomething onSomeoneSaidSomething2;
				do
				{
					onSomeoneSaidSomething2 = onSomeoneSaidSomething;
					onSomeoneSaidSomething = Interlocked.CompareExchange<DialogueRunner.OnSomeoneSaidSomething>(ref this._onSomeoneSaidSomething, (DialogueRunner.OnSomeoneSaidSomething)Delegate.Combine(onSomeoneSaidSomething2, value), onSomeoneSaidSomething);
				}
				while (onSomeoneSaidSomething != onSomeoneSaidSomething2);
			}
			remove
			{
				DialogueRunner.OnSomeoneSaidSomething onSomeoneSaidSomething = this._onSomeoneSaidSomething;
				DialogueRunner.OnSomeoneSaidSomething onSomeoneSaidSomething2;
				do
				{
					onSomeoneSaidSomething2 = onSomeoneSaidSomething;
					onSomeoneSaidSomething = Interlocked.CompareExchange<DialogueRunner.OnSomeoneSaidSomething>(ref this._onSomeoneSaidSomething, (DialogueRunner.OnSomeoneSaidSomething)Delegate.Remove(onSomeoneSaidSomething2, value), onSomeoneSaidSomething);
				}
				while (onSomeoneSaidSomething != onSomeoneSaidSomething2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000006 RID: 6 RVA: 0x000022BC File Offset: 0x000004BC
		// (remove) Token: 0x06000007 RID: 7 RVA: 0x000022F8 File Offset: 0x000004F8
		private event DialogueRunner.OnFocusConversation _onFocusConversation
		{
			add
			{
				DialogueRunner.OnFocusConversation onFocusConversation = this._onFocusConversation;
				DialogueRunner.OnFocusConversation onFocusConversation2;
				do
				{
					onFocusConversation2 = onFocusConversation;
					onFocusConversation = Interlocked.CompareExchange<DialogueRunner.OnFocusConversation>(ref this._onFocusConversation, (DialogueRunner.OnFocusConversation)Delegate.Combine(onFocusConversation2, value), onFocusConversation);
				}
				while (onFocusConversation != onFocusConversation2);
			}
			remove
			{
				DialogueRunner.OnFocusConversation onFocusConversation = this._onFocusConversation;
				DialogueRunner.OnFocusConversation onFocusConversation2;
				do
				{
					onFocusConversation2 = onFocusConversation;
					onFocusConversation = Interlocked.CompareExchange<DialogueRunner.OnFocusConversation>(ref this._onFocusConversation, (DialogueRunner.OnFocusConversation)Delegate.Remove(onFocusConversation2, value), onFocusConversation);
				}
				while (onFocusConversation != onFocusConversation2);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000008 RID: 8 RVA: 0x00002334 File Offset: 0x00000534
		// (remove) Token: 0x06000009 RID: 9 RVA: 0x00002370 File Offset: 0x00000570
		private event DialogueRunner.OnFocusConversation _onDefocusConversation
		{
			add
			{
				DialogueRunner.OnFocusConversation onFocusConversation = this._onDefocusConversation;
				DialogueRunner.OnFocusConversation onFocusConversation2;
				do
				{
					onFocusConversation2 = onFocusConversation;
					onFocusConversation = Interlocked.CompareExchange<DialogueRunner.OnFocusConversation>(ref this._onDefocusConversation, (DialogueRunner.OnFocusConversation)Delegate.Combine(onFocusConversation2, value), onFocusConversation);
				}
				while (onFocusConversation != onFocusConversation2);
			}
			remove
			{
				DialogueRunner.OnFocusConversation onFocusConversation = this._onDefocusConversation;
				DialogueRunner.OnFocusConversation onFocusConversation2;
				do
				{
					onFocusConversation2 = onFocusConversation;
					onFocusConversation = Interlocked.CompareExchange<DialogueRunner.OnFocusConversation>(ref this._onDefocusConversation, (DialogueRunner.OnFocusConversation)Delegate.Remove(onFocusConversation2, value), onFocusConversation);
				}
				while (onFocusConversation != onFocusConversation2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600000A RID: 10 RVA: 0x000023AC File Offset: 0x000005AC
		// (remove) Token: 0x0600000B RID: 11 RVA: 0x000023E8 File Offset: 0x000005E8
		private event DialogueRunner.OnEvent _onEvent
		{
			add
			{
				DialogueRunner.OnEvent onEvent = this._onEvent;
				DialogueRunner.OnEvent onEvent2;
				do
				{
					onEvent2 = onEvent;
					onEvent = Interlocked.CompareExchange<DialogueRunner.OnEvent>(ref this._onEvent, (DialogueRunner.OnEvent)Delegate.Combine(onEvent2, value), onEvent);
				}
				while (onEvent != onEvent2);
			}
			remove
			{
				DialogueRunner.OnEvent onEvent = this._onEvent;
				DialogueRunner.OnEvent onEvent2;
				do
				{
					onEvent2 = onEvent;
					onEvent = Interlocked.CompareExchange<DialogueRunner.OnEvent>(ref this._onEvent, (DialogueRunner.OnEvent)Delegate.Remove(onEvent2, value), onEvent);
				}
				while (onEvent != onEvent2);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002424 File Offset: 0x00000624
		public void AddToTurnOnNodeList(DialogueNode pNodeToTurnOn)
		{
			this._nodesToTurnOn.Add(pNodeToTurnOn);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002434 File Offset: 0x00000634
		public void AddToTurnOffNodeList(DialogueNode pNodeToTurnOff)
		{
			this._nodesToTurnOff.Add(pNodeToTurnOff);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002444 File Offset: 0x00000644
		private void AddNodeToNodeForConversationDictionary(string pConversation, DialogueNode n)
		{
			List<DialogueNode> list = null;
			if (!this._nodesForConversation.TryGetValue(pConversation, out list))
			{
				list = new List<DialogueNode>();
				this._nodesForConversation.Add(pConversation, list);
			}
			list.Add(n);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002480 File Offset: 0x00000680
		public T Create<T>(string pConversation, Language pLanguage, string pName) where T : DialogueNode
		{
			T t = Activator.CreateInstance<T>();
			t.CreateNewRelayEntry(this._dialogueTable, t.GetType().Name);
			t.conversation = pConversation;
			t.language = pLanguage;
			t.name = pName;
			t.SetRunner(this);
			this._dialogueNodes.Add(t);
			this.AddNodeToNodeForConversationDictionary(pConversation, t);
			if (t is IRegisteredDialogueNode)
			{
				IRegisteredDialogueNode registeredDialogueNode = t as IRegisteredDialogueNode;
				this._registeredDialogueNodes.Add(registeredDialogueNode);
			}
			return t;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002538 File Offset: 0x00000738
		public void LogNodesThatAreOn()
		{
			Console.WriteLine("Nodes that are on");
			foreach (DialogueNode dialogueNode in this._nodesThatAreOn)
			{
				Console.WriteLine("- " + dialogueNode.name);
			}
			Console.WriteLine("-----------------");
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000025C4 File Offset: 0x000007C4
		public List<DialogueNode> GetActiveNodes(string pConversation)
		{
			List<DialogueNode> list = new List<DialogueNode>();
			foreach (DialogueNode dialogueNode in this._nodesThatAreOn)
			{
				if (dialogueNode.conversation == pConversation)
				{
					list.Add(dialogueNode);
				}
			}
			return list;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002644 File Offset: 0x00000844
		public void Update(float dt)
		{
			foreach (DialogueNode dialogueNode in this._nodesThatAreOn)
			{
				try
				{
					if (dialogueNode.isOn)
					{
						dialogueNode.Update(dt);
					}
				}
				catch (Exception ex)
				{
					string text = dialogueNode.name + ": " + ex.ToString();
					D.Log("GRIMM ERROR: " + text);
					if (this.onGrimmError != null)
					{
						this.onGrimmError(text);
					}
				}
			}
			DialogueNode nodeToTurnOn;
			foreach (DialogueNode dialogueNode2 in this._nodesToTurnOn)
			{
				nodeToTurnOn = dialogueNode2;
				if (this._nodesThatAreOn.Find((DialogueNode n) => n == nodeToTurnOn) == null)
				{
					this._nodesThatAreOn.Add(nodeToTurnOn);
				}
			}
			this._nodesToTurnOn.Clear();
			DialogueNode nodeToTurnOff;
			foreach (DialogueNode dialogueNode3 in this._nodesToTurnOff)
			{
				nodeToTurnOff = dialogueNode3;
				if (this._nodesThatAreOn.Find((DialogueNode n) => n == nodeToTurnOff) != null)
				{
					this._nodesThatAreOn.Remove(nodeToTurnOff);
				}
			}
			this._nodesToTurnOff.Clear();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002850 File Offset: 0x00000A50
		public DialogueNode GetDialogueNode(string pConversation, string pName)
		{
			List<DialogueNode> nodesForConversation = this.GetNodesForConversation(pConversation);
			DialogueNode dialogueNode = nodesForConversation.Find((DialogueNode o) => o.name == pName);
			if (dialogueNode != null)
			{
				return dialogueNode;
			}
			throw new GrimmException(string.Concat(new string[] { "Can't find DialogueNode with name '", pName, "' in conversation '", pConversation, "'" }));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000028C4 File Offset: 0x00000AC4
		public List<DialogueNode> GetAllNodes()
		{
			return this._dialogueNodes;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000028CC File Offset: 0x00000ACC
		public List<IRegisteredDialogueNode> GetRegisteredDialogueNodes()
		{
			return this._registeredDialogueNodes;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000028D4 File Offset: 0x00000AD4
		private List<DialogueNode> GetNodesForConversation(string pConversation)
		{
			if (string.IsNullOrEmpty(pConversation))
			{
				return new List<DialogueNode>();
			}
			if (pConversation == "CMD")
			{
				return this._dialogueNodes.FindAll((DialogueNode n) => n.conversation == pConversation);
			}
			List<DialogueNode> list = null;
			if (!this._nodesForConversation.TryGetValue(pConversation, out list))
			{
				D.Log("Can't find conversation " + pConversation + " in _nodesForConversation dictionary");
				list = new List<DialogueNode>();
			}
			return list;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000296C File Offset: 0x00000B6C
		public BranchingDialogueNode GetActiveBranchingDialogueNode(string pConversation)
		{
			List<DialogueNode> nodesForConversation = this.GetNodesForConversation(pConversation);
			return nodesForConversation.Find((DialogueNode o) => o.isOn && o.language == this._language && o is BranchingDialogueNode) as BranchingDialogueNode;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000299C File Offset: 0x00000B9C
		private TimedDialogueNode GetActiveTimedDialogueNode(string pConversation)
		{
			List<DialogueNode> nodesForConversation = this.GetNodesForConversation(pConversation);
			return nodesForConversation.Find((DialogueNode o) => o.isOn && o.language == this._language && o is TimedDialogueNode) as TimedDialogueNode;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000029CC File Offset: 0x00000BCC
		public void FastForwardCurrentTimedDialogueNode(string pConversation)
		{
			TimedDialogueNode activeTimedDialogueNode = this.GetActiveTimedDialogueNode(pConversation);
			if (activeTimedDialogueNode != null)
			{
				if (activeTimedDialogueNode.timerStartValue - activeTimedDialogueNode.timer >= 0.5f)
				{
					activeTimedDialogueNode.timer = 0.01f;
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002A14 File Offset: 0x00000C14
		private void CheckThatThereIsOnlyOneActiveNodeInTheConversation(string pConversation)
		{
			List<DialogueNode> nodesForConversation = this.GetNodesForConversation(pConversation);
			DialogueNode[] array = nodesForConversation.FindAll((DialogueNode o) => o.language == this._language && o.isOn).ToArray();
			if (array.Length > 1)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (DialogueNode dialogueNode in array)
				{
					stringBuilder.Append(dialogueNode.name + ", ");
				}
				throw new GrimmException(string.Concat(new object[]
				{
					"There are ",
					array.Length,
					" active nodes in the conversation ",
					pConversation,
					": ",
					stringBuilder.ToString()
				}));
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002ACC File Offset: 0x00000CCC
		public bool ConversationIsRunning(string pConversation)
		{
			List<DialogueNode> nodesForConversation = this.GetNodesForConversation(pConversation);
			return nodesForConversation.Find((DialogueNode o) => o.language == this._language && o.isOn) != null;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002AFC File Offset: 0x00000CFC
		public void StartConversation(string pConversation)
		{
			if (this.ConversationIsRunning(pConversation))
			{
				this.logger.Log("Trying to start conversation " + pConversation + " again, even though it's already running");
				return;
			}
			List<DialogueNode> nodesForConversation = this.GetNodesForConversation(pConversation);
			DialogueNode dialogueNode = nodesForConversation.Find((DialogueNode o) => o.language == this._language && o is ConversationStartDialogueNode);
			if (dialogueNode != null)
			{
				this.logger.Log("Starting conversation '" + pConversation + "'");
				dialogueNode.Start();
				return;
			}
			if (this.HasConversation(pConversation))
			{
				throw new GrimmException("Can't find a ConversationStartDialogueNode in conversations '" + pConversation + "'");
			}
			throw new GrimmException("The dialogue runner doesn't contain the conversation '" + pConversation + "'");
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public string[] StartAllConversationsContaining(string pPartialName)
		{
			return this.DoSomethingToAllConversationsContaining(pPartialName, (DialogueNode o) => !this.ConversationIsRunning(o.conversation), delegate(ConversationStartDialogueNode convStartNode)
			{
				convStartNode.Start();
			}, "Started");
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public string[] StopAllConversationsContaining(string pPartialName)
		{
			return this.DoSomethingToAllConversationsContaining(pPartialName, (DialogueNode o) => this.ConversationIsRunning(o.conversation), delegate(ConversationStartDialogueNode convStartNode)
			{
				this.StopConversation(convStartNode.conversation);
			}, "Stopped");
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002C1C File Offset: 0x00000E1C
		private string[] DoSomethingToAllConversationsContaining(string pPartialName, Predicate<DialogueNode> pPred, Action<ConversationStartDialogueNode> pAction, string pDescription)
		{
			List<ConversationStartDialogueNode> allConversationsWithNameContaining = this.GetAllConversationsWithNameContaining(pPartialName, pPred);
			allConversationsWithNameContaining.ForEach(pAction);
			string[] array = allConversationsWithNameContaining.ConvertAll<string>((ConversationStartDialogueNode o) => o.conversation).ToArray();
			if (allConversationsWithNameContaining.Count > 0)
			{
				this.logger.Log(string.Concat(new object[]
				{
					pDescription,
					" ",
					allConversationsWithNameContaining.Count,
					" conversations with partial name ",
					pPartialName,
					": ",
					string.Join(", ", array)
				}));
			}
			return array;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002CC4 File Offset: 0x00000EC4
		private List<ConversationStartDialogueNode> GetAllConversationsWithNameContaining(string pPartialName, Predicate<DialogueNode> pPred)
		{
			List<ConversationStartDialogueNode> list = new List<ConversationStartDialogueNode>();
			foreach (DialogueNode dialogueNode in this._dialogueNodes)
			{
				if (dialogueNode is ConversationStartDialogueNode && dialogueNode.conversation.Contains(pPartialName) && pPred(dialogueNode))
				{
					list.Add(dialogueNode as ConversationStartDialogueNode);
				}
			}
			return list;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002D60 File Offset: 0x00000F60
		public string[] GetNamesOfAllStoppedConversationsWithNameContaining(string pPartialName)
		{
			return this.GetAllConversationsWithNameContaining(pPartialName, (DialogueNode o) => !this.ConversationIsRunning(o.conversation)).ConvertAll<string>((ConversationStartDialogueNode o) => o.conversation).ToArray();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public void StopConversation(string pConversation)
		{
			this.logger.Log("Stopping conversation '" + pConversation + "'");
			List<DialogueNode> nodesForConversation = this.GetNodesForConversation(pConversation);
			foreach (DialogueNode dialogueNode in nodesForConversation)
			{
				if (dialogueNode.isOn)
				{
					dialogueNode.Stop();
				}
				IRegisteredDialogueNode registeredDialogueNode = dialogueNode as IRegisteredDialogueNode;
				if (registeredDialogueNode != null)
				{
					registeredDialogueNode.isListening = false;
					dialogueNode.Stop();
				}
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002E54 File Offset: 0x00001054
		public void RemoveConversation(string pConversation)
		{
			List<DialogueNode> nodesForConversation = this.GetNodesForConversation(pConversation);
			foreach (DialogueNode dialogueNode in nodesForConversation)
			{
				this._nodesToTurnOff.Add(dialogueNode);
				this._dialogueNodes.Remove(dialogueNode);
				if (dialogueNode is IRegisteredDialogueNode)
				{
					IRegisteredDialogueNode registeredDialogueNode = dialogueNode as IRegisteredDialogueNode;
					this._registeredDialogueNodes.Remove(registeredDialogueNode);
				}
				this._dialogueTable.RemoveRowAt(dialogueNode.objectId);
			}
			if (this._nodesForConversation.ContainsKey(pConversation))
			{
				this._nodesForConversation.Remove(pConversation);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002F20 File Offset: 0x00001120
		public bool HasConversation(string pConversation)
		{
			int num = 0;
			foreach (DialogueNode dialogueNode in this._dialogueNodes)
			{
				if (dialogueNode.conversation == pConversation)
				{
					num++;
				}
			}
			return num > 0;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002F9C File Offset: 0x0000119C
		public void AddOnSomeoneSaidSomethingListener(DialogueRunner.OnSomeoneSaidSomething pOnSomeoneSaidSomething)
		{
			this._onSomeoneSaidSomething += pOnSomeoneSaidSomething;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002FA8 File Offset: 0x000011A8
		public void RemoveOnSomeoneSaidSomethingListener(DialogueRunner.OnSomeoneSaidSomething pOnSomeoneSaidSomething)
		{
			this._onSomeoneSaidSomething -= pOnSomeoneSaidSomething;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002FB4 File Offset: 0x000011B4
		internal void SomeoneSaidSomething(Speech pSpeech)
		{
			D.isNull(pSpeech);
			if (this._onSomeoneSaidSomething != null)
			{
				this._onSomeoneSaidSomething(pSpeech);
			}
			else
			{
				this.logger.Log("No listeners to dialogue runner");
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002FF0 File Offset: 0x000011F0
		public void AddExpression(string pName, DialogueRunner.Expression pExpression)
		{
			this._expressions[pName] = pExpression;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003000 File Offset: 0x00001200
		public bool HasExpression(string pName)
		{
			return this._expressions.ContainsKey(pName);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003010 File Offset: 0x00001210
		public bool EvaluateExpression(string pExpressionName, string[] args)
		{
			if (!this._expressions.ContainsKey(pExpressionName))
			{
				string text = "Can't find expression '" + pExpressionName + "' in Dialogue Runner";
				D.Log("ERROR: " + text);
				if (this.onGrimmError != null)
				{
					this.onGrimmError(text);
				}
			}
			DialogueRunner.Expression expression = this._expressions[pExpressionName];
			bool flag = expression(args);
			if (flag)
			{
			}
			return flag;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003088 File Offset: 0x00001288
		public string GetExpressionsAsString()
		{
			string[] array = new string[this._expressions.Count];
			int num = 0;
			foreach (string text in this._expressions.Keys)
			{
				array[num++] = text;
			}
			return string.Join(", ", array);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003114 File Offset: 0x00001314
		public void AddFunction(string pName, DialogueRunner.Function pFunction)
		{
			this._functions[pName] = pFunction;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003124 File Offset: 0x00001324
		public bool HasFunction(string pName)
		{
			return this._functions.ContainsKey(pName);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003134 File Offset: 0x00001334
		public void CallFunction(string pFunctionName, string[] args)
		{
			if (this._functions.ContainsKey(pFunctionName))
			{
				DialogueRunner.Function function = this._functions[pFunctionName];
				function(args);
			}
			else
			{
				string text = "Can't find function '" + pFunctionName + "' in Dialogue Runner";
				D.Log("ERROR! " + text);
				if (this.onGrimmError != null)
				{
					this.onGrimmError(text);
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000031A4 File Offset: 0x000013A4
		public string GetFunctionsAsString()
		{
			string[] array = new string[this._functions.Count];
			int num = 0;
			foreach (string text in this._functions.Keys)
			{
				array[num++] = text;
			}
			return string.Join(", ", array);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003230 File Offset: 0x00001430
		public void RunStringAsFunction(string pCommand)
		{
			this.RemoveConversation("CMD");
			DialogueScriptLoader dialogueScriptLoader = new DialogueScriptLoader(this);
			dialogueScriptLoader.CreateDialogueNodesFromString(pCommand, "CMD");
			this.StartConversation("CMD");
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003268 File Offset: 0x00001468
		public void AddOnEventListener(DialogueRunner.OnEvent pOnEvent)
		{
			this._onEvent += pOnEvent;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003274 File Offset: 0x00001474
		public void RemoveOnEventListener(DialogueRunner.OnEvent pOnEvent)
		{
			this._onEvent -= pOnEvent;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003280 File Offset: 0x00001480
		public void EventHappened(string pEventName)
		{
			foreach (IRegisteredDialogueNode registeredDialogueNode in this._registeredDialogueNodes.ToArray())
			{
				if (registeredDialogueNode.isListening && registeredDialogueNode.eventName == pEventName)
				{
					registeredDialogueNode.EventHappened();
				}
			}
			if (this._onEvent != null)
			{
				this._onEvent(pEventName);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000032EC File Offset: 0x000014EC
		public void ScopeEnded(string pConversation, string pScopeNode)
		{
			this.logger.Log(string.Concat(new string[] { "Scope '", pScopeNode, "' for conversation '", pConversation, "' ended" }));
			foreach (IRegisteredDialogueNode registeredDialogueNode in this._registeredDialogueNodes)
			{
				if (registeredDialogueNode.conversation == pConversation && registeredDialogueNode.ScopeNode() == pScopeNode)
				{
					if (registeredDialogueNode.isListening)
					{
						registeredDialogueNode.isListening = false;
					}
				}
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000033C4 File Offset: 0x000015C4
		public void CancelRegisteredNode(string pConversation, string pListenerHandle)
		{
			foreach (IRegisteredDialogueNode registeredDialogueNode in this._registeredDialogueNodes)
			{
				if (registeredDialogueNode.conversation == pConversation && registeredDialogueNode.handle == pListenerHandle)
				{
					this.logger.Log("Cancelled node " + registeredDialogueNode.name + " in conversation " + pConversation);
					registeredDialogueNode.isListening = false;
					if (registeredDialogueNode is ListeningDialogueNode)
					{
						ListeningDialogueNode listeningDialogueNode = registeredDialogueNode as ListeningDialogueNode;
						this.ScopeEnded(pConversation, listeningDialogueNode.name);
					}
					else if (registeredDialogueNode is WaitDialogueNode)
					{
						WaitDialogueNode waitDialogueNode = registeredDialogueNode as WaitDialogueNode;
						this.ScopeEnded(pConversation, waitDialogueNode.name);
					}
				}
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000034B4 File Offset: 0x000016B4
		public bool IsWaitingOnEvent(string pEventName)
		{
			foreach (IRegisteredDialogueNode registeredDialogueNode in this._registeredDialogueNodes)
			{
				if (registeredDialogueNode.isListening && registeredDialogueNode.eventName == pEventName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000353C File Offset: 0x0000173C
		public void AddFocusConversationListener(DialogueRunner.OnFocusConversation pOnFocusConversation)
		{
			this._onFocusConversation += pOnFocusConversation;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003548 File Offset: 0x00001748
		public void RemoveFocusConversationListener(DialogueRunner.OnFocusConversation pOnFocusConversation)
		{
			this._onFocusConversation -= pOnFocusConversation;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003554 File Offset: 0x00001754
		public void AddDefocusConversationListener(DialogueRunner.OnFocusConversation pOnDefocusConversation)
		{
			this._onDefocusConversation += pOnDefocusConversation;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003560 File Offset: 0x00001760
		public void RemoveDefocusConversationListener(DialogueRunner.OnFocusConversation pOnDefocusConversation)
		{
			this._onDefocusConversation -= pOnDefocusConversation;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000356C File Offset: 0x0000176C
		public void FocusConversation(string pConversation)
		{
			if (this._onFocusConversation != null)
			{
				this._onFocusConversation(pConversation);
				return;
			}
			throw new GrimmException("Trying to focus conversation " + pConversation + " but there is no onFocusConversation listener.");
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000035AC File Offset: 0x000017AC
		public void DefocusConversation(string pConversation)
		{
			if (this._onDefocusConversation != null)
			{
				this._onDefocusConversation(pConversation);
				this.EventHappened("Defocused_" + pConversation);
			}
			else
			{
				D.Log("Trying to defocus conversation " + pConversation + " but there is no onDefocusConversation listener.");
			}
		}

		// Token: 0x17000001 RID: 1
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000035FC File Offset: 0x000017FC
		public Language language
		{
			set
			{
				this._language = value;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003608 File Offset: 0x00001808
		private string RegisteredNodesAsString()
		{
			List<IRegisteredDialogueNode> registeredDialogueNodes = this.GetRegisteredDialogueNodes();
			List<string> list = new List<string>();
			foreach (IRegisteredDialogueNode registeredDialogueNode in registeredDialogueNodes)
			{
				list.Add(string.Concat(new string[]
				{
					"[",
					registeredDialogueNode.name,
					" in '",
					registeredDialogueNode.conversation,
					"' ",
					(!registeredDialogueNode.isListening) ? "NOT listening" : "LISTENING",
					"]"
				}));
			}
			return "Registered nodes: " + string.Join(", ", list.ToArray());
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000036EC File Offset: 0x000018EC
		public override string ToString()
		{
			return string.Format("DialogueRunner ({0} dialogue nodes, {1} registered dialogue nodes)", this._dialogueNodes.Count, this._registeredDialogueNodes.Count);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003724 File Offset: 0x00001924
		public string GetAllDialogueAsString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (DialogueNode dialogueNode in this._dialogueNodes)
			{
				if (dialogueNode is TimedDialogueNode)
				{
					stringBuilder.Append((dialogueNode as TimedDialogueNode).line + " ");
					if (Randomizer.OneIn(10))
					{
						stringBuilder.AppendLine("");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000037D0 File Offset: 0x000019D0
		public HashSet<string> GetActiveConversations()
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (DialogueNode dialogueNode in this._dialogueNodes)
			{
				if (dialogueNode.isOn)
				{
					hashSet.Add(dialogueNode.conversation);
				}
			}
			return hashSet;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003850 File Offset: 0x00001A50
		private void RegisterBuiltInAPIExpressions()
		{
			this.AddExpression("IsActive", new DialogueRunner.Expression(this.IsActive));
			this.AddFunction("StartAll", new DialogueRunner.Function(this.StartAll));
			this.AddFunction("StopAll", new DialogueRunner.Function(this.StopAll));
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000038A4 File Offset: 0x00001AA4
		private bool IsActive(string[] args)
		{
			return this.ConversationIsRunning(args[0]);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000038B0 File Offset: 0x00001AB0
		private void StartAll(string[] args)
		{
			this.StartAllConversationsContaining(args[0]);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000038BC File Offset: 0x00001ABC
		private void StopAll(string[] args)
		{
			this.StopAllConversationsContaining(args[0]);
		}

		// Token: 0x04000001 RID: 1
		private const string runStringAsFunctionCMD = "CMD";

		// Token: 0x04000002 RID: 2
		public Logger logger = new Logger();

		// Token: 0x04000003 RID: 3
		private TableTwo _dialogueTable;

		// Token: 0x04000004 RID: 4
		private Language _language;

		// Token: 0x04000005 RID: 5
		private List<DialogueNode> _dialogueNodes;

		// Token: 0x04000006 RID: 6
		private Dictionary<string, List<DialogueNode>> _nodesForConversation = new Dictionary<string, List<DialogueNode>>();

		// Token: 0x04000007 RID: 7
		private Dictionary<string, DialogueRunner.Expression> _expressions = new Dictionary<string, DialogueRunner.Expression>();

		// Token: 0x04000008 RID: 8
		private Dictionary<string, DialogueRunner.Function> _functions = new Dictionary<string, DialogueRunner.Function>();

		// Token: 0x04000009 RID: 9
		private List<IRegisteredDialogueNode> _registeredDialogueNodes = new List<IRegisteredDialogueNode>();

		// Token: 0x0400000A RID: 10
		public Action<string> onGrimmError;

		// Token: 0x0400000B RID: 11
		private List<DialogueNode> _nodesThatAreOn = new List<DialogueNode>();

		// Token: 0x0400000C RID: 12
		private List<DialogueNode> _nodesToTurnOn = new List<DialogueNode>();

		// Token: 0x0400000D RID: 13
		private List<DialogueNode> _nodesToTurnOff = new List<DialogueNode>();

		// Token: 0x02000005 RID: 5
		// (Invoke) Token: 0x06000053 RID: 83
		public delegate void OnSomeoneSaidSomething(Speech pSpeech);

		// Token: 0x02000006 RID: 6
		// (Invoke) Token: 0x06000057 RID: 87
		public delegate bool Expression(string[] args);

		// Token: 0x02000007 RID: 7
		// (Invoke) Token: 0x0600005B RID: 91
		public delegate void Function(string[] args);

		// Token: 0x02000008 RID: 8
		// (Invoke) Token: 0x0600005F RID: 95
		public delegate void OnFocusConversation(string pConversation);

		// Token: 0x02000009 RID: 9
		// (Invoke) Token: 0x06000063 RID: 99
		public delegate void OnEvent(string pEventName);
	}
}
