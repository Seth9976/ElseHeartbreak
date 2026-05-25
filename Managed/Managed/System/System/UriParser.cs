using System;
using System.Collections;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
	/// <summary>Parses a new URI scheme. This is an abstract class.</summary>
	// Token: 0x020004B9 RID: 1209
	public abstract class UriParser
	{
		// Token: 0x06002B93 RID: 11155 RVA: 0x00097E50 File Offset: 0x00096050
		private static global::System.Text.RegularExpressions.Match ParseAuthority(global::System.Text.RegularExpressions.Group g)
		{
			return global::System.UriParser.auth_regex.Match(g.Value);
		}

		/// <summary>Gets the components from a URI.</summary>
		/// <returns>A string that contains the components.</returns>
		/// <param name="uri">The URI to parse.</param>
		/// <param name="components">The <see cref="T:System.UriComponents" /> to retrieve from <paramref name="uri" />.</param>
		/// <param name="format">One of the <see cref="T:System.UriFormat" /> values that controls how special characters are escaped.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="uriFormat" /> is invalid.- or -<paramref name="uriComponents" /> is not a combination of valid <see cref="T:System.UriComponents" /> values. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="uri" /> requires user-driven parsing- or -<paramref name="uri" /> is not an absolute URI. Relative URIs cannot be used with this method.</exception>
		// Token: 0x06002B94 RID: 11156 RVA: 0x00097E64 File Offset: 0x00096064
		protected internal virtual string GetComponents(global::System.Uri uri, global::System.UriComponents components, global::System.UriFormat format)
		{
			if (format < global::System.UriFormat.UriEscaped || format > global::System.UriFormat.SafeUnescaped)
			{
				throw new ArgumentOutOfRangeException("format");
			}
			global::System.Text.RegularExpressions.Match match = global::System.UriParser.uri_regex.Match(uri.OriginalString);
			string value = this.scheme_name;
			int defaultPort = this.default_port;
			if (value == null || value == "*")
			{
				value = match.Groups[2].Value;
				defaultPort = global::System.Uri.GetDefaultPort(value);
			}
			else if (string.Compare(value, match.Groups[2].Value, true) != 0)
			{
				throw new SystemException("URI Parser: scheme mismatch: " + value + " vs. " + match.Groups[2].Value);
			}
			global::System.UriComponents uriComponents = components;
			switch (uriComponents)
			{
			case global::System.UriComponents.Scheme:
				return value;
			case global::System.UriComponents.UserInfo:
				return global::System.UriParser.ParseAuthority(match.Groups[4]).Groups[2].Value;
			default:
			{
				if (uriComponents == global::System.UriComponents.Path)
				{
					return this.Format(this.IgnoreFirstCharIf(match.Groups[5].Value, '/'), format);
				}
				if (uriComponents == global::System.UriComponents.Query)
				{
					return this.Format(match.Groups[7].Value, format);
				}
				if (uriComponents == global::System.UriComponents.Fragment)
				{
					return this.Format(match.Groups[9].Value, format);
				}
				if (uriComponents != global::System.UriComponents.StrongPort)
				{
					if (uriComponents == global::System.UriComponents.SerializationInfoString)
					{
						components = global::System.UriComponents.AbsoluteUri;
					}
					global::System.Text.RegularExpressions.Match match2 = global::System.UriParser.ParseAuthority(match.Groups[4]);
					StringBuilder stringBuilder = new StringBuilder();
					if ((components & global::System.UriComponents.Scheme) != (global::System.UriComponents)0)
					{
						stringBuilder.Append(value);
						stringBuilder.Append(global::System.Uri.GetSchemeDelimiter(value));
					}
					if ((components & global::System.UriComponents.UserInfo) != (global::System.UriComponents)0)
					{
						stringBuilder.Append(match2.Groups[1].Value);
					}
					if ((components & global::System.UriComponents.Host) != (global::System.UriComponents)0)
					{
						stringBuilder.Append(match2.Groups[3].Value);
					}
					if ((components & global::System.UriComponents.StrongPort) != (global::System.UriComponents)0)
					{
						global::System.Text.RegularExpressions.Group group = match2.Groups[4];
						stringBuilder.Append((!group.Success) ? (":" + defaultPort) : group.Value);
					}
					if ((components & global::System.UriComponents.Port) != (global::System.UriComponents)0)
					{
						string value2 = match2.Groups[5].Value;
						if (value2 != null && value2 != string.Empty && value2 != defaultPort.ToString())
						{
							stringBuilder.Append(match2.Groups[4].Value);
						}
					}
					if ((components & global::System.UriComponents.Path) != (global::System.UriComponents)0)
					{
						stringBuilder.Append(match.Groups[5]);
					}
					if ((components & global::System.UriComponents.Query) != (global::System.UriComponents)0)
					{
						stringBuilder.Append(match.Groups[6]);
					}
					if ((components & global::System.UriComponents.Fragment) != (global::System.UriComponents)0)
					{
						stringBuilder.Append(match.Groups[8]);
					}
					return this.Format(stringBuilder.ToString(), format);
				}
				global::System.Text.RegularExpressions.Group group2 = global::System.UriParser.ParseAuthority(match.Groups[4]).Groups[5];
				return (!group2.Success) ? defaultPort.ToString() : group2.Value;
			}
			case global::System.UriComponents.Host:
				return global::System.UriParser.ParseAuthority(match.Groups[4]).Groups[3].Value;
			case global::System.UriComponents.Port:
			{
				string value3 = global::System.UriParser.ParseAuthority(match.Groups[4]).Groups[5].Value;
				if (value3 != null && value3 != string.Empty && value3 != defaultPort.ToString())
				{
					return value3;
				}
				return string.Empty;
			}
			}
		}

		/// <summary>Initialize the state of the parser and validate the URI.</summary>
		/// <param name="uri">The T:System.Uri to validate.</param>
		/// <param name="parsingError">Validation errors, if any.</param>
		// Token: 0x06002B95 RID: 11157 RVA: 0x00098248 File Offset: 0x00096448
		protected internal virtual void InitializeAndValidate(global::System.Uri uri, out global::System.UriFormatException parsingError)
		{
			if (uri.Scheme != this.scheme_name && this.scheme_name != "*")
			{
				parsingError = new global::System.UriFormatException("The argument Uri's scheme does not match");
			}
			else
			{
				parsingError = null;
			}
		}

		/// <summary>Determines whether <paramref name="baseUri" /> is a base URI for <paramref name="relativeUri" />.</summary>
		/// <returns>true if <paramref name="baseUri" /> is a base URI for <paramref name="relativeUri" />; otherwise, false.</returns>
		/// <param name="baseUri">The base URI.</param>
		/// <param name="relativeUri">The URI to test.</param>
		// Token: 0x06002B96 RID: 11158 RVA: 0x00098294 File Offset: 0x00096494
		protected internal virtual bool IsBaseOf(global::System.Uri baseUri, global::System.Uri relativeUri)
		{
			if (global::System.Uri.Compare(baseUri, relativeUri, global::System.UriComponents.Scheme | global::System.UriComponents.UserInfo | global::System.UriComponents.Host | global::System.UriComponents.Port, global::System.UriFormat.Unescaped, StringComparison.InvariantCultureIgnoreCase) != 0)
			{
				return false;
			}
			string localPath = baseUri.LocalPath;
			int num = localPath.LastIndexOf('/') + 1;
			return string.Compare(localPath, 0, relativeUri.LocalPath, 0, num, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		/// <summary>Indicates whether a URI is well-formed.</summary>
		/// <returns>true if <paramref name="uri" /> is well-formed; otherwise, false.</returns>
		/// <param name="uri">The URI to check.</param>
		// Token: 0x06002B97 RID: 11159 RVA: 0x000982D8 File Offset: 0x000964D8
		protected internal virtual bool IsWellFormedOriginalString(global::System.Uri uri)
		{
			return uri.IsWellFormedOriginalString();
		}

		/// <summary>Invoked by a <see cref="T:System.Uri" /> constructor to get a <see cref="T:System.UriParser" /> instance</summary>
		/// <returns>A <see cref="T:System.UriParser" /> for the constructed <see cref="T:System.Uri" />.</returns>
		// Token: 0x06002B98 RID: 11160 RVA: 0x000982E0 File Offset: 0x000964E0
		protected internal virtual global::System.UriParser OnNewUri()
		{
			return this;
		}

		/// <summary>Invoked by the Framework when a <see cref="T:System.UriParser" /> method is registered.</summary>
		/// <param name="schemeName">The scheme that is associated with this <see cref="T:System.UriParser" />.</param>
		/// <param name="defaultPort">The port number of the scheme.</param>
		// Token: 0x06002B99 RID: 11161 RVA: 0x000982E4 File Offset: 0x000964E4
		[global::System.MonoTODO]
		protected virtual void OnRegister(string schemeName, int defaultPort)
		{
		}

		/// <summary>Called by <see cref="T:System.Uri" /> constructors and <see cref="Overload:System.Uri.TryCreate" /> to resolve a relative URI.</summary>
		/// <returns>The string of the resolved relative <see cref="T:System.Uri" />.</returns>
		/// <param name="baseUri">A base URI.</param>
		/// <param name="relativeUri">A relative URI.</param>
		/// <param name="parsingError">Errors during the resolve process, if any.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="baseUri" /> parameter is not an absolute <see cref="T:System.Uri" />- or -<paramref name="baseUri" /> parameter requires user-driven parsing.</exception>
		// Token: 0x06002B9A RID: 11162 RVA: 0x000982E8 File Offset: 0x000964E8
		[global::System.MonoTODO]
		protected internal virtual string Resolve(global::System.Uri baseUri, global::System.Uri relativeUri, out global::System.UriFormatException parsingError)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06002B9B RID: 11163 RVA: 0x000982F0 File Offset: 0x000964F0
		// (set) Token: 0x06002B9C RID: 11164 RVA: 0x000982F8 File Offset: 0x000964F8
		internal string SchemeName
		{
			get
			{
				return this.scheme_name;
			}
			set
			{
				this.scheme_name = value;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06002B9D RID: 11165 RVA: 0x00098304 File Offset: 0x00096504
		// (set) Token: 0x06002B9E RID: 11166 RVA: 0x0009830C File Offset: 0x0009650C
		internal int DefaultPort
		{
			get
			{
				return this.default_port;
			}
			set
			{
				this.default_port = value;
			}
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x00098318 File Offset: 0x00096518
		private string IgnoreFirstCharIf(string s, char c)
		{
			if (s.Length == 0)
			{
				return string.Empty;
			}
			if (s[0] == c)
			{
				return s.Substring(1);
			}
			return s;
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x0009834C File Offset: 0x0009654C
		private string Format(string s, global::System.UriFormat format)
		{
			if (s.Length == 0)
			{
				return string.Empty;
			}
			switch (format)
			{
			case global::System.UriFormat.UriEscaped:
				return global::System.Uri.EscapeString(s, false, true, true);
			case global::System.UriFormat.Unescaped:
				return global::System.Uri.Unescape(s, false);
			case global::System.UriFormat.SafeUnescaped:
				s = global::System.Uri.Unescape(s, false);
				return s;
			default:
				throw new ArgumentOutOfRangeException("format");
			}
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x000983AC File Offset: 0x000965AC
		private static void CreateDefaults()
		{
			if (global::System.UriParser.table != null)
			{
				return;
			}
			Hashtable hashtable = new Hashtable();
			global::System.UriParser.InternalRegister(hashtable, new global::System.DefaultUriParser(), global::System.Uri.UriSchemeFile, -1);
			global::System.UriParser.InternalRegister(hashtable, new global::System.DefaultUriParser(), global::System.Uri.UriSchemeFtp, 21);
			global::System.UriParser.InternalRegister(hashtable, new global::System.DefaultUriParser(), global::System.Uri.UriSchemeGopher, 70);
			global::System.UriParser.InternalRegister(hashtable, new global::System.DefaultUriParser(), global::System.Uri.UriSchemeHttp, 80);
			global::System.UriParser.InternalRegister(hashtable, new global::System.DefaultUriParser(), global::System.Uri.UriSchemeHttps, 443);
			global::System.UriParser.InternalRegister(hashtable, new global::System.DefaultUriParser(), global::System.Uri.UriSchemeMailto, 25);
			global::System.UriParser.InternalRegister(hashtable, new global::System.DefaultUriParser(), global::System.Uri.UriSchemeNetPipe, -1);
			global::System.UriParser.InternalRegister(hashtable, new global::System.DefaultUriParser(), global::System.Uri.UriSchemeNetTcp, -1);
			global::System.UriParser.InternalRegister(hashtable, new global::System.DefaultUriParser(), global::System.Uri.UriSchemeNews, 119);
			global::System.UriParser.InternalRegister(hashtable, new global::System.DefaultUriParser(), global::System.Uri.UriSchemeNntp, 119);
			global::System.UriParser.InternalRegister(hashtable, new global::System.DefaultUriParser(), "ldap", 389);
			object obj = global::System.UriParser.lock_object;
			lock (obj)
			{
				if (global::System.UriParser.table == null)
				{
					global::System.UriParser.table = hashtable;
				}
			}
		}

		/// <summary>Indicates whether the parser for a scheme is registered.</summary>
		/// <returns>true if <paramref name="schemeName" /> has been registered; otherwise, false.</returns>
		/// <param name="schemeName">The scheme name to check.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemeName" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="schemeName" /> parameter is not valid. </exception>
		// Token: 0x06002BA2 RID: 11170 RVA: 0x000984E0 File Offset: 0x000966E0
		public static bool IsKnownScheme(string schemeName)
		{
			if (schemeName == null)
			{
				throw new ArgumentNullException("schemeName");
			}
			if (schemeName.Length == 0)
			{
				throw new ArgumentOutOfRangeException("schemeName");
			}
			global::System.UriParser.CreateDefaults();
			string text = schemeName.ToLower(CultureInfo.InvariantCulture);
			return global::System.UriParser.table[text] != null;
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x00098538 File Offset: 0x00096738
		private static void InternalRegister(Hashtable table, global::System.UriParser uriParser, string schemeName, int defaultPort)
		{
			uriParser.SchemeName = schemeName;
			uriParser.DefaultPort = defaultPort;
			if (uriParser is global::System.GenericUriParser)
			{
				table.Add(schemeName, uriParser);
			}
			else
			{
				table.Add(schemeName, new global::System.DefaultUriParser
				{
					SchemeName = schemeName,
					DefaultPort = defaultPort
				});
			}
			uriParser.OnRegister(schemeName, defaultPort);
		}

		/// <summary>Associates a scheme and port number with a <see cref="T:System.UriParser" />.</summary>
		/// <param name="uriParser">The URI parser to register.</param>
		/// <param name="schemeName">The name of the scheme that is associated with this parser.</param>
		/// <param name="defaultPort">The default port number for the specified scheme.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriParser" /> parameter is null- or -<paramref name="schemeName" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="schemeName" /> parameter is not valid- or -<paramref name="defaultPort" /> parameter is not valid. The <paramref name="defaultPort" /> parameter must be not be less than zero or greater than 65,534.</exception>
		// Token: 0x06002BA4 RID: 11172 RVA: 0x00098590 File Offset: 0x00096790
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"Infrastructure\"/>\n</PermissionSet>\n")]
		public static void Register(global::System.UriParser uriParser, string schemeName, int defaultPort)
		{
			if (uriParser == null)
			{
				throw new ArgumentNullException("uriParser");
			}
			if (schemeName == null)
			{
				throw new ArgumentNullException("schemeName");
			}
			if (defaultPort < -1 || defaultPort >= 65535)
			{
				throw new ArgumentOutOfRangeException("defaultPort");
			}
			global::System.UriParser.CreateDefaults();
			string text = schemeName.ToLower(CultureInfo.InvariantCulture);
			if (global::System.UriParser.table[text] != null)
			{
				string text2 = global::Locale.GetText("Scheme '{0}' is already registred.");
				throw new InvalidOperationException(text2);
			}
			global::System.UriParser.InternalRegister(global::System.UriParser.table, uriParser, text, defaultPort);
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x0009861C File Offset: 0x0009681C
		internal static global::System.UriParser GetParser(string schemeName)
		{
			if (schemeName == null)
			{
				return null;
			}
			global::System.UriParser.CreateDefaults();
			string text = schemeName.ToLower(CultureInfo.InvariantCulture);
			return (global::System.UriParser)global::System.UriParser.table[text];
		}

		// Token: 0x04001B7B RID: 7035
		private static object lock_object = new object();

		// Token: 0x04001B7C RID: 7036
		private static Hashtable table;

		// Token: 0x04001B7D RID: 7037
		internal string scheme_name;

		// Token: 0x04001B7E RID: 7038
		private int default_port;

		// Token: 0x04001B7F RID: 7039
		private static readonly global::System.Text.RegularExpressions.Regex uri_regex = new global::System.Text.RegularExpressions.Regex("^(([^:/?#]+):)?(//([^/?#]*))?([^?#]*)(\\?([^#]*))?(#(.*))?", global::System.Text.RegularExpressions.RegexOptions.Compiled);

		// Token: 0x04001B80 RID: 7040
		private static readonly global::System.Text.RegularExpressions.Regex auth_regex = new global::System.Text.RegularExpressions.Regex("^(([^@]+)@)?(.*?)(:([0-9]+))?$");
	}
}
