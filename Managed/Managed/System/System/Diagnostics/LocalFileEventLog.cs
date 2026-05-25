using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x02000231 RID: 561
	internal class LocalFileEventLog : EventLogImpl
	{
		// Token: 0x06001331 RID: 4913 RVA: 0x00033260 File Offset: 0x00031460
		public LocalFileEventLog(EventLog coreEventLog)
			: base(coreEventLog)
		{
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00033278 File Offset: 0x00031478
		public override void BeginInit()
		{
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0003327C File Offset: 0x0003147C
		public override void Clear()
		{
			string text = this.FindLogStore(base.CoreEventLog.Log);
			if (!Directory.Exists(text))
			{
				return;
			}
			foreach (string text2 in Directory.GetFiles(text, "*.log"))
			{
				File.Delete(text2);
			}
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x000332D4 File Offset: 0x000314D4
		public override void Close()
		{
			if (this.file_watcher != null)
			{
				this.file_watcher.EnableRaisingEvents = false;
				this.file_watcher = null;
			}
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x000332F4 File Offset: 0x000314F4
		public override void CreateEventSource(EventSourceCreationData sourceData)
		{
			string text = this.FindLogStore(sourceData.LogName);
			if (!Directory.Exists(text))
			{
				base.ValidateCustomerLogName(sourceData.LogName, sourceData.MachineName);
				Directory.CreateDirectory(text);
				Directory.CreateDirectory(Path.Combine(text, sourceData.LogName));
				if (this.RunningOnUnix)
				{
					LocalFileEventLog.ModifyAccessPermissions(text, "777");
					LocalFileEventLog.ModifyAccessPermissions(text, "+t");
				}
			}
			string text2 = Path.Combine(text, sourceData.Source);
			Directory.CreateDirectory(text2);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0003337C File Offset: 0x0003157C
		public override void Delete(string logName, string machineName)
		{
			string text = this.FindLogStore(logName);
			if (!Directory.Exists(text))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event Log '{0}' does not exist on computer '{1}'.", new object[] { logName, machineName }));
			}
			Directory.Delete(text, true);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000333C8 File Offset: 0x000315C8
		public override void DeleteEventSource(string source, string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered on computer '{1}'.", new object[] { source, machineName }));
			}
			string text = this.FindSourceDirectory(source);
			if (text == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered on computer '{1}'.", new object[] { source, machineName }));
			}
			Directory.Delete(text);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00033440 File Offset: 0x00031640
		public override void Dispose(bool disposing)
		{
			this.Close();
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00033448 File Offset: 0x00031648
		public override void DisableNotification()
		{
			if (this.file_watcher == null)
			{
				return;
			}
			this.file_watcher.EnableRaisingEvents = false;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00033464 File Offset: 0x00031664
		public override void EnableNotification()
		{
			if (this.file_watcher == null)
			{
				string text = this.FindLogStore(base.CoreEventLog.Log);
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				this.file_watcher = new global::System.IO.FileSystemWatcher();
				this.file_watcher.Path = text;
				this.file_watcher.Created += delegate(object o, global::System.IO.FileSystemEventArgs e)
				{
					lock (this)
					{
						if (this._notifying)
						{
							return;
						}
						this._notifying = true;
					}
					Thread.Sleep(100);
					try
					{
						while (this.GetLatestIndex() > this.last_notification_index)
						{
							try
							{
								base.CoreEventLog.OnEntryWritten(this.GetEntry(this.last_notification_index++));
							}
							catch (Exception ex)
							{
							}
						}
					}
					finally
					{
						lock (this)
						{
							this._notifying = false;
						}
					}
				};
			}
			this.last_notification_index = this.GetLatestIndex();
			this.file_watcher.EnableRaisingEvents = true;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x000334E8 File Offset: 0x000316E8
		public override void EndInit()
		{
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x000334EC File Offset: 0x000316EC
		public override bool Exists(string logName, string machineName)
		{
			string text = this.FindLogStore(logName);
			return Directory.Exists(text);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00033508 File Offset: 0x00031708
		[global::System.MonoTODO("Use MessageTable from PE for lookup")]
		protected override string FormatMessage(string source, uint eventID, string[] replacementStrings)
		{
			return string.Join(", ", replacementStrings);
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00033518 File Offset: 0x00031718
		protected override int GetEntryCount()
		{
			string text = this.FindLogStore(base.CoreEventLog.Log);
			if (!Directory.Exists(text))
			{
				return 0;
			}
			string[] files = Directory.GetFiles(text, "*.log");
			return files.Length;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00033554 File Offset: 0x00031754
		protected override EventLogEntry GetEntry(int index)
		{
			string text = this.FindLogStore(base.CoreEventLog.Log);
			string text2 = Path.Combine(text, (index + 1).ToString(CultureInfo.InvariantCulture) + ".log");
			EventLogEntry eventLogEntry;
			using (TextReader textReader = File.OpenText(text2))
			{
				int num = int.Parse(Path.GetFileNameWithoutExtension(text2), CultureInfo.InvariantCulture);
				uint num2 = uint.Parse(textReader.ReadLine().Substring(12), CultureInfo.InvariantCulture);
				EventLogEntryType eventLogEntryType = (EventLogEntryType)((int)Enum.Parse(typeof(EventLogEntryType), textReader.ReadLine().Substring(11)));
				string text3 = textReader.ReadLine().Substring(8);
				string text4 = textReader.ReadLine().Substring(10);
				short num3 = short.Parse(text4, CultureInfo.InvariantCulture);
				string text5 = "(" + text4 + ")";
				DateTime dateTime = DateTime.ParseExact(textReader.ReadLine().Substring(15), "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
				DateTime lastWriteTime = File.GetLastWriteTime(text2);
				int num4 = int.Parse(textReader.ReadLine().Substring(20));
				ArrayList arrayList = new ArrayList();
				StringBuilder stringBuilder = new StringBuilder();
				while (arrayList.Count < num4)
				{
					char c = (char)textReader.Read();
					if (c == '\0')
					{
						arrayList.Add(stringBuilder.ToString());
						stringBuilder.Length = 0;
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				string[] array = new string[arrayList.Count];
				arrayList.CopyTo(array, 0);
				string text6 = this.FormatMessage(text3, num2, array);
				int eventID = EventLog.GetEventID((long)((ulong)num2));
				byte[] array2 = Convert.FromBase64String(textReader.ReadToEnd());
				eventLogEntry = new EventLogEntry(text5, num3, num, eventID, text3, text6, null, Environment.MachineName, eventLogEntryType, dateTime, lastWriteTime, array2, array, (long)((ulong)num2));
			}
			return eventLogEntry;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00033750 File Offset: 0x00031950
		[global::System.MonoTODO]
		protected override string GetLogDisplayName()
		{
			return base.CoreEventLog.Log;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00033760 File Offset: 0x00031960
		protected override string[] GetLogNames(string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return new string[0];
			}
			string[] directories = Directory.GetDirectories(this.EventLogStore, "*");
			string[] array = new string[directories.Length];
			for (int i = 0; i < directories.Length; i++)
			{
				array[i] = Path.GetFileName(directories[i]);
			}
			return array;
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000337C0 File Offset: 0x000319C0
		public override string LogNameFromSourceName(string source, string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return string.Empty;
			}
			string text = this.FindSourceDirectory(source);
			if (text == null)
			{
				return string.Empty;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(text);
			return directoryInfo.Parent.Name;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0003380C File Offset: 0x00031A0C
		public override bool SourceExists(string source, string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return false;
			}
			string text = this.FindSourceDirectory(source);
			return text != null;
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0003383C File Offset: 0x00031A3C
		public override void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData)
		{
			object obj = LocalFileEventLog.lockObject;
			lock (obj)
			{
				string text = this.FindLogStore(base.CoreEventLog.Log);
				string text2 = Path.Combine(text, (this.GetLatestIndex() + 1).ToString(CultureInfo.InvariantCulture) + ".log");
				try
				{
					using (TextWriter textWriter = File.CreateText(text2))
					{
						textWriter.WriteLine("InstanceID: {0}", instanceID.ToString(CultureInfo.InvariantCulture));
						textWriter.WriteLine("EntryType: {0}", (int)type);
						textWriter.WriteLine("Source: {0}", base.CoreEventLog.Source);
						textWriter.WriteLine("Category: {0}", category.ToString(CultureInfo.InvariantCulture));
						textWriter.WriteLine("TimeGenerated: {0}", DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
						textWriter.WriteLine("ReplacementStrings: {0}", replacementStrings.Length.ToString(CultureInfo.InvariantCulture));
						StringBuilder stringBuilder = new StringBuilder();
						foreach (string text3 in replacementStrings)
						{
							stringBuilder.Append(text3);
							stringBuilder.Append('\0');
						}
						textWriter.Write(stringBuilder.ToString());
						textWriter.Write(Convert.ToBase64String(rawData));
					}
				}
				catch (IOException)
				{
					File.Delete(text2);
				}
			}
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00033A00 File Offset: 0x00031C00
		private string FindSourceDirectory(string source)
		{
			string text = null;
			string[] directories = Directory.GetDirectories(this.EventLogStore, "*");
			for (int i = 0; i < directories.Length; i++)
			{
				string[] directories2 = Directory.GetDirectories(directories[i], "*");
				for (int j = 0; j < directories2.Length; j++)
				{
					string fileName = Path.GetFileName(directories2[j]);
					if (string.Compare(fileName, source, true, CultureInfo.InvariantCulture) == 0)
					{
						text = directories2[j];
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x00033A84 File Offset: 0x00031C84
		private bool RunningOnUnix
		{
			get
			{
				int platform = (int)Environment.OSVersion.Platform;
				return platform == 4 || platform == 128 || platform == 6;
			}
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00033AB8 File Offset: 0x00031CB8
		private string FindLogStore(string logName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return Path.Combine(this.EventLogStore, logName);
			}
			string[] directories = Directory.GetDirectories(this.EventLogStore, "*");
			for (int i = 0; i < directories.Length; i++)
			{
				string fileName = Path.GetFileName(directories[i]);
				if (string.Compare(fileName, logName, true, CultureInfo.InvariantCulture) == 0)
				{
					return directories[i];
				}
			}
			return Path.Combine(this.EventLogStore, logName);
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x00033B34 File Offset: 0x00031D34
		private string EventLogStore
		{
			get
			{
				string environmentVariable = Environment.GetEnvironmentVariable("MONO_EVENTLOG_TYPE");
				if (environmentVariable != null && environmentVariable.Length > "local".Length + 1)
				{
					return environmentVariable.Substring("local".Length + 1);
				}
				if (this.RunningOnUnix)
				{
					return "/var/lib/mono/eventlog";
				}
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "mono\\eventlog");
			}
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00033BA0 File Offset: 0x00031DA0
		private int GetLatestIndex()
		{
			int num = 0;
			string[] files = Directory.GetFiles(this.FindLogStore(base.CoreEventLog.Log), "*.log");
			for (int i = 0; i < files.Length; i++)
			{
				try
				{
					string text = files[i];
					int num2 = int.Parse(Path.GetFileNameWithoutExtension(text), CultureInfo.InvariantCulture);
					if (num2 > num)
					{
						num = num2;
					}
				}
				catch
				{
				}
			}
			return num;
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00033C28 File Offset: 0x00031E28
		private static void ModifyAccessPermissions(string path, string permissions)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = "chmod";
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.UseShellExecute = false;
			processStartInfo.Arguments = string.Format("{0} \"{1}\"", permissions, path);
			Process process = null;
			try
			{
				process = Process.Start(processStartInfo);
			}
			catch (Exception ex)
			{
				throw new SecurityException("Access permissions could not be modified.", ex);
			}
			process.WaitForExit();
			if (process.ExitCode != 0)
			{
				process.Close();
				throw new SecurityException("Access permissions could not be modified.");
			}
			process.Close();
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x00033CD4 File Offset: 0x00031ED4
		public override OverflowAction OverflowAction
		{
			get
			{
				return OverflowAction.DoNotOverwrite;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x00033CD8 File Offset: 0x00031ED8
		public override int MinimumRetentionDays
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x00033CE0 File Offset: 0x00031EE0
		// (set) Token: 0x0600134F RID: 4943 RVA: 0x00033CEC File Offset: 0x00031EEC
		public override long MaximumKilobytes
		{
			get
			{
				return long.MaxValue;
			}
			set
			{
				throw new NotSupportedException("This EventLog implementation does not support setting max kilobytes policy");
			}
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00033CF8 File Offset: 0x00031EF8
		public override void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			throw new NotSupportedException("This EventLog implementation does not support modifying overflow policy");
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00033D04 File Offset: 0x00031F04
		public override void RegisterDisplayName(string resourceFile, long resourceId)
		{
			throw new NotSupportedException("This EventLog implementation does not support registering display name");
		}

		// Token: 0x0400058C RID: 1420
		private const string DateFormat = "yyyyMMddHHmmssfff";

		// Token: 0x0400058D RID: 1421
		private static readonly object lockObject = new object();

		// Token: 0x0400058E RID: 1422
		private global::System.IO.FileSystemWatcher file_watcher;

		// Token: 0x0400058F RID: 1423
		private int last_notification_index;

		// Token: 0x04000590 RID: 1424
		private bool _notifying;
	}
}
