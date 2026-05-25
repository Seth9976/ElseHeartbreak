using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000080 RID: 128
	public class JsonArrayContract : JsonContract
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x000152B8 File Offset: 0x000134B8
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x000152C0 File Offset: 0x000134C0
		internal Type CollectionItemType { get; private set; }

		// Token: 0x06000618 RID: 1560 RVA: 0x000152CC File Offset: 0x000134CC
		public JsonArrayContract(Type underlyingType)
			: base(underlyingType)
		{
			if (ReflectionUtils.ImplementsGenericDefinition(underlyingType, typeof(ICollection<>), out this._genericCollectionDefinitionType))
			{
				this.CollectionItemType = this._genericCollectionDefinitionType.GetGenericArguments()[0];
			}
			else if (underlyingType.IsGenericType && underlyingType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
			{
				this._genericCollectionDefinitionType = typeof(IEnumerable<>);
				this.CollectionItemType = underlyingType.GetGenericArguments()[0];
			}
			else
			{
				this.CollectionItemType = ReflectionUtils.GetCollectionItemType(base.UnderlyingType);
			}
			if (this.CollectionItemType != null)
			{
				this._isCollectionItemTypeNullableType = ReflectionUtils.IsNullableType(this.CollectionItemType);
			}
			if (this.IsTypeGenericCollectionInterface(base.UnderlyingType))
			{
				base.CreatedType = ReflectionUtils.MakeGenericType(typeof(List<>), new Type[] { this.CollectionItemType });
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000153A8 File Offset: 0x000135A8
		internal IWrappedCollection CreateWrapper(object list)
		{
			if ((list is IList && (this.CollectionItemType == null || !this._isCollectionItemTypeNullableType)) || base.UnderlyingType.IsArray)
			{
				return new CollectionWrapper<object>((IList)list);
			}
			if (this._genericCollectionDefinitionType != null)
			{
				this.EnsureGenericWrapperCreator();
				return (IWrappedCollection)this._genericWrapperCreator(null, new object[] { list });
			}
			IList list2 = ((IEnumerable)list).Cast<object>().ToList<object>();
			if (this.CollectionItemType != null)
			{
				Array array = Array.CreateInstance(this.CollectionItemType, list2.Count);
				for (int i = 0; i < list2.Count; i++)
				{
					array.SetValue(list2[i], i);
				}
				list2 = array;
			}
			return new CollectionWrapper<object>(list2);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00015464 File Offset: 0x00013664
		private void EnsureGenericWrapperCreator()
		{
			if (this._genericWrapperType == null)
			{
				this._genericWrapperType = ReflectionUtils.MakeGenericType(typeof(CollectionWrapper<>), new Type[] { this.CollectionItemType });
				Type type;
				if (ReflectionUtils.InheritsGenericDefinition(this._genericCollectionDefinitionType, typeof(List<>)) || this._genericCollectionDefinitionType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				{
					type = ReflectionUtils.MakeGenericType(typeof(ICollection<>), new Type[] { this.CollectionItemType });
				}
				else
				{
					type = this._genericCollectionDefinitionType;
				}
				ConstructorInfo constructor = this._genericWrapperType.GetConstructor(new Type[] { type });
				this._genericWrapperCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(constructor);
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00015528 File Offset: 0x00013728
		private bool IsTypeGenericCollectionInterface(Type type)
		{
			if (!type.IsGenericType)
			{
				return false;
			}
			Type genericTypeDefinition = type.GetGenericTypeDefinition();
			return genericTypeDefinition == typeof(IList<>) || genericTypeDefinition == typeof(ICollection<>) || genericTypeDefinition == typeof(IEnumerable<>);
		}

		// Token: 0x040001A5 RID: 421
		private readonly bool _isCollectionItemTypeNullableType;

		// Token: 0x040001A6 RID: 422
		private readonly Type _genericCollectionDefinitionType;

		// Token: 0x040001A7 RID: 423
		private Type _genericWrapperType;

		// Token: 0x040001A8 RID: 424
		private MethodCall<object, object> _genericWrapperCreator;
	}
}
