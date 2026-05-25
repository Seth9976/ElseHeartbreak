using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000061 RID: 97
	public class JsonSerializer
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000416 RID: 1046 RVA: 0x0000F074 File Offset: 0x0000D274
		// (remove) Token: 0x06000417 RID: 1047 RVA: 0x0000F0AC File Offset: 0x0000D2AC
		public virtual event EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs> Error;

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000F0E1 File Offset: 0x0000D2E1
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x0000F0FC File Offset: 0x0000D2FC
		public virtual IReferenceResolver ReferenceResolver
		{
			get
			{
				if (this._referenceResolver == null)
				{
					this._referenceResolver = new DefaultReferenceResolver();
				}
				return this._referenceResolver;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Reference resolver cannot be null.");
				}
				this._referenceResolver = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000F118 File Offset: 0x0000D318
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x0000F120 File Offset: 0x0000D320
		public virtual SerializationBinder Binder
		{
			get
			{
				return this._binder;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Serialization binder cannot be null.");
				}
				this._binder = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000F13C File Offset: 0x0000D33C
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x0000F144 File Offset: 0x0000D344
		public virtual TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._typeNameHandling;
			}
			set
			{
				if (value < TypeNameHandling.None || value > TypeNameHandling.Auto)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._typeNameHandling = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000F160 File Offset: 0x0000D360
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x0000F168 File Offset: 0x0000D368
		public virtual FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return this._typeNameAssemblyFormat;
			}
			set
			{
				if (value < FormatterAssemblyStyle.Simple || value > FormatterAssemblyStyle.Full)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._typeNameAssemblyFormat = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000F184 File Offset: 0x0000D384
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x0000F18C File Offset: 0x0000D38C
		public virtual PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._preserveReferencesHandling;
			}
			set
			{
				if (value < PreserveReferencesHandling.None || value > PreserveReferencesHandling.All)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._preserveReferencesHandling = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000F1A8 File Offset: 0x0000D3A8
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x0000F1B0 File Offset: 0x0000D3B0
		public virtual ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._referenceLoopHandling;
			}
			set
			{
				if (value < ReferenceLoopHandling.Error || value > ReferenceLoopHandling.Serialize)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._referenceLoopHandling = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000F1CC File Offset: 0x0000D3CC
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
		public virtual MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._missingMemberHandling;
			}
			set
			{
				if (value < MissingMemberHandling.Ignore || value > MissingMemberHandling.Error)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._missingMemberHandling = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000F1F0 File Offset: 0x0000D3F0
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x0000F1F8 File Offset: 0x0000D3F8
		public virtual NullValueHandling NullValueHandling
		{
			get
			{
				return this._nullValueHandling;
			}
			set
			{
				if (value < NullValueHandling.Include || value > NullValueHandling.Ignore)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._nullValueHandling = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000F214 File Offset: 0x0000D414
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000F21C File Offset: 0x0000D41C
		public virtual DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._defaultValueHandling;
			}
			set
			{
				if (value < DefaultValueHandling.Include || value > DefaultValueHandling.IgnoreAndPopulate)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._defaultValueHandling = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000F238 File Offset: 0x0000D438
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000F240 File Offset: 0x0000D440
		public virtual ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._objectCreationHandling;
			}
			set
			{
				if (value < ObjectCreationHandling.Auto || value > ObjectCreationHandling.Replace)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._objectCreationHandling = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000F25C File Offset: 0x0000D45C
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0000F264 File Offset: 0x0000D464
		public virtual ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._constructorHandling;
			}
			set
			{
				if (value < ConstructorHandling.Default || value > ConstructorHandling.AllowNonPublicDefaultConstructor)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._constructorHandling = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000F280 File Offset: 0x0000D480
		public virtual JsonConverterCollection Converters
		{
			get
			{
				if (this._converters == null)
				{
					this._converters = new JsonConverterCollection();
				}
				return this._converters;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000F29B File Offset: 0x0000D49B
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x0000F2B6 File Offset: 0x0000D4B6
		public virtual IContractResolver ContractResolver
		{
			get
			{
				if (this._contractResolver == null)
				{
					this._contractResolver = DefaultContractResolver.Instance;
				}
				return this._contractResolver;
			}
			set
			{
				this._contractResolver = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000F2BF File Offset: 0x0000D4BF
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0000F2C7 File Offset: 0x0000D4C7
		public virtual StreamingContext Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000F2D0 File Offset: 0x0000D4D0
		public JsonSerializer()
		{
			this._referenceLoopHandling = ReferenceLoopHandling.Error;
			this._missingMemberHandling = MissingMemberHandling.Ignore;
			this._nullValueHandling = NullValueHandling.Include;
			this._defaultValueHandling = DefaultValueHandling.Include;
			this._objectCreationHandling = ObjectCreationHandling.Auto;
			this._preserveReferencesHandling = PreserveReferencesHandling.None;
			this._constructorHandling = ConstructorHandling.Default;
			this._typeNameHandling = TypeNameHandling.None;
			this._context = JsonSerializerSettings.DefaultContext;
			this._binder = DefaultSerializationBinder.Instance;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000F334 File Offset: 0x0000D534
		public static JsonSerializer Create(JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = new JsonSerializer();
			if (settings != null)
			{
				if (!CollectionUtils.IsNullOrEmpty<JsonConverter>(settings.Converters))
				{
					jsonSerializer.Converters.AddRange(settings.Converters);
				}
				jsonSerializer.TypeNameHandling = settings.TypeNameHandling;
				jsonSerializer.TypeNameAssemblyFormat = settings.TypeNameAssemblyFormat;
				jsonSerializer.PreserveReferencesHandling = settings.PreserveReferencesHandling;
				jsonSerializer.ReferenceLoopHandling = settings.ReferenceLoopHandling;
				jsonSerializer.MissingMemberHandling = settings.MissingMemberHandling;
				jsonSerializer.ObjectCreationHandling = settings.ObjectCreationHandling;
				jsonSerializer.NullValueHandling = settings.NullValueHandling;
				jsonSerializer.DefaultValueHandling = settings.DefaultValueHandling;
				jsonSerializer.ConstructorHandling = settings.ConstructorHandling;
				jsonSerializer.Context = settings.Context;
				if (settings.Error != null)
				{
					jsonSerializer.Error += settings.Error;
				}
				if (settings.ContractResolver != null)
				{
					jsonSerializer.ContractResolver = settings.ContractResolver;
				}
				if (settings.ReferenceResolver != null)
				{
					jsonSerializer.ReferenceResolver = settings.ReferenceResolver;
				}
				if (settings.Binder != null)
				{
					jsonSerializer.Binder = settings.Binder;
				}
			}
			return jsonSerializer;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000F434 File Offset: 0x0000D634
		public void Populate(TextReader reader, object target)
		{
			this.Populate(new JsonTextReader(reader), target);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000F443 File Offset: 0x0000D643
		public void Populate(JsonReader reader, object target)
		{
			this.PopulateInternal(reader, target);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000F450 File Offset: 0x0000D650
		internal virtual void PopulateInternal(JsonReader reader, object target)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(target, "target");
			JsonSerializerInternalReader jsonSerializerInternalReader = new JsonSerializerInternalReader(this);
			jsonSerializerInternalReader.Populate(reader, target);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000F482 File Offset: 0x0000D682
		public object Deserialize(JsonReader reader)
		{
			return this.Deserialize(reader, null);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000F48C File Offset: 0x0000D68C
		public object Deserialize(TextReader reader, Type objectType)
		{
			return this.Deserialize(new JsonTextReader(reader), objectType);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000F49B File Offset: 0x0000D69B
		public T Deserialize<T>(JsonReader reader)
		{
			return (T)((object)this.Deserialize(reader, typeof(T)));
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000F4B3 File Offset: 0x0000D6B3
		public object Deserialize(JsonReader reader, Type objectType)
		{
			return this.DeserializeInternal(reader, objectType);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		internal virtual object DeserializeInternal(JsonReader reader, Type objectType)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			JsonSerializerInternalReader jsonSerializerInternalReader = new JsonSerializerInternalReader(this);
			return jsonSerializerInternalReader.Deserialize(reader, objectType);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000F4E7 File Offset: 0x0000D6E7
		public void Serialize(TextWriter textWriter, object value)
		{
			this.Serialize(new JsonTextWriter(textWriter), value);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000F4F6 File Offset: 0x0000D6F6
		public void Serialize(JsonWriter jsonWriter, object value)
		{
			this.SerializeInternal(jsonWriter, value);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000F500 File Offset: 0x0000D700
		internal virtual void SerializeInternal(JsonWriter jsonWriter, object value)
		{
			ValidationUtils.ArgumentNotNull(jsonWriter, "jsonWriter");
			JsonSerializerInternalWriter jsonSerializerInternalWriter = new JsonSerializerInternalWriter(this);
			jsonSerializerInternalWriter.Serialize(jsonWriter, value);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000F527 File Offset: 0x0000D727
		internal JsonConverter GetMatchingConverter(Type type)
		{
			return JsonSerializer.GetMatchingConverter(this._converters, type);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000F538 File Offset: 0x0000D738
		internal static JsonConverter GetMatchingConverter(IList<JsonConverter> converters, Type objectType)
		{
			ValidationUtils.ArgumentNotNull(objectType, "objectType");
			if (converters != null)
			{
				for (int i = 0; i < converters.Count; i++)
				{
					JsonConverter jsonConverter = converters[i];
					if (jsonConverter.CanConvert(objectType))
					{
						return jsonConverter;
					}
				}
			}
			return null;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000F578 File Offset: 0x0000D778
		internal void OnError(Newtonsoft.Json.Serialization.ErrorEventArgs e)
		{
			EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs> error = this.Error;
			if (error != null)
			{
				error(this, e);
			}
		}

		// Token: 0x0400012C RID: 300
		private TypeNameHandling _typeNameHandling;

		// Token: 0x0400012D RID: 301
		private FormatterAssemblyStyle _typeNameAssemblyFormat;

		// Token: 0x0400012E RID: 302
		private PreserveReferencesHandling _preserveReferencesHandling;

		// Token: 0x0400012F RID: 303
		private ReferenceLoopHandling _referenceLoopHandling;

		// Token: 0x04000130 RID: 304
		private MissingMemberHandling _missingMemberHandling;

		// Token: 0x04000131 RID: 305
		private ObjectCreationHandling _objectCreationHandling;

		// Token: 0x04000132 RID: 306
		private NullValueHandling _nullValueHandling;

		// Token: 0x04000133 RID: 307
		private DefaultValueHandling _defaultValueHandling;

		// Token: 0x04000134 RID: 308
		private ConstructorHandling _constructorHandling;

		// Token: 0x04000135 RID: 309
		private JsonConverterCollection _converters;

		// Token: 0x04000136 RID: 310
		private IContractResolver _contractResolver;

		// Token: 0x04000137 RID: 311
		private IReferenceResolver _referenceResolver;

		// Token: 0x04000138 RID: 312
		private SerializationBinder _binder;

		// Token: 0x04000139 RID: 313
		private StreamingContext _context;
	}
}
