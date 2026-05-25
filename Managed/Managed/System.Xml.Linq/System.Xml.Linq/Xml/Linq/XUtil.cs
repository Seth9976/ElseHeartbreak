using System;
using System.Collections;
using System.Globalization;

namespace System.Xml.Linq
{
	// Token: 0x02000025 RID: 37
	internal static class XUtil
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x00009040 File Offset: 0x00007240
		public static bool ConvertToBoolean(string s)
		{
			return XmlConvert.ToBoolean(s.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00009054 File Offset: 0x00007254
		public static DateTime ToDateTime(string s)
		{
			DateTime dateTime;
			try
			{
				dateTime = XmlConvert.ToDateTime(s, XmlDateTimeSerializationMode.RoundtripKind);
			}
			catch
			{
				dateTime = DateTime.Parse(s);
			}
			return dateTime;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000090A4 File Offset: 0x000072A4
		public static string ToString(object o)
		{
			if (o == null)
			{
				throw new InvalidOperationException("Attempt to get string from null");
			}
			TypeCode typeCode = Type.GetTypeCode(o.GetType());
			switch (typeCode)
			{
			case TypeCode.Single:
				return ((float)o).ToString("r");
			case TypeCode.Double:
				return ((double)o).ToString("r");
			default:
				if (typeCode == TypeCode.Boolean)
				{
					return o.ToString().ToLower();
				}
				if (o is TimeSpan)
				{
					return XmlConvert.ToString((TimeSpan)o);
				}
				if (o is DateTimeOffset)
				{
					return XmlConvert.ToString((DateTimeOffset)o);
				}
				return o.ToString();
			case TypeCode.DateTime:
				return XmlConvert.ToString((DateTime)o, XmlDateTimeSerializationMode.RoundtripKind);
			case TypeCode.String:
				return (string)o;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00009178 File Offset: 0x00007378
		public static bool ToBoolean(object o)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00009180 File Offset: 0x00007380
		public static bool? ToNullableBoolean(object o)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009188 File Offset: 0x00007388
		public static IEnumerable ExpandArray(object o)
		{
			XNode i = o as XNode;
			if (i != null)
			{
				yield return i;
			}
			else if (o is string)
			{
				yield return o;
			}
			else if (o is IEnumerable)
			{
				foreach (object obj in ((IEnumerable)o))
				{
					foreach (object oo in XUtil.ExpandArray(obj))
					{
						yield return oo;
					}
				}
			}
			else
			{
				yield return o;
			}
			yield break;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000091B4 File Offset: 0x000073B4
		public static XNode ToNode(object o)
		{
			if (o is XAttribute)
			{
				throw new ArgumentException("Attribute node is not allowed as argument");
			}
			XNode xnode = o as XNode;
			if (xnode != null)
			{
				return xnode;
			}
			if (o is string)
			{
				return new XText((string)o);
			}
			return new XText(XUtil.ToString(o));
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00009208 File Offset: 0x00007408
		public static object GetDetachedObject(XObject child)
		{
			return (child.Owner == null) ? child : XUtil.Clone(child);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00009224 File Offset: 0x00007424
		public static object Clone(object o)
		{
			if (o is string)
			{
				return (string)o;
			}
			if (o is XAttribute)
			{
				return new XAttribute((XAttribute)o);
			}
			if (o is XElement)
			{
				return new XElement((XElement)o);
			}
			if (o is XCData)
			{
				return new XCData((XCData)o);
			}
			if (o is XComment)
			{
				return new XComment((XComment)o);
			}
			if (o is XProcessingInstruction)
			{
				return new XProcessingInstruction((XProcessingInstruction)o);
			}
			if (o is XDeclaration)
			{
				return new XDeclaration((XDeclaration)o);
			}
			if (o is XDocumentType)
			{
				return new XDocumentType((XDocumentType)o);
			}
			if (o is XText)
			{
				return new XText((XText)o);
			}
			throw new ArgumentException();
		}

		// Token: 0x0400007B RID: 123
		public const string XmlnsNamespace = "http://www.w3.org/2000/xmlns/";
	}
}
