using System;

namespace GrimmLib
{
	// Token: 0x0200000A RID: 10
	public struct Speech
	{
		// Token: 0x06000066 RID: 102 RVA: 0x000039F0 File Offset: 0x00001BF0
		public Speech(string pConversation, string pDialogueNodeName, string pSpeaker, string pLine)
		{
			this.conversation = pConversation;
			this.dialogueNodeName = pDialogueNodeName;
			this.speaker = pSpeaker;
			this.line = pLine;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003A10 File Offset: 0x00001C10
		public override string ToString()
		{
			return string.Format("TalkEventInfo conversation = '{0}', dialogueNodeName = '{1}', talker = '{2}', line = '{3}'", new object[] { this.conversation, this.dialogueNodeName, this.speaker, this.line });
		}

		// Token: 0x04000015 RID: 21
		public string conversation;

		// Token: 0x04000016 RID: 22
		public string dialogueNodeName;

		// Token: 0x04000017 RID: 23
		public string speaker;

		// Token: 0x04000018 RID: 24
		public string line;
	}
}
