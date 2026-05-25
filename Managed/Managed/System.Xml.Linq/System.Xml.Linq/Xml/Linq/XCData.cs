using System;
using System.Text;

namespace System.Xml.Linq
{
	/// <summary>Represents a text node that contains CDATA. </summary>
	// Token: 0x0200000E RID: 14
	public class XCData : XText
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XCData" /> class. </summary>
		/// <param name="value">A string that contains the value of the <see cref="T:System.Xml.Linq.XCData" /> node.</param>
		// Token: 0x06000047 RID: 71 RVA: 0x00002D9C File Offset: 0x00000F9C
		public XCData(string value)
			: base(value)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XCData" /> class. </summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XCData" /> node to copy from.</param>
		// Token: 0x06000048 RID: 72 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public XCData(XCData other)
			: base(other)
		{
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XCData" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.CDATA" />.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.CDATA;
			}
		}

		/// <summary>Writes this CDATA object to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600004A RID: 74 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public override void WriteTo(XmlWriter w)
		{
			int num = 0;
			StringBuilder stringBuilder = null;
			for (int i = 0; i < base.Value.Length - 2; i++)
			{
				if (base.Value[i] == ']' && base.Value[i + 1] == ']' && base.Value[i + 2] == '>')
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					stringBuilder.Append(base.Value, num, i - num);
					stringBuilder.Append("]]&gt;");
					num = i + 3;
				}
			}
			if (num != 0 && num != base.Value.Length)
			{
				stringBuilder.Append(base.Value, num, base.Value.Length - num);
			}
			w.WriteCData((stringBuilder != null) ? stringBuilder.ToString() : base.Value);
		}
	}
}
