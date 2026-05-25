using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;

namespace System.Data
{
	// Token: 0x02000186 RID: 390
	internal class XmlSchemaDataImporter
	{
		// Token: 0x0600148E RID: 5262 RVA: 0x00056D90 File Offset: 0x00054F90
		public XmlSchemaDataImporter(DataSet dataset, XmlReader reader, bool forDataSet)
		{
			this.dataset = dataset;
			this.forDataSet = forDataSet;
			dataset.DataSetName = "NewDataSet";
			this.schema = XmlSchema.Read(reader, null);
			if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
			{
				reader.ReadEndElement();
			}
			this.schema.Compile(null);
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x00056E40 File Offset: 0x00055040
		static XmlSchemaDataImporter()
		{
			XmlSchema xmlSchema = new XmlSchema();
			XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
			xmlSchemaAttribute.Name = "foo";
			xmlSchemaAttribute.SchemaTypeName = new XmlQualifiedName("integer", "http://www.w3.org/2001/XMLSchema");
			xmlSchema.Items.Add(xmlSchemaAttribute);
			XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
			xmlSchemaAttribute2.Name = "bar";
			xmlSchemaAttribute2.SchemaTypeName = new XmlQualifiedName("decimal", "http://www.w3.org/2001/XMLSchema");
			xmlSchema.Items.Add(xmlSchemaAttribute2);
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.Name = "bar";
			xmlSchema.Items.Add(xmlSchemaElement);
			xmlSchema.Compile(null);
			XmlSchemaDataImporter.schemaIntegerType = xmlSchemaAttribute.AttributeSchemaType.Datatype;
			XmlSchemaDataImporter.schemaDecimalType = xmlSchemaAttribute2.AttributeSchemaType.Datatype;
			XmlSchemaDataImporter.schemaAnyType = xmlSchemaElement.ElementSchemaType as XmlSchemaComplexType;
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x00056F10 File Offset: 0x00055110
		internal TableAdapterSchemaInfo CurrentAdapter
		{
			get
			{
				return this.currentAdapter;
			}
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x00056F18 File Offset: 0x00055118
		public void Process()
		{
			if (this.schema.Id != null)
			{
				this.dataset.DataSetName = this.schema.Id;
			}
			this.dataset.Namespace = this.schema.TargetNamespace;
			foreach (XmlSchemaObject xmlSchemaObject in this.schema.Items)
			{
				XmlSchemaElement xmlSchemaElement = xmlSchemaObject as XmlSchemaElement;
				if (xmlSchemaElement != null)
				{
					if (this.datasetElement == null && this.IsDataSetElement(xmlSchemaElement))
					{
						this.datasetElement = xmlSchemaElement;
					}
					if (xmlSchemaElement.ElementSchemaType is XmlSchemaComplexType && xmlSchemaElement.ElementSchemaType != XmlSchemaDataImporter.schemaAnyType)
					{
						this.targetElements.Add(xmlSchemaObject);
					}
				}
			}
			if (this.datasetElement != null)
			{
				foreach (XmlSchemaObject xmlSchemaObject2 in this.datasetElement.Constraints)
				{
					if (!(xmlSchemaObject2 is XmlSchemaKeyref))
					{
						this.ReserveSelfIdentity((XmlSchemaIdentityConstraint)xmlSchemaObject2);
					}
				}
				foreach (XmlSchemaObject xmlSchemaObject3 in this.datasetElement.Constraints)
				{
					if (xmlSchemaObject3 is XmlSchemaKeyref)
					{
						this.ReserveRelationIdentity(this.datasetElement, (XmlSchemaKeyref)xmlSchemaObject3);
					}
				}
			}
			foreach (XmlSchemaObject xmlSchemaObject4 in this.schema.Items)
			{
				if (xmlSchemaObject4 is XmlSchemaElement)
				{
					XmlSchemaElement xmlSchemaElement2 = xmlSchemaObject4 as XmlSchemaElement;
					if (xmlSchemaElement2.ElementSchemaType is XmlSchemaComplexType && xmlSchemaElement2.ElementSchemaType != XmlSchemaDataImporter.schemaAnyType)
					{
						this.targetElements.Add(xmlSchemaObject4);
					}
				}
			}
			int count = this.targetElements.Count;
			for (int i = 0; i < count; i++)
			{
				this.ProcessGlobalElement((XmlSchemaElement)this.targetElements[i]);
			}
			for (int j = count; j < this.targetElements.Count; j++)
			{
				this.ProcessDataTableElement((XmlSchemaElement)this.targetElements[j]);
			}
			foreach (XmlSchemaObject xmlSchemaObject5 in this.schema.Items)
			{
				if (xmlSchemaObject5 is XmlSchemaAnnotation)
				{
					this.HandleAnnotations((XmlSchemaAnnotation)xmlSchemaObject5, false);
				}
			}
			if (this.datasetElement != null)
			{
				foreach (XmlSchemaObject xmlSchemaObject6 in this.datasetElement.Constraints)
				{
					if (!(xmlSchemaObject6 is XmlSchemaKeyref))
					{
						this.ProcessSelfIdentity(this.reservedConstraints[xmlSchemaObject6] as ConstraintStructure);
					}
				}
				foreach (XmlSchemaObject xmlSchemaObject7 in this.datasetElement.Constraints)
				{
					if (xmlSchemaObject7 is XmlSchemaKeyref)
					{
						this.ProcessRelationIdentity(this.datasetElement, this.reservedConstraints[xmlSchemaObject7] as ConstraintStructure);
					}
				}
			}
			foreach (object obj in this.relations)
			{
				RelationStructure relationStructure = (RelationStructure)obj;
				this.dataset.Relations.Add(this.GenerateRelationship(relationStructure));
			}
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00057438 File Offset: 0x00055638
		private bool IsDataSetElement(XmlSchemaElement el)
		{
			if (el.UnhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in el.UnhandledAttributes)
				{
					if (xmlAttribute.LocalName == "IsDataSet" && xmlAttribute.NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
					{
						string value = xmlAttribute.Value;
						if (value != null)
						{
							if (XmlSchemaDataImporter.<>f__switch$mapB == null)
							{
								XmlSchemaDataImporter.<>f__switch$mapB = new Dictionary<string, int>(2)
								{
									{ "true", 0 },
									{ "false", 1 }
								};
							}
							int num;
							if (XmlSchemaDataImporter.<>f__switch$mapB.TryGetValue(value, out num))
							{
								if (num == 0)
								{
									return true;
								}
								if (num == 1)
								{
									goto IL_00CD;
								}
							}
						}
						throw new DataException(string.Format("Value {0} is invalid for attribute 'IsDataSet'.", xmlAttribute.Value));
					}
					IL_00CD:;
				}
			}
			if (this.schema.Elements.Count != 1)
			{
				return false;
			}
			if (!(el.SchemaType is XmlSchemaComplexType))
			{
				return false;
			}
			XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)el.SchemaType;
			if (xmlSchemaComplexType.AttributeUses.Count > 0)
			{
				return false;
			}
			XmlSchemaGroupBase xmlSchemaGroupBase = xmlSchemaComplexType.ContentTypeParticle as XmlSchemaGroupBase;
			if (xmlSchemaGroupBase == null || xmlSchemaGroupBase.Items.Count == 0)
			{
				return false;
			}
			foreach (XmlSchemaObject xmlSchemaObject in xmlSchemaGroupBase.Items)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)xmlSchemaObject;
				if (this.ContainsColumn(xmlSchemaParticle))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0005760C File Offset: 0x0005580C
		private bool ContainsColumn(XmlSchemaParticle p)
		{
			XmlSchemaElement xmlSchemaElement = p as XmlSchemaElement;
			if (xmlSchemaElement != null)
			{
				XmlSchemaComplexType xmlSchemaComplexType = xmlSchemaElement.ElementSchemaType as XmlSchemaComplexType;
				return xmlSchemaComplexType == null || xmlSchemaComplexType == XmlSchemaDataImporter.schemaAnyType || (xmlSchemaComplexType.AttributeUses.Count <= 0 && xmlSchemaComplexType.ContentType == XmlSchemaContentType.TextOnly);
			}
			XmlSchemaGroupBase xmlSchemaGroupBase = p as XmlSchemaGroupBase;
			for (int i = 0; i < xmlSchemaGroupBase.Items.Count; i++)
			{
				if (this.ContainsColumn((XmlSchemaParticle)xmlSchemaGroupBase.Items[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x000576AC File Offset: 0x000558AC
		private void ProcessGlobalElement(XmlSchemaElement el)
		{
			if (this.dataset.Tables.Contains(el.QualifiedName.Name))
			{
				return;
			}
			if (!(el.ElementSchemaType is XmlSchemaComplexType) || el.ElementSchemaType == XmlSchemaDataImporter.schemaAnyType)
			{
				return;
			}
			if (this.IsDataSetElement(el))
			{
				this.ProcessDataSetElement(el);
				return;
			}
			this.dataset.Locale = CultureInfo.CurrentCulture;
			this.topLevelElements.Add(el);
			this.ProcessDataTableElement(el);
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x00057734 File Offset: 0x00055934
		private void ProcessDataSetElement(XmlSchemaElement el)
		{
			this.dataset.DataSetName = el.Name;
			this.datasetElement = el;
			bool flag = false;
			if (el.UnhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in el.UnhandledAttributes)
				{
					if (xmlAttribute.LocalName == "UseCurrentLocale" && xmlAttribute.NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
					{
						flag = true;
					}
					if (xmlAttribute.LocalName == "Locale" && xmlAttribute.NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
					{
						CultureInfo cultureInfo = new CultureInfo(xmlAttribute.Value);
						this.dataset.Locale = cultureInfo;
					}
				}
			}
			if (!flag && !this.dataset.LocaleSpecified)
			{
				this.dataset.Locale = CultureInfo.CurrentCulture;
			}
			XmlSchemaComplexType xmlSchemaComplexType = el.ElementSchemaType as XmlSchemaComplexType;
			XmlSchemaParticle xmlSchemaParticle = ((xmlSchemaComplexType == null) ? null : xmlSchemaComplexType.ContentTypeParticle);
			if (xmlSchemaParticle != null)
			{
				this.HandleDataSetContentTypeParticle(xmlSchemaParticle);
			}
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x00057850 File Offset: 0x00055A50
		private void HandleDataSetContentTypeParticle(XmlSchemaParticle p)
		{
			XmlSchemaElement xmlSchemaElement = p as XmlSchemaElement;
			if (xmlSchemaElement != null)
			{
				if (xmlSchemaElement.ElementSchemaType is XmlSchemaComplexType && xmlSchemaElement.RefName != xmlSchemaElement.QualifiedName)
				{
					this.ProcessDataTableElement(xmlSchemaElement);
				}
			}
			else if (p is XmlSchemaGroupBase)
			{
				foreach (XmlSchemaObject xmlSchemaObject in ((XmlSchemaGroupBase)p).Items)
				{
					XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)xmlSchemaObject;
					this.HandleDataSetContentTypeParticle(xmlSchemaParticle);
				}
			}
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00057910 File Offset: 0x00055B10
		private void ProcessDataTableElement(XmlSchemaElement el)
		{
			string text = XmlHelper.Decode(el.QualifiedName.Name);
			if (this.dataset.Tables.Contains(text))
			{
				return;
			}
			DataTable dataTable = new DataTable(text);
			dataTable.Namespace = el.QualifiedName.Namespace;
			TableStructure tableStructure = this.currentTable;
			this.currentTable = new TableStructure(dataTable);
			this.dataset.Tables.Add(dataTable);
			if (el.UnhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in el.UnhandledAttributes)
				{
					if (xmlAttribute.LocalName == "Locale" && xmlAttribute.NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
					{
						dataTable.Locale = new CultureInfo(xmlAttribute.Value);
					}
				}
			}
			XmlSchemaComplexType xmlSchemaComplexType = null;
			xmlSchemaComplexType = (XmlSchemaComplexType)el.ElementSchemaType;
			foreach (object obj in xmlSchemaComplexType.AttributeUses)
			{
				this.ImportColumnAttribute((XmlSchemaAttribute)((DictionaryEntry)obj).Value);
			}
			if (xmlSchemaComplexType.ContentTypeParticle is XmlSchemaElement)
			{
				this.ImportColumnElement(el, (XmlSchemaElement)xmlSchemaComplexType.ContentTypeParticle);
			}
			else if (xmlSchemaComplexType.ContentTypeParticle is XmlSchemaGroupBase)
			{
				this.ImportColumnGroupBase(el, (XmlSchemaGroupBase)xmlSchemaComplexType.ContentTypeParticle);
			}
			XmlSchemaContentType contentType = xmlSchemaComplexType.ContentType;
			if (contentType == XmlSchemaContentType.TextOnly)
			{
				string text2 = el.QualifiedName.Name + "_text";
				DataColumn dataColumn = new DataColumn(text2);
				dataColumn.Namespace = el.QualifiedName.Namespace;
				dataColumn.AllowDBNull = el.MinOccurs == 0m;
				dataColumn.ColumnMapping = MappingType.SimpleContent;
				dataColumn.DataType = this.ConvertDatatype(xmlSchemaComplexType.Datatype);
				this.currentTable.NonOrdinalColumns.Add(dataColumn);
			}
			SortedList sortedList = new SortedList();
			foreach (object obj2 in this.currentTable.OrdinalColumns)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
				sortedList.Add(dictionaryEntry.Value, dictionaryEntry.Key);
			}
			foreach (object obj3 in sortedList)
			{
				DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj3;
				dataTable.Columns.Add((DataColumn)dictionaryEntry2.Value);
			}
			foreach (object obj4 in this.currentTable.NonOrdinalColumns)
			{
				DataColumn dataColumn2 = (DataColumn)obj4;
				dataTable.Columns.Add(dataColumn2);
			}
			this.currentTable = tableStructure;
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x00057CC0 File Offset: 0x00055EC0
		private DataRelation GenerateRelationship(RelationStructure rs)
		{
			DataTable dataTable = this.dataset.Tables[rs.ParentTableName];
			DataTable dataTable2 = this.dataset.Tables[rs.ChildTableName];
			string text = ((rs.ExplicitName == null) ? (XmlHelper.Decode(dataTable.TableName) + '_' + XmlHelper.Decode(dataTable2.TableName)) : rs.ExplicitName);
			DataRelation dataRelation;
			if (this.datasetElement != null)
			{
				string[] array = rs.ParentColumnName.Split(null);
				string[] array2 = rs.ChildColumnName.Split(null);
				DataColumn[] array3 = new DataColumn[array.Length];
				for (int i = 0; i < array3.Length; i++)
				{
					array3[i] = dataTable.Columns[XmlHelper.Decode(array[i])];
				}
				DataColumn[] array4 = new DataColumn[array2.Length];
				for (int j = 0; j < array4.Length; j++)
				{
					array4[j] = dataTable2.Columns[XmlHelper.Decode(array2[j])];
					if (array4[j] == null)
					{
						array4[j] = this.CreateChildColumn(array3[j], dataTable2);
					}
				}
				dataRelation = new DataRelation(text, array3, array4, rs.CreateConstraint);
			}
			else
			{
				DataColumn dataColumn = dataTable.Columns[XmlHelper.Decode(rs.ParentColumnName)];
				DataColumn dataColumn2 = dataTable2.Columns[XmlHelper.Decode(rs.ChildColumnName)];
				if (dataColumn2 == null)
				{
					dataColumn2 = this.CreateChildColumn(dataColumn, dataTable2);
				}
				dataRelation = new DataRelation(text, dataColumn, dataColumn2, rs.CreateConstraint);
			}
			dataRelation.Nested = rs.IsNested;
			if (rs.CreateConstraint)
			{
				dataRelation.ParentTable.PrimaryKey = dataRelation.ParentColumns;
			}
			return dataRelation;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00057E84 File Offset: 0x00056084
		private DataColumn CreateChildColumn(DataColumn parentColumn, DataTable childTable)
		{
			DataColumn dataColumn = childTable.Columns.Add(parentColumn.ColumnName, parentColumn.DataType);
			dataColumn.Namespace = string.Empty;
			dataColumn.ColumnMapping = MappingType.Hidden;
			return dataColumn;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00057EBC File Offset: 0x000560BC
		private void ImportColumnGroupBase(XmlSchemaElement parent, XmlSchemaGroupBase gb)
		{
			foreach (XmlSchemaObject xmlSchemaObject in gb.Items)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)xmlSchemaObject;
				XmlSchemaElement xmlSchemaElement = xmlSchemaParticle as XmlSchemaElement;
				if (xmlSchemaElement != null)
				{
					this.ImportColumnElement(parent, xmlSchemaElement);
				}
				else if (xmlSchemaParticle is XmlSchemaGroupBase)
				{
					this.ImportColumnGroupBase(parent, (XmlSchemaGroupBase)xmlSchemaParticle);
				}
			}
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x00057F58 File Offset: 0x00056158
		private XmlSchemaDatatype GetSchemaPrimitiveType(object type)
		{
			if (type is XmlSchemaComplexType)
			{
				return null;
			}
			XmlSchemaDatatype xmlSchemaDatatype = type as XmlSchemaDatatype;
			if (xmlSchemaDatatype == null && type != null)
			{
				xmlSchemaDatatype = ((XmlSchemaSimpleType)type).Datatype;
			}
			return xmlSchemaDatatype;
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00057F94 File Offset: 0x00056194
		private void ImportColumnAttribute(XmlSchemaAttribute attr)
		{
			DataColumn dataColumn = new DataColumn();
			dataColumn.ColumnName = attr.QualifiedName.Name;
			dataColumn.Namespace = attr.QualifiedName.Namespace;
			XmlSchemaDatatype schemaPrimitiveType = this.GetSchemaPrimitiveType(attr.AttributeSchemaType.Datatype);
			dataColumn.DataType = this.ConvertDatatype(schemaPrimitiveType);
			if (dataColumn.DataType == typeof(object))
			{
				dataColumn.DataType = typeof(string);
			}
			if (attr.Use == XmlSchemaUse.Prohibited)
			{
				dataColumn.ColumnMapping = MappingType.Hidden;
			}
			else
			{
				dataColumn.ColumnMapping = MappingType.Attribute;
				dataColumn.DefaultValue = this.GetAttributeDefaultValue(attr);
			}
			if (attr.Use == XmlSchemaUse.Required)
			{
				dataColumn.AllowDBNull = false;
			}
			this.FillFacet(dataColumn, attr.AttributeSchemaType);
			this.ImportColumnMetaInfo(attr, attr.QualifiedName, dataColumn);
			this.AddColumn(dataColumn);
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x00058070 File Offset: 0x00056270
		private void ImportColumnElement(XmlSchemaElement parent, XmlSchemaElement el)
		{
			DataColumn dataColumn = new DataColumn();
			dataColumn.DefaultValue = this.GetElementDefaultValue(el);
			dataColumn.AllowDBNull = el.MinOccurs == 0m;
			if (el.ElementSchemaType is XmlSchemaComplexType && el.ElementSchemaType != XmlSchemaDataImporter.schemaAnyType)
			{
				this.FillDataColumnComplexElement(parent, el, dataColumn);
			}
			else if (el.MaxOccurs != 1m)
			{
				this.FillDataColumnRepeatedSimpleElement(parent, el, dataColumn);
			}
			else
			{
				this.FillDataColumnSimpleElement(el, dataColumn);
			}
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x00058104 File Offset: 0x00056304
		private void ImportColumnMetaInfo(XmlSchemaAnnotated obj, XmlQualifiedName name, DataColumn col)
		{
			if (obj.UnhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in obj.UnhandledAttributes)
				{
					if (!(xmlAttribute.NamespaceURI != "urn:schemas-microsoft-com:xml-msdata"))
					{
						string localName = xmlAttribute.LocalName;
						switch (localName)
						{
						case "Caption":
							col.Caption = xmlAttribute.Value;
							break;
						case "DataType":
							col.DataType = Type.GetType(xmlAttribute.Value);
							break;
						case "AutoIncrement":
							col.AutoIncrement = bool.Parse(xmlAttribute.Value);
							break;
						case "AutoIncrementSeed":
							col.AutoIncrementSeed = (long)int.Parse(xmlAttribute.Value);
							break;
						case "AutoIncrementStep":
							col.AutoIncrementStep = (long)int.Parse(xmlAttribute.Value);
							break;
						case "ReadOnly":
							col.ReadOnly = XmlConvert.ToBoolean(xmlAttribute.Value);
							break;
						case "Ordinal":
						{
							int num2 = int.Parse(xmlAttribute.Value);
							break;
						}
						}
					}
				}
			}
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x000582A8 File Offset: 0x000564A8
		private void FillDataColumnComplexElement(XmlSchemaElement parent, XmlSchemaElement el, DataColumn col)
		{
			if (this.targetElements.Contains(el))
			{
				return;
			}
			string text = XmlHelper.Decode(el.QualifiedName.Name);
			if (text == this.dataset.DataSetName)
			{
				throw new ArgumentException("Nested element must not have the same name as DataSet's name.");
			}
			if (el.Annotation != null)
			{
				this.HandleAnnotations(el.Annotation, true);
			}
			else if (!this.DataSetDefinesKey(text))
			{
				this.AddParentKeyColumn(parent, el, col);
				RelationStructure relationStructure = new RelationStructure();
				relationStructure.ParentTableName = XmlHelper.Decode(parent.QualifiedName.Name);
				relationStructure.ChildTableName = text;
				relationStructure.ParentColumnName = col.ColumnName;
				relationStructure.ChildColumnName = col.ColumnName;
				relationStructure.CreateConstraint = true;
				relationStructure.IsNested = true;
				this.relations.Add(relationStructure);
			}
			if (el.RefName == XmlQualifiedName.Empty)
			{
				this.ProcessDataTableElement(el);
			}
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x000583A0 File Offset: 0x000565A0
		private bool DataSetDefinesKey(string name)
		{
			foreach (object obj in this.reservedConstraints.Values)
			{
				ConstraintStructure constraintStructure = (ConstraintStructure)obj;
				if (constraintStructure.TableName == name && (constraintStructure.IsPrimaryKey || constraintStructure.IsNested))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x00058440 File Offset: 0x00056640
		private void AddParentKeyColumn(XmlSchemaElement parent, XmlSchemaElement el, DataColumn col)
		{
			if (this.currentTable.Table.PrimaryKey.Length > 0)
			{
				throw new DataException(string.Format("There is already primary key columns in the table \"{0}\".", this.currentTable.Table.TableName));
			}
			if (this.currentTable.PrimaryKey != null)
			{
				col.ColumnName = this.currentTable.PrimaryKey.ColumnName;
				col.ColumnMapping = this.currentTable.PrimaryKey.ColumnMapping;
				col.Namespace = this.currentTable.PrimaryKey.Namespace;
				col.DataType = this.currentTable.PrimaryKey.DataType;
				col.AutoIncrement = this.currentTable.PrimaryKey.AutoIncrement;
				col.AllowDBNull = this.currentTable.PrimaryKey.AllowDBNull;
				this.ImportColumnMetaInfo(el, el.QualifiedName, col);
				return;
			}
			string text = XmlHelper.Decode(parent.QualifiedName.Name) + "_Id";
			int num = 0;
			while (this.currentTable.ContainsColumn(text))
			{
				text = string.Format("{0}_{1}", text, num++);
			}
			col.ColumnName = text;
			col.ColumnMapping = MappingType.Hidden;
			col.Namespace = parent.QualifiedName.Namespace;
			col.DataType = typeof(int);
			col.AutoIncrement = true;
			col.AllowDBNull = false;
			this.ImportColumnMetaInfo(el, el.QualifiedName, col);
			this.AddColumn(col);
			this.currentTable.PrimaryKey = col;
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x000585D0 File Offset: 0x000567D0
		private void FillDataColumnRepeatedSimpleElement(XmlSchemaElement parent, XmlSchemaElement el, DataColumn col)
		{
			if (this.targetElements.Contains(el))
			{
				return;
			}
			this.AddParentKeyColumn(parent, el, col);
			DataColumn primaryKey = this.currentTable.PrimaryKey;
			string text = XmlHelper.Decode(el.QualifiedName.Name);
			string text2 = XmlHelper.Decode(parent.QualifiedName.Name);
			DataTable dataTable = new DataTable();
			dataTable.TableName = text;
			dataTable.Namespace = el.QualifiedName.Namespace;
			DataColumn dataColumn = new DataColumn();
			dataColumn.ColumnName = text2 + "_Id";
			dataColumn.Namespace = parent.QualifiedName.Namespace;
			dataColumn.ColumnMapping = MappingType.Hidden;
			dataColumn.DataType = typeof(int);
			DataColumn dataColumn2 = new DataColumn();
			dataColumn2.ColumnName = text + "_Column";
			dataColumn2.Namespace = el.QualifiedName.Namespace;
			dataColumn2.ColumnMapping = MappingType.SimpleContent;
			dataColumn2.AllowDBNull = false;
			dataColumn2.DataType = this.ConvertDatatype(this.GetSchemaPrimitiveType(el.ElementSchemaType));
			dataTable.Columns.Add(dataColumn2);
			dataTable.Columns.Add(dataColumn);
			this.dataset.Tables.Add(dataTable);
			RelationStructure relationStructure = new RelationStructure();
			relationStructure.ParentTableName = text2;
			relationStructure.ChildTableName = dataTable.TableName;
			relationStructure.ParentColumnName = primaryKey.ColumnName;
			relationStructure.ChildColumnName = dataColumn.ColumnName;
			relationStructure.IsNested = true;
			relationStructure.CreateConstraint = true;
			this.relations.Add(relationStructure);
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0005875C File Offset: 0x0005695C
		private void FillDataColumnSimpleElement(XmlSchemaElement el, DataColumn col)
		{
			col.ColumnName = XmlHelper.Decode(el.QualifiedName.Name);
			col.Namespace = el.QualifiedName.Namespace;
			col.ColumnMapping = MappingType.Element;
			col.DataType = this.ConvertDatatype(this.GetSchemaPrimitiveType(el.ElementSchemaType));
			this.FillFacet(col, el.ElementSchemaType as XmlSchemaSimpleType);
			this.ImportColumnMetaInfo(el, el.QualifiedName, col);
			this.AddColumn(col);
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x000587D8 File Offset: 0x000569D8
		private void AddColumn(DataColumn col)
		{
			if (col.Ordinal < 0)
			{
				this.currentTable.NonOrdinalColumns.Add(col);
			}
			else
			{
				this.currentTable.OrdinalColumns.Add(col, col.Ordinal);
			}
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x00058824 File Offset: 0x00056A24
		private void FillFacet(DataColumn col, XmlSchemaSimpleType st)
		{
			if (st == null || st.Content == null)
			{
				return;
			}
			XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = ((st != null) ? (st.Content as XmlSchemaSimpleTypeRestriction) : null);
			if (xmlSchemaSimpleTypeRestriction == null)
			{
				throw new DataException("DataSet does not suport 'list' nor 'union' simple type.");
			}
			foreach (XmlSchemaObject xmlSchemaObject in xmlSchemaSimpleTypeRestriction.Facets)
			{
				XmlSchemaFacet xmlSchemaFacet = (XmlSchemaFacet)xmlSchemaObject;
				if (xmlSchemaFacet is XmlSchemaMaxLengthFacet)
				{
					col.MaxLength = int.Parse(xmlSchemaFacet.Value);
				}
			}
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x000588E4 File Offset: 0x00056AE4
		private Type ConvertDatatype(XmlSchemaDatatype dt)
		{
			if (dt == null)
			{
				return typeof(string);
			}
			if (dt.ValueType != typeof(decimal))
			{
				return dt.ValueType;
			}
			if (dt == XmlSchemaDataImporter.schemaDecimalType)
			{
				return typeof(decimal);
			}
			if (dt == XmlSchemaDataImporter.schemaIntegerType)
			{
				return typeof(long);
			}
			return typeof(ulong);
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x00058954 File Offset: 0x00056B54
		private string GetSelectorTarget(string xpath)
		{
			string text = xpath;
			int num = text.LastIndexOf('/');
			if (num > 0)
			{
				text = text.Substring(num + 1);
			}
			num = text.LastIndexOf(':');
			if (num > 0)
			{
				text = text.Substring(num + 1);
			}
			return XmlHelper.Decode(text);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x000589A0 File Offset: 0x00056BA0
		private void ReserveSelfIdentity(XmlSchemaIdentityConstraint ic)
		{
			string selectorTarget = this.GetSelectorTarget(ic.Selector.XPath);
			string[] array = new string[ic.Fields.Count];
			bool[] array2 = new bool[array.Length];
			int num = 0;
			foreach (XmlSchemaObject xmlSchemaObject in ic.Fields)
			{
				XmlSchemaXPath xmlSchemaXPath = (XmlSchemaXPath)xmlSchemaObject;
				string text = xmlSchemaXPath.XPath;
				bool flag = text.Length > 0 && text[0] == '@';
				int num2 = text.LastIndexOf(':');
				if (num2 > 0)
				{
					text = text.Substring(num2 + 1);
				}
				else if (flag)
				{
					text = text.Substring(1);
				}
				text = XmlHelper.Decode(text);
				array[num] = text;
				array2[num] = flag;
				num++;
			}
			bool flag2 = false;
			string text2 = ic.Name;
			if (ic.UnhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in ic.UnhandledAttributes)
				{
					if (!(xmlAttribute.NamespaceURI != "urn:schemas-microsoft-com:xml-msdata"))
					{
						string localName = xmlAttribute.LocalName;
						if (localName != null)
						{
							if (XmlSchemaDataImporter.<>f__switch$mapD == null)
							{
								XmlSchemaDataImporter.<>f__switch$mapD = new Dictionary<string, int>(2)
								{
									{ "ConstraintName", 0 },
									{ "PrimaryKey", 1 }
								};
							}
							int num3;
							if (XmlSchemaDataImporter.<>f__switch$mapD.TryGetValue(localName, out num3))
							{
								if (num3 != 0)
								{
									if (num3 == 1)
									{
										flag2 = bool.Parse(xmlAttribute.Value);
									}
								}
								else
								{
									text2 = xmlAttribute.Value;
								}
							}
						}
					}
				}
			}
			this.reservedConstraints.Add(ic, new ConstraintStructure(selectorTarget, array, array2, text2, flag2, null, false, false));
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00058BB4 File Offset: 0x00056DB4
		private void ProcessSelfIdentity(ConstraintStructure c)
		{
			string tableName = c.TableName;
			DataTable dataTable = this.dataset.Tables[tableName];
			if (dataTable != null)
			{
				DataColumn[] array = new DataColumn[c.Columns.Length];
				for (int i = 0; i < array.Length; i++)
				{
					string text = c.Columns[i];
					bool flag = c.IsAttribute[i];
					DataColumn dataColumn = dataTable.Columns[text];
					if (dataColumn == null)
					{
						throw new DataException(string.Format("Invalid XPath selection inside field. Cannot find: {0}", tableName));
					}
					if (flag && dataColumn.ColumnMapping != MappingType.Attribute)
					{
						throw new DataException("The XPath specified attribute field, but mapping type is not attribute.");
					}
					if (!flag && dataColumn.ColumnMapping != MappingType.Element)
					{
						throw new DataException("The XPath specified simple element field, but mapping type is not simple element.");
					}
					array[i] = dataTable.Columns[text];
				}
				bool isPrimaryKey = c.IsPrimaryKey;
				string constraintName = c.ConstraintName;
				dataTable.Constraints.Add(new UniqueConstraint(constraintName, array, isPrimaryKey));
				return;
			}
			if (this.forDataSet)
			{
				throw new DataException(string.Format("Invalid XPath selection inside selector. Cannot find: {0}", tableName));
			}
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00058CD0 File Offset: 0x00056ED0
		private void ReserveRelationIdentity(XmlSchemaElement element, XmlSchemaKeyref keyref)
		{
			string selectorTarget = this.GetSelectorTarget(keyref.Selector.XPath);
			string[] array = new string[keyref.Fields.Count];
			bool[] array2 = new bool[array.Length];
			int num = 0;
			foreach (XmlSchemaObject xmlSchemaObject in keyref.Fields)
			{
				XmlSchemaXPath xmlSchemaXPath = (XmlSchemaXPath)xmlSchemaObject;
				string text = xmlSchemaXPath.XPath;
				bool flag = text.Length > 0 && text[0] == '@';
				int num2 = text.LastIndexOf(':');
				if (num2 > 0)
				{
					text = text.Substring(num2 + 1);
				}
				else if (flag)
				{
					text = text.Substring(1);
				}
				text = XmlHelper.Decode(text);
				array[num] = text;
				array2[num] = flag;
				num++;
			}
			string text2 = keyref.Name;
			bool flag2 = false;
			bool flag3 = false;
			if (keyref.UnhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in keyref.UnhandledAttributes)
				{
					if (!(xmlAttribute.NamespaceURI != "urn:schemas-microsoft-com:xml-msdata"))
					{
						string localName = xmlAttribute.LocalName;
						switch (localName)
						{
						case "ConstraintName":
							text2 = xmlAttribute.Value;
							break;
						case "IsNested":
							if (xmlAttribute.Value == "true")
							{
								flag2 = true;
							}
							break;
						case "ConstraintOnly":
							if (xmlAttribute.Value == "true")
							{
								flag3 = true;
							}
							break;
						}
					}
				}
			}
			this.reservedConstraints.Add(keyref, new ConstraintStructure(selectorTarget, array, array2, text2, false, keyref.Refer.Name, flag2, flag3));
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00058F2C File Offset: 0x0005712C
		private void ProcessRelationIdentity(XmlSchemaElement element, ConstraintStructure c)
		{
			string tableName = c.TableName;
			DataTable dataTable = this.dataset.Tables[tableName];
			if (dataTable == null)
			{
				throw new DataException(string.Format("Invalid XPath selection inside selector. Cannot find: {0}", tableName));
			}
			DataColumn[] array = new DataColumn[c.Columns.Length];
			for (int i = 0; i < array.Length; i++)
			{
				string text = c.Columns[i];
				bool flag = c.IsAttribute[i];
				DataColumn dataColumn = dataTable.Columns[text];
				if (flag && dataColumn.ColumnMapping != MappingType.Attribute)
				{
					throw new DataException("The XPath specified attribute field, but mapping type is not attribute.");
				}
				if (!flag && dataColumn.ColumnMapping != MappingType.Element)
				{
					throw new DataException("The XPath specified simple element field, but mapping type is not simple element.");
				}
				array[i] = dataColumn;
			}
			string referName = c.ReferName;
			UniqueConstraint uniqueConstraint = this.FindConstraint(referName, element);
			ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(c.ConstraintName, uniqueConstraint.Columns, array);
			dataTable.Constraints.Add(foreignKeyConstraint);
			if (!c.IsConstraintOnly)
			{
				DataRelation dataRelation = new DataRelation(c.ConstraintName, uniqueConstraint.Columns, array, true);
				dataRelation.Nested = c.IsNested;
				dataRelation.SetParentKeyConstraint(uniqueConstraint);
				dataRelation.SetChildKeyConstraint(foreignKeyConstraint);
				this.dataset.Relations.Add(dataRelation);
			}
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00059078 File Offset: 0x00057278
		private UniqueConstraint FindConstraint(string name, XmlSchemaElement element)
		{
			foreach (XmlSchemaObject xmlSchemaObject in element.Constraints)
			{
				XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = (XmlSchemaIdentityConstraint)xmlSchemaObject;
				if (!(xmlSchemaIdentityConstraint is XmlSchemaKeyref))
				{
					if (xmlSchemaIdentityConstraint.Name == name)
					{
						string selectorTarget = this.GetSelectorTarget(xmlSchemaIdentityConstraint.Selector.XPath);
						DataTable dataTable = this.dataset.Tables[selectorTarget];
						string text = xmlSchemaIdentityConstraint.Name;
						if (xmlSchemaIdentityConstraint.UnhandledAttributes != null)
						{
							foreach (XmlAttribute xmlAttribute in xmlSchemaIdentityConstraint.UnhandledAttributes)
							{
								if (xmlAttribute.LocalName == "ConstraintName" && xmlAttribute.NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
								{
									text = xmlAttribute.Value;
								}
							}
						}
						return (UniqueConstraint)dataTable.Constraints[text];
					}
				}
			}
			throw new DataException("Target identity constraint was not found: " + name);
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x000591C4 File Offset: 0x000573C4
		private void HandleAnnotations(XmlSchemaAnnotation an, bool nested)
		{
			foreach (XmlSchemaObject xmlSchemaObject in an.Items)
			{
				XmlSchemaAppInfo xmlSchemaAppInfo = xmlSchemaObject as XmlSchemaAppInfo;
				if (xmlSchemaAppInfo != null)
				{
					foreach (XmlNode xmlNode in xmlSchemaAppInfo.Markup)
					{
						XmlElement xmlElement = xmlNode as XmlElement;
						if (xmlElement != null && xmlElement.LocalName == "Relationship" && xmlElement.NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
						{
							this.HandleRelationshipAnnotation(xmlElement, nested);
						}
						if (xmlElement != null && xmlElement.LocalName == "DataSource" && xmlElement.NamespaceURI == "urn:schemas-microsoft-com:xml-msdatasource")
						{
							this.HandleDataSourceAnnotation(xmlElement, nested);
						}
					}
				}
			}
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x000592E0 File Offset: 0x000574E0
		private void HandleDataSourceAnnotation(XmlElement el, bool nested)
		{
			string text = null;
			DbProviderFactory dbProviderFactory = null;
			XmlElement xmlElement = null;
			foreach (object obj in el.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlElement xmlElement2 = xmlNode as XmlElement;
				if (xmlElement2 != null)
				{
					XmlElement xmlElement3;
					if (xmlElement2.LocalName == "Connections" && (xmlElement3 = xmlElement2.FirstChild as XmlElement) != null)
					{
						string attribute = xmlElement3.GetAttribute("Provider");
						text = xmlElement3.GetAttribute("AppSettingsPropertyName");
						dbProviderFactory = DbProviderFactories.GetFactory(attribute);
					}
					else if (xmlElement2.LocalName == "Tables")
					{
						xmlElement = xmlElement2;
					}
				}
			}
			if (xmlElement != null && dbProviderFactory != null)
			{
				foreach (object obj2 in xmlElement.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					this.ProcessTableAdapter(xmlNode2 as XmlElement, dbProviderFactory, text);
				}
			}
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x00059450 File Offset: 0x00057650
		private void ProcessTableAdapter(XmlElement el, DbProviderFactory provider, string connStr)
		{
			string text = null;
			if (el == null)
			{
				return;
			}
			this.currentAdapter = new TableAdapterSchemaInfo(provider);
			this.currentAdapter.ConnectionString = connStr;
			this.currentAdapter.BaseClass = el.GetAttribute("BaseClass");
			text = el.GetAttribute("Name");
			this.currentAdapter.Name = el.GetAttribute("GeneratorDataComponentClassName");
			if (string.IsNullOrEmpty(this.currentAdapter.Name))
			{
				this.currentAdapter.Name = el.GetAttribute("DataAccessorName");
			}
			foreach (object obj in el.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement != null)
				{
					string localName = xmlElement.LocalName;
					if (localName != null)
					{
						if (XmlSchemaDataImporter.<>f__switch$mapF == null)
						{
							XmlSchemaDataImporter.<>f__switch$mapF = new Dictionary<string, int>(3)
							{
								{ "MainSource", 0 },
								{ "Sources", 0 },
								{ "Mappings", 1 }
							};
						}
						int num;
						if (XmlSchemaDataImporter.<>f__switch$mapF.TryGetValue(localName, out num))
						{
							if (num != 0)
							{
								if (num == 1)
								{
									DataTableMapping dataTableMapping = new DataTableMapping();
									dataTableMapping.SourceTable = "Table";
									dataTableMapping.DataSetTable = text;
									foreach (object obj2 in xmlElement.ChildNodes)
									{
										XmlNode xmlNode2 = (XmlNode)obj2;
										this.ProcessColumnMapping(xmlNode2 as XmlElement, dataTableMapping);
									}
									this.currentAdapter.Adapter.TableMappings.Add(dataTableMapping);
								}
							}
							else
							{
								foreach (object obj3 in xmlElement.ChildNodes)
								{
									XmlNode xmlNode3 = (XmlNode)obj3;
									this.ProcessDbSource(xmlNode3 as XmlElement);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x000596E0 File Offset: 0x000578E0
		private void ProcessDbSource(XmlElement el)
		{
			if (el == null)
			{
				return;
			}
			string text = el.GetAttribute("GenerateShortCommands");
			if (!string.IsNullOrEmpty(text))
			{
				this.currentAdapter.ShortCommands = Convert.ToBoolean(text);
			}
			DbCommandInfo dbCommandInfo = new DbCommandInfo();
			text = el.GetAttribute("GenerateMethods");
			if (!string.IsNullOrEmpty(text))
			{
				switch ((int)Enum.Parse(typeof(GenerateMethodsType), text))
				{
				case 1:
				{
					DbSourceMethodInfo dbSourceMethodInfo = new DbSourceMethodInfo();
					dbSourceMethodInfo.Name = el.GetAttribute("GetMethodName");
					dbSourceMethodInfo.Modifier = el.GetAttribute("GetMethodModifier");
					if (string.IsNullOrEmpty(dbSourceMethodInfo.Modifier))
					{
						dbSourceMethodInfo.Modifier = "Public";
					}
					dbSourceMethodInfo.ScalarCallRetval = el.GetAttribute("ScalarCallRetval");
					dbSourceMethodInfo.QueryType = el.GetAttribute("QueryType");
					dbSourceMethodInfo.MethodType = GenerateMethodsType.Get;
					dbCommandInfo.Methods = new DbSourceMethodInfo[1];
					dbCommandInfo.Methods[0] = dbSourceMethodInfo;
					break;
				}
				case 2:
				{
					DbSourceMethodInfo dbSourceMethodInfo = new DbSourceMethodInfo();
					dbSourceMethodInfo.Name = el.GetAttribute("FillMethodName");
					dbSourceMethodInfo.Modifier = el.GetAttribute("FillMethodModifier");
					if (string.IsNullOrEmpty(dbSourceMethodInfo.Modifier))
					{
						dbSourceMethodInfo.Modifier = "Public";
					}
					dbSourceMethodInfo.ScalarCallRetval = null;
					dbSourceMethodInfo.QueryType = null;
					dbSourceMethodInfo.MethodType = GenerateMethodsType.Fill;
					dbCommandInfo.Methods = new DbSourceMethodInfo[1];
					dbCommandInfo.Methods[0] = dbSourceMethodInfo;
					break;
				}
				case 3:
				{
					DbSourceMethodInfo dbSourceMethodInfo = new DbSourceMethodInfo();
					dbSourceMethodInfo.Name = el.GetAttribute("GetMethodName");
					dbSourceMethodInfo.Modifier = el.GetAttribute("GetMethodModifier");
					if (string.IsNullOrEmpty(dbSourceMethodInfo.Modifier))
					{
						dbSourceMethodInfo.Modifier = "Public";
					}
					dbSourceMethodInfo.ScalarCallRetval = el.GetAttribute("ScalarCallRetval");
					dbSourceMethodInfo.QueryType = el.GetAttribute("QueryType");
					dbSourceMethodInfo.MethodType = GenerateMethodsType.Get;
					dbCommandInfo.Methods = new DbSourceMethodInfo[2];
					dbCommandInfo.Methods[0] = dbSourceMethodInfo;
					dbSourceMethodInfo = new DbSourceMethodInfo();
					dbSourceMethodInfo.Name = el.GetAttribute("FillMethodName");
					dbSourceMethodInfo.Modifier = el.GetAttribute("FillMethodModifier");
					if (string.IsNullOrEmpty(dbSourceMethodInfo.Modifier))
					{
						dbSourceMethodInfo.Modifier = "Public";
					}
					dbSourceMethodInfo.ScalarCallRetval = null;
					dbSourceMethodInfo.QueryType = null;
					dbSourceMethodInfo.MethodType = GenerateMethodsType.Fill;
					dbCommandInfo.Methods[1] = dbSourceMethodInfo;
					break;
				}
				}
			}
			else
			{
				DbSourceMethodInfo dbSourceMethodInfo2 = new DbSourceMethodInfo();
				dbSourceMethodInfo2.Name = el.GetAttribute("Name");
				dbSourceMethodInfo2.Modifier = el.GetAttribute("Modifier");
				if (string.IsNullOrEmpty(dbSourceMethodInfo2.Modifier))
				{
					dbSourceMethodInfo2.Modifier = "Public";
				}
				dbSourceMethodInfo2.ScalarCallRetval = el.GetAttribute("ScalarCallRetval");
				dbSourceMethodInfo2.QueryType = el.GetAttribute("QueryType");
				dbSourceMethodInfo2.MethodType = GenerateMethodsType.None;
				dbCommandInfo.Methods = new DbSourceMethodInfo[1];
				dbCommandInfo.Methods[0] = dbSourceMethodInfo2;
			}
			foreach (object obj in el.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement != null)
				{
					string localName = xmlElement.LocalName;
					switch (localName)
					{
					case "SelectCommand":
						dbCommandInfo.Command = this.ProcessDbCommand(xmlElement.FirstChild as XmlElement);
						this.currentAdapter.Commands.Add(dbCommandInfo);
						break;
					case "InsertCommand":
						this.currentAdapter.Adapter.InsertCommand = this.ProcessDbCommand(xmlElement.FirstChild as XmlElement);
						break;
					case "UpdateCommand":
						this.currentAdapter.Adapter.UpdateCommand = this.ProcessDbCommand(xmlElement.FirstChild as XmlElement);
						break;
					case "DeleteCommand":
						this.currentAdapter.Adapter.DeleteCommand = this.ProcessDbCommand(xmlElement.FirstChild as XmlElement);
						break;
					}
				}
			}
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00059BA4 File Offset: 0x00057DA4
		private DbCommand ProcessDbCommand(XmlElement el)
		{
			string text = null;
			string text2 = null;
			ArrayList arrayList = null;
			if (el == null)
			{
				return null;
			}
			text2 = el.GetAttribute("CommandType");
			foreach (object obj in el.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement != null && xmlElement.LocalName == "CommandText")
				{
					text = xmlElement.InnerText;
				}
				else if (xmlElement != null && xmlElement.LocalName == "Parameters" && !xmlElement.IsEmpty)
				{
					arrayList = this.ProcessDbParameters(xmlElement);
				}
			}
			DbCommand dbCommand = this.currentAdapter.Provider.CreateCommand();
			dbCommand.CommandText = text;
			if (text2 == "StoredProcedure")
			{
				dbCommand.CommandType = CommandType.StoredProcedure;
			}
			else
			{
				dbCommand.CommandType = CommandType.Text;
			}
			if (arrayList != null)
			{
				dbCommand.Parameters.AddRange(arrayList.ToArray());
			}
			return dbCommand;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x00059CE4 File Offset: 0x00057EE4
		private ArrayList ProcessDbParameters(XmlElement el)
		{
			ArrayList arrayList = new ArrayList();
			if (el == null)
			{
				return arrayList;
			}
			foreach (object obj in el.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement != null)
				{
					DbParameter dbParameter = this.currentAdapter.Provider.CreateParameter();
					string text = xmlElement.GetAttribute("AllowDbNull");
					if (!string.IsNullOrEmpty(text))
					{
						dbParameter.IsNullable = Convert.ToBoolean(text);
					}
					dbParameter.ParameterName = xmlElement.GetAttribute("ParameterName");
					text = xmlElement.GetAttribute("ProviderType");
					if (!string.IsNullOrEmpty(text))
					{
						text = xmlElement.GetAttribute("DbType");
					}
					dbParameter.FrameworkDbType = text;
					text = xmlElement.GetAttribute("Direction");
					dbParameter.Direction = (ParameterDirection)((int)Enum.Parse(typeof(ParameterDirection), text));
					((IDbDataParameter)dbParameter).Precision = Convert.ToByte(xmlElement.GetAttribute("Precision"));
					((IDbDataParameter)dbParameter).Scale = Convert.ToByte(xmlElement.GetAttribute("Scale"));
					dbParameter.Size = Convert.ToInt32(xmlElement.GetAttribute("Size"));
					dbParameter.SourceColumn = xmlElement.GetAttribute("SourceColumn");
					text = xmlElement.GetAttribute("SourceColumnNullMapping");
					if (!string.IsNullOrEmpty(text))
					{
						dbParameter.SourceColumnNullMapping = Convert.ToBoolean(text);
					}
					text = xmlElement.GetAttribute("SourceVersion");
					dbParameter.SourceVersion = (DataRowVersion)((int)Enum.Parse(typeof(DataRowVersion), text));
					arrayList.Add(dbParameter);
				}
			}
			return arrayList;
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00059EB8 File Offset: 0x000580B8
		private void ProcessColumnMapping(XmlElement el, DataTableMapping tableMapping)
		{
			if (el == null)
			{
				return;
			}
			tableMapping.ColumnMappings.Add(el.GetAttribute("SourceColumn"), el.GetAttribute("DataSetColumn"));
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00059EF0 File Offset: 0x000580F0
		private void HandleRelationshipAnnotation(XmlElement el, bool nested)
		{
			string attribute = el.GetAttribute("name");
			string attribute2 = el.GetAttribute("parent", "urn:schemas-microsoft-com:xml-msdata");
			string attribute3 = el.GetAttribute("child", "urn:schemas-microsoft-com:xml-msdata");
			string attribute4 = el.GetAttribute("parentkey", "urn:schemas-microsoft-com:xml-msdata");
			string attribute5 = el.GetAttribute("childkey", "urn:schemas-microsoft-com:xml-msdata");
			RelationStructure relationStructure = new RelationStructure();
			relationStructure.ExplicitName = XmlHelper.Decode(attribute);
			relationStructure.ParentTableName = XmlHelper.Decode(attribute2);
			relationStructure.ChildTableName = XmlHelper.Decode(attribute3);
			relationStructure.ParentColumnName = attribute4;
			relationStructure.ChildColumnName = attribute5;
			relationStructure.IsNested = nested;
			relationStructure.CreateConstraint = false;
			this.relations.Add(relationStructure);
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x00059FAC File Offset: 0x000581AC
		private object GetElementDefaultValue(XmlSchemaElement elem)
		{
			if (elem.RefName == XmlQualifiedName.Empty)
			{
				return elem.DefaultValue;
			}
			XmlSchemaElement xmlSchemaElement = this.schema.Elements[elem.RefName] as XmlSchemaElement;
			if (xmlSchemaElement == null)
			{
				return null;
			}
			return xmlSchemaElement.DefaultValue;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0005A000 File Offset: 0x00058200
		private object GetAttributeDefaultValue(XmlSchemaAttribute attr)
		{
			if (attr.DefaultValue != null)
			{
				return attr.DefaultValue;
			}
			if (attr.FixedValue != null)
			{
				return attr.FixedValue;
			}
			if (attr.RefName == XmlQualifiedName.Empty)
			{
				return null;
			}
			XmlSchemaAttribute xmlSchemaAttribute = this.schema.Attributes[attr.RefName] as XmlSchemaAttribute;
			if (xmlSchemaAttribute == null)
			{
				return null;
			}
			if (xmlSchemaAttribute.DefaultValue != null)
			{
				return xmlSchemaAttribute.DefaultValue;
			}
			return xmlSchemaAttribute.FixedValue;
		}

		// Token: 0x04000840 RID: 2112
		private static readonly XmlSchemaDatatype schemaIntegerType;

		// Token: 0x04000841 RID: 2113
		private static readonly XmlSchemaDatatype schemaDecimalType;

		// Token: 0x04000842 RID: 2114
		private static readonly XmlSchemaComplexType schemaAnyType;

		// Token: 0x04000843 RID: 2115
		private DataSet dataset;

		// Token: 0x04000844 RID: 2116
		private bool forDataSet;

		// Token: 0x04000845 RID: 2117
		private XmlSchema schema;

		// Token: 0x04000846 RID: 2118
		private ArrayList relations = new ArrayList();

		// Token: 0x04000847 RID: 2119
		private Hashtable reservedConstraints = new Hashtable();

		// Token: 0x04000848 RID: 2120
		private XmlSchemaElement datasetElement;

		// Token: 0x04000849 RID: 2121
		private ArrayList topLevelElements = new ArrayList();

		// Token: 0x0400084A RID: 2122
		private ArrayList targetElements = new ArrayList();

		// Token: 0x0400084B RID: 2123
		private TableStructure currentTable;

		// Token: 0x0400084C RID: 2124
		private TableAdapterSchemaInfo currentAdapter;
	}
}
