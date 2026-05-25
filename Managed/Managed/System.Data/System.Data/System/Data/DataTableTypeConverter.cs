using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000037 RID: 55
	internal class DataTableTypeConverter : ReferenceConverter
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x00019DD4 File Offset: 0x00017FD4
		public DataTableTypeConverter()
			: base(typeof(DataTable))
		{
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00019DE8 File Offset: 0x00017FE8
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}
