using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl
{
	// Token: 0x020000A4 RID: 164
	internal class XsltExtensionFunction : XPFuncImpl
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x00022F28 File Offset: 0x00021128
		public XsltExtensionFunction(object extension, MethodInfo method, XPathNavigator currentNode)
		{
			this.extension = extension;
			this.method = method;
			ParameterInfo[] parameters = method.GetParameters();
			int num = parameters.Length;
			int num2 = parameters.Length;
			this.typeCodes = new TypeCode[parameters.Length];
			XPathResultType[] array = new XPathResultType[parameters.Length];
			bool flag = true;
			int num3 = parameters.Length - 1;
			while (0 <= num3)
			{
				this.typeCodes[num3] = Type.GetTypeCode(parameters[num3].ParameterType);
				array[num3] = XPFuncImpl.GetXPathType(parameters[num3].ParameterType, currentNode);
				if (flag)
				{
					if (parameters[num3].IsOptional)
					{
						num--;
					}
					else
					{
						flag = false;
					}
				}
				num3--;
			}
			base.Init(num, num2, XPFuncImpl.GetXPathType(method.ReturnType, currentNode), array);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00022FEC File Offset: 0x000211EC
		public override object Invoke(XsltCompiledContext xsltContext, object[] args, XPathNavigator docContext)
		{
			object obj2;
			try
			{
				ParameterInfo[] parameters = this.method.GetParameters();
				object[] array = new object[parameters.Length];
				int i = 0;
				string text;
				while (i < args.Length)
				{
					Type parameterType = parameters[i].ParameterType;
					text = parameterType.FullName;
					if (text == null)
					{
						goto IL_00E5;
					}
					if (XsltExtensionFunction.<>f__switch$map26 == null)
					{
						XsltExtensionFunction.<>f__switch$map26 = new Dictionary<string, int>(8)
						{
							{ "System.Int16", 0 },
							{ "System.UInt16", 0 },
							{ "System.Int32", 0 },
							{ "System.UInt32", 0 },
							{ "System.Int64", 0 },
							{ "System.UInt64", 0 },
							{ "System.Single", 0 },
							{ "System.Decimal", 0 }
						};
					}
					int num;
					if (!XsltExtensionFunction.<>f__switch$map26.TryGetValue(text, out num))
					{
						goto IL_00E5;
					}
					if (num != 0)
					{
						goto IL_00E5;
					}
					array[i] = Convert.ChangeType(args[i], parameterType);
					IL_00F0:
					i++;
					continue;
					IL_00E5:
					array[i] = args[i];
					goto IL_00F0;
				}
				text = this.method.ReturnType.FullName;
				object obj;
				if (text != null)
				{
					if (XsltExtensionFunction.<>f__switch$map27 == null)
					{
						XsltExtensionFunction.<>f__switch$map27 = new Dictionary<string, int>(8)
						{
							{ "System.Int16", 0 },
							{ "System.UInt16", 0 },
							{ "System.Int32", 0 },
							{ "System.UInt32", 0 },
							{ "System.Int64", 0 },
							{ "System.UInt64", 0 },
							{ "System.Single", 0 },
							{ "System.Decimal", 0 }
						};
					}
					int num;
					if (XsltExtensionFunction.<>f__switch$map27.TryGetValue(text, out num))
					{
						if (num == 0)
						{
							obj = Convert.ChangeType(this.method.Invoke(this.extension, array), typeof(double));
							goto IL_01FA;
						}
					}
				}
				obj = this.method.Invoke(this.extension, array);
				IL_01FA:
				IXPathNavigable ixpathNavigable = obj as IXPathNavigable;
				if (ixpathNavigable != null)
				{
					obj2 = ixpathNavigable.CreateNavigator();
				}
				else
				{
					obj2 = obj;
				}
			}
			catch (Exception ex)
			{
				throw new XsltException("Custom function reported an error.", ex);
			}
			return obj2;
		}

		// Token: 0x0400039C RID: 924
		private object extension;

		// Token: 0x0400039D RID: 925
		private MethodInfo method;

		// Token: 0x0400039E RID: 926
		private TypeCode[] typeCodes;
	}
}
