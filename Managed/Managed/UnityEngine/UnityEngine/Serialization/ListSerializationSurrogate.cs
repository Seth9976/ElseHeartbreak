using System;
using System.Collections;
using System.Runtime.Serialization;

namespace UnityEngine.Serialization
{
	// Token: 0x0200006E RID: 110
	internal class ListSerializationSurrogate : ISerializationSurrogate
	{
		// Token: 0x0600028C RID: 652 RVA: 0x0000A720 File Offset: 0x00008920
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			IList list = (IList)obj;
			info.AddValue("_size", list.Count);
			info.AddValue("_items", ListSerializationSurrogate.ArrayFromGenericList(list));
			info.AddValue("_version", 0);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000A764 File Offset: 0x00008964
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			IList list = (IList)Activator.CreateInstance(obj.GetType());
			int @int = info.GetInt32("_size");
			if (@int == 0)
			{
				return list;
			}
			IEnumerator enumerator = ((IEnumerable)info.GetValue("_items", typeof(IEnumerable))).GetEnumerator();
			for (int i = 0; i < @int; i++)
			{
				if (!enumerator.MoveNext())
				{
					throw new InvalidOperationException();
				}
				list.Add(enumerator.Current);
			}
			return list;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000A7E8 File Offset: 0x000089E8
		private static Array ArrayFromGenericList(IList list)
		{
			Array array = Array.CreateInstance(list.GetType().GetGenericArguments()[0], list.Count);
			list.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0400019D RID: 413
		public static readonly ISerializationSurrogate Default = new ListSerializationSurrogate();
	}
}
