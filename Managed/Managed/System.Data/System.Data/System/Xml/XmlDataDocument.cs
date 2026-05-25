using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Allows structured data to be stored, retrieved, and manipulated through a relational <see cref="T:System.Data.DataSet" />. </summary>
	// Token: 0x02000189 RID: 393
	public class XmlDataDocument : XmlDocument
	{
		/// <summary>Initializes a new instance of the XmlDataDocument class.</summary>
		// Token: 0x060014D2 RID: 5330 RVA: 0x0005C3BC File Offset: 0x0005A5BC
		public XmlDataDocument()
		{
			this.InitDelegateFields();
			this.dataSet = new DataSet();
			this.dataSet._xmlDataDocument = this;
			this.dataSet.Tables.CollectionChanged += this.tablesChanged;
			this.AddXmlDocumentListeners();
			this.DataSet.EnforceConstraints = false;
		}

		/// <summary>Initializes a new instance of the XmlDataDocument class with the specified <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="dataset">The DataSet to load into XmlDataDocument. </param>
		// Token: 0x060014D3 RID: 5331 RVA: 0x0005C434 File Offset: 0x0005A634
		public XmlDataDocument(DataSet dataset)
		{
			if (dataset == null)
			{
				throw new ArgumentException("Parameter dataset cannot be null.");
			}
			if (dataset._xmlDataDocument != null)
			{
				throw new ArgumentException("DataSet cannot be associated with two or more XmlDataDocument.");
			}
			this.InitDelegateFields();
			this.dataSet = dataset;
			this.dataSet._xmlDataDocument = this;
			XmlElement xmlElement = this.CreateElement(this.dataSet.Prefix, XmlHelper.Encode(this.dataSet.DataSetName), this.dataSet.Namespace);
			foreach (object obj in this.dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				if (dataTable.ParentRelations.Count <= 0)
				{
					this.FillNodeRows(xmlElement, dataTable, dataTable.Rows);
				}
			}
			if (xmlElement.ChildNodes.Count > 0)
			{
				this.AppendChild(xmlElement);
			}
			foreach (object obj2 in this.dataSet.Tables)
			{
				DataTable dataTable2 = (DataTable)obj2;
				dataTable2.ColumnChanged += this.columnChanged;
				dataTable2.RowDeleted += this.rowDeleted;
				dataTable2.RowChanged += this.rowChanged;
			}
			this.AddXmlDocumentListeners();
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0005C604 File Offset: 0x0005A804
		private XmlDataDocument(DataSet dataset, bool clone)
		{
			this.InitDelegateFields();
			this.dataSet = dataset;
			this.dataSet._xmlDataDocument = this;
			foreach (object obj in this.DataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					dataRow.XmlRowID = this.dataRowID;
					this.dataRowIDList.Add(this.dataRowID);
					this.dataRowID++;
				}
			}
			this.AddXmlDocumentListeners();
			foreach (object obj3 in this.dataSet.Tables)
			{
				DataTable dataTable2 = (DataTable)obj3;
				dataTable2.ColumnChanged += this.columnChanged;
				dataTable2.RowDeleted += this.rowDeleted;
				dataTable2.RowChanged += this.rowChanged;
			}
		}

		/// <summary>Gets a <see cref="T:System.Data.DataSet" /> that provides a relational representation of the data in the XmlDataDocument.</summary>
		/// <returns>A DataSet that can be used to access the data in the XmlDataDocument using a relational model.</returns>
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x0005C7D0 File Offset: 0x0005A9D0
		public DataSet DataSet
		{
			get
			{
				return this.dataSet;
			}
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0005C7D8 File Offset: 0x0005A9D8
		private void FillNodeRows(XmlElement parent, DataTable dt, ICollection rows)
		{
			foreach (object obj in dt.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				XmlDataDocument.XmlDataElement dataElement = dataRow.DataElement;
				this.FillNodeChildrenFromRow(dataRow, dataElement);
				foreach (object obj2 in dt.ChildRelations)
				{
					DataRelation dataRelation = (DataRelation)obj2;
					this.FillNodeRows(dataElement, dataRelation.ChildTable, dataRow.GetChildRows(dataRelation));
				}
				parent.AppendChild(dataElement);
			}
		}

		/// <summary>Creates a duplicate of the current node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060014D7 RID: 5335 RVA: 0x0005C8CC File Offset: 0x0005AACC
		public override XmlNode CloneNode(bool deep)
		{
			XmlDataDocument xmlDataDocument;
			if (deep)
			{
				xmlDataDocument = new XmlDataDocument(this.DataSet.Copy(), true);
			}
			else
			{
				xmlDataDocument = new XmlDataDocument(this.DataSet.Clone(), true);
			}
			xmlDataDocument.RemoveXmlDocumentListeners();
			xmlDataDocument.PreserveWhitespace = base.PreserveWhitespace;
			if (deep)
			{
				foreach (object obj in this.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					xmlDataDocument.AppendChild(xmlDataDocument.ImportNode(xmlNode, deep));
				}
			}
			xmlDataDocument.AddXmlDocumentListeners();
			return xmlDataDocument;
		}

		/// <summary>Creates an element with the specified <see cref="P:System.Xml.XmlNode.Prefix" />, <see cref="P:System.Xml.XmlDocument.LocalName" />, and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <returns>A new <see cref="T:System.Xml.XmlElement" />.</returns>
		/// <param name="prefix">The prefix of the new element If String.Empty or null, there is no prefix. </param>
		/// <param name="localName">The local name of the new element. </param>
		/// <param name="namespaceURI">The namespace Uniform Resource Identifier (URI) of the new element. If String.Empty or null, there is no namespaceURI. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060014D8 RID: 5336 RVA: 0x0005C994 File Offset: 0x0005AB94
		public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
		{
			DataTable dataTable = this.DataSet.Tables[XmlHelper.Decode(localName)];
			DataRow dataRow = ((dataTable == null) ? null : dataTable.NewRow());
			if (dataRow != null)
			{
				return this.GetElementFromRow(dataRow);
			}
			return base.CreateElement(prefix, localName, namespaceURI);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlEntityReference" /> with the specified name. <see cref="T:System.Xml.XmlEntityReference" /> nodes cannot be created for <see cref="T:System.Xml.XmlDataDocument" /> objects. Calling this method throws an exception.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlEntityReference" /> with the specified name.</returns>
		/// <param name="name">The name of the entity reference.</param>
		/// <exception cref="T:System.NotSupportedException">Calling this method.</exception>
		// Token: 0x060014D9 RID: 5337 RVA: 0x0005C9E4 File Offset: 0x0005ABE4
		public override XmlEntityReference CreateEntityReference(string name)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlElement" /> with the specified ID. This method is not supported by the <see cref="T:System.Xml.XmlDataDocument" /> class. Calling this method throws an exception.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> with the specified ID.</returns>
		/// <param name="elemId">The attribute ID to match.</param>
		/// <exception cref="T:System.NotSupportedException">Calling this method.</exception>
		// Token: 0x060014DA RID: 5338 RVA: 0x0005C9EC File Offset: 0x0005ABEC
		public override XmlElement GetElementById(string elemId)
		{
			throw new NotSupportedException();
		}

		/// <summary>Retrieves the <see cref="T:System.Xml.XmlElement" /> associated with the specified <see cref="T:System.Data.DataRow" />.</summary>
		/// <returns>The XmlElement containing a representation of the specified DataRow.</returns>
		/// <param name="r">The DataRow whose associated XmlElement you wish to retrieve. </param>
		// Token: 0x060014DB RID: 5339 RVA: 0x0005C9F4 File Offset: 0x0005ABF4
		public XmlElement GetElementFromRow(DataRow r)
		{
			return r.DataElement;
		}

		/// <summary>Retrieves the <see cref="T:System.Data.DataRow" /> associated with the specified <see cref="T:System.Xml.XmlElement" />.</summary>
		/// <returns>The DataRow containing a representation of the XmlElement; null if there is no DataRow associated with the XmlElement.</returns>
		/// <param name="e">The XmlElement whose associated DataRow you wish to retrieve. </param>
		// Token: 0x060014DC RID: 5340 RVA: 0x0005C9FC File Offset: 0x0005ABFC
		public DataRow GetRowFromElement(XmlElement e)
		{
			XmlDataDocument.XmlDataElement xmlDataElement = e as XmlDataDocument.XmlDataElement;
			if (xmlDataElement == null)
			{
				return null;
			}
			return xmlDataElement.DataRow;
		}

		/// <summary>Loads the XmlDataDocument from the specified stream.</summary>
		/// <param name="inStream">The stream containing the XML document to load. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060014DD RID: 5341 RVA: 0x0005CA20 File Offset: 0x0005AC20
		public override void Load(Stream inStream)
		{
			this.Load(new XmlTextReader(inStream));
		}

		/// <summary>Loads the XmlDataDocument using the specified URL.</summary>
		/// <param name="filename">URL for the file containing the XML document to load. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060014DE RID: 5342 RVA: 0x0005CA30 File Offset: 0x0005AC30
		public override void Load(string filename)
		{
			this.Load(new XmlTextReader(filename));
		}

		/// <summary>Loads the XmlDataDocument from the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="txtReader">The TextReader used to feed the XML data into the document. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060014DF RID: 5343 RVA: 0x0005CA40 File Offset: 0x0005AC40
		public override void Load(TextReader txtReader)
		{
			this.Load(new XmlTextReader(txtReader));
		}

		/// <summary>Loads the XmlDataDocument from the specified <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">XmlReader containing the XML document to load. </param>
		/// <exception cref="T:System.NotSupportedException">The XML being loaded contains entity references, and the reader cannot resolve entities. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060014E0 RID: 5344 RVA: 0x0005CA50 File Offset: 0x0005AC50
		public override void Load(XmlReader reader)
		{
			if (base.DocumentElement != null)
			{
				throw new InvalidOperationException("XmlDataDocument does not support multi-time loading. New XmlDadaDocument is always required.");
			}
			bool enforceConstraints = this.DataSet.EnforceConstraints;
			this.DataSet.EnforceConstraints = false;
			this.dataSet.Tables.CollectionChanged -= this.tablesChanged;
			base.Load(reader);
			this.DataSet.EnforceConstraints = enforceConstraints;
			this.dataSet.Tables.CollectionChanged += this.tablesChanged;
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XPath.XPathNavigator" /> object for navigating this document. The XPathNavigator is positioned on the node specified in the <paramref name="node" /> parameter.</summary>
		/// <returns>An XPathNavigator.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> you want the navigator initially positioned on. </param>
		// Token: 0x060014E1 RID: 5345 RVA: 0x0005CACC File Offset: 0x0005ACCC
		[MonoTODO("Create optimized XPathNavigator")]
		protected override XPathNavigator CreateNavigator(XmlNode node)
		{
			return base.CreateNavigator(node);
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0005CAD8 File Offset: 0x0005ACD8
		private void OnNodeChanging(object sender, XmlNodeChangedEventArgs args)
		{
			if (!this.raiseDocumentEvents)
			{
				return;
			}
			if (this.DataSet.EnforceConstraints)
			{
				throw new InvalidOperationException(Locale.GetText("Please set DataSet.EnforceConstraints == false before trying to edit XmlDataDocument using XML operations."));
			}
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0005CB14 File Offset: 0x0005AD14
		private void OnNodeChanged(object sender, XmlNodeChangedEventArgs args)
		{
			if (!this.raiseDocumentEvents)
			{
				return;
			}
			bool flag = this.raiseDataSetEvents;
			this.raiseDataSetEvents = false;
			try
			{
				if (args.Node != null)
				{
					DataRow rowFromElement = this.GetRowFromElement((XmlElement)args.Node.ParentNode.ParentNode);
					if (rowFromElement != null)
					{
						if (rowFromElement.Table.Columns.Contains(args.Node.ParentNode.Name))
						{
							if (rowFromElement[args.Node.ParentNode.Name].ToString() != args.Node.InnerText)
							{
								DataColumn dataColumn = rowFromElement.Table.Columns[args.Node.ParentNode.Name];
								rowFromElement[dataColumn] = XmlDataDocument.StringToObject(dataColumn.DataType, args.Node.InnerText);
							}
						}
					}
				}
			}
			finally
			{
				this.raiseDataSetEvents = flag;
			}
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0005CC34 File Offset: 0x0005AE34
		private void OnNodeRemoving(object sender, XmlNodeChangedEventArgs args)
		{
			if (!this.raiseDocumentEvents)
			{
				return;
			}
			if (this.DataSet.EnforceConstraints)
			{
				throw new InvalidOperationException(Locale.GetText("Please set DataSet.EnforceConstraints == false before trying to edit XmlDataDocument using XML operations."));
			}
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0005CC70 File Offset: 0x0005AE70
		private void OnNodeRemoved(object sender, XmlNodeChangedEventArgs args)
		{
			if (!this.raiseDocumentEvents)
			{
				return;
			}
			bool flag = this.raiseDataSetEvents;
			this.raiseDataSetEvents = false;
			try
			{
				if (args.OldParent != null)
				{
					XmlElement xmlElement = args.OldParent as XmlElement;
					if (xmlElement != null)
					{
						XmlElement xmlElement2 = args.Node as XmlElement;
						if (xmlElement2 != null)
						{
							DataRow rowFromElement = this.GetRowFromElement(xmlElement2);
							if (rowFromElement != null)
							{
								rowFromElement.Table.Rows.Remove(rowFromElement);
							}
						}
						DataRow rowFromElement2 = this.GetRowFromElement(xmlElement);
						if (rowFromElement2 != null)
						{
							rowFromElement2[args.Node.Name] = null;
						}
					}
				}
			}
			finally
			{
				this.raiseDataSetEvents = flag;
			}
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0005CD40 File Offset: 0x0005AF40
		private void OnNodeInserting(object sender, XmlNodeChangedEventArgs args)
		{
			if (!this.raiseDocumentEvents)
			{
				return;
			}
			if (this.DataSet.EnforceConstraints)
			{
				throw new InvalidOperationException(Locale.GetText("Please set DataSet.EnforceConstraints == false before trying to edit XmlDataDocument using XML operations."));
			}
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0005CD7C File Offset: 0x0005AF7C
		private void OnNodeInserted(object sender, XmlNodeChangedEventArgs args)
		{
			if (!this.raiseDocumentEvents)
			{
				return;
			}
			bool flag = this.raiseDataSetEvents;
			this.raiseDataSetEvents = false;
			try
			{
				if (!(args.NewParent is XmlElement))
				{
					foreach (object obj in args.Node.ChildNodes)
					{
						XmlNode xmlNode = (XmlNode)obj;
						this.CheckDescendantRelationship(xmlNode);
					}
				}
				else
				{
					DataRow rowFromElement = this.GetRowFromElement(args.NewParent as XmlElement);
					if (rowFromElement == null)
					{
						if (args.NewParent == base.DocumentElement)
						{
							this.CheckDescendantRelationship(args.Node);
						}
					}
					else
					{
						XmlAttribute xmlAttribute = args.Node as XmlAttribute;
						if (xmlAttribute != null)
						{
							DataColumn dataColumn = rowFromElement.Table.Columns[XmlHelper.Decode(xmlAttribute.LocalName)];
							if (dataColumn != null)
							{
								rowFromElement[dataColumn] = XmlDataDocument.StringToObject(dataColumn.DataType, args.Node.Value);
							}
						}
						else
						{
							DataRow rowFromElement2 = this.GetRowFromElement(args.Node as XmlElement);
							if (rowFromElement2 != null)
							{
								if (rowFromElement2.RowState != DataRowState.Detached && rowFromElement.RowState != DataRowState.Detached)
								{
									this.FillRelationship(rowFromElement, rowFromElement2, args.NewParent, args.Node);
								}
							}
							else if (args.Node.NodeType == XmlNodeType.Element)
							{
								DataColumn dataColumn2 = rowFromElement.Table.Columns[XmlHelper.Decode(args.Node.LocalName)];
								if (dataColumn2 != null)
								{
									rowFromElement[dataColumn2] = XmlDataDocument.StringToObject(dataColumn2.DataType, args.Node.InnerText);
								}
							}
							else if (args.Node is XmlCharacterData && args.Node.NodeType != XmlNodeType.Comment)
							{
								for (int i = 0; i < rowFromElement.Table.Columns.Count; i++)
								{
									DataColumn dataColumn3 = rowFromElement.Table.Columns[i];
									if (dataColumn3.ColumnMapping == MappingType.SimpleContent)
									{
										rowFromElement[dataColumn3] = XmlDataDocument.StringToObject(dataColumn3.DataType, args.Node.Value);
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				this.raiseDataSetEvents = flag;
			}
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0005D010 File Offset: 0x0005B210
		private void CheckDescendantRelationship(XmlNode n)
		{
			XmlElement xmlElement = n as XmlElement;
			DataRow rowFromElement = this.GetRowFromElement(xmlElement);
			if (rowFromElement == null)
			{
				return;
			}
			rowFromElement.Table.Rows.Add(rowFromElement);
			this.CheckDescendantRelationship(n, rowFromElement);
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0005D04C File Offset: 0x0005B24C
		private void CheckDescendantRelationship(XmlNode p, DataRow row)
		{
			foreach (object obj in p.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement != null)
				{
					DataRow rowFromElement = this.GetRowFromElement(xmlElement);
					if (rowFromElement != null)
					{
						rowFromElement.Table.Rows.Add(rowFromElement);
						this.FillRelationship(row, rowFromElement, p, xmlElement);
					}
				}
			}
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0005D0F8 File Offset: 0x0005B2F8
		private void FillRelationship(DataRow row, DataRow childRow, XmlNode parentNode, XmlNode childNode)
		{
			for (int i = 0; i < childRow.Table.ParentRelations.Count; i++)
			{
				DataRelation dataRelation = childRow.Table.ParentRelations[i];
				if (dataRelation.ParentTable == row.Table)
				{
					childRow.SetParentRow(row);
					break;
				}
			}
			this.CheckDescendantRelationship(childNode, childRow);
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0005D160 File Offset: 0x0005B360
		private void OnDataTableChanged(object sender, CollectionChangeEventArgs eventArgs)
		{
			if (!this.raiseDataSetEvents)
			{
				return;
			}
			bool flag = this.raiseDocumentEvents;
			this.raiseDocumentEvents = false;
			try
			{
				DataTable dataTable = (DataTable)eventArgs.Element;
				CollectionChangeAction action = eventArgs.Action;
				if (action != CollectionChangeAction.Add)
				{
					if (action == CollectionChangeAction.Remove)
					{
						dataTable.ColumnChanged -= this.columnChanged;
						dataTable.RowDeleted -= this.rowDeleted;
						dataTable.RowChanged -= this.rowChanged;
					}
				}
				else
				{
					dataTable.ColumnChanged += this.columnChanged;
					dataTable.RowDeleted += this.rowDeleted;
					dataTable.RowChanged += this.rowChanged;
				}
			}
			finally
			{
				this.raiseDocumentEvents = flag;
			}
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0005D228 File Offset: 0x0005B428
		private void OnDataTableColumnChanged(object sender, DataColumnChangeEventArgs eventArgs)
		{
			if (!this.raiseDataSetEvents)
			{
				return;
			}
			bool flag = this.raiseDocumentEvents;
			this.raiseDocumentEvents = false;
			try
			{
				DataRow row = eventArgs.Row;
				XmlElement elementFromRow = this.GetElementFromRow(row);
				if (elementFromRow != null)
				{
					DataColumn column = eventArgs.Column;
					string text = ((!row.IsNull(column)) ? row[column].ToString() : string.Empty);
					switch (column.ColumnMapping)
					{
					case MappingType.Element:
					{
						bool flag2 = false;
						for (int i = 0; i < elementFromRow.ChildNodes.Count; i++)
						{
							XmlElement xmlElement = elementFromRow.ChildNodes[i] as XmlElement;
							if (xmlElement != null && xmlElement.LocalName == XmlHelper.Encode(column.ColumnName) && xmlElement.NamespaceURI == column.Namespace)
							{
								flag2 = true;
								xmlElement.InnerText = text;
								break;
							}
						}
						if (!flag2)
						{
							XmlElement xmlElement2 = this.CreateElement(column.Prefix, XmlHelper.Encode(column.ColumnName), column.Namespace);
							xmlElement2.InnerText = text;
							elementFromRow.AppendChild(xmlElement2);
						}
						break;
					}
					case MappingType.Attribute:
						elementFromRow.SetAttribute(XmlHelper.Encode(column.ColumnName), column.Namespace, text);
						break;
					case MappingType.SimpleContent:
						elementFromRow.InnerText = text;
						break;
					}
				}
			}
			finally
			{
				this.raiseDocumentEvents = flag;
			}
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0005D3CC File Offset: 0x0005B5CC
		private void OnDataTableRowDeleted(object sender, DataRowChangeEventArgs eventArgs)
		{
			if (!this.raiseDataSetEvents)
			{
				return;
			}
			bool flag = this.raiseDocumentEvents;
			this.raiseDocumentEvents = false;
			try
			{
				XmlElement elementFromRow = this.GetElementFromRow(eventArgs.Row);
				if (elementFromRow != null)
				{
					elementFromRow.ParentNode.RemoveChild(elementFromRow);
				}
			}
			finally
			{
				this.raiseDocumentEvents = flag;
			}
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0005D444 File Offset: 0x0005B644
		[MonoTODO("Need to handle hidden columns? - see comments on each private method")]
		private void OnDataTableRowChanged(object sender, DataRowChangeEventArgs eventArgs)
		{
			if (!this.raiseDataSetEvents)
			{
				return;
			}
			bool flag = this.raiseDocumentEvents;
			this.raiseDocumentEvents = false;
			try
			{
				DataRowAction action = eventArgs.Action;
				switch (action)
				{
				case DataRowAction.Delete:
					this.OnDataTableRowDeleted(sender, eventArgs);
					break;
				default:
					if (action == DataRowAction.Add)
					{
						this.OnDataTableRowAdded(eventArgs);
					}
					break;
				case DataRowAction.Rollback:
					this.OnDataTableRowRollback(eventArgs);
					break;
				}
			}
			finally
			{
				this.raiseDocumentEvents = flag;
			}
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0005D4EC File Offset: 0x0005B6EC
		private void OnDataTableRowAdded(DataRowChangeEventArgs args)
		{
			if (!this.raiseDataSetEvents)
			{
				return;
			}
			bool flag = this.raiseDocumentEvents;
			this.raiseDocumentEvents = false;
			try
			{
				DataRow row = args.Row;
				if (base.DocumentElement == null)
				{
					this.AppendChild(base.CreateElement(XmlHelper.Encode(this.DataSet.DataSetName)));
				}
				DataTable table = args.Row.Table;
				XmlElement xmlElement = this.GetElementFromRow(row);
				if (xmlElement == null)
				{
					xmlElement = this.CreateElement(table.Prefix, XmlHelper.Encode(table.TableName), table.Namespace);
				}
				if (xmlElement.ChildNodes.Count == 0)
				{
					this.FillNodeChildrenFromRow(row, xmlElement);
				}
				if (xmlElement.ParentNode == null)
				{
					XmlElement xmlElement2 = null;
					if (table.ParentRelations.Count > 0)
					{
						for (int i = 0; i < table.ParentRelations.Count; i++)
						{
							DataRelation dataRelation = table.ParentRelations[i];
							DataRow parentRow = row.GetParentRow(dataRelation);
							if (parentRow != null)
							{
								xmlElement2 = this.GetElementFromRow(parentRow);
							}
						}
					}
					if (xmlElement2 == null)
					{
						xmlElement2 = base.DocumentElement;
					}
					xmlElement2.AppendChild(xmlElement);
				}
			}
			finally
			{
				this.raiseDocumentEvents = flag;
			}
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0005D644 File Offset: 0x0005B844
		private void FillNodeChildrenFromRow(DataRow row, XmlElement element)
		{
			DataTable table = row.Table;
			for (int i = 0; i < table.Columns.Count; i++)
			{
				DataColumn dataColumn = table.Columns[i];
				string text = ((!row.IsNull(dataColumn)) ? row[dataColumn].ToString() : string.Empty);
				switch (dataColumn.ColumnMapping)
				{
				case MappingType.Element:
				{
					XmlElement xmlElement = this.CreateElement(dataColumn.Prefix, XmlHelper.Encode(dataColumn.ColumnName), dataColumn.Namespace);
					xmlElement.InnerText = text;
					element.AppendChild(xmlElement);
					break;
				}
				case MappingType.Attribute:
				{
					XmlAttribute xmlAttribute = this.CreateAttribute(dataColumn.Prefix, XmlHelper.Encode(dataColumn.ColumnName), dataColumn.Namespace);
					xmlAttribute.Value = text;
					element.SetAttributeNode(xmlAttribute);
					break;
				}
				case MappingType.SimpleContent:
				{
					XmlText xmlText = this.CreateTextNode(text);
					element.AppendChild(xmlText);
					break;
				}
				}
			}
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0005D748 File Offset: 0x0005B948
		[MonoTODO("It does not look complete.")]
		private void OnDataTableRowRollback(DataRowChangeEventArgs args)
		{
			if (!this.raiseDataSetEvents)
			{
				return;
			}
			bool flag = this.raiseDocumentEvents;
			this.raiseDocumentEvents = false;
			try
			{
				DataRow row = args.Row;
				XmlElement elementFromRow = this.GetElementFromRow(row);
				if (elementFromRow != null)
				{
					DataTable table = row.Table;
					ArrayList arrayList = new ArrayList();
					foreach (object obj in elementFromRow.Attributes)
					{
						XmlAttribute xmlAttribute = (XmlAttribute)obj;
						DataColumn dataColumn = table.Columns[XmlHelper.Decode(xmlAttribute.LocalName)];
						if (dataColumn != null)
						{
							if (row.IsNull(dataColumn))
							{
								arrayList.Add(xmlAttribute);
							}
							else
							{
								xmlAttribute.Value = row[dataColumn].ToString();
							}
						}
					}
					foreach (object obj2 in arrayList)
					{
						XmlAttribute xmlAttribute2 = (XmlAttribute)obj2;
						elementFromRow.RemoveAttributeNode(xmlAttribute2);
					}
					arrayList.Clear();
					foreach (object obj3 in elementFromRow.ChildNodes)
					{
						XmlNode xmlNode = (XmlNode)obj3;
						if (xmlNode.NodeType == XmlNodeType.Element)
						{
							DataColumn dataColumn2 = table.Columns[XmlHelper.Decode(xmlNode.LocalName)];
							if (dataColumn2 != null)
							{
								if (row.IsNull(dataColumn2))
								{
									arrayList.Add(xmlNode);
								}
								else
								{
									xmlNode.InnerText = row[dataColumn2].ToString();
								}
							}
						}
					}
					foreach (object obj4 in arrayList)
					{
						XmlNode xmlNode2 = (XmlNode)obj4;
						elementFromRow.RemoveChild(xmlNode2);
					}
				}
			}
			finally
			{
				this.raiseDocumentEvents = flag;
			}
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0005D9FC File Offset: 0x0005BBFC
		private void InitDelegateFields()
		{
			this.columnChanged = new DataColumnChangeEventHandler(this.OnDataTableColumnChanged);
			this.rowDeleted = new DataRowChangeEventHandler(this.OnDataTableRowDeleted);
			this.rowChanged = new DataRowChangeEventHandler(this.OnDataTableRowChanged);
			this.tablesChanged = new CollectionChangeEventHandler(this.OnDataTableChanged);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0005DA54 File Offset: 0x0005BC54
		private void RemoveXmlDocumentListeners()
		{
			base.NodeInserting -= this.OnNodeInserting;
			base.NodeInserted -= this.OnNodeInserted;
			base.NodeChanging -= this.OnNodeChanging;
			base.NodeChanged -= this.OnNodeChanged;
			base.NodeRemoving -= this.OnNodeRemoving;
			base.NodeRemoved -= this.OnNodeRemoved;
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0005DAD0 File Offset: 0x0005BCD0
		private void AddXmlDocumentListeners()
		{
			base.NodeInserting += this.OnNodeInserting;
			base.NodeInserted += this.OnNodeInserted;
			base.NodeChanging += this.OnNodeChanging;
			base.NodeChanged += this.OnNodeChanged;
			base.NodeRemoving += this.OnNodeRemoving;
			base.NodeRemoved += this.OnNodeRemoved;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0005DB4C File Offset: 0x0005BD4C
		internal static object StringToObject(Type type, string value)
		{
			if (value == null || value == string.Empty)
			{
				return DBNull.Value;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				return XmlConvert.ToBoolean(value);
			case TypeCode.Char:
				return (char)XmlConvert.ToInt32(value);
			case TypeCode.SByte:
				return XmlConvert.ToSByte(value);
			case TypeCode.Byte:
				return XmlConvert.ToByte(value);
			case TypeCode.Int16:
				return XmlConvert.ToInt16(value);
			case TypeCode.UInt16:
				return XmlConvert.ToUInt16(value);
			case TypeCode.Int32:
				return XmlConvert.ToInt32(value);
			case TypeCode.UInt32:
				return XmlConvert.ToUInt32(value);
			case TypeCode.Int64:
				return XmlConvert.ToInt64(value);
			case TypeCode.UInt64:
				return XmlConvert.ToUInt64(value);
			case TypeCode.Single:
				return XmlConvert.ToSingle(value);
			case TypeCode.Double:
				return XmlConvert.ToDouble(value);
			case TypeCode.Decimal:
				return XmlConvert.ToDecimal(value);
			case TypeCode.DateTime:
				return XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.Unspecified);
			default:
				if (type == typeof(TimeSpan))
				{
					return XmlConvert.ToTimeSpan(value);
				}
				if (type == typeof(Guid))
				{
					return XmlConvert.ToGuid(value);
				}
				if (type == typeof(byte[]))
				{
					return Convert.FromBase64String(value);
				}
				return Convert.ChangeType(value, type);
			}
		}

		// Token: 0x0400085F RID: 2143
		private DataSet dataSet;

		// Token: 0x04000860 RID: 2144
		private int dataRowID = 1;

		// Token: 0x04000861 RID: 2145
		private ArrayList dataRowIDList = new ArrayList();

		// Token: 0x04000862 RID: 2146
		private bool raiseDataSetEvents = true;

		// Token: 0x04000863 RID: 2147
		private bool raiseDocumentEvents = true;

		// Token: 0x04000864 RID: 2148
		private DataColumnChangeEventHandler columnChanged;

		// Token: 0x04000865 RID: 2149
		private DataRowChangeEventHandler rowDeleted;

		// Token: 0x04000866 RID: 2150
		private DataRowChangeEventHandler rowChanged;

		// Token: 0x04000867 RID: 2151
		private CollectionChangeEventHandler tablesChanged;

		// Token: 0x0200018A RID: 394
		internal class XmlDataElement : XmlElement
		{
			// Token: 0x060014F6 RID: 5366 RVA: 0x0005DCC4 File Offset: 0x0005BEC4
			internal XmlDataElement(DataRow row, string prefix, string localName, string ns, XmlDataDocument doc)
				: base(prefix, localName, ns, doc)
			{
				this.row = row;
				if (row != null)
				{
					row.DataElement = this;
					row.XmlRowID = doc.dataRowID;
					doc.dataRowIDList.Add(row.XmlRowID);
					doc.dataRowID++;
				}
			}

			// Token: 0x170003DC RID: 988
			// (get) Token: 0x060014F7 RID: 5367 RVA: 0x0005DD28 File Offset: 0x0005BF28
			internal DataRow DataRow
			{
				get
				{
					return this.row;
				}
			}

			// Token: 0x04000868 RID: 2152
			private DataRow row;
		}
	}
}
