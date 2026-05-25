using System;
using System.Collections.Generic;

namespace System.Xml
{
	/// <summary>Enables optimized strings to be managed in a dynamic way.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000049 RID: 73
	public class XmlBinaryReaderSession : IXmlDictionary
	{
		/// <summary>Creates an <see cref="T:System.Xml.XmlDictionaryString" /> from the input parameters and adds it to an internal collection</summary>
		/// <returns>An <see cref="T:System.Xml.XmlDictionaryString" />.</returns>
		/// <param name="id">The key value.</param>
		/// <param name="value">The value.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="id" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">An entry with key = <paramref name="id" /> already exists.</exception>
		// Token: 0x06000241 RID: 577 RVA: 0x0000CDA0 File Offset: 0x0000AFA0
		public XmlDictionaryString Add(int id, string value)
		{
			XmlDictionaryString xmlDictionaryString = this.dic.Add(value);
			this.store[id] = xmlDictionaryString;
			return xmlDictionaryString;
		}

		/// <summary>Clears the internal collection of all contents.</summary>
		// Token: 0x06000242 RID: 578 RVA: 0x0000CDC8 File Offset: 0x0000AFC8
		public void Clear()
		{
			this.store.Clear();
		}

		/// <summary>Checks whether the internal collection contains an entry matching a key.</summary>
		/// <returns>true if an entry matching the <paramref name="key" /> was found; otherwise false.</returns>
		/// <param name="key">The key to search on.</param>
		/// <param name="result">The <see cref="T:System.Xml.XmlDictionaryString" />, if found; otherwise null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06000243 RID: 579 RVA: 0x0000CDD8 File Offset: 0x0000AFD8
		public bool TryLookup(int key, out XmlDictionaryString result)
		{
			return this.store.TryGetValue(key, out result);
		}

		/// <summary>Checks whether the internal collection contains an entry matching a value.</summary>
		/// <returns>true if an entry matching the <paramref name="value" /> was found; otherwise false.</returns>
		/// <param name="value">The value to search for.</param>
		/// <param name="result">The <see cref="T:System.Xml.XmlDictionaryString" />, if found; otherwise null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000244 RID: 580 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		public bool TryLookup(string value, out XmlDictionaryString result)
		{
			foreach (XmlDictionaryString xmlDictionaryString in this.store.Values)
			{
				if (xmlDictionaryString.Value == value)
				{
					result = xmlDictionaryString;
					return true;
				}
			}
			result = null;
			return false;
		}

		/// <summary>Checks whether the internal collection contains an entry matching a value.</summary>
		/// <returns>true if an entry matching the <paramref name="value" /> was found; otherwise false.</returns>
		/// <param name="value">The value to search for.</param>
		/// <param name="result">The <see cref="T:System.Xml.XmlDictionaryString" />, if found; otherwise null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000245 RID: 581 RVA: 0x0000CE70 File Offset: 0x0000B070
		public bool TryLookup(XmlDictionaryString value, out XmlDictionaryString result)
		{
			foreach (XmlDictionaryString xmlDictionaryString in this.store.Values)
			{
				if (xmlDictionaryString == value)
				{
					result = xmlDictionaryString;
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x04000139 RID: 313
		private XmlDictionary dic = new XmlDictionary();

		// Token: 0x0400013A RID: 314
		private Dictionary<int, XmlDictionaryString> store = new Dictionary<int, XmlDictionaryString>();
	}
}
