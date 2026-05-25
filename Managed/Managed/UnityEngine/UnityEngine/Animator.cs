using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020001FD RID: 509
	public sealed class Animator : Behaviour
	{
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x0600185F RID: 6239
		public extern bool isOptimizable
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001860 RID: 6240
		public extern bool isHuman
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001861 RID: 6241
		public extern bool hasRootMotion
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001862 RID: 6242
		public extern float humanScale
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x00024320 File Offset: 0x00022520
		public float GetFloat(string name)
		{
			return this.GetFloatString(name);
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x0002432C File Offset: 0x0002252C
		public float GetFloat(int id)
		{
			return this.GetFloatID(id);
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x00024338 File Offset: 0x00022538
		public void SetFloat(string name, float value)
		{
			this.SetFloatString(name, value);
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x00024344 File Offset: 0x00022544
		public void SetFloat(string name, float value, float dampTime, float deltaTime)
		{
			this.SetFloatStringDamp(name, value, dampTime, deltaTime);
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x00024354 File Offset: 0x00022554
		public void SetFloat(int id, float value)
		{
			this.SetFloatID(id, value);
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x00024360 File Offset: 0x00022560
		public void SetFloat(int id, float value, float dampTime, float deltaTime)
		{
			this.SetFloatIDDamp(id, value, dampTime, deltaTime);
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00024370 File Offset: 0x00022570
		public bool GetBool(string name)
		{
			return this.GetBoolString(name);
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0002437C File Offset: 0x0002257C
		public bool GetBool(int id)
		{
			return this.GetBoolID(id);
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x00024388 File Offset: 0x00022588
		public void SetBool(string name, bool value)
		{
			this.SetBoolString(name, value);
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00024394 File Offset: 0x00022594
		public void SetBool(int id, bool value)
		{
			this.SetBoolID(id, value);
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x000243A0 File Offset: 0x000225A0
		public int GetInteger(string name)
		{
			return this.GetIntegerString(name);
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x000243AC File Offset: 0x000225AC
		public int GetInteger(int id)
		{
			return this.GetIntegerID(id);
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x000243B8 File Offset: 0x000225B8
		public void SetInteger(string name, int value)
		{
			this.SetIntegerString(name, value);
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x000243C4 File Offset: 0x000225C4
		public void SetInteger(int id, int value)
		{
			this.SetIntegerID(id, value);
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x000243D0 File Offset: 0x000225D0
		public void SetTrigger(string name)
		{
			this.SetTriggerString(name);
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x000243DC File Offset: 0x000225DC
		public void SetTrigger(int id)
		{
			this.SetTriggerID(id);
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x000243E8 File Offset: 0x000225E8
		public void ResetTrigger(string name)
		{
			this.ResetTriggerString(name);
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x000243F4 File Offset: 0x000225F4
		public void ResetTrigger(int id)
		{
			this.ResetTriggerID(id);
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x00024400 File Offset: 0x00022600
		public bool IsParameterControlledByCurve(string name)
		{
			return this.IsParameterControlledByCurveString(name);
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x0002440C File Offset: 0x0002260C
		public bool IsParameterControlledByCurve(int id)
		{
			return this.IsParameterControlledByCurveID(id);
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x00024418 File Offset: 0x00022618
		public Vector3 deltaPosition
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_deltaPosition(out vector);
				return vector;
			}
		}

		// Token: 0x06001878 RID: 6264
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_deltaPosition(out Vector3 value);

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x00024430 File Offset: 0x00022630
		public Quaternion deltaRotation
		{
			get
			{
				Quaternion quaternion;
				this.INTERNAL_get_deltaRotation(out quaternion);
				return quaternion;
			}
		}

		// Token: 0x0600187A RID: 6266
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_deltaRotation(out Quaternion value);

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x00024448 File Offset: 0x00022648
		// (set) Token: 0x0600187C RID: 6268 RVA: 0x00024460 File Offset: 0x00022660
		public Vector3 rootPosition
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_rootPosition(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_rootPosition(ref value);
			}
		}

		// Token: 0x0600187D RID: 6269
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rootPosition(out Vector3 value);

		// Token: 0x0600187E RID: 6270
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_rootPosition(ref Vector3 value);

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x0002446C File Offset: 0x0002266C
		// (set) Token: 0x06001880 RID: 6272 RVA: 0x00024484 File Offset: 0x00022684
		public Quaternion rootRotation
		{
			get
			{
				Quaternion quaternion;
				this.INTERNAL_get_rootRotation(out quaternion);
				return quaternion;
			}
			set
			{
				this.INTERNAL_set_rootRotation(ref value);
			}
		}

		// Token: 0x06001881 RID: 6273
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rootRotation(out Quaternion value);

		// Token: 0x06001882 RID: 6274
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_rootRotation(ref Quaternion value);

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001883 RID: 6275
		// (set) Token: 0x06001884 RID: 6276
		public extern bool applyRootMotion
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x00024490 File Offset: 0x00022690
		// (set) Token: 0x06001886 RID: 6278 RVA: 0x0002449C File Offset: 0x0002269C
		[Obsolete("Use AnimationMode.updateMode instead")]
		public bool animatePhysics
		{
			get
			{
				return this.updateMode == AnimatorUpdateMode.AnimatePhysics;
			}
			set
			{
				this.updateMode = ((!value) ? AnimatorUpdateMode.Normal : AnimatorUpdateMode.AnimatePhysics);
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001887 RID: 6279
		// (set) Token: 0x06001888 RID: 6280
		public extern AnimatorUpdateMode updateMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001889 RID: 6281
		public extern bool hasTransformHierarchy
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x0600188A RID: 6282
		// (set) Token: 0x0600188B RID: 6283
		internal extern bool allowConstantClipSamplingOptimization
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x0600188C RID: 6284
		public extern float gravityWeight
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x000244B4 File Offset: 0x000226B4
		// (set) Token: 0x0600188E RID: 6286 RVA: 0x000244CC File Offset: 0x000226CC
		public Vector3 bodyPosition
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_bodyPosition(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_bodyPosition(ref value);
			}
		}

		// Token: 0x0600188F RID: 6287
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_bodyPosition(out Vector3 value);

		// Token: 0x06001890 RID: 6288
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_bodyPosition(ref Vector3 value);

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x000244D8 File Offset: 0x000226D8
		// (set) Token: 0x06001892 RID: 6290 RVA: 0x000244F0 File Offset: 0x000226F0
		public Quaternion bodyRotation
		{
			get
			{
				Quaternion quaternion;
				this.INTERNAL_get_bodyRotation(out quaternion);
				return quaternion;
			}
			set
			{
				this.INTERNAL_set_bodyRotation(ref value);
			}
		}

		// Token: 0x06001893 RID: 6291
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_bodyRotation(out Quaternion value);

		// Token: 0x06001894 RID: 6292
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_bodyRotation(ref Quaternion value);

		// Token: 0x06001895 RID: 6293
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector3 GetIKPosition(AvatarIKGoal goal);

		// Token: 0x06001896 RID: 6294 RVA: 0x000244FC File Offset: 0x000226FC
		public void SetIKPosition(AvatarIKGoal goal, Vector3 goalPosition)
		{
			Animator.INTERNAL_CALL_SetIKPosition(this, goal, ref goalPosition);
		}

		// Token: 0x06001897 RID: 6295
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetIKPosition(Animator self, AvatarIKGoal goal, ref Vector3 goalPosition);

		// Token: 0x06001898 RID: 6296
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Quaternion GetIKRotation(AvatarIKGoal goal);

		// Token: 0x06001899 RID: 6297 RVA: 0x00024508 File Offset: 0x00022708
		public void SetIKRotation(AvatarIKGoal goal, Quaternion goalRotation)
		{
			Animator.INTERNAL_CALL_SetIKRotation(this, goal, ref goalRotation);
		}

		// Token: 0x0600189A RID: 6298
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetIKRotation(Animator self, AvatarIKGoal goal, ref Quaternion goalRotation);

		// Token: 0x0600189B RID: 6299
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetIKPositionWeight(AvatarIKGoal goal);

		// Token: 0x0600189C RID: 6300
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetIKPositionWeight(AvatarIKGoal goal, float value);

		// Token: 0x0600189D RID: 6301
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetIKRotationWeight(AvatarIKGoal goal);

		// Token: 0x0600189E RID: 6302
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetIKRotationWeight(AvatarIKGoal goal, float value);

		// Token: 0x0600189F RID: 6303 RVA: 0x00024514 File Offset: 0x00022714
		public void SetLookAtPosition(Vector3 lookAtPosition)
		{
			Animator.INTERNAL_CALL_SetLookAtPosition(this, ref lookAtPosition);
		}

		// Token: 0x060018A0 RID: 6304
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetLookAtPosition(Animator self, ref Vector3 lookAtPosition);

		// Token: 0x060018A1 RID: 6305
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLookAtWeight(float weight, [DefaultValue("0.00f")] float bodyWeight, [DefaultValue("1.00f")] float headWeight, [DefaultValue("0.00f")] float eyesWeight, [DefaultValue("0.50f")] float clampWeight);

		// Token: 0x060018A2 RID: 6306 RVA: 0x00024520 File Offset: 0x00022720
		[ExcludeFromDocs]
		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight, float eyesWeight)
		{
			float num = 0.5f;
			this.SetLookAtWeight(weight, bodyWeight, headWeight, eyesWeight, num);
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00024540 File Offset: 0x00022740
		[ExcludeFromDocs]
		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight)
		{
			float num = 0.5f;
			float num2 = 0f;
			this.SetLookAtWeight(weight, bodyWeight, headWeight, num2, num);
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00024564 File Offset: 0x00022764
		[ExcludeFromDocs]
		public void SetLookAtWeight(float weight, float bodyWeight)
		{
			float num = 0.5f;
			float num2 = 0f;
			float num3 = 1f;
			this.SetLookAtWeight(weight, bodyWeight, num3, num2, num);
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x00024590 File Offset: 0x00022790
		[ExcludeFromDocs]
		public void SetLookAtWeight(float weight)
		{
			float num = 0.5f;
			float num2 = 0f;
			float num3 = 1f;
			float num4 = 0f;
			this.SetLookAtWeight(weight, num4, num3, num2, num);
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060018A6 RID: 6310
		// (set) Token: 0x060018A7 RID: 6311
		public extern bool stabilizeFeet
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060018A8 RID: 6312
		public extern int layerCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060018A9 RID: 6313
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetLayerName(int layerIndex);

		// Token: 0x060018AA RID: 6314
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetLayerWeight(int layerIndex);

		// Token: 0x060018AB RID: 6315
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLayerWeight(int layerIndex, float weight);

		// Token: 0x060018AC RID: 6316
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex);

		// Token: 0x060018AD RID: 6317
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimatorStateInfo GetNextAnimatorStateInfo(int layerIndex);

		// Token: 0x060018AE RID: 6318
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimatorTransitionInfo GetAnimatorTransitionInfo(int layerIndex);

		// Token: 0x060018AF RID: 6319
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimationInfo[] GetCurrentAnimationClipState(int layerIndex);

		// Token: 0x060018B0 RID: 6320
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimationInfo[] GetNextAnimationClipState(int layerIndex);

		// Token: 0x060018B1 RID: 6321
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsInTransition(int layerIndex);

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060018B2 RID: 6322
		// (set) Token: 0x060018B3 RID: 6323
		public extern float feetPivotActive
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060018B4 RID: 6324
		public extern float pivotWeight
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x000245C0 File Offset: 0x000227C0
		public Vector3 pivotPosition
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_pivotPosition(out vector);
				return vector;
			}
		}

		// Token: 0x060018B6 RID: 6326
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_pivotPosition(out Vector3 value);

		// Token: 0x060018B7 RID: 6327 RVA: 0x000245D8 File Offset: 0x000227D8
		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime, [DefaultValue("1")] float targetNormalizedTime)
		{
			Animator.INTERNAL_CALL_MatchTarget(this, ref matchPosition, ref matchRotation, targetBodyPart, ref weightMask, startNormalizedTime, targetNormalizedTime);
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x000245EC File Offset: 0x000227EC
		[ExcludeFromDocs]
		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime)
		{
			float num = 1f;
			Animator.INTERNAL_CALL_MatchTarget(this, ref matchPosition, ref matchRotation, targetBodyPart, ref weightMask, startNormalizedTime, num);
		}

		// Token: 0x060018B9 RID: 6329
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_MatchTarget(Animator self, ref Vector3 matchPosition, ref Quaternion matchRotation, AvatarTarget targetBodyPart, ref MatchTargetWeightMask weightMask, float startNormalizedTime, float targetNormalizedTime);

		// Token: 0x060018BA RID: 6330
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InterruptMatchTarget([DefaultValue("true")] bool completeMatch);

		// Token: 0x060018BB RID: 6331 RVA: 0x00024610 File Offset: 0x00022810
		[ExcludeFromDocs]
		public void InterruptMatchTarget()
		{
			bool flag = true;
			this.InterruptMatchTarget(flag);
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060018BC RID: 6332
		public extern bool isMatchingTarget
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060018BD RID: 6333
		// (set) Token: 0x060018BE RID: 6334
		public extern float speed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00024628 File Offset: 0x00022828
		[Obsolete("ForceStateNormalizedTime is deprecated. Please use Play or CrossFade instead.")]
		public void ForceStateNormalizedTime(float normalizedTime)
		{
			this.Play(0, 0, normalizedTime);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00024634 File Offset: 0x00022834
		[ExcludeFromDocs]
		public void CrossFade(string stateName, float transitionDuration, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.CrossFade(stateName, transitionDuration, layer, negativeInfinity);
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x00024654 File Offset: 0x00022854
		[ExcludeFromDocs]
		public void CrossFade(string stateName, float transitionDuration)
		{
			float negativeInfinity = float.NegativeInfinity;
			int num = -1;
			this.CrossFade(stateName, transitionDuration, num, negativeInfinity);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00024674 File Offset: 0x00022874
		public void CrossFade(string stateName, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			this.CrossFade(Animator.StringToHash(stateName), transitionDuration, layer, normalizedTime);
		}

		// Token: 0x060018C3 RID: 6339
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CrossFade(int stateNameHash, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime);

		// Token: 0x060018C4 RID: 6340 RVA: 0x00024688 File Offset: 0x00022888
		[ExcludeFromDocs]
		public void CrossFade(int stateNameHash, float transitionDuration, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.CrossFade(stateNameHash, transitionDuration, layer, negativeInfinity);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x000246A8 File Offset: 0x000228A8
		[ExcludeFromDocs]
		public void CrossFade(int stateNameHash, float transitionDuration)
		{
			float negativeInfinity = float.NegativeInfinity;
			int num = -1;
			this.CrossFade(stateNameHash, transitionDuration, num, negativeInfinity);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000246C8 File Offset: 0x000228C8
		[ExcludeFromDocs]
		public void Play(string stateName, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.Play(stateName, layer, negativeInfinity);
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x000246E4 File Offset: 0x000228E4
		[ExcludeFromDocs]
		public void Play(string stateName)
		{
			float negativeInfinity = float.NegativeInfinity;
			int num = -1;
			this.Play(stateName, num, negativeInfinity);
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x00024704 File Offset: 0x00022904
		public void Play(string stateName, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			this.Play(Animator.StringToHash(stateName), layer, normalizedTime);
		}

		// Token: 0x060018C9 RID: 6345
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Play(int stateNameHash, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime);

		// Token: 0x060018CA RID: 6346 RVA: 0x00024714 File Offset: 0x00022914
		[ExcludeFromDocs]
		public void Play(int stateNameHash, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.Play(stateNameHash, layer, negativeInfinity);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x00024730 File Offset: 0x00022930
		[ExcludeFromDocs]
		public void Play(int stateNameHash)
		{
			float negativeInfinity = float.NegativeInfinity;
			int num = -1;
			this.Play(stateNameHash, num, negativeInfinity);
		}

		// Token: 0x060018CC RID: 6348
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTarget(AvatarTarget targetIndex, float targetNormalizedTime);

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x00024750 File Offset: 0x00022950
		public Vector3 targetPosition
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_targetPosition(out vector);
				return vector;
			}
		}

		// Token: 0x060018CE RID: 6350
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_targetPosition(out Vector3 value);

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x00024768 File Offset: 0x00022968
		public Quaternion targetRotation
		{
			get
			{
				Quaternion quaternion;
				this.INTERNAL_get_targetRotation(out quaternion);
				return quaternion;
			}
		}

		// Token: 0x060018D0 RID: 6352
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_targetRotation(out Quaternion value);

		// Token: 0x060018D1 RID: 6353
		[WrapperlessIcall]
		[Obsolete("use mask and layers to control subset of transfroms in a skeleton", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsControlled(Transform transform);

		// Token: 0x060018D2 RID: 6354
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsBoneTransform(Transform transform);

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060018D3 RID: 6355
		internal extern Transform avatarRoot
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060018D4 RID: 6356
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Transform GetBoneTransform(HumanBodyBones humanBoneId);

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060018D5 RID: 6357
		// (set) Token: 0x060018D6 RID: 6358
		public extern AnimatorCullingMode cullingMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060018D7 RID: 6359
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StartPlayback();

		// Token: 0x060018D8 RID: 6360
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopPlayback();

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060018D9 RID: 6361
		// (set) Token: 0x060018DA RID: 6362
		public extern float playbackTime
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060018DB RID: 6363
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StartRecording(int frameCount);

		// Token: 0x060018DC RID: 6364
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopRecording();

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060018DD RID: 6365
		// (set) Token: 0x060018DE RID: 6366
		public extern float recorderStartTime
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060018DF RID: 6367
		// (set) Token: 0x060018E0 RID: 6368
		public extern float recorderStopTime
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060018E1 RID: 6369
		// (set) Token: 0x060018E2 RID: 6370
		public extern RuntimeAnimatorController runtimeAnimatorController
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060018E3 RID: 6371
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int StringToHash(string name);

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060018E4 RID: 6372
		// (set) Token: 0x060018E5 RID: 6373
		public extern Avatar avatar
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060018E6 RID: 6374
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string GetStats();

		// Token: 0x060018E7 RID: 6375
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatString(string name, float value);

		// Token: 0x060018E8 RID: 6376
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatID(int id, float value);

		// Token: 0x060018E9 RID: 6377
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetFloatString(string name);

		// Token: 0x060018EA RID: 6378
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetFloatID(int id);

		// Token: 0x060018EB RID: 6379
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBoolString(string name, bool value);

		// Token: 0x060018EC RID: 6380
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBoolID(int id, bool value);

		// Token: 0x060018ED RID: 6381
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetBoolString(string name);

		// Token: 0x060018EE RID: 6382
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetBoolID(int id);

		// Token: 0x060018EF RID: 6383
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIntegerString(string name, int value);

		// Token: 0x060018F0 RID: 6384
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIntegerID(int id, int value);

		// Token: 0x060018F1 RID: 6385
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetIntegerString(string name);

		// Token: 0x060018F2 RID: 6386
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetIntegerID(int id);

		// Token: 0x060018F3 RID: 6387
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTriggerString(string name);

		// Token: 0x060018F4 RID: 6388
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTriggerID(int id);

		// Token: 0x060018F5 RID: 6389
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResetTriggerString(string name);

		// Token: 0x060018F6 RID: 6390
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResetTriggerID(int id);

		// Token: 0x060018F7 RID: 6391
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsParameterControlledByCurveString(string name);

		// Token: 0x060018F8 RID: 6392
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsParameterControlledByCurveID(int id);

		// Token: 0x060018F9 RID: 6393
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatStringDamp(string name, float value, float dampTime, float deltaTime);

		// Token: 0x060018FA RID: 6394
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatIDDamp(int id, float value, float dampTime, float deltaTime);

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060018FB RID: 6395
		// (set) Token: 0x060018FC RID: 6396
		public extern bool layersAffectMassCenter
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060018FD RID: 6397
		public extern float leftFeetBottomHeight
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060018FE RID: 6398
		public extern float rightFeetBottomHeight
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060018FF RID: 6399
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Update(float deltaTime);

		// Token: 0x06001900 RID: 6400
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Rebind();

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001901 RID: 6401
		private extern bool isInManagerList
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001902 RID: 6402
		// (set) Token: 0x06001903 RID: 6403
		public extern bool logWarnings
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001904 RID: 6404
		// (set) Token: 0x06001905 RID: 6405
		public extern bool fireEvents
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x00024780 File Offset: 0x00022980
		[Obsolete("GetVector is deprecated.")]
		public Vector3 GetVector(string name)
		{
			return Vector3.zero;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00024788 File Offset: 0x00022988
		[Obsolete("GetVector is deprecated.")]
		public Vector3 GetVector(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00024790 File Offset: 0x00022990
		[Obsolete("SetVector is deprecated.")]
		public void SetVector(string name, Vector3 value)
		{
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00024794 File Offset: 0x00022994
		[Obsolete("SetVector is deprecated.")]
		public void SetVector(int id, Vector3 value)
		{
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00024798 File Offset: 0x00022998
		[Obsolete("GetQuaternion is deprecated.")]
		public Quaternion GetQuaternion(string name)
		{
			return Quaternion.identity;
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x000247A0 File Offset: 0x000229A0
		[Obsolete("GetQuaternion is deprecated.")]
		public Quaternion GetQuaternion(int id)
		{
			return Quaternion.identity;
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x000247A8 File Offset: 0x000229A8
		[Obsolete("SetQuaternion is deprecated.")]
		public void SetQuaternion(string name, Quaternion value)
		{
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x000247AC File Offset: 0x000229AC
		[Obsolete("SetQuaternion is deprecated.")]
		public void SetQuaternion(int id, Quaternion value)
		{
		}
	}
}
