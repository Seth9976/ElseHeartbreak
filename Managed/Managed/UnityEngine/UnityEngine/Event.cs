using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200010A RID: 266
	[StructLayout(LayoutKind.Sequential)]
	public sealed class Event
	{
		// Token: 0x060009AE RID: 2478 RVA: 0x00015DCC File Offset: 0x00013FCC
		public Event()
		{
			this.Init();
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00015DDC File Offset: 0x00013FDC
		public Event(Event other)
		{
			if (other == null)
			{
				throw new ArgumentException("Event to copy from is null.");
			}
			this.InitCopy(other);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00015DFC File Offset: 0x00013FFC
		private Event(IntPtr ptr)
		{
			this.InitPtr(ptr);
		}

		// Token: 0x060009B1 RID: 2481
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init();

		// Token: 0x060009B2 RID: 2482 RVA: 0x00015E0C File Offset: 0x0001400C
		~Event()
		{
			this.Cleanup();
		}

		// Token: 0x060009B3 RID: 2483
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Cleanup();

		// Token: 0x060009B4 RID: 2484
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InitCopy(Event other);

		// Token: 0x060009B5 RID: 2485
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InitPtr(IntPtr ptr);

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060009B6 RID: 2486
		public extern EventType rawType
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060009B7 RID: 2487
		// (set) Token: 0x060009B8 RID: 2488
		public extern EventType type
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060009B9 RID: 2489
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern EventType GetTypeForControl(int controlID);

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00015E48 File Offset: 0x00014048
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x00015E60 File Offset: 0x00014060
		public Vector2 mousePosition
		{
			get
			{
				Vector2 vector;
				this.Internal_GetMousePosition(out vector);
				return vector;
			}
			set
			{
				this.Internal_SetMousePosition(value);
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00015E6C File Offset: 0x0001406C
		private void Internal_SetMousePosition(Vector2 value)
		{
			Event.INTERNAL_CALL_Internal_SetMousePosition(this, ref value);
		}

		// Token: 0x060009BD RID: 2493
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_SetMousePosition(Event self, ref Vector2 value);

		// Token: 0x060009BE RID: 2494
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetMousePosition(out Vector2 value);

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00015E78 File Offset: 0x00014078
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x00015E90 File Offset: 0x00014090
		public Vector2 delta
		{
			get
			{
				Vector2 vector;
				this.Internal_GetMouseDelta(out vector);
				return vector;
			}
			set
			{
				this.Internal_SetMouseDelta(value);
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00015E9C File Offset: 0x0001409C
		private void Internal_SetMouseDelta(Vector2 value)
		{
			Event.INTERNAL_CALL_Internal_SetMouseDelta(this, ref value);
		}

		// Token: 0x060009C2 RID: 2498
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_SetMouseDelta(Event self, ref Vector2 value);

		// Token: 0x060009C3 RID: 2499
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetMouseDelta(out Vector2 value);

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00015EA8 File Offset: 0x000140A8
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x00015EBC File Offset: 0x000140BC
		[Obsolete("Use HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);", true)]
		public Ray mouseRay
		{
			get
			{
				return new Ray(Vector3.up, Vector3.up);
			}
			set
			{
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060009C6 RID: 2502
		// (set) Token: 0x060009C7 RID: 2503
		public extern int button
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060009C8 RID: 2504
		// (set) Token: 0x060009C9 RID: 2505
		public extern EventModifiers modifiers
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060009CA RID: 2506
		// (set) Token: 0x060009CB RID: 2507
		public extern float pressure
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060009CC RID: 2508
		// (set) Token: 0x060009CD RID: 2509
		public extern int clickCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060009CE RID: 2510
		// (set) Token: 0x060009CF RID: 2511
		public extern char character
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060009D0 RID: 2512
		// (set) Token: 0x060009D1 RID: 2513
		public extern string commandName
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060009D2 RID: 2514
		// (set) Token: 0x060009D3 RID: 2515
		public extern KeyCode keyCode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00015EC0 File Offset: 0x000140C0
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x00015ED0 File Offset: 0x000140D0
		public bool shift
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) != EventModifiers.None;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Shift;
				}
				else
				{
					this.modifiers |= EventModifiers.Shift;
				}
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00015F08 File Offset: 0x00014108
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x00015F18 File Offset: 0x00014118
		public bool control
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) != EventModifiers.None;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Control;
				}
				else
				{
					this.modifiers |= EventModifiers.Control;
				}
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00015F50 File Offset: 0x00014150
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x00015F60 File Offset: 0x00014160
		public bool alt
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) != EventModifiers.None;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Alt;
				}
				else
				{
					this.modifiers |= EventModifiers.Alt;
				}
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00015F98 File Offset: 0x00014198
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x00015FA8 File Offset: 0x000141A8
		public bool command
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) != EventModifiers.None;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Command;
				}
				else
				{
					this.modifiers |= EventModifiers.Command;
				}
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x00015FE0 File Offset: 0x000141E0
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x00015FF4 File Offset: 0x000141F4
		public bool capsLock
		{
			get
			{
				return (this.modifiers & EventModifiers.CapsLock) != EventModifiers.None;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.CapsLock;
				}
				else
				{
					this.modifiers |= EventModifiers.CapsLock;
				}
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0001602C File Offset: 0x0001422C
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x00016040 File Offset: 0x00014240
		public bool numeric
		{
			get
			{
				return (this.modifiers & EventModifiers.Numeric) != EventModifiers.None;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Shift;
				}
				else
				{
					this.modifiers |= EventModifiers.Shift;
				}
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00016078 File Offset: 0x00014278
		public bool functionKey
		{
			get
			{
				return (this.modifiers & EventModifiers.FunctionKey) != EventModifiers.None;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0001608C File Offset: 0x0001428C
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x00016094 File Offset: 0x00014294
		public static Event current
		{
			get
			{
				return Event.s_Current;
			}
			set
			{
				if (value != null)
				{
					Event.s_Current = value;
				}
				else
				{
					Event.s_Current = Event.s_MasterEvent;
				}
				Event.Internal_SetNativeEvent(Event.s_Current.m_Ptr);
			}
		}

		// Token: 0x060009E3 RID: 2531
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetNativeEvent(IntPtr ptr);

		// Token: 0x060009E4 RID: 2532 RVA: 0x000160CC File Offset: 0x000142CC
		private static void Internal_MakeMasterEventCurrent()
		{
			if (Event.s_MasterEvent == null)
			{
				Event.s_MasterEvent = new Event();
			}
			Event.s_Current = Event.s_MasterEvent;
			Event.Internal_SetNativeEvent(Event.s_MasterEvent.m_Ptr);
		}

		// Token: 0x060009E5 RID: 2533
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Use();

		// Token: 0x060009E6 RID: 2534
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool PopEvent(Event outEvent);

		// Token: 0x060009E7 RID: 2535
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetEventCount();

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x000160FC File Offset: 0x000142FC
		public bool isKey
		{
			get
			{
				EventType type = this.type;
				return type == EventType.KeyDown || type == EventType.KeyUp;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00016120 File Offset: 0x00014320
		public bool isMouse
		{
			get
			{
				EventType type = this.type;
				return type == EventType.MouseMove || type == EventType.MouseDown || type == EventType.MouseUp || type == EventType.MouseDrag;
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00016150 File Offset: 0x00014350
		public static Event KeyboardEvent(string key)
		{
			Event @event = new Event();
			@event.type = EventType.KeyDown;
			if (key == null || key == string.Empty)
			{
				return @event;
			}
			int num = 0;
			bool flag;
			do
			{
				flag = true;
				if (num >= key.Length)
				{
					break;
				}
				char c = key[num];
				switch (c)
				{
				case '#':
					@event.modifiers |= EventModifiers.Shift;
					num++;
					break;
				default:
					if (c != '^')
					{
						flag = false;
					}
					else
					{
						@event.modifiers |= EventModifiers.Control;
						num++;
					}
					break;
				case '%':
					@event.modifiers |= EventModifiers.Command;
					num++;
					break;
				case '&':
					@event.modifiers |= EventModifiers.Alt;
					num++;
					break;
				}
			}
			while (flag);
			string text = key.Substring(num, key.Length - num).ToLower();
			string text2 = text;
			switch (text2)
			{
			case "[0]":
				@event.character = '0';
				@event.keyCode = KeyCode.Keypad0;
				return @event;
			case "[1]":
				@event.character = '1';
				@event.keyCode = KeyCode.Keypad1;
				return @event;
			case "[2]":
				@event.character = '2';
				@event.keyCode = KeyCode.Keypad2;
				return @event;
			case "[3]":
				@event.character = '3';
				@event.keyCode = KeyCode.Keypad3;
				return @event;
			case "[4]":
				@event.character = '4';
				@event.keyCode = KeyCode.Keypad4;
				return @event;
			case "[5]":
				@event.character = '5';
				@event.keyCode = KeyCode.Keypad5;
				return @event;
			case "[6]":
				@event.character = '6';
				@event.keyCode = KeyCode.Keypad6;
				return @event;
			case "[7]":
				@event.character = '7';
				@event.keyCode = KeyCode.Keypad7;
				return @event;
			case "[8]":
				@event.character = '8';
				@event.keyCode = KeyCode.Keypad8;
				return @event;
			case "[9]":
				@event.character = '9';
				@event.keyCode = KeyCode.Keypad9;
				return @event;
			case "[.]":
				@event.character = '.';
				@event.keyCode = KeyCode.KeypadPeriod;
				return @event;
			case "[/]":
				@event.character = '/';
				@event.keyCode = KeyCode.KeypadDivide;
				return @event;
			case "[-]":
				@event.character = '-';
				@event.keyCode = KeyCode.KeypadMinus;
				return @event;
			case "[+]":
				@event.character = '+';
				@event.keyCode = KeyCode.KeypadPlus;
				return @event;
			case "[=]":
				@event.character = '=';
				@event.keyCode = KeyCode.KeypadEquals;
				return @event;
			case "[equals]":
				@event.character = '=';
				@event.keyCode = KeyCode.KeypadEquals;
				return @event;
			case "[enter]":
				@event.character = '\n';
				@event.keyCode = KeyCode.KeypadEnter;
				return @event;
			case "up":
				@event.keyCode = KeyCode.UpArrow;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "down":
				@event.keyCode = KeyCode.DownArrow;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "left":
				@event.keyCode = KeyCode.LeftArrow;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "right":
				@event.keyCode = KeyCode.RightArrow;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "insert":
				@event.keyCode = KeyCode.Insert;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "home":
				@event.keyCode = KeyCode.Home;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "end":
				@event.keyCode = KeyCode.End;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "pgup":
				@event.keyCode = KeyCode.PageDown;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "page up":
				@event.keyCode = KeyCode.PageUp;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "pgdown":
				@event.keyCode = KeyCode.PageUp;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "page down":
				@event.keyCode = KeyCode.PageDown;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "backspace":
				@event.keyCode = KeyCode.Backspace;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "delete":
				@event.keyCode = KeyCode.Delete;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "tab":
				@event.keyCode = KeyCode.Tab;
				return @event;
			case "f1":
				@event.keyCode = KeyCode.F1;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f2":
				@event.keyCode = KeyCode.F2;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f3":
				@event.keyCode = KeyCode.F3;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f4":
				@event.keyCode = KeyCode.F4;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f5":
				@event.keyCode = KeyCode.F5;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f6":
				@event.keyCode = KeyCode.F6;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f7":
				@event.keyCode = KeyCode.F7;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f8":
				@event.keyCode = KeyCode.F8;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f9":
				@event.keyCode = KeyCode.F9;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f10":
				@event.keyCode = KeyCode.F10;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f11":
				@event.keyCode = KeyCode.F11;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f12":
				@event.keyCode = KeyCode.F12;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f13":
				@event.keyCode = KeyCode.F13;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f14":
				@event.keyCode = KeyCode.F14;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f15":
				@event.keyCode = KeyCode.F15;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "[esc]":
				@event.keyCode = KeyCode.Escape;
				return @event;
			case "return":
				@event.character = '\n';
				@event.keyCode = KeyCode.Return;
				@event.modifiers &= ~EventModifiers.FunctionKey;
				return @event;
			case "space":
				@event.keyCode = KeyCode.Space;
				@event.character = ' ';
				@event.modifiers &= ~EventModifiers.FunctionKey;
				return @event;
			}
			if (text.Length != 1)
			{
				try
				{
					@event.keyCode = (KeyCode)((int)Enum.Parse(typeof(KeyCode), text, true));
				}
				catch (ArgumentException)
				{
					Debug.LogError(UnityString.Format("Unable to find key name that matches '{0}'", new object[] { text }));
				}
			}
			else
			{
				@event.character = text.ToLower()[0];
				@event.keyCode = (KeyCode)@event.character;
				if (@event.modifiers != EventModifiers.None)
				{
					@event.character = '\0';
				}
			}
			return @event;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00016BF0 File Offset: 0x00014DF0
		public override int GetHashCode()
		{
			int num = 1;
			if (this.isKey)
			{
				num = (int)((ushort)this.keyCode);
			}
			if (this.isMouse)
			{
				num = this.mousePosition.GetHashCode();
			}
			return (num * 37) | (int)this.modifiers;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00016C3C File Offset: 0x00014E3C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != base.GetType())
			{
				return false;
			}
			Event @event = (Event)obj;
			if (this.type != @event.type || this.modifiers != @event.modifiers)
			{
				return false;
			}
			if (this.isKey)
			{
				return this.keyCode == @event.keyCode && this.modifiers == @event.modifiers;
			}
			return this.isMouse && this.mousePosition == @event.mousePosition;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00016CEC File Offset: 0x00014EEC
		public override string ToString()
		{
			if (this.isKey)
			{
				if (this.character == '\0')
				{
					return UnityString.Format("Event:{0}   Character:\\0   Modifiers:{1}   KeyCode:{2}", new object[] { this.type, this.modifiers, this.keyCode });
				}
				return UnityString.Format(string.Concat(new object[]
				{
					"Event:",
					this.type,
					"   Character:",
					(int)this.character,
					"   Modifiers:",
					this.modifiers,
					"   KeyCode:",
					this.keyCode
				}), new object[0]);
			}
			else
			{
				if (this.isMouse)
				{
					return UnityString.Format("Event: {0}   Position: {1} Modifiers: {2}", new object[] { this.type, this.mousePosition, this.modifiers });
				}
				if (this.type == EventType.ExecuteCommand || this.type == EventType.ValidateCommand)
				{
					return UnityString.Format("Event: {0}  \"{1}\"", new object[] { this.type, this.commandName });
				}
				return string.Empty + this.type;
			}
		}

		// Token: 0x040003BE RID: 958
		[NotRenamed]
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x040003BF RID: 959
		private static Event s_Current;

		// Token: 0x040003C0 RID: 960
		private static Event s_MasterEvent;
	}
}
