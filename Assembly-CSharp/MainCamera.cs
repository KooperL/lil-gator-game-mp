using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.U2D;

public class MainCamera : MonoBehaviour
{
	// Token: 0x0600093E RID: 2366 RVA: 0x00008FC2 File Offset: 0x000071C2
	private void Awake()
	{
		MainCamera.c = base.GetComponent<Camera>();
		MainCamera.t = base.transform;
		MainCamera.p = base.GetComponent<PixelPerfectCamera>();
		MainCamera.postLayer = base.GetComponent<PostProcessLayer>();
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x00008FF0 File Offset: 0x000071F0
	private void OnEnable()
	{
		this.Awake();
	}

	public static Camera c;

	public static Transform t;

	public static PixelPerfectCamera p;

	public static PostProcessLayer postLayer;

	private static readonly int _ClipToWorld = Shader.PropertyToID("_ClipToWorld");
}
