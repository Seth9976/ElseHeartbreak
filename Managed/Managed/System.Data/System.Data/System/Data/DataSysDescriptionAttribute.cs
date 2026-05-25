using System;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Marks a property, event, or extender with a description. Visual designers can display this description when referencing the member.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000033 RID: 51
	[Obsolete("DataSysDescriptionAttribute has been deprecated")]
	[AttributeUsage(AttributeTargets.All)]
	public class DataSysDescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataSysDescriptionAttribute" /> class using the specified description string.</summary>
		/// <param name="description">The description string. </param>
		// Token: 0x06000328 RID: 808 RVA: 0x000147C0 File Offset: 0x000129C0
		[Obsolete("DataSysDescriptionAttribute has been deprecated")]
		public DataSysDescriptionAttribute(string description)
			: base(description)
		{
			this.description = description;
		}

		/// <summary>Gets the text for the description.</summary>
		/// <returns>The description string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000329 RID: 809 RVA: 0x000147D0 File Offset: 0x000129D0
		public override string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x0400012C RID: 300
		private string description;
	}
}
