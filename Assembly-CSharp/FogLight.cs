using System;
using UnityEngine;

// Token: 0x02000142 RID: 322
public class FogLight : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000608 RID: 1544 RVA: 0x000064AC File Offset: 0x000046AC
	private void Awake()
	{
		this.seed = Random.value;
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x000064B9 File Offset: 0x000046B9
	private void OnEnable()
	{
		FastUpdateManager.updateEvery1.Add(this);
		this.percentStrengthSmooth = 0f;
		PostProcessFog.fogLights.Add(this);
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x000064DC File Offset: 0x000046DC
	private void OnDisable()
	{
		if (FastUpdateManager.updateEvery1.Contains(this))
		{
			FastUpdateManager.updateEvery1.Remove(this);
		}
		PostProcessFog.fogLights.Remove(this);
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x00006503 File Offset: 0x00004703
	public void OnDrawGizmos()
	{
		Gizmos.color = new Color(1f, 1f, 1f, 0.25f);
		Gizmos.DrawSphere(base.transform.position, this.radius);
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x0002F504 File Offset: 0x0002D704
	public void ManagedUpdate()
	{
		this.percentStrengthSmooth = Mathf.SmoothDamp(this.percentStrengthSmooth, this.percentStrength, ref this.percentStrengthVel, 1f);
		if (Mathf.Approximately(this.percentStrength, this.percentStrengthSmooth))
		{
			this.percentStrengthSmooth = this.percentStrength;
			FastUpdateManager.updateEvery1.Remove(this);
			this.isSmoothing = false;
			this.percentStrengthVel = 0f;
		}
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x0002F570 File Offset: 0x0002D770
	public void SetStrength(float strength, bool max = true)
	{
		if (strength <= this.percentStrength && max)
		{
			return;
		}
		this.percentStrength = strength;
		if (this.blinkOnChange)
		{
			this.percentStrengthSmooth = Mathf.Lerp(this.percentStrength, 1f, 0.5f);
		}
		if (!this.isSmoothing)
		{
			this.isSmoothing = true;
			FastUpdateManager.updateEvery1.Add(this);
		}
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x0002F5D4 File Offset: 0x0002D7D4
	public bool GetLightData(Plane[] cameraFrustum, out Vector3 lightPosition, out float lightRadius, out float lightFalloff, out float lightIntensity)
	{
		float num = ((55f - (base.transform.position - MainCamera.t.position).magnitude > -this.radius) ? 1f : 0f);
		lightPosition = base.transform.position;
		lightRadius = 0f;
		lightFalloff = 0f;
		lightIntensity = 0f;
		if (num <= 0f)
		{
			return false;
		}
		Bounds bounds;
		bounds..ctor(base.transform.position, 2f * this.radius * Vector3.one);
		if (!GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
		{
			return false;
		}
		if (this.customLightCurves)
		{
			lightRadius = this.radius * num * this.radiusCurve.Evaluate(this.percentStrengthSmooth);
			lightIntensity = this.intensity * num * this.intensityCurve.Evaluate(this.percentStrengthSmooth);
			lightFalloff = this.falloff;
		}
		else
		{
			num *= this.percentStrengthSmooth;
			lightRadius = this.radius * num;
			lightFalloff = this.falloff;
			lightIntensity = this.intensity * num;
		}
		if (this.positionVariance)
		{
			lightPosition += this.positionMagnitude * PerlinUtil.Perlin3(this.seed, Time.time * this.positionFrequency);
		}
		if (this.strengthVariance)
		{
			float num2 = 1f + this.strengthMagnitude * PerlinUtil.Perlin1(this.seed, Time.time * this.strengthFrequency);
			lightRadius *= num2;
			lightFalloff *= num2;
			lightIntensity *= num2;
		}
		return true;
	}

	// Token: 0x04000812 RID: 2066
	public float radius = 15f;

	// Token: 0x04000813 RID: 2067
	public float falloff = 5f;

	// Token: 0x04000814 RID: 2068
	[Range(0f, 1f)]
	public float intensity = 1f;

	// Token: 0x04000815 RID: 2069
	[Space]
	private bool isSmoothing;

	// Token: 0x04000816 RID: 2070
	[Range(0f, 1f)]
	public float percentStrength;

	// Token: 0x04000817 RID: 2071
	[Range(0f, 1f)]
	public float percentStrengthSmooth;

	// Token: 0x04000818 RID: 2072
	private float percentStrengthVel;

	// Token: 0x04000819 RID: 2073
	public bool blinkOnChange;

	// Token: 0x0400081A RID: 2074
	public bool customLightCurves;

	// Token: 0x0400081B RID: 2075
	public AnimationCurve radiusCurve;

	// Token: 0x0400081C RID: 2076
	public AnimationCurve intensityCurve;

	// Token: 0x0400081D RID: 2077
	[Header("Variance")]
	private float seed;

	// Token: 0x0400081E RID: 2078
	public bool positionVariance = true;

	// Token: 0x0400081F RID: 2079
	public float positionMagnitude = 5f;

	// Token: 0x04000820 RID: 2080
	public float positionFrequency = 0.2f;

	// Token: 0x04000821 RID: 2081
	public bool strengthVariance = true;

	// Token: 0x04000822 RID: 2082
	public float strengthMagnitude = 0.2f;

	// Token: 0x04000823 RID: 2083
	public float strengthFrequency = 0.2f;
}
