using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000A9 RID: 169
	public struct CombineInstance
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000B668 File Offset: 0x00009868
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000B678 File Offset: 0x00009878
		public Mesh mesh
		{
			get
			{
				return this.InternalGetMesh(this.m_MeshInstanceID);
			}
			set
			{
				this.m_MeshInstanceID = ((!(value != null)) ? 0 : value.GetInstanceID());
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000B698 File Offset: 0x00009898
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000B6A0 File Offset: 0x000098A0
		public int subMeshIndex
		{
			get
			{
				return this.m_SubMeshIndex;
			}
			set
			{
				this.m_SubMeshIndex = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000B6AC File Offset: 0x000098AC
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000B6B4 File Offset: 0x000098B4
		public Matrix4x4 transform
		{
			get
			{
				return this.m_Transform;
			}
			set
			{
				this.m_Transform = value;
			}
		}

		// Token: 0x0600038D RID: 909
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Mesh InternalGetMesh(int instanceID);

		// Token: 0x04000264 RID: 612
		private int m_MeshInstanceID;

		// Token: 0x04000265 RID: 613
		private int m_SubMeshIndex;

		// Token: 0x04000266 RID: 614
		private Matrix4x4 m_Transform;
	}
}
