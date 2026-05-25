using System;
using GameTypes;

namespace RelayLib
{
	// Token: 0x02000010 RID: 16
	public abstract class RelayObjectTwo
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00004434 File Offset: 0x00002634
		// (set) Token: 0x0600007C RID: 124 RVA: 0x0000443C File Offset: 0x0000263C
		public TableTwo table { get; private set; }

		// Token: 0x0600007D RID: 125 RVA: 0x00004448 File Offset: 0x00002648
		public void LoadFromExistingRelayEntry(TableTwo pTable, int pObjectID, string pClassName)
		{
			this.table = pTable;
			this.objectId = pObjectID;
			D.assert(this.table.ContainsRow(this.objectId), "object id does not exist! " + this.objectId);
			D.assert(this.table.GetValue<string>(pObjectID, RelayObjectTwo.CSHARP_CLASS_FIELD_NAME) == pClassName, "classname missmatch!");
			this.SetupCells();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000044B8 File Offset: 0x000026B8
		public void CreateNewRelayEntry(TableTwo pTable, string pClassName)
		{
			this.table = pTable;
			this.objectId = this.table.CreateRow().row;
			this.table.EnsureField<string>(RelayObjectTwo.CSHARP_CLASS_FIELD_NAME);
			this.table.SetValue<string>(this.objectId, RelayObjectTwo.CSHARP_CLASS_FIELD_NAME, pClassName);
			this.SetupCells();
		}

		// Token: 0x0600007F RID: 127
		protected abstract void SetupCells();

		// Token: 0x06000080 RID: 128 RVA: 0x00004514 File Offset: 0x00002714
		protected ValueEntry<T> EnsureCell<T>(string pAttributeName, T pDefaultValue)
		{
			this.table.EnsureField<T>(pAttributeName);
			return this.table.GetValueEntryEnsureDefault<T>(this.objectId, pAttributeName, pDefaultValue);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004544 File Offset: 0x00002744
		public void AddDataListener<T>(string pFieldName, ValueEntry<T>.DataChangeHandler pHandler)
		{
			this.table.GetValueEntry<T>(this.objectId, pFieldName).onDataChanged += pHandler;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000456C File Offset: 0x0000276C
		public void RemoveDataListener<T>(string pFieldName, ValueEntry<T>.DataChangeHandler pHandler)
		{
			this.table.GetValueEntry<T>(this.objectId, pFieldName).onDataChanged -= pHandler;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00004594 File Offset: 0x00002794
		// (set) Token: 0x06000084 RID: 132 RVA: 0x0000459C File Offset: 0x0000279C
		public int objectId { get; protected set; }

		// Token: 0x0400001C RID: 28
		public static string CSHARP_CLASS_FIELD_NAME = "csharp-class";
	}
}
