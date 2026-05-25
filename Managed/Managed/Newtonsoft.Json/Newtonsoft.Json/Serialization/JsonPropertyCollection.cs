using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000083 RID: 131
	public class JsonPropertyCollection : KeyedCollection<string, JsonProperty>
	{
		// Token: 0x06000651 RID: 1617 RVA: 0x000158D0 File Offset: 0x00013AD0
		public JsonPropertyCollection(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			this._type = type;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x000158EA File Offset: 0x00013AEA
		protected override string GetKeyForItem(JsonProperty item)
		{
			return item.PropertyName;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000158F4 File Offset: 0x00013AF4
		public void AddProperty(JsonProperty property)
		{
			if (base.Contains(property.PropertyName))
			{
				if (property.Ignored)
				{
					return;
				}
				JsonProperty jsonProperty = base[property.PropertyName];
				if (!jsonProperty.Ignored)
				{
					throw new JsonSerializationException("A member with the name '{0}' already exists on '{1}'. Use the JsonPropertyAttribute to specify another name.".FormatWith(CultureInfo.InvariantCulture, new object[] { property.PropertyName, this._type }));
				}
				base.Remove(jsonProperty);
			}
			base.Add(property);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001596C File Offset: 0x00013B6C
		public JsonProperty GetClosestMatchProperty(string propertyName)
		{
			JsonProperty jsonProperty = this.GetProperty(propertyName, StringComparison.Ordinal);
			if (jsonProperty == null)
			{
				jsonProperty = this.GetProperty(propertyName, StringComparison.OrdinalIgnoreCase);
			}
			return jsonProperty;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00015990 File Offset: 0x00013B90
		public JsonProperty GetProperty(string propertyName, StringComparison comparisonType)
		{
			foreach (JsonProperty jsonProperty in this)
			{
				if (string.Equals(propertyName, jsonProperty.PropertyName, comparisonType))
				{
					return jsonProperty;
				}
			}
			return null;
		}

		// Token: 0x040001C6 RID: 454
		private readonly Type _type;
	}
}
