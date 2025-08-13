using System;
using Cinemachine;
using UnityEngine;

// Token: 0x0200017B RID: 379
public class MixingCameraSmooth : MonoBehaviour
{
	// Token: 0x060007CF RID: 1999 RVA: 0x000260C5 File Offset: 0x000242C5
	private void OnValidate()
	{
		if (this.mixingCamera == null)
		{
			this.mixingCamera = base.GetComponent<CinemachineMixingCamera>();
		}
		this.UpdateWeights();
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x000260E7 File Offset: 0x000242E7
	private void OnEnable()
	{
		this.t = 0f;
		this.UpdateWeights();
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x000260FA File Offset: 0x000242FA
	private void Start()
	{
		this.childCameraCount = this.mixingCamera.ChildCameras.Length;
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x00026110 File Offset: 0x00024310
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

	// Token: 0x060007D3 RID: 2003 RVA: 0x0002618C File Offset: 0x0002438C
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

	// Token: 0x060007D4 RID: 2004 RVA: 0x00026208 File Offset: 0x00024408
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

	// Token: 0x04000A01 RID: 2561
	public CinemachineMixingCamera mixingCamera;

	// Token: 0x04000A02 RID: 2562
	public float smoothTime = 5f;

	// Token: 0x04000A03 RID: 2563
	[Range(0f, 1f)]
	public float endSmoothing;

	// Token: 0x04000A04 RID: 2564
	private const float initialT = 0f;

	// Token: 0x04000A05 RID: 2565
	public float t;

	// Token: 0x04000A06 RID: 2566
	private int childCameraCount;

	// Token: 0x04000A07 RID: 2567
	private const int endOffset = -1;
}
