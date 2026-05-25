using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates when a dependency is to be loaded by the referring assembly. This class cannot be inherited. </summary>
	// Token: 0x0200032C RID: 812
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Serializable]
	public sealed class DependencyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.DependencyAttribute" /> class with the specified <see cref="T:System.Runtime.CompilerServices.LoadHint" /> value. </summary>
		/// <param name="dependentAssemblyArgument">The dependent assembly to bind to.</param>
		/// <param name="loadHintArgument">One of the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> values.</param>
		// Token: 0x060028A4 RID: 10404 RVA: 0x00091E28 File Offset: 0x00090028
		public DependencyAttribute(string dependentAssemblyArgument, LoadHint loadHintArgument)
		{
			this.dependentAssembly = dependentAssemblyArgument;
			this.hint = loadHintArgument;
		}

		/// <summary>Gets the value of the dependent assembly. </summary>
		/// <returns>The name of the dependent assembly.</returns>
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x060028A5 RID: 10405 RVA: 0x00091E40 File Offset: 0x00090040
		public string DependentAssembly
		{
			get
			{
				return this.dependentAssembly;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> value that indicates when an assembly is to load a dependency. </summary>
		/// <returns>One of the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> values.</returns>
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060028A6 RID: 10406 RVA: 0x00091E48 File Offset: 0x00090048
		public LoadHint LoadHint
		{
			get
			{
				return this.hint;
			}
		}

		// Token: 0x04001089 RID: 4233
		private string dependentAssembly;

		// Token: 0x0400108A RID: 4234
		private LoadHint hint;
	}
}
