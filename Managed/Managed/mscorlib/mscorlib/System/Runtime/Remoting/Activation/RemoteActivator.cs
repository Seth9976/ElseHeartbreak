using System;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000441 RID: 1089
	internal class RemoteActivator : MarshalByRefObject, IActivator
	{
		// Token: 0x06002E03 RID: 11779 RVA: 0x00099430 File Offset: 0x00097630
		public IConstructionReturnMessage Activate(IConstructionCallMessage msg)
		{
			if (!RemotingConfiguration.IsActivationAllowed(msg.ActivationType))
			{
				throw new RemotingException("The type " + msg.ActivationTypeName + " is not allowed to be client activated");
			}
			object[] array = new object[]
			{
				new RemoteActivationAttribute(msg.ContextProperties)
			};
			MarshalByRefObject marshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(msg.ActivationType, msg.Args, array);
			ObjRef objRef = RemotingServices.Marshal(marshalByRefObject);
			return new ConstructionResponse(objRef, null, msg);
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x000994A4 File Offset: 0x000976A4
		public override object InitializeLifetimeService()
		{
			ILease lease = (ILease)base.InitializeLifetimeService();
			if (lease.CurrentState == LeaseState.Initial)
			{
				lease.InitialLeaseTime = TimeSpan.FromMinutes(30.0);
				lease.SponsorshipTimeout = TimeSpan.FromMinutes(1.0);
				lease.RenewOnCallTime = TimeSpan.FromMinutes(10.0);
			}
			return lease;
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x00099508 File Offset: 0x00097708
		public ActivatorLevel Level
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002E06 RID: 11782 RVA: 0x00099510 File Offset: 0x00097710
		// (set) Token: 0x06002E07 RID: 11783 RVA: 0x00099518 File Offset: 0x00097718
		public IActivator NextActivator
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
