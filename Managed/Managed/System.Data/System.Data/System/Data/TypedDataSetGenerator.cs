using System;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace System.Data
{
	/// <summary>Used to create a strongly typed <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>3</filterpriority>
	// Token: 0x02000083 RID: 131
	[Obsolete("TypedDataSetGenerator class will be removed in a future release. Please use System.Data.Design.TypedDataSetGenerator in System.Design.dll.")]
	public class TypedDataSetGenerator
	{
		/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="dataSet">The source <see cref="T:System.Data.DataSet" /> that specifies the metadata for the typed <see cref="T:System.Data.DataSet" />. </param>
		/// <param name="codeNamespace">The namespace that provides the target namespace for the typed <see cref="T:System.Data.DataSet" />. </param>
		/// <param name="codeGen">The generator used to create the typed <see cref="T:System.Data.DataSet" />. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000663 RID: 1635 RVA: 0x0001F48C File Offset: 0x0001D68C
		public static void Generate(DataSet dataSet, CodeNamespace codeNamespace, ICodeGenerator codeGen)
		{
			CustomDataClassGenerator.CreateDataSetClasses(dataSet, codeNamespace, codeGen, null);
		}

		/// <summary>Transforms a string in a valid, typed <see cref="T:System.Data.DataSet" /> name.</summary>
		/// <returns>A string that is the converted name.</returns>
		/// <param name="name">The source name to transform into a valid, typed <see cref="T:System.Data.DataSet" /> name. </param>
		/// <param name="codeGen">The generator used to perform the conversion. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000664 RID: 1636 RVA: 0x0001F498 File Offset: 0x0001D698
		public static string GenerateIdName(string name, ICodeGenerator codeGen)
		{
			return CustomDataClassGenerator.MakeSafeName(name, codeGen);
		}
	}
}
