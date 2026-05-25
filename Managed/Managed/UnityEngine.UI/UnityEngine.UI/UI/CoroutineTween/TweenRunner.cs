using System;
using System.Collections;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x02000033 RID: 51
	internal class TweenRunner<T> where T : struct, ITweenValue
	{
		// Token: 0x06000140 RID: 320 RVA: 0x0000543C File Offset: 0x0000363C
		private static IEnumerator Start(T tweenInfo)
		{
			if (!tweenInfo.ValidTarget())
			{
				yield break;
			}
			float elapsedTime = 0f;
			while (elapsedTime < tweenInfo.duration)
			{
				elapsedTime += ((!tweenInfo.ignoreTimeScale) ? Time.deltaTime : Time.unscaledDeltaTime);
				float percentage = Mathf.Clamp01(elapsedTime / tweenInfo.duration);
				tweenInfo.TweenValue(percentage);
				yield return null;
			}
			tweenInfo.TweenValue(1f);
			yield break;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005460 File Offset: 0x00003660
		public void Init(MonoBehaviour coroutineContainer)
		{
			this.m_CoroutineContainer = coroutineContainer;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000546C File Offset: 0x0000366C
		public void StartTween(T info)
		{
			if (this.m_CoroutineContainer == null)
			{
				Debug.LogWarning("Coroutine container not configured... did you forget to call Init?");
				return;
			}
			if (this.m_Tween != null)
			{
				this.m_CoroutineContainer.StopCoroutine(this.m_Tween);
				this.m_Tween = null;
			}
			if (!this.m_CoroutineContainer.gameObject.activeInHierarchy)
			{
				info.TweenValue(1f);
				return;
			}
			this.m_Tween = TweenRunner<T>.Start(info);
			this.m_CoroutineContainer.StartCoroutine(this.m_Tween);
		}

		// Token: 0x04000095 RID: 149
		protected MonoBehaviour m_CoroutineContainer;

		// Token: 0x04000096 RID: 150
		protected IEnumerator m_Tween;
	}
}
