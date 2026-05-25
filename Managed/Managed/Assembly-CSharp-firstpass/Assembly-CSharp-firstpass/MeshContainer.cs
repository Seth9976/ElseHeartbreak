using System;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class MeshContainer
{
	// Token: 0x06000055 RID: 85 RVA: 0x000047FC File Offset: 0x000029FC
	public MeshContainer(Mesh m)
	{
		this.mesh = m;
		this.vertices = m.vertices;
		this.normals = m.normals;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00004824 File Offset: 0x00002A24
	public void Update()
	{
		this.mesh.vertices = this.vertices;
		this.mesh.normals = this.normals;
	}

	// Token: 0x0400005E RID: 94
	public Mesh mesh;

	// Token: 0x0400005F RID: 95
	public Vector3[] vertices;

	// Token: 0x04000060 RID: 96
	public Vector3[] normals;
}
