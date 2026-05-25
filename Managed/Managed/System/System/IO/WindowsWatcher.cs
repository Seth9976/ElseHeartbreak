using System;

namespace System.IO
{
	// Token: 0x020002AC RID: 684
	internal class WindowsWatcher : IFileWatcher
	{
		// Token: 0x060017C5 RID: 6085 RVA: 0x000414C4 File Offset: 0x0003F6C4
		private WindowsWatcher()
		{
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x000414CC File Offset: 0x0003F6CC
		public static bool GetInstance(out IFileWatcher watcher)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x000414D4 File Offset: 0x0003F6D4
		public void StartDispatching(FileSystemWatcher fsw)
		{
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x000414D8 File Offset: 0x0003F6D8
		public void StopDispatching(FileSystemWatcher fsw)
		{
		}
	}
}
