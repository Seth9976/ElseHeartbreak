using System;

namespace System.Runtime.Versioning
{
	/// <summary>Provides methods to aid developers in writing version-safe code. This class cannot be inherited.</summary>
	// Token: 0x02000530 RID: 1328
	public static class VersioningHelper
	{
		// Token: 0x06003454 RID: 13396 RVA: 0x000ABDA0 File Offset: 0x000A9FA0
		private static int GetDomainId()
		{
			return AppDomain.CurrentDomain.Id;
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000ABDAC File Offset: 0x000A9FAC
		private static int GetProcessId()
		{
			return 0;
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x000ABDB0 File Offset: 0x000A9FB0
		private static string SafeName(string name, bool process, bool appdomain)
		{
			if (process && appdomain)
			{
				return string.Concat(new string[]
				{
					name,
					"_",
					VersioningHelper.GetProcessId().ToString(),
					"_",
					VersioningHelper.GetDomainId().ToString()
				});
			}
			if (process)
			{
				return name + "_" + VersioningHelper.GetProcessId().ToString();
			}
			if (appdomain)
			{
				return name + "_" + VersioningHelper.GetDomainId().ToString();
			}
			return name;
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x000ABE48 File Offset: 0x000AA048
		private static string ConvertFromMachine(string name, ResourceScope to, Type type)
		{
			switch (to)
			{
			case ResourceScope.Machine:
				return VersioningHelper.SafeName(name, false, false);
			case ResourceScope.Process:
				return VersioningHelper.SafeName(name, true, false);
			case ResourceScope.AppDomain:
				return VersioningHelper.SafeName(name, true, true);
			}
			throw new ArgumentException("to");
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x000ABE9C File Offset: 0x000AA09C
		private static string ConvertFromProcess(string name, ResourceScope to, Type type)
		{
			if (to < ResourceScope.Process || to >= ResourceScope.Private)
			{
				throw new ArgumentException("to");
			}
			bool flag = (to & ResourceScope.AppDomain) == ResourceScope.AppDomain;
			return VersioningHelper.SafeName(name, false, flag);
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x000ABED4 File Offset: 0x000AA0D4
		private static string ConvertFromAppDomain(string name, ResourceScope to, Type type)
		{
			if (to < ResourceScope.AppDomain || to >= ResourceScope.Private)
			{
				throw new ArgumentException("to");
			}
			return VersioningHelper.SafeName(name, false, false);
		}

		/// <summary>Returns a version-safe name based on the specified resource name and the intended resource consumption source.</summary>
		/// <returns>A version-safe name.</returns>
		/// <param name="name">The name of the resource.</param>
		/// <param name="from">The scope of the resource.</param>
		/// <param name="to">The desired resource consumption scope.</param>
		// Token: 0x0600345A RID: 13402 RVA: 0x000ABF04 File Offset: 0x000AA104
		[MonoTODO("process id is always 0")]
		public static string MakeVersionSafeName(string name, ResourceScope from, ResourceScope to)
		{
			return VersioningHelper.MakeVersionSafeName(name, from, to, null);
		}

		/// <summary>Returns a version-safe name based on the specified resource name, the intended resource consumption scope, and the type using the resource.</summary>
		/// <returns>A version-safe name.</returns>
		/// <param name="name">The name of the resource.</param>
		/// <param name="from">The beginning of the scope range.</param>
		/// <param name="to">The end of the scope range.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the resource.</param>
		/// <exception cref="T:System.ArgumentException">The values for <paramref name="from " />and <paramref name="to " />are invalid. The resource type in the <see cref="T:System.Runtime.Versioning.ResourceScope" />  enumeration is going from a more restrictive resource type to a more general resource type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type " />is null.</exception>
		// Token: 0x0600345B RID: 13403 RVA: 0x000ABF10 File Offset: 0x000AA110
		[MonoTODO("type?")]
		public static string MakeVersionSafeName(string name, ResourceScope from, ResourceScope to, Type type)
		{
			if ((from & ResourceScope.Private) != ResourceScope.None)
			{
				to &= ~(ResourceScope.Private | ResourceScope.Assembly);
			}
			else if ((from & ResourceScope.Assembly) != ResourceScope.None)
			{
				to &= ~ResourceScope.Assembly;
			}
			string text = ((name != null) ? name : string.Empty);
			switch (from)
			{
			case ResourceScope.Machine:
				break;
			case ResourceScope.Process:
				goto IL_008F;
			default:
				switch (from)
				{
				case ResourceScope.Machine | ResourceScope.Private:
					break;
				case ResourceScope.Process | ResourceScope.Private:
					goto IL_008F;
				default:
					switch (from)
					{
					case ResourceScope.Machine | ResourceScope.Assembly:
						goto IL_0086;
					case ResourceScope.Process | ResourceScope.Assembly:
						goto IL_008F;
					case ResourceScope.AppDomain | ResourceScope.Assembly:
						goto IL_0098;
					}
					throw new ArgumentException("from");
				case ResourceScope.AppDomain | ResourceScope.Private:
					goto IL_0098;
				}
				break;
			case ResourceScope.AppDomain:
				goto IL_0098;
			}
			IL_0086:
			return VersioningHelper.ConvertFromMachine(text, to, type);
			IL_008F:
			return VersioningHelper.ConvertFromProcess(text, to, type);
			IL_0098:
			return VersioningHelper.ConvertFromAppDomain(text, to, type);
		}
	}
}
