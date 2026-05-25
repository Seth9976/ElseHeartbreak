using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200004C RID: 76
	[AddComponentMenu("UI/Input Field", 31)]
	public class InputField : Selectable, IEventSystemHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IUpdateSelectedHandler, ISubmitHandler, ICanvasElement
	{
		// Token: 0x0600020B RID: 523 RVA: 0x000095F0 File Offset: 0x000077F0
		protected InputField()
		{
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00009694 File Offset: 0x00007894
		protected TextGenerator cachedInputTextGenerator
		{
			get
			{
				if (this.m_InputTextCache == null)
				{
					this.m_InputTextCache = new TextGenerator();
				}
				return this.m_InputTextCache;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000096C4 File Offset: 0x000078C4
		// (set) Token: 0x0600020E RID: 526 RVA: 0x000096B4 File Offset: 0x000078B4
		public bool shouldHideMobileInput
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				switch (platform)
				{
				case RuntimePlatform.IPhonePlayer:
				case RuntimePlatform.Android:
					break;
				default:
					if (platform != RuntimePlatform.BB10Player)
					{
						return true;
					}
					break;
				}
				return this.m_HideMobileInput;
			}
			set
			{
				SetPropertyUtility.SetStruct<bool>(ref this.m_HideMobileInput, value);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00009704 File Offset: 0x00007904
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00009760 File Offset: 0x00007960
		public string text
		{
			get
			{
				if (InputField.m_Keyboard != null && InputField.m_Keyboard.active && !this.InPlaceEditing() && EventSystem.current.currentSelectedGameObject == base.gameObject)
				{
					return InputField.m_Keyboard.text;
				}
				return this.m_Text;
			}
			set
			{
				if (this.text == value)
				{
					return;
				}
				this.m_Text = value;
				if (InputField.m_Keyboard != null)
				{
					InputField.m_Keyboard.text = this.m_Text;
				}
				if (this.m_CaretPosition > this.m_Text.Length)
				{
					this.m_CaretPosition = (this.m_CaretSelectPosition = this.m_Text.Length);
				}
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000097D8 File Offset: 0x000079D8
		public bool isFocused
		{
			get
			{
				return this.m_AllowInput;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000097E0 File Offset: 0x000079E0
		// (set) Token: 0x06000214 RID: 532 RVA: 0x000097E8 File Offset: 0x000079E8
		public float caretBlinkRate
		{
			get
			{
				return this.m_CaretBlinkRate;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_CaretBlinkRate, value) && this.m_AllowInput)
				{
					this.SetCaretActive();
				}
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00009818 File Offset: 0x00007A18
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00009820 File Offset: 0x00007A20
		public Text textComponent
		{
			get
			{
				return this.m_TextComponent;
			}
			set
			{
				SetPropertyUtility.SetClass<Text>(ref this.m_TextComponent, value);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00009830 File Offset: 0x00007A30
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00009838 File Offset: 0x00007A38
		public Graphic placeholder
		{
			get
			{
				return this.m_Placeholder;
			}
			set
			{
				SetPropertyUtility.SetClass<Graphic>(ref this.m_Placeholder, value);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00009848 File Offset: 0x00007A48
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00009850 File Offset: 0x00007A50
		public Color selectionColor
		{
			get
			{
				return this.m_SelectionColor;
			}
			set
			{
				SetPropertyUtility.SetColor(ref this.m_SelectionColor, value);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00009860 File Offset: 0x00007A60
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00009868 File Offset: 0x00007A68
		public InputField.SubmitEvent onEndEdit
		{
			get
			{
				return this.m_EndEdit;
			}
			set
			{
				SetPropertyUtility.SetClass<InputField.SubmitEvent>(ref this.m_EndEdit, value);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00009878 File Offset: 0x00007A78
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00009880 File Offset: 0x00007A80
		public InputField.OnChangeEvent onValueChange
		{
			get
			{
				return this.m_OnValueChange;
			}
			set
			{
				SetPropertyUtility.SetClass<InputField.OnChangeEvent>(ref this.m_OnValueChange, value);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00009890 File Offset: 0x00007A90
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00009898 File Offset: 0x00007A98
		public InputField.OnValidateInput onValidateInput
		{
			get
			{
				return this.m_OnValidateInput;
			}
			set
			{
				SetPropertyUtility.SetClass<InputField.OnValidateInput>(ref this.m_OnValidateInput, value);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000098A8 File Offset: 0x00007AA8
		// (set) Token: 0x06000222 RID: 546 RVA: 0x000098B0 File Offset: 0x00007AB0
		public int characterLimit
		{
			get
			{
				return this.m_CharacterLimit;
			}
			set
			{
				SetPropertyUtility.SetStruct<int>(ref this.m_CharacterLimit, value);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000098C0 File Offset: 0x00007AC0
		// (set) Token: 0x06000224 RID: 548 RVA: 0x000098C8 File Offset: 0x00007AC8
		public InputField.ContentType contentType
		{
			get
			{
				return this.m_ContentType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.ContentType>(ref this.m_ContentType, value))
				{
					this.EnforceContentType();
				}
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000098E4 File Offset: 0x00007AE4
		// (set) Token: 0x06000226 RID: 550 RVA: 0x000098EC File Offset: 0x00007AEC
		public InputField.LineType lineType
		{
			get
			{
				return this.m_LineType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.LineType>(ref this.m_LineType, value))
				{
					this.SetToCustomIfContentTypeIsNot(new InputField.ContentType[]
					{
						InputField.ContentType.Standard,
						InputField.ContentType.Autocorrected
					});
				}
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00009910 File Offset: 0x00007B10
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00009918 File Offset: 0x00007B18
		public InputField.InputType inputType
		{
			get
			{
				return this.m_InputType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.InputType>(ref this.m_InputType, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00009934 File Offset: 0x00007B34
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000993C File Offset: 0x00007B3C
		public TouchScreenKeyboardType keyboardType
		{
			get
			{
				return this.m_KeyboardType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TouchScreenKeyboardType>(ref this.m_KeyboardType, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00009958 File Offset: 0x00007B58
		// (set) Token: 0x0600022C RID: 556 RVA: 0x00009960 File Offset: 0x00007B60
		public InputField.CharacterValidation characterValidation
		{
			get
			{
				return this.m_CharacterValidation;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.CharacterValidation>(ref this.m_CharacterValidation, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000997C File Offset: 0x00007B7C
		public bool multiLine
		{
			get
			{
				return this.m_LineType == InputField.LineType.MultiLineNewline || this.lineType == InputField.LineType.MultiLineSubmit;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00009998 File Offset: 0x00007B98
		// (set) Token: 0x0600022F RID: 559 RVA: 0x000099A0 File Offset: 0x00007BA0
		public char asteriskChar
		{
			get
			{
				return this.m_AsteriskChar;
			}
			set
			{
				SetPropertyUtility.SetStruct<char>(ref this.m_AsteriskChar, value);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000230 RID: 560 RVA: 0x000099B0 File Offset: 0x00007BB0
		public bool wasCanceled
		{
			get
			{
				return this.m_WasCanceled;
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000099B8 File Offset: 0x00007BB8
		protected void ClampPos(ref int pos)
		{
			if (pos < 0)
			{
				pos = 0;
			}
			else if (pos > this.text.Length)
			{
				pos = this.text.Length;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000232 RID: 562 RVA: 0x000099F4 File Offset: 0x00007BF4
		// (set) Token: 0x06000233 RID: 563 RVA: 0x00009A08 File Offset: 0x00007C08
		protected int caretPositionInternal
		{
			get
			{
				return this.m_CaretPosition + Input.compositionString.Length;
			}
			set
			{
				this.m_CaretPosition = value;
				this.ClampPos(ref this.m_CaretPosition);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00009A20 File Offset: 0x00007C20
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00009A34 File Offset: 0x00007C34
		protected int caretSelectPositionInternal
		{
			get
			{
				return this.m_CaretSelectPosition + Input.compositionString.Length;
			}
			set
			{
				this.m_CaretSelectPosition = value;
				this.ClampPos(ref this.m_CaretSelectPosition);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00009A4C File Offset: 0x00007C4C
		private bool hasSelection
		{
			get
			{
				return this.caretPositionInternal != this.caretSelectPositionInternal;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00009A60 File Offset: 0x00007C60
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00009A74 File Offset: 0x00007C74
		public int caretPosition
		{
			get
			{
				return this.m_CaretSelectPosition + Input.compositionString.Length;
			}
			set
			{
				this.selectionAnchorPosition = value;
				this.selectionFocusPosition = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00009A84 File Offset: 0x00007C84
		// (set) Token: 0x0600023A RID: 570 RVA: 0x00009A98 File Offset: 0x00007C98
		public int selectionAnchorPosition
		{
			get
			{
				return this.m_CaretPosition + Input.compositionString.Length;
			}
			set
			{
				if (Input.compositionString.Length != 0)
				{
					return;
				}
				this.m_CaretPosition = value;
				this.ClampPos(ref this.m_CaretPosition);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00009AC0 File Offset: 0x00007CC0
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00009AD4 File Offset: 0x00007CD4
		public int selectionFocusPosition
		{
			get
			{
				return this.m_CaretSelectPosition + Input.compositionString.Length;
			}
			set
			{
				if (Input.compositionString.Length != 0)
				{
					return;
				}
				this.m_CaretSelectPosition = value;
				this.ClampPos(ref this.m_CaretSelectPosition);
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00009AFC File Offset: 0x00007CFC
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_Text == null)
			{
				this.m_Text = string.Empty;
			}
			this.m_DrawStart = 0;
			this.m_DrawEnd = this.m_Text.Length;
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
				this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
				this.UpdateLabel();
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00009B84 File Offset: 0x00007D84
		protected override void OnDisable()
		{
			this.m_BlinkCoroutine = null;
			this.DeactivateInputField();
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
				this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
			}
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.m_CachedInputRenderer)
			{
				this.m_CachedInputRenderer.SetVertices(null, 0);
			}
			base.OnDisable();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00009C08 File Offset: 0x00007E08
		private IEnumerator CaretBlink()
		{
			this.m_CaretVisible = true;
			yield return null;
			while (this.isFocused && this.m_CaretBlinkRate > 0f)
			{
				float blinkPeriod = 1f / this.m_CaretBlinkRate;
				bool blinkState = (Time.unscaledTime - this.m_BlinkStartTime) % blinkPeriod < blinkPeriod / 2f;
				if (this.m_CaretVisible != blinkState)
				{
					this.m_CaretVisible = blinkState;
					this.UpdateGeometry();
				}
				yield return null;
			}
			this.m_BlinkCoroutine = null;
			yield break;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00009C24 File Offset: 0x00007E24
		private void SetCaretVisible()
		{
			if (!this.m_AllowInput)
			{
				return;
			}
			this.m_CaretVisible = true;
			this.m_BlinkStartTime = Time.unscaledTime;
			this.SetCaretActive();
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009C58 File Offset: 0x00007E58
		private void SetCaretActive()
		{
			if (!this.m_AllowInput)
			{
				return;
			}
			if (this.m_CaretBlinkRate > 0f)
			{
				if (this.m_BlinkCoroutine == null)
				{
					this.m_BlinkCoroutine = base.StartCoroutine(this.CaretBlink());
				}
			}
			else
			{
				this.m_CaretVisible = true;
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00009CAC File Offset: 0x00007EAC
		protected void OnFocus()
		{
			this.SelectAll();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009CB4 File Offset: 0x00007EB4
		protected void SelectAll()
		{
			this.caretPositionInternal = this.text.Length;
			this.caretSelectPositionInternal = 0;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009CDC File Offset: 0x00007EDC
		public void MoveTextEnd(bool shift)
		{
			int length = this.text.Length;
			if (shift)
			{
				this.caretSelectPositionInternal = length;
			}
			else
			{
				this.caretPositionInternal = length;
				this.caretSelectPositionInternal = this.caretPositionInternal;
			}
			this.UpdateLabel();
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009D20 File Offset: 0x00007F20
		public void MoveTextStart(bool shift)
		{
			int num = 0;
			if (shift)
			{
				this.caretSelectPositionInternal = num;
			}
			else
			{
				this.caretPositionInternal = num;
				this.caretSelectPositionInternal = this.caretPositionInternal;
			}
			this.UpdateLabel();
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00009D5C File Offset: 0x00007F5C
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00009D84 File Offset: 0x00007F84
		private static string clipboard
		{
			get
			{
				TextEditor textEditor = new TextEditor();
				textEditor.Paste();
				return textEditor.content.text;
			}
			set
			{
				TextEditor textEditor = new TextEditor();
				textEditor.content = new GUIContent(value);
				textEditor.OnFocus();
				textEditor.Copy();
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00009DB0 File Offset: 0x00007FB0
		private bool InPlaceEditing()
		{
			return !TouchScreenKeyboard.isSupported;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009DBC File Offset: 0x00007FBC
		protected virtual void LateUpdate()
		{
			if (this.m_ShouldActivateNextUpdate)
			{
				if (!this.isFocused)
				{
					this.ActivateInputFieldInternal();
					this.m_ShouldActivateNextUpdate = false;
					return;
				}
				this.m_ShouldActivateNextUpdate = false;
			}
			if (this.InPlaceEditing() || !this.isFocused)
			{
				return;
			}
			this.AssignPositioningIfNeeded();
			if (InputField.m_Keyboard == null || !InputField.m_Keyboard.active)
			{
				if (InputField.m_Keyboard != null && InputField.m_Keyboard.wasCanceled)
				{
					this.m_WasCanceled = true;
				}
				this.OnDeselect(null);
				return;
			}
			string text = InputField.m_Keyboard.text;
			if (this.m_Text != text)
			{
				this.m_Text = string.Empty;
				foreach (char c in text)
				{
					if (c == '\r' || c == '\u0003')
					{
						c = '\n';
					}
					if (this.onValidateInput != null)
					{
						c = this.onValidateInput(this.m_Text, this.m_Text.Length, c);
					}
					else if (this.characterValidation != InputField.CharacterValidation.None)
					{
						c = this.Validate(this.m_Text, this.m_Text.Length, c);
					}
					if (this.lineType == InputField.LineType.MultiLineSubmit && c == '\n')
					{
						InputField.m_Keyboard.text = this.m_Text;
						this.OnDeselect(null);
						return;
					}
					if (c != '\0')
					{
						this.m_Text += c;
					}
				}
				if (this.characterLimit > 0 && this.m_Text.Length > this.characterLimit)
				{
					this.m_Text = this.m_Text.Substring(0, this.characterLimit);
				}
				int length = this.m_Text.Length;
				this.caretSelectPositionInternal = length;
				this.caretPositionInternal = length;
				if (this.m_Text != text)
				{
					InputField.m_Keyboard.text = this.m_Text;
				}
				this.SendOnValueChangedAndUpdateLabel();
			}
			if (InputField.m_Keyboard.done)
			{
				if (InputField.m_Keyboard.wasCanceled)
				{
					this.m_WasCanceled = true;
				}
				this.OnDeselect(null);
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009FEC File Offset: 0x000081EC
		public Vector2 ScreenToLocal(Vector2 screen)
		{
			Canvas canvas = this.m_TextComponent.canvas;
			if (canvas == null)
			{
				return screen;
			}
			Vector3 vector = Vector3.zero;
			if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				vector = this.m_TextComponent.transform.InverseTransformPoint(screen);
			}
			else if (canvas.worldCamera != null)
			{
				Ray ray = canvas.worldCamera.ScreenPointToRay(screen);
				Plane plane = new Plane(this.m_TextComponent.transform.forward, this.m_TextComponent.transform.position);
				float num;
				plane.Raycast(ray, out num);
				vector = this.m_TextComponent.transform.InverseTransformPoint(ray.GetPoint(num));
			}
			return new Vector2(vector.x, vector.y);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000A0C4 File Offset: 0x000082C4
		private int GetUnclampedCharacterLineFromPosition(Vector2 pos, TextGenerator generator)
		{
			if (!this.multiLine)
			{
				return 0;
			}
			float num = this.m_TextComponent.rectTransform.rect.yMax;
			if (pos.y > num)
			{
				return -1;
			}
			for (int i = 0; i < generator.lineCount; i++)
			{
				float num2 = (float)generator.lines[i].height / this.m_TextComponent.pixelsPerUnit;
				if (pos.y <= num && pos.y > num - num2)
				{
					return i;
				}
				num -= num2;
			}
			return generator.lineCount;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000A168 File Offset: 0x00008368
		protected int GetCharacterIndexFromPosition(Vector2 pos)
		{
			TextGenerator cachedTextGenerator = this.m_TextComponent.cachedTextGenerator;
			if (cachedTextGenerator.lineCount == 0)
			{
				return 0;
			}
			int unclampedCharacterLineFromPosition = this.GetUnclampedCharacterLineFromPosition(pos, cachedTextGenerator);
			if (unclampedCharacterLineFromPosition < 0)
			{
				return 0;
			}
			if (unclampedCharacterLineFromPosition >= cachedTextGenerator.lineCount)
			{
				return cachedTextGenerator.characterCountVisible;
			}
			int startCharIdx = cachedTextGenerator.lines[unclampedCharacterLineFromPosition].startCharIdx;
			int lineEndPosition = InputField.GetLineEndPosition(cachedTextGenerator, unclampedCharacterLineFromPosition);
			for (int i = startCharIdx; i < lineEndPosition; i++)
			{
				if (i >= cachedTextGenerator.characterCountVisible)
				{
					break;
				}
				UICharInfo uicharInfo = cachedTextGenerator.characters[i];
				Vector2 vector = uicharInfo.cursorPos / this.m_TextComponent.pixelsPerUnit;
				float num = pos.x - vector.x;
				float num2 = vector.x + uicharInfo.charWidth / this.m_TextComponent.pixelsPerUnit - pos.x;
				if (num < num2)
				{
					return i;
				}
			}
			return lineEndPosition;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000A264 File Offset: 0x00008464
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left && this.m_TextComponent != null && InputField.m_Keyboard == null;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000A2B0 File Offset: 0x000084B0
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_UpdateDrag = true;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000A2C8 File Offset: 0x000084C8
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.textComponent.rectTransform, eventData.position, eventData.pressEventCamera, out vector);
			this.caretSelectPositionInternal = this.GetCharacterIndexFromPosition(vector) + this.m_DrawStart;
			this.MarkGeometryAsDirty();
			this.m_DragPositionOutOfBounds = !RectTransformUtility.RectangleContainsScreenPoint(this.textComponent.rectTransform, eventData.position, eventData.pressEventCamera);
			if (this.m_DragPositionOutOfBounds && this.m_DragCoroutine == null)
			{
				this.m_DragCoroutine = base.StartCoroutine(this.MouseDragOutsideRect(eventData));
			}
			eventData.Use();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000A370 File Offset: 0x00008570
		private IEnumerator MouseDragOutsideRect(PointerEventData eventData)
		{
			while (this.m_UpdateDrag && this.m_DragPositionOutOfBounds)
			{
				Vector2 localMousePos;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.textComponent.rectTransform, eventData.position, eventData.pressEventCamera, out localMousePos);
				Rect rect = this.textComponent.rectTransform.rect;
				if (this.multiLine)
				{
					if (localMousePos.y > rect.yMax)
					{
						this.MoveUp(true, true);
					}
					else if (localMousePos.y < rect.yMin)
					{
						this.MoveDown(true, true);
					}
				}
				else if (localMousePos.x < rect.xMin)
				{
					this.MoveLeft(true, false);
				}
				else if (localMousePos.x > rect.xMax)
				{
					this.MoveRight(true, false);
				}
				this.UpdateLabel();
				float delay = ((!this.multiLine) ? 0.05f : 0.1f);
				yield return new WaitForSeconds(delay);
			}
			this.m_DragCoroutine = null;
			yield break;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000A39C File Offset: 0x0000859C
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_UpdateDrag = false;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000A3B4 File Offset: 0x000085B4
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
			bool allowInput = this.m_AllowInput;
			base.OnPointerDown(eventData);
			if (!this.InPlaceEditing() && (InputField.m_Keyboard == null || !InputField.m_Keyboard.active))
			{
				this.OnSelect(eventData);
				return;
			}
			if (allowInput)
			{
				Vector2 vector = this.ScreenToLocal(eventData.position);
				int num = this.GetCharacterIndexFromPosition(vector) + this.m_DrawStart;
				this.caretPositionInternal = num;
				this.caretSelectPositionInternal = num;
			}
			this.UpdateLabel();
			eventData.Use();
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000A458 File Offset: 0x00008658
		protected InputField.EditState KeyPressed(Event evt)
		{
			EventModifiers modifiers = evt.modifiers;
			RuntimePlatform platform = Application.platform;
			bool flag = platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.OSXPlayer || platform == RuntimePlatform.OSXWebPlayer;
			bool flag2 = ((!flag) ? ((modifiers & EventModifiers.Control) != EventModifiers.None) : ((modifiers & EventModifiers.Command) != EventModifiers.None));
			bool flag3 = (modifiers & EventModifiers.Shift) != EventModifiers.None;
			bool flag4 = (modifiers & EventModifiers.Alt) != EventModifiers.None;
			bool flag5 = flag2 && !flag4 && !flag3;
			KeyCode keyCode = evt.keyCode;
			switch (keyCode)
			{
			case KeyCode.KeypadEnter:
				break;
			default:
				switch (keyCode)
				{
				case KeyCode.A:
					if (flag5)
					{
						this.SelectAll();
						return InputField.EditState.Continue;
					}
					goto IL_01CF;
				default:
					switch (keyCode)
					{
					case KeyCode.V:
						if (flag5)
						{
							this.Append(InputField.clipboard);
							return InputField.EditState.Continue;
						}
						goto IL_01CF;
					default:
						if (keyCode == KeyCode.Backspace)
						{
							this.Backspace();
							return InputField.EditState.Continue;
						}
						if (keyCode != KeyCode.Return)
						{
							if (keyCode == KeyCode.Escape)
							{
								this.m_WasCanceled = true;
								return InputField.EditState.Finish;
							}
							if (keyCode != KeyCode.Delete)
							{
								goto IL_01CF;
							}
							this.ForwardSpace();
							return InputField.EditState.Continue;
						}
						break;
					case KeyCode.X:
						if (flag5)
						{
							InputField.clipboard = this.GetSelectedString();
							this.Delete();
							this.SendOnValueChangedAndUpdateLabel();
							return InputField.EditState.Continue;
						}
						goto IL_01CF;
					}
					break;
				case KeyCode.C:
					if (flag5)
					{
						InputField.clipboard = this.GetSelectedString();
						return InputField.EditState.Continue;
					}
					goto IL_01CF;
				}
				break;
			case KeyCode.UpArrow:
				this.MoveUp(flag3);
				return InputField.EditState.Continue;
			case KeyCode.DownArrow:
				this.MoveDown(flag3);
				return InputField.EditState.Continue;
			case KeyCode.RightArrow:
				this.MoveRight(flag3, flag2);
				return InputField.EditState.Continue;
			case KeyCode.LeftArrow:
				this.MoveLeft(flag3, flag2);
				return InputField.EditState.Continue;
			case KeyCode.Home:
				this.MoveTextStart(flag3);
				return InputField.EditState.Continue;
			case KeyCode.End:
				this.MoveTextEnd(flag3);
				return InputField.EditState.Continue;
			}
			if (this.lineType != InputField.LineType.MultiLineNewline)
			{
				return InputField.EditState.Finish;
			}
			IL_01CF:
			if (!this.multiLine && evt.character == '\t')
			{
				return InputField.EditState.Continue;
			}
			char c = evt.character;
			if (c == '\r' || c == '\u0003')
			{
				c = '\n';
			}
			if (this.IsValidChar(c))
			{
				this.Append(c);
			}
			if (c == '\0' && Input.compositionString.Length > 0)
			{
				this.UpdateLabel();
			}
			return InputField.EditState.Continue;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000A6A0 File Offset: 0x000088A0
		private bool IsValidChar(char c)
		{
			return c != '\u007f' && (c == '\t' || c == '\n' || this.m_TextComponent.font.HasCharacter(c));
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000A6D0 File Offset: 0x000088D0
		public void ProcessEvent(Event e)
		{
			this.KeyPressed(e);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000A6DC File Offset: 0x000088DC
		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			if (!this.isFocused)
			{
				return;
			}
			bool flag = false;
			while (Event.PopEvent(this.m_ProcessingEvent))
			{
				if (this.m_ProcessingEvent.rawType == EventType.KeyDown)
				{
					flag = true;
					InputField.EditState editState = this.KeyPressed(this.m_ProcessingEvent);
					if (editState == InputField.EditState.Finish)
					{
						this.DeactivateInputField();
						break;
					}
				}
			}
			if (flag)
			{
				this.UpdateLabel();
			}
			eventData.Use();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000A750 File Offset: 0x00008950
		private string GetSelectedString()
		{
			if (!this.hasSelection)
			{
				return string.Empty;
			}
			int num = this.caretPositionInternal;
			int num2 = this.caretSelectPositionInternal;
			if (num > num2)
			{
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = num; i < num2; i++)
			{
				stringBuilder.Append(this.text[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000A7C0 File Offset: 0x000089C0
		private int FindtNextWordBegin()
		{
			if (this.caretSelectPositionInternal + 1 >= this.text.Length)
			{
				return this.text.Length;
			}
			int num = this.text.IndexOfAny(InputField.kSeparators, this.caretSelectPositionInternal + 1);
			if (num == -1)
			{
				num = this.text.Length;
			}
			else
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000A828 File Offset: 0x00008A28
		private void MoveRight(bool shift, bool ctrl)
		{
			if (this.hasSelection && !shift)
			{
				int num = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal);
				this.caretSelectPositionInternal = num;
				this.caretPositionInternal = num;
				return;
			}
			int num2;
			if (ctrl)
			{
				num2 = this.FindtNextWordBegin();
			}
			else
			{
				num2 = this.caretSelectPositionInternal + 1;
			}
			if (shift)
			{
				this.caretSelectPositionInternal = num2;
			}
			else
			{
				int num = num2;
				this.caretPositionInternal = num;
				this.caretSelectPositionInternal = num;
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000A8A4 File Offset: 0x00008AA4
		private int FindtPrevWordBegin()
		{
			if (this.caretSelectPositionInternal - 2 < 0)
			{
				return 0;
			}
			int num = this.text.LastIndexOfAny(InputField.kSeparators, this.caretSelectPositionInternal - 2);
			if (num == -1)
			{
				num = 0;
			}
			else
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		private void MoveLeft(bool shift, bool ctrl)
		{
			if (this.hasSelection && !shift)
			{
				int num = Mathf.Min(this.caretPositionInternal, this.caretSelectPositionInternal);
				this.caretSelectPositionInternal = num;
				this.caretPositionInternal = num;
				return;
			}
			int num2;
			if (ctrl)
			{
				num2 = this.FindtPrevWordBegin();
			}
			else
			{
				num2 = this.caretSelectPositionInternal - 1;
			}
			if (shift)
			{
				this.caretSelectPositionInternal = num2;
			}
			else
			{
				int num = num2;
				this.caretPositionInternal = num;
				this.caretSelectPositionInternal = num;
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000A96C File Offset: 0x00008B6C
		private int DetermineCharacterLine(int charPos, TextGenerator generator)
		{
			if (!this.multiLine)
			{
				return 0;
			}
			for (int i = 0; i < generator.lineCount - 1; i++)
			{
				if (generator.lines[i + 1].startCharIdx > charPos)
				{
					return i;
				}
			}
			return generator.lineCount - 1;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000A9C8 File Offset: 0x00008BC8
		private int LineUpCharacterPosition(int originalPos, bool goToFirstChar)
		{
			if (originalPos >= this.cachedInputTextGenerator.characterCountVisible)
			{
				return 0;
			}
			UICharInfo uicharInfo = this.cachedInputTextGenerator.characters[originalPos];
			int num = this.DetermineCharacterLine(originalPos, this.cachedInputTextGenerator);
			if (num - 1 < 0)
			{
				return (!goToFirstChar) ? originalPos : 0;
			}
			int num2 = this.cachedInputTextGenerator.lines[num].startCharIdx - 1;
			for (int i = this.cachedInputTextGenerator.lines[num - 1].startCharIdx; i < num2; i++)
			{
				if (this.cachedInputTextGenerator.characters[i].cursorPos.x >= uicharInfo.cursorPos.x)
				{
					return i;
				}
			}
			return num2;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000AA9C File Offset: 0x00008C9C
		private int LineDownCharacterPosition(int originalPos, bool goToLastChar)
		{
			if (originalPos >= this.cachedInputTextGenerator.characterCountVisible)
			{
				return this.text.Length;
			}
			UICharInfo uicharInfo = this.cachedInputTextGenerator.characters[originalPos];
			int num = this.DetermineCharacterLine(originalPos, this.cachedInputTextGenerator);
			if (num + 1 >= this.cachedInputTextGenerator.lineCount)
			{
				return (!goToLastChar) ? originalPos : this.text.Length;
			}
			int lineEndPosition = InputField.GetLineEndPosition(this.cachedInputTextGenerator, num + 1);
			for (int i = this.cachedInputTextGenerator.lines[num + 1].startCharIdx; i < lineEndPosition; i++)
			{
				if (this.cachedInputTextGenerator.characters[i].cursorPos.x >= uicharInfo.cursorPos.x)
				{
					return i;
				}
			}
			return lineEndPosition;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000AB80 File Offset: 0x00008D80
		private void MoveDown(bool shift)
		{
			this.MoveDown(shift, true);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000AB8C File Offset: 0x00008D8C
		private void MoveDown(bool shift, bool goToLastChar)
		{
			if (this.hasSelection && !shift)
			{
				int num = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal);
				this.caretSelectPositionInternal = num;
				this.caretPositionInternal = num;
			}
			int num2 = ((!this.multiLine) ? this.text.Length : this.LineDownCharacterPosition(this.caretSelectPositionInternal, goToLastChar));
			if (shift)
			{
				this.caretSelectPositionInternal = num2;
			}
			else
			{
				int num = num2;
				this.caretSelectPositionInternal = num;
				this.caretPositionInternal = num;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000AC18 File Offset: 0x00008E18
		private void MoveUp(bool shift)
		{
			this.MoveUp(shift, true);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000AC24 File Offset: 0x00008E24
		private void MoveUp(bool shift, bool goToFirstChar)
		{
			if (this.hasSelection && !shift)
			{
				int num = Mathf.Min(this.caretPositionInternal, this.caretSelectPositionInternal);
				this.caretSelectPositionInternal = num;
				this.caretPositionInternal = num;
			}
			int num2 = ((!this.multiLine) ? 0 : this.LineUpCharacterPosition(this.caretSelectPositionInternal, goToFirstChar));
			if (shift)
			{
				this.caretSelectPositionInternal = num2;
			}
			else
			{
				int num = num2;
				this.caretPositionInternal = num;
				this.caretSelectPositionInternal = num;
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000ACA4 File Offset: 0x00008EA4
		private void Delete()
		{
			if (this.caretPositionInternal == this.caretSelectPositionInternal)
			{
				return;
			}
			if (this.caretPositionInternal < this.caretSelectPositionInternal)
			{
				this.m_Text = this.text.Substring(0, this.caretPositionInternal) + this.text.Substring(this.caretSelectPositionInternal, this.text.Length - this.caretSelectPositionInternal);
				this.caretSelectPositionInternal = this.caretPositionInternal;
			}
			else
			{
				this.m_Text = this.text.Substring(0, this.caretSelectPositionInternal) + this.text.Substring(this.caretPositionInternal, this.text.Length - this.caretPositionInternal);
				this.caretPositionInternal = this.caretSelectPositionInternal;
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000AD74 File Offset: 0x00008F74
		private void ForwardSpace()
		{
			if (this.hasSelection)
			{
				this.Delete();
				this.SendOnValueChangedAndUpdateLabel();
			}
			else if (this.caretPositionInternal < this.text.Length)
			{
				this.m_Text = this.text.Remove(this.caretPositionInternal, 1);
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000ADD4 File Offset: 0x00008FD4
		private void Backspace()
		{
			if (this.hasSelection)
			{
				this.Delete();
				this.SendOnValueChangedAndUpdateLabel();
			}
			else if (this.caretPositionInternal > 0)
			{
				this.m_Text = this.text.Remove(this.caretPositionInternal - 1, 1);
				int num = this.caretPositionInternal - 1;
				this.caretPositionInternal = num;
				this.caretSelectPositionInternal = num;
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000AE40 File Offset: 0x00009040
		private void Insert(char c)
		{
			string text = c.ToString();
			this.Delete();
			if (this.characterLimit > 0 && this.text.Length >= this.characterLimit)
			{
				return;
			}
			this.m_Text = this.text.Insert(this.m_CaretPosition, text);
			this.caretSelectPositionInternal = (this.caretPositionInternal += text.Length);
			this.SendOnValueChanged();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000AEB8 File Offset: 0x000090B8
		private void SendOnValueChangedAndUpdateLabel()
		{
			this.SendOnValueChanged();
			this.UpdateLabel();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000AEC8 File Offset: 0x000090C8
		private void SendOnValueChanged()
		{
			if (this.onValueChange != null)
			{
				this.onValueChange.Invoke(this.text);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000AEF4 File Offset: 0x000090F4
		protected void SendOnSubmit()
		{
			if (this.onEndEdit != null)
			{
				this.onEndEdit.Invoke(this.m_Text);
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000AF14 File Offset: 0x00009114
		protected virtual void Append(string input)
		{
			if (!this.InPlaceEditing())
			{
				return;
			}
			int i = 0;
			int length = input.Length;
			while (i < length)
			{
				char c = input[i];
				if (c >= ' ')
				{
					this.Append(c);
				}
				i++;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000AF60 File Offset: 0x00009160
		protected virtual void Append(char input)
		{
			if (!this.InPlaceEditing())
			{
				return;
			}
			if (this.onValidateInput != null)
			{
				input = this.onValidateInput(this.text, this.caretPositionInternal, input);
			}
			else if (this.characterValidation != InputField.CharacterValidation.None)
			{
				input = this.Validate(this.text, this.caretPositionInternal, input);
			}
			if (input == '\0')
			{
				return;
			}
			this.Insert(input);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000AFD4 File Offset: 0x000091D4
		protected void UpdateLabel()
		{
			if (this.m_TextComponent != null && this.m_TextComponent.font != null && !this.m_PreventFontCallback)
			{
				string text;
				if (Input.compositionString.Length > 0)
				{
					text = this.text.Substring(0, this.m_CaretPosition) + Input.compositionString + this.text.Substring(this.m_CaretPosition);
				}
				else
				{
					text = this.text;
				}
				string text2;
				if (this.inputType == InputField.InputType.Password)
				{
					text2 = new string(this.asteriskChar, text.Length);
				}
				else
				{
					text2 = text;
				}
				bool flag = string.IsNullOrEmpty(text);
				if (this.m_Placeholder != null)
				{
					this.m_Placeholder.enabled = flag;
				}
				if (!this.m_AllowInput)
				{
					this.m_DrawStart = 0;
					this.m_DrawEnd = this.m_Text.Length;
				}
				if (!flag)
				{
					Vector2 size = this.m_TextComponent.rectTransform.rect.size;
					TextGenerationSettings generationSettings = this.m_TextComponent.GetGenerationSettings(size);
					generationSettings.generateOutOfBounds = true;
					this.m_PreventFontCallback = true;
					this.cachedInputTextGenerator.Populate(text2, generationSettings);
					this.m_PreventFontCallback = false;
					this.SetDrawRangeToContainCaretPosition(this.cachedInputTextGenerator, this.caretSelectPositionInternal, ref this.m_DrawStart, ref this.m_DrawEnd);
					text2 = text2.Substring(this.m_DrawStart, Mathf.Min(this.m_DrawEnd, text2.Length) - this.m_DrawStart);
					this.SetCaretVisible();
				}
				this.m_TextComponent.text = text2;
				this.MarkGeometryAsDirty();
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000B178 File Offset: 0x00009378
		private bool IsSelectionVisible()
		{
			return this.m_DrawStart <= this.caretPositionInternal && this.m_DrawStart <= this.caretSelectPositionInternal && this.m_DrawEnd >= this.caretPositionInternal && this.m_DrawEnd >= this.caretSelectPositionInternal;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000B1D0 File Offset: 0x000093D0
		private static int GetLineStartPosition(TextGenerator gen, int line)
		{
			line = Mathf.Clamp(line, 0, gen.lines.Count - 1);
			return gen.lines[line].startCharIdx;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000B208 File Offset: 0x00009408
		private static int GetLineEndPosition(TextGenerator gen, int line)
		{
			line = Mathf.Max(line, 0);
			if (line + 1 < gen.lines.Count)
			{
				return gen.lines[line + 1].startCharIdx;
			}
			return gen.characterCountVisible;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000B250 File Offset: 0x00009450
		private void SetDrawRangeToContainCaretPosition(TextGenerator gen, int caretPos, ref int drawStart, ref int drawEnd)
		{
			Vector2 size = gen.rectExtents.size;
			if (this.multiLine)
			{
				IList<UILineInfo> lines = gen.lines;
				int num = this.DetermineCharacterLine(caretPos, gen);
				int num2 = (int)size.y;
				if (drawEnd <= caretPos)
				{
					drawEnd = InputField.GetLineEndPosition(gen, num);
					int num3 = num;
					while (num3 >= 0 && num3 < lines.Count)
					{
						num2 -= lines[num3].height;
						if (num2 < 0)
						{
							break;
						}
						drawStart = InputField.GetLineStartPosition(gen, num3);
						num3--;
					}
				}
				else
				{
					if (drawStart > caretPos)
					{
						drawStart = InputField.GetLineStartPosition(gen, num);
					}
					int num4 = this.DetermineCharacterLine(drawStart, gen);
					int num5 = num4;
					drawEnd = InputField.GetLineEndPosition(gen, num5);
					num2 -= lines[num5].height;
					for (;;)
					{
						if (num5 < lines.Count - 1)
						{
							num5++;
							if (num2 < lines[num5].height)
							{
								break;
							}
							drawEnd = InputField.GetLineEndPosition(gen, num5);
							num2 -= lines[num5].height;
						}
						else
						{
							if (num4 <= 0)
							{
								break;
							}
							num4--;
							if (num2 < lines[num4].height)
							{
								break;
							}
							drawStart = InputField.GetLineStartPosition(gen, num4);
							num2 -= lines[num4].height;
						}
					}
				}
			}
			else
			{
				float num6 = size.x;
				IList<UICharInfo> characters = gen.characters;
				if (drawEnd <= caretPos)
				{
					drawEnd = Mathf.Min(caretPos, gen.characterCountVisible);
					drawStart = 0;
					for (int i = drawEnd; i > 0; i--)
					{
						num6 -= characters[i - 1].charWidth;
						if (num6 < 0f)
						{
							drawStart = i;
							break;
						}
					}
				}
				else
				{
					if (drawStart > caretPos)
					{
						drawStart = caretPos;
					}
					drawEnd = gen.characterCountVisible;
					for (int j = drawStart; j < gen.characterCountVisible; j++)
					{
						num6 -= characters[j].charWidth;
						if (num6 < 0f)
						{
							drawEnd = j;
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000B4BC File Offset: 0x000096BC
		private void MarkGeometryAsDirty()
		{
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000B4C4 File Offset: 0x000096C4
		public virtual void Rebuild(CanvasUpdate update)
		{
			if (update == CanvasUpdate.LatePreRender)
			{
				this.UpdateGeometry();
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000B4EC File Offset: 0x000096EC
		private void UpdateGeometry()
		{
			if (!this.shouldHideMobileInput)
			{
				return;
			}
			if (this.m_CachedInputRenderer == null && this.m_TextComponent != null)
			{
				GameObject gameObject = new GameObject(base.transform.name + " Input Caret");
				gameObject.hideFlags = HideFlags.DontSave;
				gameObject.transform.SetParent(this.m_TextComponent.transform.parent);
				gameObject.transform.SetAsFirstSibling();
				gameObject.layer = base.gameObject.layer;
				this.caretRectTrans = gameObject.AddComponent<RectTransform>();
				this.m_CachedInputRenderer = gameObject.AddComponent<CanvasRenderer>();
				this.m_CachedInputRenderer.SetMaterial(Graphic.defaultGraphicMaterial, null);
				gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
				this.AssignPositioningIfNeeded();
			}
			if (this.m_CachedInputRenderer == null)
			{
				return;
			}
			this.OnFillVBO(this.m_Vbo);
			if (this.m_Vbo.Count == 0)
			{
				this.m_CachedInputRenderer.SetVertices(null, 0);
			}
			else
			{
				this.m_CachedInputRenderer.SetVertices(this.m_Vbo.ToArray(), this.m_Vbo.Count);
			}
			this.m_Vbo.Clear();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000B628 File Offset: 0x00009828
		private void AssignPositioningIfNeeded()
		{
			if (this.m_TextComponent != null && this.caretRectTrans != null && (this.caretRectTrans.localPosition != this.m_TextComponent.rectTransform.localPosition || this.caretRectTrans.localRotation != this.m_TextComponent.rectTransform.localRotation || this.caretRectTrans.localScale != this.m_TextComponent.rectTransform.localScale || this.caretRectTrans.anchorMin != this.m_TextComponent.rectTransform.anchorMin || this.caretRectTrans.anchorMax != this.m_TextComponent.rectTransform.anchorMax || this.caretRectTrans.anchoredPosition != this.m_TextComponent.rectTransform.anchoredPosition || this.caretRectTrans.sizeDelta != this.m_TextComponent.rectTransform.sizeDelta || this.caretRectTrans.pivot != this.m_TextComponent.rectTransform.pivot))
			{
				this.caretRectTrans.localPosition = this.m_TextComponent.rectTransform.localPosition;
				this.caretRectTrans.localRotation = this.m_TextComponent.rectTransform.localRotation;
				this.caretRectTrans.localScale = this.m_TextComponent.rectTransform.localScale;
				this.caretRectTrans.anchorMin = this.m_TextComponent.rectTransform.anchorMin;
				this.caretRectTrans.anchorMax = this.m_TextComponent.rectTransform.anchorMax;
				this.caretRectTrans.anchoredPosition = this.m_TextComponent.rectTransform.anchoredPosition;
				this.caretRectTrans.sizeDelta = this.m_TextComponent.rectTransform.sizeDelta;
				this.caretRectTrans.pivot = this.m_TextComponent.rectTransform.pivot;
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000B858 File Offset: 0x00009A58
		private void OnFillVBO(List<UIVertex> vbo)
		{
			if (!this.isFocused)
			{
				return;
			}
			Rect rect = this.m_TextComponent.rectTransform.rect;
			Vector2 size = rect.size;
			Vector2 textAnchorPivot = Text.GetTextAnchorPivot(this.m_TextComponent.alignment);
			Vector2 zero = Vector2.zero;
			zero.x = Mathf.Lerp(rect.xMin, rect.xMax, textAnchorPivot.x);
			zero.y = Mathf.Lerp(rect.yMin, rect.yMax, textAnchorPivot.y);
			Vector2 vector = this.m_TextComponent.PixelAdjustPoint(zero);
			Vector2 vector2 = vector - zero + Vector2.Scale(size, textAnchorPivot);
			vector2.x -= Mathf.Floor(0.5f + vector2.x);
			vector2.y -= Mathf.Floor(0.5f + vector2.y);
			if (!this.hasSelection)
			{
				this.GenerateCursor(vbo, vector2);
			}
			else
			{
				this.GenerateHightlight(vbo, vector2);
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000B96C File Offset: 0x00009B6C
		private void GenerateCursor(List<UIVertex> vbo, Vector2 roundingOffset)
		{
			if (!this.m_CaretVisible)
			{
				return;
			}
			if (this.m_CursorVerts == null)
			{
				this.CreateCursorVerts();
			}
			float num = 1f;
			float num2 = (float)this.m_TextComponent.fontSize;
			int num3 = Mathf.Max(0, this.caretPositionInternal - this.m_DrawStart);
			TextGenerator cachedTextGenerator = this.m_TextComponent.cachedTextGenerator;
			if (cachedTextGenerator == null)
			{
				return;
			}
			if (this.m_TextComponent.resizeTextForBestFit)
			{
				num2 = (float)cachedTextGenerator.fontSizeUsedForBestFit / this.m_TextComponent.pixelsPerUnit;
			}
			Vector2 zero = Vector2.zero;
			if (cachedTextGenerator.characterCountVisible + 1 > num3 || num3 == 0)
			{
				UICharInfo uicharInfo = cachedTextGenerator.characters[num3];
				zero.x = uicharInfo.cursorPos.x;
				zero.y = uicharInfo.cursorPos.y;
			}
			zero.x /= this.m_TextComponent.pixelsPerUnit;
			if (zero.x > this.m_TextComponent.rectTransform.rect.xMax)
			{
				zero.x = this.m_TextComponent.rectTransform.rect.xMax;
			}
			this.m_CursorVerts[0].position = new Vector3(zero.x, zero.y - num2, 0f);
			this.m_CursorVerts[1].position = new Vector3(zero.x + num, zero.y - num2, 0f);
			this.m_CursorVerts[2].position = new Vector3(zero.x + num, zero.y, 0f);
			this.m_CursorVerts[3].position = new Vector3(zero.x, zero.y, 0f);
			if (roundingOffset != Vector2.zero)
			{
				for (int i = 0; i < this.m_CursorVerts.Length; i++)
				{
					UIVertex uivertex = this.m_CursorVerts[i];
					uivertex.position.x = uivertex.position.x + roundingOffset.x;
					uivertex.position.y = uivertex.position.y + roundingOffset.y;
					vbo.Add(uivertex);
				}
			}
			else
			{
				for (int j = 0; j < this.m_CursorVerts.Length; j++)
				{
					vbo.Add(this.m_CursorVerts[j]);
				}
			}
			zero.y = (float)Screen.height - zero.y;
			Input.compositionCursorPos = zero;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000BC20 File Offset: 0x00009E20
		private void CreateCursorVerts()
		{
			this.m_CursorVerts = new UIVertex[4];
			for (int i = 0; i < this.m_CursorVerts.Length; i++)
			{
				this.m_CursorVerts[i] = UIVertex.simpleVert;
				this.m_CursorVerts[i].color = this.m_TextComponent.color;
				this.m_CursorVerts[i].uv0 = Vector2.zero;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000BCA0 File Offset: 0x00009EA0
		private float SumLineHeights(int endLine, TextGenerator generator)
		{
			float num = 0f;
			for (int i = 0; i < endLine; i++)
			{
				num += (float)generator.lines[i].height;
			}
			return num;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000BCE0 File Offset: 0x00009EE0
		private void GenerateHightlight(List<UIVertex> vbo, Vector2 roundingOffset)
		{
			int num = Mathf.Max(0, this.caretPositionInternal - this.m_DrawStart);
			int num2 = Mathf.Max(0, this.caretSelectPositionInternal - this.m_DrawStart);
			if (num > num2)
			{
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			num2--;
			TextGenerator cachedTextGenerator = this.m_TextComponent.cachedTextGenerator;
			int num4 = this.DetermineCharacterLine(num, cachedTextGenerator);
			float num5 = (float)this.m_TextComponent.fontSize;
			if (this.m_TextComponent.resizeTextForBestFit)
			{
				num5 = (float)cachedTextGenerator.fontSizeUsedForBestFit / this.m_TextComponent.pixelsPerUnit;
			}
			if (this.cachedInputTextGenerator != null && this.cachedInputTextGenerator.lines.Count > 0)
			{
				num5 = (float)this.cachedInputTextGenerator.lines[0].height;
			}
			if (this.m_TextComponent.resizeTextForBestFit && this.cachedInputTextGenerator != null)
			{
				num5 = (float)this.cachedInputTextGenerator.fontSizeUsedForBestFit;
			}
			int num6 = InputField.GetLineEndPosition(cachedTextGenerator, num4);
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.uv0 = Vector2.zero;
			simpleVert.color = this.selectionColor;
			int num7 = num;
			while (num7 <= num2 && num7 < cachedTextGenerator.characterCountVisible)
			{
				if (num7 + 1 == num6 || num7 == num2)
				{
					UICharInfo uicharInfo = cachedTextGenerator.characters[num];
					UICharInfo uicharInfo2 = cachedTextGenerator.characters[num7];
					Vector2 vector = new Vector2(uicharInfo.cursorPos.x / this.m_TextComponent.pixelsPerUnit, uicharInfo.cursorPos.y);
					Vector2 vector2 = new Vector2((uicharInfo2.cursorPos.x + uicharInfo2.charWidth) / this.m_TextComponent.pixelsPerUnit, vector.y - num5 / this.m_TextComponent.pixelsPerUnit);
					if (vector2.x > this.m_TextComponent.rectTransform.rect.xMax || vector2.x < this.m_TextComponent.rectTransform.rect.xMin)
					{
						vector2.x = this.m_TextComponent.rectTransform.rect.xMax;
					}
					simpleVert.position = new Vector3(vector.x, vector2.y, 0f) + roundingOffset;
					vbo.Add(simpleVert);
					simpleVert.position = new Vector3(vector2.x, vector2.y, 0f) + roundingOffset;
					vbo.Add(simpleVert);
					simpleVert.position = new Vector3(vector2.x, vector.y, 0f) + roundingOffset;
					vbo.Add(simpleVert);
					simpleVert.position = new Vector3(vector.x, vector.y, 0f) + roundingOffset;
					vbo.Add(simpleVert);
					num = num7 + 1;
					num4++;
					num6 = InputField.GetLineEndPosition(cachedTextGenerator, num4);
				}
				num7++;
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000C008 File Offset: 0x0000A208
		protected char Validate(string text, int pos, char ch)
		{
			if (this.characterValidation == InputField.CharacterValidation.None || !base.enabled)
			{
				return ch;
			}
			if (this.characterValidation == InputField.CharacterValidation.Integer || this.characterValidation == InputField.CharacterValidation.Decimal)
			{
				if (pos != 0 || text.Length <= 0 || text[0] != '-')
				{
					if (ch >= '0' && ch <= '9')
					{
						return ch;
					}
					if (ch == '-' && pos == 0)
					{
						return ch;
					}
					if (ch == '.' && this.characterValidation == InputField.CharacterValidation.Decimal && !text.Contains("."))
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == InputField.CharacterValidation.Alphanumeric)
			{
				if (ch >= 'A' && ch <= 'Z')
				{
					return ch;
				}
				if (ch >= 'a' && ch <= 'z')
				{
					return ch;
				}
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
			}
			else if (this.characterValidation == InputField.CharacterValidation.Name)
			{
				char c = ((text.Length <= 0) ? ' ' : text[Mathf.Clamp(pos, 0, text.Length - 1)]);
				char c2 = ((text.Length <= 0) ? '\n' : text[Mathf.Clamp(pos + 1, 0, text.Length - 1)]);
				if (char.IsLetter(ch))
				{
					if (char.IsLower(ch) && c == ' ')
					{
						return char.ToUpper(ch);
					}
					if (char.IsUpper(ch) && c != ' ' && c != '\'')
					{
						return char.ToLower(ch);
					}
					return ch;
				}
				else if (ch == '\'')
				{
					if (c != ' ' && c != '\'' && c2 != '\'' && !text.Contains("'"))
					{
						return ch;
					}
				}
				else if (ch == ' ' && c != ' ' && c != '\'' && c2 != ' ' && c2 != '\'')
				{
					return ch;
				}
			}
			else if (this.characterValidation == InputField.CharacterValidation.EmailAddress)
			{
				if (ch >= 'A' && ch <= 'Z')
				{
					return ch;
				}
				if (ch >= 'a' && ch <= 'z')
				{
					return ch;
				}
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
				if (ch == '@' && text.IndexOf('@') == -1)
				{
					return ch;
				}
				if ("!#$%&'*+-/=?^_`{|}~".IndexOf(ch) != -1)
				{
					return ch;
				}
				if (ch == '.')
				{
					char c3 = ((text.Length <= 0) ? ' ' : text[Mathf.Clamp(pos, 0, text.Length - 1)]);
					char c4 = ((text.Length <= 0) ? '\n' : text[Mathf.Clamp(pos + 1, 0, text.Length - 1)]);
					if (c3 != '.' && c4 != '.')
					{
						return ch;
					}
				}
			}
			return '\0';
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000C2F0 File Offset: 0x0000A4F0
		public void ActivateInputField()
		{
			if (this.m_TextComponent == null || this.m_TextComponent.font == null || !this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (this.isFocused && InputField.m_Keyboard != null && !InputField.m_Keyboard.active)
			{
				InputField.m_Keyboard.active = true;
				InputField.m_Keyboard.text = this.m_Text;
			}
			this.m_ShouldActivateNextUpdate = true;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000C384 File Offset: 0x0000A584
		private void ActivateInputFieldInternal()
		{
			if (EventSystem.current.currentSelectedGameObject != base.gameObject)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}
			if (TouchScreenKeyboard.isSupported)
			{
				if (Input.touchSupported)
				{
					TouchScreenKeyboard.hideInput = this.shouldHideMobileInput;
				}
				InputField.m_Keyboard = ((this.inputType != InputField.InputType.Password) ? TouchScreenKeyboard.Open(this.m_Text, this.keyboardType, this.inputType == InputField.InputType.AutoCorrect, this.multiLine) : TouchScreenKeyboard.Open(this.m_Text, this.keyboardType, false, this.multiLine, true));
			}
			else
			{
				Input.imeCompositionMode = IMECompositionMode.On;
				this.OnFocus();
			}
			this.m_AllowInput = true;
			this.m_OriginalText = this.text;
			this.m_WasCanceled = false;
			this.SetCaretVisible();
			this.UpdateLabel();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000C460 File Offset: 0x0000A660
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.ActivateInputField();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000C470 File Offset: 0x0000A670
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.ActivateInputField();
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000C484 File Offset: 0x0000A684
		public void DeactivateInputField()
		{
			if (!this.m_AllowInput)
			{
				return;
			}
			this.m_HasDoneFocusTransition = false;
			this.m_AllowInput = false;
			if (this.m_TextComponent != null && this.IsInteractable())
			{
				if (this.m_WasCanceled)
				{
					this.text = this.m_OriginalText;
				}
				if (InputField.m_Keyboard != null)
				{
					InputField.m_Keyboard.active = false;
					InputField.m_Keyboard = null;
				}
				this.m_CaretPosition = (this.m_CaretSelectPosition = 0);
				this.SendOnSubmit();
				Input.imeCompositionMode = IMECompositionMode.Auto;
			}
			this.MarkGeometryAsDirty();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000C51C File Offset: 0x0000A71C
		public override void OnDeselect(BaseEventData eventData)
		{
			this.DeactivateInputField();
			base.OnDeselect(eventData);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000C52C File Offset: 0x0000A72C
		public virtual void OnSubmit(BaseEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (!this.isFocused)
			{
				this.m_ShouldActivateNextUpdate = true;
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000C558 File Offset: 0x0000A758
		private void EnforceContentType()
		{
			switch (this.contentType)
			{
			case InputField.ContentType.Standard:
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = InputField.CharacterValidation.None;
				return;
			case InputField.ContentType.Autocorrected:
				this.m_InputType = InputField.InputType.AutoCorrect;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = InputField.CharacterValidation.None;
				return;
			case InputField.ContentType.IntegerNumber:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				this.m_CharacterValidation = InputField.CharacterValidation.Integer;
				return;
			case InputField.ContentType.DecimalNumber:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NumbersAndPunctuation;
				this.m_CharacterValidation = InputField.CharacterValidation.Decimal;
				return;
			case InputField.ContentType.Alphanumeric:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.ASCIICapable;
				this.m_CharacterValidation = InputField.CharacterValidation.Alphanumeric;
				return;
			case InputField.ContentType.Name:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = InputField.CharacterValidation.Name;
				return;
			case InputField.ContentType.EmailAddress:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.EmailAddress;
				this.m_CharacterValidation = InputField.CharacterValidation.EmailAddress;
				return;
			case InputField.ContentType.Password:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Password;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = InputField.CharacterValidation.None;
				return;
			case InputField.ContentType.Pin:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Password;
				this.m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				this.m_CharacterValidation = InputField.CharacterValidation.Integer;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000C694 File Offset: 0x0000A894
		private void SetToCustomIfContentTypeIsNot(params InputField.ContentType[] allowedContentTypes)
		{
			if (this.contentType == InputField.ContentType.Custom)
			{
				return;
			}
			for (int i = 0; i < allowedContentTypes.Length; i++)
			{
				if (this.contentType == allowedContentTypes[i])
				{
					return;
				}
			}
			this.contentType = InputField.ContentType.Custom;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000C6DC File Offset: 0x0000A8DC
		private void SetToCustom()
		{
			if (this.contentType == InputField.ContentType.Custom)
			{
				return;
			}
			this.contentType = InputField.ContentType.Custom;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000C6F4 File Offset: 0x0000A8F4
		protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			if (this.m_HasDoneFocusTransition)
			{
				state = Selectable.SelectionState.Highlighted;
			}
			else if (state == Selectable.SelectionState.Pressed)
			{
				this.m_HasDoneFocusTransition = true;
			}
			base.DoStateTransition(state, instant);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000C720 File Offset: 0x0000A920
		virtual bool UnityEngine.UI.ICanvasElement.IsDestroyed()
		{
			return base.IsDestroyed();
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000C728 File Offset: 0x0000A928
		virtual Transform UnityEngine.UI.ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x0400010F RID: 271
		private const float kHScrollSpeed = 0.05f;

		// Token: 0x04000110 RID: 272
		private const float kVScrollSpeed = 0.1f;

		// Token: 0x04000111 RID: 273
		private const string kEmailSpecialCharacters = "!#$%&'*+-/=?^_`{|}~";

		// Token: 0x04000112 RID: 274
		protected static TouchScreenKeyboard m_Keyboard;

		// Token: 0x04000113 RID: 275
		private static readonly char[] kSeparators = new char[] { ' ', '.', ',' };

		// Token: 0x04000114 RID: 276
		[FormerlySerializedAs("text")]
		[SerializeField]
		protected Text m_TextComponent;

		// Token: 0x04000115 RID: 277
		[SerializeField]
		protected Graphic m_Placeholder;

		// Token: 0x04000116 RID: 278
		[SerializeField]
		private InputField.ContentType m_ContentType;

		// Token: 0x04000117 RID: 279
		[SerializeField]
		[FormerlySerializedAs("inputType")]
		private InputField.InputType m_InputType;

		// Token: 0x04000118 RID: 280
		[SerializeField]
		[FormerlySerializedAs("asteriskChar")]
		private char m_AsteriskChar = '*';

		// Token: 0x04000119 RID: 281
		[SerializeField]
		[FormerlySerializedAs("keyboardType")]
		private TouchScreenKeyboardType m_KeyboardType;

		// Token: 0x0400011A RID: 282
		[SerializeField]
		private InputField.LineType m_LineType;

		// Token: 0x0400011B RID: 283
		[FormerlySerializedAs("hideMobileInput")]
		[SerializeField]
		private bool m_HideMobileInput;

		// Token: 0x0400011C RID: 284
		[FormerlySerializedAs("validation")]
		[SerializeField]
		private InputField.CharacterValidation m_CharacterValidation;

		// Token: 0x0400011D RID: 285
		[SerializeField]
		[FormerlySerializedAs("characterLimit")]
		private int m_CharacterLimit;

		// Token: 0x0400011E RID: 286
		[FormerlySerializedAs("onSubmit")]
		[FormerlySerializedAs("m_OnSubmit")]
		[SerializeField]
		private InputField.SubmitEvent m_EndEdit = new InputField.SubmitEvent();

		// Token: 0x0400011F RID: 287
		[SerializeField]
		[FormerlySerializedAs("onValueChange")]
		private InputField.OnChangeEvent m_OnValueChange = new InputField.OnChangeEvent();

		// Token: 0x04000120 RID: 288
		[SerializeField]
		[FormerlySerializedAs("onValidateInput")]
		private InputField.OnValidateInput m_OnValidateInput;

		// Token: 0x04000121 RID: 289
		[SerializeField]
		[FormerlySerializedAs("selectionColor")]
		private Color m_SelectionColor = new Color(0.65882355f, 0.80784315f, 1f, 0.7529412f);

		// Token: 0x04000122 RID: 290
		[SerializeField]
		[FormerlySerializedAs("mValue")]
		protected string m_Text = string.Empty;

		// Token: 0x04000123 RID: 291
		[Range(0f, 8f)]
		[SerializeField]
		private float m_CaretBlinkRate = 1.7f;

		// Token: 0x04000124 RID: 292
		protected int m_CaretPosition;

		// Token: 0x04000125 RID: 293
		protected int m_CaretSelectPosition;

		// Token: 0x04000126 RID: 294
		private RectTransform caretRectTrans;

		// Token: 0x04000127 RID: 295
		protected UIVertex[] m_CursorVerts;

		// Token: 0x04000128 RID: 296
		private TextGenerator m_InputTextCache;

		// Token: 0x04000129 RID: 297
		private CanvasRenderer m_CachedInputRenderer;

		// Token: 0x0400012A RID: 298
		private bool m_PreventFontCallback;

		// Token: 0x0400012B RID: 299
		private readonly List<UIVertex> m_Vbo = new List<UIVertex>();

		// Token: 0x0400012C RID: 300
		private bool m_AllowInput;

		// Token: 0x0400012D RID: 301
		private bool m_ShouldActivateNextUpdate;

		// Token: 0x0400012E RID: 302
		private bool m_UpdateDrag;

		// Token: 0x0400012F RID: 303
		private bool m_DragPositionOutOfBounds;

		// Token: 0x04000130 RID: 304
		protected bool m_CaretVisible;

		// Token: 0x04000131 RID: 305
		private Coroutine m_BlinkCoroutine;

		// Token: 0x04000132 RID: 306
		private float m_BlinkStartTime;

		// Token: 0x04000133 RID: 307
		protected int m_DrawStart;

		// Token: 0x04000134 RID: 308
		protected int m_DrawEnd;

		// Token: 0x04000135 RID: 309
		private Coroutine m_DragCoroutine;

		// Token: 0x04000136 RID: 310
		private string m_OriginalText = string.Empty;

		// Token: 0x04000137 RID: 311
		private bool m_WasCanceled;

		// Token: 0x04000138 RID: 312
		private bool m_HasDoneFocusTransition;

		// Token: 0x04000139 RID: 313
		private Event m_ProcessingEvent = new Event();

		// Token: 0x0200004D RID: 77
		public enum ContentType
		{
			// Token: 0x0400013B RID: 315
			Standard,
			// Token: 0x0400013C RID: 316
			Autocorrected,
			// Token: 0x0400013D RID: 317
			IntegerNumber,
			// Token: 0x0400013E RID: 318
			DecimalNumber,
			// Token: 0x0400013F RID: 319
			Alphanumeric,
			// Token: 0x04000140 RID: 320
			Name,
			// Token: 0x04000141 RID: 321
			EmailAddress,
			// Token: 0x04000142 RID: 322
			Password,
			// Token: 0x04000143 RID: 323
			Pin,
			// Token: 0x04000144 RID: 324
			Custom
		}

		// Token: 0x0200004E RID: 78
		public enum InputType
		{
			// Token: 0x04000146 RID: 326
			Standard,
			// Token: 0x04000147 RID: 327
			AutoCorrect,
			// Token: 0x04000148 RID: 328
			Password
		}

		// Token: 0x0200004F RID: 79
		public enum CharacterValidation
		{
			// Token: 0x0400014A RID: 330
			None,
			// Token: 0x0400014B RID: 331
			Integer,
			// Token: 0x0400014C RID: 332
			Decimal,
			// Token: 0x0400014D RID: 333
			Alphanumeric,
			// Token: 0x0400014E RID: 334
			Name,
			// Token: 0x0400014F RID: 335
			EmailAddress
		}

		// Token: 0x02000050 RID: 80
		public enum LineType
		{
			// Token: 0x04000151 RID: 337
			SingleLine,
			// Token: 0x04000152 RID: 338
			MultiLineSubmit,
			// Token: 0x04000153 RID: 339
			MultiLineNewline
		}

		// Token: 0x02000051 RID: 81
		[Serializable]
		public class SubmitEvent : UnityEvent<string>
		{
		}

		// Token: 0x02000052 RID: 82
		[Serializable]
		public class OnChangeEvent : UnityEvent<string>
		{
		}

		// Token: 0x02000053 RID: 83
		protected enum EditState
		{
			// Token: 0x04000155 RID: 341
			Continue,
			// Token: 0x04000156 RID: 342
			Finish
		}

		// Token: 0x02000094 RID: 148
		// (Invoke) Token: 0x060004E3 RID: 1251
		public delegate char OnValidateInput(string text, int charIndex, char addedChar);
	}
}
