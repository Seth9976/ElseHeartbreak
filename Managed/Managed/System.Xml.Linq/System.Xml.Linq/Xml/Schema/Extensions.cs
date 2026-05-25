using System;
using System.Xml.Linq;

namespace System.Xml.Schema
{
	/// <summary>This class contains the LINQ to XML extension methods for XSD validation. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000026 RID: 38
	[MonoTODO]
	public static class Extensions
	{
		/// <summary>Gets the post-schema-validation infoset (PSVI) of a validated attribute.</summary>
		/// <returns>A <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> that contains the post-schema-validation infoset for an <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="source">An <see cref="T:System.Xml.Linq.XAttribute" /> that has been previously validated.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001F1 RID: 497 RVA: 0x00009300 File Offset: 0x00007500
		[MonoTODO]
		public static IXmlSchemaInfo GetSchemaInfo(this XAttribute attribute)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the post-schema-validation infoset (PSVI) of a validated element.</summary>
		/// <returns>A <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> that contains the post-schema-validation infoset (PSVI) for an <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="source">An <see cref="T:System.Xml.Linq.XElement" /> that has been previously validated.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001F2 RID: 498 RVA: 0x00009308 File Offset: 0x00007508
		[MonoTODO]
		public static IXmlSchemaInfo GetSchemaInfo(this XElement element)
		{
			throw new NotImplementedException();
		}

		/// <summary>This method validates that an <see cref="T:System.Xml.Linq.XAttribute" /> conforms to a specified <see cref="T:System.Xml.Schema.XmlSchemaObject" /> and an <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="source">The <see cref="T:System.Xml.Linq.XAttribute" /> to validate.</param>
		/// <param name="partialValidationType">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> that specifies the sub-tree to validate.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to validate against.</param>
		/// <param name="validationEventHandler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If null, throws an exception upon validation errors.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001F3 RID: 499 RVA: 0x00009310 File Offset: 0x00007510
		[MonoTODO]
		public static void Validate(this XAttribute attribute, XmlSchemaObject partialValidationType, XmlSchemaSet schemas, ValidationEventHandler handler)
		{
			throw new NotImplementedException();
		}

		/// <summary>Validates that an <see cref="T:System.Xml.Linq.XAttribute" /> conforms to a specified <see cref="T:System.Xml.Schema.XmlSchemaObject" /> and an <see cref="T:System.Xml.Schema.XmlSchemaSet" />, optionally populating the XML tree with the post-schema-validation infoset (PSVI).</summary>
		/// <param name="source">The <see cref="T:System.Xml.Linq.XAttribute" /> to validate.</param>
		/// <param name="partialValidationType">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> that specifies the sub-tree to validate.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to validate against.</param>
		/// <param name="validationEventHandler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If null, throws an exception upon validation errors.</param>
		/// <param name="addSchemaInfo">A <see cref="T:System.Boolean" /> indicating whether to populate the post-schema-validation infoset (PSVI).</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001F4 RID: 500 RVA: 0x00009318 File Offset: 0x00007518
		[MonoTODO]
		public static void Validate(this XAttribute attribute, XmlSchemaObject partialValidationType, XmlSchemaSet schemas, ValidationEventHandler handler, bool addSchemaInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>This method validates that an <see cref="T:System.Xml.Linq.XDocument" /> conforms to an XSD in an <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="source">The <see cref="T:System.Xml.Linq.XDocument" /> to validate.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to validate against.</param>
		/// <param name="validationEventHandler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If null, throws an exception upon validation errors.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001F5 RID: 501 RVA: 0x00009320 File Offset: 0x00007520
		[MonoTODO]
		public static void Validate(this XDocument document, XmlSchemaSet schemas, ValidationEventHandler handler)
		{
			throw new NotImplementedException();
		}

		/// <summary>Validates that an <see cref="T:System.Xml.Linq.XDocument" /> conforms to an XSD in an <see cref="T:System.Xml.Schema.XmlSchemaSet" />, optionally populating the XML tree with the post-schema-validation infoset (PSVI).</summary>
		/// <param name="source">The <see cref="T:System.Xml.Linq.XDocument" /> to validate.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to validate against.</param>
		/// <param name="validationEventHandler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If null, throws an exception upon validation errors.</param>
		/// <param name="addSchemaInfo">A <see cref="T:System.Boolean" /> indicating whether to populate the post-schema-validation infoset (PSVI).</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001F6 RID: 502 RVA: 0x00009328 File Offset: 0x00007528
		[MonoTODO]
		public static void Validate(this XDocument document, XmlSchemaSet schemas, ValidationEventHandler handler, bool addSchemaInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>This method validates that an <see cref="T:System.Xml.Linq.XElement" /> sub-tree conforms to a specified <see cref="T:System.Xml.Schema.XmlSchemaObject" /> and an <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="source">The <see cref="T:System.Xml.Linq.XElement" /> to validate.</param>
		/// <param name="partialValidationType">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> that specifies the sub-tree to validate.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to validate against.</param>
		/// <param name="validationEventHandler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If null, throws an exception upon validation errors.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001F7 RID: 503 RVA: 0x00009330 File Offset: 0x00007530
		[MonoTODO]
		public static void Validate(this XElement element, XmlSchemaObject partialValidationType, XmlSchemaSet schemas, ValidationEventHandler handler)
		{
			throw new NotImplementedException();
		}

		/// <summary>Validates that an <see cref="T:System.Xml.Linq.XElement" /> sub-tree conforms to a specified <see cref="T:System.Xml.Schema.XmlSchemaObject" /> and an <see cref="T:System.Xml.Schema.XmlSchemaSet" />, optionally populating the XML tree with the post-schema-validation infoset (PSVI).</summary>
		/// <param name="source">The <see cref="T:System.Xml.Linq.XElement" /> to validate.</param>
		/// <param name="partialValidationType">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> that specifies the sub-tree to validate.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to validate against.</param>
		/// <param name="validationEventHandler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If null, throws an exception upon validation errors.</param>
		/// <param name="addSchemaInfo">A <see cref="T:System.Boolean" /> indicating whether to populate the post-schema-validation infoset (PSVI).</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001F8 RID: 504 RVA: 0x00009338 File Offset: 0x00007538
		[MonoTODO]
		public static void Validate(this XElement element, XmlSchemaObject partialValidationType, XmlSchemaSet schemas, ValidationEventHandler handler, bool addSchemaInfo)
		{
			throw new NotImplementedException();
		}
	}
}
