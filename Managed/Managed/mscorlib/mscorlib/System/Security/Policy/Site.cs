using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Security;

namespace System.Security.Policy
{
	/// <summary>Provides the Web site from which a code assembly originates as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x02000651 RID: 1617
	[ComVisible(true)]
	[Serializable]
	public sealed class Site : IBuiltInEvidence, IIdentityPermissionFactory
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Site" /> class with the Web site from which a code assembly originates.</summary>
		/// <param name="name">The Web site of origin for the associated code assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06003D88 RID: 15752 RVA: 0x000D41FC File Offset: 0x000D23FC
		public Site(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("url");
			}
			if (!Site.IsValid(name))
			{
				throw new ArgumentException(Locale.GetText("name is not valid"));
			}
			this.origin_site = name;
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x000D4238 File Offset: 0x000D2438
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return ((!verbose) ? 1 : 3) + this.origin_site.Length;
		}

		// Token: 0x06003D8A RID: 15754 RVA: 0x000D4254 File Offset: 0x000D2454
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06003D8B RID: 15755 RVA: 0x000D4258 File Offset: 0x000D2458
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		/// <summary>Creates a new <see cref="T:System.Security.Policy.Site" /> from the specified URL.</summary>
		/// <returns>The new <see cref="T:System.Security.Policy.Site" />.</returns>
		/// <param name="url">The URL from which to create the new <see cref="T:System.Security.Policy.Site" />. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="url" /> parameter is not a valid URL. -or-The <paramref name="url" /> parameter is a file name.</exception>
		// Token: 0x06003D8C RID: 15756 RVA: 0x000D425C File Offset: 0x000D245C
		public static Site CreateFromUrl(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			if (url.Length == 0)
			{
				throw new FormatException(Locale.GetText("Empty URL."));
			}
			string text = Site.UrlToSite(url);
			if (text == null)
			{
				string text2 = string.Format(Locale.GetText("Invalid URL '{0}'."), url);
				throw new ArgumentException(text2, "url");
			}
			return new Site(text);
		}

		/// <summary>Creates an equivalent copy of the <see cref="T:System.Security.Policy.Site" />.</summary>
		/// <returns>A new, identical copy of the <see cref="T:System.Security.Policy.Site" />.</returns>
		// Token: 0x06003D8D RID: 15757 RVA: 0x000D42C8 File Offset: 0x000D24C8
		public object Copy()
		{
			return new Site(this.origin_site);
		}

		/// <summary>Creates an identity permission that corresponds to the current <see cref="T:System.Security.Policy.Site" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> for the specified <see cref="T:System.Security.Policy.Site" />.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> from which to construct the identity permission. </param>
		// Token: 0x06003D8E RID: 15758 RVA: 0x000D42D8 File Offset: 0x000D24D8
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new SiteIdentityPermission(this.origin_site);
		}

		/// <summary>Compares the current <see cref="T:System.Security.Policy.Site" /> to the specified object for equivalence.</summary>
		/// <returns>true if the two instances of the <see cref="T:System.Security.Policy.Site" /> class are equal; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Security.Policy.Site" /> to test for equivalence with the current object. </param>
		// Token: 0x06003D8F RID: 15759 RVA: 0x000D42E8 File Offset: 0x000D24E8
		public override bool Equals(object o)
		{
			Site site = o as Site;
			return site != null && string.Compare(site.Name, this.origin_site, true, CultureInfo.InvariantCulture) == 0;
		}

		/// <summary>Returns the hash code of the current Web site name.</summary>
		/// <returns>The hash code of the current Web site name.</returns>
		// Token: 0x06003D90 RID: 15760 RVA: 0x000D4320 File Offset: 0x000D2520
		public override int GetHashCode()
		{
			return this.origin_site.GetHashCode();
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Site" />.</summary>
		/// <returns>A representation the current <see cref="T:System.Security.Policy.Site" />.</returns>
		// Token: 0x06003D91 RID: 15761 RVA: 0x000D4330 File Offset: 0x000D2530
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Site");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Name", this.origin_site));
			return securityElement.ToString();
		}

		/// <summary>Gets the site from which the code assembly originates.</summary>
		/// <returns>The name of the site from which the code assembly originates.</returns>
		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06003D92 RID: 15762 RVA: 0x000D4374 File Offset: 0x000D2574
		public string Name
		{
			get
			{
				return this.origin_site;
			}
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x000D437C File Offset: 0x000D257C
		internal static bool IsValid(string name)
		{
			if (name == string.Empty)
			{
				return false;
			}
			if (name.Length == 1 && name == ".")
			{
				return false;
			}
			string[] array = name.Split(new char[] { '.' });
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (i != 0 || !(text == "*"))
				{
					foreach (char c in text)
					{
						int num = Convert.ToInt32(c);
						if (num != 33 && num != 45 && (num < 35 || num > 41) && (num < 48 || num > 57) && (num < 64 || num > 90) && (num < 94 || num > 95) && (num < 97 || num > 123) && (num < 125 || num > 126))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x000D44C0 File Offset: 0x000D26C0
		internal static string UrlToSite(string url)
		{
			if (url == null)
			{
				return null;
			}
			Uri uri = new Uri(url);
			if (uri.Scheme == Uri.UriSchemeFile)
			{
				return null;
			}
			string host = uri.Host;
			return (!Site.IsValid(host)) ? null : host;
		}

		// Token: 0x04001A94 RID: 6804
		internal string origin_site;
	}
}
