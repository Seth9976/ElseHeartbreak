using System;

namespace System.Xml
{
	/// <summary>Represents an entry stored in a <see cref="T:System.Xml.XmlDictionary" />.</summary>
	// Token: 0x0200004F RID: 79
	public class XmlDictionaryString
	{
		/// <summary>Creates an instance of this class.</summary>
		/// <param name="dictionary">The <see cref="T:System.Xml.IXmlDictionary" /> containing this instance.</param>
		/// <param name="value">The string that is the value of the dictionary entry.</param>
		/// <param name="key">The integer that is the key of the dictionary entry.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="key" /> is less than 0 or greater than <see cref="F:System.Int32.MaxValue" /> / 4.</exception>
		// Token: 0x060002E6 RID: 742 RVA: 0x0000EB0C File Offset: 0x0000CD0C
		public XmlDictionaryString(IXmlDictionary dictionary, string value, int key)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (key < 0 || key > 536870911)
			{
				throw new ArgumentOutOfRangeException("key");
			}
			this.dict = dictionary;
			this.value = value;
			this.key = key;
		}

		/// <summary>Gets an <see cref="T:System.Xml.XmlDictionaryString" /> representing the empty string.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlDictionaryString" /> representing the empty string.</returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000EB8C File Offset: 0x0000CD8C
		public static XmlDictionaryString Empty
		{
			get
			{
				return XmlDictionaryString.empty;
			}
		}

		/// <summary>Represents the <see cref="T:System.Xml.IXmlDictionary" /> passed to the constructor of this instance of <see cref="T:System.Xml.XmlDictionaryString" />.</summary>
		/// <returns>The <see cref="T:System.Xml.IXmlDictionary" /> for this dictionary entry.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000EB94 File Offset: 0x0000CD94
		public IXmlDictionary Dictionary
		{
			get
			{
				return this.dict;
			}
		}

		/// <summary>Gets the integer key for this instance of the class. </summary>
		/// <returns>The integer key for this instance of the class.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000EB9C File Offset: 0x0000CD9C
		public int Key
		{
			get
			{
				return this.key;
			}
		}

		/// <summary>Gets the string value for this instance of the class. </summary>
		/// <returns>The string value for this instance of the class.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000EBA4 File Offset: 0x0000CDA4
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Displays a text representation of this object.</summary>
		/// <returns>The string value for this instance of the class.</returns>
		// Token: 0x060002EC RID: 748 RVA: 0x0000EBAC File Offset: 0x0000CDAC
		public override string ToString()
		{
			return this.value;
		}

		// Token: 0x0400014B RID: 331
		private static XmlDictionaryString empty = new XmlDictionaryString(XmlDictionary.EmptyDictionary.Instance, string.Empty, 0);

		// Token: 0x0400014C RID: 332
		private readonly IXmlDictionary dict;

		// Token: 0x0400014D RID: 333
		private readonly string value;

		// Token: 0x0400014E RID: 334
		private readonly int key;
	}
}
