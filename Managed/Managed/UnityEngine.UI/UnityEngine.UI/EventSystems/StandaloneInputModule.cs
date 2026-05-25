using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000029 RID: 41
	[AddComponentMenu("Event/Standalone Input Module")]
	public class StandaloneInputModule : PointerInputModule
	{
		// Token: 0x060000EF RID: 239 RVA: 0x0000419C File Offset: 0x0000239C
		protected StandaloneInputModule()
		{
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000041DC File Offset: 0x000023DC
		[Obsolete("Mode is no longer needed on input module as it handles both mouse and keyboard simultaneously.", false)]
		public StandaloneInputModule.InputMode inputMode
		{
			get
			{
				return StandaloneInputModule.InputMode.Mouse;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000041E0 File Offset: 0x000023E0
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x000041E8 File Offset: 0x000023E8
		public bool allowActivationOnMobileDevice
		{
			get
			{
				return this.m_AllowActivationOnMobileDevice;
			}
			set
			{
				this.m_AllowActivationOnMobileDevice = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000041F4 File Offset: 0x000023F4
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x000041FC File Offset: 0x000023FC
		public float inputActionsPerSecond
		{
			get
			{
				return this.m_InputActionsPerSecond;
			}
			set
			{
				this.m_InputActionsPerSecond = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004208 File Offset: 0x00002408
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00004210 File Offset: 0x00002410
		public string horizontalAxis
		{
			get
			{
				return this.m_HorizontalAxis;
			}
			set
			{
				this.m_HorizontalAxis = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000421C File Offset: 0x0000241C
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00004224 File Offset: 0x00002424
		public string verticalAxis
		{
			get
			{
				return this.m_VerticalAxis;
			}
			set
			{
				this.m_VerticalAxis = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004230 File Offset: 0x00002430
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00004238 File Offset: 0x00002438
		public string submitButton
		{
			get
			{
				return this.m_SubmitButton;
			}
			set
			{
				this.m_SubmitButton = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004244 File Offset: 0x00002444
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000424C File Offset: 0x0000244C
		public string cancelButton
		{
			get
			{
				return this.m_CancelButton;
			}
			set
			{
				this.m_CancelButton = value;
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004258 File Offset: 0x00002458
		public override void UpdateModule()
		{
			this.m_LastMousePosition = this.m_MousePosition;
			this.m_MousePosition = Input.mousePosition;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004278 File Offset: 0x00002478
		public override bool IsModuleSupported()
		{
			return this.m_AllowActivationOnMobileDevice || Input.mousePresent;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004290 File Offset: 0x00002490
		public override bool ShouldActivateModule()
		{
			if (!base.ShouldActivateModule())
			{
				return false;
			}
			bool flag = Input.GetButtonDown(this.m_SubmitButton);
			flag |= Input.GetButtonDown(this.m_CancelButton);
			flag |= !Mathf.Approximately(Input.GetAxisRaw(this.m_HorizontalAxis), 0f);
			flag |= !Mathf.Approximately(Input.GetAxisRaw(this.m_VerticalAxis), 0f);
			flag |= (this.m_MousePosition - this.m_LastMousePosition).sqrMagnitude > 0f;
			return flag | Input.GetMouseButtonDown(0);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004328 File Offset: 0x00002528
		public override void ActivateModule()
		{
			base.ActivateModule();
			this.m_MousePosition = Input.mousePosition;
			this.m_LastMousePosition = Input.mousePosition;
			GameObject gameObject = base.eventSystem.currentSelectedGameObject;
			if (gameObject == null)
			{
				gameObject = base.eventSystem.firstSelectedGameObject;
			}
			base.eventSystem.SetSelectedGameObject(gameObject, this.GetBaseEventData());
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004394 File Offset: 0x00002594
		public override void DeactivateModule()
		{
			base.DeactivateModule();
			base.ClearSelection();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000043A4 File Offset: 0x000025A4
		public override void Process()
		{
			bool flag = this.SendUpdateEventToSelectedObject();
			if (base.eventSystem.sendNavigationEvents)
			{
				if (!flag)
				{
					flag |= this.SendMoveEventToSelectedObject();
				}
				if (!flag)
				{
					this.SendSubmitEventToSelectedObject();
				}
			}
			this.ProcessMouseEvent();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000043EC File Offset: 0x000025EC
		protected bool SendSubmitEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			if (Input.GetButtonDown(this.m_SubmitButton))
			{
				ExecuteEvents.Execute<ISubmitHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.submitHandler);
			}
			if (Input.GetButtonDown(this.m_CancelButton))
			{
				ExecuteEvents.Execute<ICancelHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.cancelHandler);
			}
			return baseEventData.used;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000446C File Offset: 0x0000266C
		private bool AllowMoveEventProcessing(float time)
		{
			bool flag = Input.GetButtonDown(this.m_HorizontalAxis);
			flag |= Input.GetButtonDown(this.m_VerticalAxis);
			return flag | (time > this.m_NextAction);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000044A0 File Offset: 0x000026A0
		private Vector2 GetRawMoveVector()
		{
			Vector2 zero = Vector2.zero;
			zero.x = Input.GetAxisRaw(this.m_HorizontalAxis);
			zero.y = Input.GetAxisRaw(this.m_VerticalAxis);
			if (Input.GetButtonDown(this.m_HorizontalAxis))
			{
				if (zero.x < 0f)
				{
					zero.x = -1f;
				}
				if (zero.x > 0f)
				{
					zero.x = 1f;
				}
			}
			if (Input.GetButtonDown(this.m_VerticalAxis))
			{
				if (zero.y < 0f)
				{
					zero.y = -1f;
				}
				if (zero.y > 0f)
				{
					zero.y = 1f;
				}
			}
			return zero;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000456C File Offset: 0x0000276C
		protected bool SendMoveEventToSelectedObject()
		{
			float unscaledTime = Time.unscaledTime;
			if (!this.AllowMoveEventProcessing(unscaledTime))
			{
				return false;
			}
			Vector2 rawMoveVector = this.GetRawMoveVector();
			AxisEventData axisEventData = this.GetAxisEventData(rawMoveVector.x, rawMoveVector.y, 0.6f);
			if (!Mathf.Approximately(axisEventData.moveVector.x, 0f) || !Mathf.Approximately(axisEventData.moveVector.y, 0f))
			{
				ExecuteEvents.Execute<IMoveHandler>(base.eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
			}
			this.m_NextAction = unscaledTime + 1f / this.m_InputActionsPerSecond;
			return axisEventData.used;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000461C File Offset: 0x0000281C
		protected void ProcessMouseEvent()
		{
			this.ProcessMouseEvent(0);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004628 File Offset: 0x00002828
		protected void ProcessMouseEvent(int id)
		{
			PointerInputModule.MouseState mousePointerEventData = this.GetMousePointerEventData(id);
			bool flag = mousePointerEventData.AnyPressesThisFrame();
			bool flag2 = mousePointerEventData.AnyReleasesThisFrame();
			PointerInputModule.MouseButtonEventData eventData = mousePointerEventData.GetButtonState(PointerEventData.InputButton.Left).eventData;
			if (!StandaloneInputModule.UseMouse(flag, flag2, eventData.buttonData))
			{
				return;
			}
			this.ProcessMousePress(eventData);
			this.ProcessMove(eventData.buttonData);
			this.ProcessDrag(eventData.buttonData);
			this.ProcessMousePress(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Right).eventData);
			this.ProcessDrag(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Right).eventData.buttonData);
			this.ProcessMousePress(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Middle).eventData);
			this.ProcessDrag(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Middle).eventData.buttonData);
			if (!Mathf.Approximately(eventData.buttonData.scrollDelta.sqrMagnitude, 0f))
			{
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IScrollHandler>(eventData.buttonData.pointerCurrentRaycast.gameObject);
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(eventHandler, eventData.buttonData, ExecuteEvents.scrollHandler);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004730 File Offset: 0x00002930
		protected static bool UseMouse(bool pressed, bool released, PointerEventData pointerData)
		{
			return pressed || released || pointerData.IsPointerMoving() || pointerData.IsScrolling();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004758 File Offset: 0x00002958
		protected bool SendUpdateEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			ExecuteEvents.Execute<IUpdateSelectedHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.updateSelectedHandler);
			return baseEventData.used;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000047A4 File Offset: 0x000029A4
		protected void ProcessMousePress(PointerInputModule.MouseButtonEventData data)
		{
			PointerEventData buttonData = data.buttonData;
			GameObject gameObject = buttonData.pointerCurrentRaycast.gameObject;
			if (data.PressedThisFrame())
			{
				buttonData.eligibleForClick = true;
				buttonData.delta = Vector2.zero;
				buttonData.dragging = false;
				buttonData.useDragThreshold = true;
				buttonData.pressPosition = buttonData.position;
				buttonData.pointerPressRaycast = buttonData.pointerCurrentRaycast;
				base.DeselectIfSelectionChanged(gameObject, buttonData);
				GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject, buttonData, ExecuteEvents.pointerDownHandler);
				if (gameObject2 == null)
				{
					gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				}
				float unscaledTime = Time.unscaledTime;
				if (gameObject2 == buttonData.lastPress)
				{
					float num = unscaledTime - buttonData.clickTime;
					if (num < 0.3f)
					{
						buttonData.clickCount++;
					}
					else
					{
						buttonData.clickCount = 1;
					}
					buttonData.clickTime = unscaledTime;
				}
				else
				{
					buttonData.clickCount = 1;
				}
				buttonData.pointerPress = gameObject2;
				buttonData.rawPointerPress = gameObject;
				buttonData.clickTime = unscaledTime;
				buttonData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
				if (buttonData.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(buttonData.pointerDrag, buttonData, ExecuteEvents.initializePotentialDrag);
				}
			}
			if (data.ReleasedThisFrame())
			{
				ExecuteEvents.Execute<IPointerUpHandler>(buttonData.pointerPress, buttonData, ExecuteEvents.pointerUpHandler);
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (buttonData.pointerPress == eventHandler && buttonData.eligibleForClick)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(buttonData.pointerPress, buttonData, ExecuteEvents.pointerClickHandler);
				}
				else if (buttonData.pointerDrag != null)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject, buttonData, ExecuteEvents.dropHandler);
				}
				buttonData.eligibleForClick = false;
				buttonData.pointerPress = null;
				buttonData.rawPointerPress = null;
				if (buttonData.pointerDrag != null && buttonData.dragging)
				{
					ExecuteEvents.Execute<IEndDragHandler>(buttonData.pointerDrag, buttonData, ExecuteEvents.endDragHandler);
				}
				buttonData.dragging = false;
				buttonData.pointerDrag = null;
				if (gameObject != buttonData.pointerEnter)
				{
					base.HandlePointerExitAndEnter(buttonData, null);
					base.HandlePointerExitAndEnter(buttonData, gameObject);
				}
			}
		}

		// Token: 0x04000078 RID: 120
		private float m_NextAction;

		// Token: 0x04000079 RID: 121
		private Vector2 m_LastMousePosition;

		// Token: 0x0400007A RID: 122
		private Vector2 m_MousePosition;

		// Token: 0x0400007B RID: 123
		[SerializeField]
		private string m_HorizontalAxis = "Horizontal";

		// Token: 0x0400007C RID: 124
		[SerializeField]
		private string m_VerticalAxis = "Vertical";

		// Token: 0x0400007D RID: 125
		[SerializeField]
		private string m_SubmitButton = "Submit";

		// Token: 0x0400007E RID: 126
		[SerializeField]
		private string m_CancelButton = "Cancel";

		// Token: 0x0400007F RID: 127
		[SerializeField]
		private float m_InputActionsPerSecond = 10f;

		// Token: 0x04000080 RID: 128
		[SerializeField]
		private bool m_AllowActivationOnMobileDevice;

		// Token: 0x0200002A RID: 42
		[Obsolete("Mode is no longer needed on input module as it handles both mouse and keyboard simultaneously.", false)]
		public enum InputMode
		{
			// Token: 0x04000082 RID: 130
			Mouse,
			// Token: 0x04000083 RID: 131
			Buttons
		}
	}
}
