using System;
using System.Collections.Generic;
using System.IO;

namespace Boo.Lang.Runtime
{
	// Token: 0x02000040 RID: 64
	public class TextReaderEnumerator
	{
		// Token: 0x06000269 RID: 617 RVA: 0x000092BC File Offset: 0x000074BC
		public static IEnumerable<string> lines(TextReader reader)
		{
			try
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					yield return line;
				}
			}
			finally
			{
				if (reader != null)
				{
					reader.Dispose();
				}
			}
			yield break;
		}
	}
}
