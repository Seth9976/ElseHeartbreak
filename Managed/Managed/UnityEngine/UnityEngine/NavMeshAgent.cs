using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020001C6 RID: 454
	public sealed class NavMeshAgent : Behaviour
	{
		// Token: 0x060015DE RID: 5598 RVA: 0x000233C4 File Offset: 0x000215C4
		public bool SetDestination(Vector3 target)
		{
			return NavMeshAgent.INTERNAL_CALL_SetDestination(this, ref target);
		}

		// Token: 0x060015DF RID: 5599
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_SetDestination(NavMeshAgent self, ref Vector3 target);

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x000233D0 File Offset: 0x000215D0
		// (set) Token: 0x060015E1 RID: 5601 RVA: 0x000233E8 File Offset: 0x000215E8
		public Vector3 destination
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_destination(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_destination(ref value);
			}
		}

		// Token: 0x060015E2 RID: 5602
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_destination(out Vector3 value);

		// Token: 0x060015E3 RID: 5603
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_destination(ref Vector3 value);

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060015E4 RID: 5604
		// (set) Token: 0x060015E5 RID: 5605
		public extern float stoppingDistance
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x000233F4 File Offset: 0x000215F4
		// (set) Token: 0x060015E7 RID: 5607 RVA: 0x0002340C File Offset: 0x0002160C
		public Vector3 velocity
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_velocity(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_velocity(ref value);
			}
		}

		// Token: 0x060015E8 RID: 5608
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_velocity(out Vector3 value);

		// Token: 0x060015E9 RID: 5609
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_velocity(ref Vector3 value);

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x00023418 File Offset: 0x00021618
		// (set) Token: 0x060015EB RID: 5611 RVA: 0x00023430 File Offset: 0x00021630
		public Vector3 nextPosition
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_nextPosition(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_nextPosition(ref value);
			}
		}

		// Token: 0x060015EC RID: 5612
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_nextPosition(out Vector3 value);

		// Token: 0x060015ED RID: 5613
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_nextPosition(ref Vector3 value);

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x0002343C File Offset: 0x0002163C
		public Vector3 steeringTarget
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_steeringTarget(out vector);
				return vector;
			}
		}

		// Token: 0x060015EF RID: 5615
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_steeringTarget(out Vector3 value);

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x00023454 File Offset: 0x00021654
		public Vector3 desiredVelocity
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_desiredVelocity(out vector);
				return vector;
			}
		}

		// Token: 0x060015F1 RID: 5617
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_desiredVelocity(out Vector3 value);

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060015F2 RID: 5618
		public extern float remainingDistance
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060015F3 RID: 5619
		// (set) Token: 0x060015F4 RID: 5620
		public extern float baseOffset
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060015F5 RID: 5621
		public extern bool isOnOffMeshLink
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060015F6 RID: 5622
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ActivateCurrentOffMeshLink(bool activated);

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x0002346C File Offset: 0x0002166C
		public OffMeshLinkData currentOffMeshLinkData
		{
			get
			{
				return this.GetCurrentOffMeshLinkDataInternal();
			}
		}

		// Token: 0x060015F8 RID: 5624
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern OffMeshLinkData GetCurrentOffMeshLinkDataInternal();

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x00023474 File Offset: 0x00021674
		public OffMeshLinkData nextOffMeshLinkData
		{
			get
			{
				return this.GetNextOffMeshLinkDataInternal();
			}
		}

		// Token: 0x060015FA RID: 5626
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern OffMeshLinkData GetNextOffMeshLinkDataInternal();

		// Token: 0x060015FB RID: 5627
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CompleteOffMeshLink();

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060015FC RID: 5628
		// (set) Token: 0x060015FD RID: 5629
		public extern bool autoTraverseOffMeshLink
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060015FE RID: 5630
		// (set) Token: 0x060015FF RID: 5631
		public extern bool autoBraking
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001600 RID: 5632
		// (set) Token: 0x06001601 RID: 5633
		public extern bool autoRepath
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001602 RID: 5634
		public extern bool hasPath
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001603 RID: 5635
		public extern bool pathPending
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001604 RID: 5636
		public extern bool isPathStale
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001605 RID: 5637
		public extern NavMeshPathStatus pathStatus
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x0002347C File Offset: 0x0002167C
		public Vector3 pathEndPosition
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_pathEndPosition(out vector);
				return vector;
			}
		}

		// Token: 0x06001607 RID: 5639
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_pathEndPosition(out Vector3 value);

		// Token: 0x06001608 RID: 5640 RVA: 0x00023494 File Offset: 0x00021694
		public bool Warp(Vector3 newPosition)
		{
			return NavMeshAgent.INTERNAL_CALL_Warp(this, ref newPosition);
		}

		// Token: 0x06001609 RID: 5641
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Warp(NavMeshAgent self, ref Vector3 newPosition);

		// Token: 0x0600160A RID: 5642 RVA: 0x000234A0 File Offset: 0x000216A0
		public void Move(Vector3 offset)
		{
			NavMeshAgent.INTERNAL_CALL_Move(this, ref offset);
		}

		// Token: 0x0600160B RID: 5643
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Move(NavMeshAgent self, ref Vector3 offset);

		// Token: 0x0600160C RID: 5644
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop([DefaultValue("false")] bool stopUpdates);

		// Token: 0x0600160D RID: 5645 RVA: 0x000234AC File Offset: 0x000216AC
		[ExcludeFromDocs]
		public void Stop()
		{
			bool flag = false;
			this.Stop(flag);
		}

		// Token: 0x0600160E RID: 5646
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Resume();

		// Token: 0x0600160F RID: 5647
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetPath();

		// Token: 0x06001610 RID: 5648
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetPath(NavMeshPath path);

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x000234C4 File Offset: 0x000216C4
		// (set) Token: 0x06001612 RID: 5650 RVA: 0x000234E0 File Offset: 0x000216E0
		public NavMeshPath path
		{
			get
			{
				NavMeshPath navMeshPath = new NavMeshPath();
				this.CopyPathTo(navMeshPath);
				return navMeshPath;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException();
				}
				this.SetPath(value);
			}
		}

		// Token: 0x06001613 RID: 5651
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void CopyPathTo(NavMeshPath path);

		// Token: 0x06001614 RID: 5652
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool FindClosestEdge(out NavMeshHit hit);

		// Token: 0x06001615 RID: 5653 RVA: 0x000234F8 File Offset: 0x000216F8
		public bool Raycast(Vector3 targetPosition, out NavMeshHit hit)
		{
			return NavMeshAgent.INTERNAL_CALL_Raycast(this, ref targetPosition, out hit);
		}

		// Token: 0x06001616 RID: 5654
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Raycast(NavMeshAgent self, ref Vector3 targetPosition, out NavMeshHit hit);

		// Token: 0x06001617 RID: 5655 RVA: 0x00023504 File Offset: 0x00021704
		public bool CalculatePath(Vector3 targetPosition, NavMeshPath path)
		{
			path.ClearCorners();
			return this.CalculatePathInternal(targetPosition, path);
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00023514 File Offset: 0x00021714
		private bool CalculatePathInternal(Vector3 targetPosition, NavMeshPath path)
		{
			return NavMeshAgent.INTERNAL_CALL_CalculatePathInternal(this, ref targetPosition, path);
		}

		// Token: 0x06001619 RID: 5657
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_CalculatePathInternal(NavMeshAgent self, ref Vector3 targetPosition, NavMeshPath path);

		// Token: 0x0600161A RID: 5658
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SamplePathPosition(int passableMask, float maxDistance, out NavMeshHit hit);

		// Token: 0x0600161B RID: 5659
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLayerCost(int layer, float cost);

		// Token: 0x0600161C RID: 5660
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetLayerCost(int layer);

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x0600161D RID: 5661
		// (set) Token: 0x0600161E RID: 5662
		public extern int walkableMask
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x0600161F RID: 5663
		// (set) Token: 0x06001620 RID: 5664
		public extern float speed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001621 RID: 5665
		// (set) Token: 0x06001622 RID: 5666
		public extern float angularSpeed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001623 RID: 5667
		// (set) Token: 0x06001624 RID: 5668
		public extern float acceleration
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001625 RID: 5669
		// (set) Token: 0x06001626 RID: 5670
		public extern bool updatePosition
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001627 RID: 5671
		// (set) Token: 0x06001628 RID: 5672
		public extern bool updateRotation
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001629 RID: 5673
		// (set) Token: 0x0600162A RID: 5674
		public extern float radius
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600162B RID: 5675
		// (set) Token: 0x0600162C RID: 5676
		public extern float height
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600162D RID: 5677
		// (set) Token: 0x0600162E RID: 5678
		public extern ObstacleAvoidanceType obstacleAvoidanceType
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600162F RID: 5679
		// (set) Token: 0x06001630 RID: 5680
		public extern int avoidancePriority
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
