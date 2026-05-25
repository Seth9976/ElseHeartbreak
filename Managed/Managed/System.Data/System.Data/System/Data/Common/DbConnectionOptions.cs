using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security;
using System.Text;

namespace System.Data.Common
{
	// Token: 0x020000BB RID: 187
	internal class DbConnectionOptions
	{
		// Token: 0x060008BD RID: 2237 RVA: 0x0002BE0C File Offset: 0x0002A00C
		internal DbConnectionOptions()
		{
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0002BE14 File Offset: 0x0002A014
		protected internal DbConnectionOptions(DbConnectionOptions connectionOptions)
		{
			this.options = connectionOptions.options;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0002BE28 File Offset: 0x0002A028
		public DbConnectionOptions(string connectionString)
		{
			this.options = new NameValueCollection();
			this.ParseConnectionString(connectionString);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0002BE44 File Offset: 0x0002A044
		[MonoTODO]
		public DbConnectionOptions(string connectionString, Hashtable synonyms, bool useFirstKeyValuePair)
			: this(connectionString)
		{
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0002BE50 File Offset: 0x0002A050
		[MonoTODO]
		public bool IsEmpty
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000193 RID: 403
		public string this[string keyword]
		{
			get
			{
				return this.options[keyword];
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0002BE68 File Offset: 0x0002A068
		public ICollection Keys
		{
			get
			{
				return this.options.Keys;
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0002BE78 File Offset: 0x0002A078
		[MonoTODO]
		protected void BuildConnectionString(StringBuilder builder, string[] withoutOptions, string insertValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0002BE80 File Offset: 0x0002A080
		public bool ContainsKey(string keyword)
		{
			return this.options.Get(keyword) != null;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0002BE94 File Offset: 0x0002A094
		public bool ConvertValueToBoolean(string keyname, bool defaultvalue)
		{
			if (this.ContainsKey(keyname))
			{
				return bool.Parse(this[keyname].Trim());
			}
			return defaultvalue;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0002BEC0 File Offset: 0x0002A0C0
		public int ConvertValueToInt32(string keyname, int defaultvalue)
		{
			if (this.ContainsKey(keyname))
			{
				return int.Parse(this[keyname].Trim());
			}
			return defaultvalue;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0002BEEC File Offset: 0x0002A0EC
		[MonoTODO]
		public bool ConvertValueToIntegratedSecurity()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0002BEF4 File Offset: 0x0002A0F4
		public string ConvertValueToString(string keyname, string defaultValue)
		{
			if (this.ContainsKey(keyname))
			{
				return this[keyname];
			}
			return defaultValue;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0002BF0C File Offset: 0x0002A10C
		[MonoTODO]
		protected internal virtual PermissionSet CreatePermissionSet()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0002BF14 File Offset: 0x0002A114
		[MonoTODO]
		protected internal virtual string Expand()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0002BF1C File Offset: 0x0002A11C
		[MonoTODO]
		public static string RemoveKeyValuePairs(string connectionString, string[] keynames)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0002BF24 File Offset: 0x0002A124
		[MonoTODO]
		public string UsersConnectionString(bool hisPasswordPwd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0002BF2C File Offset: 0x0002A12C
		internal void ParseConnectionString(string connectionString)
		{
			if (connectionString.Length == 0)
			{
				return;
			}
			connectionString += ";";
			bool flag = false;
			bool flag2 = false;
			bool flag3 = true;
			string text = string.Empty;
			string text2 = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < connectionString.Length; i++)
			{
				char c = connectionString[i];
				char c2;
				if (i == connectionString.Length - 1)
				{
					c2 = '\0';
				}
				else
				{
					c2 = connectionString[i + 1];
				}
				char c3 = c;
				switch (c3)
				{
				case ' ':
					if (flag || flag2)
					{
						stringBuilder.Append(c);
					}
					else if (stringBuilder.Length > 0 && !c2.Equals(';'))
					{
						stringBuilder.Append(c);
					}
					break;
				default:
					switch (c3)
					{
					case ';':
						if (flag2 || flag)
						{
							stringBuilder.Append(c);
						}
						else
						{
							if (text != string.Empty && text != null)
							{
								text2 = stringBuilder.ToString();
								this.options[text.Trim()] = text2;
							}
							flag3 = true;
							text = string.Empty;
							text2 = string.Empty;
							stringBuilder = new StringBuilder();
						}
						break;
					default:
						if (c3 != '\'')
						{
							stringBuilder.Append(c);
						}
						else if (flag2)
						{
							stringBuilder.Append(c);
						}
						else if (c2.Equals(c))
						{
							stringBuilder.Append(c);
							i++;
						}
						else
						{
							flag = !flag;
						}
						break;
					case '=':
						if (flag2 || flag || !flag3)
						{
							stringBuilder.Append(c);
						}
						else if (c2.Equals(c))
						{
							stringBuilder.Append(c);
							i++;
						}
						else
						{
							text = stringBuilder.ToString();
							stringBuilder = new StringBuilder();
							flag3 = false;
						}
						break;
					}
					break;
				case '"':
					if (flag)
					{
						stringBuilder.Append(c);
					}
					else if (c2.Equals(c))
					{
						stringBuilder.Append(c);
						i++;
					}
					else
					{
						flag2 = !flag2;
					}
					break;
				}
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			ArrayList arrayList = new ArrayList();
			arrayList.AddRange(this.Keys);
			arrayList.Sort();
			foreach (object obj in arrayList)
			{
				string text3 = (string)obj;
				string text4 = string.Format("{0}=\"{1}\";", text3, this[text3].Replace("\"", "\"\""));
				stringBuilder2.Append(text4);
			}
			this.normalizedConnectionString = stringBuilder2.ToString();
		}

		// Token: 0x04000322 RID: 802
		internal NameValueCollection options;

		// Token: 0x04000323 RID: 803
		internal string normalizedConnectionString;
	}
}
