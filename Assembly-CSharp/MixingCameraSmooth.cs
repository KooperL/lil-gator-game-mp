using System;
using Cinemachine;
using UnityEngine;

// Token: 0x020001F1 RID: 497
public class MixingCameraSmooth : MonoBehaviour
{
	// Token: 0x06000931 RID: 2353 RVA: 0x00008F81 File Offset: 0x00007181
	private void OnValidate()
	{
		if (this.mixingCamera == null)
		{
			this.mixingCamera = base.GetComponent<CinemachineMixingCamera>();
		}
		this.UpdateWeights();
	}

	// Token: 0x06000932 RID: 2354 RVA: 0x00008FA3 File Offset: 0x000071A3
	private void OnEnable()
	{
		this.t = 0f;
		this.UpdateWeights();
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x00008FB6 File Offset: 0x000071B6
	private void Start()
	{
		this.childCameraCount = this.mixingCamera.ChildCameras.Length;
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x000391E0 File Offset: 0x000373E0
	private void LateUpdate()
	{
		float num = Time.deltaTime * (float)this.childCameraCount / this.smoothTime;
		if (this.endSmoothing > 0f)
		{
			num *= 1f - this.endSmoothing * Mathf.InverseLerp(-1f, (float)(this.childCameraCount + -1), this.t);
		}
		this.t = Mathf.MoveTowards(this.t, (float)(this.childCameraCount + -1), num);
		this.UpdateWeights();
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x0003925C File Offset: 0x0003745C
	private void UpdateWeights()
	{
		float num = this.t;
		for (int i = 0; i < this.childCameraCount; i++)
		{
			float num2 = Mathf.Sign(num - (float)i);
			float num3 = Mathf.InverseLerp(Mathf.Clamp((float)i + 2f * num2, 0f, (float)(this.childCameraCount - 1)), (float)i, num);
			int num4 = this.childCameraCount;
			num3 = Mathf.SmoothStep(0f, 1f, num3);
			this.mixingCamera.SetWeight(i, num3);
		}
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x000392D8 File Offset: 0x000374D8
	private void UpdateWeightsOld()
	{
		float num = this.t;
		for (int i = 0; i < this.childCameraCount; i++)
		{
			float num2 = Mathf.Sign(num - (float)i);
			float num3 = Mathf.InverseLerp((float)i + 2f * num2, (float)i, num);
			if (this.childCameraCount > 2)
			{
				num3 = Mathf.Pow(num3, 2f);
			}
			num3 = Mathf.SmoothStep(0f, 1f, num3);
			this.mixingCamera.SetWeight(i, num3);
		}
	}

	// Token: 0x04000BDD RID: 3037
	public CinemachineMixingCamera mixingCamera;

	// Token: 0x04000BDE RID: 3038
	public float smoothTime = 5f;

	// Token: 0x04000BDF RID: 3039
	[Range(0f, 1f)]
	public float endSmoothing;

	// Token: 0x04000BE0 RID: 3040
	private const float initialT = 0f;

	// Token: 0x04000BE1 RID: 3041
	public float t;

	// Token: 0x04000BE2 RID: 3042
	private int childCameraCount;

	// Token: 0x04000BE3 RID: 3043
	private const int endOffset = -1;
}
