using System;

namespace System.Reflection.Emit
{
	// Token: 0x020002CE RID: 718
	internal class PointerType : DerivedType
	{
		// Token: 0x06002458 RID: 9304 RVA: 0x000820B4 File Offset: 0x000802B4
		internal PointerType(Type elementType)
			: base(elementType)
		{
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000820C0 File Offset: 0x000802C0
		protected override bool IsPointerImpl()
		{
			return true;
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x000820C4 File Offset: 0x000802C4
		public override Type BaseType
		{
			get
			{
				return typeof(Array);
			}
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000820D0 File Offset: 0x000802D0
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			return elementName + "*";
		}
	}
}
