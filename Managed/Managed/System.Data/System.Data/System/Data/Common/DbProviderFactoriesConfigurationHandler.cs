using System;
using System.Configuration;
using System.Xml;

namespace System.Data.Common
{
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000CB RID: 203
	public class DbProviderFactoriesConfigurationHandler : IConfigurationSectionHandler
	{
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060009EB RID: 2539 RVA: 0x0002EB10 File Offset: 0x0002CD10
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			DataSet dataSet = (parent as DataSet) ?? this.CreateDataSet();
			this.FillDataTables(dataSet, section);
			return dataSet;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0002EB3C File Offset: 0x0002CD3C
		private DataSet CreateDataSet()
		{
			DataSet dataSet = new DataSet("system.data");
			DataTable dataTable = dataSet.Tables.Add("DbProviderFactories");
			DataColumn[] array = new DataColumn[]
			{
				new DataColumn("Name", typeof(string)),
				new DataColumn("Description", typeof(string)),
				new DataColumn("InvariantName", typeof(string)),
				new DataColumn("AssemblyQualifiedName", typeof(string))
			};
			dataTable.Columns.AddRange(array);
			dataTable.PrimaryKey = new DataColumn[] { array[2] };
			return dataSet;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0002EBE8 File Offset: 0x0002CDE8
		private void FillDataTables(DataSet ds, XmlNode section)
		{
			DataTable dataTable = ds.Tables[0];
			foreach (object obj in section.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					if (xmlNode.Name == "DbProviderFactories")
					{
						foreach (object obj2 in xmlNode.ChildNodes)
						{
							XmlNode xmlNode2 = (XmlNode)obj2;
							if (xmlNode2.NodeType == XmlNodeType.Element)
							{
								string name = xmlNode2.Name;
								switch (name)
								{
								case "add":
									this.AddRow(dataTable, xmlNode2);
									continue;
								case "clear":
									dataTable.Rows.Clear();
									continue;
								case "remove":
									this.RemoveRow(dataTable, xmlNode2);
									continue;
								}
								throw new ConfigurationErrorsException("Unrecognized element.", xmlNode2);
							}
						}
					}
				}
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0002EDA8 File Offset: 0x0002CFA8
		private string GetAttributeValue(XmlNode node, string name, bool required)
		{
			XmlAttribute xmlAttribute = node.Attributes[name];
			if (xmlAttribute == null)
			{
				if (!required)
				{
					return null;
				}
				throw new ConfigurationErrorsException("Required Attribute '" + name + "' is  missing!", node);
			}
			else
			{
				string value = xmlAttribute.Value;
				if (value == string.Empty)
				{
					throw new ConfigurationException("Attribute '" + name + "' cannot be empty!", node);
				}
				return value;
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0002EE18 File Offset: 0x0002D018
		private void AddRow(DataTable dt, XmlNode addNode)
		{
			string attributeValue = this.GetAttributeValue(addNode, "name", true);
			string attributeValue2 = this.GetAttributeValue(addNode, "description", true);
			string attributeValue3 = this.GetAttributeValue(addNode, "invariant", true);
			string attributeValue4 = this.GetAttributeValue(addNode, "type", true);
			DataRow dataRow = dt.NewRow();
			dataRow[0] = attributeValue;
			dataRow[1] = attributeValue2;
			dataRow[2] = attributeValue3;
			dataRow[3] = attributeValue4;
			dt.Rows.Add(dataRow);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0002EE98 File Offset: 0x0002D098
		private void RemoveRow(DataTable dt, XmlNode removeNode)
		{
			string attributeValue = this.GetAttributeValue(removeNode, "invariant", true);
			DataRow dataRow = dt.Rows.Find(attributeValue);
			if (dataRow != null)
			{
				dataRow.Delete();
			}
		}
	}
}
