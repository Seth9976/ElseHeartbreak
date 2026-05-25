using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UnityEngine.Serialization
{
	// Token: 0x0200006D RID: 109
	public class UnitySurrogateSelector : ISurrogateSelector
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000A688 File Offset: 0x00008888
		public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
		{
			if (type.IsGenericType)
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				if (genericTypeDefinition == typeof(List<>))
				{
					selector = this;
					return ListSerializationSurrogate.Default;
				}
				if (genericTypeDefinition == typeof(Dictionary<, >))
				{
					selector = this;
					Type type2 = typeof(DictionarySerializationSurrogate<, >).MakeGenericType(type.GetGenericArguments());
					return (ISerializationSurrogate)Activator.CreateInstance(type2);
				}
			}
			selector = null;
			return null;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000A6FC File Offset: 0x000088FC
		public void ChainSelector(ISurrogateSelector selector)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000A704 File Offset: 0x00008904
		public ISurrogateSelector GetNextSelector()
		{
			throw new NotImplementedException();
		}
	}
}
