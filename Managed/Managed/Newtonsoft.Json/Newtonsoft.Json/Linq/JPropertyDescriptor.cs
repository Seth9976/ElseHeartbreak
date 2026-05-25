using System;
using System.ComponentModel;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000024 RID: 36
	public class JPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06000134 RID: 308 RVA: 0x000061E8 File Offset: 0x000043E8
		public JPropertyDescriptor(string name, Type propertyType)
			: base(name, null)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			ValidationUtils.ArgumentNotNull(propertyType, "propertyType");
			this._propertyType = propertyType;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000620F File Offset: 0x0000440F
		private static JObject CastInstance(object instance)
		{
			return (JObject)instance;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006217 File Offset: 0x00004417
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000621C File Offset: 0x0000441C
		public override object GetValue(object component)
		{
			return JPropertyDescriptor.CastInstance(component)[this.Name];
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000623C File Offset: 0x0000443C
		public override void ResetValue(object component)
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006240 File Offset: 0x00004440
		public override void SetValue(object component, object value)
		{
			JToken jtoken = ((value is JToken) ? ((JToken)value) : new JValue(value));
			JPropertyDescriptor.CastInstance(component)[this.Name] = jtoken;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006276 File Offset: 0x00004476
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00006279 File Offset: 0x00004479
		public override Type ComponentType
		{
			get
			{
				return typeof(JObject);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00006285 File Offset: 0x00004485
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00006288 File Offset: 0x00004488
		public override Type PropertyType
		{
			get
			{
				return this._propertyType;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006290 File Offset: 0x00004490
		protected override int NameHashCode
		{
			get
			{
				return base.NameHashCode;
			}
		}

		// Token: 0x04000081 RID: 129
		private readonly Type _propertyType;
	}
}
