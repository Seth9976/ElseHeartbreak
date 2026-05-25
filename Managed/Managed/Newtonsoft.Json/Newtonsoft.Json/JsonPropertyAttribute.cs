using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000059 RID: 89
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class JsonPropertyAttribute : Attribute
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000DDF8 File Offset: 0x0000BFF8
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000DE1E File Offset: 0x0000C01E
		public NullValueHandling NullValueHandling
		{
			get
			{
				NullValueHandling? nullValueHandling = this._nullValueHandling;
				if (nullValueHandling == null)
				{
					return NullValueHandling.Include;
				}
				return nullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._nullValueHandling = new NullValueHandling?(value);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000DE2C File Offset: 0x0000C02C
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0000DE52 File Offset: 0x0000C052
		public DefaultValueHandling DefaultValueHandling
		{
			get
			{
				DefaultValueHandling? defaultValueHandling = this._defaultValueHandling;
				if (defaultValueHandling == null)
				{
					return DefaultValueHandling.Include;
				}
				return defaultValueHandling.GetValueOrDefault();
			}
			set
			{
				this._defaultValueHandling = new DefaultValueHandling?(value);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000DE60 File Offset: 0x0000C060
		// (set) Token: 0x06000385 RID: 901 RVA: 0x0000DE86 File Offset: 0x0000C086
		public ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				ReferenceLoopHandling? referenceLoopHandling = this._referenceLoopHandling;
				if (referenceLoopHandling == null)
				{
					return ReferenceLoopHandling.Error;
				}
				return referenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._referenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000DE94 File Offset: 0x0000C094
		// (set) Token: 0x06000387 RID: 903 RVA: 0x0000DEBA File Offset: 0x0000C0BA
		public ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				ObjectCreationHandling? objectCreationHandling = this._objectCreationHandling;
				if (objectCreationHandling == null)
				{
					return ObjectCreationHandling.Auto;
				}
				return objectCreationHandling.GetValueOrDefault();
			}
			set
			{
				this._objectCreationHandling = new ObjectCreationHandling?(value);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000DEC8 File Offset: 0x0000C0C8
		// (set) Token: 0x06000389 RID: 905 RVA: 0x0000DEEE File Offset: 0x0000C0EE
		public TypeNameHandling TypeNameHandling
		{
			get
			{
				TypeNameHandling? typeNameHandling = this._typeNameHandling;
				if (typeNameHandling == null)
				{
					return TypeNameHandling.None;
				}
				return typeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000DEFC File Offset: 0x0000C0FC
		// (set) Token: 0x0600038B RID: 907 RVA: 0x0000DF22 File Offset: 0x0000C122
		public bool IsReference
		{
			get
			{
				return this._isReference ?? false;
			}
			set
			{
				this._isReference = new bool?(value);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000DF30 File Offset: 0x0000C130
		// (set) Token: 0x0600038D RID: 909 RVA: 0x0000DF56 File Offset: 0x0000C156
		public int Order
		{
			get
			{
				int? order = this._order;
				if (order == null)
				{
					return 0;
				}
				return order.GetValueOrDefault();
			}
			set
			{
				this._order = new int?(value);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000DF64 File Offset: 0x0000C164
		// (set) Token: 0x0600038F RID: 911 RVA: 0x0000DF6C File Offset: 0x0000C16C
		public string PropertyName { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000DF75 File Offset: 0x0000C175
		// (set) Token: 0x06000391 RID: 913 RVA: 0x0000DF7D File Offset: 0x0000C17D
		public Required Required { get; set; }

		// Token: 0x06000392 RID: 914 RVA: 0x0000DF86 File Offset: 0x0000C186
		public JsonPropertyAttribute()
		{
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000DF8E File Offset: 0x0000C18E
		public JsonPropertyAttribute(string propertyName)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x04000113 RID: 275
		internal NullValueHandling? _nullValueHandling;

		// Token: 0x04000114 RID: 276
		internal DefaultValueHandling? _defaultValueHandling;

		// Token: 0x04000115 RID: 277
		internal ReferenceLoopHandling? _referenceLoopHandling;

		// Token: 0x04000116 RID: 278
		internal ObjectCreationHandling? _objectCreationHandling;

		// Token: 0x04000117 RID: 279
		internal TypeNameHandling? _typeNameHandling;

		// Token: 0x04000118 RID: 280
		internal bool? _isReference;

		// Token: 0x04000119 RID: 281
		internal int? _order;
	}
}
