using System;
using System.Collections;

namespace System.Net.Configuration
{
	// Token: 0x020002CE RID: 718
	internal class ConnectionManagementData
	{
		// Token: 0x060018B4 RID: 6324 RVA: 0x00043D74 File Offset: 0x00041F74
		public ConnectionManagementData(object parent)
		{
			this.data = new Hashtable(CaseInsensitiveHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant);
			if (parent != null && parent is ConnectionManagementData)
			{
				ConnectionManagementData connectionManagementData = (ConnectionManagementData)parent;
				foreach (object obj in connectionManagementData.data.Keys)
				{
					string text = (string)obj;
					this.data[text] = connectionManagementData.data[text];
				}
			}
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x00043E2C File Offset: 0x0004202C
		public void Add(string address, string nconns)
		{
			if (nconns == null || nconns == string.Empty)
			{
				nconns = "2";
			}
			this.data[address] = uint.Parse(nconns);
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00043E70 File Offset: 0x00042070
		public void Add(string address, int nconns)
		{
			this.data[address] = (uint)nconns;
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00043E84 File Offset: 0x00042084
		public void Remove(string address)
		{
			this.data.Remove(address);
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00043E94 File Offset: 0x00042094
		public void Clear()
		{
			this.data.Clear();
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00043EA4 File Offset: 0x000420A4
		public uint GetMaxConnections(string hostOrIP)
		{
			object obj = this.data[hostOrIP];
			if (obj == null)
			{
				obj = this.data["*"];
			}
			if (obj == null)
			{
				return 2U;
			}
			return (uint)obj;
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x00043EE4 File Offset: 0x000420E4
		public Hashtable Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x04000FBA RID: 4026
		private const int defaultMaxConnections = 2;

		// Token: 0x04000FBB RID: 4027
		private Hashtable data;
	}
}
