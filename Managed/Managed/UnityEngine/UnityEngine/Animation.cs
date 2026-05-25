using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020001F2 RID: 498
	public sealed class Animation : Behaviour, IEnumerable
	{
		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060017ED RID: 6125
		// (set) Token: 0x060017EE RID: 6126
		public extern AnimationClip clip
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060017EF RID: 6127
		// (set) Token: 0x060017F0 RID: 6128
		public extern bool playAutomatically
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060017F1 RID: 6129
		// (set) Token: 0x060017F2 RID: 6130
		public extern WrapMode wrapMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00023F58 File Offset: 0x00022158
		public void Stop()
		{
			Animation.INTERNAL_CALL_Stop(this);
		}

		// Token: 0x060017F4 RID: 6132
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Stop(Animation self);

		// Token: 0x060017F5 RID: 6133 RVA: 0x00023F60 File Offset: 0x00022160
		public void Stop(string name)
		{
			this.Internal_StopByName(name);
		}

		// Token: 0x060017F6 RID: 6134
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_StopByName(string name);

		// Token: 0x060017F7 RID: 6135 RVA: 0x00023F6C File Offset: 0x0002216C
		public void Rewind(string name)
		{
			this.Internal_RewindByName(name);
		}

		// Token: 0x060017F8 RID: 6136
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_RewindByName(string name);

		// Token: 0x060017F9 RID: 6137 RVA: 0x00023F78 File Offset: 0x00022178
		public void Rewind()
		{
			Animation.INTERNAL_CALL_Rewind(this);
		}

		// Token: 0x060017FA RID: 6138
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Rewind(Animation self);

		// Token: 0x060017FB RID: 6139 RVA: 0x00023F80 File Offset: 0x00022180
		public void Sample()
		{
			Animation.INTERNAL_CALL_Sample(this);
		}

		// Token: 0x060017FC RID: 6140
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Sample(Animation self);

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060017FD RID: 6141
		public extern bool isPlaying
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060017FE RID: 6142
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsPlaying(string name);

		// Token: 0x17000645 RID: 1605
		public AnimationState this[string name]
		{
			get
			{
				return this.GetState(name);
			}
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x00023F94 File Offset: 0x00022194
		[ExcludeFromDocs]
		public bool Play()
		{
			PlayMode playMode = PlayMode.StopSameLayer;
			return this.Play(playMode);
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x00023FAC File Offset: 0x000221AC
		public bool Play([DefaultValue("PlayMode.StopSameLayer")] PlayMode mode)
		{
			return this.PlayDefaultAnimation(mode);
		}

		// Token: 0x06001802 RID: 6146
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Play(string animation, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x06001803 RID: 6147 RVA: 0x00023FB8 File Offset: 0x000221B8
		[ExcludeFromDocs]
		public bool Play(string animation)
		{
			PlayMode playMode = PlayMode.StopSameLayer;
			return this.Play(animation, playMode);
		}

		// Token: 0x06001804 RID: 6148
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CrossFade(string animation, [DefaultValue("0.3F")] float fadeLength, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x06001805 RID: 6149 RVA: 0x00023FD0 File Offset: 0x000221D0
		[ExcludeFromDocs]
		public void CrossFade(string animation, float fadeLength)
		{
			PlayMode playMode = PlayMode.StopSameLayer;
			this.CrossFade(animation, fadeLength, playMode);
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x00023FE8 File Offset: 0x000221E8
		[ExcludeFromDocs]
		public void CrossFade(string animation)
		{
			PlayMode playMode = PlayMode.StopSameLayer;
			float num = 0.3f;
			this.CrossFade(animation, num, playMode);
		}

		// Token: 0x06001807 RID: 6151
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Blend(string animation, [DefaultValue("1.0F")] float targetWeight, [DefaultValue("0.3F")] float fadeLength);

		// Token: 0x06001808 RID: 6152 RVA: 0x00024008 File Offset: 0x00022208
		[ExcludeFromDocs]
		public void Blend(string animation, float targetWeight)
		{
			float num = 0.3f;
			this.Blend(animation, targetWeight, num);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x00024024 File Offset: 0x00022224
		[ExcludeFromDocs]
		public void Blend(string animation)
		{
			float num = 0.3f;
			float num2 = 1f;
			this.Blend(animation, num2, num);
		}

		// Token: 0x0600180A RID: 6154
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimationState CrossFadeQueued(string animation, [DefaultValue("0.3F")] float fadeLength, [DefaultValue("QueueMode.CompleteOthers")] QueueMode queue, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x0600180B RID: 6155 RVA: 0x00024048 File Offset: 0x00022248
		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation, float fadeLength, QueueMode queue)
		{
			PlayMode playMode = PlayMode.StopSameLayer;
			return this.CrossFadeQueued(animation, fadeLength, queue, playMode);
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00024064 File Offset: 0x00022264
		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation, float fadeLength)
		{
			PlayMode playMode = PlayMode.StopSameLayer;
			QueueMode queueMode = QueueMode.CompleteOthers;
			return this.CrossFadeQueued(animation, fadeLength, queueMode, playMode);
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x00024080 File Offset: 0x00022280
		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation)
		{
			PlayMode playMode = PlayMode.StopSameLayer;
			QueueMode queueMode = QueueMode.CompleteOthers;
			float num = 0.3f;
			return this.CrossFadeQueued(animation, num, queueMode, playMode);
		}

		// Token: 0x0600180E RID: 6158
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimationState PlayQueued(string animation, [DefaultValue("QueueMode.CompleteOthers")] QueueMode queue, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x0600180F RID: 6159 RVA: 0x000240A4 File Offset: 0x000222A4
		[ExcludeFromDocs]
		public AnimationState PlayQueued(string animation, QueueMode queue)
		{
			PlayMode playMode = PlayMode.StopSameLayer;
			return this.PlayQueued(animation, queue, playMode);
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x000240BC File Offset: 0x000222BC
		[ExcludeFromDocs]
		public AnimationState PlayQueued(string animation)
		{
			PlayMode playMode = PlayMode.StopSameLayer;
			QueueMode queueMode = QueueMode.CompleteOthers;
			return this.PlayQueued(animation, queueMode, playMode);
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x000240D8 File Offset: 0x000222D8
		public void AddClip(AnimationClip clip, string newName)
		{
			this.AddClip(clip, newName, int.MinValue, int.MaxValue);
		}

		// Token: 0x06001812 RID: 6162
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddClip(AnimationClip clip, string newName, int firstFrame, int lastFrame, [DefaultValue("false")] bool addLoopFrame);

		// Token: 0x06001813 RID: 6163 RVA: 0x000240EC File Offset: 0x000222EC
		[ExcludeFromDocs]
		public void AddClip(AnimationClip clip, string newName, int firstFrame, int lastFrame)
		{
			bool flag = false;
			this.AddClip(clip, newName, firstFrame, lastFrame, flag);
		}

		// Token: 0x06001814 RID: 6164
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveClip(AnimationClip clip);

		// Token: 0x06001815 RID: 6165 RVA: 0x00024108 File Offset: 0x00022308
		public void RemoveClip(string clipName)
		{
			this.RemoveClip2(clipName);
		}

		// Token: 0x06001816 RID: 6166
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetClipCount();

		// Token: 0x06001817 RID: 6167
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RemoveClip2(string clipName);

		// Token: 0x06001818 RID: 6168
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool PlayDefaultAnimation(PlayMode mode);

		// Token: 0x06001819 RID: 6169 RVA: 0x00024114 File Offset: 0x00022314
		[Obsolete("use PlayMode instead of AnimationPlayMode.")]
		public bool Play(AnimationPlayMode mode)
		{
			return this.PlayDefaultAnimation((PlayMode)mode);
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00024120 File Offset: 0x00022320
		[Obsolete("use PlayMode instead of AnimationPlayMode.")]
		public bool Play(string animation, AnimationPlayMode mode)
		{
			return this.Play(animation, (PlayMode)mode);
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0002412C File Offset: 0x0002232C
		public void SyncLayer(int layer)
		{
			Animation.INTERNAL_CALL_SyncLayer(this, layer);
		}

		// Token: 0x0600181C RID: 6172
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SyncLayer(Animation self, int layer);

		// Token: 0x0600181D RID: 6173 RVA: 0x00024138 File Offset: 0x00022338
		public IEnumerator GetEnumerator()
		{
			return new Animation.Enumerator(this);
		}

		// Token: 0x0600181E RID: 6174
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern AnimationState GetState(string name);

		// Token: 0x0600181F RID: 6175
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern AnimationState GetStateAtIndex(int index);

		// Token: 0x06001820 RID: 6176
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetStateCount();

		// Token: 0x06001821 RID: 6177 RVA: 0x00024140 File Offset: 0x00022340
		public AnimationClip GetClip(string name)
		{
			AnimationState state = this.GetState(name);
			if (state)
			{
				return state.clip;
			}
			return null;
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001822 RID: 6178
		// (set) Token: 0x06001823 RID: 6179
		public extern bool animatePhysics
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001824 RID: 6180
		// (set) Token: 0x06001825 RID: 6181
		[Obsolete("Use cullingType instead")]
		public extern bool animateOnlyIfVisible
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001826 RID: 6182
		// (set) Token: 0x06001827 RID: 6183
		public extern AnimationCullingType cullingType
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x00024168 File Offset: 0x00022368
		// (set) Token: 0x06001829 RID: 6185 RVA: 0x00024180 File Offset: 0x00022380
		public Bounds localBounds
		{
			get
			{
				Bounds bounds;
				this.INTERNAL_get_localBounds(out bounds);
				return bounds;
			}
			set
			{
				this.INTERNAL_set_localBounds(ref value);
			}
		}

		// Token: 0x0600182A RID: 6186
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localBounds(out Bounds value);

		// Token: 0x0600182B RID: 6187
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localBounds(ref Bounds value);

		// Token: 0x020001F3 RID: 499
		private sealed class Enumerator : IEnumerator
		{
			// Token: 0x0600182C RID: 6188 RVA: 0x0002418C File Offset: 0x0002238C
			internal Enumerator(Animation outer)
			{
				this.m_Outer = outer;
			}

			// Token: 0x1700064A RID: 1610
			// (get) Token: 0x0600182D RID: 6189 RVA: 0x000241A4 File Offset: 0x000223A4
			public object Current
			{
				get
				{
					return this.m_Outer.GetStateAtIndex(this.m_CurrentIndex);
				}
			}

			// Token: 0x0600182E RID: 6190 RVA: 0x000241B8 File Offset: 0x000223B8
			public bool MoveNext()
			{
				int stateCount = this.m_Outer.GetStateCount();
				this.m_CurrentIndex++;
				return this.m_CurrentIndex < stateCount;
			}

			// Token: 0x0600182F RID: 6191 RVA: 0x000241E8 File Offset: 0x000223E8
			public void Reset()
			{
				this.m_CurrentIndex = -1;
			}

			// Token: 0x04000749 RID: 1865
			private Animation m_Outer;

			// Token: 0x0400074A RID: 1866
			private int m_CurrentIndex = -1;
		}
	}
}
