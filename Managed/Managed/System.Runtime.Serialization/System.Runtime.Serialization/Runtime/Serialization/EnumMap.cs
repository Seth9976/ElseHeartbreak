using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace System.Runtime.Serialization
{
	// Token: 0x0200002C RID: 44
	internal class EnumMap : SerializationMap
	{
		// Token: 0x06000103 RID: 259 RVA: 0x000062C4 File Offset: 0x000044C4
		public EnumMap(Type type, XmlQualifiedName qname, KnownTypeCollection knownTypes)
			: base(type, qname, knownTypes)
		{
			bool flag = false;
			object[] customAttributes = this.RuntimeType.GetCustomAttributes(typeof(DataContractAttribute), false);
			if (customAttributes.Length != 0)
			{
				flag = true;
			}
			this.flag_attr = type.GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0;
			this.enum_members = new List<EnumMemberInfo>();
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public;
			FieldInfo[] fields = this.RuntimeType.GetFields(bindingFlags);
			int i = 0;
			while (i < fields.Length)
			{
				FieldInfo fieldInfo = fields[i];
				string text = fieldInfo.Name;
				if (!flag)
				{
					goto IL_00AA;
				}
				EnumMemberAttribute enumMemberAttribute = this.GetEnumMemberAttribute(fieldInfo);
				if (enumMemberAttribute != null)
				{
					if (enumMemberAttribute.Value != null)
					{
						text = enumMemberAttribute.Value;
						goto IL_00AA;
					}
					goto IL_00AA;
				}
				IL_00C3:
				i++;
				continue;
				IL_00AA:
				this.enum_members.Add(new EnumMemberInfo(text, fieldInfo.GetValue(null)));
				goto IL_00C3;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000063A8 File Offset: 0x000045A8
		private EnumMemberAttribute GetEnumMemberAttribute(MemberInfo mi)
		{
			object[] customAttributes = mi.GetCustomAttributes(typeof(EnumMemberAttribute), false);
			if (customAttributes.Length == 0)
			{
				return null;
			}
			return (EnumMemberAttribute)customAttributes[0];
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000063DC File Offset: 0x000045DC
		public override XmlSchemaType GetSchemaType(XmlSchemaSet schemas, Dictionary<XmlQualifiedName, XmlSchemaType> generated_schema_types)
		{
			if (generated_schema_types.ContainsKey(base.XmlName))
			{
				return generated_schema_types[base.XmlName];
			}
			XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
			xmlSchemaSimpleType.Name = base.XmlName.Name;
			XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = new XmlSchemaSimpleTypeRestriction();
			xmlSchemaSimpleType.Content = xmlSchemaSimpleTypeRestriction;
			xmlSchemaSimpleTypeRestriction.BaseTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
			foreach (EnumMemberInfo enumMemberInfo in this.enum_members)
			{
				XmlSchemaEnumerationFacet xmlSchemaEnumerationFacet = new XmlSchemaEnumerationFacet();
				xmlSchemaEnumerationFacet.Value = enumMemberInfo.XmlName;
				xmlSchemaSimpleTypeRestriction.Facets.Add(xmlSchemaEnumerationFacet);
			}
			generated_schema_types[base.XmlName] = xmlSchemaSimpleType;
			XmlSchema schema = base.GetSchema(schemas, base.XmlName.Namespace);
			XmlSchemaElement schemaElement = base.GetSchemaElement(base.XmlName, xmlSchemaSimpleType);
			schemaElement.IsNillable = true;
			schema.Items.Add(xmlSchemaSimpleType);
			schema.Items.Add(schemaElement);
			return xmlSchemaSimpleType;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000650C File Offset: 0x0000470C
		public override void Serialize(object graph, XmlFormatterSerializer serializer)
		{
			foreach (EnumMemberInfo enumMemberInfo in this.enum_members)
			{
				if (object.Equals(enumMemberInfo.Value, graph))
				{
					serializer.Writer.WriteString(enumMemberInfo.XmlName);
					return;
				}
			}
			throw new SerializationException(string.Format("Enum value '{0}' is invalid for type '{1}' and cannot be serialized.", graph, this.RuntimeType));
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000065AC File Offset: 0x000047AC
		public override object DeserializeEmptyContent(XmlReader reader, XmlFormatterDeserializer deserializer)
		{
			if (!this.flag_attr)
			{
				throw new SerializationException(string.Format("Enum value '' is invalid for type '{0}' and cannot be deserialized.", this.RuntimeType));
			}
			return Enum.ToObject(this.RuntimeType, 0);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000065DC File Offset: 0x000047DC
		public override object DeserializeContent(XmlReader reader, XmlFormatterDeserializer deserializer)
		{
			string text = ((reader.NodeType == XmlNodeType.Text) ? reader.ReadContentAsString() : string.Empty);
			if (text != string.Empty)
			{
				foreach (EnumMemberInfo enumMemberInfo in this.enum_members)
				{
					if (enumMemberInfo.XmlName == text)
					{
						return enumMemberInfo.Value;
					}
				}
			}
			if (!this.flag_attr)
			{
				throw new SerializationException(string.Format("Enum value '{0}' is invalid for type '{1}' and cannot be deserialized.", text, this.RuntimeType));
			}
			return Enum.ToObject(this.RuntimeType, 0);
		}

		// Token: 0x04000089 RID: 137
		private List<EnumMemberInfo> enum_members;

		// Token: 0x0400008A RID: 138
		private bool flag_attr;
	}
}
