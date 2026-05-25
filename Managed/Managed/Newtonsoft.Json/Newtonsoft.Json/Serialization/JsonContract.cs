using System;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200002D RID: 45
	public abstract class JsonContract
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00008745 File Offset: 0x00006945
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000874D File Offset: 0x0000694D
		public Type UnderlyingType { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00008756 File Offset: 0x00006956
		// (set) Token: 0x060001FA RID: 506 RVA: 0x0000875E File Offset: 0x0000695E
		public Type CreatedType { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00008767 File Offset: 0x00006967
		// (set) Token: 0x060001FC RID: 508 RVA: 0x0000876F File Offset: 0x0000696F
		public bool? IsReference { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00008778 File Offset: 0x00006978
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00008780 File Offset: 0x00006980
		public JsonConverter Converter { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00008789 File Offset: 0x00006989
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00008791 File Offset: 0x00006991
		internal JsonConverter InternalConverter { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000879A File Offset: 0x0000699A
		// (set) Token: 0x06000202 RID: 514 RVA: 0x000087A2 File Offset: 0x000069A2
		public MethodInfo OnDeserialized { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000087AB File Offset: 0x000069AB
		// (set) Token: 0x06000204 RID: 516 RVA: 0x000087B3 File Offset: 0x000069B3
		public MethodInfo OnDeserializing { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000087BC File Offset: 0x000069BC
		// (set) Token: 0x06000206 RID: 518 RVA: 0x000087C4 File Offset: 0x000069C4
		public MethodInfo OnSerialized { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000087CD File Offset: 0x000069CD
		// (set) Token: 0x06000208 RID: 520 RVA: 0x000087D5 File Offset: 0x000069D5
		public MethodInfo OnSerializing { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000087DE File Offset: 0x000069DE
		// (set) Token: 0x0600020A RID: 522 RVA: 0x000087E6 File Offset: 0x000069E6
		public Func<object> DefaultCreator { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600020B RID: 523 RVA: 0x000087EF File Offset: 0x000069EF
		// (set) Token: 0x0600020C RID: 524 RVA: 0x000087F7 File Offset: 0x000069F7
		public bool DefaultCreatorNonPublic { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00008800 File Offset: 0x00006A00
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00008808 File Offset: 0x00006A08
		public MethodInfo OnError { get; set; }

		// Token: 0x0600020F RID: 527 RVA: 0x00008814 File Offset: 0x00006A14
		internal void InvokeOnSerializing(object o, StreamingContext context)
		{
			if (this.OnSerializing != null)
			{
				this.OnSerializing.Invoke(o, new object[] { context });
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008848 File Offset: 0x00006A48
		internal void InvokeOnSerialized(object o, StreamingContext context)
		{
			if (this.OnSerialized != null)
			{
				this.OnSerialized.Invoke(o, new object[] { context });
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000887C File Offset: 0x00006A7C
		internal void InvokeOnDeserializing(object o, StreamingContext context)
		{
			if (this.OnDeserializing != null)
			{
				this.OnDeserializing.Invoke(o, new object[] { context });
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000088B0 File Offset: 0x00006AB0
		internal void InvokeOnDeserialized(object o, StreamingContext context)
		{
			if (this.OnDeserialized != null)
			{
				this.OnDeserialized.Invoke(o, new object[] { context });
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000088E4 File Offset: 0x00006AE4
		internal void InvokeOnError(object o, StreamingContext context, ErrorContext errorContext)
		{
			if (this.OnError != null)
			{
				this.OnError.Invoke(o, new object[] { context, errorContext });
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000891B File Offset: 0x00006B1B
		internal JsonContract(Type underlyingType)
		{
			ValidationUtils.ArgumentNotNull(underlyingType, "underlyingType");
			this.UnderlyingType = underlyingType;
			this.CreatedType = underlyingType;
		}
	}
}
