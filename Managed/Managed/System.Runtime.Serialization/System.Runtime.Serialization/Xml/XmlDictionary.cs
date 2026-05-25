using System;
using System.Collections.Generic;

namespace System.Xml
{
	/// <summary>Implements a dictionary used to optimize Windows Communication Foundation (WCF)'s XML reader/writer implementations.</summary>
	// Token: 0x0200004B RID: 75
	public class XmlDictionary : IXmlDictionary
	{
		/// <summary>Creates an empty <see cref="T:System.Xml.XmlDictionary" />.</summary>
		// Token: 0x0600024A RID: 586 RVA: 0x0000CFF8 File Offset: 0x0000B1F8
		public XmlDictionary()
		{
			this.dict = new Dictionary<string, XmlDictionaryString>();
			this.list = new List<XmlDictionaryString>();
		}

		/// <summary>Creates a <see cref="T:System.Xml.XmlDictionary" /> with an initial capacity.</summary>
		/// <param name="capacity">The initial size of the dictionary.</param>
		// Token: 0x0600024B RID: 587 RVA: 0x0000D018 File Offset: 0x0000B218
		public XmlDictionary(int capacity)
		{
			this.dict = new Dictionary<string, XmlDictionaryString>(capacity);
			this.list = new List<XmlDictionaryString>(capacity);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000D038 File Offset: 0x0000B238
		private XmlDictionary(bool isReadOnly)
			: this(1)
		{
			this.is_readonly = isReadOnly;
		}

		/// <summary>Gets a static empty <see cref="T:System.Xml.IXmlDictionary" />.</summary>
		/// <returns>A static empty <see cref="T:System.Xml.IXmlDictionary" />.</returns>
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000D058 File Offset: 0x0000B258
		public static IXmlDictionary Empty
		{
			get
			{
				return XmlDictionary.empty;
			}
		}

		/// <summary>Adds a string to the <see cref="T:System.Xml.XmlDictionary" />.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlDictionaryString" /> that was added.</returns>
		/// <param name="value">String to add to the dictionary.</param>
		// Token: 0x0600024F RID: 591 RVA: 0x0000D060 File Offset: 0x0000B260
		public virtual XmlDictionaryString Add(string value)
		{
			if (this.is_readonly)
			{
				throw new InvalidOperationException();
			}
			XmlDictionaryString xmlDictionaryString;
			if (this.dict.TryGetValue(value, out xmlDictionaryString))
			{
				return xmlDictionaryString;
			}
			xmlDictionaryString = new XmlDictionaryString(this, value, this.dict.Count);
			this.dict.Add(value, xmlDictionaryString);
			this.list.Add(xmlDictionaryString);
			return xmlDictionaryString;
		}

		/// <summary>Attempts to look up an entry in the dictionary.</summary>
		/// <returns>true if key is in the dictionary, otherwise false.</returns>
		/// <param name="key">Key to look up.</param>
		/// <param name="result">If <paramref name="key" /> is defined, the <see cref="T:System.Xml.XmlDictionaryString" /> that is mapped to the key; otherwise null.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000250 RID: 592 RVA: 0x0000D0C0 File Offset: 0x0000B2C0
		public virtual bool TryLookup(int key, out XmlDictionaryString result)
		{
			if (key < 0 || this.dict.Count <= key)
			{
				result = null;
				return false;
			}
			result = this.list[key];
			return true;
		}

		/// <summary>Checks the dictionary for a specified string value.</summary>
		/// <returns>true if value is in the dictionary, otherwise false.</returns>
		/// <param name="value">String value being checked for.</param>
		/// <param name="result">The corresponding <see cref="T:System.Xml.XmlDictionaryString" />, if found; otherwise null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000251 RID: 593 RVA: 0x0000D0FC File Offset: 0x0000B2FC
		public virtual bool TryLookup(string value, out XmlDictionaryString result)
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			return this.dict.TryGetValue(value, out result);
		}

		/// <summary>Checks the dictionary for a specified <see cref="T:System.Xml.XmlDictionaryString" />.</summary>
		/// <returns>true if <see cref="T:System.Xml.XmlDictionaryString" /> is in the dictionary, otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.Xml.XmlDictionaryString" /> being checked for.</param>
		/// <param name="result">The matching <see cref="T:System.Xml.XmlDictionaryString" />, if found; otherwise null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000252 RID: 594 RVA: 0x0000D118 File Offset: 0x0000B318
		public virtual bool TryLookup(XmlDictionaryString value, out XmlDictionaryString result)
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			if (value.Dictionary != this)
			{
				result = null;
				return false;
			}
			for (int i = 0; i < this.list.Count; i++)
			{
				if (object.ReferenceEquals(this.list[i], value))
				{
					result = value;
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x0400013C RID: 316
		private static XmlDictionary empty = new XmlDictionary(true);

		// Token: 0x0400013D RID: 317
		private readonly bool is_readonly;

		// Token: 0x0400013E RID: 318
		private Dictionary<string, XmlDictionaryString> dict;

		// Token: 0x0400013F RID: 319
		private List<XmlDictionaryString> list;

		// Token: 0x0200004C RID: 76
		internal class EmptyDictionary : XmlDictionary
		{
			// Token: 0x06000253 RID: 595 RVA: 0x0000D180 File Offset: 0x0000B380
			public EmptyDictionary()
				: base(1)
			{
			}

			// Token: 0x04000140 RID: 320
			public static readonly XmlDictionary.EmptyDictionary Instance = new XmlDictionary.EmptyDictionary();
		}
	}
}
