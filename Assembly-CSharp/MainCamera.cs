using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.U2D;

// Token: 0x02000173 RID: 371
public class MainCamera : MonoBehaviour
{
	// Token: 0x0600079F RID: 1951 RVA: 0x0002566E File Offset: 0x0002386E
	private void Awake()
	{
		MainCamera.c = base.GetComponent<Camera>();
		MainCamera.t = base.transform;
		MainCamera.p = base.GetComponent<PixelPerfectCamera>();
		MainCamera.postLayer = base.GetComponent<PostProcessLayer>();
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x0002569C File Offset: 0x0002389C
	private void OnEnable()
	{
		this.Awake();
	}

	// Token: 0x040009C9 RID: 2505
	public static Camera c;

	// Token: 0x040009CA RID: 2506
	public static Transform t;

	// Token: 0x040009CB RID: 2507
	public static PixelPerfectCamera p;

	// Token: 0x040009CC RID: 2508
	public static PostProcessLayer postLayer;

	// Token: 0x040009CD RID: 2509
	private static readonly int _ClipToWorld = Shader.PropertyToID("_ClipToWorld");
}
