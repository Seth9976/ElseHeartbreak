using System;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x020002CC RID: 716
	internal class ArrayType : DerivedType
	{
		// Token: 0x0600244A RID: 9290 RVA: 0x00081F48 File Offset: 0x00080148
		internal ArrayType(Type elementType, int rank)
			: base(elementType)
		{
			this.rank = rank;
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x00081F58 File Offset: 0x00080158
		protected override bool IsArrayImpl()
		{
			return true;
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x00081F5C File Offset: 0x0008015C
		public override int GetArrayRank()
		{
			return (this.rank != 0) ? this.rank : 1;
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x0600244D RID: 9293 RVA: 0x00081F78 File Offset: 0x00080178
		public override Type BaseType
		{
			get
			{
				return typeof(Array);
			}
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x00081F84 File Offset: 0x00080184
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			if (((ModuleBuilder)this.elementType.Module).assemblyb.IsCompilerContext)
			{
				return (this.elementType.Attributes & TypeAttributes.VisibilityMask) | TypeAttributes.Sealed | TypeAttributes.Serializable;
			}
			return this.elementType.Attributes;
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x00081FD8 File Offset: 0x000801D8
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(elementName);
			stringBuilder.Append("[");
			for (int i = 1; i < this.rank; i++)
			{
				stringBuilder.Append(",");
			}
			if (this.rank == 1)
			{
				stringBuilder.Append("*");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000DBC RID: 3516
		private int rank;
	}
}
