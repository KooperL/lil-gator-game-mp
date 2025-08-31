using System;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
	// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0001A2A6 File Offset: 0x000184A6
	public static EffectsManager e
	{
		get
		{
			if (EffectsManager.instance == null)
			{
				EffectsManager.instance = Object.FindObjectOfType<EffectsManager>();
			}
			return EffectsManager.instance;
		}
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x0001A2C4 File Offset: 0x000184C4
	private void Awake()
	{
		EffectsManager.instance = this;
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x0001A2CC File Offset: 0x000184CC
	private void OnEnable()
	{
		EffectsManager.instance = this;
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x0001A2D4 File Offset: 0x000184D4
	private float ModifyVolume(float volume, float lastSoundTime)
	{
		return volume * Mathf.Lerp(0.3f, 1f, Mathf.InverseLerp(0.1f, 0.25f, Time.time - lastSoundTime));
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x0001A300 File Offset: 0x00018500
	public void Splash(Vector3 position, float volume = 0.8f)
	{
		if (Vector3.SqrMagnitude(MainCamera.t.position - position) < this.maxDistance * this.maxDistance && this.splashPrefab != null)
		{
			volume = this.ModifyVolume(volume, this.lastSplashTime);
			this.lastSplashTime = Time.time;
			if (volume == 0.3f)
			{
				return;
			}
			GameObject gameObject = Object.Instantiate<GameObject>(this.splashPrefab, position, Quaternion.identity);
			if (volume != 1f)
			{
				AudioSourceVariance component = gameObject.GetComponent<AudioSourceVariance>();
				component.maxVolume *= volume;
				component.minVolume *= volume;
			}
		}
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x0001A39D File Offset: 0x0001859D
	public void Ripple(Vector3 position, int count)
	{
		this.Ripple(position, count, Vector3.zero);
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x0001A3AC File Offset: 0x000185AC
	public void Ripple(Vector3 position, int count, Vector3 direction)
	{
		if (Vector3.SqrMagnitude(MainCamera.t.position - position) >= this.maxDistance * this.maxDistance || this.rippleSystem == null)
		{
			return;
		}
		position.y += 0.02f;
		ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
		emitParams.position = position;
		if (direction != Vector3.zero)
		{
			emitParams.rotation = Quaternion.FromToRotation(Vector3.forward, direction).eulerAngles.y;
		}
		this.rippleSystem.Emit(emitParams, count);
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x0001A445 File Offset: 0x00018645
	public void Dust(Vector3 position, int count)
	{
		this.Dust(position, count, Vector3.zero, 0f);
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x0001A45C File Offset: 0x0001865C
	public void Dust(Vector3 position, int count, Vector3 velocity, float startSize = 0f)
	{
		if (MainCamera.t == null)
		{
			return;
		}
		if (Vector3.SqrMagnitude(MainCamera.t.position - position) >= this.maxDistance * this.maxDistance || this.dustSystem == null)
		{
			return;
		}
		ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
		emitParams.position = position;
		if (velocity != Vector3.zero)
		{
			emitParams.velocity = velocity;
		}
		else
		{
			emitParams.applyShapeToPosition = true;
		}
		if (startSize != 0f)
		{
			emitParams.startSize = startSize;
		}
		this.dustSystem.Emit(emitParams, count);
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x0001A4F8 File Offset: 0x000186F8
	public void FloorDust(Vector3 position, int count, Vector3 normal)
	{
		if (Vector3.SqrMagnitude(MainCamera.t.position - position) >= this.maxDistance * this.maxDistance || this.dustSystem == null)
		{
			return;
		}
		ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
		emitParams.position = position;
		emitParams.applyShapeToPosition = false;
		for (int i = 0; i < count; i++)
		{
			emitParams.velocity = Random.Range(8f, 10f) * Vector3.ProjectOnPlane(Random.onUnitSphere, normal).normalized + 2f * Random.value * normal;
			this.dustSystem.Emit(emitParams, 1);
		}
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x0001A5B0 File Offset: 0x000187B0
	public void Bonk(Vector3 position)
	{
		if (Vector3.SqrMagnitude(MainCamera.t.position - position) < this.maxDistance * this.maxDistance && this.bonkPrefab != null)
		{
			Object.Instantiate<GameObject>(this.bonkPrefab, position, Quaternion.identity);
		}
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x0001A604 File Offset: 0x00018804
	public void Bonk(Vector3 position, BoxCollider box)
	{
		if (Vector3.SqrMagnitude(MainCamera.t.position - position) < this.maxDistance * this.maxDistance && this.bonkPrefab != null)
		{
			Object.Instantiate<GameObject>(this.bonkPrefab, position, Quaternion.identity).GetComponent<FitBounds>().Fit(box);
		}
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x0001A65F File Offset: 0x0001885F
	private void EmitForSystem(Vector3 position, int count, Vector3 direction, Vector3 velocity, ParticleSystem system)
	{
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x0001A664 File Offset: 0x00018864
	public void Dig(Vector3 position)
	{
		if (Vector3.SqrMagnitude(MainCamera.t.position - position) < this.maxDistance * this.maxDistance && this.digPrefab != null)
		{
			Object.Instantiate<GameObject>(this.digPrefab, position, Quaternion.identity);
		}
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x0001A6B8 File Offset: 0x000188B8
	public void TryToPlayHitSound(Vector3 position, Vector3 direction, float volume = 1f)
	{
		SurfaceMaterial surfaceMaterial = MaterialManager.m.SampleSurfaceMaterial(position, direction);
		if (surfaceMaterial == null)
		{
			return;
		}
		surfaceMaterial.PlayImpact(position, volume, 1f);
	}

	private static EffectsManager instance;

	public GameObject splashPrefab;

	public GameObject bonkPrefab;

	public ParticleSystem rippleSystem;

	public ParticleSystem dustSystem;

	public GameObject sweatPrefab;

	public float maxDistance = 80f;

	public GameObject digPrefab;

	private const float minVolume = 0.3f;

	private const float minDelay = 0.1f;

	private const float maxDelay = 0.25f;

	private float lastSplashTime = -10f;
}
