using System;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000099 RID: 153
	public class ReflectionValueProvider : IValueProvider
	{
		// Token: 0x06000761 RID: 1889 RVA: 0x0001A9A6 File Offset: 0x00018BA6
		public ReflectionValueProvider(MemberInfo memberInfo)
		{
			ValidationUtils.ArgumentNotNull(memberInfo, "memberInfo");
			this._memberInfo = memberInfo;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001A9C0 File Offset: 0x00018BC0
		public void SetValue(object target, object value)
		{
			try
			{
				ReflectionUtils.SetMemberValue(this._memberInfo, target, value);
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

		// Token: 0x06000763 RID: 1891 RVA: 0x0001AA24 File Offset: 0x00018C24
		public object GetValue(object target)
		{
			object memberValue;
			try
			{
				memberValue = ReflectionUtils.GetMemberValue(this._memberInfo, target);
			}
			catch (Exception ex)
			{
				throw new JsonSerializationException("Error getting value from '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					this._memberInfo.Name,
					target.GetType()
				}), ex);
			}
			return memberValue;
		}

		// Token: 0x0400024F RID: 591
		private readonly MemberInfo _memberInfo;
	}
}
