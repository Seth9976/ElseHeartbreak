using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Runtime.Serialization
{
	/// <summary>Allows the transformation of a set of XML schema files (.xsd) into common language runtime (CLR) types. </summary>
	// Token: 0x02000035 RID: 53
	public class XsdDataContractImporter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.XsdDataContractImporter" /> class. </summary>
		// Token: 0x06000146 RID: 326 RVA: 0x00007540 File Offset: 0x00005740
		public XsdDataContractImporter()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.XsdDataContractImporter" /> class with the <see cref="T:System.CodeDom.CodeCompileUnit" /> that will be used to generate CLR code.</summary>
		/// <param name="codeCompileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> that will be used to store the code. </param>
		// Token: 0x06000147 RID: 327 RVA: 0x0000754C File Offset: 0x0000574C
		public XsdDataContractImporter(CodeCompileUnit ccu)
		{
			this.ccu = ccu;
			this.imported_names = new Dictionary<XmlQualifiedName, XmlQualifiedName>();
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeCompileUnit" /> used for storing the CLR types generated.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> used to store the CLR types generated.</returns>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000758C File Offset: 0x0000578C
		public CodeCompileUnit CodeCompileUnit
		{
			get
			{
				if (this.ccu == null)
				{
					this.ccu = new CodeCompileUnit();
				}
				return this.ccu;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Runtime.Serialization.ImportOptions" /> that contains settable options for the import operation. </summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.ImportOptions" /> that contains settable options. </returns>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000075AC File Offset: 0x000057AC
		// (set) Token: 0x0600014B RID: 331 RVA: 0x000075B4 File Offset: 0x000057B4
		public ImportOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		/// <summary>Returns a list of <see cref="T:System.CodeDom.CodeTypeReference" /> objects that represents the known types generated when generating code for the specified schema type.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IList`1" /> of type <see cref="T:System.CodeDom.CodeTypeReference" />.</returns>
		/// <param name="typeName">An <see cref="T:System.Xml.XmlQualifiedName" /> that represents the schema type to look up known types for.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600014C RID: 332 RVA: 0x000075C0 File Offset: 0x000057C0
		[MonoTODO]
		public ICollection<CodeTypeReference> GetKnownTypeReferences(XmlQualifiedName typeName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a <see cref="T:System.CodeDom.CodeTypeReference" /> to the CLR type generated for the schema type with the specified <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> reference to the CLR type generated for the schema type with the <paramref name="typeName" /> specified.</returns>
		/// <param name="typeName">The <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the schema type to look up.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600014D RID: 333 RVA: 0x000075C8 File Offset: 0x000057C8
		[MonoTODO]
		public CodeTypeReference GetCodeTypeReference(XmlQualifiedName typeName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a <see cref="T:System.CodeDom.CodeTypeReference" /> for the specified XML qualified element and schema element.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that represents the type that was generated for the specified schema type.</returns>
		/// <param name="typeName">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the XML qualified name of the schema type to look up.</param>
		/// <param name="element">An <see cref="T:System.Xml.Schema.XmlSchemaElement" /> that specifies an element in an XML schema.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600014E RID: 334 RVA: 0x000075D0 File Offset: 0x000057D0
		[MonoTODO]
		public CodeTypeReference GetCodeTypeReference(XmlQualifiedName typeName, XmlSchemaElement element)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value that indicates whether the schemas contained in an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> can be transformed into a <see cref="T:System.CodeDom.CodeCompileUnit" />. </summary>
		/// <returns>true if the schemas can be transformed to data contract types; otherwise, false. </returns>
		/// <param name="schemas">A <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that contains the schemas to transform. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemas" /> parameter is null.</exception>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">A data contract involved in the import is invalid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600014F RID: 335 RVA: 0x000075D8 File Offset: 0x000057D8
		public bool CanImport(XmlSchemaSet schemas)
		{
			foreach (object obj in schemas.GlobalElements)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				if (!this.CanImport(schemas, xmlSchemaElement))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets a value that indicates whether the specified set of types contained in an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> can be transformed into CLR types generated into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
		/// <returns>true if the schemas can be transformed; otherwise, false. </returns>
		/// <param name="schemas">A <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that contains the schemas to transform.</param>
		/// <param name="typeNames">An <see cref="T:System.Collections.Generic.ICollection`1" /> of <see cref="T:System.Xml.XmlQualifiedName" /> that represents the set of schema types to import.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemas" /> or <paramref name="typeNames" /> parameter is null.</exception>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">A data contract involved in the import is invalid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000150 RID: 336 RVA: 0x00007658 File Offset: 0x00005858
		public bool CanImport(XmlSchemaSet schemas, ICollection<XmlQualifiedName> typeNames)
		{
			foreach (XmlQualifiedName xmlQualifiedName in typeNames)
			{
				if (!this.CanImport(schemas, xmlQualifiedName))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets a value that indicates whether the schemas contained in an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> can be transformed into a <see cref="T:System.CodeDom.CodeCompileUnit" />. </summary>
		/// <returns>true if the schemas can be transformed to data contract types; otherwise, false.</returns>
		/// <param name="schemas">A <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that contains the schema representations. </param>
		/// <param name="typeName">An <see cref="T:System.Collections.IList" /> of <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the names of the schema types that need to be imported from the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemas" /> or <paramref name="typeName" /> parameter is null.</exception>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">A data contract involved in the import is invalid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000151 RID: 337 RVA: 0x000076C8 File Offset: 0x000058C8
		public bool CanImport(XmlSchemaSet schemas, XmlQualifiedName name)
		{
			return this.CanImport(schemas, (XmlSchemaElement)schemas.GlobalElements[name]);
		}

		/// <summary>Gets a value that indicates whether a specific schema element contained in an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> can be imported.</summary>
		/// <returns>true if the element can be imported; otherwise, false.</returns>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to import.</param>
		/// <param name="element">A specific <see cref="T:System.Xml.Schema.XmlSchemaElement" /> to check in the set of schemas.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemas" /> or <paramref name="element" /> parameter is null.</exception>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">A data contract involved in the import is invalid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000152 RID: 338 RVA: 0x000076E4 File Offset: 0x000058E4
		[MonoTODO]
		public bool CanImport(XmlSchemaSet schemas, XmlSchemaElement element)
		{
			throw new NotImplementedException();
		}

		/// <summary>Transforms the specified set of XML schemas contained in an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> into a <see cref="T:System.CodeDom.CodeCompileUnit" />. </summary>
		/// <param name="schemas">A <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that contains the schema representations to generate CLR types for.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemas" /> parameter is null.</exception>
		// Token: 0x06000153 RID: 339 RVA: 0x000076EC File Offset: 0x000058EC
		[MonoTODO]
		public void Import(XmlSchemaSet schemas)
		{
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			schemas.Compile();
			foreach (object obj in schemas.GlobalElements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				this.ImportInternal(schemas, xmlSchemaElement.QualifiedName);
			}
		}

		/// <summary>Transforms the specified set of schema types contained in an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> into CLR types generated into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
		/// <param name="schemas">A <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that contains the schema representations.</param>
		/// <param name="typeNames">A <see cref="T:System.Collections.Generic.ICollection`1" />  (of <see cref="T:System.Xml.XmlQualifiedName" />) that represents the set of schema types to import.</param>
		// Token: 0x06000154 RID: 340 RVA: 0x00007780 File Offset: 0x00005980
		public void Import(XmlSchemaSet schemas, ICollection<XmlQualifiedName> typeNames)
		{
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			if (typeNames == null)
			{
				throw new ArgumentNullException("typeNames");
			}
			schemas.Compile();
			foreach (XmlQualifiedName xmlQualifiedName in typeNames)
			{
				this.ImportInternal(schemas, xmlQualifiedName);
			}
		}

		/// <summary>Transforms the specified XML schema type contained in an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
		/// <param name="schemas">A <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that contains the schema representations. </param>
		/// <param name="typeName">A <see cref="T:System.Xml.XmlQualifiedName" /> that represents a specific schema type to import.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemas" /> or <paramref name="typeName" /> parameter is null.</exception>
		// Token: 0x06000155 RID: 341 RVA: 0x00007808 File Offset: 0x00005A08
		public void Import(XmlSchemaSet schemas, XmlQualifiedName name)
		{
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			schemas.Compile();
			if (schemas.GlobalTypes[name] == null)
			{
				throw new InvalidDataContractException(string.Format("Type with name '{0}' not found in schema with namespace '{1}'", name.Name, name.Namespace));
			}
			this.ImportInternal(schemas, name);
		}

		/// <summary>Transforms the specified schema element in the set of specified XML schemas into a <see cref="T:System.CodeDom.CodeCompileUnit" /> and returns an <see cref="T:System.Xml.XmlQualifiedName" /> that represents the data contract name for the specified element.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> that represents the specified element.</returns>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that contains the schemas to transform.</param>
		/// <param name="element">An <see cref="T:System.Xml.Schema.XmlSchemaElement" /> that represents the specific schema element to transform. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemas" /> or <paramref name="element" /> parameter is null.</exception>
		// Token: 0x06000156 RID: 342 RVA: 0x0000787C File Offset: 0x00005A7C
		[MonoTODO]
		public XmlQualifiedName Import(XmlSchemaSet schemas, XmlSchemaElement element)
		{
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			schemas.Compile();
			XmlQualifiedName xmlQualifiedName = this.ImportInternal(schemas, element.QualifiedName);
			foreach (object obj in schemas.GlobalTypes.Names)
			{
				XmlQualifiedName xmlQualifiedName2 = (XmlQualifiedName)obj;
				this.ImportInternal(schemas, xmlQualifiedName2);
			}
			return xmlQualifiedName;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000792C File Offset: 0x00005B2C
		private XmlQualifiedName ImportInternal(XmlSchemaSet schemas, XmlQualifiedName qname)
		{
			if (qname.Namespace == "http://schemas.microsoft.com/2003/10/Serialization/")
			{
				return qname;
			}
			if (this.imported_names.ContainsKey(qname))
			{
				return this.imported_names[qname];
			}
			XmlSchemas xmlSchemas = new XmlSchemas();
			foreach (object obj in schemas.Schemas())
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				xmlSchemas.Add(xmlSchema);
			}
			XmlSchemaImporter xmlSchemaImporter = new XmlSchemaImporter(xmlSchemas);
			XmlTypeMapping xmlTypeMapping = xmlSchemaImporter.ImportTypeMapping(qname);
			this.ImportFromTypeMapping(xmlTypeMapping);
			return qname;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000079F4 File Offset: 0x00005BF4
		private void ImportFromTypeMapping(XmlTypeMapping mapping)
		{
			if (mapping == null)
			{
				return;
			}
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(mapping.TypeName, mapping.Namespace);
			if (this.imported_names.ContainsKey(xmlQualifiedName))
			{
				return;
			}
			CodeNamespace codeNamespace = new CodeNamespace();
			codeNamespace.Name = this.FromXmlnsToClrName(mapping.Namespace);
			XmlCodeExporter xmlCodeExporter = new XmlCodeExporter(codeNamespace);
			xmlCodeExporter.ExportTypeMapping(mapping);
			List<CodeTypeDeclaration> list = new List<CodeTypeDeclaration>();
			foreach (object obj in codeNamespace.Types)
			{
				CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)obj;
				string @namespace = this.GetNamespace(codeTypeDeclaration);
				if (@namespace != null)
				{
					XmlQualifiedName xmlQualifiedName2 = new XmlQualifiedName(codeTypeDeclaration.Name, @namespace);
					if (this.imported_names.ContainsKey(xmlQualifiedName2))
					{
						list.Add(codeTypeDeclaration);
					}
					else if (xmlQualifiedName2.Namespace == "http://schemas.microsoft.com/2003/10/Serialization/Arrays")
					{
						list.Add(codeTypeDeclaration);
					}
					else
					{
						this.imported_names[xmlQualifiedName2] = xmlQualifiedName2;
						codeTypeDeclaration.Comments.Clear();
						codeTypeDeclaration.CustomAttributes.Clear();
						codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("System.CodeDom.Compiler.GeneratedCodeAttribute"), new CodeAttributeArgument[]
						{
							new CodeAttributeArgument(new CodePrimitiveExpression("System.Runtime.Serialization")),
							new CodeAttributeArgument(new CodePrimitiveExpression("3.0.0.0"))
						}));
						codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("System.Runtime.Serialization.DataContractAttribute")));
						if (!codeTypeDeclaration.IsEnum)
						{
							codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(typeof(object)));
							codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference("System.Runtime.Serialization.IExtensibleDataObject"));
							foreach (object obj2 in codeTypeDeclaration.Members)
							{
								CodeTypeMember codeTypeMember = (CodeTypeMember)obj2;
								CodeMemberProperty codeMemberProperty = codeTypeMember as CodeMemberProperty;
								if (codeMemberProperty != null)
								{
									if ((codeMemberProperty.Attributes & MemberAttributes.Public) == MemberAttributes.Public)
									{
										codeMemberProperty.CustomAttributes.Clear();
										codeMemberProperty.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("System.Runtime.Serialization.DataMemberAttribute")));
										codeMemberProperty.Comments.Clear();
									}
								}
							}
							CodeMemberField codeMemberField = new CodeMemberField(new CodeTypeReference("System.Runtime.Serialization.ExtensionDataObject"), "extensionDataField");
							codeMemberField.Attributes = (MemberAttributes)20482;
							codeTypeDeclaration.Members.Add(codeMemberField);
							CodeMemberProperty codeMemberProperty2 = new CodeMemberProperty();
							codeMemberProperty2.Type = new CodeTypeReference("System.Runtime.Serialization.ExtensionDataObject");
							codeMemberProperty2.Name = "ExtensionData";
							codeMemberProperty2.Attributes = (MemberAttributes)24578;
							codeMemberProperty2.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "extensionDataField")));
							codeMemberProperty2.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "extensionDataField"), new CodePropertySetValueReferenceExpression()));
							codeTypeDeclaration.Members.Add(codeMemberProperty2);
						}
					}
				}
			}
			foreach (CodeTypeDeclaration codeTypeDeclaration2 in list)
			{
				codeNamespace.Types.Remove(codeTypeDeclaration2);
			}
			if (codeNamespace.Types.Count > 0)
			{
				this.CodeCompileUnit.Namespaces.Add(codeNamespace);
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00007DE4 File Offset: 0x00005FE4
		private string FromXmlnsToClrName(string xns)
		{
			Uri uri;
			string text;
			if (xns.StartsWith("http://schemas.datacontract.org/2004/07/", StringComparison.Ordinal))
			{
				xns = xns.Substring("http://schemas.datacontract.org/2004/07/".Length);
			}
			else if (Uri.TryCreate(xns, UriKind.Absolute, out uri) && (text = this.MakeStringNamespaceComponentsValid(uri.GetComponents(UriComponents.Host | UriComponents.Path, UriFormat.Unescaped))).Length > 0)
			{
				xns = text;
			}
			return this.MakeStringNamespaceComponentsValid(xns);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00007E50 File Offset: 0x00006050
		private string MakeStringNamespaceComponentsValid(string ns)
		{
			string[] array = ns.Split(XsdDataContractImporter.split_tokens, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = CodeIdentifier.MakeValid(array[i]);
			}
			return string.Join(".", array);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007E94 File Offset: 0x00006094
		private string GetNamespace(CodeTypeDeclaration type)
		{
			foreach (object obj in type.CustomAttributes)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj;
				if (codeAttributeDeclaration.Name == "System.Xml.Serialization.XmlTypeAttribute" || codeAttributeDeclaration.Name == "System.Xml.Serialization.XmlRootAttribute")
				{
					foreach (object obj2 in codeAttributeDeclaration.Arguments)
					{
						CodeAttributeArgument codeAttributeArgument = (CodeAttributeArgument)obj2;
						if (codeAttributeArgument.Name == "Namespace")
						{
							return ((CodePrimitiveExpression)codeAttributeArgument.Value).Value as string;
						}
					}
					return null;
				}
			}
			return null;
		}

		// Token: 0x040000AB RID: 171
		private const string default_ns_prefix = "http://schemas.datacontract.org/2004/07/";

		// Token: 0x040000AC RID: 172
		private ImportOptions options;

		// Token: 0x040000AD RID: 173
		private CodeCompileUnit ccu;

		// Token: 0x040000AE RID: 174
		private Dictionary<XmlQualifiedName, XmlQualifiedName> imported_names = new Dictionary<XmlQualifiedName, XmlQualifiedName>();

		// Token: 0x040000AF RID: 175
		private static readonly char[] split_tokens = new char[] { '/', '.' };
	}
}
