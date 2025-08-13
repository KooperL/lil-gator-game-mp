using System;
using UnityEngine;

// Token: 0x020000B2 RID: 178
[RequireComponent(typeof(Camera))]
public class CameraLayerCullDistances : MonoBehaviour
{
	// Token: 0x06000297 RID: 663 RVA: 0x000203A8 File Offset: 0x0001E5A8
	private void OnValidate()
	{
		if (this.shadowCaster == null)
		{
			this.shadowCaster = Object.FindObjectOfType<Light>();
		}
		if (this.camera == null)
		{
			this.camera = base.GetComponent<Camera>();
		}
		if (this.layerFogMultipliers == null)
		{
			this.layerFogMultipliers = new float[32];
		}
		if (this.layerCullDistances == null)
		{
			this.layerCullDistances = new float[32];
		}
		if (this.layerShadowMultipliers == null)
		{
			this.layerShadowMultipliers = new float[32];
		}
		if (this.layerShadowCullDistances == null)
		{
			this.layerShadowCullDistances = new float[32];
		}
		this.SetFogDistance(this.fogDistance, this.fogDistance, 0f);
	}

	// Token: 0x06000298 RID: 664 RVA: 0x000041B4 File Offset: 0x000023B4
	private void Start()
	{
		this.camera.layerCullDistances = this.layerCullDistances;
		this.camera.layerCullSpherical = true;
		if (this.shadowCaster != null)
		{
			this.shadowCaster.layerShadowCullDistances = this.layerShadowCullDistances;
		}
	}

	// Token: 0x06000299 RID: 665 RVA: 0x00020454 File Offset: 0x0001E654
	public void SetFogDistance(float fogDistance, float shadowDistance, float offset = 0f)
	{
		this.fogDistance = fogDistance;
		for (int i = 0; i < 32; i++)
		{
			if (this.layerFogMultipliers[i] == 0f)
			{
				this.layerCullDistances[i] = 0f;
			}
			else
			{
				this.layerCullDistances[i] = fogDistance * this.layerFogMultipliers[i] + offset;
			}
		}
		for (int j = 0; j < 32; j++)
		{
			if (this.layerShadowMultipliers[j] == 0f)
			{
				this.layerShadowCullDistances[j] = 0f;
			}
			else
			{
				this.layerShadowCullDistances[j] = shadowDistance * this.layerShadowMultipliers[j] + offset;
			}
		}
		this.camera.layerCullDistances = this.layerCullDistances;
		if (this.shadowCaster != null)
		{
			this.shadowCaster.layerShadowCullDistances = this.layerShadowCullDistances;
		}
	}

	// Token: 0x0400039B RID: 923
	public Camera camera;

	// Token: 0x0400039C RID: 924
	public Light shadowCaster;

	// Token: 0x0400039D RID: 925
	public float fogDistance = 55f;

	// Token: 0x0400039E RID: 926
	public float[] layerFogMultipliers;

	// Token: 0x0400039F RID: 927
	[ReadOnly]
	public float[] layerCullDistances;

	// Token: 0x040003A0 RID: 928
	public float[] layerShadowMultipliers;

	// Token: 0x040003A1 RID: 929
	[ReadOnly]
	public float[] layerShadowCullDistances;

	// Token: 0x040003A2 RID: 930
	public bool layerCullSpherical = true;

	// Token: 0x040003A3 RID: 931
	private const int layerCount = 32;
}
