using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000068 RID: 104
	public sealed class WWW : IDisposable
	{
		// Token: 0x06000248 RID: 584 RVA: 0x0000A064 File Offset: 0x00008264
		public WWW(string url)
		{
			this.InitWWW(url, null, null);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000A078 File Offset: 0x00008278
		public WWW(string url, WWWForm form)
		{
			string[] array = WWW.FlattenedHeadersFrom(form.headers);
			this.InitWWW(url, form.data, array);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000A0A8 File Offset: 0x000082A8
		public WWW(string url, byte[] postData)
		{
			this.InitWWW(url, postData, null);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000A0BC File Offset: 0x000082BC
		[Obsolete("This overload is deprecated. Use the one with Dictionary argument.")]
		public WWW(string url, byte[] postData, Hashtable headers)
		{
			string[] array = WWW.FlattenedHeadersFrom(headers);
			this.InitWWW(url, postData, array);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000A0E0 File Offset: 0x000082E0
		public WWW(string url, byte[] postData, Dictionary<string, string> headers)
		{
			string[] array = WWW.FlattenedHeadersFrom(headers);
			this.InitWWW(url, postData, array);
		}

		// Token: 0x0600024D RID: 589
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern WWW(string url, int version, uint crc);

		// Token: 0x0600024E RID: 590 RVA: 0x0000A104 File Offset: 0x00008304
		private static string[] FlattenedHeadersFrom(Hashtable headers)
		{
			if (headers == null)
			{
				return null;
			}
			string[] array = new string[headers.Count * 2];
			int num = 0;
			foreach (object obj in headers)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				array[num++] = dictionaryEntry.Key.ToString();
				array[num++] = dictionaryEntry.Value.ToString();
			}
			return array;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000A1AC File Offset: 0x000083AC
		private static string[] FlattenedHeadersFrom(Dictionary<string, string> headers)
		{
			if (headers == null)
			{
				return null;
			}
			string[] array = new string[headers.Count * 2];
			int num = 0;
			foreach (KeyValuePair<string, string> keyValuePair in headers)
			{
				array[num++] = keyValuePair.Key.ToString();
				array[num++] = keyValuePair.Value.ToString();
			}
			return array;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000A248 File Offset: 0x00008448
		internal static Dictionary<string, string> ParseHTTPHeaderString(string input)
		{
			if (input == null)
			{
				throw new ArgumentException("input was null to ParseHTTPHeaderString");
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			StringReader stringReader = new StringReader(input);
			int num = 0;
			for (;;)
			{
				string text = stringReader.ReadLine();
				if (text == null)
				{
					break;
				}
				if (num++ == 0 && text.StartsWith("HTTP"))
				{
					dictionary["STATUS"] = text;
				}
				else
				{
					int num2 = text.IndexOf(": ");
					if (num2 != -1)
					{
						string text2 = text.Substring(0, num2).ToUpper();
						string text3 = text.Substring(num2 + 2);
						dictionary[text2] = text3;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000A2F8 File Offset: 0x000084F8
		public void Dispose()
		{
			this.DestroyWWW(true);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000A304 File Offset: 0x00008504
		~WWW()
		{
			this.DestroyWWW(false);
		}

		// Token: 0x06000253 RID: 595
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void DestroyWWW(bool cancel);

		// Token: 0x06000254 RID: 596
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitWWW(string url, byte[] postData, string[] iHeaders);

		// Token: 0x06000255 RID: 597
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool enforceWebSecurityRestrictions();

		// Token: 0x06000256 RID: 598 RVA: 0x0000A340 File Offset: 0x00008540
		[ExcludeFromDocs]
		public static string EscapeURL(string s)
		{
			Encoding utf = Encoding.UTF8;
			return WWW.EscapeURL(s, utf);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000A35C File Offset: 0x0000855C
		public static string EscapeURL(string s, [DefaultValue("System.Text.Encoding.UTF8")] Encoding e)
		{
			if (s == null)
			{
				return null;
			}
			if (s == string.Empty)
			{
				return string.Empty;
			}
			if (e == null)
			{
				return null;
			}
			return WWWTranscoder.URLEncode(s, e);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000A38C File Offset: 0x0000858C
		[ExcludeFromDocs]
		public static string UnEscapeURL(string s)
		{
			Encoding utf = Encoding.UTF8;
			return WWW.UnEscapeURL(s, utf);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000A3A8 File Offset: 0x000085A8
		public static string UnEscapeURL(string s, [DefaultValue("System.Text.Encoding.UTF8")] Encoding e)
		{
			if (s == null)
			{
				return null;
			}
			if (s.IndexOf('%') == -1 && s.IndexOf('+') == -1)
			{
				return s;
			}
			return WWWTranscoder.URLDecode(s, e);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000A3E4 File Offset: 0x000085E4
		public Dictionary<string, string> responseHeaders
		{
			get
			{
				if (!this.isDone)
				{
					throw new UnityException("WWW is not finished downloading yet");
				}
				return WWW.ParseHTTPHeaderString(this.responseHeadersString);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600025B RID: 603
		private extern string responseHeadersString
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000A408 File Offset: 0x00008608
		public string text
		{
			get
			{
				if (!this.isDone)
				{
					throw new UnityException("WWW is not ready downloading yet");
				}
				return this.GetTextEncoder().GetString(this.bytes, 0, this.bytes.Length);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000A448 File Offset: 0x00008648
		internal static Encoding DefaultEncoding
		{
			get
			{
				return Encoding.ASCII;
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A450 File Offset: 0x00008650
		private Encoding GetTextEncoder()
		{
			string text = null;
			if (this.responseHeaders.TryGetValue("CONTENT-TYPE", out text))
			{
				int num = text.IndexOf("charset", StringComparison.OrdinalIgnoreCase);
				if (num > -1)
				{
					int num2 = text.IndexOf('=', num);
					if (num2 > -1)
					{
						string text2 = text.Substring(num2 + 1).Trim().Trim(new char[] { '\'', '"' })
							.Trim();
						int num3 = text2.IndexOf(';');
						if (num3 > -1)
						{
							text2 = text2.Substring(0, num3);
						}
						try
						{
							return Encoding.GetEncoding(text2);
						}
						catch (Exception)
						{
							Debug.Log("Unsupported encoding: '" + text2 + "'");
						}
					}
				}
			}
			return Encoding.UTF8;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000A534 File Offset: 0x00008734
		[Obsolete("Please use WWW.text instead")]
		public string data
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000260 RID: 608
		public extern byte[] bytes
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000261 RID: 609
		public extern int size
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000262 RID: 610
		public extern string error
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000263 RID: 611
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Texture2D GetTexture(bool markNonReadable);

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000A53C File Offset: 0x0000873C
		public Texture2D texture
		{
			get
			{
				return this.GetTexture(false);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000A548 File Offset: 0x00008748
		public Texture2D textureNonReadable
		{
			get
			{
				return this.GetTexture(true);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000A554 File Offset: 0x00008754
		public AudioClip audioClip
		{
			get
			{
				return this.GetAudioClip(true);
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000A560 File Offset: 0x00008760
		public AudioClip GetAudioClip(bool threeD)
		{
			return this.GetAudioClip(threeD, false);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000A56C File Offset: 0x0000876C
		public AudioClip GetAudioClip(bool threeD, bool stream)
		{
			return this.GetAudioClip(threeD, stream, AudioType.UNKNOWN);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000A578 File Offset: 0x00008778
		public AudioClip GetAudioClip(bool threeD, bool stream, AudioType audioType)
		{
			return this.GetAudioClipInternal(threeD, stream, false, audioType);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000A584 File Offset: 0x00008784
		public AudioClip GetAudioClipCompressed()
		{
			return this.GetAudioClipCompressed(true);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A590 File Offset: 0x00008790
		public AudioClip GetAudioClipCompressed(bool threeD)
		{
			return this.GetAudioClipCompressed(threeD, AudioType.UNKNOWN);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000A59C File Offset: 0x0000879C
		public AudioClip GetAudioClipCompressed(bool threeD, AudioType audioType)
		{
			return this.GetAudioClipInternal(threeD, false, true, audioType);
		}

		// Token: 0x0600026D RID: 621
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AudioClip GetAudioClipInternal(bool threeD, bool stream, bool compressed, AudioType audioType);

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600026E RID: 622
		public extern MovieTexture movie
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600026F RID: 623
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void LoadImageIntoTexture(Texture2D tex);

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000270 RID: 624
		public extern bool isDone
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000271 RID: 625
		[WrapperlessIcall]
		[Obsolete("All blocking WWW functions have been deprecated, please use one of the asynchronous functions instead.", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetURL(string url);

		// Token: 0x06000272 RID: 626 RVA: 0x0000A5A8 File Offset: 0x000087A8
		[Obsolete("All blocking WWW functions have been deprecated, please use one of the asynchronous functions instead.", true)]
		public static Texture2D GetTextureFromURL(string url)
		{
			return new WWW(url).texture;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000273 RID: 627
		public extern float progress
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000274 RID: 628
		public extern float uploadProgress
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000275 RID: 629
		public extern int bytesDownloaded
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000276 RID: 630
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void LoadUnityWeb();

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000277 RID: 631
		[Obsolete(".oggVorbis accessor is deprecated, use .audioClip or GetAudioClip() instead.")]
		public extern AudioClip oggVorbis
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000278 RID: 632
		public extern string url
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000279 RID: 633
		public extern AssetBundle assetBundle
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600027A RID: 634
		// (set) Token: 0x0600027B RID: 635
		public extern ThreadPriority threadPriority
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A5B8 File Offset: 0x000087B8
		[ExcludeFromDocs]
		public static WWW LoadFromCacheOrDownload(string url, int version)
		{
			uint num = 0U;
			return WWW.LoadFromCacheOrDownload(url, version, num);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A5D0 File Offset: 0x000087D0
		public static WWW LoadFromCacheOrDownload(string url, int version, [DefaultValue("0")] uint crc)
		{
			return new WWW(url, version, crc);
		}

		// Token: 0x0400019A RID: 410
		internal IntPtr m_Ptr;
	}
}
