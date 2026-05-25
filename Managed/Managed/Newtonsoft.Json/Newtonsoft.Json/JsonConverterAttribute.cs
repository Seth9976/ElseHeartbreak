using System;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x0200003A RID: 58
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class JsonConverterAttribute : Attribute
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00008C54 File Offset: 0x00006E54
		public Type ConverterType
		{
			get
			{
				return this._converterType;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00008C5C File Offset: 0x00006E5C
		public JsonConverterAttribute(Type converterType)
		{
			if (converterType == null)
			{
				throw new ArgumentNullException("converterType");
			}
			this._converterType = converterType;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00008C7C File Offset: 0x00006E7C
		internal static JsonConverter CreateJsonConverterInstance(Type converterType)
		{
			JsonConverter jsonConverter;
			try
			{
				jsonConverter = (JsonConverter)Activator.CreateInstance(converterType);
			}
			catch (Exception ex)
			{
				throw new Exception("Error creating {0}".FormatWith(CultureInfo.InvariantCulture, new object[] { converterType }), ex);
			}
			return jsonConverter;
		}

		// Token: 0x040000B4 RID: 180
		private readonly Type _converterType;
	}
}
