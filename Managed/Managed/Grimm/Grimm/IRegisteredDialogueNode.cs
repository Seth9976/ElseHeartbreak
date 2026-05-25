using System;

namespace GrimmLib
{
	// Token: 0x02000026 RID: 38
	public interface IRegisteredDialogueNode
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600016F RID: 367
		// (set) Token: 0x06000170 RID: 368
		string handle { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000171 RID: 369
		// (set) Token: 0x06000172 RID: 370
		string conversation { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000173 RID: 371
		// (set) Token: 0x06000174 RID: 372
		string name { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000175 RID: 373
		// (set) Token: 0x06000176 RID: 374
		bool isListening { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000177 RID: 375
		// (set) Token: 0x06000178 RID: 376
		string eventName { get; set; }

		// Token: 0x06000179 RID: 377
		void EventHappened();

		// Token: 0x0600017A RID: 378
		string ScopeNode();
	}
}
