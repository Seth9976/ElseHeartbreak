using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000063 RID: 99
	public abstract class JContainer : JToken, IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, ITypedList, IBindingList, IList, ICollection, IEnumerable
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000457 RID: 1111 RVA: 0x0000FD08 File Offset: 0x0000DF08
		// (remove) Token: 0x06000458 RID: 1112 RVA: 0x0000FD40 File Offset: 0x0000DF40
		public event ListChangedEventHandler ListChanged;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000459 RID: 1113 RVA: 0x0000FD78 File Offset: 0x0000DF78
		// (remove) Token: 0x0600045A RID: 1114 RVA: 0x0000FDB0 File Offset: 0x0000DFB0
		public event AddingNewEventHandler AddingNew;

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600045B RID: 1115
		protected abstract IList<JToken> ChildrenTokens { get; }

		// Token: 0x0600045C RID: 1116 RVA: 0x0000FDE5 File Offset: 0x0000DFE5
		internal JContainer()
		{
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
		internal JContainer(JContainer other)
		{
			ValidationUtils.ArgumentNotNull(other, "c");
			foreach (JToken jtoken in ((IEnumerable<JToken>)other))
			{
				this.Add(jtoken);
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000FE4C File Offset: 0x0000E04C
		internal void CheckReentrancy()
		{
			if (this._busy)
			{
				throw new InvalidOperationException("Cannot change {0} during a collection change event.".FormatWith(CultureInfo.InvariantCulture, new object[] { base.GetType() }));
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000FE88 File Offset: 0x0000E088
		protected virtual void OnAddingNew(AddingNewEventArgs e)
		{
			AddingNewEventHandler addingNew = this.AddingNew;
			if (addingNew != null)
			{
				addingNew(this, e);
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			ListChangedEventHandler listChanged = this.ListChanged;
			if (listChanged != null)
			{
				this._busy = true;
				try
				{
					listChanged(this, e);
				}
				finally
				{
					this._busy = false;
				}
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000FEE8 File Offset: 0x0000E0E8
		public override bool HasValues
		{
			get
			{
				return this.ChildrenTokens.Count > 0;
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000FEF8 File Offset: 0x0000E0F8
		internal bool ContentsEqual(JContainer container)
		{
			JToken jtoken = this.First;
			JToken jtoken2 = container.First;
			if (jtoken == jtoken2)
			{
				return true;
			}
			while (jtoken != null || jtoken2 != null)
			{
				if (jtoken == null || jtoken2 == null || !jtoken.DeepEquals(jtoken2))
				{
					return false;
				}
				jtoken = ((jtoken != this.Last) ? jtoken.Next : null);
				jtoken2 = ((jtoken2 != container.Last) ? jtoken2.Next : null);
			}
			return true;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000FF59 File Offset: 0x0000E159
		public override JToken First
		{
			get
			{
				return this.ChildrenTokens.FirstOrDefault<JToken>();
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000FF66 File Offset: 0x0000E166
		public override JToken Last
		{
			get
			{
				return this.ChildrenTokens.LastOrDefault<JToken>();
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000FF73 File Offset: 0x0000E173
		public override JEnumerable<JToken> Children()
		{
			return new JEnumerable<JToken>(this.ChildrenTokens);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000FF80 File Offset: 0x0000E180
		public override IEnumerable<T> Values<T>()
		{
			return this.ChildrenTokens.Convert<JToken, T>();
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001020C File Offset: 0x0000E40C
		public IEnumerable<JToken> Descendants()
		{
			foreach (JToken o in this.ChildrenTokens)
			{
				yield return o;
				JContainer c = o as JContainer;
				if (c != null)
				{
					foreach (JToken d in c.Descendants())
					{
						yield return d;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00010229 File Offset: 0x0000E429
		internal bool IsMultiContent(object content)
		{
			return content is IEnumerable && !(content is string) && !(content is JToken) && !(content is byte[]);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00010254 File Offset: 0x0000E454
		internal JToken EnsureParentToken(JToken item)
		{
			if (item == null)
			{
				return new JValue(null);
			}
			if (item.Parent != null)
			{
				item = item.CloneToken();
			}
			else
			{
				JContainer jcontainer = this;
				while (jcontainer.Parent != null)
				{
					jcontainer = jcontainer.Parent;
				}
				if (item == jcontainer)
				{
					item = item.CloneToken();
				}
			}
			return item;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001029D File Offset: 0x0000E49D
		internal int IndexOfItem(JToken item)
		{
			return this.ChildrenTokens.IndexOf(item, JContainer.JTokenReferenceEqualityComparer.Instance);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000102B0 File Offset: 0x0000E4B0
		internal virtual void InsertItem(int index, JToken item)
		{
			if (index > this.ChildrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index must be within the bounds of the List.");
			}
			this.CheckReentrancy();
			item = this.EnsureParentToken(item);
			JToken jtoken = ((index == 0) ? null : this.ChildrenTokens[index - 1]);
			JToken jtoken2 = ((index == this.ChildrenTokens.Count) ? null : this.ChildrenTokens[index]);
			this.ValidateToken(item, null);
			item.Parent = this;
			item.Previous = jtoken;
			if (jtoken != null)
			{
				jtoken.Next = item;
			}
			item.Next = jtoken2;
			if (jtoken2 != null)
			{
				jtoken2.Previous = item;
			}
			this.ChildrenTokens.Insert(index, item);
			if (this.ListChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00010370 File Offset: 0x0000E570
		internal virtual void RemoveItemAt(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Index is less than 0.");
			}
			if (index >= this.ChildrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index is equal to or greater than Count.");
			}
			this.CheckReentrancy();
			JToken jtoken = this.ChildrenTokens[index];
			JToken jtoken2 = ((index == 0) ? null : this.ChildrenTokens[index - 1]);
			JToken jtoken3 = ((index == this.ChildrenTokens.Count - 1) ? null : this.ChildrenTokens[index + 1]);
			if (jtoken2 != null)
			{
				jtoken2.Next = jtoken3;
			}
			if (jtoken3 != null)
			{
				jtoken3.Previous = jtoken2;
			}
			jtoken.Parent = null;
			jtoken.Previous = null;
			jtoken.Next = null;
			this.ChildrenTokens.RemoveAt(index);
			this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001043C File Offset: 0x0000E63C
		internal virtual bool RemoveItem(JToken item)
		{
			int num = this.IndexOfItem(item);
			if (num >= 0)
			{
				this.RemoveItemAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001045F File Offset: 0x0000E65F
		internal virtual JToken GetItem(int index)
		{
			return this.ChildrenTokens[index];
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00010470 File Offset: 0x0000E670
		internal virtual void SetItem(int index, JToken item)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Index is less than 0.");
			}
			if (index >= this.ChildrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index is equal to or greater than Count.");
			}
			JToken jtoken = this.ChildrenTokens[index];
			if (JContainer.IsTokenUnchanged(jtoken, item))
			{
				return;
			}
			this.CheckReentrancy();
			item = this.EnsureParentToken(item);
			this.ValidateToken(item, jtoken);
			JToken jtoken2 = ((index == 0) ? null : this.ChildrenTokens[index - 1]);
			JToken jtoken3 = ((index == this.ChildrenTokens.Count - 1) ? null : this.ChildrenTokens[index + 1]);
			item.Parent = this;
			item.Previous = jtoken2;
			if (jtoken2 != null)
			{
				jtoken2.Next = item;
			}
			item.Next = jtoken3;
			if (jtoken3 != null)
			{
				jtoken3.Previous = item;
			}
			this.ChildrenTokens[index] = item;
			jtoken.Parent = null;
			jtoken.Previous = null;
			jtoken.Next = null;
			this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001056C File Offset: 0x0000E76C
		internal virtual void ClearItems()
		{
			this.CheckReentrancy();
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				jtoken.Parent = null;
				jtoken.Previous = null;
				jtoken.Next = null;
			}
			this.ChildrenTokens.Clear();
			this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x000105E8 File Offset: 0x0000E7E8
		internal virtual void ReplaceItem(JToken existing, JToken replacement)
		{
			if (existing == null || existing.Parent != this)
			{
				return;
			}
			int num = this.IndexOfItem(existing);
			this.SetItem(num, replacement);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00010612 File Offset: 0x0000E812
		internal virtual bool ContainsItem(JToken item)
		{
			return this.IndexOfItem(item) != -1;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00010624 File Offset: 0x0000E824
		internal virtual void CopyItemsTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", "arrayIndex is less than 0.");
			}
			if (arrayIndex >= array.Length)
			{
				throw new ArgumentException("arrayIndex is equal to or greater than the length of array.");
			}
			if (this.Count > array.Length - arrayIndex)
			{
				throw new ArgumentException("The number of elements in the source JObject is greater than the available space from arrayIndex to the end of the destination array.");
			}
			int num = 0;
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				array.SetValue(jtoken, arrayIndex + num);
				num++;
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000106CC File Offset: 0x0000E8CC
		internal static bool IsTokenUnchanged(JToken currentValue, JToken newValue)
		{
			JValue jvalue = currentValue as JValue;
			return jvalue != null && ((jvalue.Type == JTokenType.Null && newValue == null) || jvalue.Equals(newValue));
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x000106FC File Offset: 0x0000E8FC
		internal virtual void ValidateToken(JToken o, JToken existing)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			if (o.Type == JTokenType.Property)
			{
				throw new ArgumentException("Can not add {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					o.GetType(),
					base.GetType()
				}));
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001074C File Offset: 0x0000E94C
		public virtual void Add(object content)
		{
			this.AddInternal(this.ChildrenTokens.Count, content);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00010760 File Offset: 0x0000E960
		public void AddFirst(object content)
		{
			this.AddInternal(0, content);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001076C File Offset: 0x0000E96C
		internal void AddInternal(int index, object content)
		{
			if (this.IsMultiContent(content))
			{
				IEnumerable enumerable = (IEnumerable)content;
				int num = index;
				using (IEnumerator enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						this.AddInternal(num, obj);
						num++;
					}
					return;
				}
			}
			JToken jtoken = this.CreateFromContent(content);
			this.InsertItem(index, jtoken);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000107EC File Offset: 0x0000E9EC
		internal JToken CreateFromContent(object content)
		{
			if (content is JToken)
			{
				return (JToken)content;
			}
			return new JValue(content);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00010803 File Offset: 0x0000EA03
		public JsonWriter CreateWriter()
		{
			return new JTokenWriter(this);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001080B File Offset: 0x0000EA0B
		public void ReplaceAll(object content)
		{
			this.ClearItems();
			this.Add(content);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0001081A File Offset: 0x0000EA1A
		public void RemoveAll()
		{
			this.ClearItems();
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00010824 File Offset: 0x0000EA24
		internal void ReadTokenFrom(JsonReader r)
		{
			int depth = r.Depth;
			if (!r.Read())
			{
				throw new Exception("Error reading {0} from JsonReader.".FormatWith(CultureInfo.InvariantCulture, new object[] { base.GetType().Name }));
			}
			this.ReadContentFrom(r);
			int depth2 = r.Depth;
			if (depth2 > depth)
			{
				throw new Exception("Unexpected end of content while loading {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { base.GetType().Name }));
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000108A8 File Offset: 0x0000EAA8
		internal void ReadContentFrom(JsonReader r)
		{
			ValidationUtils.ArgumentNotNull(r, "r");
			IJsonLineInfo jsonLineInfo = r as IJsonLineInfo;
			JContainer jcontainer = this;
			for (;;)
			{
				if (jcontainer is JProperty && ((JProperty)jcontainer).Value != null)
				{
					if (jcontainer == this)
					{
						break;
					}
					jcontainer = jcontainer.Parent;
				}
				switch (r.TokenType)
				{
				case JsonToken.None:
					goto IL_0224;
				case JsonToken.StartObject:
				{
					JObject jobject = new JObject();
					jobject.SetLineInfo(jsonLineInfo);
					jcontainer.Add(jobject);
					jcontainer = jobject;
					goto IL_0224;
				}
				case JsonToken.StartArray:
				{
					JArray jarray = new JArray();
					jarray.SetLineInfo(jsonLineInfo);
					jcontainer.Add(jarray);
					jcontainer = jarray;
					goto IL_0224;
				}
				case JsonToken.StartConstructor:
				{
					JConstructor jconstructor = new JConstructor(r.Value.ToString());
					jconstructor.SetLineInfo(jconstructor);
					jcontainer.Add(jconstructor);
					jcontainer = jconstructor;
					goto IL_0224;
				}
				case JsonToken.PropertyName:
				{
					string text = r.Value.ToString();
					JProperty jproperty = new JProperty(text);
					jproperty.SetLineInfo(jsonLineInfo);
					JObject jobject2 = (JObject)jcontainer;
					JProperty jproperty2 = jobject2.Property(text);
					if (jproperty2 == null)
					{
						jcontainer.Add(jproperty);
					}
					else
					{
						jproperty2.Replace(jproperty);
					}
					jcontainer = jproperty;
					goto IL_0224;
				}
				case JsonToken.Comment:
				{
					JValue jvalue = JValue.CreateComment(r.Value.ToString());
					jvalue.SetLineInfo(jsonLineInfo);
					jcontainer.Add(jvalue);
					goto IL_0224;
				}
				case JsonToken.Integer:
				case JsonToken.Float:
				case JsonToken.String:
				case JsonToken.Boolean:
				case JsonToken.Date:
				case JsonToken.Bytes:
				{
					JValue jvalue = new JValue(r.Value);
					jvalue.SetLineInfo(jsonLineInfo);
					jcontainer.Add(jvalue);
					goto IL_0224;
				}
				case JsonToken.Null:
				{
					JValue jvalue = new JValue(null, JTokenType.Null);
					jvalue.SetLineInfo(jsonLineInfo);
					jcontainer.Add(jvalue);
					goto IL_0224;
				}
				case JsonToken.Undefined:
				{
					JValue jvalue = new JValue(null, JTokenType.Undefined);
					jvalue.SetLineInfo(jsonLineInfo);
					jcontainer.Add(jvalue);
					goto IL_0224;
				}
				case JsonToken.EndObject:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_0224;
				case JsonToken.EndArray:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_0224;
				case JsonToken.EndConstructor:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_0224;
				}
				goto Block_4;
				IL_0224:
				if (!r.Read())
				{
					return;
				}
			}
			return;
			Block_4:
			throw new InvalidOperationException("The JsonReader should not be on a token of type {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { r.TokenType }));
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00010AE4 File Offset: 0x0000ECE4
		internal int ContentsHashCode()
		{
			int num = 0;
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				num ^= jtoken.GetDeepHashCode();
			}
			return num;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00010B38 File Offset: 0x0000ED38
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			return string.Empty;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00010B40 File Offset: 0x0000ED40
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			ICustomTypeDescriptor customTypeDescriptor = this.First as ICustomTypeDescriptor;
			if (customTypeDescriptor != null)
			{
				return customTypeDescriptor.GetProperties();
			}
			return null;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00010B64 File Offset: 0x0000ED64
		int IList<JToken>.IndexOf(JToken item)
		{
			return this.IndexOfItem(item);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00010B6D File Offset: 0x0000ED6D
		void IList<JToken>.Insert(int index, JToken item)
		{
			this.InsertItem(index, item);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00010B77 File Offset: 0x0000ED77
		void IList<JToken>.RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x170000DD RID: 221
		JToken IList<JToken>.this[int index]
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, value);
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00010B93 File Offset: 0x0000ED93
		void ICollection<JToken>.Add(JToken item)
		{
			this.Add(item);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00010B9C File Offset: 0x0000ED9C
		void ICollection<JToken>.Clear()
		{
			this.ClearItems();
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00010BA4 File Offset: 0x0000EDA4
		bool ICollection<JToken>.Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00010BAD File Offset: 0x0000EDAD
		void ICollection<JToken>.CopyTo(JToken[] array, int arrayIndex)
		{
			this.CopyItemsTo(array, arrayIndex);
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00010BB7 File Offset: 0x0000EDB7
		bool ICollection<JToken>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00010BBA File Offset: 0x0000EDBA
		bool ICollection<JToken>.Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00010BC3 File Offset: 0x0000EDC3
		private JToken EnsureValue(object value)
		{
			if (value == null)
			{
				return null;
			}
			if (value is JToken)
			{
				return (JToken)value;
			}
			throw new ArgumentException("Argument is not a JToken.");
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00010BE3 File Offset: 0x0000EDE3
		int IList.Add(object value)
		{
			this.Add(this.EnsureValue(value));
			return this.Count - 1;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00010BFA File Offset: 0x0000EDFA
		void IList.Clear()
		{
			this.ClearItems();
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00010C02 File Offset: 0x0000EE02
		bool IList.Contains(object value)
		{
			return this.ContainsItem(this.EnsureValue(value));
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00010C11 File Offset: 0x0000EE11
		int IList.IndexOf(object value)
		{
			return this.IndexOfItem(this.EnsureValue(value));
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00010C20 File Offset: 0x0000EE20
		void IList.Insert(int index, object value)
		{
			this.InsertItem(index, this.EnsureValue(value));
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00010C30 File Offset: 0x0000EE30
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00010C33 File Offset: 0x0000EE33
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00010C36 File Offset: 0x0000EE36
		void IList.Remove(object value)
		{
			this.RemoveItem(this.EnsureValue(value));
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00010C46 File Offset: 0x0000EE46
		void IList.RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x170000E1 RID: 225
		object IList.this[int index]
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, this.EnsureValue(value));
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00010C68 File Offset: 0x0000EE68
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyItemsTo(array, index);
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00010C72 File Offset: 0x0000EE72
		public int Count
		{
			get
			{
				return this.ChildrenTokens.Count;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00010C7F File Offset: 0x0000EE7F
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00010C82 File Offset: 0x0000EE82
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00010CA4 File Offset: 0x0000EEA4
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00010CA8 File Offset: 0x0000EEA8
		object IBindingList.AddNew()
		{
			AddingNewEventArgs addingNewEventArgs = new AddingNewEventArgs();
			this.OnAddingNew(addingNewEventArgs);
			if (addingNewEventArgs.NewObject == null)
			{
				throw new Exception("Could not determine new value to add to '{0}'.".FormatWith(CultureInfo.InvariantCulture, new object[] { base.GetType() }));
			}
			if (!(addingNewEventArgs.NewObject is JToken))
			{
				throw new Exception("New item to be added to collection must be compatible with {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { typeof(JToken) }));
			}
			JToken jtoken = (JToken)addingNewEventArgs.NewObject;
			this.Add(jtoken);
			return jtoken;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00010D3B File Offset: 0x0000EF3B
		bool IBindingList.AllowEdit
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00010D3E File Offset: 0x0000EF3E
		bool IBindingList.AllowNew
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00010D41 File Offset: 0x0000EF41
		bool IBindingList.AllowRemove
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00010D44 File Offset: 0x0000EF44
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00010D4B File Offset: 0x0000EF4B
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00010D52 File Offset: 0x0000EF52
		bool IBindingList.IsSorted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00010D55 File Offset: 0x0000EF55
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00010D57 File Offset: 0x0000EF57
		void IBindingList.RemoveSort()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00010D5E File Offset: 0x0000EF5E
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				return ListSortDirection.Ascending;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00010D61 File Offset: 0x0000EF61
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00010D64 File Offset: 0x0000EF64
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00010D67 File Offset: 0x0000EF67
		bool IBindingList.SupportsSearching
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00010D6A File Offset: 0x0000EF6A
		bool IBindingList.SupportsSorting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400013E RID: 318
		private object _syncRoot;

		// Token: 0x0400013F RID: 319
		private bool _busy;

		// Token: 0x02000064 RID: 100
		private class JTokenReferenceEqualityComparer : IEqualityComparer<JToken>
		{
			// Token: 0x060004AC RID: 1196 RVA: 0x00010D6D File Offset: 0x0000EF6D
			public bool Equals(JToken x, JToken y)
			{
				return object.ReferenceEquals(x, y);
			}

			// Token: 0x060004AD RID: 1197 RVA: 0x00010D76 File Offset: 0x0000EF76
			public int GetHashCode(JToken obj)
			{
				if (obj == null)
				{
					return 0;
				}
				return obj.GetHashCode();
			}

			// Token: 0x04000140 RID: 320
			public static readonly JContainer.JTokenReferenceEqualityComparer Instance = new JContainer.JTokenReferenceEqualityComparer();
		}
	}
}
