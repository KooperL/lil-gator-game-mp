using System;
using UnityEngine;

// Token: 0x020000F0 RID: 240
public class FogLight : MonoBehaviour, IManagedUpdate
{
	// Token: 0x060004F6 RID: 1270 RVA: 0x0001AA05 File Offset: 0x00018C05
	private void Awake()
	{
		this.seed = Random.value;
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x0001AA12 File Offset: 0x00018C12
	private void OnEnable()
	{
		FastUpdateManager.updateEvery1.Add(this);
		this.percentStrengthSmooth = 0f;
		PostProcessFog.fogLights.Add(this);
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x0001AA35 File Offset: 0x00018C35
	private void OnDisable()
	{
		if (FastUpdateManager.updateEvery1.Contains(this))
		{
			FastUpdateManager.updateEvery1.Remove(this);
		}
		PostProcessFog.fogLights.Remove(this);
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x0001AA5C File Offset: 0x00018C5C
	public void OnDrawGizmos()
	{
		Gizmos.color = new Color(1f, 1f, 1f, 0.25f);
		Gizmos.DrawSphere(base.transform.position, this.radius);
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x0001AA94 File Offset: 0x00018C94
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

	// Token: 0x060004FB RID: 1275 RVA: 0x0001AB00 File Offset: 0x00018D00
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

	// Token: 0x060004FC RID: 1276 RVA: 0x0001AB64 File Offset: 0x00018D64
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
		Bounds bounds = new Bounds(base.transform.position, 2f * this.radius * Vector3.one);
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

	// Token: 0x040006CD RID: 1741
	public float radius = 15f;

	// Token: 0x040006CE RID: 1742
	public float falloff = 5f;

	// Token: 0x040006CF RID: 1743
	[Range(0f, 1f)]
	public float intensity = 1f;

	// Token: 0x040006D0 RID: 1744
	[Space]
	private bool isSmoothing;

	// Token: 0x040006D1 RID: 1745
	[Range(0f, 1f)]
	public float percentStrength;

	// Token: 0x040006D2 RID: 1746
	[Range(0f, 1f)]
	public float percentStrengthSmooth;

	// Token: 0x040006D3 RID: 1747
	private float percentStrengthVel;

	// Token: 0x040006D4 RID: 1748
	public bool blinkOnChange;

	// Token: 0x040006D5 RID: 1749
	public bool customLightCurves;

	// Token: 0x040006D6 RID: 1750
	public AnimationCurve radiusCurve;

	// Token: 0x040006D7 RID: 1751
	public AnimationCurve intensityCurve;

	// Token: 0x040006D8 RID: 1752
	[Header("Variance")]
	private float seed;

	// Token: 0x040006D9 RID: 1753
	public bool positionVariance = true;

	// Token: 0x040006DA RID: 1754
	public float positionMagnitude = 5f;

	// Token: 0x040006DB RID: 1755
	public float positionFrequency = 0.2f;

	// Token: 0x040006DC RID: 1756
	public bool strengthVariance = true;

	// Token: 0x040006DD RID: 1757
	public float strengthMagnitude = 0.2f;

	// Token: 0x040006DE RID: 1758
	public float strengthFrequency = 0.2f;
}
