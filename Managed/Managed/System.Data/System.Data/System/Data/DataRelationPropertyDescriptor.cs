using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000028 RID: 40
	internal class DataRelationPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x0000D724 File Offset: 0x0000B924
		internal DataRelationPropertyDescriptor(DataRelation relation)
			: base(relation.RelationName, null)
		{
			this._relation = relation;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000D73C File Offset: 0x0000B93C
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000D748 File Offset: 0x0000B948
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000D74C File Offset: 0x0000B94C
		public override Type PropertyType
		{
			get
			{
				return typeof(IBindingList);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000D758 File Offset: 0x0000B958
		public DataRelation Relation
		{
			get
			{
				return this._relation;
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000D760 File Offset: 0x0000B960
		public override bool CanResetValue(object obj)
		{
			return false;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000D764 File Offset: 0x0000B964
		public override bool Equals(object obj)
		{
			DataRelationPropertyDescriptor dataRelationPropertyDescriptor = obj as DataRelationPropertyDescriptor;
			return dataRelationPropertyDescriptor != null && this.Relation == dataRelationPropertyDescriptor.Relation;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000D790 File Offset: 0x0000B990
		public override int GetHashCode()
		{
			return this._relation.GetHashCode();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000D7A0 File Offset: 0x0000B9A0
		public override object GetValue(object obj)
		{
			DataRowView dataRowView = (DataRowView)obj;
			return dataRowView.CreateChildView(this.Relation);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
		public override void ResetValue(object obj)
		{
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
		public override void SetValue(object obj, object val)
		{
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000D7C8 File Offset: 0x0000B9C8
		public override bool ShouldSerializeValue(object obj)
		{
			return false;
		}

		// Token: 0x040000E9 RID: 233
		private DataRelation _relation;
	}
}
