using System;
using System.Text;

namespace Boo.Lang
{
	// Token: 0x02000024 RID: 36
	public class QuackFuMember : IQuackFuMember
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003F9C File Offset: 0x0000219C
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00003FA4 File Offset: 0x000021A4
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("value");
				}
				this.name = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003FC4 File Offset: 0x000021C4
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00003FCC File Offset: 0x000021CC
		public QuackFuMemberKind Kind
		{
			get
			{
				return this.kind;
			}
			set
			{
				this.kind = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003FD8 File Offset: 0x000021D8
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00003FE0 File Offset: 0x000021E0
		public Type ReturnType
		{
			get
			{
				return this.returnType;
			}
			set
			{
				this.returnType = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003FEC File Offset: 0x000021EC
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00003FF4 File Offset: 0x000021F4
		public string[] ArgumentNames
		{
			get
			{
				return this.argumentNames;
			}
			set
			{
				this.argumentNames = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004000 File Offset: 0x00002200
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00004008 File Offset: 0x00002208
		public Type[] ArgumentTypes
		{
			get
			{
				return this.argumentTypes;
			}
			set
			{
				this.argumentTypes = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004014 File Offset: 0x00002214
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000401C File Offset: 0x0000221C
		public string Info
		{
			get
			{
				return this.info;
			}
			set
			{
				this.info = value;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004028 File Offset: 0x00002228
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Name);
			if (this.Kind == QuackFuMemberKind.Method)
			{
				stringBuilder.Append("(");
				if (this.ArgumentNames != null)
				{
					for (int i = 0; i < this.ArgumentNames.Length; i++)
					{
						string text = this.ArgumentNames[i];
						if (i > 0)
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append(text);
						if (this.ArgumentTypes != null && this.ArgumentTypes.Length > i)
						{
							Type type = this.ArgumentTypes[i];
							QuackFuMember.AppendTypeInformation(stringBuilder, type);
						}
					}
				}
				stringBuilder.Append(")");
			}
			QuackFuMember.AppendTypeInformation(stringBuilder, this.ReturnType);
			return stringBuilder.ToString();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000040F0 File Offset: 0x000022F0
		private static void AppendTypeInformation(StringBuilder sb, Type type)
		{
			if (type != null)
			{
				sb.Append(" as ");
				sb.Append(QuackFuMember.GetBooTypeName(type));
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004114 File Offset: 0x00002314
		private static string GetBooTypeName(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type == typeof(int))
			{
				return "int";
			}
			if (type == typeof(string))
			{
				return "string";
			}
			return type.FullName;
		}

		// Token: 0x0400001E RID: 30
		protected string name;

		// Token: 0x0400001F RID: 31
		protected QuackFuMemberKind kind;

		// Token: 0x04000020 RID: 32
		protected Type returnType;

		// Token: 0x04000021 RID: 33
		protected string[] argumentNames;

		// Token: 0x04000022 RID: 34
		protected Type[] argumentTypes;

		// Token: 0x04000023 RID: 35
		protected string info;
	}
}
