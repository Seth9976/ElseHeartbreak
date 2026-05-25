using System;
using System.Text;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000021 RID: 33
	public class PointerEventData : BaseEventData
	{
		// Token: 0x06000092 RID: 146 RVA: 0x000032B8 File Offset: 0x000014B8
		public PointerEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
			this.eligibleForClick = false;
			this.pointerId = -1;
			this.position = Vector2.zero;
			this.delta = Vector2.zero;
			this.pressPosition = Vector2.zero;
			this.clickTime = 0f;
			this.clickCount = 0;
			this.scrollDelta = Vector2.zero;
			this.useDragThreshold = true;
			this.dragging = false;
			this.button = PointerEventData.InputButton.Left;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003330 File Offset: 0x00001530
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003338 File Offset: 0x00001538
		public GameObject pointerEnter { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003344 File Offset: 0x00001544
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000334C File Offset: 0x0000154C
		public GameObject lastPress { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003358 File Offset: 0x00001558
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00003360 File Offset: 0x00001560
		public GameObject rawPointerPress { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000336C File Offset: 0x0000156C
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003374 File Offset: 0x00001574
		public GameObject pointerDrag { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003380 File Offset: 0x00001580
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00003388 File Offset: 0x00001588
		public RaycastResult pointerCurrentRaycast { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003394 File Offset: 0x00001594
		// (set) Token: 0x0600009E RID: 158 RVA: 0x0000339C File Offset: 0x0000159C
		public RaycastResult pointerPressRaycast { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000033A8 File Offset: 0x000015A8
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000033B0 File Offset: 0x000015B0
		public bool eligibleForClick { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000033BC File Offset: 0x000015BC
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x000033C4 File Offset: 0x000015C4
		public int pointerId { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000033D0 File Offset: 0x000015D0
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000033D8 File Offset: 0x000015D8
		public Vector2 position { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000033E4 File Offset: 0x000015E4
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000033EC File Offset: 0x000015EC
		public Vector2 delta { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000033F8 File Offset: 0x000015F8
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00003400 File Offset: 0x00001600
		public Vector2 pressPosition { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000340C File Offset: 0x0000160C
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003414 File Offset: 0x00001614
		[Obsolete("Use either pointerCurrentRaycast.worldPosition or pointerPressRaycast.worldPosition")]
		public Vector3 worldPosition { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003420 File Offset: 0x00001620
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00003428 File Offset: 0x00001628
		[Obsolete("Use either pointerCurrentRaycast.worldNormal or pointerPressRaycast.worldNormal")]
		public Vector3 worldNormal { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003434 File Offset: 0x00001634
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000343C File Offset: 0x0000163C
		public float clickTime { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003448 File Offset: 0x00001648
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003450 File Offset: 0x00001650
		public int clickCount { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000345C File Offset: 0x0000165C
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00003464 File Offset: 0x00001664
		public Vector2 scrollDelta { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003470 File Offset: 0x00001670
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003478 File Offset: 0x00001678
		public bool useDragThreshold { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003484 File Offset: 0x00001684
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000348C File Offset: 0x0000168C
		public bool dragging { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003498 File Offset: 0x00001698
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000034A0 File Offset: 0x000016A0
		public PointerEventData.InputButton button { get; set; }

		// Token: 0x060000B9 RID: 185 RVA: 0x000034AC File Offset: 0x000016AC
		public bool IsPointerMoving()
		{
			return this.delta.sqrMagnitude > 0f;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000034D0 File Offset: 0x000016D0
		public bool IsScrolling()
		{
			return this.scrollDelta.sqrMagnitude > 0f;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000034F4 File Offset: 0x000016F4
		public Camera enterEventCamera
		{
			get
			{
				return (!(this.pointerCurrentRaycast.module == null)) ? this.pointerCurrentRaycast.module.eventCamera : null;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003534 File Offset: 0x00001734
		public Camera pressEventCamera
		{
			get
			{
				return (!(this.pointerPressRaycast.module == null)) ? this.pointerPressRaycast.module.eventCamera : null;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003574 File Offset: 0x00001774
		// (set) Token: 0x060000BE RID: 190 RVA: 0x0000357C File Offset: 0x0000177C
		public GameObject pointerPress
		{
			get
			{
				return this.m_PointerPress;
			}
			set
			{
				if (this.m_PointerPress == value)
				{
					return;
				}
				this.lastPress = this.m_PointerPress;
				this.m_PointerPress = value;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000035A4 File Offset: 0x000017A4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<b>Position</b>: " + this.position);
			stringBuilder.AppendLine("<b>delta</b>: " + this.delta);
			stringBuilder.AppendLine("<b>eligibleForClick</b>: " + this.eligibleForClick);
			stringBuilder.AppendLine("<b>pointerEnter</b>: " + this.pointerEnter);
			stringBuilder.AppendLine("<b>pointerPress</b>: " + this.pointerPress);
			stringBuilder.AppendLine("<b>lastPointerPress</b>: " + this.lastPress);
			stringBuilder.AppendLine("<b>pointerDrag</b>: " + this.pointerDrag);
			stringBuilder.AppendLine("<b>Use Drag Threshold</b>: " + this.useDragThreshold);
			return stringBuilder.ToString();
		}

		// Token: 0x0400004C RID: 76
		private GameObject m_PointerPress;

		// Token: 0x02000022 RID: 34
		public enum InputButton
		{
			// Token: 0x04000061 RID: 97
			Left,
			// Token: 0x04000062 RID: 98
			Right,
			// Token: 0x04000063 RID: 99
			Middle
		}

		// Token: 0x02000023 RID: 35
		public enum FramePressState
		{
			// Token: 0x04000065 RID: 101
			Pressed,
			// Token: 0x04000066 RID: 102
			Released,
			// Token: 0x04000067 RID: 103
			PressedAndReleased,
			// Token: 0x04000068 RID: 104
			NotChanged
		}
	}
}
