using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Represents a parameter to a <see cref="T:System.Data.Common.DbCommand" /> and optionally, its mapping to a <see cref="T:System.Data.DataSet" /> column. For more information on parameters, see Configuring Parameters and Parameter Data Types (ADO.NET).</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000C9 RID: 201
	public abstract class DbParameter : MarshalByRefObject, IDataParameter, IDbDataParameter
	{
		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataParameter.Precision" />.</summary>
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0002EAB4 File Offset: 0x0002CCB4
		// (set) Token: 0x060009CD RID: 2509 RVA: 0x0002EAB8 File Offset: 0x0002CCB8
		byte IDbDataParameter.Precision
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataParameter.Scale" />.</summary>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0002EABC File Offset: 0x0002CCBC
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x0002EAC0 File Offset: 0x0002CCC0
		byte IDbDataParameter.Scale
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.DbType" /> values. The default is <see cref="F:System.Data.DbType.String" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property is not set to a valid <see cref="T:System.Data.DbType" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060009D0 RID: 2512
		// (set) Token: 0x060009D1 RID: 2513
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[RefreshProperties(RefreshProperties.All)]
		public abstract DbType DbType { get; set; }

		/// <summary>Gets or sets a value that indicates whether the parameter is input-only, output-only, bidirectional, or a stored procedure return value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is Input.</returns>
		/// <exception cref="T:System.ArgumentException">The property is not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060009D2 RID: 2514
		// (set) Token: 0x060009D3 RID: 2515
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(ParameterDirection.Input)]
		public abstract ParameterDirection Direction { get; set; }

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.Common.DbParameter" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.Common.DbParameter" />. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060009D4 RID: 2516
		// (set) Token: 0x060009D5 RID: 2517
		[DefaultValue("")]
		public abstract string ParameterName { get; set; }

		/// <summary>Gets or sets the maximum size, in bytes, of the data within the column.</summary>
		/// <returns>The maximum size, in bytes, of the data within the column. The default value is inferred from the parameter value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060009D6 RID: 2518
		// (set) Token: 0x060009D7 RID: 2519
		public abstract int Size { get; set; }

		/// <summary>Gets or sets the value of the parameter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060009D8 RID: 2520
		// (set) Token: 0x060009D9 RID: 2521
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.All)]
		public abstract object Value { get; set; }

		/// <summary>Gets or sets a value that indicates whether the parameter accepts null values.</summary>
		/// <returns>true if null values are accepted; otherwise false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060009DA RID: 2522
		// (set) Token: 0x060009DB RID: 2523
		[Browsable(false)]
		[DesignOnly(true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract bool IsNullable { get; set; }

		/// <summary>Gets or sets the name of the source column mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.Common.DbParameter.Value" />.</summary>
		/// <returns>The name of the source column mapped to the <see cref="T:System.Data.DataSet" />. The default is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060009DC RID: 2524
		// (set) Token: 0x060009DD RID: 2525
		[DefaultValue("")]
		public abstract string SourceColumn { get; set; }

		/// <summary>Sets or gets a value which indicates whether the source column is nullable. This allows <see cref="T:System.Data.Common.DbCommandBuilder" /> to correctly generate Update statements for nullable columns.</summary>
		/// <returns>true if the source column is nullable; false if it is not.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060009DE RID: 2526
		// (set) Token: 0x060009DF RID: 2527
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(false)]
		public abstract bool SourceColumnNullMapping { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when you load <see cref="P:System.Data.Common.DbParameter.Value" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is Current.</returns>
		/// <exception cref="T:System.ArgumentException">The property is not set to one of the <see cref="T:System.Data.DataRowVersion" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060009E0 RID: 2528
		// (set) Token: 0x060009E1 RID: 2529
		[DefaultValue(DataRowVersion.Current)]
		public abstract DataRowVersion SourceVersion { get; set; }

		/// <summary>Resets the DbType property to its original settings.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009E2 RID: 2530
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public abstract void ResetDbType();

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0002EAC4 File Offset: 0x0002CCC4
		// (set) Token: 0x060009E4 RID: 2532 RVA: 0x0002EAC8 File Offset: 0x0002CCC8
		internal virtual object FrameworkDbType
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0002EACC File Offset: 0x0002CCCC
		// (set) Token: 0x060009E6 RID: 2534 RVA: 0x0002EAD4 File Offset: 0x0002CCD4
		protected internal static Hashtable DbTypeMapping
		{
			get
			{
				return DbParameter.dbTypeMapping;
			}
			set
			{
				DbParameter.dbTypeMapping = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0002EADC File Offset: 0x0002CCDC
		internal virtual Type SystemType
		{
			get
			{
				return (Type)DbParameter.dbTypeMapping[this.DbType];
			}
		}

		// Token: 0x0400036B RID: 875
		internal static Hashtable dbTypeMapping;
	}
}
