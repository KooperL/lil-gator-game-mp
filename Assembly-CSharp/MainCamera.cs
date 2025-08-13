using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.U2D;

// Token: 0x020001E7 RID: 487
public class MainCamera : MonoBehaviour
{
	// Token: 0x060008FD RID: 2301 RVA: 0x00008CA6 File Offset: 0x00006EA6
	private void Awake()
	{
		MainCamera.c = base.GetComponent<Camera>();
		MainCamera.t = base.transform;
		MainCamera.p = base.GetComponent<PixelPerfectCamera>();
		MainCamera.postLayer = base.GetComponent<PostProcessLayer>();
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x00008CD4 File Offset: 0x00006ED4
	private void OnEnable()
	{
		this.Awake();
	}

	// Token: 0x04000B97 RID: 2967
	public static Camera c;

	// Token: 0x04000B98 RID: 2968
	public static Transform t;

	// Token: 0x04000B99 RID: 2969
	public static PixelPerfectCamera p;

	// Token: 0x04000B9A RID: 2970
	public static PostProcessLayer postLayer;

	// Token: 0x04000B9B RID: 2971
	private static readonly int _ClipToWorld = Shader.PropertyToID("_ClipToWorld");
}
