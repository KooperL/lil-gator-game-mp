using System;
using Cinemachine;
using UnityEngine;

public class MixingCameraSmooth : MonoBehaviour
{
	// Token: 0x06000972 RID: 2418 RVA: 0x000092B2 File Offset: 0x000074B2
	private void OnValidate()
	{
		if (this.mixingCamera == null)
		{
			this.mixingCamera = base.GetComponent<CinemachineMixingCamera>();
		}
		this.UpdateWeights();
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x000092D4 File Offset: 0x000074D4
	private void OnEnable()
	{
		this.t = 0f;
		this.UpdateWeights();
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x000092E7 File Offset: 0x000074E7
	private void Start()
	{
		this.childCameraCount = this.mixingCamera.ChildCameras.Length;
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x0003AB50 File Offset: 0x00038D50
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

	// Token: 0x06000976 RID: 2422 RVA: 0x0003ABCC File Offset: 0x00038DCC
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

	// Token: 0x06000977 RID: 2423 RVA: 0x0003AC48 File Offset: 0x00038E48
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

	public CinemachineMixingCamera mixingCamera;

	public float smoothTime = 5f;

	[Range(0f, 1f)]
	public float endSmoothing;

	private const float initialT = 0f;

	public float t;

	private int childCameraCount;

	private const int endOffset = -1;
}
