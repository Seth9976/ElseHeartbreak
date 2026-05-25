using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000131 RID: 305
	public sealed class ParticleSystem : Component
	{
		// Token: 0x06000CAE RID: 3246
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Collider InstanceIDToCollider(int instanceID);

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000CAF RID: 3247
		// (set) Token: 0x06000CB0 RID: 3248
		public extern float startDelay
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000CB1 RID: 3249
		public extern bool isPlaying
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000CB2 RID: 3250
		public extern bool isStopped
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000CB3 RID: 3251
		public extern bool isPaused
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000CB4 RID: 3252
		// (set) Token: 0x06000CB5 RID: 3253
		public extern bool loop
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000CB6 RID: 3254
		// (set) Token: 0x06000CB7 RID: 3255
		public extern bool playOnAwake
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000CB8 RID: 3256
		// (set) Token: 0x06000CB9 RID: 3257
		public extern float time
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000CBA RID: 3258
		public extern float duration
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000CBB RID: 3259
		// (set) Token: 0x06000CBC RID: 3260
		public extern float playbackSpeed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000CBD RID: 3261
		public extern int particleCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000CBE RID: 3262
		public extern int safeCollisionEventSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000CBF RID: 3263
		// (set) Token: 0x06000CC0 RID: 3264
		public extern bool enableEmission
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000CC1 RID: 3265
		// (set) Token: 0x06000CC2 RID: 3266
		public extern float emissionRate
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000CC3 RID: 3267
		// (set) Token: 0x06000CC4 RID: 3268
		public extern float startSpeed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000CC5 RID: 3269
		// (set) Token: 0x06000CC6 RID: 3270
		public extern float startSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0001CD6C File Offset: 0x0001AF6C
		// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x0001CD84 File Offset: 0x0001AF84
		public Color startColor
		{
			get
			{
				Color color;
				this.INTERNAL_get_startColor(out color);
				return color;
			}
			set
			{
				this.INTERNAL_set_startColor(ref value);
			}
		}

		// Token: 0x06000CC9 RID: 3273
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_startColor(out Color value);

		// Token: 0x06000CCA RID: 3274
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_startColor(ref Color value);

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000CCB RID: 3275
		// (set) Token: 0x06000CCC RID: 3276
		public extern float startRotation
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000CCD RID: 3277
		// (set) Token: 0x06000CCE RID: 3278
		public extern float startLifetime
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000CCF RID: 3279
		// (set) Token: 0x06000CD0 RID: 3280
		public extern float gravityModifier
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000CD1 RID: 3281
		// (set) Token: 0x06000CD2 RID: 3282
		public extern int maxParticles
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000CD3 RID: 3283
		// (set) Token: 0x06000CD4 RID: 3284
		public extern ParticleSystemSimulationSpace simulationSpace
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000CD5 RID: 3285
		// (set) Token: 0x06000CD6 RID: 3286
		public extern uint randomSeed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000CD7 RID: 3287
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetParticles(ParticleSystem.Particle[] particles, int size);

		// Token: 0x06000CD8 RID: 3288
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetParticles(ParticleSystem.Particle[] particles);

		// Token: 0x06000CD9 RID: 3289
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetCollisionEvents(GameObject go, ParticleSystem.CollisionEvent[] collisionEvents);

		// Token: 0x06000CDA RID: 3290
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_Simulate(float t, bool restart);

		// Token: 0x06000CDB RID: 3291
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_Play();

		// Token: 0x06000CDC RID: 3292
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_Stop();

		// Token: 0x06000CDD RID: 3293
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_Pause();

		// Token: 0x06000CDE RID: 3294
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_Clear();

		// Token: 0x06000CDF RID: 3295
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_IsAlive();

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0001CD90 File Offset: 0x0001AF90
		[ExcludeFromDocs]
		public void Simulate(float t, bool withChildren)
		{
			bool flag = true;
			this.Simulate(t, withChildren, flag);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0001CDA8 File Offset: 0x0001AFA8
		[ExcludeFromDocs]
		public void Simulate(float t)
		{
			bool flag = true;
			bool flag2 = true;
			this.Simulate(t, flag2, flag);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0001CDC4 File Offset: 0x0001AFC4
		public void Simulate(float t, [DefaultValue("true")] bool withChildren, [DefaultValue("true")] bool restart)
		{
			if (withChildren)
			{
				ParticleSystem[] particleSystems = ParticleSystem.GetParticleSystems(this);
				foreach (ParticleSystem particleSystem in particleSystems)
				{
					particleSystem.Internal_Simulate(t, restart);
				}
			}
			else
			{
				this.Internal_Simulate(t, restart);
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0001CE10 File Offset: 0x0001B010
		[ExcludeFromDocs]
		public void Play()
		{
			bool flag = true;
			this.Play(flag);
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0001CE28 File Offset: 0x0001B028
		public void Play([DefaultValue("true")] bool withChildren)
		{
			if (withChildren)
			{
				ParticleSystem[] particleSystems = ParticleSystem.GetParticleSystems(this);
				foreach (ParticleSystem particleSystem in particleSystems)
				{
					particleSystem.Internal_Play();
				}
			}
			else
			{
				this.Internal_Play();
			}
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0001CE70 File Offset: 0x0001B070
		[ExcludeFromDocs]
		public void Stop()
		{
			bool flag = true;
			this.Stop(flag);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0001CE88 File Offset: 0x0001B088
		public void Stop([DefaultValue("true")] bool withChildren)
		{
			if (withChildren)
			{
				ParticleSystem[] particleSystems = ParticleSystem.GetParticleSystems(this);
				foreach (ParticleSystem particleSystem in particleSystems)
				{
					particleSystem.Internal_Stop();
				}
			}
			else
			{
				this.Internal_Stop();
			}
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0001CED0 File Offset: 0x0001B0D0
		[ExcludeFromDocs]
		public void Pause()
		{
			bool flag = true;
			this.Pause(flag);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0001CEE8 File Offset: 0x0001B0E8
		public void Pause([DefaultValue("true")] bool withChildren)
		{
			if (withChildren)
			{
				ParticleSystem[] particleSystems = ParticleSystem.GetParticleSystems(this);
				foreach (ParticleSystem particleSystem in particleSystems)
				{
					particleSystem.Internal_Pause();
				}
			}
			else
			{
				this.Internal_Pause();
			}
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0001CF30 File Offset: 0x0001B130
		[ExcludeFromDocs]
		public void Clear()
		{
			bool flag = true;
			this.Clear(flag);
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0001CF48 File Offset: 0x0001B148
		public void Clear([DefaultValue("true")] bool withChildren)
		{
			if (withChildren)
			{
				ParticleSystem[] particleSystems = ParticleSystem.GetParticleSystems(this);
				foreach (ParticleSystem particleSystem in particleSystems)
				{
					particleSystem.Internal_Clear();
				}
			}
			else
			{
				this.Internal_Clear();
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0001CF90 File Offset: 0x0001B190
		[ExcludeFromDocs]
		public bool IsAlive()
		{
			bool flag = true;
			return this.IsAlive(flag);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0001CFA8 File Offset: 0x0001B1A8
		public bool IsAlive([DefaultValue("true")] bool withChildren)
		{
			if (withChildren)
			{
				ParticleSystem[] particleSystems = ParticleSystem.GetParticleSystems(this);
				foreach (ParticleSystem particleSystem in particleSystems)
				{
					if (particleSystem.Internal_IsAlive())
					{
						return true;
					}
				}
				return false;
			}
			return this.Internal_IsAlive();
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0001CFF4 File Offset: 0x0001B1F4
		public void Emit(int count)
		{
			ParticleSystem.INTERNAL_CALL_Emit(this, count);
		}

		// Token: 0x06000CEE RID: 3310
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Emit(ParticleSystem self, int count);

		// Token: 0x06000CEF RID: 3311 RVA: 0x0001D000 File Offset: 0x0001B200
		public void Emit(Vector3 position, Vector3 velocity, float size, float lifetime, Color32 color)
		{
			ParticleSystem.Particle particle = default(ParticleSystem.Particle);
			particle.position = position;
			particle.velocity = velocity;
			particle.lifetime = lifetime;
			particle.startLifetime = lifetime;
			particle.size = size;
			particle.rotation = 0f;
			particle.angularVelocity = 0f;
			particle.color = color;
			particle.randomSeed = 5U;
			this.Internal_Emit(ref particle);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0001D070 File Offset: 0x0001B270
		public void Emit(ParticleSystem.Particle particle)
		{
			this.Internal_Emit(ref particle);
		}

		// Token: 0x06000CF1 RID: 3313
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_Emit(ref ParticleSystem.Particle particle);

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0001D07C File Offset: 0x0001B27C
		internal static ParticleSystem[] GetParticleSystems(ParticleSystem root)
		{
			if (!root)
			{
				return null;
			}
			List<ParticleSystem> list = new List<ParticleSystem>();
			list.Add(root);
			ParticleSystem.GetDirectParticleSystemChildrenRecursive(root.transform, list);
			return list.ToArray();
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0001D0B8 File Offset: 0x0001B2B8
		private static void GetDirectParticleSystemChildrenRecursive(Transform transform, List<ParticleSystem> particleSystems)
		{
			foreach (object obj in transform)
			{
				Transform transform2 = (Transform)obj;
				ParticleSystem component = transform2.gameObject.GetComponent<ParticleSystem>();
				if (component != null)
				{
					particleSystems.Add(component);
					ParticleSystem.GetDirectParticleSystemChildrenRecursive(transform2, particleSystems);
				}
			}
		}

		// Token: 0x02000132 RID: 306
		public struct Particle
		{
			// Token: 0x170002E6 RID: 742
			// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0001D144 File Offset: 0x0001B344
			// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x0001D14C File Offset: 0x0001B34C
			public Vector3 position
			{
				get
				{
					return this.m_Position;
				}
				set
				{
					this.m_Position = value;
				}
			}

			// Token: 0x170002E7 RID: 743
			// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0001D158 File Offset: 0x0001B358
			// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x0001D160 File Offset: 0x0001B360
			public Vector3 velocity
			{
				get
				{
					return this.m_Velocity;
				}
				set
				{
					this.m_Velocity = value;
				}
			}

			// Token: 0x170002E8 RID: 744
			// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0001D16C File Offset: 0x0001B36C
			// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x0001D174 File Offset: 0x0001B374
			public float lifetime
			{
				get
				{
					return this.m_Lifetime;
				}
				set
				{
					this.m_Lifetime = value;
				}
			}

			// Token: 0x170002E9 RID: 745
			// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0001D180 File Offset: 0x0001B380
			// (set) Token: 0x06000CFB RID: 3323 RVA: 0x0001D188 File Offset: 0x0001B388
			public float startLifetime
			{
				get
				{
					return this.m_StartLifetime;
				}
				set
				{
					this.m_StartLifetime = value;
				}
			}

			// Token: 0x170002EA RID: 746
			// (get) Token: 0x06000CFC RID: 3324 RVA: 0x0001D194 File Offset: 0x0001B394
			// (set) Token: 0x06000CFD RID: 3325 RVA: 0x0001D19C File Offset: 0x0001B39C
			public float size
			{
				get
				{
					return this.m_Size;
				}
				set
				{
					this.m_Size = value;
				}
			}

			// Token: 0x170002EB RID: 747
			// (get) Token: 0x06000CFE RID: 3326 RVA: 0x0001D1A8 File Offset: 0x0001B3A8
			// (set) Token: 0x06000CFF RID: 3327 RVA: 0x0001D1B0 File Offset: 0x0001B3B0
			public Vector3 axisOfRotation
			{
				get
				{
					return this.m_AxisOfRotation;
				}
				set
				{
					this.m_AxisOfRotation = value;
				}
			}

			// Token: 0x170002EC RID: 748
			// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0001D1BC File Offset: 0x0001B3BC
			// (set) Token: 0x06000D01 RID: 3329 RVA: 0x0001D1CC File Offset: 0x0001B3CC
			public float rotation
			{
				get
				{
					return this.m_Rotation * 57.29578f;
				}
				set
				{
					this.m_Rotation = value * 0.017453292f;
				}
			}

			// Token: 0x170002ED RID: 749
			// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0001D1DC File Offset: 0x0001B3DC
			// (set) Token: 0x06000D03 RID: 3331 RVA: 0x0001D1EC File Offset: 0x0001B3EC
			public float angularVelocity
			{
				get
				{
					return this.m_AngularVelocity * 57.29578f;
				}
				set
				{
					this.m_AngularVelocity = value * 0.017453292f;
				}
			}

			// Token: 0x170002EE RID: 750
			// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0001D1FC File Offset: 0x0001B3FC
			// (set) Token: 0x06000D05 RID: 3333 RVA: 0x0001D204 File Offset: 0x0001B404
			public Color32 color
			{
				get
				{
					return this.m_Color;
				}
				set
				{
					this.m_Color = value;
				}
			}

			// Token: 0x170002EF RID: 751
			// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0001D210 File Offset: 0x0001B410
			// (set) Token: 0x06000D07 RID: 3335 RVA: 0x0001D224 File Offset: 0x0001B424
			[Obsolete("randomValue property is deprecated. Use randomSeed instead to control random behavior of particles.")]
			public float randomValue
			{
				get
				{
					return BitConverter.ToSingle(BitConverter.GetBytes(this.m_RandomSeed), 0);
				}
				set
				{
					this.m_RandomSeed = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
				}
			}

			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0001D238 File Offset: 0x0001B438
			// (set) Token: 0x06000D09 RID: 3337 RVA: 0x0001D240 File Offset: 0x0001B440
			public uint randomSeed
			{
				get
				{
					return this.m_RandomSeed;
				}
				set
				{
					this.m_RandomSeed = value;
				}
			}

			// Token: 0x0400056B RID: 1387
			private Vector3 m_Position;

			// Token: 0x0400056C RID: 1388
			private Vector3 m_Velocity;

			// Token: 0x0400056D RID: 1389
			private Vector3 m_AnimatedVelocity;

			// Token: 0x0400056E RID: 1390
			private Vector3 m_AxisOfRotation;

			// Token: 0x0400056F RID: 1391
			private float m_Rotation;

			// Token: 0x04000570 RID: 1392
			private float m_AngularVelocity;

			// Token: 0x04000571 RID: 1393
			private float m_Size;

			// Token: 0x04000572 RID: 1394
			private Color32 m_Color;

			// Token: 0x04000573 RID: 1395
			private uint m_RandomSeed;

			// Token: 0x04000574 RID: 1396
			private float m_Lifetime;

			// Token: 0x04000575 RID: 1397
			private float m_StartLifetime;

			// Token: 0x04000576 RID: 1398
			private float m_EmitAccumulator0;

			// Token: 0x04000577 RID: 1399
			private float m_EmitAccumulator1;
		}

		// Token: 0x02000133 RID: 307
		public struct CollisionEvent
		{
			// Token: 0x170002F1 RID: 753
			// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0001D24C File Offset: 0x0001B44C
			public Vector3 intersection
			{
				get
				{
					return this.m_Intersection;
				}
			}

			// Token: 0x170002F2 RID: 754
			// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0001D254 File Offset: 0x0001B454
			public Vector3 normal
			{
				get
				{
					return this.m_Normal;
				}
			}

			// Token: 0x170002F3 RID: 755
			// (get) Token: 0x06000D0C RID: 3340 RVA: 0x0001D25C File Offset: 0x0001B45C
			public Vector3 velocity
			{
				get
				{
					return this.m_Velocity;
				}
			}

			// Token: 0x170002F4 RID: 756
			// (get) Token: 0x06000D0D RID: 3341 RVA: 0x0001D264 File Offset: 0x0001B464
			public Collider collider
			{
				get
				{
					return ParticleSystem.InstanceIDToCollider(this.m_ColliderInstanceID);
				}
			}

			// Token: 0x04000578 RID: 1400
			private Vector3 m_Intersection;

			// Token: 0x04000579 RID: 1401
			private Vector3 m_Normal;

			// Token: 0x0400057A RID: 1402
			private Vector3 m_Velocity;

			// Token: 0x0400057B RID: 1403
			private int m_ColliderInstanceID;
		}
	}
}
