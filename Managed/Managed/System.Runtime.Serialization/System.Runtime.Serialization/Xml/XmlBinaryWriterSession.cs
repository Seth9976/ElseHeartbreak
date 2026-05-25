using System;
using System.Collections.Generic;

namespace System.Xml
{
	/// <summary>Enables using a dynamic dictionary to compress common strings that appear in a message and maintain state.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200004A RID: 74
	public class XmlBinaryWriterSession
	{
		/// <summary>Clears out the internal collections.</summary>
		// Token: 0x06000247 RID: 583 RVA: 0x0000CF00 File Offset: 0x0000B100
		public void Reset()
		{
			this.dic.Clear();
		}

		/// <summary>Tries to add an <see cref="T:System.Xml.XmlDictionaryString" /> to the internal collection.</summary>
		/// <returns>true, unless an exception was thrown.</returns>
		/// <param name="value">The <see cref="T:System.Xml.XmlDictionaryString" /> to add.</param>
		/// <param name="key">The key of the <see cref="T:System.Xml.XmlDictionaryString" /> that was successfully added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">An entry with key = <paramref name="key" /> already exists.</exception>
		// Token: 0x06000248 RID: 584 RVA: 0x0000CF10 File Offset: 0x0000B110
		public virtual bool TryAdd(XmlDictionaryString value, out int key)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.TryLookup(value, out key))
			{
				throw new InvalidOperationException("Argument XmlDictionaryString was already added to the writer session");
			}
			key = this.dic.Count;
			this.dic.Add(key, value);
			return true;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000CF64 File Offset: 0x0000B164
		internal bool TryLookup(XmlDictionaryString value, out int key)
		{
			foreach (KeyValuePair<int, XmlDictionaryString> keyValuePair in this.dic)
			{
				if (keyValuePair.Value.Value == value.Value)
				{
					key = keyValuePair.Key;
					return true;
				}
			}
			key = -1;
			return false;
		}

		// Token: 0x0400013B RID: 315
		private Dictionary<int, XmlDictionaryString> dic = new Dictionary<int, XmlDictionaryString>();
	}
}
