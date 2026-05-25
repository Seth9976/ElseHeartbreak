using System;
using System.Reflection;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200008D RID: 141
	public class JsonObjectContract : JsonContract
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x000176DA File Offset: 0x000158DA
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x000176E2 File Offset: 0x000158E2
		public MemberSerialization MemberSerialization { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x000176EB File Offset: 0x000158EB
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x000176F3 File Offset: 0x000158F3
		public JsonPropertyCollection Properties { get; private set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x000176FC File Offset: 0x000158FC
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x00017704 File Offset: 0x00015904
		public JsonPropertyCollection ConstructorParameters { get; private set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001770D File Offset: 0x0001590D
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x00017715 File Offset: 0x00015915
		public ConstructorInfo OverrideConstructor { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001771E File Offset: 0x0001591E
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x00017726 File Offset: 0x00015926
		public ConstructorInfo ParametrizedConstructor { get; set; }

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001772F File Offset: 0x0001592F
		public JsonObjectContract(Type underlyingType)
			: base(underlyingType)
		{
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
			this.ConstructorParameters = new JsonPropertyCollection(base.UnderlyingType);
		}
	}
}
