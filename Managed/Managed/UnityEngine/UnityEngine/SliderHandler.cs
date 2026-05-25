using System;

namespace UnityEngine
{
	// Token: 0x02000046 RID: 70
	internal struct SliderHandler
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00004158 File Offset: 0x00002358
		public SliderHandler(Rect position, float currentValue, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id)
		{
			this.position = position;
			this.currentValue = currentValue;
			this.size = size;
			this.start = start;
			this.end = end;
			this.slider = slider;
			this.thumb = thumb;
			this.horiz = horiz;
			this.id = id;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000041AC File Offset: 0x000023AC
		public float Handle()
		{
			if (this.slider == null || this.thumb == null)
			{
				return this.currentValue;
			}
			switch (this.CurrentEventType())
			{
			case EventType.MouseDown:
				return this.OnMouseDown();
			case EventType.MouseUp:
				return this.OnMouseUp();
			case EventType.MouseDrag:
				return this.OnMouseDrag();
			case EventType.Repaint:
				return this.OnRepaint();
			}
			return this.currentValue;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000422C File Offset: 0x0000242C
		private float OnMouseDown()
		{
			if (!this.position.Contains(this.CurrentEvent().mousePosition) || this.IsEmptySlider())
			{
				return this.currentValue;
			}
			GUI.scrollTroughSide = 0;
			GUIUtility.hotControl = this.id;
			this.CurrentEvent().Use();
			if (this.ThumbSelectionRect().Contains(this.CurrentEvent().mousePosition))
			{
				this.StartDraggingWithValue(this.ClampedCurrentValue());
				return this.currentValue;
			}
			GUI.changed = true;
			if (this.SupportsPageMovements())
			{
				this.SliderState().isDragging = false;
				GUI.nextScrollStepTime = SystemClock.now.AddMilliseconds(250.0);
				GUI.scrollTroughSide = this.CurrentScrollTroughSide();
				return this.PageMovementValue();
			}
			float num = this.ValueForCurrentMousePosition();
			this.StartDraggingWithValue(num);
			return this.Clamp(num);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004318 File Offset: 0x00002518
		private float OnMouseDrag()
		{
			if (GUIUtility.hotControl != this.id)
			{
				return this.currentValue;
			}
			SliderState sliderState = this.SliderState();
			if (!sliderState.isDragging)
			{
				return this.currentValue;
			}
			GUI.changed = true;
			this.CurrentEvent().Use();
			float num = this.MousePosition() - sliderState.dragStartPos;
			float num2 = sliderState.dragStartValue + num / this.ValuesPerPixel();
			return this.Clamp(num2);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000438C File Offset: 0x0000258C
		private float OnMouseUp()
		{
			if (GUIUtility.hotControl == this.id)
			{
				this.CurrentEvent().Use();
				GUIUtility.hotControl = 0;
			}
			return this.currentValue;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000043B8 File Offset: 0x000025B8
		private float OnRepaint()
		{
			this.slider.Draw(this.position, GUIContent.none, this.id);
			this.thumb.Draw(this.ThumbRect(), GUIContent.none, this.id);
			if (GUIUtility.hotControl != this.id || !this.position.Contains(this.CurrentEvent().mousePosition) || this.IsEmptySlider())
			{
				return this.currentValue;
			}
			if (this.ThumbRect().Contains(this.CurrentEvent().mousePosition))
			{
				if (GUI.scrollTroughSide != 0)
				{
					GUIUtility.hotControl = 0;
				}
				return this.currentValue;
			}
			GUI.InternalRepaintEditorWindow();
			if (SystemClock.now < GUI.nextScrollStepTime)
			{
				return this.currentValue;
			}
			if (this.CurrentScrollTroughSide() != GUI.scrollTroughSide)
			{
				return this.currentValue;
			}
			GUI.nextScrollStepTime = SystemClock.now.AddMilliseconds(30.0);
			if (this.SupportsPageMovements())
			{
				this.SliderState().isDragging = false;
				GUI.changed = true;
				return this.PageMovementValue();
			}
			return this.ClampedCurrentValue();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000044F0 File Offset: 0x000026F0
		private EventType CurrentEventType()
		{
			return this.CurrentEvent().GetTypeForControl(this.id);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004504 File Offset: 0x00002704
		private int CurrentScrollTroughSide()
		{
			float num = ((!this.horiz) ? this.CurrentEvent().mousePosition.y : this.CurrentEvent().mousePosition.x);
			float num2 = ((!this.horiz) ? this.ThumbRect().y : this.ThumbRect().x);
			return (num <= num2) ? (-1) : 1;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004588 File Offset: 0x00002788
		private bool IsEmptySlider()
		{
			return this.start == this.end;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004598 File Offset: 0x00002798
		private bool SupportsPageMovements()
		{
			return this.size != 0f && GUI.usePageScrollbars;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000045B4 File Offset: 0x000027B4
		private float PageMovementValue()
		{
			float num = this.currentValue;
			int num2 = ((this.start <= this.end) ? 1 : (-1));
			if (this.MousePosition() > this.PageUpMovementBound())
			{
				num += this.size * (float)num2 * 0.9f;
			}
			else
			{
				num -= this.size * (float)num2 * 0.9f;
			}
			return this.Clamp(num);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004624 File Offset: 0x00002824
		private float PageUpMovementBound()
		{
			if (this.horiz)
			{
				return this.ThumbRect().xMax - this.position.x;
			}
			return this.ThumbRect().yMax - this.position.y;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004678 File Offset: 0x00002878
		private Event CurrentEvent()
		{
			return Event.current;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004680 File Offset: 0x00002880
		private float ValueForCurrentMousePosition()
		{
			if (this.horiz)
			{
				return (this.MousePosition() - this.ThumbRect().width * 0.5f) / this.ValuesPerPixel() + this.start - this.size * 0.5f;
			}
			return (this.MousePosition() - this.ThumbRect().height * 0.5f) / this.ValuesPerPixel() + this.start - this.size * 0.5f;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004708 File Offset: 0x00002908
		private float Clamp(float value)
		{
			return Mathf.Clamp(value, this.MinValue(), this.MaxValue());
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000471C File Offset: 0x0000291C
		private Rect ThumbSelectionRect()
		{
			return this.ThumbRect();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004734 File Offset: 0x00002934
		private void StartDraggingWithValue(float dragStartValue)
		{
			SliderState sliderState = this.SliderState();
			sliderState.dragStartPos = this.MousePosition();
			sliderState.dragStartValue = dragStartValue;
			sliderState.isDragging = true;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004764 File Offset: 0x00002964
		private SliderState SliderState()
		{
			return (SliderState)GUIUtility.GetStateObject(typeof(SliderState), this.id);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004780 File Offset: 0x00002980
		private Rect ThumbRect()
		{
			return (!this.horiz) ? this.VerticalThumbRect() : this.HorizontalThumbRect();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000047A0 File Offset: 0x000029A0
		private Rect VerticalThumbRect()
		{
			float num = this.ValuesPerPixel();
			if (this.start < this.end)
			{
				return new Rect(this.position.x + (float)this.slider.padding.left, (this.ClampedCurrentValue() - this.start) * num + this.position.y + (float)this.slider.padding.top, this.position.width - (float)this.slider.padding.horizontal, this.size * num + this.ThumbSize());
			}
			return new Rect(this.position.x + (float)this.slider.padding.left, (this.ClampedCurrentValue() + this.size - this.start) * num + this.position.y + (float)this.slider.padding.top, this.position.width - (float)this.slider.padding.horizontal, this.size * -num + this.ThumbSize());
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000048DC File Offset: 0x00002ADC
		private Rect HorizontalThumbRect()
		{
			float num = this.ValuesPerPixel();
			if (this.start < this.end)
			{
				return new Rect((this.ClampedCurrentValue() - this.start) * num + this.position.x + (float)this.slider.padding.left, this.position.y + (float)this.slider.padding.top, this.size * num + this.ThumbSize(), this.position.height - (float)this.slider.padding.vertical);
			}
			return new Rect((this.ClampedCurrentValue() + this.size - this.start) * num + this.position.x + (float)this.slider.padding.left, this.position.y, this.size * -num + this.ThumbSize(), this.position.height);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000049F4 File Offset: 0x00002BF4
		private float ClampedCurrentValue()
		{
			return this.Clamp(this.currentValue);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004A04 File Offset: 0x00002C04
		private float MousePosition()
		{
			if (this.horiz)
			{
				return this.CurrentEvent().mousePosition.x - this.position.x;
			}
			return this.CurrentEvent().mousePosition.y - this.position.y;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004A64 File Offset: 0x00002C64
		private float ValuesPerPixel()
		{
			if (this.horiz)
			{
				return (this.position.width - (float)this.slider.padding.horizontal - this.ThumbSize()) / (this.end - this.start);
			}
			return (this.position.height - (float)this.slider.padding.vertical - this.ThumbSize()) / (this.end - this.start);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004AE8 File Offset: 0x00002CE8
		private float ThumbSize()
		{
			if (this.horiz)
			{
				return (this.thumb.fixedWidth == 0f) ? ((float)this.thumb.padding.horizontal) : this.thumb.fixedWidth;
			}
			return (this.thumb.fixedHeight == 0f) ? ((float)this.thumb.padding.vertical) : this.thumb.fixedHeight;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004B70 File Offset: 0x00002D70
		private float MaxValue()
		{
			return Mathf.Max(this.start, this.end) - this.size;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004B8C File Offset: 0x00002D8C
		private float MinValue()
		{
			return Mathf.Min(this.start, this.end);
		}

		// Token: 0x040000EB RID: 235
		private readonly Rect position;

		// Token: 0x040000EC RID: 236
		private readonly float currentValue;

		// Token: 0x040000ED RID: 237
		private readonly float size;

		// Token: 0x040000EE RID: 238
		private readonly float start;

		// Token: 0x040000EF RID: 239
		private readonly float end;

		// Token: 0x040000F0 RID: 240
		private readonly GUIStyle slider;

		// Token: 0x040000F1 RID: 241
		private readonly GUIStyle thumb;

		// Token: 0x040000F2 RID: 242
		private readonly bool horiz;

		// Token: 0x040000F3 RID: 243
		private readonly int id;
	}
}
