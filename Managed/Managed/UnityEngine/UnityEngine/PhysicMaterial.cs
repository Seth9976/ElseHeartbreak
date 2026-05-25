using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200019D RID: 413
	public sealed class PhysicMaterial : Object
	{
		// Token: 0x06001393 RID: 5011 RVA: 0x000218E0 File Offset: 0x0001FAE0
		public PhysicMaterial()
		{
			PhysicMaterial.Internal_CreateDynamicsMaterial(this, null);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x000218F0 File Offset: 0x0001FAF0
		public PhysicMaterial(string name)
		{
			PhysicMaterial.Internal_CreateDynamicsMaterial(this, name);
		}

		// Token: 0x06001395 RID: 5013
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateDynamicsMaterial([Writable] PhysicMaterial mat, string name);

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001396 RID: 5014
		// (set) Token: 0x06001397 RID: 5015
		public extern float dynamicFriction
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001398 RID: 5016
		// (set) Token: 0x06001399 RID: 5017
		public extern float staticFriction
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600139A RID: 5018
		// (set) Token: 0x0600139B RID: 5019
		public extern float bounciness
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x00021900 File Offset: 0x0001FB00
		// (set) Token: 0x0600139D RID: 5021 RVA: 0x00021908 File Offset: 0x0001FB08
		[Obsolete("Use PhysicMaterial.bounciness instead", true)]
		public float bouncyness
		{
			get
			{
				return this.bounciness;
			}
			set
			{
				this.bounciness = value;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x00021914 File Offset: 0x0001FB14
		// (set) Token: 0x0600139F RID: 5023 RVA: 0x0002192C File Offset: 0x0001FB2C
		public Vector3 frictionDirection2
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_frictionDirection2(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_frictionDirection2(ref value);
			}
		}

		// Token: 0x060013A0 RID: 5024
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_frictionDirection2(out Vector3 value);

		// Token: 0x060013A1 RID: 5025
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_frictionDirection2(ref Vector3 value);

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060013A2 RID: 5026
		// (set) Token: 0x060013A3 RID: 5027
		public extern float dynamicFriction2
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060013A4 RID: 5028
		// (set) Token: 0x060013A5 RID: 5029
		public extern float staticFriction2
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060013A6 RID: 5030
		// (set) Token: 0x060013A7 RID: 5031
		public extern PhysicMaterialCombine frictionCombine
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060013A8 RID: 5032
		// (set) Token: 0x060013A9 RID: 5033
		public extern PhysicMaterialCombine bounceCombine
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00021938 File Offset: 0x0001FB38
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x00021940 File Offset: 0x0001FB40
		[Obsolete("use PhysicMaterial.frictionDirection2 instead.")]
		public Vector3 frictionDirection
		{
			get
			{
				return this.frictionDirection2;
			}
			set
			{
				this.frictionDirection2 = value;
			}
		}
	}
}
