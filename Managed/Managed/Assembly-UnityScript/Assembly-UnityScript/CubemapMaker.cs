using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

// Token: 0x02000003 RID: 3
[Serializable]
public class CubemapMaker : MonoBehaviour
{
	// Token: 0x06000002 RID: 2 RVA: 0x0000210C File Offset: 0x0000030C
	public CubemapMaker()
	{
		this.path = "CubemapMaker/CreatedCubemap";
		this.filename = "CreatedCubemap";
		this.size = 1024;
		this.textureFormat = TextureFormat.ARGB32;
		this.cameraOptions = new CameraOptions();
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002148 File Offset: 0x00000348
	public virtual void Update()
	{
		if (Input.GetKeyDown(KeyCode.F12))
		{
			this.StartCoroutine_Auto(this.CreateCubemap());
		}
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002168 File Offset: 0x00000368
	public virtual IEnumerator CreateCubemap()
	{
		return new CubemapMaker.$CreateCubemap$4(this).GetEnumerator();
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002178 File Offset: 0x00000378
	private IEnumerator Snapshot(Cubemap cubemap, CubemapFace face, Camera cam)
	{
		return new CubemapMaker.$Snapshot$10(cubemap, face, cam, this).GetEnumerator();
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002188 File Offset: 0x00000388
	private Quaternion RotationOf(CubemapFace face)
	{
		Quaternion quaternion = default(Quaternion);
		if (face == CubemapFace.PositiveX)
		{
			quaternion = Quaternion.Euler((float)0, (float)90, (float)0);
		}
		else if (face == CubemapFace.NegativeX)
		{
			quaternion = Quaternion.Euler((float)0, (float)(-90), (float)0);
		}
		else if (face == CubemapFace.PositiveY)
		{
			quaternion = Quaternion.Euler((float)(-90), (float)0, (float)0);
		}
		else if (face == CubemapFace.NegativeY)
		{
			quaternion = Quaternion.Euler((float)90, (float)0, (float)0);
		}
		else if (face == CubemapFace.NegativeZ)
		{
			quaternion = Quaternion.Euler((float)0, (float)180, (float)0);
		}
		else
		{
			quaternion = Quaternion.identity;
		}
		return quaternion;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000222C File Offset: 0x0000042C
	public virtual void OnDrawGizmos()
	{
		Gizmos.DrawSphere(this.transform.position, 0.4f);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002244 File Offset: 0x00000444
	public virtual void Main()
	{
	}

	// Token: 0x04000004 RID: 4
	public string path;

	// Token: 0x04000005 RID: 5
	public string filename;

	// Token: 0x04000006 RID: 6
	public int size;

	// Token: 0x04000007 RID: 7
	private TextureFormat textureFormat;

	// Token: 0x04000008 RID: 8
	public bool mipmap;

	// Token: 0x04000009 RID: 9
	public CameraOptions cameraOptions;

	// Token: 0x02000004 RID: 4
	[CompilerGenerated]
	[Serializable]
	internal sealed class $CreateCubemap$4 : GenericGenerator<Coroutine>
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002248 File Offset: 0x00000448
		public $CreateCubemap$4(CubemapMaker self_)
		{
			this.$self_$9 = self_;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002258 File Offset: 0x00000458
		public override IEnumerator<Coroutine> GetEnumerator()
		{
			return new CubemapMaker.$CreateCubemap$4.$(this.$self_$9);
		}

		// Token: 0x0400000A RID: 10
		internal CubemapMaker $self_$9;
	}

	// Token: 0x02000006 RID: 6
	[CompilerGenerated]
	[Serializable]
	internal sealed class $Snapshot$10 : GenericGenerator<Type>
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002544 File Offset: 0x00000744
		public $Snapshot$10(Cubemap cubemap, CubemapFace face, Camera cam, CubemapMaker self_)
		{
			this.$cubemap$20 = cubemap;
			this.$face$21 = face;
			this.$cam$22 = cam;
			this.$self_$23 = self_;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002578 File Offset: 0x00000778
		public override IEnumerator<Type> GetEnumerator()
		{
			return new CubemapMaker.$Snapshot$10.$(this.$cubemap$20, this.$face$21, this.$cam$22, this.$self_$23);
		}

		// Token: 0x0400000F RID: 15
		internal Cubemap $cubemap$20;

		// Token: 0x04000010 RID: 16
		internal CubemapFace $face$21;

		// Token: 0x04000011 RID: 17
		internal Camera $cam$22;

		// Token: 0x04000012 RID: 18
		internal CubemapMaker $self_$23;
	}
}
