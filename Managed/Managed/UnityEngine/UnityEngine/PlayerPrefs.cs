using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200017A RID: 378
	public sealed class PlayerPrefs
	{
		// Token: 0x0600114D RID: 4429
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySetInt(string key, int value);

		// Token: 0x0600114E RID: 4430
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySetFloat(string key, float value);

		// Token: 0x0600114F RID: 4431
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySetSetString(string key, string value);

		// Token: 0x06001150 RID: 4432 RVA: 0x00020288 File Offset: 0x0001E488
		public static void SetInt(string key, int value)
		{
			if (!PlayerPrefs.TrySetInt(key, value))
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x06001151 RID: 4433
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetInt(string key, [DefaultValue("0")] int defaultValue);

		// Token: 0x06001152 RID: 4434 RVA: 0x000202A4 File Offset: 0x0001E4A4
		[ExcludeFromDocs]
		public static int GetInt(string key)
		{
			int num = 0;
			return PlayerPrefs.GetInt(key, num);
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x000202BC File Offset: 0x0001E4BC
		public static void SetFloat(string key, float value)
		{
			if (!PlayerPrefs.TrySetFloat(key, value))
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x06001154 RID: 4436
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetFloat(string key, [DefaultValue("0.0F")] float defaultValue);

		// Token: 0x06001155 RID: 4437 RVA: 0x000202D8 File Offset: 0x0001E4D8
		[ExcludeFromDocs]
		public static float GetFloat(string key)
		{
			float num = 0f;
			return PlayerPrefs.GetFloat(key, num);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000202F4 File Offset: 0x0001E4F4
		public static void SetString(string key, string value)
		{
			if (!PlayerPrefs.TrySetSetString(key, value))
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x06001157 RID: 4439
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetString(string key, [DefaultValue("\"\"")] string defaultValue);

		// Token: 0x06001158 RID: 4440 RVA: 0x00020310 File Offset: 0x0001E510
		[ExcludeFromDocs]
		public static string GetString(string key)
		{
			string empty = string.Empty;
			return PlayerPrefs.GetString(key, empty);
		}

		// Token: 0x06001159 RID: 4441
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasKey(string key);

		// Token: 0x0600115A RID: 4442
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteKey(string key);

		// Token: 0x0600115B RID: 4443
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteAll();

		// Token: 0x0600115C RID: 4444
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Save();
	}
}
