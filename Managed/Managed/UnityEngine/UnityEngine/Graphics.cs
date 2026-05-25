using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000C2 RID: 194
	public sealed class Graphics
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x0000BFFC File Offset: 0x0000A1FC
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex)
		{
			MaterialPropertyBlock materialPropertyBlock = null;
			Graphics.DrawMesh(mesh, position, rotation, material, layer, camera, submeshIndex, materialPropertyBlock);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000C01C File Offset: 0x0000A21C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera)
		{
			MaterialPropertyBlock materialPropertyBlock = null;
			int num = 0;
			Graphics.DrawMesh(mesh, position, rotation, material, layer, camera, num, materialPropertyBlock);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000C03C File Offset: 0x0000A23C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer)
		{
			MaterialPropertyBlock materialPropertyBlock = null;
			int num = 0;
			Camera camera = null;
			Graphics.DrawMesh(mesh, position, rotation, material, layer, camera, num, materialPropertyBlock);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000C060 File Offset: 0x0000A260
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, [DefaultValue("null")] Camera camera, [DefaultValue("0")] int submeshIndex, [DefaultValue("null")] MaterialPropertyBlock properties)
		{
			Internal_DrawMeshTRArguments internal_DrawMeshTRArguments = default(Internal_DrawMeshTRArguments);
			internal_DrawMeshTRArguments.position = position;
			internal_DrawMeshTRArguments.rotation = rotation;
			internal_DrawMeshTRArguments.layer = layer;
			internal_DrawMeshTRArguments.submeshIndex = submeshIndex;
			internal_DrawMeshTRArguments.castShadows = 1;
			internal_DrawMeshTRArguments.receiveShadows = 1;
			Graphics.Internal_DrawMeshTR(ref internal_DrawMeshTRArguments, properties, material, mesh, camera);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000C0B4 File Offset: 0x0000A2B4
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex)
		{
			MaterialPropertyBlock materialPropertyBlock = null;
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, materialPropertyBlock);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000C0D4 File Offset: 0x0000A2D4
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera)
		{
			MaterialPropertyBlock materialPropertyBlock = null;
			int num = 0;
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, num, materialPropertyBlock);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000C0F4 File Offset: 0x0000A2F4
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer)
		{
			MaterialPropertyBlock materialPropertyBlock = null;
			int num = 0;
			Camera camera = null;
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, num, materialPropertyBlock);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000C114 File Offset: 0x0000A314
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, [DefaultValue("null")] Camera camera, [DefaultValue("0")] int submeshIndex, [DefaultValue("null")] MaterialPropertyBlock properties)
		{
			Internal_DrawMeshMatrixArguments internal_DrawMeshMatrixArguments = default(Internal_DrawMeshMatrixArguments);
			internal_DrawMeshMatrixArguments.matrix = matrix;
			internal_DrawMeshMatrixArguments.layer = layer;
			internal_DrawMeshMatrixArguments.submeshIndex = submeshIndex;
			internal_DrawMeshMatrixArguments.castShadows = 1;
			internal_DrawMeshMatrixArguments.receiveShadows = 1;
			Graphics.Internal_DrawMeshMatrix(ref internal_DrawMeshMatrixArguments, properties, material, mesh, camera);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000C160 File Offset: 0x0000A360
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows, bool receiveShadows)
		{
			Internal_DrawMeshTRArguments internal_DrawMeshTRArguments = default(Internal_DrawMeshTRArguments);
			internal_DrawMeshTRArguments.position = position;
			internal_DrawMeshTRArguments.rotation = rotation;
			internal_DrawMeshTRArguments.layer = layer;
			internal_DrawMeshTRArguments.submeshIndex = submeshIndex;
			internal_DrawMeshTRArguments.castShadows = ((!castShadows) ? 0 : 1);
			internal_DrawMeshTRArguments.receiveShadows = ((!receiveShadows) ? 0 : 1);
			Graphics.Internal_DrawMeshTR(ref internal_DrawMeshTRArguments, properties, material, mesh, camera);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000C1D0 File Offset: 0x0000A3D0
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows, bool receiveShadows)
		{
			Internal_DrawMeshMatrixArguments internal_DrawMeshMatrixArguments = default(Internal_DrawMeshMatrixArguments);
			internal_DrawMeshMatrixArguments.matrix = matrix;
			internal_DrawMeshMatrixArguments.layer = layer;
			internal_DrawMeshMatrixArguments.submeshIndex = submeshIndex;
			internal_DrawMeshMatrixArguments.castShadows = ((!castShadows) ? 0 : 1);
			internal_DrawMeshMatrixArguments.receiveShadows = ((!receiveShadows) ? 0 : 1);
			Graphics.Internal_DrawMeshMatrix(ref internal_DrawMeshMatrixArguments, properties, material, mesh, camera);
		}

		// Token: 0x0600051B RID: 1307
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshTR(ref Internal_DrawMeshTRArguments arguments, MaterialPropertyBlock properties, Material material, Mesh mesh, Camera camera);

		// Token: 0x0600051C RID: 1308
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshMatrix(ref Internal_DrawMeshMatrixArguments arguments, MaterialPropertyBlock properties, Material material, Mesh mesh, Camera camera);

		// Token: 0x0600051D RID: 1309 RVA: 0x0000C238 File Offset: 0x0000A438
		public static void DrawMeshNow(Mesh mesh, Vector3 position, Quaternion rotation)
		{
			Graphics.Internal_DrawMeshNow1(mesh, position, rotation, -1);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000C244 File Offset: 0x0000A444
		public static void DrawMeshNow(Mesh mesh, Vector3 position, Quaternion rotation, int materialIndex)
		{
			Graphics.Internal_DrawMeshNow1(mesh, position, rotation, materialIndex);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000C250 File Offset: 0x0000A450
		public static void DrawMeshNow(Mesh mesh, Matrix4x4 matrix)
		{
			Graphics.Internal_DrawMeshNow2(mesh, matrix, -1);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000C25C File Offset: 0x0000A45C
		public static void DrawMeshNow(Mesh mesh, Matrix4x4 matrix, int materialIndex)
		{
			Graphics.Internal_DrawMeshNow2(mesh, matrix, materialIndex);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000C268 File Offset: 0x0000A468
		private static void Internal_DrawMeshNow1(Mesh mesh, Vector3 position, Quaternion rotation, int materialIndex)
		{
			Graphics.INTERNAL_CALL_Internal_DrawMeshNow1(mesh, ref position, ref rotation, materialIndex);
		}

		// Token: 0x06000522 RID: 1314
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_DrawMeshNow1(Mesh mesh, ref Vector3 position, ref Quaternion rotation, int materialIndex);

		// Token: 0x06000523 RID: 1315 RVA: 0x0000C278 File Offset: 0x0000A478
		private static void Internal_DrawMeshNow2(Mesh mesh, Matrix4x4 matrix, int materialIndex)
		{
			Graphics.INTERNAL_CALL_Internal_DrawMeshNow2(mesh, ref matrix, materialIndex);
		}

		// Token: 0x06000524 RID: 1316
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_DrawMeshNow2(Mesh mesh, ref Matrix4x4 matrix, int materialIndex);

		// Token: 0x06000525 RID: 1317
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DrawProcedural(MeshTopology topology, int vertexCount, [DefaultValue("1")] int instanceCount);

		// Token: 0x06000526 RID: 1318 RVA: 0x0000C284 File Offset: 0x0000A484
		[ExcludeFromDocs]
		public static void DrawProcedural(MeshTopology topology, int vertexCount)
		{
			int num = 1;
			Graphics.DrawProcedural(topology, vertexCount, num);
		}

		// Token: 0x06000527 RID: 1319
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DrawProceduralIndirect(MeshTopology topology, ComputeBuffer bufferWithArgs, [DefaultValue("0")] int argsOffset);

		// Token: 0x06000528 RID: 1320 RVA: 0x0000C29C File Offset: 0x0000A49C
		[ExcludeFromDocs]
		public static void DrawProceduralIndirect(MeshTopology topology, ComputeBuffer bufferWithArgs)
		{
			int num = 0;
			Graphics.DrawProceduralIndirect(topology, bufferWithArgs, num);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000C2B4 File Offset: 0x0000A4B4
		[Obsolete("Use Graphics.DrawMeshNow instead.")]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation)
		{
			Graphics.Internal_DrawMeshNow1(mesh, position, rotation, -1);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
		[Obsolete("Use Graphics.DrawMeshNow instead.")]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, int materialIndex)
		{
			Graphics.Internal_DrawMeshNow1(mesh, position, rotation, materialIndex);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000C2CC File Offset: 0x0000A4CC
		[Obsolete("Use Graphics.DrawMeshNow instead.")]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix)
		{
			Graphics.Internal_DrawMeshNow2(mesh, matrix, -1);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		[Obsolete("Use Graphics.DrawMeshNow instead.")]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, int materialIndex)
		{
			Graphics.Internal_DrawMeshNow2(mesh, matrix, materialIndex);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture)
		{
			Material material = null;
			Graphics.DrawTexture(screenRect, texture, material);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000C2FC File Offset: 0x0000A4FC
		public static void DrawTexture(Rect screenRect, Texture texture, [DefaultValue("null")] Material mat)
		{
			Graphics.DrawTexture(screenRect, texture, 0, 0, 0, 0, mat);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000C30C File Offset: 0x0000A50C
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
		{
			Material material = null;
			Graphics.DrawTexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, material);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000C32C File Offset: 0x0000A52C
		public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat)
		{
			Graphics.DrawTexture(screenRect, texture, new Rect(0f, 0f, 1f, 1f), leftBorder, rightBorder, topBorder, bottomBorder, mat);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000C364 File Offset: 0x0000A564
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
		{
			Material material = null;
			Graphics.DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, material);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000C384 File Offset: 0x0000A584
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat)
		{
			InternalDrawTextureArguments internalDrawTextureArguments = default(InternalDrawTextureArguments);
			internalDrawTextureArguments.screenRect = screenRect;
			internalDrawTextureArguments.texture = texture;
			internalDrawTextureArguments.sourceRect = sourceRect;
			internalDrawTextureArguments.leftBorder = leftBorder;
			internalDrawTextureArguments.rightBorder = rightBorder;
			internalDrawTextureArguments.topBorder = topBorder;
			internalDrawTextureArguments.bottomBorder = bottomBorder;
			Color32 color = default(Color32);
			color.r = (color.g = (color.b = (color.a = 128)));
			internalDrawTextureArguments.color = color;
			internalDrawTextureArguments.mat = mat;
			Graphics.DrawTexture(ref internalDrawTextureArguments);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000C420 File Offset: 0x0000A620
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color)
		{
			Material material = null;
			Graphics.DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, material);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000C444 File Offset: 0x0000A644
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color, [DefaultValue("null")] Material mat)
		{
			InternalDrawTextureArguments internalDrawTextureArguments = default(InternalDrawTextureArguments);
			internalDrawTextureArguments.screenRect = screenRect;
			internalDrawTextureArguments.texture = texture;
			internalDrawTextureArguments.sourceRect = sourceRect;
			internalDrawTextureArguments.leftBorder = leftBorder;
			internalDrawTextureArguments.rightBorder = rightBorder;
			internalDrawTextureArguments.topBorder = topBorder;
			internalDrawTextureArguments.bottomBorder = bottomBorder;
			internalDrawTextureArguments.color = color;
			internalDrawTextureArguments.mat = mat;
			Graphics.DrawTexture(ref internalDrawTextureArguments);
		}

		// Token: 0x06000535 RID: 1333
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DrawTexture(ref InternalDrawTextureArguments arguments);

		// Token: 0x06000536 RID: 1334
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Blit(Texture source, RenderTexture dest);

		// Token: 0x06000537 RID: 1335 RVA: 0x0000C4B4 File Offset: 0x0000A6B4
		[ExcludeFromDocs]
		public static void Blit(Texture source, RenderTexture dest, Material mat)
		{
			int num = -1;
			Graphics.Blit(source, dest, mat, num);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000C4CC File Offset: 0x0000A6CC
		public static void Blit(Texture source, RenderTexture dest, Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.Internal_BlitMaterial(source, dest, mat, pass, true);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000C4D8 File Offset: 0x0000A6D8
		[ExcludeFromDocs]
		public static void Blit(Texture source, Material mat)
		{
			int num = -1;
			Graphics.Blit(source, mat, num);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		public static void Blit(Texture source, Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.Internal_BlitMaterial(source, null, mat, pass, false);
		}

		// Token: 0x0600053B RID: 1339
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BlitMaterial(Texture source, RenderTexture dest, Material mat, int pass, bool setRT);

		// Token: 0x0600053C RID: 1340 RVA: 0x0000C4FC File Offset: 0x0000A6FC
		public static void BlitMultiTap(Texture source, RenderTexture dest, Material mat, params Vector2[] offsets)
		{
			Graphics.Internal_BlitMultiTap(source, dest, mat, offsets);
		}

		// Token: 0x0600053D RID: 1341
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BlitMultiTap(Texture source, RenderTexture dest, Material mat, Vector2[] offsets);

		// Token: 0x0600053E RID: 1342 RVA: 0x0000C508 File Offset: 0x0000A708
		public static void SetRenderTarget(RenderTexture rt)
		{
			Graphics.Internal_SetRT(rt, 0, -1);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000C514 File Offset: 0x0000A714
		public static void SetRenderTarget(RenderTexture rt, int mipLevel)
		{
			Graphics.Internal_SetRT(rt, mipLevel, -1);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000C520 File Offset: 0x0000A720
		public static void SetRenderTarget(RenderTexture rt, int mipLevel, CubemapFace face)
		{
			Graphics.Internal_SetRT(rt, mipLevel, (int)face);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0000C52C File Offset: 0x0000A72C
		public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer)
		{
			Graphics.Internal_SetRTBuffer(out colorBuffer, out depthBuffer);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0000C538 File Offset: 0x0000A738
		public static void SetRenderTarget(RenderBuffer[] colorBuffers, RenderBuffer depthBuffer)
		{
			Graphics.Internal_SetRTBuffers(colorBuffers, out depthBuffer);
		}

		// Token: 0x06000543 RID: 1347
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRT(RenderTexture rt, int mipLevel, int face);

		// Token: 0x06000544 RID: 1348
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRTBuffer(out RenderBuffer colorBuffer, out RenderBuffer depthBuffer);

		// Token: 0x06000545 RID: 1349
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRTBuffers(RenderBuffer[] colorBuffers, out RenderBuffer depthBuffer);

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0000C544 File Offset: 0x0000A744
		public static RenderBuffer activeColorBuffer
		{
			get
			{
				RenderBuffer renderBuffer;
				Graphics.GetActiveColorBuffer(out renderBuffer);
				return renderBuffer;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0000C55C File Offset: 0x0000A75C
		public static RenderBuffer activeDepthBuffer
		{
			get
			{
				RenderBuffer renderBuffer;
				Graphics.GetActiveDepthBuffer(out renderBuffer);
				return renderBuffer;
			}
		}

		// Token: 0x06000548 RID: 1352
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveColorBuffer(out RenderBuffer res);

		// Token: 0x06000549 RID: 1353
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveDepthBuffer(out RenderBuffer res);

		// Token: 0x0600054A RID: 1354 RVA: 0x0000C574 File Offset: 0x0000A774
		public static void SetRandomWriteTarget(int index, RenderTexture uav)
		{
			Graphics.Internal_SetRandomWriteTargetRT(index, uav);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000C580 File Offset: 0x0000A780
		public static void SetRandomWriteTarget(int index, ComputeBuffer uav)
		{
			Graphics.Internal_SetRandomWriteTargetBuffer(index, uav);
		}

		// Token: 0x0600054C RID: 1356
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearRandomWriteTargets();

		// Token: 0x0600054D RID: 1357
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRandomWriteTargetRT(int index, RenderTexture uav);

		// Token: 0x0600054E RID: 1358
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRandomWriteTargetBuffer(int index, ComputeBuffer uav);

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0000C58C File Offset: 0x0000A78C
		[Obsolete("Use SystemInfo.graphicsDeviceName instead.")]
		public static string deviceName
		{
			get
			{
				return SystemInfo.graphicsDeviceName;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0000C594 File Offset: 0x0000A794
		[Obsolete("Use SystemInfo.graphicsDeviceVendor instead.")]
		public static string deviceVendor
		{
			get
			{
				return SystemInfo.graphicsDeviceVendor;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0000C59C File Offset: 0x0000A79C
		[Obsolete("Use SystemInfo.graphicsDeviceVersion instead.")]
		public static string deviceVersion
		{
			get
			{
				return SystemInfo.graphicsDeviceVersion;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0000C5A4 File Offset: 0x0000A7A4
		[Obsolete("Use SystemInfo.supportsVertexPrograms instead.")]
		public static bool supportsVertexProgram
		{
			get
			{
				return SystemInfo.supportsVertexPrograms;
			}
		}

		// Token: 0x06000553 RID: 1363
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetupVertexLights(Light[] lights);
	}
}
