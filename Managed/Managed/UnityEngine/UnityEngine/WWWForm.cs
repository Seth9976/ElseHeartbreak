using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200014B RID: 331
	public sealed class WWWForm
	{
		// Token: 0x06000DDD RID: 3549 RVA: 0x0001D7DC File Offset: 0x0001B9DC
		public WWWForm()
		{
			this.formData = new List<byte[]>();
			this.fieldNames = new List<string>();
			this.fileNames = new List<string>();
			this.types = new List<string>();
			this.boundary = new byte[40];
			for (int i = 0; i < 40; i++)
			{
				int num = Random.Range(48, 110);
				if (num > 57)
				{
					num += 7;
				}
				if (num > 90)
				{
					num += 6;
				}
				this.boundary[i] = (byte)num;
			}
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0001D868 File Offset: 0x0001BA68
		[ExcludeFromDocs]
		public void AddField(string fieldName, string value)
		{
			Encoding utf = Encoding.UTF8;
			this.AddField(fieldName, value, utf);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0001D884 File Offset: 0x0001BA84
		public void AddField(string fieldName, string value, [DefaultValue("System.Text.Encoding.UTF8")] Encoding e)
		{
			this.fieldNames.Add(fieldName);
			this.fileNames.Add(null);
			this.formData.Add(e.GetBytes(value));
			this.types.Add("text/plain; charset=\"" + e.WebName + "\"");
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0001D8DC File Offset: 0x0001BADC
		public void AddField(string fieldName, int i)
		{
			this.AddField(fieldName, i.ToString());
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0001D8EC File Offset: 0x0001BAEC
		[ExcludeFromDocs]
		public void AddBinaryData(string fieldName, byte[] contents, string fileName)
		{
			string text = null;
			this.AddBinaryData(fieldName, contents, fileName, text);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0001D908 File Offset: 0x0001BB08
		[ExcludeFromDocs]
		public void AddBinaryData(string fieldName, byte[] contents)
		{
			string text = null;
			string text2 = null;
			this.AddBinaryData(fieldName, contents, text2, text);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0001D924 File Offset: 0x0001BB24
		public void AddBinaryData(string fieldName, byte[] contents, [DefaultValue("null")] string fileName, [DefaultValue("null")] string mimeType)
		{
			this.containsFiles = true;
			bool flag = contents.Length > 8 && contents[0] == 137 && contents[1] == 80 && contents[2] == 78 && contents[3] == 71 && contents[4] == 13 && contents[5] == 10 && contents[6] == 26 && contents[7] == 10;
			if (fileName == null)
			{
				fileName = fieldName + ((!flag) ? ".dat" : ".png");
			}
			if (mimeType == null)
			{
				if (flag)
				{
					mimeType = "image/png";
				}
				else
				{
					mimeType = "application/octet-stream";
				}
			}
			this.fieldNames.Add(fieldName);
			this.fileNames.Add(fileName);
			this.formData.Add(contents);
			this.types.Add(mimeType);
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0001DA0C File Offset: 0x0001BC0C
		public Hashtable headers
		{
			get
			{
				Hashtable hashtable = new Hashtable();
				if (this.containsFiles)
				{
					hashtable["Content-Type"] = "multipart/form-data; boundary=\"" + Encoding.UTF8.GetString(this.boundary) + "\"";
				}
				else
				{
					hashtable["Content-Type"] = "application/x-www-form-urlencoded";
				}
				return hashtable;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x0001DA6C File Offset: 0x0001BC6C
		public byte[] data
		{
			get
			{
				if (this.containsFiles)
				{
					byte[] bytes = WWW.DefaultEncoding.GetBytes("--");
					byte[] bytes2 = WWW.DefaultEncoding.GetBytes("\r\n");
					byte[] bytes3 = WWW.DefaultEncoding.GetBytes("Content-Type: ");
					byte[] bytes4 = WWW.DefaultEncoding.GetBytes("Content-disposition: form-data; name=\"");
					byte[] bytes5 = WWW.DefaultEncoding.GetBytes("\"");
					byte[] bytes6 = WWW.DefaultEncoding.GetBytes("; filename=\"");
					using (MemoryStream memoryStream = new MemoryStream(1024))
					{
						for (int i = 0; i < this.formData.Count; i++)
						{
							memoryStream.Write(bytes2, 0, bytes2.Length);
							memoryStream.Write(bytes, 0, bytes.Length);
							memoryStream.Write(this.boundary, 0, this.boundary.Length);
							memoryStream.Write(bytes2, 0, bytes2.Length);
							memoryStream.Write(bytes3, 0, bytes3.Length);
							byte[] bytes7 = Encoding.UTF8.GetBytes(this.types[i]);
							memoryStream.Write(bytes7, 0, bytes7.Length);
							memoryStream.Write(bytes2, 0, bytes2.Length);
							memoryStream.Write(bytes4, 0, bytes4.Length);
							string headerName = Encoding.UTF8.HeaderName;
							string text = this.fieldNames[i];
							if (!WWWTranscoder.SevenBitClean(text, Encoding.UTF8) || text.IndexOf("=?") > -1)
							{
								text = string.Concat(new string[]
								{
									"=?",
									headerName,
									"?Q?",
									WWWTranscoder.QPEncode(text, Encoding.UTF8),
									"?="
								});
							}
							byte[] bytes8 = Encoding.UTF8.GetBytes(text);
							memoryStream.Write(bytes8, 0, bytes8.Length);
							memoryStream.Write(bytes5, 0, bytes5.Length);
							if (this.fileNames[i] != null)
							{
								string text2 = this.fileNames[i];
								if (!WWWTranscoder.SevenBitClean(text2, Encoding.UTF8) || text2.IndexOf("=?") > -1)
								{
									text2 = string.Concat(new string[]
									{
										"=?",
										headerName,
										"?Q?",
										WWWTranscoder.QPEncode(text2, Encoding.UTF8),
										"?="
									});
								}
								byte[] bytes9 = Encoding.UTF8.GetBytes(text2);
								memoryStream.Write(bytes6, 0, bytes6.Length);
								memoryStream.Write(bytes9, 0, bytes9.Length);
								memoryStream.Write(bytes5, 0, bytes5.Length);
							}
							memoryStream.Write(bytes2, 0, bytes2.Length);
							memoryStream.Write(bytes2, 0, bytes2.Length);
							byte[] array = this.formData[i];
							memoryStream.Write(array, 0, array.Length);
						}
						memoryStream.Write(bytes2, 0, bytes2.Length);
						memoryStream.Write(bytes, 0, bytes.Length);
						memoryStream.Write(this.boundary, 0, this.boundary.Length);
						memoryStream.Write(bytes, 0, bytes.Length);
						memoryStream.Write(bytes2, 0, bytes2.Length);
						return memoryStream.ToArray();
					}
				}
				byte[] bytes10 = WWW.DefaultEncoding.GetBytes("&");
				byte[] bytes11 = WWW.DefaultEncoding.GetBytes("=");
				byte[] array5;
				using (MemoryStream memoryStream2 = new MemoryStream(1024))
				{
					for (int j = 0; j < this.formData.Count; j++)
					{
						byte[] array2 = WWWTranscoder.URLEncode(Encoding.UTF8.GetBytes(this.fieldNames[j]));
						byte[] array3 = this.formData[j];
						byte[] array4 = WWWTranscoder.URLEncode(array3);
						if (j > 0)
						{
							memoryStream2.Write(bytes10, 0, bytes10.Length);
						}
						memoryStream2.Write(array2, 0, array2.Length);
						memoryStream2.Write(bytes11, 0, bytes11.Length);
						memoryStream2.Write(array4, 0, array4.Length);
					}
					array5 = memoryStream2.ToArray();
				}
				return array5;
			}
		}

		// Token: 0x040005BE RID: 1470
		private List<byte[]> formData;

		// Token: 0x040005BF RID: 1471
		private List<string> fieldNames;

		// Token: 0x040005C0 RID: 1472
		private List<string> fileNames;

		// Token: 0x040005C1 RID: 1473
		private List<string> types;

		// Token: 0x040005C2 RID: 1474
		private byte[] boundary;

		// Token: 0x040005C3 RID: 1475
		private bool containsFiles;
	}
}
