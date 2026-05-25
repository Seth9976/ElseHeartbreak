using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Provides a set of properties that are carried with the execution code path. This class cannot be inherited.</summary>
	// Token: 0x0200048F RID: 1167
	[ComVisible(true)]
	[Serializable]
	public sealed class CallContext
	{
		// Token: 0x06002F94 RID: 12180 RVA: 0x0009D57C File Offset: 0x0009B77C
		private CallContext()
		{
		}

		/// <summary>Gets or sets the host context associated with the current thread.</summary>
		/// <returns>The host context associated with the current thread.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06002F95 RID: 12181 RVA: 0x0009D584 File Offset: 0x0009B784
		// (set) Token: 0x06002F96 RID: 12182 RVA: 0x0009D58C File Offset: 0x0009B78C
		public static object HostContext
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

		/// <summary>Empties a data slot with the specified name.</summary>
		/// <param name="name">The name of the data slot to empty. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002F97 RID: 12183 RVA: 0x0009D594 File Offset: 0x0009B794
		public static void FreeNamedDataSlot(string name)
		{
			CallContext.Datastore.Remove(name);
		}

		/// <summary>Retrieves an object with the specified name from the <see cref="T:System.Runtime.Remoting.Messaging.CallContext" />.</summary>
		/// <returns>The object in the call context associated with the specified name.</returns>
		/// <param name="name">The name of the item in the call context. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002F98 RID: 12184 RVA: 0x0009D5A4 File Offset: 0x0009B7A4
		public static object GetData(string name)
		{
			return CallContext.Datastore[name];
		}

		/// <summary>Stores a given object and associates it with the specified name.</summary>
		/// <param name="name">The name with which to associate the new item in the call context. </param>
		/// <param name="data">The object to store in the call context. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002F99 RID: 12185 RVA: 0x0009D5B4 File Offset: 0x0009B7B4
		public static void SetData(string name, object data)
		{
			CallContext.Datastore[name] = data;
		}

		/// <summary>Retrieves an object with the specified name from the logical call context.</summary>
		/// <returns>The object in the logical call context associated with the specified name.</returns>
		/// <param name="name">The name of the item in the logical call context. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x06002F9A RID: 12186 RVA: 0x0009D5C4 File Offset: 0x0009B7C4
		[MonoTODO]
		public static object LogicalGetData(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Stores a given object in the logical call context and associates it with the specified name.</summary>
		/// <param name="name">The name with which to associate the new item in the logical call context. </param>
		/// <param name="data">The object to store in the logical call context. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x06002F9B RID: 12187 RVA: 0x0009D5CC File Offset: 0x0009B7CC
		[MonoTODO]
		public static void LogicalSetData(string name, object data)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the headers that are sent along with the method call.</summary>
		/// <returns>The headers that are sent along with the method call.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002F9C RID: 12188 RVA: 0x0009D5D4 File Offset: 0x0009B7D4
		public static Header[] GetHeaders()
		{
			return CallContext.Headers;
		}

		/// <summary>Sets the headers that are sent along with the method call.</summary>
		/// <param name="headers">A <see cref="T:System.Runtime.Remoting.Messaging.Header" /> array of the headers that are to be sent along with the method call. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002F9D RID: 12189 RVA: 0x0009D5DC File Offset: 0x0009B7DC
		public static void SetHeaders(Header[] headers)
		{
			CallContext.Headers = headers;
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x0009D5E4 File Offset: 0x0009B7E4
		internal static LogicalCallContext CreateLogicalCallContext(bool createEmpty)
		{
			LogicalCallContext logicalCallContext = null;
			if (CallContext.datastore != null)
			{
				foreach (object obj in CallContext.datastore)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (dictionaryEntry.Value is ILogicalThreadAffinative)
					{
						if (logicalCallContext == null)
						{
							logicalCallContext = new LogicalCallContext();
						}
						logicalCallContext.SetData((string)dictionaryEntry.Key, dictionaryEntry.Value);
					}
				}
			}
			if (logicalCallContext == null && createEmpty)
			{
				return new LogicalCallContext();
			}
			return logicalCallContext;
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x0009D6A0 File Offset: 0x0009B8A0
		internal static object SetCurrentCallContext(LogicalCallContext ctx)
		{
			object obj = CallContext.datastore;
			if (ctx != null && ctx.HasInfo)
			{
				CallContext.datastore = (Hashtable)ctx.Datastore.Clone();
			}
			else
			{
				CallContext.datastore = null;
			}
			return obj;
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x0009D6E8 File Offset: 0x0009B8E8
		internal static void UpdateCurrentCallContext(LogicalCallContext ctx)
		{
			Hashtable hashtable = ctx.Datastore;
			foreach (object obj in hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				CallContext.SetData((string)dictionaryEntry.Key, dictionaryEntry.Value);
			}
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x0009D76C File Offset: 0x0009B96C
		internal static void RestoreCallContext(object oldContext)
		{
			CallContext.datastore = (Hashtable)oldContext;
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x0009D77C File Offset: 0x0009B97C
		private static Hashtable Datastore
		{
			get
			{
				Hashtable hashtable = CallContext.datastore;
				if (hashtable == null)
				{
					return CallContext.datastore = new Hashtable();
				}
				return hashtable;
			}
		}

		// Token: 0x04001447 RID: 5191
		[ThreadStatic]
		private static Header[] Headers;

		// Token: 0x04001448 RID: 5192
		[ThreadStatic]
		private static Hashtable datastore;
	}
}
