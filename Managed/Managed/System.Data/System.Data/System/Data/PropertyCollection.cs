using System;
using System.Collections;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents a collection of properties that can be added to <see cref="T:System.Data.DataColumn" />, <see cref="T:System.Data.DataSet" />, or <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200006A RID: 106
	[Serializable]
	public class PropertyCollection : Hashtable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.PropertyCollection" /> class.</summary>
		// Token: 0x0600063D RID: 1597 RVA: 0x0001F274 File Offset: 0x0001D474
		public PropertyCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.PropertyCollection" /> class.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object.</param>
		/// <param name="context">The source and destination of a given serialized stream.</param>
		// Token: 0x0600063E RID: 1598 RVA: 0x0001F27C File Offset: 0x0001D47C
		protected PropertyCollection(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
