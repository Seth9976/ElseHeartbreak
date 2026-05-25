using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.IO
{
	// Token: 0x02000290 RID: 656
	internal class KeventWatcher : IFileWatcher
	{
		// Token: 0x060016D6 RID: 5846 RVA: 0x0003EAEC File Offset: 0x0003CCEC
		private KeventWatcher()
		{
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x0003EAF4 File Offset: 0x0003CCF4
		public static bool GetInstance(out IFileWatcher watcher)
		{
			if (KeventWatcher.failed)
			{
				watcher = null;
				return false;
			}
			if (KeventWatcher.instance != null)
			{
				watcher = KeventWatcher.instance;
				return true;
			}
			KeventWatcher.watches = Hashtable.Synchronized(new Hashtable());
			KeventWatcher.requests = Hashtable.Synchronized(new Hashtable());
			KeventWatcher.conn = KeventWatcher.kqueue();
			if (KeventWatcher.conn == -1)
			{
				KeventWatcher.failed = true;
				watcher = null;
				return false;
			}
			KeventWatcher.instance = new KeventWatcher();
			watcher = KeventWatcher.instance;
			return true;
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x0003EB74 File Offset: 0x0003CD74
		public void StartDispatching(FileSystemWatcher fsw)
		{
			KeventData keventData;
			lock (this)
			{
				if (KeventWatcher.thread == null)
				{
					KeventWatcher.thread = new Thread(new ThreadStart(this.Monitor));
					KeventWatcher.thread.IsBackground = true;
					KeventWatcher.thread.Start();
				}
				keventData = (KeventData)KeventWatcher.watches[fsw];
			}
			if (keventData == null)
			{
				keventData = new KeventData();
				keventData.FSW = fsw;
				keventData.Directory = fsw.FullPath;
				keventData.FileMask = fsw.MangledFilter;
				keventData.IncludeSubdirs = fsw.IncludeSubdirectories;
				keventData.Enabled = true;
				lock (this)
				{
					KeventWatcher.StartMonitoringDirectory(keventData);
					KeventWatcher.watches[fsw] = keventData;
					KeventWatcher.stop = false;
				}
			}
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x0003EC7C File Offset: 0x0003CE7C
		private static void StartMonitoringDirectory(KeventData data)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(data.Directory);
			if (data.DirEntries == null)
			{
				data.DirEntries = new Hashtable();
				foreach (FileSystemInfo fileSystemInfo in directoryInfo.GetFileSystemInfos())
				{
					data.DirEntries.Add(fileSystemInfo.FullName, new KeventFileData(fileSystemInfo, fileSystemInfo.LastAccessTime, fileSystemInfo.LastWriteTime));
				}
			}
			int num = KeventWatcher.open(data.Directory, 0, 0);
			kevent kevent = default(kevent);
			kevent.udata = IntPtr.Zero;
			timespec timespec = default(timespec);
			timespec.tv_sec = 0;
			timespec.tv_usec = 0;
			if (num > 0)
			{
				kevent.ident = num;
				kevent.filter = -4;
				kevent.flags = 21;
				kevent.fflags = 31U;
				kevent.data = 0;
				kevent.udata = Marshal.StringToHGlobalAuto(data.Directory);
				kevent kevent2 = default(kevent);
				kevent2.udata = IntPtr.Zero;
				KeventWatcher.kevent(KeventWatcher.conn, ref kevent, 1, ref kevent2, 0, ref timespec);
				data.ev = kevent;
				KeventWatcher.requests[num] = data;
			}
			if (!data.IncludeSubdirs)
			{
				return;
			}
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x0003EDBC File Offset: 0x0003CFBC
		public void StopDispatching(FileSystemWatcher fsw)
		{
			lock (this)
			{
				KeventData keventData = (KeventData)KeventWatcher.watches[fsw];
				if (keventData != null)
				{
					KeventWatcher.StopMonitoringDirectory(keventData);
					KeventWatcher.watches.Remove(fsw);
					if (KeventWatcher.watches.Count == 0)
					{
						KeventWatcher.stop = true;
					}
					if (!keventData.IncludeSubdirs)
					{
					}
				}
			}
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x0003EE4C File Offset: 0x0003D04C
		private static void StopMonitoringDirectory(KeventData data)
		{
			KeventWatcher.close(data.ev.ident);
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x0003EE60 File Offset: 0x0003D060
		private void Monitor()
		{
			while (!KeventWatcher.stop)
			{
				kevent kevent = default(kevent);
				kevent.udata = IntPtr.Zero;
				kevent kevent2 = default(kevent);
				kevent2.udata = IntPtr.Zero;
				timespec timespec = default(timespec);
				timespec.tv_sec = 0;
				timespec.tv_usec = 0;
				int num;
				lock (this)
				{
					num = KeventWatcher.kevent(KeventWatcher.conn, ref kevent2, 0, ref kevent, 1, ref timespec);
				}
				if (num > 0)
				{
					KeventData keventData = (KeventData)KeventWatcher.requests[kevent.ident];
					KeventWatcher.StopMonitoringDirectory(keventData);
					KeventWatcher.StartMonitoringDirectory(keventData);
					this.ProcessEvent(kevent);
				}
				else
				{
					Thread.Sleep(500);
				}
			}
			lock (this)
			{
				KeventWatcher.thread = null;
				KeventWatcher.stop = false;
			}
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0003EF84 File Offset: 0x0003D184
		private void ProcessEvent(kevent ev)
		{
			lock (this)
			{
				KeventData keventData = (KeventData)KeventWatcher.requests[ev.ident];
				if (keventData.Enabled)
				{
					string text = string.Empty;
					FileSystemWatcher fsw = keventData.FSW;
					DirectoryInfo directoryInfo = new DirectoryInfo(keventData.Directory);
					FileSystemInfo fileSystemInfo = null;
					try
					{
						foreach (FileSystemInfo fileSystemInfo2 in directoryInfo.GetFileSystemInfos())
						{
							if (keventData.DirEntries.ContainsKey(fileSystemInfo2.FullName) && fileSystemInfo2 is FileInfo)
							{
								KeventFileData keventFileData = (KeventFileData)keventData.DirEntries[fileSystemInfo2.FullName];
								if (keventFileData.LastWriteTime != fileSystemInfo2.LastWriteTime)
								{
									text = fileSystemInfo2.Name;
									FileAction fileAction = FileAction.Modified;
									keventData.DirEntries[fileSystemInfo2.FullName] = new KeventFileData(fileSystemInfo2, fileSystemInfo2.LastAccessTime, fileSystemInfo2.LastWriteTime);
									if (fsw.IncludeSubdirectories && fileSystemInfo2 is DirectoryInfo)
									{
										keventData.Directory = text;
										KeventWatcher.requests[ev.ident] = keventData;
										this.ProcessEvent(ev);
									}
									this.PostEvent(text, fsw, fileAction, fileSystemInfo);
								}
							}
						}
					}
					catch (Exception)
					{
					}
					try
					{
						bool flag = true;
						while (flag)
						{
							foreach (object obj in keventData.DirEntries.Values)
							{
								KeventFileData keventFileData2 = (KeventFileData)obj;
								if (!File.Exists(keventFileData2.fsi.FullName) && !Directory.Exists(keventFileData2.fsi.FullName))
								{
									text = keventFileData2.fsi.Name;
									FileAction fileAction = FileAction.Removed;
									keventData.DirEntries.Remove(keventFileData2.fsi.FullName);
									this.PostEvent(text, fsw, fileAction, fileSystemInfo);
									break;
								}
							}
							flag = false;
						}
					}
					catch (Exception)
					{
					}
					try
					{
						foreach (FileSystemInfo fileSystemInfo3 in directoryInfo.GetFileSystemInfos())
						{
							if (!keventData.DirEntries.ContainsKey(fileSystemInfo3.FullName))
							{
								fileSystemInfo = fileSystemInfo3;
								text = fileSystemInfo3.Name;
								FileAction fileAction = FileAction.Added;
								keventData.DirEntries[fileSystemInfo3.FullName] = new KeventFileData(fileSystemInfo3, fileSystemInfo3.LastAccessTime, fileSystemInfo3.LastWriteTime);
								this.PostEvent(text, fsw, fileAction, fileSystemInfo);
							}
						}
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0003F2CC File Offset: 0x0003D4CC
		private void PostEvent(string filename, FileSystemWatcher fsw, FileAction fa, FileSystemInfo changedFsi)
		{
			RenamedEventArgs renamedEventArgs = null;
			if (fa == (FileAction)0)
			{
				return;
			}
			if (fsw.IncludeSubdirectories && fa == FileAction.Added && changedFsi is DirectoryInfo)
			{
				KeventData keventData = new KeventData();
				keventData.FSW = fsw;
				keventData.Directory = changedFsi.FullName;
				keventData.FileMask = fsw.MangledFilter;
				keventData.IncludeSubdirs = fsw.IncludeSubdirectories;
				keventData.Enabled = true;
				lock (this)
				{
					KeventWatcher.StartMonitoringDirectory(keventData);
				}
			}
			if (!fsw.Pattern.IsMatch(filename, true))
			{
				return;
			}
			lock (fsw)
			{
				fsw.DispatchEvents(fa, filename, ref renamedEventArgs);
				if (fsw.Waiting)
				{
					fsw.Waiting = false;
					global::System.Threading.Monitor.PulseAll(fsw);
				}
			}
		}

		// Token: 0x060016DF RID: 5855
		[DllImport("libc")]
		private static extern int open(string path, int flags, int mode_t);

		// Token: 0x060016E0 RID: 5856
		[DllImport("libc")]
		private static extern int close(int fd);

		// Token: 0x060016E1 RID: 5857
		[DllImport("libc")]
		private static extern int kqueue();

		// Token: 0x060016E2 RID: 5858
		[DllImport("libc")]
		private static extern int kevent(int kqueue, ref kevent ev, int nchanges, ref kevent evtlist, int nevents, ref timespec ts);

		// Token: 0x04000781 RID: 1921
		private static bool failed;

		// Token: 0x04000782 RID: 1922
		private static KeventWatcher instance;

		// Token: 0x04000783 RID: 1923
		private static Hashtable watches;

		// Token: 0x04000784 RID: 1924
		private static Hashtable requests;

		// Token: 0x04000785 RID: 1925
		private static Thread thread;

		// Token: 0x04000786 RID: 1926
		private static int conn;

		// Token: 0x04000787 RID: 1927
		private static bool stop;
	}
}
