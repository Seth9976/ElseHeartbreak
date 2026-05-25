using System;

namespace System.Reflection.Emit
{
	// Token: 0x020002CD RID: 717
	internal class ByRefType : DerivedType
	{
		// Token: 0x06002450 RID: 9296 RVA: 0x00082050 File Offset: 0x00080250
		internal ByRefType(Type elementType)
			: base(elementType)
		{
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x0008205C File Offset: 0x0008025C
		protected override bool IsByRefImpl()
		{
			return true;
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06002452 RID: 9298 RVA: 0x00082060 File Offset: 0x00080260
		public override Type BaseType
		{
			get
			{
				return typeof(Array);
			}
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x0008206C File Offset: 0x0008026C
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			return elementName + "&";
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x00082084 File Offset: 0x00080284
		public override Type MakeArrayType()
		{
			throw new ArgumentException("Cannot create an array type of a byref type");
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x00082090 File Offset: 0x00080290
		public override Type MakeArrayType(int rank)
		{
			throw new ArgumentException("Cannot create an array type of a byref type");
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x0008209C File Offset: 0x0008029C
		public override Type MakeByRefType()
		{
			throw new ArgumentException("Cannot create a byref type of an already byref type");
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000820A8 File Offset: 0x000802A8
		public override Type MakePointerType()
		{
			throw new ArgumentException("Cannot create a pointer type of a byref type");
		}
	}
}
