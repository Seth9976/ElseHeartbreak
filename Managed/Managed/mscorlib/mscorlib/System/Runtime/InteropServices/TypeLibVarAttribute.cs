using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Contains the <see cref="T:System.Runtime.InteropServices.VARFLAGS" /> that were originally imported for this field from the COM type library.</summary>
	// Token: 0x020003CB RID: 971
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	public sealed class TypeLibVarAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.TypeLibVarAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> value for the attributed field as found in the type library it was imported from. </param>
		// Token: 0x06002B8C RID: 11148 RVA: 0x00093D1C File Offset: 0x00091F1C
		public TypeLibVarAttribute(short flags)
		{
			this.flags = (TypeLibVarFlags)flags;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.TypeLibVarAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> value for the attributed field as found in the type library it was imported from. </param>
		// Token: 0x06002B8D RID: 11149 RVA: 0x00093D2C File Offset: 0x00091F2C
		public TypeLibVarAttribute(TypeLibVarFlags flags)
		{
			this.flags = flags;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> value for this field.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> value for this field.</returns>
		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002B8E RID: 11150 RVA: 0x00093D3C File Offset: 0x00091F3C
		public TypeLibVarFlags Value
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x04001206 RID: 4614
		private TypeLibVarFlags flags;
	}
}
