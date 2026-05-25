using System;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000032 RID: 50
	public class DynamicValueProvider : IValueProvider
	{
		// Token: 0x0600021C RID: 540 RVA: 0x00008968 File Offset: 0x00006B68
		public DynamicValueProvider(MemberInfo memberInfo)
		{
			ValidationUtils.ArgumentNotNull(memberInfo, "memberInfo");
			this._memberInfo = memberInfo;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008984 File Offset: 0x00006B84
		public void SetValue(object target, object value)
		{
			try
			{
				if (this._setter == null)
				{
					this._setter = DynamicReflectionDelegateFactory.Instance.CreateSet<object>(this._memberInfo);
				}
				this._setter(target, value);
			}
			catch (Exception ex)
			{
				throw new JsonSerializationException("Error setting value to '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					this._memberInfo.Name,
					target.GetType()
				}), ex);
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008A04 File Offset: 0x00006C04
		public object GetValue(object target)
		{
			object obj;
			try
			{
				if (this._getter == null)
				{
					this._getter = DynamicReflectionDelegateFactory.Instance.CreateGet<object>(this._memberInfo);
				}
				obj = this._getter(target);
			}
			catch (Exception ex)
			{
				throw new JsonSerializationException("Error getting value from '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					this._memberInfo.Name,
					target.GetType()
				}), ex);
			}
			return obj;
		}

		// Token: 0x0400009F RID: 159
		private readonly MemberInfo _memberInfo;

		// Token: 0x040000A0 RID: 160
		private Func<object, object> _getter;

		// Token: 0x040000A1 RID: 161
		private Action<object, object> _setter;
	}
}
