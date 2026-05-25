using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000007 RID: 7
	public class AST_VariableDeclaration : AST
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002494 File Offset: 0x00000694
		public AST_VariableDeclaration(Token token, ReturnValueType type, string name)
			: base(token)
		{
			this.m_type = type;
			this.m_name = name;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000024AC File Offset: 0x000006AC
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000024B4 File Offset: 0x000006B4
		public ReturnValueType Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000024C0 File Offset: 0x000006C0
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000024C8 File Offset: 0x000006C8
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024D4 File Offset: 0x000006D4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.ToString(),
				" ",
				this.m_name,
				" of type ",
				this.m_type
			});
		}

		// Token: 0x04000006 RID: 6
		private string m_name;

		// Token: 0x04000007 RID: 7
		private ReturnValueType m_type;
	}
}
