using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x02000265 RID: 613
	internal class Win32EventLog : EventLogImpl
	{
		// Token: 0x060015C4 RID: 5572 RVA: 0x00039820 File Offset: 0x00037A20
		public Win32EventLog(EventLog coreEventLog)
			: base(coreEventLog)
		{
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0003982C File Offset: 0x00037A2C
		public override void BeginInit()
		{
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00039830 File Offset: 0x00037A30
		public override void Clear()
		{
			int num = Win32EventLog.PInvoke.ClearEventLog(this.ReadHandle, null);
			if (num != 1)
			{
				throw new global::System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x0003985C File Offset: 0x00037A5C
		public override void Close()
		{
			if (this._readHandle != IntPtr.Zero)
			{
				this.CloseEventLog(this._readHandle);
				this._readHandle = IntPtr.Zero;
			}
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00039898 File Offset: 0x00037A98
		public override void CreateEventSource(EventSourceCreationData sourceData)
		{
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(sourceData.MachineName, true))
			{
				if (eventLogKey == null)
				{
					throw new InvalidOperationException("EventLog registry key is missing.");
				}
				bool flag = false;
				RegistryKey registryKey = null;
				try
				{
					registryKey = eventLogKey.OpenSubKey(sourceData.LogName, true);
					if (registryKey == null)
					{
						base.ValidateCustomerLogName(sourceData.LogName, sourceData.MachineName);
						registryKey = eventLogKey.CreateSubKey(sourceData.LogName);
						registryKey.SetValue("Sources", new string[] { sourceData.LogName, sourceData.Source });
						Win32EventLog.UpdateLogRegistry(registryKey);
						using (RegistryKey registryKey2 = registryKey.CreateSubKey(sourceData.LogName))
						{
							Win32EventLog.UpdateSourceRegistry(registryKey2, sourceData);
						}
						flag = true;
					}
					if (sourceData.LogName != sourceData.Source)
					{
						if (!flag)
						{
							string[] array = (string[])registryKey.GetValue("Sources");
							if (array == null)
							{
								registryKey.SetValue("Sources", new string[] { sourceData.LogName, sourceData.Source });
							}
							else
							{
								bool flag2 = false;
								for (int i = 0; i < array.Length; i++)
								{
									if (array[i] == sourceData.Source)
									{
										flag2 = true;
										break;
									}
								}
								if (!flag2)
								{
									string[] array2 = new string[array.Length + 1];
									Array.Copy(array, 0, array2, 0, array.Length);
									array2[array.Length] = sourceData.Source;
									registryKey.SetValue("Sources", array2);
								}
							}
						}
						using (RegistryKey registryKey3 = registryKey.CreateSubKey(sourceData.Source))
						{
							Win32EventLog.UpdateSourceRegistry(registryKey3, sourceData);
						}
					}
				}
				finally
				{
					if (registryKey != null)
					{
						registryKey.Close();
					}
				}
			}
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x00039AD0 File Offset: 0x00037CD0
		public override void Delete(string logName, string machineName)
		{
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(machineName, true))
			{
				if (eventLogKey == null)
				{
					throw new InvalidOperationException("The event log key does not exist.");
				}
				using (RegistryKey registryKey = eventLogKey.OpenSubKey(logName, false))
				{
					if (registryKey == null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event Log '{0}' does not exist on computer '{1}'.", new object[] { logName, machineName }));
					}
					base.CoreEventLog.Clear();
					string text = (string)registryKey.GetValue("File");
					if (text != null)
					{
						try
						{
							File.Delete(text);
						}
						catch (Exception)
						{
						}
					}
				}
				eventLogKey.DeleteSubKeyTree(logName);
			}
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00039BD4 File Offset: 0x00037DD4
		public override void DeleteEventSource(string source, string machineName)
		{
			using (RegistryKey registryKey = Win32EventLog.FindLogKeyBySource(source, machineName, true))
			{
				if (registryKey == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered on computer '{1}'.", new object[] { source, machineName }));
				}
				registryKey.DeleteSubKeyTree(source);
				string[] array = (string[])registryKey.GetValue("Sources");
				if (array != null)
				{
					ArrayList arrayList = new ArrayList();
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != source)
						{
							arrayList.Add(array[i]);
						}
					}
					string[] array2 = new string[arrayList.Count];
					arrayList.CopyTo(array2, 0);
					registryKey.SetValue("Sources", array2);
				}
			}
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00039CB4 File Offset: 0x00037EB4
		public override void Dispose(bool disposing)
		{
			this.Close();
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00039CBC File Offset: 0x00037EBC
		public override void EndInit()
		{
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x00039CC0 File Offset: 0x00037EC0
		public override bool Exists(string logName, string machineName)
		{
			bool flag;
			using (RegistryKey registryKey = Win32EventLog.FindLogKeyByName(logName, machineName, false))
			{
				flag = registryKey != null;
			}
			return flag;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00039D14 File Offset: 0x00037F14
		[global::System.MonoTODO]
		protected override string FormatMessage(string source, uint messageID, string[] replacementStrings)
		{
			string text = null;
			string[] messageResourceDlls = this.GetMessageResourceDlls(source, "EventMessageFile");
			for (int i = 0; i < messageResourceDlls.Length; i++)
			{
				text = Win32EventLog.FetchMessage(messageResourceDlls[i], messageID, replacementStrings);
				if (text != null)
				{
					break;
				}
			}
			return (text == null) ? string.Join(", ", replacementStrings) : text;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00039D74 File Offset: 0x00037F74
		private string FormatCategory(string source, int category)
		{
			string text = null;
			string[] messageResourceDlls = this.GetMessageResourceDlls(source, "CategoryMessageFile");
			for (int i = 0; i < messageResourceDlls.Length; i++)
			{
				text = Win32EventLog.FetchMessage(messageResourceDlls[i], (uint)category, new string[0]);
				if (text != null)
				{
					break;
				}
			}
			return (text == null) ? ("(" + category.ToString(CultureInfo.InvariantCulture) + ")") : text;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x00039DE8 File Offset: 0x00037FE8
		protected override int GetEntryCount()
		{
			int num = 0;
			int numberOfEventLogRecords = Win32EventLog.PInvoke.GetNumberOfEventLogRecords(this.ReadHandle, ref num);
			if (numberOfEventLogRecords != 1)
			{
				throw new global::System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
			}
			return num;
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00039E18 File Offset: 0x00038018
		protected override EventLogEntry GetEntry(int index)
		{
			index += this.OldestEventLogEntry;
			int num = 0;
			int num2 = 0;
			byte[] array = new byte[524287];
			this.ReadEventLog(index, array, ref num, ref num2);
			MemoryStream memoryStream = new MemoryStream(array);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			binaryReader.ReadBytes(8);
			int num3 = binaryReader.ReadInt32();
			int num4 = binaryReader.ReadInt32();
			int num5 = binaryReader.ReadInt32();
			uint num6 = binaryReader.ReadUInt32();
			int eventID = EventLog.GetEventID((long)((ulong)num6));
			short num7 = binaryReader.ReadInt16();
			short num8 = binaryReader.ReadInt16();
			short num9 = binaryReader.ReadInt16();
			binaryReader.ReadInt16();
			binaryReader.ReadInt32();
			int num10 = binaryReader.ReadInt32();
			int num11 = binaryReader.ReadInt32();
			int num12 = binaryReader.ReadInt32();
			int num13 = binaryReader.ReadInt32();
			int num14 = binaryReader.ReadInt32();
			DateTime dateTime = new DateTime(1970, 1, 1);
			DateTime dateTime2 = dateTime.AddSeconds((double)num4);
			DateTime dateTime3 = new DateTime(1970, 1, 1);
			DateTime dateTime4 = dateTime3.AddSeconds((double)num5);
			StringBuilder stringBuilder = new StringBuilder();
			while (binaryReader.PeekChar() != 0)
			{
				stringBuilder.Append(binaryReader.ReadChar());
			}
			binaryReader.ReadChar();
			string text = stringBuilder.ToString();
			stringBuilder.Length = 0;
			while (binaryReader.PeekChar() != 0)
			{
				stringBuilder.Append(binaryReader.ReadChar());
			}
			binaryReader.ReadChar();
			string text2 = stringBuilder.ToString();
			stringBuilder.Length = 0;
			while (binaryReader.PeekChar() != 0)
			{
				stringBuilder.Append(binaryReader.ReadChar());
			}
			binaryReader.ReadChar();
			string text3 = null;
			if (num11 != 0)
			{
				memoryStream.Position = (long)num12;
				byte[] array2 = binaryReader.ReadBytes(num11);
				text3 = Win32EventLog.LookupAccountSid(text2, array2);
			}
			memoryStream.Position = (long)num10;
			string[] array3 = new string[(int)num8];
			for (int i = 0; i < (int)num8; i++)
			{
				stringBuilder.Length = 0;
				while (binaryReader.PeekChar() != 0)
				{
					stringBuilder.Append(binaryReader.ReadChar());
				}
				binaryReader.ReadChar();
				array3[i] = stringBuilder.ToString();
			}
			byte[] array4 = new byte[num13];
			memoryStream.Position = (long)num14;
			binaryReader.Read(array4, 0, num13);
			string text4 = this.FormatMessage(text, num6, array3);
			string text5 = this.FormatCategory(text, (int)num9);
			return new EventLogEntry(text5, num9, num3, eventID, text, text4, text3, text2, (EventLogEntryType)num7, dateTime2, dateTime4, array4, array3, (long)((ulong)num6));
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x0003A0A8 File Offset: 0x000382A8
		[global::System.MonoTODO]
		protected override string GetLogDisplayName()
		{
			return base.CoreEventLog.Log;
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x0003A0B8 File Offset: 0x000382B8
		protected override string[] GetLogNames(string machineName)
		{
			string[] array;
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(machineName, true))
			{
				if (eventLogKey == null)
				{
					array = new string[0];
				}
				else
				{
					array = eventLogKey.GetSubKeyNames();
				}
			}
			return array;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0003A11C File Offset: 0x0003831C
		public override string LogNameFromSourceName(string source, string machineName)
		{
			string text;
			using (RegistryKey registryKey = Win32EventLog.FindLogKeyBySource(source, machineName, false))
			{
				if (registryKey == null)
				{
					text = string.Empty;
				}
				else
				{
					text = Win32EventLog.GetLogName(registryKey);
				}
			}
			return text;
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0003A180 File Offset: 0x00038380
		public override bool SourceExists(string source, string machineName)
		{
			RegistryKey registryKey = Win32EventLog.FindLogKeyBySource(source, machineName, false);
			if (registryKey != null)
			{
				registryKey.Close();
				return true;
			}
			return false;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0003A1A8 File Offset: 0x000383A8
		public override void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData)
		{
			IntPtr intPtr = this.RegisterEventSource();
			try
			{
				int num = Win32EventLog.PInvoke.ReportEvent(intPtr, (ushort)type, (ushort)category, instanceID, IntPtr.Zero, (ushort)replacementStrings.Length, (uint)rawData.Length, replacementStrings, rawData);
				if (num != 1)
				{
					throw new global::System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				this.DeregisterEventSource(intPtr);
			}
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0003A214 File Offset: 0x00038414
		private static void UpdateLogRegistry(RegistryKey logKey)
		{
			if (logKey.GetValue("File") == null)
			{
				string logName = Win32EventLog.GetLogName(logKey);
				string text;
				if (logName.Length > 8)
				{
					text = logName.Substring(0, 8) + ".evt";
				}
				else
				{
					text = logName + ".evt";
				}
				string text2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "config");
				logKey.SetValue("File", Path.Combine(text2, text));
			}
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x0003A28C File Offset: 0x0003848C
		private static void UpdateSourceRegistry(RegistryKey sourceKey, EventSourceCreationData data)
		{
			if (data.CategoryCount > 0)
			{
				sourceKey.SetValue("CategoryCount", data.CategoryCount);
			}
			if (data.CategoryResourceFile != null && data.CategoryResourceFile.Length > 0)
			{
				sourceKey.SetValue("CategoryMessageFile", data.CategoryResourceFile);
			}
			if (data.MessageResourceFile != null && data.MessageResourceFile.Length > 0)
			{
				sourceKey.SetValue("EventMessageFile", data.MessageResourceFile);
			}
			if (data.ParameterResourceFile != null && data.ParameterResourceFile.Length > 0)
			{
				sourceKey.SetValue("ParameterMessageFile", data.ParameterResourceFile);
			}
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0003A348 File Offset: 0x00038548
		private static string GetLogName(RegistryKey logKey)
		{
			string name = logKey.Name;
			return name.Substring(name.LastIndexOf("\\") + 1);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0003A370 File Offset: 0x00038570
		private void ReadEventLog(int index, byte[] buffer, ref int bytesRead, ref int minBufferNeeded)
		{
			for (int i = 0; i < 3; i++)
			{
				int num = Win32EventLog.PInvoke.ReadEventLog(this.ReadHandle, (Win32EventLog.ReadFlags)6, index, buffer, buffer.Length, ref bytesRead, ref minBufferNeeded);
				if (num != 1)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (i >= 2)
					{
						throw new global::System.ComponentModel.Win32Exception(lastWin32Error);
					}
					base.CoreEventLog.Reset();
				}
			}
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0003A3D0 File Offset: 0x000385D0
		[global::System.MonoTODO("Support remote machines")]
		private static RegistryKey GetEventLogKey(string machineName, bool writable)
		{
			return Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\EventLog", writable);
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x0003A3E4 File Offset: 0x000385E4
		private static RegistryKey FindSourceKeyByName(string source, string machineName, bool writable)
		{
			if (source == null || source.Length == 0)
			{
				return null;
			}
			RegistryKey registryKey = null;
			RegistryKey registryKey2;
			try
			{
				registryKey = Win32EventLog.GetEventLogKey(machineName, writable);
				if (registryKey == null)
				{
					registryKey2 = null;
				}
				else
				{
					string[] subKeyNames = registryKey.GetSubKeyNames();
					for (int i = 0; i < subKeyNames.Length; i++)
					{
						using (RegistryKey registryKey3 = registryKey.OpenSubKey(subKeyNames[i], writable))
						{
							if (registryKey3 == null)
							{
								break;
							}
							RegistryKey registryKey4 = registryKey3.OpenSubKey(source, writable);
							if (registryKey4 != null)
							{
								return registryKey4;
							}
						}
					}
					registryKey2 = null;
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return registryKey2;
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x0003A4CC File Offset: 0x000386CC
		private static RegistryKey FindLogKeyByName(string logName, string machineName, bool writable)
		{
			RegistryKey registryKey;
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(machineName, writable))
			{
				if (eventLogKey == null)
				{
					registryKey = null;
				}
				else
				{
					registryKey = eventLogKey.OpenSubKey(logName, writable);
				}
			}
			return registryKey;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x0003A52C File Offset: 0x0003872C
		private static RegistryKey FindLogKeyBySource(string source, string machineName, bool writable)
		{
			if (source == null || source.Length == 0)
			{
				return null;
			}
			RegistryKey registryKey = null;
			RegistryKey registryKey2;
			try
			{
				registryKey = Win32EventLog.GetEventLogKey(machineName, writable);
				if (registryKey == null)
				{
					registryKey2 = null;
				}
				else
				{
					string[] subKeyNames = registryKey.GetSubKeyNames();
					for (int i = 0; i < subKeyNames.Length; i++)
					{
						RegistryKey registryKey3 = null;
						try
						{
							RegistryKey registryKey4 = registryKey.OpenSubKey(subKeyNames[i], writable);
							if (registryKey4 != null)
							{
								registryKey3 = registryKey4.OpenSubKey(source, writable);
								if (registryKey3 != null)
								{
									return registryKey4;
								}
							}
						}
						finally
						{
							if (registryKey3 != null)
							{
								registryKey3.Close();
							}
						}
					}
					registryKey2 = null;
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return registryKey2;
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x0003A610 File Offset: 0x00038810
		private int OldestEventLogEntry
		{
			get
			{
				int num = 0;
				int oldestEventLogRecord = Win32EventLog.PInvoke.GetOldestEventLogRecord(this.ReadHandle, ref num);
				if (oldestEventLogRecord != 1)
				{
					throw new global::System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
				}
				return num;
			}
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0003A640 File Offset: 0x00038840
		private void CloseEventLog(IntPtr hEventLog)
		{
			int num = Win32EventLog.PInvoke.CloseEventLog(hEventLog);
			if (num != 1)
			{
				throw new global::System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0003A668 File Offset: 0x00038868
		private void DeregisterEventSource(IntPtr hEventLog)
		{
			int num = Win32EventLog.PInvoke.DeregisterEventSource(hEventLog);
			if (num != 1)
			{
				throw new global::System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x0003A690 File Offset: 0x00038890
		private static string LookupAccountSid(string machineName, byte[] sid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			uint capacity = (uint)stringBuilder.Capacity;
			StringBuilder stringBuilder2 = new StringBuilder();
			uint capacity2 = (uint)stringBuilder2.Capacity;
			string text = null;
			while (text == null)
			{
				Win32EventLog.SidNameUse sidNameUse;
				if (!Win32EventLog.PInvoke.LookupAccountSid(machineName, sid, stringBuilder, ref capacity, stringBuilder2, ref capacity2, out sidNameUse))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error == 122)
					{
						stringBuilder.EnsureCapacity((int)capacity);
						stringBuilder2.EnsureCapacity((int)capacity2);
					}
					else
					{
						text = string.Empty;
					}
				}
				else
				{
					text = string.Format("{0}\\{1}", stringBuilder2.ToString(), stringBuilder.ToString());
				}
			}
			return text;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x0003A72C File Offset: 0x0003892C
		private static string FetchMessage(string msgDll, uint messageID, string[] replacementStrings)
		{
			IntPtr intPtr = Win32EventLog.PInvoke.LoadLibraryEx(msgDll, IntPtr.Zero, Win32EventLog.LoadFlags.LibraryAsDataFile);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr[] array = new IntPtr[replacementStrings.Length];
			try
			{
				for (int i = 0; i < replacementStrings.Length; i++)
				{
					array[i] = Marshal.StringToHGlobalAuto(replacementStrings[i]);
				}
				int num = Win32EventLog.PInvoke.FormatMessage(Win32EventLog.FormatMessageFlags.AllocateBuffer | Win32EventLog.FormatMessageFlags.FromHModule | Win32EventLog.FormatMessageFlags.ArgumentArray, intPtr, messageID, 0, ref intPtr2, 0, array);
				if (num != 0)
				{
					string text = Marshal.PtrToStringAuto(intPtr2);
					intPtr2 = Win32EventLog.PInvoke.LocalFree(intPtr2);
					return text.TrimEnd(null);
				}
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 317)
				{
				}
			}
			finally
			{
				foreach (IntPtr intPtr3 in array)
				{
					if (intPtr3 != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr3);
					}
				}
				Win32EventLog.PInvoke.FreeLibrary(intPtr);
			}
			return null;
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x0003A844 File Offset: 0x00038A44
		private string[] GetMessageResourceDlls(string source, string valueName)
		{
			RegistryKey registryKey = Win32EventLog.FindSourceKeyByName(source, base.CoreEventLog.MachineName, false);
			if (registryKey != null)
			{
				string text = registryKey.GetValue(valueName) as string;
				if (text != null)
				{
					return text.Split(new char[] { ';' });
				}
			}
			return new string[0];
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x0003A898 File Offset: 0x00038A98
		private IntPtr ReadHandle
		{
			get
			{
				if (this._readHandle != IntPtr.Zero)
				{
					return this._readHandle;
				}
				string logName = base.CoreEventLog.GetLogName();
				this._readHandle = Win32EventLog.PInvoke.OpenEventLog(base.CoreEventLog.MachineName, logName);
				if (this._readHandle == IntPtr.Zero)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event Log '{0}' on computer '{1}' cannot be opened.", new object[]
					{
						logName,
						base.CoreEventLog.MachineName
					}), new global::System.ComponentModel.Win32Exception());
				}
				return this._readHandle;
			}
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x0003A934 File Offset: 0x00038B34
		private IntPtr RegisterEventSource()
		{
			IntPtr intPtr = Win32EventLog.PInvoke.RegisterEventSource(base.CoreEventLog.MachineName, base.CoreEventLog.Source);
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event source '{0}' on computer '{1}' cannot be opened.", new object[]
				{
					base.CoreEventLog.Source,
					base.CoreEventLog.MachineName
				}), new global::System.ComponentModel.Win32Exception());
			}
			return intPtr;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x0003A9AC File Offset: 0x00038BAC
		public override void DisableNotification()
		{
			if (this._notifyResetEvent != null)
			{
				this._notifyResetEvent.Close();
				this._notifyResetEvent = null;
			}
			if (this._notifyThread != null)
			{
				if (this._notifyThread.ThreadState == ThreadState.Running)
				{
					this._notifyThread.Abort();
				}
				this._notifyThread = null;
			}
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x0003AA04 File Offset: 0x00038C04
		public override void EnableNotification()
		{
			this._notifyResetEvent = new ManualResetEvent(false);
			this._lastEntryWritten = this.OldestEventLogEntry + base.EntryCount;
			if (Win32EventLog.PInvoke.NotifyChangeEventLog(this.ReadHandle, this._notifyResetEvent.Handle) == 0)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unable to receive notifications for log '{0}' on computer '{1}'.", new object[]
				{
					base.CoreEventLog.GetLogName(),
					base.CoreEventLog.MachineName
				}), new global::System.ComponentModel.Win32Exception());
			}
			this._notifyThread = new Thread(new ThreadStart(this.NotifyEventThread));
			this._notifyThread.IsBackground = true;
			this._notifyThread.Start();
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x0003AAB8 File Offset: 0x00038CB8
		private void NotifyEventThread()
		{
			for (;;)
			{
				this._notifyResetEvent.WaitOne();
				lock (this)
				{
					if (this._notifying)
					{
						break;
					}
					this._notifying = true;
				}
				try
				{
					int oldestEventLogEntry = this.OldestEventLogEntry;
					if (this._lastEntryWritten < oldestEventLogEntry)
					{
						this._lastEntryWritten = oldestEventLogEntry;
					}
					int num = this._lastEntryWritten - oldestEventLogEntry;
					int num2 = base.EntryCount + oldestEventLogEntry;
					for (int i = num; i < num2 - 1; i++)
					{
						EventLogEntry entry = this.GetEntry(i);
						base.CoreEventLog.OnEntryWritten(entry);
					}
					this._lastEntryWritten = num2;
				}
				finally
				{
					lock (this)
					{
						this._notifying = false;
					}
				}
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x0003ABD4 File Offset: 0x00038DD4
		public override OverflowAction OverflowAction
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x0003ABDC File Offset: 0x00038DDC
		public override int MinimumRetentionDays
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x0003ABE4 File Offset: 0x00038DE4
		// (set) Token: 0x060015ED RID: 5613 RVA: 0x0003ABEC File Offset: 0x00038DEC
		public override long MaximumKilobytes
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x0003ABF4 File Offset: 0x00038DF4
		public override void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x0003ABFC File Offset: 0x00038DFC
		public override void RegisterDisplayName(string resourceFile, long resourceId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040006B6 RID: 1718
		private const int MESSAGE_NOT_FOUND = 317;

		// Token: 0x040006B7 RID: 1719
		private ManualResetEvent _notifyResetEvent;

		// Token: 0x040006B8 RID: 1720
		private IntPtr _readHandle;

		// Token: 0x040006B9 RID: 1721
		private Thread _notifyThread;

		// Token: 0x040006BA RID: 1722
		private int _lastEntryWritten;

		// Token: 0x040006BB RID: 1723
		private bool _notifying;

		// Token: 0x02000266 RID: 614
		private class PInvoke
		{
			// Token: 0x060015F1 RID: 5617
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int ClearEventLog(IntPtr hEventLog, string lpBackupFileName);

			// Token: 0x060015F2 RID: 5618
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int CloseEventLog(IntPtr hEventLog);

			// Token: 0x060015F3 RID: 5619
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int DeregisterEventSource(IntPtr hEventLog);

			// Token: 0x060015F4 RID: 5620
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			public static extern int FormatMessage(Win32EventLog.FormatMessageFlags dwFlags, IntPtr lpSource, uint dwMessageId, int dwLanguageId, ref IntPtr lpBuffer, int nSize, IntPtr[] arguments);

			// Token: 0x060015F5 RID: 5621
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern bool FreeLibrary(IntPtr hModule);

			// Token: 0x060015F6 RID: 5622
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int GetNumberOfEventLogRecords(IntPtr hEventLog, ref int NumberOfRecords);

			// Token: 0x060015F7 RID: 5623
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int GetOldestEventLogRecord(IntPtr hEventLog, ref int OldestRecord);

			// Token: 0x060015F8 RID: 5624
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, Win32EventLog.LoadFlags dwFlags);

			// Token: 0x060015F9 RID: 5625
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern IntPtr LocalFree(IntPtr hMem);

			// Token: 0x060015FA RID: 5626
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern bool LookupAccountSid(string lpSystemName, [MarshalAs(UnmanagedType.LPArray)] byte[] Sid, StringBuilder lpName, ref uint cchName, StringBuilder ReferencedDomainName, ref uint cchReferencedDomainName, out Win32EventLog.SidNameUse peUse);

			// Token: 0x060015FB RID: 5627
			[DllImport("Advapi32.dll", SetLastError = true)]
			public static extern int NotifyChangeEventLog(IntPtr hEventLog, IntPtr hEvent);

			// Token: 0x060015FC RID: 5628
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern IntPtr OpenEventLog(string machineName, string logName);

			// Token: 0x060015FD RID: 5629
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern IntPtr RegisterEventSource(string machineName, string sourceName);

			// Token: 0x060015FE RID: 5630
			[DllImport("Advapi32.dll", SetLastError = true)]
			public static extern int ReportEvent(IntPtr hHandle, ushort wType, ushort wCategory, uint dwEventID, IntPtr sid, ushort wNumStrings, uint dwDataSize, string[] lpStrings, byte[] lpRawData);

			// Token: 0x060015FF RID: 5631
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int ReadEventLog(IntPtr hEventLog, Win32EventLog.ReadFlags dwReadFlags, int dwRecordOffset, byte[] buffer, int nNumberOfBytesToRead, ref int pnBytesRead, ref int pnMinNumberOfBytesNeeded);

			// Token: 0x040006BC RID: 1724
			public const int ERROR_INSUFFICIENT_BUFFER = 122;

			// Token: 0x040006BD RID: 1725
			public const int ERROR_EVENTLOG_FILE_CHANGED = 1503;
		}

		// Token: 0x02000267 RID: 615
		private enum ReadFlags
		{
			// Token: 0x040006BF RID: 1727
			Sequential = 1,
			// Token: 0x040006C0 RID: 1728
			Seek,
			// Token: 0x040006C1 RID: 1729
			ForwardsRead = 4,
			// Token: 0x040006C2 RID: 1730
			BackwardsRead = 8
		}

		// Token: 0x02000268 RID: 616
		private enum LoadFlags : uint
		{
			// Token: 0x040006C4 RID: 1732
			LibraryAsDataFile = 2U
		}

		// Token: 0x02000269 RID: 617
		[Flags]
		private enum FormatMessageFlags
		{
			// Token: 0x040006C6 RID: 1734
			AllocateBuffer = 256,
			// Token: 0x040006C7 RID: 1735
			IgnoreInserts = 512,
			// Token: 0x040006C8 RID: 1736
			FromHModule = 2048,
			// Token: 0x040006C9 RID: 1737
			FromSystem = 4096,
			// Token: 0x040006CA RID: 1738
			ArgumentArray = 8192
		}

		// Token: 0x0200026A RID: 618
		private enum SidNameUse
		{
			// Token: 0x040006CC RID: 1740
			User = 1,
			// Token: 0x040006CD RID: 1741
			Group,
			// Token: 0x040006CE RID: 1742
			Domain,
			// Token: 0x040006CF RID: 1743
			lias,
			// Token: 0x040006D0 RID: 1744
			WellKnownGroup,
			// Token: 0x040006D1 RID: 1745
			DeletedAccount,
			// Token: 0x040006D2 RID: 1746
			Invalid,
			// Token: 0x040006D3 RID: 1747
			Unknown,
			// Token: 0x040006D4 RID: 1748
			Computer
		}
	}
}
