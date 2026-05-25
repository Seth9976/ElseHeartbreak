using System;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000035 RID: 53
	internal class DefaultReferenceResolver : IReferenceResolver
	{
		// Token: 0x06000228 RID: 552 RVA: 0x00008AC0 File Offset: 0x00006CC0
		private BidirectionalDictionary<string, object> GetMappings(object context)
		{
			JsonSerializerInternalBase jsonSerializerInternalBase;
			if (context is JsonSerializerInternalBase)
			{
				jsonSerializerInternalBase = (JsonSerializerInternalBase)context;
			}
			else
			{
				if (!(context is JsonSerializerProxy))
				{
					throw new Exception("The DefaultReferenceResolver can only be used internally.");
				}
				jsonSerializerInternalBase = ((JsonSerializerProxy)context).GetInternalSerializer();
			}
			return jsonSerializerInternalBase.DefaultReferenceMappings;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00008B08 File Offset: 0x00006D08
		public object ResolveReference(object context, string reference)
		{
			object obj;
			this.GetMappings(context).TryGetByFirst(reference, out obj);
			return obj;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00008B28 File Offset: 0x00006D28
		public string GetReference(object context, object value)
		{
			BidirectionalDictionary<string, object> mappings = this.GetMappings(context);
			string text;
			if (!mappings.TryGetBySecond(value, out text))
			{
				this._referenceCount++;
				text = this._referenceCount.ToString(CultureInfo.InvariantCulture);
				mappings.Add(text, value);
			}
			return text;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00008B70 File Offset: 0x00006D70
		public void AddReference(object context, string reference, object value)
		{
			this.GetMappings(context).Add(reference, value);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00008B80 File Offset: 0x00006D80
		public bool IsReferenced(object context, object value)
		{
			string text;
			return this.GetMappings(context).TryGetBySecond(value, out text);
		}

		// Token: 0x040000A4 RID: 164
		private int _referenceCount;
	}
}
