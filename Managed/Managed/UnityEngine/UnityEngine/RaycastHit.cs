using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200019B RID: 411
	public struct RaycastHit
	{
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x0002168C File Offset: 0x0001F88C
		// (set) Token: 0x06001382 RID: 4994 RVA: 0x00021694 File Offset: 0x0001F894
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
			set
			{
				this.m_Point = value;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x000216A0 File Offset: 0x0001F8A0
		// (set) Token: 0x06001384 RID: 4996 RVA: 0x000216A8 File Offset: 0x0001F8A8
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
			set
			{
				this.m_Normal = value;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x000216B4 File Offset: 0x0001F8B4
		// (set) Token: 0x06001386 RID: 4998 RVA: 0x000216FC File Offset: 0x0001F8FC
		public Vector3 barycentricCoordinate
		{
			get
			{
				return new Vector3(1f - (this.m_UV.y + this.m_UV.x), this.m_UV.x, this.m_UV.y);
			}
			set
			{
				this.m_UV = value;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x0002170C File Offset: 0x0001F90C
		// (set) Token: 0x06001388 RID: 5000 RVA: 0x00021714 File Offset: 0x0001F914
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = value;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x00021720 File Offset: 0x0001F920
		public int triangleIndex
		{
			get
			{
				return this.m_FaceID;
			}
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00021728 File Offset: 0x0001F928
		private static void CalculateRaycastTexCoord(out Vector2 output, Collider col, Vector2 uv, Vector3 point, int face, int index)
		{
			RaycastHit.INTERNAL_CALL_CalculateRaycastTexCoord(out output, col, ref uv, ref point, face, index);
		}

		// Token: 0x0600138B RID: 5003
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_CalculateRaycastTexCoord(out Vector2 output, Collider col, ref Vector2 uv, ref Vector3 point, int face, int index);

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x0002173C File Offset: 0x0001F93C
		public Vector2 textureCoord
		{
			get
			{
				Vector2 vector;
				RaycastHit.CalculateRaycastTexCoord(out vector, this.collider, this.m_UV, this.m_Point, this.m_FaceID, 0);
				return vector;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x0002176C File Offset: 0x0001F96C
		public Vector2 textureCoord2
		{
			get
			{
				Vector2 vector;
				RaycastHit.CalculateRaycastTexCoord(out vector, this.collider, this.m_UV, this.m_Point, this.m_FaceID, 1);
				return vector;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0002179C File Offset: 0x0001F99C
		[Obsolete("Use textureCoord2 instead")]
		public Vector2 textureCoord1
		{
			get
			{
				Vector2 vector;
				RaycastHit.CalculateRaycastTexCoord(out vector, this.collider, this.m_UV, this.m_Point, this.m_FaceID, 1);
				return vector;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x000217CC File Offset: 0x0001F9CC
		public Vector2 lightmapCoord
		{
			get
			{
				Vector2 vector;
				RaycastHit.CalculateRaycastTexCoord(out vector, this.collider, this.m_UV, this.m_Point, this.m_FaceID, 1);
				if (this.collider.renderer != null)
				{
					Vector4 lightmapTilingOffset = this.collider.renderer.lightmapTilingOffset;
					vector.x = vector.x * lightmapTilingOffset.x + lightmapTilingOffset.z;
					vector.y = vector.y * lightmapTilingOffset.y + lightmapTilingOffset.w;
				}
				return vector;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x00021860 File Offset: 0x0001FA60
		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x00021868 File Offset: 0x0001FA68
		public Rigidbody rigidbody
		{
			get
			{
				return (!(this.collider != null)) ? null : this.collider.attachedRigidbody;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x00021898 File Offset: 0x0001FA98
		public Transform transform
		{
			get
			{
				Rigidbody rigidbody = this.rigidbody;
				if (rigidbody != null)
				{
					return rigidbody.transform;
				}
				if (this.collider != null)
				{
					return this.collider.transform;
				}
				return null;
			}
		}

		// Token: 0x04000668 RID: 1640
		private Vector3 m_Point;

		// Token: 0x04000669 RID: 1641
		private Vector3 m_Normal;

		// Token: 0x0400066A RID: 1642
		private int m_FaceID;

		// Token: 0x0400066B RID: 1643
		private float m_Distance;

		// Token: 0x0400066C RID: 1644
		private Vector2 m_UV;

		// Token: 0x0400066D RID: 1645
		private Collider m_Collider;
	}
}
