using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200012A RID: 298
	public sealed class BitStream
	{
		// Token: 0x06000C6A RID: 3178
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serializeb(ref int value);

		// Token: 0x06000C6B RID: 3179
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serializec(ref char value);

		// Token: 0x06000C6C RID: 3180
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serializes(ref short value);

		// Token: 0x06000C6D RID: 3181
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serializei(ref int value);

		// Token: 0x06000C6E RID: 3182
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serializef(ref float value, float maximumDelta);

		// Token: 0x06000C6F RID: 3183 RVA: 0x0001CAD8 File Offset: 0x0001ACD8
		private void Serializeq(ref Quaternion value, float maximumDelta)
		{
			BitStream.INTERNAL_CALL_Serializeq(this, ref value, maximumDelta);
		}

		// Token: 0x06000C70 RID: 3184
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Serializeq(BitStream self, ref Quaternion value, float maximumDelta);

		// Token: 0x06000C71 RID: 3185 RVA: 0x0001CAE4 File Offset: 0x0001ACE4
		private void Serializev(ref Vector3 value, float maximumDelta)
		{
			BitStream.INTERNAL_CALL_Serializev(this, ref value, maximumDelta);
		}

		// Token: 0x06000C72 RID: 3186
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Serializev(BitStream self, ref Vector3 value, float maximumDelta);

		// Token: 0x06000C73 RID: 3187 RVA: 0x0001CAF0 File Offset: 0x0001ACF0
		private void Serializen(ref NetworkViewID viewID)
		{
			BitStream.INTERNAL_CALL_Serializen(this, ref viewID);
		}

		// Token: 0x06000C74 RID: 3188
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Serializen(BitStream self, ref NetworkViewID viewID);

		// Token: 0x06000C75 RID: 3189 RVA: 0x0001CAFC File Offset: 0x0001ACFC
		public void Serialize(ref bool value)
		{
			int num = ((!value) ? 0 : 1);
			this.Serializeb(ref num);
			value = num != 0;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0001CB30 File Offset: 0x0001AD30
		public void Serialize(ref char value)
		{
			this.Serializec(ref value);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0001CB3C File Offset: 0x0001AD3C
		public void Serialize(ref short value)
		{
			this.Serializes(ref value);
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0001CB48 File Offset: 0x0001AD48
		public void Serialize(ref int value)
		{
			this.Serializei(ref value);
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0001CB54 File Offset: 0x0001AD54
		[ExcludeFromDocs]
		public void Serialize(ref float value)
		{
			float num = 1E-05f;
			this.Serialize(ref value, num);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0001CB70 File Offset: 0x0001AD70
		public void Serialize(ref float value, [DefaultValue("0.00001F")] float maxDelta)
		{
			this.Serializef(ref value, maxDelta);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0001CB7C File Offset: 0x0001AD7C
		[ExcludeFromDocs]
		public void Serialize(ref Quaternion value)
		{
			float num = 1E-05f;
			this.Serialize(ref value, num);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0001CB98 File Offset: 0x0001AD98
		public void Serialize(ref Quaternion value, [DefaultValue("0.00001F")] float maxDelta)
		{
			this.Serializeq(ref value, maxDelta);
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0001CBA4 File Offset: 0x0001ADA4
		[ExcludeFromDocs]
		public void Serialize(ref Vector3 value)
		{
			float num = 1E-05f;
			this.Serialize(ref value, num);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0001CBC0 File Offset: 0x0001ADC0
		public void Serialize(ref Vector3 value, [DefaultValue("0.00001F")] float maxDelta)
		{
			this.Serializev(ref value, maxDelta);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0001CBCC File Offset: 0x0001ADCC
		public void Serialize(ref NetworkPlayer value)
		{
			int index = value.index;
			this.Serializei(ref index);
			value.index = index;
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0001CBF0 File Offset: 0x0001ADF0
		public void Serialize(ref NetworkViewID viewID)
		{
			this.Serializen(ref viewID);
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000C81 RID: 3201
		public extern bool isReading
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000C82 RID: 3202
		public extern bool isWriting
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000C83 RID: 3203
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serialize(ref string value);

		// Token: 0x04000554 RID: 1364
		internal IntPtr m_Ptr;
	}
}
