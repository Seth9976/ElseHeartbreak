using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates to a COM client that all classes in the current version of an assembly are compatible with classes in an earlier version of the assembly.</summary>
	// Token: 0x02000377 RID: 887
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComCompatibleVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComCompatibleVersionAttribute" /> class with the major version, minor version, build, and revision numbers of the assembly.</summary>
		/// <param name="major">The major version number of the assembly. </param>
		/// <param name="minor">The minor version number of the assembly. </param>
		/// <param name="build">The build number of the assembly. </param>
		/// <param name="revision">The revision number of the assembly. </param>
		// Token: 0x06002A22 RID: 10786 RVA: 0x00092288 File Offset: 0x00090488
		public ComCompatibleVersionAttribute(int major, int minor, int build, int revision)
		{
			this.major = major;
			this.minor = minor;
			this.build = build;
			this.revision = revision;
		}

		/// <summary>Gets the major version number of the assembly.</summary>
		/// <returns>The major version number of the assembly.</returns>
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06002A23 RID: 10787 RVA: 0x000922B0 File Offset: 0x000904B0
		public int MajorVersion
		{
			get
			{
				return this.major;
			}
		}

		/// <summary>Gets the minor version number of the assembly.</summary>
		/// <returns>The minor version number of the assembly.</returns>
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06002A24 RID: 10788 RVA: 0x000922B8 File Offset: 0x000904B8
		public int MinorVersion
		{
			get
			{
				return this.minor;
			}
		}

		/// <summary>Gets the build number of the assembly.</summary>
		/// <returns>The build number of the assembly.</returns>
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002A25 RID: 10789 RVA: 0x000922C0 File Offset: 0x000904C0
		public int BuildNumber
		{
			get
			{
				return this.build;
			}
		}

		/// <summary>Gets the revision number of the assembly.</summary>
		/// <returns>The revision number of the assembly.</returns>
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002A26 RID: 10790 RVA: 0x000922C8 File Offset: 0x000904C8
		public int RevisionNumber
		{
			get
			{
				return this.revision;
			}
		}

		// Token: 0x040010D7 RID: 4311
		private int major;

		// Token: 0x040010D8 RID: 4312
		private int minor;

		// Token: 0x040010D9 RID: 4313
		private int build;

		// Token: 0x040010DA RID: 4314
		private int revision;
	}
}
