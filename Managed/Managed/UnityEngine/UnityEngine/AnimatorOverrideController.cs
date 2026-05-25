using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001E7 RID: 487
	public sealed class AnimatorOverrideController : RuntimeAnimatorController
	{
		// Token: 0x0600178C RID: 6028 RVA: 0x00023B84 File Offset: 0x00021D84
		public AnimatorOverrideController()
		{
			AnimatorOverrideController.Internal_CreateAnimationSet(this);
		}

		// Token: 0x0600178D RID: 6029
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateAnimationSet([Writable] AnimatorOverrideController self);

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x0600178E RID: 6030
		// (set) Token: 0x0600178F RID: 6031
		public extern RuntimeAnimatorController runtimeAnimatorController
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000625 RID: 1573
		public AnimationClip this[string name]
		{
			get
			{
				return this.Internal_GetClipByName(name, true);
			}
			set
			{
				this.Internal_SetClipByName(name, value);
			}
		}

		// Token: 0x06001792 RID: 6034
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationClip Internal_GetClipByName(string name, bool returnEffectiveClip);

		// Token: 0x06001793 RID: 6035
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetClipByName(string name, AnimationClip clip);

		// Token: 0x17000626 RID: 1574
		public AnimationClip this[AnimationClip clip]
		{
			get
			{
				return this.Internal_GetClip(clip, true);
			}
			set
			{
				this.Internal_SetClip(clip, value);
			}
		}

		// Token: 0x06001796 RID: 6038
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationClip Internal_GetClip(AnimationClip originalClip, bool returnEffectiveClip);

		// Token: 0x06001797 RID: 6039
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetClip(AnimationClip originalClip, AnimationClip overrideClip);

		// Token: 0x06001798 RID: 6040
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool ShouldHideDuplicateOriginalClips();

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001799 RID: 6041 RVA: 0x00023BC4 File Offset: 0x00021DC4
		// (set) Token: 0x0600179A RID: 6042 RVA: 0x00023C80 File Offset: 0x00021E80
		public AnimationClipPair[] clips
		{
			get
			{
				AnimationClip[] array = this.GetOriginalClips();
				if (this.ShouldHideDuplicateOriginalClips())
				{
					Dictionary<AnimationClip, bool> dictionary = new Dictionary<AnimationClip, bool>(array.Length);
					foreach (AnimationClip animationClip in array)
					{
						dictionary[animationClip] = true;
					}
					array = new AnimationClip[dictionary.Count];
					dictionary.Keys.CopyTo(array, 0);
				}
				AnimationClipPair[] array3 = new AnimationClipPair[array.Length];
				for (int j = 0; j < array.Length; j++)
				{
					array3[j] = new AnimationClipPair();
					array3[j].originalClip = array[j];
					array3[j].overrideClip = this.Internal_GetClip(array[j], false);
				}
				return array3;
			}
			set
			{
				for (int i = 0; i < value.Length; i++)
				{
					this.Internal_SetClip(value[i].originalClip, value[i].overrideClip);
				}
			}
		}

		// Token: 0x0600179B RID: 6043
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationClip[] GetOriginalClips();

		// Token: 0x0600179C RID: 6044
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationClip[] GetOverrideClips();
	}
}
