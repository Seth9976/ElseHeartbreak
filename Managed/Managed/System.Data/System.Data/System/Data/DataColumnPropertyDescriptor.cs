using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x02000020 RID: 32
	internal class DataColumnPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06000185 RID: 389 RVA: 0x0000BE54 File Offset: 0x0000A054
		public DataColumnPropertyDescriptor(string name, int columnIndex, Attribute[] attrs)
			: base(name, attrs)
		{
			this.columnIndex = columnIndex;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000BE74 File Offset: 0x0000A074
		public DataColumnPropertyDescriptor(DataColumn dc)
			: base(dc.ColumnName, null)
		{
			this.columnIndex = dc.Ordinal;
			this.componentType = typeof(DataRowView);
			this.propertyType = dc.DataType;
			this.readOnly = dc.ReadOnly;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000BED0 File Offset: 0x0000A0D0
		public void SetReadOnly(bool value)
		{
			this.readOnly = value;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		public void SetComponentType(Type type)
		{
			this.componentType = type;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000BEE8 File Offset: 0x0000A0E8
		public void SetPropertyType(Type type)
		{
			this.propertyType = type;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
		public void SetBrowsable(bool browsable)
		{
			this.browsable = browsable;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000BF00 File Offset: 0x0000A100
		public override object GetValue(object component)
		{
			if (this.componentType == typeof(DataRowView) && component is DataRowView)
			{
				DataRowView dataRowView = (DataRowView)component;
				return dataRowView[base.Name];
			}
			if (this.componentType == typeof(DbDataRecord) && component is DbDataRecord)
			{
				DbDataRecord dbDataRecord = (DbDataRecord)component;
				return dbDataRecord[this.columnIndex];
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000BF7C File Offset: 0x0000A17C
		public override void SetValue(object component, object value)
		{
			DataRowView dataRowView = (DataRowView)component;
			dataRowView[base.Name] = value;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		[MonoTODO]
		public override void ResetValue(object component)
		{
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000BFA4 File Offset: 0x0000A1A4
		[MonoTODO]
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000BFA8 File Offset: 0x0000A1A8
		[MonoTODO]
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000BFAC File Offset: 0x0000A1AC
		public override Type ComponentType
		{
			get
			{
				return this.componentType;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
		public override bool IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		public override bool IsBrowsable
		{
			get
			{
				return this.browsable && base.IsBrowsable;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000BFD4 File Offset: 0x0000A1D4
		public override Type PropertyType
		{
			get
			{
				return this.propertyType;
			}
		}

		// Token: 0x040000C8 RID: 200
		private bool readOnly = true;

		// Token: 0x040000C9 RID: 201
		private Type componentType;

		// Token: 0x040000CA RID: 202
		private Type propertyType;

		// Token: 0x040000CB RID: 203
		private bool browsable = true;

		// Token: 0x040000CC RID: 204
		private int columnIndex;
	}
}
