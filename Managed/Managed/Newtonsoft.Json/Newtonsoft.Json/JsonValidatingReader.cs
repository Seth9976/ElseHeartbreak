using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x0200003D RID: 61
	public class JsonValidatingReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000265 RID: 613 RVA: 0x00008E6C File Offset: 0x0000706C
		// (remove) Token: 0x06000266 RID: 614 RVA: 0x00008EA4 File Offset: 0x000070A4
		public event ValidationEventHandler ValidationEventHandler;

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00008ED9 File Offset: 0x000070D9
		public override object Value
		{
			get
			{
				return this._reader.Value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00008EE6 File Offset: 0x000070E6
		public override int Depth
		{
			get
			{
				return this._reader.Depth;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00008EF3 File Offset: 0x000070F3
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00008F00 File Offset: 0x00007100
		public override char QuoteChar
		{
			get
			{
				return this._reader.QuoteChar;
			}
			protected internal set
			{
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00008F02 File Offset: 0x00007102
		public override JsonToken TokenType
		{
			get
			{
				return this._reader.TokenType;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00008F0F File Offset: 0x0000710F
		public override Type ValueType
		{
			get
			{
				return this._reader.ValueType;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008F1C File Offset: 0x0000711C
		private void Push(JsonValidatingReader.SchemaScope scope)
		{
			this._stack.Push(scope);
			this._currentScope = scope;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008F34 File Offset: 0x00007134
		private JsonValidatingReader.SchemaScope Pop()
		{
			JsonValidatingReader.SchemaScope schemaScope = this._stack.Pop();
			this._currentScope = ((this._stack.Count != 0) ? this._stack.Peek() : null);
			return schemaScope;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00008F6F File Offset: 0x0000716F
		private IEnumerable<JsonSchemaModel> CurrentSchemas
		{
			get
			{
				return this._currentScope.Schemas;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00008F7C File Offset: 0x0000717C
		private IEnumerable<JsonSchemaModel> CurrentMemberSchemas
		{
			get
			{
				if (this._currentScope == null)
				{
					return new List<JsonSchemaModel>(new JsonSchemaModel[] { this._model });
				}
				if (this._currentScope.Schemas == null || this._currentScope.Schemas.Count == 0)
				{
					return Enumerable.Empty<JsonSchemaModel>();
				}
				switch (this._currentScope.TokenType)
				{
				case JTokenType.None:
					return this._currentScope.Schemas;
				case JTokenType.Object:
				{
					if (this._currentScope.CurrentPropertyName == null)
					{
						throw new Exception("CurrentPropertyName has not been set on scope.");
					}
					IList<JsonSchemaModel> list = new List<JsonSchemaModel>();
					foreach (JsonSchemaModel jsonSchemaModel in this.CurrentSchemas)
					{
						JsonSchemaModel jsonSchemaModel2;
						if (jsonSchemaModel.Properties != null && jsonSchemaModel.Properties.TryGetValue(this._currentScope.CurrentPropertyName, out jsonSchemaModel2))
						{
							list.Add(jsonSchemaModel2);
						}
						if (jsonSchemaModel.PatternProperties != null)
						{
							foreach (KeyValuePair<string, JsonSchemaModel> keyValuePair in jsonSchemaModel.PatternProperties)
							{
								if (Regex.IsMatch(this._currentScope.CurrentPropertyName, keyValuePair.Key))
								{
									list.Add(keyValuePair.Value);
								}
							}
						}
						if (list.Count == 0 && jsonSchemaModel.AllowAdditionalProperties && jsonSchemaModel.AdditionalProperties != null)
						{
							list.Add(jsonSchemaModel.AdditionalProperties);
						}
					}
					return list;
				}
				case JTokenType.Array:
				{
					IList<JsonSchemaModel> list2 = new List<JsonSchemaModel>();
					foreach (JsonSchemaModel jsonSchemaModel3 in this.CurrentSchemas)
					{
						if (!CollectionUtils.IsNullOrEmpty<JsonSchemaModel>(jsonSchemaModel3.Items))
						{
							if (jsonSchemaModel3.Items.Count == 1)
							{
								list2.Add(jsonSchemaModel3.Items[0]);
							}
							else if (jsonSchemaModel3.Items.Count > this._currentScope.ArrayItemCount - 1)
							{
								list2.Add(jsonSchemaModel3.Items[this._currentScope.ArrayItemCount - 1]);
							}
						}
						if (jsonSchemaModel3.AllowAdditionalProperties && jsonSchemaModel3.AdditionalProperties != null)
						{
							list2.Add(jsonSchemaModel3.AdditionalProperties);
						}
					}
					return list2;
				}
				case JTokenType.Constructor:
					return Enumerable.Empty<JsonSchemaModel>();
				default:
					throw new ArgumentOutOfRangeException("TokenType", "Unexpected token type: {0}".FormatWith(CultureInfo.InvariantCulture, new object[] { this._currentScope.TokenType }));
				}
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00009234 File Offset: 0x00007434
		private void RaiseError(string message, JsonSchemaModel schema)
		{
			string text = (((IJsonLineInfo)this).HasLineInfo() ? (message + " Line {0}, position {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				((IJsonLineInfo)this).LineNumber,
				((IJsonLineInfo)this).LinePosition
			})) : message);
			this.OnValidationEvent(new JsonSchemaException(text, null, ((IJsonLineInfo)this).LineNumber, ((IJsonLineInfo)this).LinePosition));
		}

		// Token: 0x06000272 RID: 626 RVA: 0x000092A4 File Offset: 0x000074A4
		private void OnValidationEvent(JsonSchemaException exception)
		{
			ValidationEventHandler validationEventHandler = this.ValidationEventHandler;
			if (validationEventHandler != null)
			{
				validationEventHandler(this, new ValidationEventArgs(exception));
				return;
			}
			throw exception;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000092CA File Offset: 0x000074CA
		public JsonValidatingReader(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			this._reader = reader;
			this._stack = new Stack<JsonValidatingReader.SchemaScope>();
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000092EF File Offset: 0x000074EF
		// (set) Token: 0x06000275 RID: 629 RVA: 0x000092F7 File Offset: 0x000074F7
		public JsonSchema Schema
		{
			get
			{
				return this._schema;
			}
			set
			{
				if (this.TokenType != JsonToken.None)
				{
					throw new Exception("Cannot change schema while validating JSON.");
				}
				this._schema = value;
				this._model = null;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000931A File Offset: 0x0000751A
		public JsonReader Reader
		{
			get
			{
				return this._reader;
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00009324 File Offset: 0x00007524
		private void ValidateInEnumAndNotDisallowed(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			JToken jtoken = new JValue(this._reader.Value);
			if (schema.Enum != null)
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				jtoken.WriteTo(new JsonTextWriter(stringWriter), new JsonConverter[0]);
				if (!schema.Enum.ContainsValue(jtoken, new JTokenEqualityComparer()))
				{
					this.RaiseError("Value {0} is not defined in enum.".FormatWith(CultureInfo.InvariantCulture, new object[] { stringWriter.ToString() }), schema);
				}
			}
			JsonSchemaType? currentNodeSchemaType = this.GetCurrentNodeSchemaType();
			if (currentNodeSchemaType != null && JsonSchemaGenerator.HasFlag(new JsonSchemaType?(schema.Disallow), currentNodeSchemaType.Value))
			{
				this.RaiseError("Type {0} is disallowed.".FormatWith(CultureInfo.InvariantCulture, new object[] { currentNodeSchemaType }), schema);
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000093F8 File Offset: 0x000075F8
		private JsonSchemaType? GetCurrentNodeSchemaType()
		{
			switch (this._reader.TokenType)
			{
			case JsonToken.StartObject:
				return new JsonSchemaType?(JsonSchemaType.Object);
			case JsonToken.StartArray:
				return new JsonSchemaType?(JsonSchemaType.Array);
			case JsonToken.Integer:
				return new JsonSchemaType?(JsonSchemaType.Integer);
			case JsonToken.Float:
				return new JsonSchemaType?(JsonSchemaType.Float);
			case JsonToken.String:
				return new JsonSchemaType?(JsonSchemaType.String);
			case JsonToken.Boolean:
				return new JsonSchemaType?(JsonSchemaType.Boolean);
			case JsonToken.Null:
				return new JsonSchemaType?(JsonSchemaType.Null);
			}
			return null;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00009484 File Offset: 0x00007684
		public override byte[] ReadAsBytes()
		{
			byte[] array = this._reader.ReadAsBytes();
			this.ValidateCurrentToken();
			return array;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000094A4 File Offset: 0x000076A4
		public override decimal? ReadAsDecimal()
		{
			decimal? num = this._reader.ReadAsDecimal();
			this.ValidateCurrentToken();
			return num;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000094C4 File Offset: 0x000076C4
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			DateTimeOffset? dateTimeOffset = this._reader.ReadAsDateTimeOffset();
			this.ValidateCurrentToken();
			return dateTimeOffset;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000094E4 File Offset: 0x000076E4
		public override bool Read()
		{
			if (!this._reader.Read())
			{
				return false;
			}
			if (this._reader.TokenType == JsonToken.Comment)
			{
				return true;
			}
			this.ValidateCurrentToken();
			return true;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000950C File Offset: 0x0000770C
		private void ValidateCurrentToken()
		{
			if (this._model == null)
			{
				JsonSchemaModelBuilder jsonSchemaModelBuilder = new JsonSchemaModelBuilder();
				this._model = jsonSchemaModelBuilder.Build(this._schema);
			}
			switch (this._reader.TokenType)
			{
			case JsonToken.StartObject:
			{
				this.ProcessValue();
				IList<JsonSchemaModel> list = this.CurrentMemberSchemas.Where(new Func<JsonSchemaModel, bool>(this.ValidateObject)).ToList<JsonSchemaModel>();
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Object, list));
				return;
			}
			case JsonToken.StartArray:
			{
				this.ProcessValue();
				IList<JsonSchemaModel> list2 = this.CurrentMemberSchemas.Where(new Func<JsonSchemaModel, bool>(this.ValidateArray)).ToList<JsonSchemaModel>();
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Array, list2));
				return;
			}
			case JsonToken.StartConstructor:
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Constructor, null));
				return;
			case JsonToken.PropertyName:
			{
				using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentSchemas.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JsonSchemaModel jsonSchemaModel = enumerator.Current;
						this.ValidatePropertyName(jsonSchemaModel);
					}
					return;
				}
				break;
			}
			case JsonToken.Comment:
				goto IL_02E9;
			case JsonToken.Raw:
			case JsonToken.Undefined:
			case JsonToken.Date:
				return;
			case JsonToken.Integer:
				break;
			case JsonToken.Float:
				goto IL_0163;
			case JsonToken.String:
				goto IL_01A3;
			case JsonToken.Boolean:
				goto IL_01E3;
			case JsonToken.Null:
				goto IL_0223;
			case JsonToken.EndObject:
				goto IL_0263;
			case JsonToken.EndArray:
				foreach (JsonSchemaModel jsonSchemaModel2 in this.CurrentSchemas)
				{
					this.ValidateEndArray(jsonSchemaModel2);
				}
				this.Pop();
				return;
			case JsonToken.EndConstructor:
				this.Pop();
				return;
			default:
				goto IL_02E9;
			}
			this.ProcessValue();
			using (IEnumerator<JsonSchemaModel> enumerator3 = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					JsonSchemaModel jsonSchemaModel3 = enumerator3.Current;
					this.ValidateInteger(jsonSchemaModel3);
				}
				return;
			}
			IL_0163:
			this.ProcessValue();
			using (IEnumerator<JsonSchemaModel> enumerator4 = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					JsonSchemaModel jsonSchemaModel4 = enumerator4.Current;
					this.ValidateFloat(jsonSchemaModel4);
				}
				return;
			}
			IL_01A3:
			this.ProcessValue();
			using (IEnumerator<JsonSchemaModel> enumerator5 = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator5.MoveNext())
				{
					JsonSchemaModel jsonSchemaModel5 = enumerator5.Current;
					this.ValidateString(jsonSchemaModel5);
				}
				return;
			}
			IL_01E3:
			this.ProcessValue();
			using (IEnumerator<JsonSchemaModel> enumerator6 = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator6.MoveNext())
				{
					JsonSchemaModel jsonSchemaModel6 = enumerator6.Current;
					this.ValidateBoolean(jsonSchemaModel6);
				}
				return;
			}
			IL_0223:
			this.ProcessValue();
			using (IEnumerator<JsonSchemaModel> enumerator7 = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator7.MoveNext())
				{
					JsonSchemaModel jsonSchemaModel7 = enumerator7.Current;
					this.ValidateNull(jsonSchemaModel7);
				}
				return;
			}
			IL_0263:
			foreach (JsonSchemaModel jsonSchemaModel8 in this.CurrentSchemas)
			{
				this.ValidateEndObject(jsonSchemaModel8);
			}
			this.Pop();
			return;
			IL_02E9:
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00009884 File Offset: 0x00007A84
		private void ValidateEndObject(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			Dictionary<string, bool> requiredProperties = this._currentScope.RequiredProperties;
			if (requiredProperties != null)
			{
				List<string> list = (from kv in requiredProperties
					where !kv.Value
					select kv.Key).ToList<string>();
				if (list.Count > 0)
				{
					this.RaiseError("Required properties are missing from object: {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { string.Join(", ", list.ToArray()) }), schema);
				}
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000992C File Offset: 0x00007B2C
		private void ValidateEndArray(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			int arrayItemCount = this._currentScope.ArrayItemCount;
			if (schema.MaximumItems != null && arrayItemCount > schema.MaximumItems)
			{
				this.RaiseError("Array item count {0} exceeds maximum count of {1}.".FormatWith(CultureInfo.InvariantCulture, new object[] { arrayItemCount, schema.MaximumItems }), schema);
			}
			if (schema.MinimumItems != null && arrayItemCount < schema.MinimumItems)
			{
				this.RaiseError("Array item count {0} is less than minimum count of {1}.".FormatWith(CultureInfo.InvariantCulture, new object[] { arrayItemCount, schema.MinimumItems }), schema);
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00009A1D File Offset: 0x00007C1D
		private void ValidateNull(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Null))
			{
				return;
			}
			this.ValidateInEnumAndNotDisallowed(schema);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00009A36 File Offset: 0x00007C36
		private void ValidateBoolean(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Boolean))
			{
				return;
			}
			this.ValidateInEnumAndNotDisallowed(schema);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00009A50 File Offset: 0x00007C50
		private void ValidateString(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.String))
			{
				return;
			}
			this.ValidateInEnumAndNotDisallowed(schema);
			string text = this._reader.Value.ToString();
			if (schema.MaximumLength != null && text.Length > schema.MaximumLength)
			{
				this.RaiseError("String '{0}' exceeds maximum length of {1}.".FormatWith(CultureInfo.InvariantCulture, new object[] { text, schema.MaximumLength }), schema);
			}
			if (schema.MinimumLength != null && text.Length < schema.MinimumLength)
			{
				this.RaiseError("String '{0}' is less than minimum length of {1}.".FormatWith(CultureInfo.InvariantCulture, new object[] { text, schema.MinimumLength }), schema);
			}
			if (schema.Patterns != null)
			{
				foreach (string text2 in schema.Patterns)
				{
					if (!Regex.IsMatch(text, text2))
					{
						this.RaiseError("String '{0}' does not match regex pattern '{1}'.".FormatWith(CultureInfo.InvariantCulture, new object[] { text, text2 }), schema);
					}
				}
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00009BD4 File Offset: 0x00007DD4
		private void ValidateInteger(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Integer))
			{
				return;
			}
			this.ValidateInEnumAndNotDisallowed(schema);
			long num = Convert.ToInt64(this._reader.Value, CultureInfo.InvariantCulture);
			if (schema.Maximum != null)
			{
				double num2 = (double)num;
				double? maximum = schema.Maximum;
				if (num2 > maximum.GetValueOrDefault() && maximum != null)
				{
					this.RaiseError("Integer {0} exceeds maximum value of {1}.".FormatWith(CultureInfo.InvariantCulture, new object[] { num, schema.Maximum }), schema);
				}
				if (schema.ExclusiveMaximum)
				{
					double num3 = (double)num;
					double? maximum2 = schema.Maximum;
					if (num3 == maximum2.GetValueOrDefault() && maximum2 != null)
					{
						this.RaiseError("Integer {0} equals maximum value of {1} and exclusive maximum is true.".FormatWith(CultureInfo.InvariantCulture, new object[] { num, schema.Maximum }), schema);
					}
				}
			}
			if (schema.Minimum != null)
			{
				double num4 = (double)num;
				double? minimum = schema.Minimum;
				if (num4 < minimum.GetValueOrDefault() && minimum != null)
				{
					this.RaiseError("Integer {0} is less than minimum value of {1}.".FormatWith(CultureInfo.InvariantCulture, new object[] { num, schema.Minimum }), schema);
				}
				if (schema.ExclusiveMinimum)
				{
					double num5 = (double)num;
					double? minimum2 = schema.Minimum;
					if (num5 == minimum2.GetValueOrDefault() && minimum2 != null)
					{
						this.RaiseError("Integer {0} equals minimum value of {1} and exclusive minimum is true.".FormatWith(CultureInfo.InvariantCulture, new object[] { num, schema.Minimum }), schema);
					}
				}
			}
			if (schema.DivisibleBy != null && !JsonValidatingReader.IsZero((double)num % schema.DivisibleBy.Value))
			{
				this.RaiseError("Integer {0} is not evenly divisible by {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JsonConvert.ToString(num),
					schema.DivisibleBy
				}), schema);
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00009E18 File Offset: 0x00008018
		private void ProcessValue()
		{
			if (this._currentScope != null && this._currentScope.TokenType == JTokenType.Array)
			{
				this._currentScope.ArrayItemCount++;
				foreach (JsonSchemaModel jsonSchemaModel in this.CurrentSchemas)
				{
					if (jsonSchemaModel != null && jsonSchemaModel.Items != null && jsonSchemaModel.Items.Count > 1 && this._currentScope.ArrayItemCount >= jsonSchemaModel.Items.Count)
					{
						this.RaiseError("Index {0} has not been defined and the schema does not allow additional items.".FormatWith(CultureInfo.InvariantCulture, new object[] { this._currentScope.ArrayItemCount }), jsonSchemaModel);
					}
				}
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00009EF0 File Offset: 0x000080F0
		private void ValidateFloat(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Float))
			{
				return;
			}
			this.ValidateInEnumAndNotDisallowed(schema);
			double num = Convert.ToDouble(this._reader.Value, CultureInfo.InvariantCulture);
			if (schema.Maximum != null)
			{
				double num2 = num;
				double? maximum = schema.Maximum;
				if (num2 > maximum.GetValueOrDefault() && maximum != null)
				{
					this.RaiseError("Float {0} exceeds maximum value of {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						JsonConvert.ToString(num),
						schema.Maximum
					}), schema);
				}
				if (schema.ExclusiveMaximum)
				{
					double num3 = num;
					double? maximum2 = schema.Maximum;
					if (num3 == maximum2.GetValueOrDefault() && maximum2 != null)
					{
						this.RaiseError("Float {0} equals maximum value of {1} and exclusive maximum is true.".FormatWith(CultureInfo.InvariantCulture, new object[]
						{
							JsonConvert.ToString(num),
							schema.Maximum
						}), schema);
					}
				}
			}
			if (schema.Minimum != null)
			{
				double num4 = num;
				double? minimum = schema.Minimum;
				if (num4 < minimum.GetValueOrDefault() && minimum != null)
				{
					this.RaiseError("Float {0} is less than minimum value of {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						JsonConvert.ToString(num),
						schema.Minimum
					}), schema);
				}
				if (schema.ExclusiveMinimum)
				{
					double num5 = num;
					double? minimum2 = schema.Minimum;
					if (num5 == minimum2.GetValueOrDefault() && minimum2 != null)
					{
						this.RaiseError("Float {0} equals minimum value of {1} and exclusive minimum is true.".FormatWith(CultureInfo.InvariantCulture, new object[]
						{
							JsonConvert.ToString(num),
							schema.Minimum
						}), schema);
					}
				}
			}
			if (schema.DivisibleBy != null && !JsonValidatingReader.IsZero(num % schema.DivisibleBy.Value))
			{
				this.RaiseError("Float {0} is not evenly divisible by {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JsonConvert.ToString(num),
					schema.DivisibleBy
				}), schema);
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000A130 File Offset: 0x00008330
		private static bool IsZero(double value)
		{
			double num = 2.220446049250313E-16;
			return Math.Abs(value) < 10.0 * num;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000A15C File Offset: 0x0000835C
		private void ValidatePropertyName(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			string text = Convert.ToString(this._reader.Value, CultureInfo.InvariantCulture);
			if (this._currentScope.RequiredProperties.ContainsKey(text))
			{
				this._currentScope.RequiredProperties[text] = true;
			}
			if (!schema.AllowAdditionalProperties && !this.IsPropertyDefinied(schema, text))
			{
				this.RaiseError("Property '{0}' has not been defined and the schema does not allow additional properties.".FormatWith(CultureInfo.InvariantCulture, new object[] { text }), schema);
			}
			this._currentScope.CurrentPropertyName = text;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000A1EC File Offset: 0x000083EC
		private bool IsPropertyDefinied(JsonSchemaModel schema, string propertyName)
		{
			if (schema.Properties != null && schema.Properties.ContainsKey(propertyName))
			{
				return true;
			}
			if (schema.PatternProperties != null)
			{
				foreach (string text in schema.PatternProperties.Keys)
				{
					if (Regex.IsMatch(propertyName, text))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000A268 File Offset: 0x00008468
		private bool ValidateArray(JsonSchemaModel schema)
		{
			return schema == null || this.TestType(schema, JsonSchemaType.Array);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000A278 File Offset: 0x00008478
		private bool ValidateObject(JsonSchemaModel schema)
		{
			return schema == null || this.TestType(schema, JsonSchemaType.Object);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000A288 File Offset: 0x00008488
		private bool TestType(JsonSchemaModel currentSchema, JsonSchemaType currentType)
		{
			if (!JsonSchemaGenerator.HasFlag(new JsonSchemaType?(currentSchema.Type), currentType))
			{
				this.RaiseError("Invalid type. Expected {0} but got {1}.".FormatWith(CultureInfo.InvariantCulture, new object[] { currentSchema.Type, currentType }), currentSchema);
				return false;
			}
			return true;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000A2E0 File Offset: 0x000084E0
		bool IJsonLineInfo.HasLineInfo()
		{
			IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000A304 File Offset: 0x00008504
		int IJsonLineInfo.LineNumber
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LineNumber;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000A328 File Offset: 0x00008528
		int IJsonLineInfo.LinePosition
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LinePosition;
			}
		}

		// Token: 0x040000CF RID: 207
		private readonly JsonReader _reader;

		// Token: 0x040000D0 RID: 208
		private readonly Stack<JsonValidatingReader.SchemaScope> _stack;

		// Token: 0x040000D1 RID: 209
		private JsonSchema _schema;

		// Token: 0x040000D2 RID: 210
		private JsonSchemaModel _model;

		// Token: 0x040000D3 RID: 211
		private JsonValidatingReader.SchemaScope _currentScope;

		// Token: 0x0200003E RID: 62
		private class SchemaScope
		{
			// Token: 0x1700006E RID: 110
			// (get) Token: 0x06000291 RID: 657 RVA: 0x0000A34C File Offset: 0x0000854C
			// (set) Token: 0x06000292 RID: 658 RVA: 0x0000A354 File Offset: 0x00008554
			public string CurrentPropertyName { get; set; }

			// Token: 0x1700006F RID: 111
			// (get) Token: 0x06000293 RID: 659 RVA: 0x0000A35D File Offset: 0x0000855D
			// (set) Token: 0x06000294 RID: 660 RVA: 0x0000A365 File Offset: 0x00008565
			public int ArrayItemCount { get; set; }

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x06000295 RID: 661 RVA: 0x0000A36E File Offset: 0x0000856E
			public IList<JsonSchemaModel> Schemas
			{
				get
				{
					return this._schemas;
				}
			}

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x06000296 RID: 662 RVA: 0x0000A376 File Offset: 0x00008576
			public Dictionary<string, bool> RequiredProperties
			{
				get
				{
					return this._requiredProperties;
				}
			}

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x06000297 RID: 663 RVA: 0x0000A37E File Offset: 0x0000857E
			public JTokenType TokenType
			{
				get
				{
					return this._tokenType;
				}
			}

			// Token: 0x06000298 RID: 664 RVA: 0x0000A38C File Offset: 0x0000858C
			public SchemaScope(JTokenType tokenType, IList<JsonSchemaModel> schemas)
			{
				this._tokenType = tokenType;
				this._schemas = schemas;
				this._requiredProperties = schemas.SelectMany(new Func<JsonSchemaModel, IEnumerable<string>>(this.GetRequiredProperties)).Distinct<string>().ToDictionary((string p) => p, (string p) => false);
			}

			// Token: 0x06000299 RID: 665 RVA: 0x0000A420 File Offset: 0x00008620
			private IEnumerable<string> GetRequiredProperties(JsonSchemaModel schema)
			{
				if (schema == null || schema.Properties == null)
				{
					return Enumerable.Empty<string>();
				}
				return from p in schema.Properties
					where p.Value.Required
					select p.Key;
			}

			// Token: 0x040000D7 RID: 215
			private readonly JTokenType _tokenType;

			// Token: 0x040000D8 RID: 216
			private readonly IList<JsonSchemaModel> _schemas;

			// Token: 0x040000D9 RID: 217
			private readonly Dictionary<string, bool> _requiredProperties;
		}
	}
}
