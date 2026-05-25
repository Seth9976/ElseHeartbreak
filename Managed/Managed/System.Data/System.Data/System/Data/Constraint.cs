using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data
{
	/// <summary>Represents a constraint that can be enforced on one or more <see cref="T:System.Data.DataColumn" /> objects.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000012 RID: 18
	[TypeConverter(typeof(ConstraintConverter))]
	[DefaultProperty("ConstraintName")]
	public abstract class Constraint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Constraint" /> class.</summary>
		// Token: 0x06000033 RID: 51 RVA: 0x000039BC File Offset: 0x00001BBC
		protected Constraint()
		{
			this.dataSet = null;
			this._properties = new PropertyCollection();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000035 RID: 53 RVA: 0x000039F0 File Offset: 0x00001BF0
		// (remove) Token: 0x06000036 RID: 54 RVA: 0x00003A04 File Offset: 0x00001C04
		internal event DelegateConstraintNameChange BeforeConstraintNameChange
		{
			add
			{
				this.events.AddHandler(Constraint.beforeConstraintNameChange, value);
			}
			remove
			{
				this.events.RemoveHandler(Constraint.beforeConstraintNameChange, value);
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataSet" /> to which this constraint belongs.</summary>
		/// <returns>The <see cref="T:System.Data.DataSet" /> to which the constraint belongs.</returns>
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00003A18 File Offset: 0x00001C18
		[CLSCompliant(false)]
		protected internal virtual DataSet _DataSet
		{
			get
			{
				return this.dataSet;
			}
		}

		/// <summary>The name of a constraint in the <see cref="T:System.Data.ConstraintCollection" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.Constraint" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.Constraint" /> name is a null value or empty string. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The <see cref="T:System.Data.ConstraintCollection" /> already contains a <see cref="T:System.Data.Constraint" /> with the same name (The comparison is not case-sensitive.). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003A20 File Offset: 0x00001C20
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00003A40 File Offset: 0x00001C40
		[DefaultValue("")]
		[DataCategory("Data")]
		public virtual string ConstraintName
		{
			get
			{
				return (this._constraintName != null) ? this._constraintName : string.Empty;
			}
			set
			{
				this._onConstraintNameChange(value);
				this._constraintName = value;
			}
		}

		/// <summary>Gets the collection of user-defined constraint properties.</summary>
		/// <returns>A <see cref="T:System.Data.PropertyCollection" /> of custom information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003A50 File Offset: 0x00001C50
		[Browsable(false)]
		[DataCategory("Data")]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				return this._properties;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> to which the constraint applies.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> to which the constraint applies.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003B RID: 59
		public abstract DataTable Table { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003A58 File Offset: 0x00001C58
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00003A60 File Offset: 0x00001C60
		internal ConstraintCollection ConstraintCollection
		{
			get
			{
				return this._constraintCollection;
			}
			set
			{
				this._constraintCollection = value;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003A6C File Offset: 0x00001C6C
		private void _onConstraintNameChange(string newName)
		{
			DelegateConstraintNameChange delegateConstraintNameChange = this.events[Constraint.beforeConstraintNameChange] as DelegateConstraintNameChange;
			if (delegateConstraintNameChange != null)
			{
				delegateConstraintNameChange(this, newName);
			}
		}

		// Token: 0x0600003F RID: 63
		internal abstract void AddToConstraintCollectionSetup(ConstraintCollection collection);

		// Token: 0x06000040 RID: 64
		internal abstract bool IsConstraintViolated();

		// Token: 0x06000041 RID: 65 RVA: 0x00003AA0 File Offset: 0x00001CA0
		internal static void ThrowConstraintException()
		{
			throw new ConstraintException("Failed to enable constraints. One or more rows contain values violating non-null, unique, or foreign-key constraints.");
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003AAC File Offset: 0x00001CAC
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00003AB4 File Offset: 0x00001CB4
		internal virtual bool InitInProgress
		{
			get
			{
				return this.initInProgress;
			}
			set
			{
				this.initInProgress = value;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003AC0 File Offset: 0x00001CC0
		internal virtual void FinishInit(DataTable table)
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003AC4 File Offset: 0x00001CC4
		internal void AssertConstraint()
		{
			if (!this.IsConstraintViolated())
			{
				return;
			}
			if (this.Table._duringDataLoad || (this.Table.DataSet != null && !this.Table.DataSet.EnforceConstraints))
			{
				return;
			}
			Constraint.ThrowConstraintException();
		}

		// Token: 0x06000046 RID: 70
		internal abstract void AssertConstraint(DataRow row);

		// Token: 0x06000047 RID: 71 RVA: 0x00003B18 File Offset: 0x00001D18
		internal virtual void RollbackAssert(DataRow row)
		{
		}

		// Token: 0x06000048 RID: 72
		internal abstract void RemoveFromConstraintCollectionCleanup(ConstraintCollection collection);

		// Token: 0x06000049 RID: 73 RVA: 0x00003B1C File Offset: 0x00001D1C
		[MonoTODO]
		protected void CheckStateForProperty()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the constraint's <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> to which this constraint will belong. </param>
		// Token: 0x0600004A RID: 74 RVA: 0x00003B24 File Offset: 0x00001D24
		protected internal void SetDataSet(DataSet dataSet)
		{
			this.dataSet = dataSet;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003B30 File Offset: 0x00001D30
		internal void SetExtendedProperties(PropertyCollection properties)
		{
			this._properties = properties;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003B3C File Offset: 0x00001D3C
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00003B44 File Offset: 0x00001D44
		internal Index Index
		{
			get
			{
				return this._index;
			}
			set
			{
				if (this._index != null)
				{
					this._index.RemoveRef();
					this.Table.DropIndex(this._index);
				}
				this._index = value;
				if (this._index != null)
				{
					this._index.AddRef();
				}
			}
		}

		// Token: 0x0600004E RID: 78
		internal abstract bool IsColumnContained(DataColumn column);

		// Token: 0x0600004F RID: 79
		internal abstract bool CanRemoveFromCollection(ConstraintCollection col, bool shouldThrow);

		/// <summary>Gets the <see cref="P:System.Data.Constraint.ConstraintName" />, if there is one, as a string.</summary>
		/// <returns>The string value of the <see cref="P:System.Data.Constraint.ConstraintName" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000050 RID: 80 RVA: 0x00003B98 File Offset: 0x00001D98
		public override string ToString()
		{
			return (this._constraintName != null) ? this._constraintName : string.Empty;
		}

		// Token: 0x04000079 RID: 121
		private static readonly object beforeConstraintNameChange = new object();

		// Token: 0x0400007A RID: 122
		private EventHandlerList events = new EventHandlerList();

		// Token: 0x0400007B RID: 123
		private string _constraintName;

		// Token: 0x0400007C RID: 124
		private PropertyCollection _properties;

		// Token: 0x0400007D RID: 125
		private Index _index;

		// Token: 0x0400007E RID: 126
		private ConstraintCollection _constraintCollection;

		// Token: 0x0400007F RID: 127
		private DataSet dataSet;

		// Token: 0x04000080 RID: 128
		private bool initInProgress;
	}
}
