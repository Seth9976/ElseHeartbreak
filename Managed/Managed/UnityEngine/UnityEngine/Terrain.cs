using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000218 RID: 536
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public sealed class Terrain : MonoBehaviour
	{
		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x00024EF4 File Offset: 0x000230F4
		// (set) Token: 0x060019D0 RID: 6608 RVA: 0x00025050 File Offset: 0x00023250
		private IntPtr InstanceObject
		{
			get
			{
				this.MakeSureObjectIsAlive();
				if (this.m_TerrainInstance == IntPtr.Zero)
				{
					this.m_TerrainInstance = this.Construct();
					this.Internal_SetTerrainData(this.m_TerrainInstance, this.m_TerrainData);
					this.Internal_SetTreeDistance(this.m_TerrainInstance, this.m_TreeDistance);
					this.Internal_SetTreeBillboardDistance(this.m_TerrainInstance, this.m_TreeBillboardDistance);
					this.Internal_SetTreeCrossFadeLength(this.m_TerrainInstance, this.m_TreeCrossFadeLength);
					this.Internal_SetTreeMaximumFullLODCount(this.m_TerrainInstance, this.m_TreeMaximumFullLODCount);
					this.Internal_SetDetailObjectDistance(this.m_TerrainInstance, this.m_DetailObjectDistance);
					this.Internal_SetDetailObjectDensity(this.m_TerrainInstance, this.m_DetailObjectDensity);
					this.Internal_SetHeightmapPixelError(this.m_TerrainInstance, this.m_HeightmapPixelError);
					this.Internal_SetBasemapDistance(this.m_TerrainInstance, this.m_SplatMapDistance);
					this.Internal_SetHeightmapMaximumLOD(this.m_TerrainInstance, this.m_HeightmapMaximumLOD);
					this.Internal_SetCastShadows(this.m_TerrainInstance, this.m_CastShadows);
					this.Internal_SetLightmapIndex(this.m_TerrainInstance, this.m_LightmapIndex);
					this.Internal_SetLightmapSize(this.m_TerrainInstance, this.m_LightmapSize);
					this.Internal_SetDrawTreesAndFoliage(this.m_TerrainInstance, this.m_DrawTreesAndFoliage);
					this.Internal_SetCollectDetailPatches(this.m_TerrainInstance, this.m_CollectDetailPatches);
					this.Internal_SetMaterialTemplate(this.m_TerrainInstance, this.m_MaterialTemplate);
				}
				return this.m_TerrainInstance;
			}
			set
			{
				this.m_TerrainInstance = value;
			}
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x0002505C File Offset: 0x0002325C
		private void OnDestroy()
		{
			this.OnDisable();
			this.Cleanup(this.m_TerrainInstance);
			this.m_TerrainInstance = IntPtr.Zero;
		}

		// Token: 0x060019D2 RID: 6610
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void MakeSureObjectIsAlive();

		// Token: 0x060019D3 RID: 6611
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Cleanup(IntPtr terrainInstance);

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x0002507C File Offset: 0x0002327C
		// (set) Token: 0x060019D5 RID: 6613 RVA: 0x0002508C File Offset: 0x0002328C
		public TerrainRenderFlags editorRenderFlags
		{
			get
			{
				return (TerrainRenderFlags)this.GetEditorRenderFlags(this.InstanceObject);
			}
			set
			{
				this.SetEditorRenderFlags(this.InstanceObject, (int)value);
			}
		}

		// Token: 0x060019D6 RID: 6614
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetEditorRenderFlags(IntPtr terrainInstance);

		// Token: 0x060019D7 RID: 6615
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetEditorRenderFlags(IntPtr terrainInstance, int flags);

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x0002509C File Offset: 0x0002329C
		// (set) Token: 0x060019D9 RID: 6617 RVA: 0x000250E0 File Offset: 0x000232E0
		public TerrainData terrainData
		{
			get
			{
				if (this.m_TerrainData != this.Internal_GetTerrainData(this.InstanceObject))
				{
					this.Internal_SetTerrainData(this.InstanceObject, this.m_TerrainData);
				}
				return this.m_TerrainData;
			}
			set
			{
				this.m_TerrainData = value;
				this.Internal_SetTerrainData(this.InstanceObject, value);
			}
		}

		// Token: 0x060019DA RID: 6618
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern TerrainData Internal_GetTerrainData(IntPtr terrainInstance);

		// Token: 0x060019DB RID: 6619
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetTerrainData(IntPtr terrainInstance, TerrainData value);

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060019DC RID: 6620 RVA: 0x000250F8 File Offset: 0x000232F8
		// (set) Token: 0x060019DD RID: 6621 RVA: 0x00025134 File Offset: 0x00023334
		public float treeDistance
		{
			get
			{
				if (this.m_TreeDistance != this.Internal_GetTreeDistance(this.InstanceObject))
				{
					this.Internal_SetTreeDistance(this.InstanceObject, this.m_TreeDistance);
				}
				return this.m_TreeDistance;
			}
			set
			{
				this.m_TreeDistance = value;
				this.Internal_SetTreeDistance(this.InstanceObject, value);
			}
		}

		// Token: 0x060019DE RID: 6622
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float Internal_GetTreeDistance(IntPtr terrainInstance);

		// Token: 0x060019DF RID: 6623
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetTreeDistance(IntPtr terrainInstance, float value);

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060019E0 RID: 6624 RVA: 0x0002514C File Offset: 0x0002334C
		// (set) Token: 0x060019E1 RID: 6625 RVA: 0x00025188 File Offset: 0x00023388
		public float treeBillboardDistance
		{
			get
			{
				if (this.m_TreeBillboardDistance != this.Internal_GetTreeBillboardDistance(this.InstanceObject))
				{
					this.Internal_SetTreeBillboardDistance(this.InstanceObject, this.m_TreeBillboardDistance);
				}
				return this.m_TreeBillboardDistance;
			}
			set
			{
				this.m_TreeBillboardDistance = value;
				this.Internal_SetTreeBillboardDistance(this.InstanceObject, value);
			}
		}

		// Token: 0x060019E2 RID: 6626
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float Internal_GetTreeBillboardDistance(IntPtr terrainInstance);

		// Token: 0x060019E3 RID: 6627
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetTreeBillboardDistance(IntPtr terrainInstance, float value);

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x000251A0 File Offset: 0x000233A0
		// (set) Token: 0x060019E5 RID: 6629 RVA: 0x000251DC File Offset: 0x000233DC
		public float treeCrossFadeLength
		{
			get
			{
				if (this.m_TreeCrossFadeLength != this.Internal_GetTreeCrossFadeLength(this.InstanceObject))
				{
					this.Internal_SetTreeCrossFadeLength(this.InstanceObject, this.m_TreeCrossFadeLength);
				}
				return this.m_TreeCrossFadeLength;
			}
			set
			{
				this.m_TreeCrossFadeLength = value;
				this.Internal_SetTreeCrossFadeLength(this.InstanceObject, value);
			}
		}

		// Token: 0x060019E6 RID: 6630
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float Internal_GetTreeCrossFadeLength(IntPtr terrainInstance);

		// Token: 0x060019E7 RID: 6631
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetTreeCrossFadeLength(IntPtr terrainInstance, float value);

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x000251F4 File Offset: 0x000233F4
		// (set) Token: 0x060019E9 RID: 6633 RVA: 0x00025230 File Offset: 0x00023430
		public int treeMaximumFullLODCount
		{
			get
			{
				if (this.m_TreeMaximumFullLODCount != this.Internal_GetTreeMaximumFullLODCount(this.InstanceObject))
				{
					this.Internal_SetTreeMaximumFullLODCount(this.InstanceObject, this.m_TreeMaximumFullLODCount);
				}
				return this.m_TreeMaximumFullLODCount;
			}
			set
			{
				this.m_TreeMaximumFullLODCount = value;
				this.Internal_SetTreeMaximumFullLODCount(this.InstanceObject, value);
			}
		}

		// Token: 0x060019EA RID: 6634
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int Internal_GetTreeMaximumFullLODCount(IntPtr terrainInstance);

		// Token: 0x060019EB RID: 6635
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetTreeMaximumFullLODCount(IntPtr terrainInstance, int value);

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x00025248 File Offset: 0x00023448
		// (set) Token: 0x060019ED RID: 6637 RVA: 0x00025284 File Offset: 0x00023484
		public float detailObjectDistance
		{
			get
			{
				if (this.m_DetailObjectDistance != this.Internal_GetDetailObjectDistance(this.InstanceObject))
				{
					this.Internal_SetDetailObjectDistance(this.InstanceObject, this.m_DetailObjectDistance);
				}
				return this.m_DetailObjectDistance;
			}
			set
			{
				this.m_DetailObjectDistance = value;
				this.Internal_SetDetailObjectDistance(this.InstanceObject, value);
			}
		}

		// Token: 0x060019EE RID: 6638
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float Internal_GetDetailObjectDistance(IntPtr terrainInstance);

		// Token: 0x060019EF RID: 6639
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetDetailObjectDistance(IntPtr terrainInstance, float value);

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x0002529C File Offset: 0x0002349C
		// (set) Token: 0x060019F1 RID: 6641 RVA: 0x000252D8 File Offset: 0x000234D8
		public float detailObjectDensity
		{
			get
			{
				if (this.m_DetailObjectDensity != this.Internal_GetDetailObjectDensity(this.InstanceObject))
				{
					this.Internal_SetDetailObjectDensity(this.InstanceObject, this.m_DetailObjectDensity);
				}
				return this.m_DetailObjectDensity;
			}
			set
			{
				this.m_DetailObjectDensity = value;
				this.Internal_SetDetailObjectDensity(this.InstanceObject, value);
			}
		}

		// Token: 0x060019F2 RID: 6642
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float Internal_GetDetailObjectDensity(IntPtr terrainInstance);

		// Token: 0x060019F3 RID: 6643
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetDetailObjectDensity(IntPtr terrainInstance, float value);

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060019F4 RID: 6644 RVA: 0x000252F0 File Offset: 0x000234F0
		// (set) Token: 0x060019F5 RID: 6645 RVA: 0x0002532C File Offset: 0x0002352C
		public float heightmapPixelError
		{
			get
			{
				if (this.m_HeightmapPixelError != this.Internal_GetHeightmapPixelError(this.InstanceObject))
				{
					this.Internal_SetHeightmapPixelError(this.InstanceObject, this.m_HeightmapPixelError);
				}
				return this.m_HeightmapPixelError;
			}
			set
			{
				this.m_HeightmapPixelError = value;
				this.Internal_SetHeightmapPixelError(this.InstanceObject, value);
			}
		}

		// Token: 0x060019F6 RID: 6646
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float Internal_GetHeightmapPixelError(IntPtr terrainInstance);

		// Token: 0x060019F7 RID: 6647
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetHeightmapPixelError(IntPtr terrainInstance, float value);

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x00025344 File Offset: 0x00023544
		// (set) Token: 0x060019F9 RID: 6649 RVA: 0x00025380 File Offset: 0x00023580
		public int heightmapMaximumLOD
		{
			get
			{
				if (this.m_HeightmapMaximumLOD != this.Internal_GetHeightmapMaximumLOD(this.InstanceObject))
				{
					this.Internal_SetHeightmapMaximumLOD(this.InstanceObject, this.m_HeightmapMaximumLOD);
				}
				return this.m_HeightmapMaximumLOD;
			}
			set
			{
				this.m_HeightmapMaximumLOD = value;
				this.Internal_SetHeightmapMaximumLOD(this.InstanceObject, value);
			}
		}

		// Token: 0x060019FA RID: 6650
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int Internal_GetHeightmapMaximumLOD(IntPtr terrainInstance);

		// Token: 0x060019FB RID: 6651
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetHeightmapMaximumLOD(IntPtr terrainInstance, int value);

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x00025398 File Offset: 0x00023598
		// (set) Token: 0x060019FD RID: 6653 RVA: 0x000253D4 File Offset: 0x000235D4
		public float basemapDistance
		{
			get
			{
				if (this.m_SplatMapDistance != this.Internal_GetBasemapDistance(this.InstanceObject))
				{
					this.Internal_SetBasemapDistance(this.InstanceObject, this.m_SplatMapDistance);
				}
				return this.m_SplatMapDistance;
			}
			set
			{
				this.m_SplatMapDistance = value;
				this.Internal_SetBasemapDistance(this.InstanceObject, value);
			}
		}

		// Token: 0x060019FE RID: 6654
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float Internal_GetBasemapDistance(IntPtr terrainInstance);

		// Token: 0x060019FF RID: 6655
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetBasemapDistance(IntPtr terrainInstance, float value);

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x000253EC File Offset: 0x000235EC
		// (set) Token: 0x06001A01 RID: 6657 RVA: 0x000253F4 File Offset: 0x000235F4
		[Obsolete("use basemapDistance", true)]
		public float splatmapDistance
		{
			get
			{
				return this.basemapDistance;
			}
			set
			{
				this.basemapDistance = value;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x00025400 File Offset: 0x00023600
		// (set) Token: 0x06001A03 RID: 6659 RVA: 0x0002543C File Offset: 0x0002363C
		public int lightmapIndex
		{
			get
			{
				if (this.m_LightmapIndex != this.Internal_GetLightmapIndex(this.InstanceObject))
				{
					this.Internal_SetLightmapIndex(this.InstanceObject, this.m_LightmapIndex);
				}
				return this.m_LightmapIndex;
			}
			set
			{
				this.m_LightmapIndex = value;
				this.Internal_SetLightmapIndex(this.InstanceObject, value);
			}
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x00025454 File Offset: 0x00023654
		private void SetLightmapIndex(int value)
		{
			this.lightmapIndex = value;
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x00025460 File Offset: 0x00023660
		private void ShiftLightmapIndex(int offset)
		{
			this.lightmapIndex += offset;
		}

		// Token: 0x06001A06 RID: 6662
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int Internal_GetLightmapIndex(IntPtr terrainInstance);

		// Token: 0x06001A07 RID: 6663
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetLightmapIndex(IntPtr terrainInstance, int value);

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x00025470 File Offset: 0x00023670
		// (set) Token: 0x06001A09 RID: 6665 RVA: 0x000254AC File Offset: 0x000236AC
		internal int lightmapSize
		{
			get
			{
				if (this.m_LightmapSize != this.Internal_GetLightmapSize(this.InstanceObject))
				{
					this.Internal_SetLightmapSize(this.InstanceObject, this.m_LightmapSize);
				}
				return this.m_LightmapSize;
			}
			set
			{
				this.m_LightmapSize = value;
				this.Internal_SetLightmapSize(this.InstanceObject, value);
			}
		}

		// Token: 0x06001A0A RID: 6666
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int Internal_GetLightmapSize(IntPtr terrainInstance);

		// Token: 0x06001A0B RID: 6667
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetLightmapSize(IntPtr terrainInstance, int value);

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001A0C RID: 6668 RVA: 0x000254C4 File Offset: 0x000236C4
		// (set) Token: 0x06001A0D RID: 6669 RVA: 0x00025500 File Offset: 0x00023700
		public bool castShadows
		{
			get
			{
				if (this.m_CastShadows != this.Internal_GetCastShadows(this.InstanceObject))
				{
					this.Internal_SetCastShadows(this.InstanceObject, this.m_CastShadows);
				}
				return this.m_CastShadows;
			}
			set
			{
				this.m_CastShadows = value;
				this.Internal_SetCastShadows(this.InstanceObject, value);
			}
		}

		// Token: 0x06001A0E RID: 6670
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_GetCastShadows(IntPtr terrainInstance);

		// Token: 0x06001A0F RID: 6671
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetCastShadows(IntPtr terrainInstance, bool value);

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001A10 RID: 6672 RVA: 0x00025518 File Offset: 0x00023718
		// (set) Token: 0x06001A11 RID: 6673 RVA: 0x0002555C File Offset: 0x0002375C
		public Material materialTemplate
		{
			get
			{
				if (this.m_MaterialTemplate != this.Internal_GetMaterialTemplate(this.InstanceObject))
				{
					this.Internal_SetMaterialTemplate(this.InstanceObject, this.m_MaterialTemplate);
				}
				return this.m_MaterialTemplate;
			}
			set
			{
				this.m_MaterialTemplate = value;
				this.Internal_SetMaterialTemplate(this.InstanceObject, value);
			}
		}

		// Token: 0x06001A12 RID: 6674
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Material Internal_GetMaterialTemplate(IntPtr terrainInstance);

		// Token: 0x06001A13 RID: 6675
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetMaterialTemplate(IntPtr terrainInstance, Material value);

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x00025574 File Offset: 0x00023774
		// (set) Token: 0x06001A15 RID: 6677 RVA: 0x000255B0 File Offset: 0x000237B0
		internal bool drawTreesAndFoliage
		{
			get
			{
				if (this.m_DrawTreesAndFoliage != this.Internal_GetDrawTreesAndFoliage(this.InstanceObject))
				{
					this.Internal_SetDrawTreesAndFoliage(this.InstanceObject, this.m_DrawTreesAndFoliage);
				}
				return this.m_DrawTreesAndFoliage;
			}
			set
			{
				this.m_DrawTreesAndFoliage = value;
				this.Internal_SetDrawTreesAndFoliage(this.InstanceObject, value);
			}
		}

		// Token: 0x06001A16 RID: 6678
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_GetDrawTreesAndFoliage(IntPtr terrainInstance);

		// Token: 0x06001A17 RID: 6679
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetDrawTreesAndFoliage(IntPtr terrainInstance, bool value);

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x000255C8 File Offset: 0x000237C8
		// (set) Token: 0x06001A19 RID: 6681 RVA: 0x00025604 File Offset: 0x00023804
		public bool collectDetailPatches
		{
			get
			{
				if (this.m_CollectDetailPatches != this.Internal_GetCollectDetailPatches(this.InstanceObject))
				{
					this.Internal_SetCollectDetailPatches(this.InstanceObject, this.m_CollectDetailPatches);
				}
				return this.m_CollectDetailPatches;
			}
			set
			{
				this.m_CollectDetailPatches = value;
				this.Internal_SetCollectDetailPatches(this.InstanceObject, value);
			}
		}

		// Token: 0x06001A1A RID: 6682
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_GetCollectDetailPatches(IntPtr terrainInstance);

		// Token: 0x06001A1B RID: 6683
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetCollectDetailPatches(IntPtr terrainInstance, bool value);

		// Token: 0x06001A1C RID: 6684 RVA: 0x0002561C File Offset: 0x0002381C
		public float SampleHeight(Vector3 worldPosition)
		{
			return this.Internal_SampleHeight(this.InstanceObject, worldPosition);
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x0002562C File Offset: 0x0002382C
		private float Internal_SampleHeight(IntPtr terrainInstance, Vector3 worldPosition)
		{
			return Terrain.INTERNAL_CALL_Internal_SampleHeight(this, terrainInstance, ref worldPosition);
		}

		// Token: 0x06001A1E RID: 6686
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float INTERNAL_CALL_Internal_SampleHeight(Terrain self, IntPtr terrainInstance, ref Vector3 worldPosition);

		// Token: 0x06001A1F RID: 6687 RVA: 0x00025638 File Offset: 0x00023838
		internal void ApplyDelayedHeightmapModification()
		{
			this.Internal_ApplyDelayedHeightmapModification(this.InstanceObject);
		}

		// Token: 0x06001A20 RID: 6688
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_ApplyDelayedHeightmapModification(IntPtr terrainInstance);

		// Token: 0x06001A21 RID: 6689 RVA: 0x00025648 File Offset: 0x00023848
		public void AddTreeInstance(TreeInstance instance)
		{
			this.Internal_AddTreeInstance(this.InstanceObject, instance);
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00025658 File Offset: 0x00023858
		private void Internal_AddTreeInstance(IntPtr terrainInstance, TreeInstance instance)
		{
			Terrain.INTERNAL_CALL_Internal_AddTreeInstance(this, terrainInstance, ref instance);
		}

		// Token: 0x06001A23 RID: 6691
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_AddTreeInstance(Terrain self, IntPtr terrainInstance, ref TreeInstance instance);

		// Token: 0x06001A24 RID: 6692 RVA: 0x00025664 File Offset: 0x00023864
		public void SetNeighbors(Terrain left, Terrain top, Terrain right, Terrain bottom)
		{
			this.Internal_SetNeighbors(this.InstanceObject, (!(left != null)) ? IntPtr.Zero : left.InstanceObject, (!(top != null)) ? IntPtr.Zero : top.InstanceObject, (!(right != null)) ? IntPtr.Zero : right.InstanceObject, (!(bottom != null)) ? IntPtr.Zero : bottom.InstanceObject);
		}

		// Token: 0x06001A25 RID: 6693
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetNeighbors(IntPtr terrainInstance, IntPtr left, IntPtr top, IntPtr right, IntPtr bottom);

		// Token: 0x06001A26 RID: 6694 RVA: 0x000256F0 File Offset: 0x000238F0
		public Vector3 GetPosition()
		{
			return this.Internal_GetPosition(this.InstanceObject);
		}

		// Token: 0x06001A27 RID: 6695
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Vector3 Internal_GetPosition(IntPtr terrainInstance);

		// Token: 0x06001A28 RID: 6696 RVA: 0x00025700 File Offset: 0x00023900
		public void Flush()
		{
			this.Internal_Flush(this.InstanceObject);
		}

		// Token: 0x06001A29 RID: 6697
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_Flush(IntPtr terrainInstance);

		// Token: 0x06001A2A RID: 6698 RVA: 0x00025710 File Offset: 0x00023910
		internal void RemoveTrees(Vector2 position, float radius, int prototypeIndex)
		{
			this.Internal_RemoveTrees(this.InstanceObject, position, radius, prototypeIndex);
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00025724 File Offset: 0x00023924
		private void Internal_RemoveTrees(IntPtr terrainInstance, Vector2 position, float radius, int prototypeIndex)
		{
			Terrain.INTERNAL_CALL_Internal_RemoveTrees(this, terrainInstance, ref position, radius, prototypeIndex);
		}

		// Token: 0x06001A2C RID: 6700
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_RemoveTrees(Terrain self, IntPtr terrainInstance, ref Vector2 position, float radius, int prototypeIndex);

		// Token: 0x06001A2D RID: 6701 RVA: 0x00025734 File Offset: 0x00023934
		private void OnTerrainChanged(TerrainChangedFlags flags)
		{
			this.Internal_OnTerrainChanged(this.InstanceObject, flags);
		}

		// Token: 0x06001A2E RID: 6702
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_OnTerrainChanged(IntPtr terrainInstance, TerrainChangedFlags flags);

		// Token: 0x06001A2F RID: 6703
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr Construct();

		// Token: 0x06001A30 RID: 6704 RVA: 0x00025744 File Offset: 0x00023944
		internal void OnEnable()
		{
			this.Internal_OnEnable(this.InstanceObject);
		}

		// Token: 0x06001A31 RID: 6705
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_OnEnable(IntPtr terrainInstance);

		// Token: 0x06001A32 RID: 6706 RVA: 0x00025754 File Offset: 0x00023954
		internal void OnDisable()
		{
			this.Internal_OnDisable(this.InstanceObject);
		}

		// Token: 0x06001A33 RID: 6707
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_OnDisable(IntPtr terrainInstance);

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001A34 RID: 6708
		public static extern Terrain activeTerrain
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001A35 RID: 6709
		public static extern Terrain[] activeTerrains
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x00025764 File Offset: 0x00023964
		public static GameObject CreateTerrainGameObject(TerrainData assignTerrain)
		{
			GameObject gameObject = new GameObject("Terrain", new Type[]
			{
				typeof(Terrain),
				typeof(TerrainCollider)
			});
			gameObject.isStatic = true;
			Terrain terrain = gameObject.GetComponent(typeof(Terrain)) as Terrain;
			TerrainCollider terrainCollider = gameObject.GetComponent(typeof(TerrainCollider)) as TerrainCollider;
			terrainCollider.terrainData = assignTerrain;
			terrain.terrainData = assignTerrain;
			terrain.OnEnable();
			return gameObject;
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x000257E4 File Offset: 0x000239E4
		private static void ReconnectTerrainData()
		{
			List<Terrain> list = new List<Terrain>(Terrain.activeTerrains);
			foreach (Terrain terrain in list)
			{
				if (terrain.terrainData == null)
				{
					terrain.OnDisable();
				}
				else if (!terrain.terrainData.HasUser(terrain.gameObject))
				{
					terrain.OnDisable();
					terrain.OnEnable();
				}
			}
		}

		// Token: 0x04000833 RID: 2099
		[SerializeField]
		private TerrainData m_TerrainData;

		// Token: 0x04000834 RID: 2100
		[SerializeField]
		private float m_TreeDistance = 5000f;

		// Token: 0x04000835 RID: 2101
		[SerializeField]
		private float m_TreeBillboardDistance = 50f;

		// Token: 0x04000836 RID: 2102
		[SerializeField]
		private float m_TreeCrossFadeLength = 5f;

		// Token: 0x04000837 RID: 2103
		[SerializeField]
		private int m_TreeMaximumFullLODCount = 50;

		// Token: 0x04000838 RID: 2104
		[SerializeField]
		private float m_DetailObjectDistance = 80f;

		// Token: 0x04000839 RID: 2105
		[SerializeField]
		private float m_DetailObjectDensity = 1f;

		// Token: 0x0400083A RID: 2106
		[SerializeField]
		private float m_HeightmapPixelError = 5f;

		// Token: 0x0400083B RID: 2107
		[SerializeField]
		private float m_SplatMapDistance = 1000f;

		// Token: 0x0400083C RID: 2108
		[SerializeField]
		private int m_HeightmapMaximumLOD;

		// Token: 0x0400083D RID: 2109
		[SerializeField]
		private bool m_CastShadows = true;

		// Token: 0x0400083E RID: 2110
		[SerializeField]
		private int m_LightmapIndex = -1;

		// Token: 0x0400083F RID: 2111
		[SerializeField]
		private int m_LightmapSize = 1024;

		// Token: 0x04000840 RID: 2112
		[SerializeField]
		private bool m_DrawTreesAndFoliage = true;

		// Token: 0x04000841 RID: 2113
		[SerializeField]
		private bool m_CollectDetailPatches = true;

		// Token: 0x04000842 RID: 2114
		[SerializeField]
		private Material m_MaterialTemplate;

		// Token: 0x04000843 RID: 2115
		[NonSerialized]
		private IntPtr m_TerrainInstance;
	}
}
