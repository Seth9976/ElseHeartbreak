using System;

namespace System.Xml
{
	/// <summary>An interface that defines the contract that an Xml dictionary must implement to be used by <see cref="T:System.Xml.XmlDictionaryReader" /> and <see cref="T:System.Xml.XmlDictionaryWriter" /> implementations.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200003A RID: 58
	public interface IXmlDictionary
	{
		/// <summary>Attempts to look up an entry in the dictionary.</summary>
		/// <returns>true if key is in the dictionary, otherwise false.</returns>
		/// <param name="key">Key to look up.</param>
		/// <param name="result">If <paramref name="key" /> is defined, the <see cref="T:System.Xml.XmlDictionaryString" /> that is mapped to the key; otherwise null.</param>
		// Token: 0x06000165 RID: 357
		bool TryLookup(int key, out XmlDictionaryString result);

		/// <summary>Checks the dictionary for a specified string value.</summary>
		/// <returns>true if value is in the dictionary, otherwise false.</returns>
		/// <param name="value">String value being checked for.</param>
		/// <param name="result">The corresponding <see cref="T:System.Xml.XmlDictionaryString" />, if found; otherwise null.</param>
		// Token: 0x06000166 RID: 358
		bool TryLookup(string value, out XmlDictionaryString result);

		/// <summary>Checks the dictionary for a specified <see cref="T:System.Xml.XmlDictionaryString" />.</summary>
		/// <returns>true if <see cref="T:System.Xml.XmlDictionaryString" /> is in the dictionary, otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.Xml.XmlDictionaryString" /> being checked for.</param>
		/// <param name="result">The matching <see cref="T:System.Xml.XmlDictionaryString" />, if found; otherwise null.</param>
		// Token: 0x06000167 RID: 359
		bool TryLookup(XmlDictionaryString value, out XmlDictionaryString result);
	}
}
