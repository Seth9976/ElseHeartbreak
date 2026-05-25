using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000215 RID: 533
	public sealed class TerrainData : Object
	{
		// Token: 0x06001980 RID: 6528 RVA: 0x00024C30 File Offset: 0x00022E30
		public TerrainData()
		{
			this.Internal_Create(this);
		}

		// Token: 0x06001981 RID: 6529
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_Create([Writable] TerrainData terrainData);

		// Token: 0x06001982 RID: 6530
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool HasUser(GameObject user);

		// Token: 0x06001983 RID: 6531
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void AddUser(GameObject user);

		// Token: 0x06001984 RID: 6532
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RemoveUser(GameObject user);

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001985 RID: 6533
		// (set) Token: 0x06001986 RID: 6534
		public extern PhysicMaterial physicMaterial
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001987 RID: 6535
		public extern int heightmapWidth
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001988 RID: 6536
		public extern int heightmapHeight
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001989 RID: 6537
		// (set) Token: 0x0600198A RID: 6538
		public extern int heightmapResolution
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x0600198B RID: 6539 RVA: 0x00024C40 File Offset: 0x00022E40
		public Vector3 heightmapScale
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_heightmapScale(out vector);
				return vector;
			}
		}

		// Token: 0x0600198C RID: 6540
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_heightmapScale(out Vector3 value);

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x0600198D RID: 6541 RVA: 0x00024C58 File Offset: 0x00022E58
		// (set) Token: 0x0600198E RID: 6542 RVA: 0x00024C70 File Offset: 0x00022E70
		public Vector3 size
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_size(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_size(ref value);
			}
		}

		// Token: 0x0600198F RID: 6543
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_size(out Vector3 value);

		// Token: 0x06001990 RID: 6544
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_size(ref Vector3 value);

		// Token: 0x06001991 RID: 6545
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetHeight(int x, int y);

		// Token: 0x06001992 RID: 6546
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetInterpolatedHeight(float x, float y);

		// Token: 0x06001993 RID: 6547
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float[,] GetHeights(int xBase, int yBase, int width, int height);

		// Token: 0x06001994 RID: 6548 RVA: 0x00024C7C File Offset: 0x00022E7C
		public void SetHeights(int xBase, int yBase, float[,] heights)
		{
			if (heights == null)
			{
				throw new NullReferenceException();
			}
			if (xBase + heights.GetLength(1) > this.heightmapWidth || xBase + heights.GetLength(1) < 0 || yBase + heights.GetLength(0) < 0 || xBase < 0 || yBase < 0 || yBase + heights.GetLength(0) > this.heightmapHeight)
			{
				throw new ArgumentException(UnityString.Format("X or Y base out of bounds. Setting up to {0}x{1} while map size is {2}x{3}", new object[]
				{
					xBase + heights.GetLength(1),
					yBase + heights.GetLength(0),
					this.heightmapWidth,
					this.heightmapHeight
				}));
			}
			this.Internal_SetHeights(xBase, yBase, heights.GetLength(1), heights.GetLength(0), heights);
		}

		// Token: 0x06001995 RID: 6549
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetHeights(int xBase, int yBase, int width, int height, float[,] heights);

		// Token: 0x06001996 RID: 6550
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetHeightsDelayLOD(int xBase, int yBase, int width, int height, float[,] heights);

		// Token: 0x06001997 RID: 6551 RVA: 0x00024D54 File Offset: 0x00022F54
		internal void SetHeightsDelayLOD(int xBase, int yBase, float[,] heights)
		{
			this.Internal_SetHeightsDelayLOD(xBase, yBase, heights.GetLength(1), heights.GetLength(0), heights);
		}

		// Token: 0x06001998 RID: 6552
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetSteepness(float x, float y);

		// Token: 0x06001999 RID: 6553
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector3 GetInterpolatedNormal(float x, float y);

		// Token: 0x0600199A RID: 6554
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetAdjustedSize(int size);

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x0600199B RID: 6555
		// (set) Token: 0x0600199C RID: 6556
		public extern float wavingGrassStrength
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x0600199D RID: 6557
		// (set) Token: 0x0600199E RID: 6558
		public extern float wavingGrassAmount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x0600199F RID: 6559
		// (set) Token: 0x060019A0 RID: 6560
		public extern float wavingGrassSpeed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x00024D78 File Offset: 0x00022F78
		// (set) Token: 0x060019A2 RID: 6562 RVA: 0x00024D90 File Offset: 0x00022F90
		public Color wavingGrassTint
		{
			get
			{
				Color color;
				this.INTERNAL_get_wavingGrassTint(out color);
				return color;
			}
			set
			{
				this.INTERNAL_set_wavingGrassTint(ref value);
			}
		}

		// Token: 0x060019A3 RID: 6563
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_wavingGrassTint(out Color value);

		// Token: 0x060019A4 RID: 6564
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_wavingGrassTint(ref Color value);

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060019A5 RID: 6565
		public extern int detailWidth
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060019A6 RID: 6566
		public extern int detailHeight
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060019A7 RID: 6567
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetDetailResolution(int detailResolution, int resolutionPerPatch);

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060019A8 RID: 6568
		public extern int detailResolution
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060019A9 RID: 6569
		internal extern int detailResolutionPerPatch
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060019AA RID: 6570
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void ResetDirtyDetails();

		// Token: 0x060019AB RID: 6571
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RefreshPrototypes();

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060019AC RID: 6572
		// (set) Token: 0x060019AD RID: 6573
		public extern DetailPrototype[] detailPrototypes
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060019AE RID: 6574
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[] GetSupportedLayers(int xBase, int yBase, int totalWidth, int totalHeight);

		// Token: 0x060019AF RID: 6575
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[,] GetDetailLayer(int xBase, int yBase, int width, int height, int layer);

		// Token: 0x060019B0 RID: 6576 RVA: 0x00024D9C File Offset: 0x00022F9C
		public void SetDetailLayer(int xBase, int yBase, int layer, int[,] details)
		{
			this.Internal_SetDetailLayer(xBase, yBase, details.GetLength(1), details.GetLength(0), layer, details);
		}

		// Token: 0x060019B1 RID: 6577
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetDetailLayer(int xBase, int yBase, int totalWidth, int totalHeight, int detailIndex, int[,] data);

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060019B2 RID: 6578
		// (set) Token: 0x060019B3 RID: 6579
		public extern TreeInstance[] treeInstances
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060019B4 RID: 6580
		// (set) Token: 0x060019B5 RID: 6581
		public extern TreePrototype[] treePrototypes
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060019B6 RID: 6582
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RemoveTreePrototype(int index);

		// Token: 0x060019B7 RID: 6583
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RecalculateTreePositions();

		// Token: 0x060019B8 RID: 6584
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RemoveDetailPrototype(int index);

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060019B9 RID: 6585
		public extern int alphamapLayers
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060019BA RID: 6586
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float[,,] GetAlphamaps(int x, int y, int width, int height);

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060019BB RID: 6587
		// (set) Token: 0x060019BC RID: 6588
		public extern int alphamapResolution
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060019BD RID: 6589
		public extern int alphamapWidth
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060019BE RID: 6590
		public extern int alphamapHeight
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060019BF RID: 6591
		// (set) Token: 0x060019C0 RID: 6592
		public extern int baseMapResolution
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00024DC4 File Offset: 0x00022FC4
		public void SetAlphamaps(int x, int y, float[,,] map)
		{
			if (map.GetLength(2) != this.alphamapLayers)
			{
				throw new Exception(UnityString.Format("Float array size wrong (layers should be {0})", new object[] { this.alphamapLayers }));
			}
			this.Internal_SetAlphamaps(x, y, map.GetLength(1), map.GetLength(0), map);
		}

		// Token: 0x060019C2 RID: 6594
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetAlphamaps(int x, int y, int width, int height, float[,,] map);

		// Token: 0x060019C3 RID: 6595
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RecalculateBasemapIfDirty();

		// Token: 0x060019C4 RID: 6596
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetBasemapDirty(bool dirty);

		// Token: 0x060019C5 RID: 6597
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Texture2D GetAlphamapTexture(int index);

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060019C6 RID: 6598
		private extern int alphamapTextureCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x00024E20 File Offset: 0x00023020
		internal Texture2D[] alphamapTextures
		{
			get
			{
				Texture2D[] array = new Texture2D[this.alphamapTextureCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.GetAlphamapTexture(i);
				}
				return array;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060019C8 RID: 6600
		// (set) Token: 0x060019C9 RID: 6601
		public extern SplatPrototype[] splatPrototypes
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060019CA RID: 6602
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool HasTreeInstances();

		// Token: 0x060019CB RID: 6603
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void AddTree(out TreeInstance tree);

		// Token: 0x060019CC RID: 6604 RVA: 0x00024E58 File Offset: 0x00023058
		internal int RemoveTrees(Vector2 position, float radius, int prototypeIndex)
		{
			return TerrainData.INTERNAL_CALL_RemoveTrees(this, ref position, radius, prototypeIndex);
		}

		// Token: 0x060019CD RID: 6605
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_RemoveTrees(TerrainData self, ref Vector2 position, float radius, int prototypeIndex);
	}
}
