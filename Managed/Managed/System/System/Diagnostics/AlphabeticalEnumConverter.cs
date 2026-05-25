using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x0200020C RID: 524
	internal sealed class AlphabeticalEnumConverter : global::System.ComponentModel.EnumConverter
	{
		// Token: 0x0600118D RID: 4493 RVA: 0x0002EB9C File Offset: 0x0002CD9C
		public AlphabeticalEnumConverter(Type type)
			: base(type)
		{
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0002EBA8 File Offset: 0x0002CDA8
		[global::System.MonoTODO("Create sorted standart values")]
		public override global::System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(global::System.ComponentModel.ITypeDescriptorContext context)
		{
			return base.Values;
		}
	}
}
