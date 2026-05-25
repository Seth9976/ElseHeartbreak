using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000B7 RID: 183
	public sealed class ParticleEmitter : Component
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000469 RID: 1129
		// (set) Token: 0x0600046A RID: 1130
		public extern bool emit
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600046B RID: 1131
		// (set) Token: 0x0600046C RID: 1132
		public extern float minSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600046D RID: 1133
		// (set) Token: 0x0600046E RID: 1134
		public extern float maxSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600046F RID: 1135
		// (set) Token: 0x06000470 RID: 1136
		public extern float minEnergy
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000471 RID: 1137
		// (set) Token: 0x06000472 RID: 1138
		public extern float maxEnergy
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000473 RID: 1139
		// (set) Token: 0x06000474 RID: 1140
		public extern float minEmission
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000475 RID: 1141
		// (set) Token: 0x06000476 RID: 1142
		public extern float maxEmission
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000477 RID: 1143
		// (set) Token: 0x06000478 RID: 1144
		public extern float emitterVelocityScale
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000BC00 File Offset: 0x00009E00
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0000BC18 File Offset: 0x00009E18
		public Vector3 worldVelocity
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_worldVelocity(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_worldVelocity(ref value);
			}
		}

		// Token: 0x0600047B RID: 1147
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_worldVelocity(out Vector3 value);

		// Token: 0x0600047C RID: 1148
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_worldVelocity(ref Vector3 value);

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000BC24 File Offset: 0x00009E24
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0000BC3C File Offset: 0x00009E3C
		public Vector3 localVelocity
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_localVelocity(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_localVelocity(ref value);
			}
		}

		// Token: 0x0600047F RID: 1151
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localVelocity(out Vector3 value);

		// Token: 0x06000480 RID: 1152
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localVelocity(ref Vector3 value);

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000BC48 File Offset: 0x00009E48
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0000BC60 File Offset: 0x00009E60
		public Vector3 rndVelocity
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_rndVelocity(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_rndVelocity(ref value);
			}
		}

		// Token: 0x06000483 RID: 1155
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rndVelocity(out Vector3 value);

		// Token: 0x06000484 RID: 1156
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_rndVelocity(ref Vector3 value);

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000485 RID: 1157
		// (set) Token: 0x06000486 RID: 1158
		public extern bool useWorldSpace
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000487 RID: 1159
		// (set) Token: 0x06000488 RID: 1160
		public extern bool rndRotation
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000489 RID: 1161
		// (set) Token: 0x0600048A RID: 1162
		public extern float angularVelocity
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600048B RID: 1163
		// (set) Token: 0x0600048C RID: 1164
		public extern float rndAngularVelocity
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600048D RID: 1165
		// (set) Token: 0x0600048E RID: 1166
		public extern Particle[] particles
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600048F RID: 1167
		public extern int particleCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000BC6C File Offset: 0x00009E6C
		public void ClearParticles()
		{
			ParticleEmitter.INTERNAL_CALL_ClearParticles(this);
		}

		// Token: 0x06000491 RID: 1169
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ClearParticles(ParticleEmitter self);

		// Token: 0x06000492 RID: 1170 RVA: 0x0000BC74 File Offset: 0x00009E74
		public void Emit()
		{
			this.Emit2((int)Random.Range(this.minEmission, this.maxEmission));
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000BC90 File Offset: 0x00009E90
		public void Emit(int count)
		{
			this.Emit2(count);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000BC9C File Offset: 0x00009E9C
		public void Emit(Vector3 pos, Vector3 velocity, float size, float energy, Color color)
		{
			InternalEmitParticleArguments internalEmitParticleArguments = default(InternalEmitParticleArguments);
			internalEmitParticleArguments.pos = pos;
			internalEmitParticleArguments.velocity = velocity;
			internalEmitParticleArguments.size = size;
			internalEmitParticleArguments.energy = energy;
			internalEmitParticleArguments.color = color;
			internalEmitParticleArguments.rotation = 0f;
			internalEmitParticleArguments.angularVelocity = 0f;
			this.Emit3(ref internalEmitParticleArguments);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000BCFC File Offset: 0x00009EFC
		public void Emit(Vector3 pos, Vector3 velocity, float size, float energy, Color color, float rotation, float angularVelocity)
		{
			InternalEmitParticleArguments internalEmitParticleArguments = default(InternalEmitParticleArguments);
			internalEmitParticleArguments.pos = pos;
			internalEmitParticleArguments.velocity = velocity;
			internalEmitParticleArguments.size = size;
			internalEmitParticleArguments.energy = energy;
			internalEmitParticleArguments.color = color;
			internalEmitParticleArguments.rotation = rotation;
			internalEmitParticleArguments.angularVelocity = angularVelocity;
			this.Emit3(ref internalEmitParticleArguments);
		}

		// Token: 0x06000496 RID: 1174
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Emit2(int count);

		// Token: 0x06000497 RID: 1175
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Emit3(ref InternalEmitParticleArguments args);

		// Token: 0x06000498 RID: 1176
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Simulate(float deltaTime);

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000499 RID: 1177
		// (set) Token: 0x0600049A RID: 1178
		public extern bool enabled
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
