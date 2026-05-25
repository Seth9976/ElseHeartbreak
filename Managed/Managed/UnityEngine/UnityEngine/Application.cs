using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000154 RID: 340
	public sealed class Application
	{
		// Token: 0x06000E27 RID: 3623
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Quit();

		// Token: 0x06000E28 RID: 3624
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CancelQuit();

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000E29 RID: 3625
		public static extern int loadedLevel
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000E2A RID: 3626
		public static extern string loadedLevelName
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0001E474 File Offset: 0x0001C674
		public static void LoadLevel(int index)
		{
			Application.LoadLevelAsync(null, index, false, true);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0001E480 File Offset: 0x0001C680
		public static void LoadLevel(string name)
		{
			Application.LoadLevelAsync(name, -1, false, true);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0001E48C File Offset: 0x0001C68C
		public static AsyncOperation LoadLevelAsync(int index)
		{
			return Application.LoadLevelAsync(null, index, false, false);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0001E498 File Offset: 0x0001C698
		public static AsyncOperation LoadLevelAsync(string levelName)
		{
			return Application.LoadLevelAsync(levelName, -1, false, false);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0001E4A4 File Offset: 0x0001C6A4
		public static AsyncOperation LoadLevelAdditiveAsync(int index)
		{
			return Application.LoadLevelAsync(null, index, true, false);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
		public static AsyncOperation LoadLevelAdditiveAsync(string levelName)
		{
			return Application.LoadLevelAsync(levelName, -1, true, false);
		}

		// Token: 0x06000E31 RID: 3633
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AsyncOperation LoadLevelAsync(string monoLevelName, int index, bool additive, bool mustCompleteNextFrame);

		// Token: 0x06000E32 RID: 3634 RVA: 0x0001E4BC File Offset: 0x0001C6BC
		public static void LoadLevelAdditive(int index)
		{
			Application.LoadLevelAsync(null, index, true, true);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0001E4C8 File Offset: 0x0001C6C8
		public static void LoadLevelAdditive(string name)
		{
			Application.LoadLevelAsync(name, -1, true, true);
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000E34 RID: 3636
		public static extern bool isLoadingLevel
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000E35 RID: 3637
		public static extern int levelCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000E36 RID: 3638
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetStreamProgressForLevelByName(string levelName);

		// Token: 0x06000E37 RID: 3639
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetStreamProgressForLevel(int levelIndex);

		// Token: 0x06000E38 RID: 3640 RVA: 0x0001E4D4 File Offset: 0x0001C6D4
		public static float GetStreamProgressForLevel(string levelName)
		{
			return Application.GetStreamProgressForLevelByName(levelName);
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000E39 RID: 3641
		public static extern int streamedBytes
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000E3A RID: 3642
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanStreamedLevelBeLoadedByName(string levelName);

		// Token: 0x06000E3B RID: 3643
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CanStreamedLevelBeLoaded(int levelIndex);

		// Token: 0x06000E3C RID: 3644 RVA: 0x0001E4DC File Offset: 0x0001C6DC
		public static bool CanStreamedLevelBeLoaded(string levelName)
		{
			return Application.CanStreamedLevelBeLoadedByName(levelName);
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000E3D RID: 3645
		public static extern bool isPlaying
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000E3E RID: 3646
		public static extern bool isEditor
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000E3F RID: 3647
		public static extern bool isWebPlayer
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000E40 RID: 3648
		public static extern RuntimePlatform platform
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0001E4E4 File Offset: 0x0001C6E4
		public static bool isMobilePlatform
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				return platform == RuntimePlatform.IPhonePlayer || platform == RuntimePlatform.Android || platform == RuntimePlatform.WP8Player || platform == RuntimePlatform.BB10Player || platform == RuntimePlatform.TizenPlayer;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x0001E520 File Offset: 0x0001C720
		public static bool isConsolePlatform
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				return platform == RuntimePlatform.PS3 || platform == RuntimePlatform.PS4 || platform == RuntimePlatform.XBOX360 || platform == RuntimePlatform.XboxOne;
			}
		}

		// Token: 0x06000E43 RID: 3651
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CaptureScreenshot(string filename, [DefaultValue("0")] int superSize);

		// Token: 0x06000E44 RID: 3652 RVA: 0x0001E554 File Offset: 0x0001C754
		[ExcludeFromDocs]
		public static void CaptureScreenshot(string filename)
		{
			int num = 0;
			Application.CaptureScreenshot(filename, num);
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000E45 RID: 3653
		// (set) Token: 0x06000E46 RID: 3654
		public static extern bool runInBackground
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0001E56C File Offset: 0x0001C76C
		[Obsolete("use Application.isEditor instead")]
		public static bool isPlayer
		{
			get
			{
				return !Application.isEditor;
			}
		}

		// Token: 0x06000E48 RID: 3656
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasProLicense();

		// Token: 0x06000E49 RID: 3657
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasAdvancedLicense();

		// Token: 0x06000E4A RID: 3658
		[Obsolete("Use Object.DontDestroyOnLoad instead")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DontDestroyOnLoad(Object mono);

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000E4B RID: 3659
		public static extern string dataPath
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000E4C RID: 3660
		public static extern string streamingAssetsPath
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000E4D RID: 3661
		[SecurityCritical]
		public static extern string persistentDataPath
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000E4E RID: 3662
		public static extern string temporaryCachePath
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000E4F RID: 3663
		public static extern string srcValue
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000E50 RID: 3664
		public static extern string absoluteURL
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0001E578 File Offset: 0x0001C778
		[Obsolete("Please use absoluteURL instead")]
		public static string absoluteUrl
		{
			get
			{
				return Application.absoluteURL;
			}
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0001E580 File Offset: 0x0001C780
		private static string ObjectToJSString(object o)
		{
			if (o == null)
			{
				return "null";
			}
			if (o is string)
			{
				string text = o.ToString().Replace("\\", "\\\\");
				text = text.Replace("\"", "\\\"");
				text = text.Replace("\n", "\\n");
				text = text.Replace("\r", "\\r");
				text = text.Replace("\0", string.Empty);
				text = text.Replace("\u2028", string.Empty);
				text = text.Replace("\u2029", string.Empty);
				return '"' + text + '"';
			}
			if (o is int || o is short || o is uint || o is ushort || o is byte)
			{
				return o.ToString();
			}
			if (o is float)
			{
				NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
				return ((float)o).ToString(numberFormat);
			}
			if (o is double)
			{
				NumberFormatInfo numberFormat2 = CultureInfo.InvariantCulture.NumberFormat;
				return ((double)o).ToString(numberFormat2);
			}
			if (o is char)
			{
				if ((char)o == '"')
				{
					return "\"\\\"\"";
				}
				return '"' + o.ToString() + '"';
			}
			else
			{
				if (o is IList)
				{
					IList list = (IList)o;
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("new Array(");
					int count = list.Count;
					for (int i = 0; i < count; i++)
					{
						if (i != 0)
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append(Application.ObjectToJSString(list[i]));
					}
					stringBuilder.Append(")");
					return stringBuilder.ToString();
				}
				return Application.ObjectToJSString(o.ToString());
			}
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0001E788 File Offset: 0x0001C988
		public static void ExternalCall(string functionName, params object[] args)
		{
			Application.Internal_ExternalCall(Application.BuildInvocationForArguments(functionName, args));
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0001E798 File Offset: 0x0001C998
		private static string BuildInvocationForArguments(string functionName, params object[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(functionName);
			stringBuilder.Append('(');
			int num = args.Length;
			for (int i = 0; i < num; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(Application.ObjectToJSString(args[i]));
			}
			stringBuilder.Append(')');
			stringBuilder.Append(';');
			return stringBuilder.ToString();
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0001E80C File Offset: 0x0001CA0C
		public static void ExternalEval(string script)
		{
			if (script.Length > 0 && script[script.Length - 1] != ';')
			{
				script += ';';
			}
			Application.Internal_ExternalCall(script);
		}

		// Token: 0x06000E56 RID: 3670
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ExternalCall(string script);

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000E57 RID: 3671
		public static extern string unityVersion
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000E58 RID: 3672
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetBuildUnityVersion();

		// Token: 0x06000E59 RID: 3673
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetNumericUnityVersion(string version);

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000E5A RID: 3674
		public static extern bool webSecurityEnabled
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000E5B RID: 3675
		public static extern string webSecurityHostUrl
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000E5C RID: 3676
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void OpenURL(string url);

		// Token: 0x06000E5D RID: 3677
		[Obsolete("For internal use only")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CommitSuicide(int mode);

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000E5E RID: 3678
		// (set) Token: 0x06000E5F RID: 3679
		public static extern int targetFrameRate
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000E60 RID: 3680
		public static extern SystemLanguage systemLanguage
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0001E850 File Offset: 0x0001CA50
		public static void RegisterLogCallback(Application.LogCallback handler)
		{
			Application.s_LogCallback = handler;
			Application.SetLogCallbackDefined(handler != null, false);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0001E868 File Offset: 0x0001CA68
		public static void RegisterLogCallbackThreaded(Application.LogCallback handler)
		{
			Application.s_LogCallback = handler;
			Application.SetLogCallbackDefined(handler != null, true);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0001E880 File Offset: 0x0001CA80
		private static void CallLogCallback(string logString, string stackTrace, LogType type)
		{
			if (Application.s_LogCallback != null)
			{
				Application.s_LogCallback(logString, stackTrace, type);
			}
		}

		// Token: 0x06000E64 RID: 3684
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLogCallbackDefined(bool defined, bool threaded);

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000E65 RID: 3685
		// (set) Token: 0x06000E66 RID: 3686
		public static extern ThreadPriority backgroundLoadingPriority
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000E67 RID: 3687
		public static extern NetworkReachability internetReachability
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000E68 RID: 3688
		public static extern bool genuine
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000E69 RID: 3689
		public static extern bool genuineCheckAvailable
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000E6A RID: 3690
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AsyncOperation RequestUserAuthorization(UserAuthorization mode);

		// Token: 0x06000E6B RID: 3691
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasUserAuthorization(UserAuthorization mode);

		// Token: 0x06000E6C RID: 3692
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReplyToUserAuthorizationRequest(bool reply, [DefaultValue("false")] bool remember);

		// Token: 0x06000E6D RID: 3693 RVA: 0x0001E8A0 File Offset: 0x0001CAA0
		[ExcludeFromDocs]
		internal static void ReplyToUserAuthorizationRequest(bool reply)
		{
			bool flag = false;
			Application.ReplyToUserAuthorizationRequest(reply, flag);
		}

		// Token: 0x06000E6E RID: 3694
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetUserAuthorizationRequestMode_Internal();

		// Token: 0x06000E6F RID: 3695 RVA: 0x0001E8B8 File Offset: 0x0001CAB8
		internal static UserAuthorization GetUserAuthorizationRequestMode()
		{
			return (UserAuthorization)Application.GetUserAuthorizationRequestMode_Internal();
		}

		// Token: 0x040005D4 RID: 1492
		private static volatile Application.LogCallback s_LogCallback;

		// Token: 0x0200022B RID: 555
		// (Invoke) Token: 0x06001AD1 RID: 6865
		public delegate void LogCallback(string condition, string stackTrace, LogType type);
	}
}
