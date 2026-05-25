using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x0200003C RID: 60
	public class JsonSerializerSettings
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00008CFD File Offset: 0x00006EFD
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00008D05 File Offset: 0x00006F05
		public ReferenceLoopHandling ReferenceLoopHandling { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00008D0E File Offset: 0x00006F0E
		// (set) Token: 0x06000248 RID: 584 RVA: 0x00008D16 File Offset: 0x00006F16
		public MissingMemberHandling MissingMemberHandling { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00008D1F File Offset: 0x00006F1F
		// (set) Token: 0x0600024A RID: 586 RVA: 0x00008D27 File Offset: 0x00006F27
		public ObjectCreationHandling ObjectCreationHandling { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00008D30 File Offset: 0x00006F30
		// (set) Token: 0x0600024C RID: 588 RVA: 0x00008D38 File Offset: 0x00006F38
		public NullValueHandling NullValueHandling { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00008D41 File Offset: 0x00006F41
		// (set) Token: 0x0600024E RID: 590 RVA: 0x00008D49 File Offset: 0x00006F49
		public DefaultValueHandling DefaultValueHandling { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00008D52 File Offset: 0x00006F52
		// (set) Token: 0x06000250 RID: 592 RVA: 0x00008D5A File Offset: 0x00006F5A
		public IList<JsonConverter> Converters { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00008D63 File Offset: 0x00006F63
		// (set) Token: 0x06000252 RID: 594 RVA: 0x00008D6B File Offset: 0x00006F6B
		public PreserveReferencesHandling PreserveReferencesHandling { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00008D74 File Offset: 0x00006F74
		// (set) Token: 0x06000254 RID: 596 RVA: 0x00008D7C File Offset: 0x00006F7C
		public TypeNameHandling TypeNameHandling { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00008D85 File Offset: 0x00006F85
		// (set) Token: 0x06000256 RID: 598 RVA: 0x00008D8D File Offset: 0x00006F8D
		public FormatterAssemblyStyle TypeNameAssemblyFormat { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00008D96 File Offset: 0x00006F96
		// (set) Token: 0x06000258 RID: 600 RVA: 0x00008D9E File Offset: 0x00006F9E
		public ConstructorHandling ConstructorHandling { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00008DA7 File Offset: 0x00006FA7
		// (set) Token: 0x0600025A RID: 602 RVA: 0x00008DAF File Offset: 0x00006FAF
		public IContractResolver ContractResolver { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00008DB8 File Offset: 0x00006FB8
		// (set) Token: 0x0600025C RID: 604 RVA: 0x00008DC0 File Offset: 0x00006FC0
		public IReferenceResolver ReferenceResolver { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00008DC9 File Offset: 0x00006FC9
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00008DD1 File Offset: 0x00006FD1
		public SerializationBinder Binder { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00008DDA File Offset: 0x00006FDA
		// (set) Token: 0x06000260 RID: 608 RVA: 0x00008DE2 File Offset: 0x00006FE2
		public EventHandler<ErrorEventArgs> Error { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00008DEB File Offset: 0x00006FEB
		// (set) Token: 0x06000262 RID: 610 RVA: 0x00008DF3 File Offset: 0x00006FF3
		public StreamingContext Context { get; set; }

		// Token: 0x06000263 RID: 611 RVA: 0x00008DFC File Offset: 0x00006FFC
		public JsonSerializerSettings()
		{
			this.ReferenceLoopHandling = ReferenceLoopHandling.Error;
			this.MissingMemberHandling = MissingMemberHandling.Ignore;
			this.ObjectCreationHandling = ObjectCreationHandling.Auto;
			this.NullValueHandling = NullValueHandling.Include;
			this.DefaultValueHandling = DefaultValueHandling.Include;
			this.PreserveReferencesHandling = PreserveReferencesHandling.None;
			this.TypeNameHandling = TypeNameHandling.None;
			this.TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple;
			this.Context = JsonSerializerSettings.DefaultContext;
			this.Converters = new List<JsonConverter>();
		}

		// Token: 0x040000B6 RID: 182
		internal const ReferenceLoopHandling DefaultReferenceLoopHandling = ReferenceLoopHandling.Error;

		// Token: 0x040000B7 RID: 183
		internal const MissingMemberHandling DefaultMissingMemberHandling = MissingMemberHandling.Ignore;

		// Token: 0x040000B8 RID: 184
		internal const NullValueHandling DefaultNullValueHandling = NullValueHandling.Include;

		// Token: 0x040000B9 RID: 185
		internal const DefaultValueHandling DefaultDefaultValueHandling = DefaultValueHandling.Include;

		// Token: 0x040000BA RID: 186
		internal const ObjectCreationHandling DefaultObjectCreationHandling = ObjectCreationHandling.Auto;

		// Token: 0x040000BB RID: 187
		internal const PreserveReferencesHandling DefaultPreserveReferencesHandling = PreserveReferencesHandling.None;

		// Token: 0x040000BC RID: 188
		internal const ConstructorHandling DefaultConstructorHandling = ConstructorHandling.Default;

		// Token: 0x040000BD RID: 189
		internal const TypeNameHandling DefaultTypeNameHandling = TypeNameHandling.None;

		// Token: 0x040000BE RID: 190
		internal const FormatterAssemblyStyle DefaultTypeNameAssemblyFormat = FormatterAssemblyStyle.Simple;

		// Token: 0x040000BF RID: 191
		internal static readonly StreamingContext DefaultContext = default(StreamingContext);
	}
}
