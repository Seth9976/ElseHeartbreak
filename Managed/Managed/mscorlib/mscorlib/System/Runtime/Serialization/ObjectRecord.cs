using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x02000500 RID: 1280
	internal class ObjectRecord
	{
		// Token: 0x06003321 RID: 13089 RVA: 0x000A586C File Offset: 0x000A3A6C
		public void SetMemberValue(ObjectManager manager, MemberInfo member, object value)
		{
			if (member is FieldInfo)
			{
				((FieldInfo)member).SetValue(this.ObjectInstance, value);
			}
			else
			{
				if (!(member is PropertyInfo))
				{
					throw new SerializationException("Cannot perform fixup");
				}
				((PropertyInfo)member).SetValue(this.ObjectInstance, value, null);
			}
			if (this.Member != null)
			{
				ObjectRecord objectRecord = manager.GetObjectRecord(this.IdOfContainingObj);
				if (objectRecord.IsRegistered)
				{
					objectRecord.SetMemberValue(manager, this.Member, this.ObjectInstance);
				}
			}
			else if (this.ArrayIndex != null)
			{
				ObjectRecord objectRecord2 = manager.GetObjectRecord(this.IdOfContainingObj);
				if (objectRecord2.IsRegistered)
				{
					objectRecord2.SetArrayValue(manager, this.ObjectInstance, this.ArrayIndex);
				}
			}
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x000A593C File Offset: 0x000A3B3C
		public void SetArrayValue(ObjectManager manager, object value, int[] indices)
		{
			((Array)this.ObjectInstance).SetValue(value, indices);
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x000A5950 File Offset: 0x000A3B50
		public void SetMemberValue(ObjectManager manager, string memberName, object value)
		{
			if (this.Info == null)
			{
				throw new SerializationException("Cannot perform fixup");
			}
			this.Info.AddValue(memberName, value, value.GetType());
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06003324 RID: 13092 RVA: 0x000A597C File Offset: 0x000A3B7C
		public bool IsInstanceReady
		{
			get
			{
				return this.IsRegistered && !this.IsUnsolvedObjectReference && (!this.ObjectInstance.GetType().IsValueType || (!this.HasPendingFixups && this.Info == null));
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06003325 RID: 13093 RVA: 0x000A59D4 File Offset: 0x000A3BD4
		public bool IsUnsolvedObjectReference
		{
			get
			{
				return this.Status != ObjectRecordStatus.ReferenceSolved;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06003326 RID: 13094 RVA: 0x000A59E4 File Offset: 0x000A3BE4
		public bool IsRegistered
		{
			get
			{
				return this.Status != ObjectRecordStatus.Unregistered;
			}
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x000A59F4 File Offset: 0x000A3BF4
		public bool DoFixups(bool asContainer, ObjectManager manager, bool strict)
		{
			BaseFixupRecord baseFixupRecord = null;
			BaseFixupRecord baseFixupRecord2 = ((!asContainer) ? this.FixupChainAsRequired : this.FixupChainAsContainer);
			bool flag = true;
			while (baseFixupRecord2 != null)
			{
				if (baseFixupRecord2.DoFixup(manager, strict))
				{
					this.UnchainFixup(baseFixupRecord2, baseFixupRecord, asContainer);
					if (asContainer)
					{
						baseFixupRecord2.ObjectRequired.RemoveFixup(baseFixupRecord2, false);
					}
					else
					{
						baseFixupRecord2.ObjectToBeFixed.RemoveFixup(baseFixupRecord2, true);
					}
				}
				else
				{
					baseFixupRecord = baseFixupRecord2;
					flag = false;
				}
				baseFixupRecord2 = ((!asContainer) ? baseFixupRecord2.NextSameRequired : baseFixupRecord2.NextSameContainer);
			}
			return flag;
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x000A5A88 File Offset: 0x000A3C88
		public void RemoveFixup(BaseFixupRecord fixupToRemove, bool asContainer)
		{
			BaseFixupRecord baseFixupRecord = null;
			for (BaseFixupRecord baseFixupRecord2 = ((!asContainer) ? this.FixupChainAsRequired : this.FixupChainAsContainer); baseFixupRecord2 != null; baseFixupRecord2 = ((!asContainer) ? baseFixupRecord2.NextSameRequired : baseFixupRecord2.NextSameContainer))
			{
				if (baseFixupRecord2 == fixupToRemove)
				{
					this.UnchainFixup(baseFixupRecord2, baseFixupRecord, asContainer);
					return;
				}
				baseFixupRecord = baseFixupRecord2;
			}
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x000A5AE8 File Offset: 0x000A3CE8
		private void UnchainFixup(BaseFixupRecord fixup, BaseFixupRecord prevFixup, bool asContainer)
		{
			if (prevFixup == null)
			{
				if (asContainer)
				{
					this.FixupChainAsContainer = fixup.NextSameContainer;
				}
				else
				{
					this.FixupChainAsRequired = fixup.NextSameRequired;
				}
			}
			else if (asContainer)
			{
				prevFixup.NextSameContainer = fixup.NextSameContainer;
			}
			else
			{
				prevFixup.NextSameRequired = fixup.NextSameRequired;
			}
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x000A5B48 File Offset: 0x000A3D48
		public void ChainFixup(BaseFixupRecord fixup, bool asContainer)
		{
			if (asContainer)
			{
				fixup.NextSameContainer = this.FixupChainAsContainer;
				this.FixupChainAsContainer = fixup;
			}
			else
			{
				fixup.NextSameRequired = this.FixupChainAsRequired;
				this.FixupChainAsRequired = fixup;
			}
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x000A5B7C File Offset: 0x000A3D7C
		public bool LoadData(ObjectManager manager, ISurrogateSelector selector, StreamingContext context)
		{
			if (this.Info != null)
			{
				if (this.Surrogate != null)
				{
					object obj = this.Surrogate.SetObjectData(this.ObjectInstance, this.Info, context, this.SurrogateSelector);
					if (obj != null)
					{
						this.ObjectInstance = obj;
					}
					this.Status = ObjectRecordStatus.ReferenceSolved;
				}
				else
				{
					if (!(this.ObjectInstance is ISerializable))
					{
						throw new SerializationException("No surrogate selector was found for type " + this.ObjectInstance.GetType().FullName);
					}
					object[] array = new object[] { this.Info, context };
					ConstructorInfo constructor = this.ObjectInstance.GetType().GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(SerializationInfo),
						typeof(StreamingContext)
					}, null);
					if (constructor == null)
					{
						throw new SerializationException("The constructor to deserialize an object of type " + this.ObjectInstance.GetType().FullName + " was not found.");
					}
					constructor.Invoke(this.ObjectInstance, array);
				}
				this.Info = null;
			}
			if (this.ObjectInstance is IObjectReference && this.Status != ObjectRecordStatus.ReferenceSolved)
			{
				try
				{
					this.ObjectInstance = ((IObjectReference)this.ObjectInstance).GetRealObject(context);
					int num = 100;
					while (this.ObjectInstance is IObjectReference && num > 0)
					{
						object realObject = ((IObjectReference)this.ObjectInstance).GetRealObject(context);
						if (realObject == this.ObjectInstance)
						{
							break;
						}
						this.ObjectInstance = realObject;
						num--;
					}
					if (num == 0)
					{
						throw new SerializationException("The implementation of the IObjectReference interface returns too many nested references to other objects that implement IObjectReference.");
					}
					this.Status = ObjectRecordStatus.ReferenceSolved;
				}
				catch (NullReferenceException)
				{
					return false;
				}
			}
			if (this.Member != null)
			{
				ObjectRecord objectRecord = manager.GetObjectRecord(this.IdOfContainingObj);
				objectRecord.SetMemberValue(manager, this.Member, this.ObjectInstance);
			}
			else if (this.ArrayIndex != null)
			{
				ObjectRecord objectRecord2 = manager.GetObjectRecord(this.IdOfContainingObj);
				objectRecord2.SetArrayValue(manager, this.ObjectInstance, this.ArrayIndex);
			}
			return true;
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x0600332C RID: 13100 RVA: 0x000A5DC8 File Offset: 0x000A3FC8
		public bool HasPendingFixups
		{
			get
			{
				return this.FixupChainAsContainer != null;
			}
		}

		// Token: 0x04001546 RID: 5446
		public ObjectRecordStatus Status;

		// Token: 0x04001547 RID: 5447
		public object OriginalObject;

		// Token: 0x04001548 RID: 5448
		public object ObjectInstance;

		// Token: 0x04001549 RID: 5449
		public long ObjectID;

		// Token: 0x0400154A RID: 5450
		public SerializationInfo Info;

		// Token: 0x0400154B RID: 5451
		public long IdOfContainingObj;

		// Token: 0x0400154C RID: 5452
		public ISerializationSurrogate Surrogate;

		// Token: 0x0400154D RID: 5453
		public ISurrogateSelector SurrogateSelector;

		// Token: 0x0400154E RID: 5454
		public MemberInfo Member;

		// Token: 0x0400154F RID: 5455
		public int[] ArrayIndex;

		// Token: 0x04001550 RID: 5456
		public BaseFixupRecord FixupChainAsContainer;

		// Token: 0x04001551 RID: 5457
		public BaseFixupRecord FixupChainAsRequired;

		// Token: 0x04001552 RID: 5458
		public ObjectRecord Next;
	}
}
