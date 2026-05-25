using System;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000082 RID: 130
	public class JsonProperty
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001575B File Offset: 0x0001395B
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x00015763 File Offset: 0x00013963
		public string PropertyName { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0001576C File Offset: 0x0001396C
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x00015774 File Offset: 0x00013974
		public int? Order { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0001577D File Offset: 0x0001397D
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x00015785 File Offset: 0x00013985
		public string UnderlyingName { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0001578E File Offset: 0x0001398E
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x00015796 File Offset: 0x00013996
		public IValueProvider ValueProvider { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0001579F File Offset: 0x0001399F
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x000157A7 File Offset: 0x000139A7
		public Type PropertyType { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x000157B0 File Offset: 0x000139B0
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x000157B8 File Offset: 0x000139B8
		public JsonConverter Converter { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x000157C1 File Offset: 0x000139C1
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x000157C9 File Offset: 0x000139C9
		public JsonConverter MemberConverter { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x000157D2 File Offset: 0x000139D2
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x000157DA File Offset: 0x000139DA
		public bool Ignored { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x000157E3 File Offset: 0x000139E3
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x000157EB File Offset: 0x000139EB
		public bool Readable { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x000157F4 File Offset: 0x000139F4
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x000157FC File Offset: 0x000139FC
		public bool Writable { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00015805 File Offset: 0x00013A05
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x0001580D File Offset: 0x00013A0D
		public object DefaultValue { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00015816 File Offset: 0x00013A16
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x0001581E File Offset: 0x00013A1E
		public Required Required { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00015827 File Offset: 0x00013A27
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x0001582F File Offset: 0x00013A2F
		public bool? IsReference { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x00015838 File Offset: 0x00013A38
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x00015840 File Offset: 0x00013A40
		public NullValueHandling? NullValueHandling { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x00015849 File Offset: 0x00013A49
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x00015851 File Offset: 0x00013A51
		public DefaultValueHandling? DefaultValueHandling { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0001585A File Offset: 0x00013A5A
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x00015862 File Offset: 0x00013A62
		public ReferenceLoopHandling? ReferenceLoopHandling { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0001586B File Offset: 0x00013A6B
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x00015873 File Offset: 0x00013A73
		public ObjectCreationHandling? ObjectCreationHandling { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0001587C File Offset: 0x00013A7C
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x00015884 File Offset: 0x00013A84
		public TypeNameHandling? TypeNameHandling { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0001588D File Offset: 0x00013A8D
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x00015895 File Offset: 0x00013A95
		public Predicate<object> ShouldSerialize { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0001589E File Offset: 0x00013A9E
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x000158A6 File Offset: 0x00013AA6
		public Predicate<object> GetIsSpecified { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x000158AF File Offset: 0x00013AAF
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x000158B7 File Offset: 0x00013AB7
		public Action<object, object> SetIsSpecified { get; set; }

		// Token: 0x0600064F RID: 1615 RVA: 0x000158C0 File Offset: 0x00013AC0
		public override string ToString()
		{
			return this.PropertyName;
		}
	}
}
