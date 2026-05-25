using System;
using System.Xml;

namespace System.Data
{
	// Token: 0x02000091 RID: 145
	internal class XmlConstants
	{
		// Token: 0x04000284 RID: 644
		public const string SchemaPrefix = "xs";

		// Token: 0x04000285 RID: 645
		public const string SchemaNamespace = "http://www.w3.org/2001/XMLSchema";

		// Token: 0x04000286 RID: 646
		public const string XmlnsNS = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04000287 RID: 647
		public const string XmlNS = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x04000288 RID: 648
		public const string SchemaElement = "schema";

		// Token: 0x04000289 RID: 649
		public const string AttributeFormDefault = "attributeFormDefault";

		// Token: 0x0400028A RID: 650
		public const string ElementFormDefault = "elementFormDefault";

		// Token: 0x0400028B RID: 651
		public const string Qualified = "qualified";

		// Token: 0x0400028C RID: 652
		public const string Unqualified = "unqualified";

		// Token: 0x0400028D RID: 653
		public const string Element = "element";

		// Token: 0x0400028E RID: 654
		public const string Choice = "choice";

		// Token: 0x0400028F RID: 655
		public const string ComplexType = "complexType";

		// Token: 0x04000290 RID: 656
		public const string SimpleType = "simpleType";

		// Token: 0x04000291 RID: 657
		public const string Restriction = "restriction";

		// Token: 0x04000292 RID: 658
		public const string MaxLength = "maxLength";

		// Token: 0x04000293 RID: 659
		public const string Sequence = "sequence";

		// Token: 0x04000294 RID: 660
		public const string MaxOccurs = "maxOccurs";

		// Token: 0x04000295 RID: 661
		public const string MinOccurs = "minOccurs";

		// Token: 0x04000296 RID: 662
		public const string Unbounded = "unbounded";

		// Token: 0x04000297 RID: 663
		public const string Name = "name";

		// Token: 0x04000298 RID: 664
		public const string Type = "type";

		// Token: 0x04000299 RID: 665
		public const string Id = "id";

		// Token: 0x0400029A RID: 666
		public const string TargetNamespace = "targetNamespace";

		// Token: 0x0400029B RID: 667
		public const string Form = "form";

		// Token: 0x0400029C RID: 668
		public const string Attribute = "attribute";

		// Token: 0x0400029D RID: 669
		public const string Default = "default";

		// Token: 0x0400029E RID: 670
		public const string Caption = "Caption";

		// Token: 0x0400029F RID: 671
		public const string Base = "base";

		// Token: 0x040002A0 RID: 672
		public const string Value = "value";

		// Token: 0x040002A1 RID: 673
		public const string DataType = "DataType";

		// Token: 0x040002A2 RID: 674
		public const string AutoIncrement = "AutoIncrement";

		// Token: 0x040002A3 RID: 675
		public const string AutoIncrementSeed = "AutoIncrementSeed";

		// Token: 0x040002A4 RID: 676
		public const string AutoIncrementStep = "AutoIncrementStep";

		// Token: 0x040002A5 RID: 677
		public const string MsdataPrefix = "msdata";

		// Token: 0x040002A6 RID: 678
		public const string MsdataNamespace = "urn:schemas-microsoft-com:xml-msdata";

		// Token: 0x040002A7 RID: 679
		public const string MsdatasourceNamespace = "urn:schemas-microsoft-com:xml-msdatasource";

		// Token: 0x040002A8 RID: 680
		public const string MspropPrefix = "msprop";

		// Token: 0x040002A9 RID: 681
		public const string MspropNamespace = "urn:schemas-microsoft-com:xml-msprop";

		// Token: 0x040002AA RID: 682
		public const string DiffgrPrefix = "diffgr";

		// Token: 0x040002AB RID: 683
		public const string DiffgrNamespace = "urn:schemas-microsoft-com:xml-diffgram-v1";

		// Token: 0x040002AC RID: 684
		public const string TnsPrefix = "mstns";

		// Token: 0x040002AD RID: 685
		public const string IsDataSet = "IsDataSet";

		// Token: 0x040002AE RID: 686
		public const string Locale = "Locale";

		// Token: 0x040002AF RID: 687
		public const string Ordinal = "Ordinal";

		// Token: 0x040002B0 RID: 688
		public const string IsNested = "IsNested";

		// Token: 0x040002B1 RID: 689
		public const string ConstraintOnly = "ConstraintOnly";

		// Token: 0x040002B2 RID: 690
		public const string RelationName = "RelationName";

		// Token: 0x040002B3 RID: 691
		public const string ConstraintName = "ConstraintName";

		// Token: 0x040002B4 RID: 692
		public const string PrimaryKey = "PrimaryKey";

		// Token: 0x040002B5 RID: 693
		public const string ColumnName = "ColumnName";

		// Token: 0x040002B6 RID: 694
		public const string ReadOnly = "ReadOnly";

		// Token: 0x040002B7 RID: 695
		public const string UseCurrentCulture = "UseCurrentCulture";

		// Token: 0x040002B8 RID: 696
		public static XmlQualifiedName QnString = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040002B9 RID: 697
		public static XmlQualifiedName QnShort = new XmlQualifiedName("short", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040002BA RID: 698
		public static XmlQualifiedName QnInt = new XmlQualifiedName("int", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040002BB RID: 699
		public static XmlQualifiedName QnLong = new XmlQualifiedName("long", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040002BC RID: 700
		public static XmlQualifiedName QnBoolean = new XmlQualifiedName("boolean", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040002BD RID: 701
		public static XmlQualifiedName QnUnsignedByte = new XmlQualifiedName("unsignedByte", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040002BE RID: 702
		public static XmlQualifiedName QnChar = new XmlQualifiedName("char", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040002BF RID: 703
		public static XmlQualifiedName QnDateTime = new XmlQualifiedName("dateTime", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040002C0 RID: 704
		public static XmlQualifiedName QnDecimal = new XmlQualifiedName("decimal", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040002C1 RID: 705
		public static XmlQualifiedName QnDouble = new XmlQualifiedName("double", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040002C2 RID: 706
		public static XmlQualifiedName QnSbyte = new XmlQualifiedName("byte", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040002C3 RID: 707
		public static XmlQualifiedName QnFloat = new XmlQualifiedName("float", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040002C4 RID: 708
		public static XmlQualifiedName QnDuration = new XmlQualifiedName("duration", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040002C5 RID: 709
		public static XmlQualifiedName QnUnsignedShort = new XmlQualifiedName("unsignedShort", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040002C6 RID: 710
		public static XmlQualifiedName QnUnsignedInt = new XmlQualifiedName("unsignedInt", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040002C7 RID: 711
		public static XmlQualifiedName QnUnsignedLong = new XmlQualifiedName("unsignedLong", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040002C8 RID: 712
		public static XmlQualifiedName QnUri = new XmlQualifiedName("anyURI", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040002C9 RID: 713
		public static XmlQualifiedName QnBase64Binary = new XmlQualifiedName("base64Binary", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040002CA RID: 714
		public static XmlQualifiedName QnXmlQualifiedName = new XmlQualifiedName("QName", "http://www.w3.org/2001/XMLSchema");
	}
}
