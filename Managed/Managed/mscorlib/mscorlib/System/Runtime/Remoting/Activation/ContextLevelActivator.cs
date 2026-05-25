using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200043D RID: 1085
	[Serializable]
	internal class ContextLevelActivator : IActivator
	{
		// Token: 0x06002DF3 RID: 11763 RVA: 0x00099364 File Offset: 0x00097564
		public ContextLevelActivator(IActivator next)
		{
			this.m_NextActivator = next;
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06002DF4 RID: 11764 RVA: 0x00099374 File Offset: 0x00097574
		public ActivatorLevel Level
		{
			get
			{
				return ActivatorLevel.Context;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x00099378 File Offset: 0x00097578
		// (set) Token: 0x06002DF6 RID: 11766 RVA: 0x00099380 File Offset: 0x00097580
		public IActivator NextActivator
		{
			get
			{
				return this.m_NextActivator;
			}
			set
			{
				this.m_NextActivator = value;
			}
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x0009938C File Offset: 0x0009758C
		public IConstructionReturnMessage Activate(IConstructionCallMessage ctorCall)
		{
			ServerIdentity serverIdentity = RemotingServices.CreateContextBoundObjectIdentity(ctorCall.ActivationType);
			RemotingServices.SetMessageTargetIdentity(ctorCall, serverIdentity);
			ConstructionCall constructionCall = ctorCall as ConstructionCall;
			if (constructionCall == null || !constructionCall.IsContextOk)
			{
				serverIdentity.Context = Context.CreateNewContext(ctorCall);
				Context context = Context.SwitchToContext(serverIdentity.Context);
				try
				{
					return this.m_NextActivator.Activate(ctorCall);
				}
				finally
				{
					Context.SwitchToContext(context);
				}
			}
			return this.m_NextActivator.Activate(ctorCall);
		}

		// Token: 0x040013C2 RID: 5058
		private IActivator m_NextActivator;
	}
}
