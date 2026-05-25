using System;
using UnityEngine.Events;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x02000030 RID: 48
	internal struct ColorTween : ITweenValue
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000052D8 File Offset: 0x000034D8
		// (set) Token: 0x06000130 RID: 304 RVA: 0x000052E0 File Offset: 0x000034E0
		public Color startColor
		{
			get
			{
				return this.m_StartColor;
			}
			set
			{
				this.m_StartColor = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000052EC File Offset: 0x000034EC
		// (set) Token: 0x06000132 RID: 306 RVA: 0x000052F4 File Offset: 0x000034F4
		public Color targetColor
		{
			get
			{
				return this.m_TargetColor;
			}
			set
			{
				this.m_TargetColor = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005300 File Offset: 0x00003500
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00005308 File Offset: 0x00003508
		public ColorTween.ColorTweenMode tweenMode
		{
			get
			{
				return this.m_TweenMode;
			}
			set
			{
				this.m_TweenMode = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00005314 File Offset: 0x00003514
		// (set) Token: 0x06000136 RID: 310 RVA: 0x0000531C File Offset: 0x0000351C
		public float duration
		{
			get
			{
				return this.m_Duration;
			}
			set
			{
				this.m_Duration = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005328 File Offset: 0x00003528
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00005330 File Offset: 0x00003530
		public bool ignoreTimeScale
		{
			get
			{
				return this.m_IgnoreTimeScale;
			}
			set
			{
				this.m_IgnoreTimeScale = value;
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000533C File Offset: 0x0000353C
		public void TweenValue(float floatPercentage)
		{
			if (!this.ValidTarget())
			{
				return;
			}
			Color color = Color.Lerp(this.m_StartColor, this.m_TargetColor, floatPercentage);
			if (this.m_TweenMode == ColorTween.ColorTweenMode.Alpha)
			{
				color.r = this.m_StartColor.r;
				color.g = this.m_StartColor.g;
				color.b = this.m_StartColor.b;
			}
			else if (this.m_TweenMode == ColorTween.ColorTweenMode.RGB)
			{
				color.a = this.m_StartColor.a;
			}
			this.m_Target.Invoke(color);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000053DC File Offset: 0x000035DC
		public void AddOnChangedCallback(UnityAction<Color> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new ColorTween.ColorTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000540C File Offset: 0x0000360C
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005414 File Offset: 0x00003614
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000541C File Offset: 0x0000361C
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x0400008B RID: 139
		private ColorTween.ColorTweenCallback m_Target;

		// Token: 0x0400008C RID: 140
		private Color m_StartColor;

		// Token: 0x0400008D RID: 141
		private Color m_TargetColor;

		// Token: 0x0400008E RID: 142
		private ColorTween.ColorTweenMode m_TweenMode;

		// Token: 0x0400008F RID: 143
		private float m_Duration;

		// Token: 0x04000090 RID: 144
		private bool m_IgnoreTimeScale;

		// Token: 0x02000031 RID: 49
		public enum ColorTweenMode
		{
			// Token: 0x04000092 RID: 146
			All,
			// Token: 0x04000093 RID: 147
			RGB,
			// Token: 0x04000094 RID: 148
			Alpha
		}

		// Token: 0x02000032 RID: 50
		public class ColorTweenCallback : UnityEvent<Color>
		{
		}
	}
}
