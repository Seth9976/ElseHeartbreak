using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace System.Runtime.Serialization
{
	/// <summary>Allows the transformation of a set of .NET Framework types that are used in data contracts into an XML schema file (.xsd). </summary>
	// Token: 0x02000034 RID: 52
	public class XsdDataContractExporter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.XsdDataContractExporter" /> class. </summary>
		// Token: 0x06000135 RID: 309 RVA: 0x00007140 File Offset: 0x00005340
		public XsdDataContractExporter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.XsdDataContractExporter" /> class with the specified set of schemas. </summary>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that contains the schemas to be exported. </param>
		// Token: 0x06000136 RID: 310 RVA: 0x00007148 File Offset: 0x00005348
		public XsdDataContractExporter(XmlSchemaSet schemas)
		{
			this.schemas = schemas;
		}

		/// <summary>Gets the collection of exported XML schemas. </summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that contains the schemas transformed from the set of common language runtime (CLR) types after calling the <see cref="Overload:System.Runtime.Serialization.XsdDataContractExporter.Export" /> method.</returns>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00007158 File Offset: 0x00005358
		public XmlSchemaSet Schemas
		{
			get
			{
				if (this.schemas == null)
				{
					this.schemas = new XmlSchemaSet();
					this.schemas.Add(XsdDataContractExporter.MSTypesSchema);
				}
				return this.schemas;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Runtime.Serialization.ExportOptions" /> that contains options that can be set for the export operation. </summary>
		/// <returns>An <see cref="T:System.Runtime.Serialization.ExportOptions" /> that contains options used to customize how types are exported to schemas.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00007188 File Offset: 0x00005388
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00007190 File Offset: 0x00005390
		public ExportOptions Options
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

		/// <summary>Gets a value that indicates whether the set of .common language runtime (CLR) types contained in a <see cref="T:System.Collections.Generic.ICollection`1" /> can be exported. </summary>
		/// <returns>true if the types can be exported; otherwise, false.</returns>
		/// <param name="types">A <see cref="T:System.Collections.Generic.ICollection`1" />   that contains the specified types to export.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600013A RID: 314 RVA: 0x0000719C File Offset: 0x0000539C
		public bool CanExport(ICollection<Type> types)
		{
			foreach (Type type in types)
			{
				if (!this.CanExport(type))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets a value that indicates whether the set of .common language runtime (CLR) types contained in a set of assemblies can be exported. </summary>
		/// <returns>true if the types can be exported; otherwise, false.</returns>
		/// <param name="assemblies">A <see cref="T:System.Collections.Generic.ICollection`1" />   of <see cref="T:System.Reflection.Assembly" /> that contains the assemblies with the types to export.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600013B RID: 315 RVA: 0x0000720C File Offset: 0x0000540C
		public bool CanExport(ICollection<Assembly> assemblies)
		{
			foreach (Assembly assembly in assemblies)
			{
				foreach (Module module in assembly.GetModules())
				{
					foreach (Type type in module.GetTypes())
					{
						if (!this.CanExport(type))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		/// <summary>Gets a value that indicates whether the specified common language runtime (CLR) type can be exported. </summary>
		/// <returns>true if the type can be exported; otherwise, false. </returns>
		/// <param name="type">The <see cref="T:System.Type" /> to export. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600013C RID: 316 RVA: 0x000072C8 File Offset: 0x000054C8
		public bool CanExport(Type type)
		{
			return !this.KnownTypes.GetQName(type).IsEmpty;
		}

		/// <summary>Transforms the types contained in the <see cref="T:System.Collections.Generic.ICollection`1" /> passed to this method.</summary>
		/// <param name="types">A  <see cref="T:System.Collections.Generic.ICollection`1" /> (of <see cref="T:System.Type" />) that contains the types to export.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="types" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">A type in the collection is null.</exception>
		// Token: 0x0600013D RID: 317 RVA: 0x000072EC File Offset: 0x000054EC
		public void Export(ICollection<Type> types)
		{
			foreach (Type type in types)
			{
				this.Export(type);
			}
		}

		/// <summary>Transforms the types contained in the specified collection of assemblies. </summary>
		/// <param name="assemblies">A <see cref="T:System.Collections.Generic.ICollection`1" />   (of <see cref="T:System.Reflection.Assembly" />) that contains the types to export.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="assemblies" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">An <see cref="T:System.Reflection.Assembly" /> in the collection is null.</exception>
		// Token: 0x0600013E RID: 318 RVA: 0x0000734C File Offset: 0x0000554C
		public void Export(ICollection<Assembly> assemblies)
		{
			foreach (Assembly assembly in assemblies)
			{
				foreach (Module module in assembly.GetModules())
				{
					foreach (Type type in module.GetTypes())
					{
						this.Export(type);
					}
				}
			}
		}

		/// <summary>Transforms the specified .NET Framework type into an XML schema definition language (XSD) schema. </summary>
		/// <param name="type">The <see cref="T:System.Type" /> to transform into an XML schema. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> argument is null.</exception>
		// Token: 0x0600013F RID: 319 RVA: 0x000073F8 File Offset: 0x000055F8
		[MonoTODO]
		public void Export(Type type)
		{
			this.KnownTypes.Add(type);
			SerializationMap serializationMap = this.KnownTypes.FindUserMap(type);
			if (serializationMap == null)
			{
				return;
			}
			serializationMap.GetSchemaType(this.Schemas, this.GeneratedTypes);
			this.Schemas.Compile();
		}

		/// <summary>Returns the top-level name and namespace for the <see cref="T:System.Type" />.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlQualifiedName" /> that represents the top-level name and namespace for this <see cref="T:System.Type" />, which is written to the stream when writing this object. </returns>
		/// <param name="type">The <see cref="T:System.Type" /> to query.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> argument is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000140 RID: 320 RVA: 0x00007444 File Offset: 0x00005644
		[MonoTODO]
		public XmlQualifiedName GetRootElementName(Type type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the XML schema type for the specified type.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> that contains the XML schema. </returns>
		/// <param name="type">The type to return a schema for.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> argument is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000141 RID: 321 RVA: 0x0000744C File Offset: 0x0000564C
		[MonoTODO]
		public XmlSchemaType GetSchemaType(Type type)
		{
			SerializationMap serializationMap = this.KnownTypes.FindUserMap(type);
			if (serializationMap == null)
			{
				return null;
			}
			return serializationMap.GetSchemaType(this.Schemas, this.GeneratedTypes);
		}

		/// <summary>Returns the contract name and contract namespace for the <see cref="T:System.Type" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> that represents the contract name of the type and its namespace.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> that was exported. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> argument is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000142 RID: 322 RVA: 0x00007480 File Offset: 0x00005680
		public XmlQualifiedName GetSchemaTypeName(Type type)
		{
			XmlQualifiedName qname = this.KnownTypes.GetQName(type);
			if (qname.Namespace == "http://schemas.microsoft.com/2003/10/Serialization/")
			{
				return new XmlQualifiedName(qname.Name, "http://www.w3.org/2001/XMLSchema");
			}
			return qname;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000074C4 File Offset: 0x000056C4
		private KnownTypeCollection KnownTypes
		{
			get
			{
				if (this.known_types == null)
				{
					this.known_types = new KnownTypeCollection();
				}
				return this.known_types;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000074E4 File Offset: 0x000056E4
		private Dictionary<XmlQualifiedName, XmlSchemaType> GeneratedTypes
		{
			get
			{
				if (this.generated_schema_types == null)
				{
					this.generated_schema_types = new Dictionary<XmlQualifiedName, XmlSchemaType>();
				}
				return this.generated_schema_types;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00007504 File Offset: 0x00005704
		private static XmlSchema MSTypesSchema
		{
			get
			{
				if (XsdDataContractExporter.mstypes_schema == null)
				{
					Assembly callingAssembly = Assembly.GetCallingAssembly();
					Stream manifestResourceStream = callingAssembly.GetManifestResourceStream("mstypes.schema");
					XsdDataContractExporter.mstypes_schema = XmlSchema.Read(manifestResourceStream, null);
				}
				return XsdDataContractExporter.mstypes_schema;
			}
		}

		// Token: 0x040000A6 RID: 166
		private ExportOptions options;

		// Token: 0x040000A7 RID: 167
		private KnownTypeCollection known_types;

		// Token: 0x040000A8 RID: 168
		private XmlSchemaSet schemas;

		// Token: 0x040000A9 RID: 169
		private Dictionary<XmlQualifiedName, XmlSchemaType> generated_schema_types;

		// Token: 0x040000AA RID: 170
		private static XmlSchema mstypes_schema;
	}
}
