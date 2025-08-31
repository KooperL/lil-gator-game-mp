using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraLayerCullDistances : MonoBehaviour
{
	// Token: 0x0600024D RID: 589 RVA: 0x0000C534 File Offset: 0x0000A734
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

	// Token: 0x0600024E RID: 590 RVA: 0x0000C5DF File Offset: 0x0000A7DF
	private void Start()
	{
		this.camera.layerCullDistances = this.layerCullDistances;
		this.camera.layerCullSpherical = true;
		if (this.shadowCaster != null)
		{
			this.shadowCaster.layerShadowCullDistances = this.layerShadowCullDistances;
		}
	}

	// Token: 0x0600024F RID: 591 RVA: 0x0000C620 File Offset: 0x0000A820
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

	public Camera camera;

	public Light shadowCaster;

	public float fogDistance = 55f;

	public float[] layerFogMultipliers;

	[ReadOnly]
	public float[] layerCullDistances;

	public float[] layerShadowMultipliers;

	[ReadOnly]
	public float[] layerShadowCullDistances;

	public bool layerCullSpherical = true;

	private const int layerCount = 32;
}
