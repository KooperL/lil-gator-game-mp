using System;
using UnityEngine;

public class FogLight : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000642 RID: 1602 RVA: 0x00006772 File Offset: 0x00004972
	private void Awake()
	{
		this.seed = global::UnityEngine.Random.value;
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0000677F File Offset: 0x0000497F
	private void OnEnable()
	{
		FastUpdateManager.updateEvery1.Add(this);
		this.percentStrengthSmooth = 0f;
		PostProcessFog.fogLights.Add(this);
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x000067A2 File Offset: 0x000049A2
	private void OnDisable()
	{
		if (FastUpdateManager.updateEvery1.Contains(this))
		{
			FastUpdateManager.updateEvery1.Remove(this);
		}
		PostProcessFog.fogLights.Remove(this);
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x000067C9 File Offset: 0x000049C9
	public void OnDrawGizmos()
	{
		Gizmos.color = new Color(1f, 1f, 1f, 0.25f);
		Gizmos.DrawSphere(base.transform.position, this.radius);
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x00030C00 File Offset: 0x0002EE00
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

	// Token: 0x06000647 RID: 1607 RVA: 0x00030C6C File Offset: 0x0002EE6C
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

	// Token: 0x06000648 RID: 1608 RVA: 0x00030CD0 File Offset: 0x0002EED0
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

	public float radius = 15f;

	public float falloff = 5f;

	[Range(0f, 1f)]
	public float intensity = 1f;

	[Space]
	private bool isSmoothing;

	[Range(0f, 1f)]
	public float percentStrength;

	[Range(0f, 1f)]
	public float percentStrengthSmooth;

	private float percentStrengthVel;

	public bool blinkOnChange;

	public bool customLightCurves;

	public AnimationCurve radiusCurve;

	public AnimationCurve intensityCurve;

	[Header("Variance")]
	private float seed;

	public bool positionVariance = true;

	public float positionMagnitude = 5f;

	public float positionFrequency = 0.2f;

	public bool strengthVariance = true;

	public float strengthMagnitude = 0.2f;

	public float strengthFrequency = 0.2f;
}
