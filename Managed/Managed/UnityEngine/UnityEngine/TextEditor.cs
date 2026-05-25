using System;
using System.Collections.Generic;

namespace UnityEngine
{
	// Token: 0x0200004D RID: 77
	public class TextEditor
	{
		// Token: 0x06000139 RID: 313 RVA: 0x000053C0 File Offset: 0x000035C0
		private void ClearCursorPos()
		{
			this.hasHorizontalCursorPos = false;
			this.m_iAltCursorPos = -1;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000053D0 File Offset: 0x000035D0
		public void OnFocus()
		{
			if (this.multiline)
			{
				this.pos = (this.selectPos = 0);
			}
			else
			{
				this.SelectAll();
			}
			this.m_HasFocus = true;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000540C File Offset: 0x0000360C
		public void OnLostFocus()
		{
			this.m_HasFocus = false;
			this.scrollOffset = Vector2.zero;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005420 File Offset: 0x00003620
		private void GrabGraphicalCursorPos()
		{
			if (!this.hasHorizontalCursorPos)
			{
				this.graphicalCursorPos = this.style.GetCursorPixelPosition(this.position, this.content, this.pos);
				this.graphicalSelectCursorPos = this.style.GetCursorPixelPosition(this.position, this.content, this.selectPos);
				this.hasHorizontalCursorPos = false;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005488 File Offset: 0x00003688
		public bool HandleKeyEvent(Event e)
		{
			this.InitKeyActions();
			EventModifiers modifiers = e.modifiers;
			e.modifiers &= ~EventModifiers.CapsLock;
			if (TextEditor.s_Keyactions.ContainsKey(e))
			{
				TextEditor.TextEditOp textEditOp = TextEditor.s_Keyactions[e];
				this.PerformOperation(textEditOp);
				e.modifiers = modifiers;
				this.UpdateScrollOffset();
				return true;
			}
			e.modifiers = modifiers;
			return false;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000054EC File Offset: 0x000036EC
		public bool DeleteLineBack()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			int num = this.pos;
			int num2 = num;
			while (num2-- != 0)
			{
				if (this.content.text[num2] == '\n')
				{
					num = num2 + 1;
					break;
				}
			}
			if (num2 == -1)
			{
				num = 0;
			}
			if (this.pos != num)
			{
				this.content.text = this.content.text.Remove(num, this.pos - num);
				this.selectPos = (this.pos = num);
				return true;
			}
			return false;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005594 File Offset: 0x00003794
		public bool DeleteWordBack()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			int num = this.FindEndOfPreviousWord(this.pos);
			if (this.pos != num)
			{
				this.content.text = this.content.text.Remove(num, this.pos - num);
				this.selectPos = (this.pos = num);
				return true;
			}
			return false;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005608 File Offset: 0x00003808
		public bool DeleteWordForward()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			int num = this.FindStartOfNextWord(this.pos);
			if (this.pos < this.content.text.Length)
			{
				this.content.text = this.content.text.Remove(this.pos, num - this.pos);
				return true;
			}
			return false;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005680 File Offset: 0x00003880
		public bool Delete()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			if (this.pos < this.content.text.Length)
			{
				this.content.text = this.content.text.Remove(this.pos, 1);
				return true;
			}
			return false;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000056E4 File Offset: 0x000038E4
		public bool CanPaste()
		{
			return GUIUtility.systemCopyBuffer.Length != 0;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000056F8 File Offset: 0x000038F8
		public bool Backspace()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			if (this.pos > 0)
			{
				this.content.text = this.content.text.Remove(this.pos - 1, 1);
				this.selectPos = --this.pos;
				this.ClearCursorPos();
				return true;
			}
			return false;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000576C File Offset: 0x0000396C
		public void SelectAll()
		{
			this.pos = 0;
			this.selectPos = this.content.text.Length;
			this.ClearCursorPos();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005794 File Offset: 0x00003994
		public void SelectNone()
		{
			this.selectPos = this.pos;
			this.ClearCursorPos();
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000057A8 File Offset: 0x000039A8
		public bool hasSelection
		{
			get
			{
				return this.pos != this.selectPos;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000057BC File Offset: 0x000039BC
		public string SelectedText
		{
			get
			{
				int length = this.content.text.Length;
				if (this.pos > length)
				{
					this.pos = length;
				}
				if (this.selectPos > length)
				{
					this.selectPos = length;
				}
				if (this.pos == this.selectPos)
				{
					return string.Empty;
				}
				if (this.pos < this.selectPos)
				{
					return this.content.text.Substring(this.pos, this.selectPos - this.pos);
				}
				return this.content.text.Substring(this.selectPos, this.pos - this.selectPos);
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005870 File Offset: 0x00003A70
		public bool DeleteSelection()
		{
			int length = this.content.text.Length;
			if (this.pos > length)
			{
				this.pos = length;
			}
			if (this.selectPos > length)
			{
				this.selectPos = length;
			}
			if (this.pos == this.selectPos)
			{
				return false;
			}
			if (this.pos < this.selectPos)
			{
				this.content.text = this.content.text.Substring(0, this.pos) + this.content.text.Substring(this.selectPos, this.content.text.Length - this.selectPos);
				this.selectPos = this.pos;
			}
			else
			{
				this.content.text = this.content.text.Substring(0, this.selectPos) + this.content.text.Substring(this.pos, this.content.text.Length - this.pos);
				this.pos = this.selectPos;
			}
			this.ClearCursorPos();
			return true;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000059A4 File Offset: 0x00003BA4
		public void ReplaceSelection(string replace)
		{
			this.DeleteSelection();
			this.content.text = this.content.text.Insert(this.pos, replace);
			this.selectPos = (this.pos += replace.Length);
			this.ClearCursorPos();
			this.UpdateScrollOffset();
			this.m_TextHeightPotentiallyChanged = true;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005A0C File Offset: 0x00003C0C
		public void Insert(char c)
		{
			this.ReplaceSelection(c.ToString());
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005A1C File Offset: 0x00003C1C
		public void MoveSelectionToAltCursor()
		{
			if (this.m_iAltCursorPos == -1)
			{
				return;
			}
			int iAltCursorPos = this.m_iAltCursorPos;
			string selectedText = this.SelectedText;
			this.content.text = this.content.text.Insert(iAltCursorPos, selectedText);
			if (iAltCursorPos < this.pos)
			{
				this.pos += selectedText.Length;
				this.selectPos += selectedText.Length;
			}
			this.DeleteSelection();
			this.selectPos = (this.pos = iAltCursorPos);
			this.ClearCursorPos();
			this.UpdateScrollOffset();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005AB8 File Offset: 0x00003CB8
		public void MoveRight()
		{
			this.ClearCursorPos();
			if (this.selectPos == this.pos)
			{
				this.pos++;
				this.ClampPos();
				this.selectPos = this.pos;
			}
			else if (this.selectPos > this.pos)
			{
				this.pos = this.selectPos;
			}
			else
			{
				this.selectPos = this.pos;
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005B38 File Offset: 0x00003D38
		public void MoveLeft()
		{
			if (this.selectPos == this.pos)
			{
				this.pos--;
				if (this.pos < 0)
				{
					this.pos = 0;
				}
				this.selectPos = this.pos;
			}
			else if (this.selectPos > this.pos)
			{
				this.selectPos = this.pos;
			}
			else
			{
				this.pos = this.selectPos;
			}
			this.ClearCursorPos();
			this.UpdateScrollOffset();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005BC4 File Offset: 0x00003DC4
		public void MoveUp()
		{
			if (this.selectPos < this.pos)
			{
				this.selectPos = this.pos;
			}
			else
			{
				this.pos = this.selectPos;
			}
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y - 1f;
			this.pos = (this.selectPos = this.style.GetCursorStringIndex(this.position, this.content, this.graphicalCursorPos));
			if (this.pos <= 0)
			{
				this.ClearCursorPos();
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005C60 File Offset: 0x00003E60
		public void MoveDown()
		{
			if (this.selectPos > this.pos)
			{
				this.selectPos = this.pos;
			}
			else
			{
				this.pos = this.selectPos;
			}
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y + (this.style.lineHeight + 5f);
			this.pos = (this.selectPos = this.style.GetCursorStringIndex(this.position, this.content, this.graphicalCursorPos));
			if (this.pos == this.content.text.Length)
			{
				this.ClearCursorPos();
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005D18 File Offset: 0x00003F18
		public void MoveLineStart()
		{
			int num = ((this.selectPos >= this.pos) ? this.pos : this.selectPos);
			int num2 = num;
			while (num2-- != 0)
			{
				if (this.content.text[num2] == '\n')
				{
					this.selectPos = (this.pos = num2 + 1);
					return;
				}
			}
			this.selectPos = (this.pos = 0);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005D9C File Offset: 0x00003F9C
		public void MoveLineEnd()
		{
			int num = ((this.selectPos <= this.pos) ? this.pos : this.selectPos);
			int i = num;
			int length = this.content.text.Length;
			while (i < length)
			{
				if (this.content.text[i] == '\n')
				{
					this.selectPos = (this.pos = i);
					return;
				}
				i++;
			}
			this.selectPos = (this.pos = length);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005E30 File Offset: 0x00004030
		public void MoveGraphicalLineStart()
		{
			this.pos = (this.selectPos = this.GetGraphicalLineStart((this.pos >= this.selectPos) ? this.selectPos : this.pos));
			this.UpdateScrollOffset();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005E7C File Offset: 0x0000407C
		public void MoveGraphicalLineEnd()
		{
			this.pos = (this.selectPos = this.GetGraphicalLineEnd((this.pos <= this.selectPos) ? this.selectPos : this.pos));
			this.UpdateScrollOffset();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005EC8 File Offset: 0x000040C8
		public void MoveTextStart()
		{
			this.selectPos = (this.pos = 0);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005EEC File Offset: 0x000040EC
		public void MoveTextEnd()
		{
			this.selectPos = (this.pos = this.content.text.Length);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005F20 File Offset: 0x00004120
		public void MoveParagraphForward()
		{
			this.pos = ((this.pos <= this.selectPos) ? this.selectPos : this.pos);
			if (this.pos < this.content.text.Length)
			{
				this.selectPos = (this.pos = this.content.text.IndexOf('\n', this.pos + 1));
				if (this.pos == -1)
				{
					this.selectPos = (this.pos = this.content.text.Length);
				}
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005FCC File Offset: 0x000041CC
		public void MoveParagraphBackward()
		{
			this.pos = ((this.pos >= this.selectPos) ? this.selectPos : this.pos);
			if (this.pos > 1)
			{
				this.selectPos = (this.pos = this.content.text.LastIndexOf('\n', this.pos - 2) + 1);
			}
			else
			{
				this.selectPos = (this.pos = 0);
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006054 File Offset: 0x00004254
		public void MoveCursorToPosition(Vector2 cursorPosition)
		{
			this.selectPos = this.style.GetCursorStringIndex(this.position, this.content, cursorPosition + this.scrollOffset);
			if (!Event.current.shift)
			{
				this.pos = this.selectPos;
			}
			this.ClampPos();
			this.UpdateScrollOffset();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000060B4 File Offset: 0x000042B4
		public void MoveAltCursorToPosition(Vector2 cursorPosition)
		{
			this.m_iAltCursorPos = this.style.GetCursorStringIndex(this.position, this.content, cursorPosition + this.scrollOffset);
			this.ClampPos();
			this.UpdateScrollOffset();
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000060EC File Offset: 0x000042EC
		public bool IsOverSelection(Vector2 cursorPosition)
		{
			int cursorStringIndex = this.style.GetCursorStringIndex(this.position, this.content, cursorPosition + this.scrollOffset);
			return cursorStringIndex < Mathf.Max(this.pos, this.selectPos) && cursorStringIndex > Mathf.Min(this.pos, this.selectPos);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000614C File Offset: 0x0000434C
		public void SelectToPosition(Vector2 cursorPosition)
		{
			if (!this.m_MouseDragSelectsWholeWords)
			{
				this.pos = this.style.GetCursorStringIndex(this.position, this.content, cursorPosition + this.scrollOffset);
			}
			else
			{
				int num = this.style.GetCursorStringIndex(this.position, this.content, cursorPosition + this.scrollOffset);
				if (this.m_DblClickSnap == TextEditor.DblClickSnapping.WORDS)
				{
					if (num < this.m_DblClickInitPos)
					{
						this.pos = this.FindEndOfClassification(num, -1);
						this.selectPos = this.FindEndOfClassification(this.m_DblClickInitPos, 1);
					}
					else
					{
						if (num >= this.content.text.Length)
						{
							num = this.content.text.Length - 1;
						}
						this.pos = this.FindEndOfClassification(num, 1);
						this.selectPos = this.FindEndOfClassification(this.m_DblClickInitPos - 1, -1);
					}
				}
				else if (num < this.m_DblClickInitPos)
				{
					if (num > 0)
					{
						this.pos = this.content.text.LastIndexOf('\n', num - 2) + 1;
					}
					else
					{
						this.pos = 0;
					}
					this.selectPos = this.content.text.LastIndexOf('\n', this.m_DblClickInitPos);
				}
				else
				{
					if (num < this.content.text.Length)
					{
						this.pos = this.content.text.IndexOf('\n', num + 1) + 1;
						if (this.pos <= 0)
						{
							this.pos = this.content.text.Length;
						}
					}
					else
					{
						this.pos = this.content.text.Length;
					}
					this.selectPos = this.content.text.LastIndexOf('\n', this.m_DblClickInitPos - 2) + 1;
				}
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000633C File Offset: 0x0000453C
		public void SelectLeft()
		{
			if (this.m_bJustSelected && this.pos > this.selectPos)
			{
				int num = this.pos;
				this.pos = this.selectPos;
				this.selectPos = num;
			}
			this.m_bJustSelected = false;
			this.pos--;
			if (this.pos < 0)
			{
				this.pos = 0;
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000063B0 File Offset: 0x000045B0
		public void SelectRight()
		{
			if (this.m_bJustSelected && this.pos < this.selectPos)
			{
				int num = this.pos;
				this.pos = this.selectPos;
				this.selectPos = num;
			}
			this.m_bJustSelected = false;
			this.pos++;
			int length = this.content.text.Length;
			if (this.pos > length)
			{
				this.pos = length;
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006434 File Offset: 0x00004634
		public void SelectUp()
		{
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y - 1f;
			this.pos = this.style.GetCursorStringIndex(this.position, this.content, this.graphicalCursorPos);
			this.UpdateScrollOffset();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006488 File Offset: 0x00004688
		public void SelectDown()
		{
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y + (this.style.lineHeight + 5f);
			this.pos = this.style.GetCursorStringIndex(this.position, this.content, this.graphicalCursorPos);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000064E8 File Offset: 0x000046E8
		public void SelectTextEnd()
		{
			this.pos = this.content.text.Length;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006508 File Offset: 0x00004708
		public void SelectTextStart()
		{
			this.pos = 0;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006518 File Offset: 0x00004718
		public void MouseDragSelectsWholeWords(bool on)
		{
			this.m_MouseDragSelectsWholeWords = on;
			this.m_DblClickInitPos = this.pos;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006530 File Offset: 0x00004730
		public void DblClickSnap(TextEditor.DblClickSnapping snapping)
		{
			this.m_DblClickSnap = snapping;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000653C File Offset: 0x0000473C
		private int GetGraphicalLineStart(int p)
		{
			Vector2 cursorPixelPosition = this.style.GetCursorPixelPosition(this.position, this.content, p);
			cursorPixelPosition.x = 0f;
			return this.style.GetCursorStringIndex(this.position, this.content, cursorPixelPosition);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006588 File Offset: 0x00004788
		private int GetGraphicalLineEnd(int p)
		{
			Vector2 cursorPixelPosition = this.style.GetCursorPixelPosition(this.position, this.content, p);
			cursorPixelPosition.x += 5000f;
			return this.style.GetCursorStringIndex(this.position, this.content, cursorPixelPosition);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000065DC File Offset: 0x000047DC
		private int FindNextSeperator(int startPos)
		{
			int length = this.content.text.Length;
			while (startPos < length && !TextEditor.isLetterLikeChar(this.content.text[startPos]))
			{
				startPos++;
			}
			while (startPos < length && TextEditor.isLetterLikeChar(this.content.text[startPos]))
			{
				startPos++;
			}
			return startPos;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006654 File Offset: 0x00004854
		private static bool isLetterLikeChar(char c)
		{
			return char.IsLetterOrDigit(c) || c == '\'';
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000666C File Offset: 0x0000486C
		private int FindPrevSeperator(int startPos)
		{
			startPos--;
			while (startPos > 0 && !TextEditor.isLetterLikeChar(this.content.text[startPos]))
			{
				startPos--;
			}
			while (startPos >= 0 && TextEditor.isLetterLikeChar(this.content.text[startPos]))
			{
				startPos--;
			}
			return startPos + 1;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000066DC File Offset: 0x000048DC
		public void MoveWordRight()
		{
			this.pos = ((this.pos <= this.selectPos) ? this.selectPos : this.pos);
			this.pos = (this.selectPos = this.FindNextSeperator(this.pos));
			this.ClearCursorPos();
			this.UpdateScrollOffset();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006738 File Offset: 0x00004938
		public void MoveToStartOfNextWord()
		{
			this.ClearCursorPos();
			if (this.pos != this.selectPos)
			{
				this.MoveRight();
				return;
			}
			this.pos = (this.selectPos = this.FindStartOfNextWord(this.pos));
			this.UpdateScrollOffset();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006784 File Offset: 0x00004984
		public void MoveToEndOfPreviousWord()
		{
			this.ClearCursorPos();
			if (this.pos != this.selectPos)
			{
				this.MoveLeft();
				return;
			}
			this.pos = (this.selectPos = this.FindEndOfPreviousWord(this.pos));
			this.UpdateScrollOffset();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000067D0 File Offset: 0x000049D0
		public void SelectToStartOfNextWord()
		{
			this.ClearCursorPos();
			this.pos = this.FindStartOfNextWord(this.pos);
			this.UpdateScrollOffset();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000067F0 File Offset: 0x000049F0
		public void SelectToEndOfPreviousWord()
		{
			this.ClearCursorPos();
			this.pos = this.FindEndOfPreviousWord(this.pos);
			this.UpdateScrollOffset();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006810 File Offset: 0x00004A10
		private TextEditor.CharacterType ClassifyChar(char c)
		{
			if (char.IsWhiteSpace(c))
			{
				return TextEditor.CharacterType.WhiteSpace;
			}
			if (char.IsLetterOrDigit(c) || c == '\'')
			{
				return TextEditor.CharacterType.LetterLike;
			}
			return TextEditor.CharacterType.Symbol;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006838 File Offset: 0x00004A38
		public int FindStartOfNextWord(int p)
		{
			int length = this.content.text.Length;
			if (p == length)
			{
				return p;
			}
			char c = this.content.text[p];
			TextEditor.CharacterType characterType = this.ClassifyChar(c);
			if (characterType != TextEditor.CharacterType.WhiteSpace)
			{
				p++;
				while (p < length && this.ClassifyChar(this.content.text[p]) == characterType)
				{
					p++;
				}
			}
			else if (c == '\t' || c == '\n')
			{
				return p + 1;
			}
			if (p == length)
			{
				return p;
			}
			c = this.content.text[p];
			if (c == ' ')
			{
				while (p < length && char.IsWhiteSpace(this.content.text[p]))
				{
					p++;
				}
			}
			else if (c == '\t' || c == '\n')
			{
				return p;
			}
			return p;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006934 File Offset: 0x00004B34
		private int FindEndOfPreviousWord(int p)
		{
			if (p == 0)
			{
				return p;
			}
			p--;
			while (p > 0 && this.content.text[p] == ' ')
			{
				p--;
			}
			TextEditor.CharacterType characterType = this.ClassifyChar(this.content.text[p]);
			if (characterType != TextEditor.CharacterType.WhiteSpace)
			{
				while (p > 0 && this.ClassifyChar(this.content.text[p - 1]) == characterType)
				{
					p--;
				}
			}
			return p;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000069C8 File Offset: 0x00004BC8
		public void MoveWordLeft()
		{
			this.pos = ((this.pos >= this.selectPos) ? this.selectPos : this.pos);
			this.pos = this.FindPrevSeperator(this.pos);
			this.selectPos = this.pos;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006A24 File Offset: 0x00004C24
		public void SelectWordRight()
		{
			this.ClearCursorPos();
			int num = this.selectPos;
			if (this.pos < this.selectPos)
			{
				this.selectPos = this.pos;
				this.MoveWordRight();
				this.selectPos = num;
				this.pos = ((this.pos >= this.selectPos) ? this.selectPos : this.pos);
				return;
			}
			this.selectPos = this.pos;
			this.MoveWordRight();
			this.selectPos = num;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006AB0 File Offset: 0x00004CB0
		public void SelectWordLeft()
		{
			this.ClearCursorPos();
			int num = this.selectPos;
			if (this.pos > this.selectPos)
			{
				this.selectPos = this.pos;
				this.MoveWordLeft();
				this.selectPos = num;
				this.pos = ((this.pos <= this.selectPos) ? this.selectPos : this.pos);
				return;
			}
			this.selectPos = this.pos;
			this.MoveWordLeft();
			this.selectPos = num;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006B3C File Offset: 0x00004D3C
		public void ExpandSelectGraphicalLineStart()
		{
			this.ClearCursorPos();
			if (this.pos < this.selectPos)
			{
				this.pos = this.GetGraphicalLineStart(this.pos);
			}
			else
			{
				int num = this.pos;
				this.pos = this.GetGraphicalLineStart(this.selectPos);
				this.selectPos = num;
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006BA0 File Offset: 0x00004DA0
		public void ExpandSelectGraphicalLineEnd()
		{
			this.ClearCursorPos();
			if (this.pos > this.selectPos)
			{
				this.pos = this.GetGraphicalLineEnd(this.pos);
			}
			else
			{
				int num = this.pos;
				this.pos = this.GetGraphicalLineEnd(this.selectPos);
				this.selectPos = num;
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006C04 File Offset: 0x00004E04
		public void SelectGraphicalLineStart()
		{
			this.ClearCursorPos();
			this.pos = this.GetGraphicalLineStart(this.pos);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006C24 File Offset: 0x00004E24
		public void SelectGraphicalLineEnd()
		{
			this.ClearCursorPos();
			this.pos = this.GetGraphicalLineEnd(this.pos);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006C44 File Offset: 0x00004E44
		public void SelectParagraphForward()
		{
			this.ClearCursorPos();
			bool flag = this.pos < this.selectPos;
			if (this.pos < this.content.text.Length)
			{
				this.pos = this.content.text.IndexOf('\n', this.pos + 1);
				if (this.pos == -1)
				{
					this.pos = this.content.text.Length;
				}
				if (flag && this.pos > this.selectPos)
				{
					this.pos = this.selectPos;
				}
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006CEC File Offset: 0x00004EEC
		public void SelectParagraphBackward()
		{
			this.ClearCursorPos();
			bool flag = this.pos > this.selectPos;
			if (this.pos > 1)
			{
				this.pos = this.content.text.LastIndexOf('\n', this.pos - 2) + 1;
				if (flag && this.pos < this.selectPos)
				{
					this.pos = this.selectPos;
				}
			}
			else
			{
				this.selectPos = (this.pos = 0);
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00006D7C File Offset: 0x00004F7C
		public void SelectCurrentWord()
		{
			this.ClearCursorPos();
			int length = this.content.text.Length;
			this.selectPos = this.pos;
			if (length == 0)
			{
				return;
			}
			if (this.pos >= length)
			{
				this.pos = length - 1;
			}
			if (this.selectPos >= length)
			{
				this.selectPos--;
			}
			if (this.pos < this.selectPos)
			{
				this.pos = this.FindEndOfClassification(this.pos, -1);
				this.selectPos = this.FindEndOfClassification(this.selectPos, 1);
			}
			else
			{
				this.pos = this.FindEndOfClassification(this.pos, 1);
				this.selectPos = this.FindEndOfClassification(this.selectPos, -1);
			}
			this.m_bJustSelected = true;
			this.UpdateScrollOffset();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00006E54 File Offset: 0x00005054
		private int FindEndOfClassification(int p, int dir)
		{
			int length = this.content.text.Length;
			if (p >= length || p < 0)
			{
				return p;
			}
			TextEditor.CharacterType characterType = this.ClassifyChar(this.content.text[p]);
			for (;;)
			{
				p += dir;
				if (p < 0)
				{
					break;
				}
				if (p >= length)
				{
					return length;
				}
				if (this.ClassifyChar(this.content.text[p]) != characterType)
				{
					goto Block_4;
				}
			}
			return 0;
			Block_4:
			if (dir == 1)
			{
				return p;
			}
			return p + 1;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00006EDC File Offset: 0x000050DC
		public void SelectCurrentParagraph()
		{
			this.ClearCursorPos();
			int length = this.content.text.Length;
			if (this.pos < length)
			{
				this.pos = this.content.text.IndexOf('\n', this.pos);
				if (this.pos == -1)
				{
					this.pos = this.content.text.Length;
				}
				else
				{
					this.pos++;
				}
			}
			if (this.selectPos != 0)
			{
				this.selectPos = this.content.text.LastIndexOf('\n', this.selectPos - 1) + 1;
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006F94 File Offset: 0x00005194
		public void UpdateScrollOffsetIfNeeded()
		{
			if (this.m_TextHeightPotentiallyChanged)
			{
				this.UpdateScrollOffset();
				this.m_TextHeightPotentiallyChanged = false;
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006FB0 File Offset: 0x000051B0
		private void UpdateScrollOffset()
		{
			int num = this.pos;
			this.graphicalCursorPos = this.style.GetCursorPixelPosition(new Rect(0f, 0f, this.position.width, this.position.height), this.content, num);
			Rect rect = this.style.padding.Remove(this.position);
			Vector2 vector = new Vector2(this.style.CalcSize(this.content).x, this.style.CalcHeight(this.content, this.position.width));
			if (vector.x < this.position.width)
			{
				this.scrollOffset.x = 0f;
			}
			else
			{
				if (this.graphicalCursorPos.x + 1f > this.scrollOffset.x + rect.width)
				{
					this.scrollOffset.x = this.graphicalCursorPos.x - rect.width;
				}
				if (this.graphicalCursorPos.x < this.scrollOffset.x + (float)this.style.padding.left)
				{
					this.scrollOffset.x = this.graphicalCursorPos.x - (float)this.style.padding.left;
				}
			}
			if (vector.y < rect.height)
			{
				this.scrollOffset.y = 0f;
			}
			else
			{
				if (this.graphicalCursorPos.y + this.style.lineHeight > this.scrollOffset.y + rect.height + (float)this.style.padding.top)
				{
					this.scrollOffset.y = this.graphicalCursorPos.y - rect.height - (float)this.style.padding.top + this.style.lineHeight;
				}
				if (this.graphicalCursorPos.y < this.scrollOffset.y + (float)this.style.padding.top)
				{
					this.scrollOffset.y = this.graphicalCursorPos.y - (float)this.style.padding.top;
				}
			}
			if (this.scrollOffset.y > 0f && vector.y - this.scrollOffset.y < rect.height)
			{
				this.scrollOffset.y = vector.y - rect.height - (float)this.style.padding.top - (float)this.style.padding.bottom;
			}
			this.scrollOffset.y = ((this.scrollOffset.y >= 0f) ? this.scrollOffset.y : 0f);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000072C0 File Offset: 0x000054C0
		public void DrawCursor(string text)
		{
			string text2 = this.content.text;
			int num = this.pos;
			if (Input.compositionString.Length > 0)
			{
				this.content.text = text.Substring(0, this.pos) + Input.compositionString + text.Substring(this.selectPos);
				num += Input.compositionString.Length;
			}
			else
			{
				this.content.text = text;
			}
			this.graphicalCursorPos = this.style.GetCursorPixelPosition(new Rect(0f, 0f, this.position.width, this.position.height), this.content, num);
			this.UpdateScrollOffset();
			Vector2 contentOffset = this.style.contentOffset;
			this.style.contentOffset -= this.scrollOffset;
			this.style.Internal_clipOffset = this.scrollOffset;
			Input.compositionCursorPos = this.graphicalCursorPos + new Vector2(this.position.x, this.position.y + this.style.lineHeight) - this.scrollOffset;
			if (Input.compositionString.Length > 0)
			{
				this.style.DrawWithTextSelection(this.position, this.content, this.controlID, this.pos, this.pos + Input.compositionString.Length, true);
			}
			else
			{
				this.style.DrawWithTextSelection(this.position, this.content, this.controlID, this.pos, this.selectPos);
			}
			if (this.m_iAltCursorPos != -1)
			{
				this.style.DrawCursor(this.position, this.content, this.controlID, this.m_iAltCursorPos);
			}
			this.style.contentOffset = contentOffset;
			this.style.Internal_clipOffset = Vector2.zero;
			this.content.text = text2;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000074C4 File Offset: 0x000056C4
		private bool PerformOperation(TextEditor.TextEditOp operation)
		{
			switch (operation)
			{
			case TextEditor.TextEditOp.MoveLeft:
				this.MoveLeft();
				return false;
			case TextEditor.TextEditOp.MoveRight:
				this.MoveRight();
				return false;
			case TextEditor.TextEditOp.MoveUp:
				this.MoveUp();
				return false;
			case TextEditor.TextEditOp.MoveDown:
				this.MoveDown();
				return false;
			case TextEditor.TextEditOp.MoveLineStart:
				this.MoveLineStart();
				return false;
			case TextEditor.TextEditOp.MoveLineEnd:
				this.MoveLineEnd();
				return false;
			case TextEditor.TextEditOp.MoveTextStart:
				this.MoveTextStart();
				return false;
			case TextEditor.TextEditOp.MoveTextEnd:
				this.MoveTextEnd();
				return false;
			case TextEditor.TextEditOp.MoveGraphicalLineStart:
				this.MoveGraphicalLineStart();
				return false;
			case TextEditor.TextEditOp.MoveGraphicalLineEnd:
				this.MoveGraphicalLineEnd();
				return false;
			case TextEditor.TextEditOp.MoveWordLeft:
				this.MoveWordLeft();
				return false;
			case TextEditor.TextEditOp.MoveWordRight:
				this.MoveWordRight();
				return false;
			case TextEditor.TextEditOp.MoveParagraphForward:
				this.MoveParagraphForward();
				return false;
			case TextEditor.TextEditOp.MoveParagraphBackward:
				this.MoveParagraphBackward();
				return false;
			case TextEditor.TextEditOp.MoveToStartOfNextWord:
				this.MoveToStartOfNextWord();
				return false;
			case TextEditor.TextEditOp.MoveToEndOfPreviousWord:
				this.MoveToEndOfPreviousWord();
				return false;
			case TextEditor.TextEditOp.SelectLeft:
				this.SelectLeft();
				return false;
			case TextEditor.TextEditOp.SelectRight:
				this.SelectRight();
				return false;
			case TextEditor.TextEditOp.SelectUp:
				this.SelectUp();
				return false;
			case TextEditor.TextEditOp.SelectDown:
				this.SelectDown();
				return false;
			case TextEditor.TextEditOp.SelectTextStart:
				this.SelectTextStart();
				return false;
			case TextEditor.TextEditOp.SelectTextEnd:
				this.SelectTextEnd();
				return false;
			case TextEditor.TextEditOp.ExpandSelectGraphicalLineStart:
				this.ExpandSelectGraphicalLineStart();
				return false;
			case TextEditor.TextEditOp.ExpandSelectGraphicalLineEnd:
				this.ExpandSelectGraphicalLineEnd();
				return false;
			case TextEditor.TextEditOp.SelectGraphicalLineStart:
				this.SelectGraphicalLineStart();
				return false;
			case TextEditor.TextEditOp.SelectGraphicalLineEnd:
				this.SelectGraphicalLineEnd();
				return false;
			case TextEditor.TextEditOp.SelectWordLeft:
				this.SelectWordLeft();
				return false;
			case TextEditor.TextEditOp.SelectWordRight:
				this.SelectWordRight();
				return false;
			case TextEditor.TextEditOp.SelectToEndOfPreviousWord:
				this.SelectToEndOfPreviousWord();
				return false;
			case TextEditor.TextEditOp.SelectToStartOfNextWord:
				this.SelectToStartOfNextWord();
				return false;
			case TextEditor.TextEditOp.SelectParagraphBackward:
				this.SelectParagraphBackward();
				return false;
			case TextEditor.TextEditOp.SelectParagraphForward:
				this.SelectParagraphForward();
				return false;
			case TextEditor.TextEditOp.Delete:
				return this.Delete();
			case TextEditor.TextEditOp.Backspace:
				return this.Backspace();
			case TextEditor.TextEditOp.DeleteWordBack:
				return this.DeleteWordBack();
			case TextEditor.TextEditOp.DeleteWordForward:
				return this.DeleteWordForward();
			case TextEditor.TextEditOp.DeleteLineBack:
				return this.DeleteLineBack();
			case TextEditor.TextEditOp.Cut:
				return this.Cut();
			case TextEditor.TextEditOp.Copy:
				this.Copy();
				return false;
			case TextEditor.TextEditOp.Paste:
				return this.Paste();
			case TextEditor.TextEditOp.SelectAll:
				this.SelectAll();
				return false;
			case TextEditor.TextEditOp.SelectNone:
				this.SelectNone();
				return false;
			}
			Debug.Log("Unimplemented: " + operation);
			return false;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007764 File Offset: 0x00005964
		public void SaveBackup()
		{
			this.oldText = this.content.text;
			this.oldPos = this.pos;
			this.oldSelectPos = this.selectPos;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007790 File Offset: 0x00005990
		public void Undo()
		{
			this.content.text = this.oldText;
			this.pos = this.oldPos;
			this.selectPos = this.oldSelectPos;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000077C4 File Offset: 0x000059C4
		public bool Cut()
		{
			if (this.isPasswordField)
			{
				return false;
			}
			this.Copy();
			return this.DeleteSelection();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000077E0 File Offset: 0x000059E0
		public void Copy()
		{
			if (this.selectPos == this.pos)
			{
				return;
			}
			if (this.isPasswordField)
			{
				return;
			}
			string text;
			if (this.pos < this.selectPos)
			{
				text = this.content.text.Substring(this.pos, this.selectPos - this.pos);
			}
			else
			{
				text = this.content.text.Substring(this.selectPos, this.pos - this.selectPos);
			}
			GUIUtility.systemCopyBuffer = text;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007870 File Offset: 0x00005A70
		public bool Paste()
		{
			string systemCopyBuffer = GUIUtility.systemCopyBuffer;
			if (systemCopyBuffer != string.Empty)
			{
				this.ReplaceSelection(systemCopyBuffer);
				return true;
			}
			return false;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000078A0 File Offset: 0x00005AA0
		private static void MapKey(string key, TextEditor.TextEditOp action)
		{
			TextEditor.s_Keyactions[Event.KeyboardEvent(key)] = action;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000078B4 File Offset: 0x00005AB4
		private void InitKeyActions()
		{
			if (TextEditor.s_Keyactions != null)
			{
				return;
			}
			TextEditor.s_Keyactions = new Dictionary<Event, TextEditor.TextEditOp>();
			TextEditor.MapKey("left", TextEditor.TextEditOp.MoveLeft);
			TextEditor.MapKey("right", TextEditor.TextEditOp.MoveRight);
			TextEditor.MapKey("up", TextEditor.TextEditOp.MoveUp);
			TextEditor.MapKey("down", TextEditor.TextEditOp.MoveDown);
			TextEditor.MapKey("#left", TextEditor.TextEditOp.SelectLeft);
			TextEditor.MapKey("#right", TextEditor.TextEditOp.SelectRight);
			TextEditor.MapKey("#up", TextEditor.TextEditOp.SelectUp);
			TextEditor.MapKey("#down", TextEditor.TextEditOp.SelectDown);
			TextEditor.MapKey("delete", TextEditor.TextEditOp.Delete);
			TextEditor.MapKey("backspace", TextEditor.TextEditOp.Backspace);
			TextEditor.MapKey("#backspace", TextEditor.TextEditOp.Backspace);
			if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.OSXDashboardPlayer || Application.platform == RuntimePlatform.OSXEditor)
			{
				TextEditor.MapKey("^left", TextEditor.TextEditOp.MoveGraphicalLineStart);
				TextEditor.MapKey("^right", TextEditor.TextEditOp.MoveGraphicalLineEnd);
				TextEditor.MapKey("&left", TextEditor.TextEditOp.MoveWordLeft);
				TextEditor.MapKey("&right", TextEditor.TextEditOp.MoveWordRight);
				TextEditor.MapKey("&up", TextEditor.TextEditOp.MoveParagraphBackward);
				TextEditor.MapKey("&down", TextEditor.TextEditOp.MoveParagraphForward);
				TextEditor.MapKey("%left", TextEditor.TextEditOp.MoveGraphicalLineStart);
				TextEditor.MapKey("%right", TextEditor.TextEditOp.MoveGraphicalLineEnd);
				TextEditor.MapKey("%up", TextEditor.TextEditOp.MoveTextStart);
				TextEditor.MapKey("%down", TextEditor.TextEditOp.MoveTextEnd);
				TextEditor.MapKey("#home", TextEditor.TextEditOp.SelectTextStart);
				TextEditor.MapKey("#end", TextEditor.TextEditOp.SelectTextEnd);
				TextEditor.MapKey("#^left", TextEditor.TextEditOp.ExpandSelectGraphicalLineStart);
				TextEditor.MapKey("#^right", TextEditor.TextEditOp.ExpandSelectGraphicalLineEnd);
				TextEditor.MapKey("#^up", TextEditor.TextEditOp.SelectParagraphBackward);
				TextEditor.MapKey("#^down", TextEditor.TextEditOp.SelectParagraphForward);
				TextEditor.MapKey("#&left", TextEditor.TextEditOp.SelectWordLeft);
				TextEditor.MapKey("#&right", TextEditor.TextEditOp.SelectWordRight);
				TextEditor.MapKey("#&up", TextEditor.TextEditOp.SelectParagraphBackward);
				TextEditor.MapKey("#&down", TextEditor.TextEditOp.SelectParagraphForward);
				TextEditor.MapKey("#%left", TextEditor.TextEditOp.ExpandSelectGraphicalLineStart);
				TextEditor.MapKey("#%right", TextEditor.TextEditOp.ExpandSelectGraphicalLineEnd);
				TextEditor.MapKey("#%up", TextEditor.TextEditOp.SelectTextStart);
				TextEditor.MapKey("#%down", TextEditor.TextEditOp.SelectTextEnd);
				TextEditor.MapKey("%a", TextEditor.TextEditOp.SelectAll);
				TextEditor.MapKey("%x", TextEditor.TextEditOp.Cut);
				TextEditor.MapKey("%c", TextEditor.TextEditOp.Copy);
				TextEditor.MapKey("%v", TextEditor.TextEditOp.Paste);
				TextEditor.MapKey("^d", TextEditor.TextEditOp.Delete);
				TextEditor.MapKey("^h", TextEditor.TextEditOp.Backspace);
				TextEditor.MapKey("^b", TextEditor.TextEditOp.MoveLeft);
				TextEditor.MapKey("^f", TextEditor.TextEditOp.MoveRight);
				TextEditor.MapKey("^a", TextEditor.TextEditOp.MoveLineStart);
				TextEditor.MapKey("^e", TextEditor.TextEditOp.MoveLineEnd);
				TextEditor.MapKey("&delete", TextEditor.TextEditOp.DeleteWordForward);
				TextEditor.MapKey("&backspace", TextEditor.TextEditOp.DeleteWordBack);
				TextEditor.MapKey("%backspace", TextEditor.TextEditOp.DeleteLineBack);
			}
			else
			{
				TextEditor.MapKey("home", TextEditor.TextEditOp.MoveGraphicalLineStart);
				TextEditor.MapKey("end", TextEditor.TextEditOp.MoveGraphicalLineEnd);
				TextEditor.MapKey("%left", TextEditor.TextEditOp.MoveWordLeft);
				TextEditor.MapKey("%right", TextEditor.TextEditOp.MoveWordRight);
				TextEditor.MapKey("%up", TextEditor.TextEditOp.MoveParagraphBackward);
				TextEditor.MapKey("%down", TextEditor.TextEditOp.MoveParagraphForward);
				TextEditor.MapKey("^left", TextEditor.TextEditOp.MoveToEndOfPreviousWord);
				TextEditor.MapKey("^right", TextEditor.TextEditOp.MoveToStartOfNextWord);
				TextEditor.MapKey("^up", TextEditor.TextEditOp.MoveParagraphBackward);
				TextEditor.MapKey("^down", TextEditor.TextEditOp.MoveParagraphForward);
				TextEditor.MapKey("#^left", TextEditor.TextEditOp.SelectToEndOfPreviousWord);
				TextEditor.MapKey("#^right", TextEditor.TextEditOp.SelectToStartOfNextWord);
				TextEditor.MapKey("#^up", TextEditor.TextEditOp.SelectParagraphBackward);
				TextEditor.MapKey("#^down", TextEditor.TextEditOp.SelectParagraphForward);
				TextEditor.MapKey("#home", TextEditor.TextEditOp.SelectGraphicalLineStart);
				TextEditor.MapKey("#end", TextEditor.TextEditOp.SelectGraphicalLineEnd);
				TextEditor.MapKey("^delete", TextEditor.TextEditOp.DeleteWordForward);
				TextEditor.MapKey("^backspace", TextEditor.TextEditOp.DeleteWordBack);
				TextEditor.MapKey("%backspace", TextEditor.TextEditOp.DeleteLineBack);
				TextEditor.MapKey("^a", TextEditor.TextEditOp.SelectAll);
				TextEditor.MapKey("^x", TextEditor.TextEditOp.Cut);
				TextEditor.MapKey("^c", TextEditor.TextEditOp.Copy);
				TextEditor.MapKey("^v", TextEditor.TextEditOp.Paste);
				TextEditor.MapKey("#delete", TextEditor.TextEditOp.Cut);
				TextEditor.MapKey("^insert", TextEditor.TextEditOp.Copy);
				TextEditor.MapKey("#insert", TextEditor.TextEditOp.Paste);
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007C74 File Offset: 0x00005E74
		public void ClampPos()
		{
			if (this.m_HasFocus && this.controlID != GUIUtility.keyboardControl)
			{
				this.OnLostFocus();
			}
			if (!this.m_HasFocus && this.controlID == GUIUtility.keyboardControl)
			{
				this.OnFocus();
			}
			if (this.pos < 0)
			{
				this.pos = 0;
			}
			else if (this.pos > this.content.text.Length)
			{
				this.pos = this.content.text.Length;
			}
			if (this.selectPos < 0)
			{
				this.selectPos = 0;
			}
			else if (this.selectPos > this.content.text.Length)
			{
				this.selectPos = this.content.text.Length;
			}
			if (this.m_iAltCursorPos > this.content.text.Length)
			{
				this.m_iAltCursorPos = this.content.text.Length;
			}
		}

		// Token: 0x040000FD RID: 253
		public int pos;

		// Token: 0x040000FE RID: 254
		public int selectPos;

		// Token: 0x040000FF RID: 255
		public int controlID;

		// Token: 0x04000100 RID: 256
		public GUIContent content = new GUIContent();

		// Token: 0x04000101 RID: 257
		public GUIStyle style = GUIStyle.none;

		// Token: 0x04000102 RID: 258
		public Rect position;

		// Token: 0x04000103 RID: 259
		public bool multiline;

		// Token: 0x04000104 RID: 260
		public bool hasHorizontalCursorPos;

		// Token: 0x04000105 RID: 261
		public bool isPasswordField;

		// Token: 0x04000106 RID: 262
		internal bool m_HasFocus;

		// Token: 0x04000107 RID: 263
		public Vector2 scrollOffset = Vector2.zero;

		// Token: 0x04000108 RID: 264
		private bool m_TextHeightPotentiallyChanged;

		// Token: 0x04000109 RID: 265
		public Vector2 graphicalCursorPos;

		// Token: 0x0400010A RID: 266
		public Vector2 graphicalSelectCursorPos;

		// Token: 0x0400010B RID: 267
		private bool m_MouseDragSelectsWholeWords;

		// Token: 0x0400010C RID: 268
		private int m_DblClickInitPos;

		// Token: 0x0400010D RID: 269
		private TextEditor.DblClickSnapping m_DblClickSnap;

		// Token: 0x0400010E RID: 270
		private bool m_bJustSelected;

		// Token: 0x0400010F RID: 271
		private int m_iAltCursorPos = -1;

		// Token: 0x04000110 RID: 272
		private string oldText;

		// Token: 0x04000111 RID: 273
		private int oldPos;

		// Token: 0x04000112 RID: 274
		private int oldSelectPos;

		// Token: 0x04000113 RID: 275
		private static Dictionary<Event, TextEditor.TextEditOp> s_Keyactions;

		// Token: 0x0200004E RID: 78
		public enum DblClickSnapping : byte
		{
			// Token: 0x04000115 RID: 277
			WORDS,
			// Token: 0x04000116 RID: 278
			PARAGRAPHS
		}

		// Token: 0x0200004F RID: 79
		private enum CharacterType
		{
			// Token: 0x04000118 RID: 280
			LetterLike,
			// Token: 0x04000119 RID: 281
			Symbol,
			// Token: 0x0400011A RID: 282
			Symbol2,
			// Token: 0x0400011B RID: 283
			WhiteSpace
		}

		// Token: 0x02000050 RID: 80
		private enum TextEditOp
		{
			// Token: 0x0400011D RID: 285
			MoveLeft,
			// Token: 0x0400011E RID: 286
			MoveRight,
			// Token: 0x0400011F RID: 287
			MoveUp,
			// Token: 0x04000120 RID: 288
			MoveDown,
			// Token: 0x04000121 RID: 289
			MoveLineStart,
			// Token: 0x04000122 RID: 290
			MoveLineEnd,
			// Token: 0x04000123 RID: 291
			MoveTextStart,
			// Token: 0x04000124 RID: 292
			MoveTextEnd,
			// Token: 0x04000125 RID: 293
			MovePageUp,
			// Token: 0x04000126 RID: 294
			MovePageDown,
			// Token: 0x04000127 RID: 295
			MoveGraphicalLineStart,
			// Token: 0x04000128 RID: 296
			MoveGraphicalLineEnd,
			// Token: 0x04000129 RID: 297
			MoveWordLeft,
			// Token: 0x0400012A RID: 298
			MoveWordRight,
			// Token: 0x0400012B RID: 299
			MoveParagraphForward,
			// Token: 0x0400012C RID: 300
			MoveParagraphBackward,
			// Token: 0x0400012D RID: 301
			MoveToStartOfNextWord,
			// Token: 0x0400012E RID: 302
			MoveToEndOfPreviousWord,
			// Token: 0x0400012F RID: 303
			SelectLeft,
			// Token: 0x04000130 RID: 304
			SelectRight,
			// Token: 0x04000131 RID: 305
			SelectUp,
			// Token: 0x04000132 RID: 306
			SelectDown,
			// Token: 0x04000133 RID: 307
			SelectTextStart,
			// Token: 0x04000134 RID: 308
			SelectTextEnd,
			// Token: 0x04000135 RID: 309
			SelectPageUp,
			// Token: 0x04000136 RID: 310
			SelectPageDown,
			// Token: 0x04000137 RID: 311
			ExpandSelectGraphicalLineStart,
			// Token: 0x04000138 RID: 312
			ExpandSelectGraphicalLineEnd,
			// Token: 0x04000139 RID: 313
			SelectGraphicalLineStart,
			// Token: 0x0400013A RID: 314
			SelectGraphicalLineEnd,
			// Token: 0x0400013B RID: 315
			SelectWordLeft,
			// Token: 0x0400013C RID: 316
			SelectWordRight,
			// Token: 0x0400013D RID: 317
			SelectToEndOfPreviousWord,
			// Token: 0x0400013E RID: 318
			SelectToStartOfNextWord,
			// Token: 0x0400013F RID: 319
			SelectParagraphBackward,
			// Token: 0x04000140 RID: 320
			SelectParagraphForward,
			// Token: 0x04000141 RID: 321
			Delete,
			// Token: 0x04000142 RID: 322
			Backspace,
			// Token: 0x04000143 RID: 323
			DeleteWordBack,
			// Token: 0x04000144 RID: 324
			DeleteWordForward,
			// Token: 0x04000145 RID: 325
			DeleteLineBack,
			// Token: 0x04000146 RID: 326
			Cut,
			// Token: 0x04000147 RID: 327
			Copy,
			// Token: 0x04000148 RID: 328
			Paste,
			// Token: 0x04000149 RID: 329
			SelectAll,
			// Token: 0x0400014A RID: 330
			SelectNone,
			// Token: 0x0400014B RID: 331
			ScrollStart,
			// Token: 0x0400014C RID: 332
			ScrollEnd,
			// Token: 0x0400014D RID: 333
			ScrollPageUp,
			// Token: 0x0400014E RID: 334
			ScrollPageDown
		}
	}
}
