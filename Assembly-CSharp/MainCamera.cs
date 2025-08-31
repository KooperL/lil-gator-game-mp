using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.U2D;

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

	public static Camera c;

	public static Transform t;

	public static PixelPerfectCamera p;

	public static PostProcessLayer postLayer;

	private static readonly int _ClipToWorld = Shader.PropertyToID("_ClipToWorld");
}
