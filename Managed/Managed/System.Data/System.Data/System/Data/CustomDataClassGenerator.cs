using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Text;

namespace System.Data
{
	// Token: 0x02000016 RID: 22
	internal class CustomDataClassGenerator
	{
		// Token: 0x06000074 RID: 116 RVA: 0x0000438C File Offset: 0x0000258C
		public static void CreateDataSetClasses(DataSet ds, CodeNamespace cns, ICodeGenerator gen, ClassGeneratorOptions options)
		{
			new Generator(ds, cns, gen, options).Run();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000439C File Offset: 0x0000259C
		public static void CreateDataSetClasses(DataSet ds, CodeNamespace cns, CodeDomProvider codeProvider, ClassGeneratorOptions options)
		{
			new Generator(ds, cns, codeProvider, options).Run();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000043AC File Offset: 0x000025AC
		public static void CreateDataSetClasses(DataSet ds, CodeCompileUnit cunit, CodeNamespace cns, CodeDomProvider codeProvider, ClassGeneratorOptions options)
		{
			new Generator(ds, cunit, cns, codeProvider, options).Run();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000043C0 File Offset: 0x000025C0
		public static string MakeSafeName(string name, ICodeGenerator codeGen)
		{
			if (name == null || codeGen == null)
			{
				throw new NullReferenceException();
			}
			name = codeGen.CreateValidIdentifier(name);
			return CustomDataClassGenerator.MakeSafeNameInternal(name);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000043E4 File Offset: 0x000025E4
		public static string MakeSafeName(string name, CodeDomProvider provider)
		{
			if (name == null || provider == null)
			{
				throw new NullReferenceException();
			}
			name = provider.CreateValidIdentifier(name);
			return CustomDataClassGenerator.MakeSafeNameInternal(name);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004408 File Offset: 0x00002608
		public static string MakeSafeNameInternal(string name)
		{
			if (name.Length == 0)
			{
				return "_";
			}
			StringBuilder stringBuilder = null;
			if (!char.IsLetter(name, 0) && name[0] != '_')
			{
				stringBuilder = new StringBuilder();
				stringBuilder.Append('_');
			}
			int num = 0;
			for (int i = 0; i < name.Length; i++)
			{
				if (!char.IsLetterOrDigit(name, i))
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					stringBuilder.Append(name, num, i - num);
					stringBuilder.Append('_');
					num = i + 1;
				}
			}
			if (stringBuilder != null)
			{
				stringBuilder.Append(name, num, name.Length - num);
				return stringBuilder.ToString();
			}
			return name;
		}
	}
}
