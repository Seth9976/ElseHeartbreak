using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000129 RID: 297
	public sealed class Network
	{
		// Token: 0x06000C14 RID: 3092
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern NetworkConnectionError InitializeServer(int connections, int listenPort, bool useNat);

		// Token: 0x06000C15 RID: 3093
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NetworkConnectionError Internal_InitializeServerDeprecated(int connections, int listenPort);

		// Token: 0x06000C16 RID: 3094 RVA: 0x0001C838 File Offset: 0x0001AA38
		[Obsolete("Use the IntializeServer(connections, listenPort, useNat) function instead")]
		public static NetworkConnectionError InitializeServer(int connections, int listenPort)
		{
			return Network.Internal_InitializeServerDeprecated(connections, listenPort);
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000C17 RID: 3095
		// (set) Token: 0x06000C18 RID: 3096
		public static extern string incomingPassword
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000C19 RID: 3097
		// (set) Token: 0x06000C1A RID: 3098
		public static extern NetworkLogLevel logLevel
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000C1B RID: 3099
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void InitializeSecurity();

		// Token: 0x06000C1C RID: 3100
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NetworkConnectionError Internal_ConnectToSingleIP(string IP, int remotePort, int localPort, [DefaultValue("\"\"")] string password);

		// Token: 0x06000C1D RID: 3101 RVA: 0x0001C844 File Offset: 0x0001AA44
		[ExcludeFromDocs]
		private static NetworkConnectionError Internal_ConnectToSingleIP(string IP, int remotePort, int localPort)
		{
			string empty = string.Empty;
			return Network.Internal_ConnectToSingleIP(IP, remotePort, localPort, empty);
		}

		// Token: 0x06000C1E RID: 3102
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NetworkConnectionError Internal_ConnectToGuid(string guid, string password);

		// Token: 0x06000C1F RID: 3103
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NetworkConnectionError Internal_ConnectToIPs(string[] IP, int remotePort, int localPort, [DefaultValue("\"\"")] string password);

		// Token: 0x06000C20 RID: 3104 RVA: 0x0001C860 File Offset: 0x0001AA60
		[ExcludeFromDocs]
		private static NetworkConnectionError Internal_ConnectToIPs(string[] IP, int remotePort, int localPort)
		{
			string empty = string.Empty;
			return Network.Internal_ConnectToIPs(IP, remotePort, localPort, empty);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0001C87C File Offset: 0x0001AA7C
		[ExcludeFromDocs]
		public static NetworkConnectionError Connect(string IP, int remotePort)
		{
			string empty = string.Empty;
			return Network.Connect(IP, remotePort, empty);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0001C898 File Offset: 0x0001AA98
		public static NetworkConnectionError Connect(string IP, int remotePort, [DefaultValue("\"\"")] string password)
		{
			return Network.Internal_ConnectToSingleIP(IP, remotePort, 0, password);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0001C8A4 File Offset: 0x0001AAA4
		[ExcludeFromDocs]
		public static NetworkConnectionError Connect(string[] IPs, int remotePort)
		{
			string empty = string.Empty;
			return Network.Connect(IPs, remotePort, empty);
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0001C8C0 File Offset: 0x0001AAC0
		public static NetworkConnectionError Connect(string[] IPs, int remotePort, [DefaultValue("\"\"")] string password)
		{
			return Network.Internal_ConnectToIPs(IPs, remotePort, 0, password);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0001C8CC File Offset: 0x0001AACC
		[ExcludeFromDocs]
		public static NetworkConnectionError Connect(string GUID)
		{
			string empty = string.Empty;
			return Network.Connect(GUID, empty);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0001C8E8 File Offset: 0x0001AAE8
		public static NetworkConnectionError Connect(string GUID, [DefaultValue("\"\"")] string password)
		{
			return Network.Internal_ConnectToGuid(GUID, password);
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0001C8F4 File Offset: 0x0001AAF4
		[ExcludeFromDocs]
		public static NetworkConnectionError Connect(HostData hostData)
		{
			string empty = string.Empty;
			return Network.Connect(hostData, empty);
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0001C910 File Offset: 0x0001AB10
		public static NetworkConnectionError Connect(HostData hostData, [DefaultValue("\"\"")] string password)
		{
			if (hostData == null)
			{
				throw new NullReferenceException();
			}
			if (hostData.guid.Length > 0 && hostData.useNat)
			{
				return Network.Connect(hostData.guid, password);
			}
			return Network.Connect(hostData.ip, hostData.port, password);
		}

		// Token: 0x06000C29 RID: 3113
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Disconnect([DefaultValue("200")] int timeout);

		// Token: 0x06000C2A RID: 3114 RVA: 0x0001C964 File Offset: 0x0001AB64
		[ExcludeFromDocs]
		public static void Disconnect()
		{
			int num = 200;
			Network.Disconnect(num);
		}

		// Token: 0x06000C2B RID: 3115
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CloseConnection(NetworkPlayer target, bool sendDisconnectionNotification);

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000C2C RID: 3116
		public static extern NetworkPlayer[] connections
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000C2D RID: 3117
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetPlayer();

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0001C980 File Offset: 0x0001AB80
		public static NetworkPlayer player
		{
			get
			{
				NetworkPlayer networkPlayer;
				networkPlayer.index = Network.Internal_GetPlayer();
				return networkPlayer;
			}
		}

		// Token: 0x06000C2F RID: 3119
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_AllocateViewID(out NetworkViewID viewID);

		// Token: 0x06000C30 RID: 3120 RVA: 0x0001C99C File Offset: 0x0001AB9C
		public static NetworkViewID AllocateViewID()
		{
			NetworkViewID networkViewID;
			Network.Internal_AllocateViewID(out networkViewID);
			return networkViewID;
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0001C9B4 File Offset: 0x0001ABB4
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object prefab, Vector3 position, Quaternion rotation, int group)
		{
			return Network.INTERNAL_CALL_Instantiate(prefab, ref position, ref rotation, group);
		}

		// Token: 0x06000C32 RID: 3122
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object INTERNAL_CALL_Instantiate(Object prefab, ref Vector3 position, ref Quaternion rotation, int group);

		// Token: 0x06000C33 RID: 3123 RVA: 0x0001C9C4 File Offset: 0x0001ABC4
		public static void Destroy(NetworkViewID viewID)
		{
			Network.INTERNAL_CALL_Destroy(ref viewID);
		}

		// Token: 0x06000C34 RID: 3124
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Destroy(ref NetworkViewID viewID);

		// Token: 0x06000C35 RID: 3125 RVA: 0x0001C9D0 File Offset: 0x0001ABD0
		public static void Destroy(GameObject gameObject)
		{
			if (gameObject != null)
			{
				NetworkView networkView = gameObject.networkView;
				if (networkView != null)
				{
					Network.Destroy(networkView.viewID);
				}
				else
				{
					Debug.LogError("Couldn't destroy game object because no network view is attached to it.", gameObject);
				}
			}
		}

		// Token: 0x06000C36 RID: 3126
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DestroyPlayerObjects(NetworkPlayer playerID);

		// Token: 0x06000C37 RID: 3127 RVA: 0x0001CA18 File Offset: 0x0001AC18
		private static void Internal_RemoveRPCs(NetworkPlayer playerID, NetworkViewID viewID, uint channelMask)
		{
			Network.INTERNAL_CALL_Internal_RemoveRPCs(playerID, ref viewID, channelMask);
		}

		// Token: 0x06000C38 RID: 3128
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_RemoveRPCs(NetworkPlayer playerID, ref NetworkViewID viewID, uint channelMask);

		// Token: 0x06000C39 RID: 3129 RVA: 0x0001CA24 File Offset: 0x0001AC24
		public static void RemoveRPCs(NetworkPlayer playerID)
		{
			Network.Internal_RemoveRPCs(playerID, NetworkViewID.unassigned, uint.MaxValue);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0001CA34 File Offset: 0x0001AC34
		public static void RemoveRPCs(NetworkPlayer playerID, int group)
		{
			Network.Internal_RemoveRPCs(playerID, NetworkViewID.unassigned, 1U << group);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0001CA48 File Offset: 0x0001AC48
		public static void RemoveRPCs(NetworkViewID viewID)
		{
			Network.Internal_RemoveRPCs(NetworkPlayer.unassigned, viewID, uint.MaxValue);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0001CA58 File Offset: 0x0001AC58
		public static void RemoveRPCsInGroup(int group)
		{
			Network.Internal_RemoveRPCs(NetworkPlayer.unassigned, NetworkViewID.unassigned, 1U << group);
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000C3D RID: 3133
		public static extern bool isClient
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000C3E RID: 3134
		public static extern bool isServer
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000C3F RID: 3135
		public static extern NetworkPeerType peerType
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000C40 RID: 3136
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLevelPrefix(int prefix);

		// Token: 0x06000C41 RID: 3137
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetLastPing(NetworkPlayer player);

		// Token: 0x06000C42 RID: 3138
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetAveragePing(NetworkPlayer player);

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000C43 RID: 3139
		// (set) Token: 0x06000C44 RID: 3140
		public static extern float sendRate
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000C45 RID: 3141
		// (set) Token: 0x06000C46 RID: 3142
		public static extern bool isMessageQueueRunning
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000C47 RID: 3143
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetReceivingEnabled(NetworkPlayer player, int group, bool enabled);

		// Token: 0x06000C48 RID: 3144
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetSendingGlobal(int group, bool enabled);

		// Token: 0x06000C49 RID: 3145
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetSendingSpecific(NetworkPlayer player, int group, bool enabled);

		// Token: 0x06000C4A RID: 3146 RVA: 0x0001CA70 File Offset: 0x0001AC70
		public static void SetSendingEnabled(int group, bool enabled)
		{
			Network.Internal_SetSendingGlobal(group, enabled);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0001CA7C File Offset: 0x0001AC7C
		public static void SetSendingEnabled(NetworkPlayer player, int group, bool enabled)
		{
			Network.Internal_SetSendingSpecific(player, group, enabled);
		}

		// Token: 0x06000C4C RID: 3148
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTime(out double t);

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x0001CA88 File Offset: 0x0001AC88
		public static double time
		{
			get
			{
				double num;
				Network.Internal_GetTime(out num);
				return num;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000C4E RID: 3150
		// (set) Token: 0x06000C4F RID: 3151
		public static extern int minimumAllocatableViewIDs
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000C50 RID: 3152
		// (set) Token: 0x06000C51 RID: 3153
		[Obsolete("No longer needed. This is now explicitly set in the InitializeServer function call. It is implicitly set when calling Connect depending on if an IP/port combination is used (useNat=false) or a GUID is used(useNat=true).")]
		public static extern bool useNat
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000C52 RID: 3154
		// (set) Token: 0x06000C53 RID: 3155
		public static extern string natFacilitatorIP
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000C54 RID: 3156
		// (set) Token: 0x06000C55 RID: 3157
		public static extern int natFacilitatorPort
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000C56 RID: 3158
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ConnectionTesterStatus TestConnection([DefaultValue("false")] bool forceTest);

		// Token: 0x06000C57 RID: 3159 RVA: 0x0001CAA0 File Offset: 0x0001ACA0
		[ExcludeFromDocs]
		public static ConnectionTesterStatus TestConnection()
		{
			bool flag = false;
			return Network.TestConnection(flag);
		}

		// Token: 0x06000C58 RID: 3160
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ConnectionTesterStatus TestConnectionNAT([DefaultValue("false")] bool forceTest);

		// Token: 0x06000C59 RID: 3161 RVA: 0x0001CAB8 File Offset: 0x0001ACB8
		[ExcludeFromDocs]
		public static ConnectionTesterStatus TestConnectionNAT()
		{
			bool flag = false;
			return Network.TestConnectionNAT(flag);
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000C5A RID: 3162
		// (set) Token: 0x06000C5B RID: 3163
		public static extern string connectionTesterIP
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000C5C RID: 3164
		// (set) Token: 0x06000C5D RID: 3165
		public static extern int connectionTesterPort
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000C5E RID: 3166
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HavePublicAddress();

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000C5F RID: 3167
		// (set) Token: 0x06000C60 RID: 3168
		public static extern int maxConnections
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000C61 RID: 3169
		// (set) Token: 0x06000C62 RID: 3170
		public static extern string proxyIP
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000C63 RID: 3171
		// (set) Token: 0x06000C64 RID: 3172
		public static extern int proxyPort
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000C65 RID: 3173
		// (set) Token: 0x06000C66 RID: 3174
		public static extern bool useProxy
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000C67 RID: 3175
		// (set) Token: 0x06000C68 RID: 3176
		public static extern string proxyPassword
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
