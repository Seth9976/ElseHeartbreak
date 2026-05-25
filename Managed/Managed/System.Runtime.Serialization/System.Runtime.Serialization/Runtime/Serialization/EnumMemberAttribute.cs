using System;

namespace System.Runtime.Serialization
{
	/// <summary>Specifies that the field is an enumeration member and should be serialized.</summary>
	// Token: 0x02000017 RID: 23
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class EnumMemberAttribute : Attribute
	{
		/// <summary>Gets or sets the value associated with the enumeration member the attribute is applied to.</summary>
		/// <returns>The value associated with the enumeration member.</returns>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002A68 File Offset: 0x00000C68
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002A70 File Offset: 0x00000C70
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x0400003B RID: 59
		private string value;
	}
}
