using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000096 RID: 150
	public sealed class CrashReport
	{
		// Token: 0x06000323 RID: 803 RVA: 0x0000B2A4 File Offset: 0x000094A4
		private CrashReport(string id, DateTime time, string text)
		{
			this.id = id;
			this.time = time;
			this.text = text;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000B2D0 File Offset: 0x000094D0
		private static int Compare(CrashReport c1, CrashReport c2)
		{
			long ticks = c1.time.Ticks;
			long ticks2 = c2.time.Ticks;
			if (ticks > ticks2)
			{
				return 1;
			}
			if (ticks < ticks2)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000B310 File Offset: 0x00009510
		private static void PopulateReports()
		{
			object obj = CrashReport.reportsLock;
			lock (obj)
			{
				if (CrashReport.internalReports == null)
				{
					string[] reports = CrashReport.GetReports();
					CrashReport.internalReports = new List<CrashReport>(reports.Length);
					foreach (string text in reports)
					{
						double num;
						string text2;
						CrashReport.GetReportData(text, out num, out text2);
						DateTime dateTime = new DateTime(1970, 1, 1);
						DateTime dateTime2 = dateTime.AddSeconds(num);
						CrashReport.internalReports.Add(new CrashReport(text, dateTime2, text2));
					}
					CrashReport.internalReports.Sort(new Comparison<CrashReport>(CrashReport.Compare));
				}
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000B3E0 File Offset: 0x000095E0
		public static CrashReport[] reports
		{
			get
			{
				CrashReport.PopulateReports();
				object obj = CrashReport.reportsLock;
				CrashReport[] array;
				lock (obj)
				{
					array = CrashReport.internalReports.ToArray();
				}
				return array;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000B438 File Offset: 0x00009638
		public static CrashReport lastReport
		{
			get
			{
				CrashReport.PopulateReports();
				object obj = CrashReport.reportsLock;
				lock (obj)
				{
					if (CrashReport.internalReports.Count > 0)
					{
						return CrashReport.internalReports[CrashReport.internalReports.Count - 1];
					}
				}
				return null;
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000B4B0 File Offset: 0x000096B0
		public static void RemoveAll()
		{
			foreach (CrashReport crashReport in CrashReport.reports)
			{
				crashReport.Remove();
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000B4E4 File Offset: 0x000096E4
		public void Remove()
		{
			if (CrashReport.RemoveReport(this.id))
			{
				object obj = CrashReport.reportsLock;
				lock (obj)
				{
					CrashReport.internalReports.Remove(this);
				}
			}
		}

		// Token: 0x0600032B RID: 811
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetReports();

		// Token: 0x0600032C RID: 812
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetReportData(string id, out double secondsSinceUnixEpoch, out string text);

		// Token: 0x0600032D RID: 813
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveReport(string id);

		// Token: 0x04000228 RID: 552
		private static List<CrashReport> internalReports;

		// Token: 0x04000229 RID: 553
		private static object reportsLock = new object();

		// Token: 0x0400022A RID: 554
		private readonly string id;

		// Token: 0x0400022B RID: 555
		public readonly DateTime time;

		// Token: 0x0400022C RID: 556
		public readonly string text;
	}
}
