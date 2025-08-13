using System;
using UnityEngine;

// Token: 0x0200013B RID: 315
public class EffectsManager : MonoBehaviour
{
	// Token: 0x1700009A RID: 154
	// (get) Token: 0x060005DC RID: 1500 RVA: 0x000062D9 File Offset: 0x000044D9
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

	// Token: 0x060005DD RID: 1501 RVA: 0x000062F7 File Offset: 0x000044F7
	private void Awake()
	{
		EffectsManager.instance = this;
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x000062F7 File Offset: 0x000044F7
	private void OnEnable()
	{
		EffectsManager.instance = this;
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x000062FF File Offset: 0x000044FF
	private float ModifyVolume(float volume, float lastSoundTime)
	{
		return volume * Mathf.Lerp(0.3f, 1f, Mathf.InverseLerp(0.1f, 0.25f, Time.time - lastSoundTime));
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x0002EF18 File Offset: 0x0002D118
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

	// Token: 0x060005E1 RID: 1505 RVA: 0x00006328 File Offset: 0x00004528
	public void Ripple(Vector3 position, int count)
	{
		this.Ripple(position, count, Vector3.zero);
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x0002EFB8 File Offset: 0x0002D1B8
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

	// Token: 0x060005E3 RID: 1507 RVA: 0x00006337 File Offset: 0x00004537
	public void Dust(Vector3 position, int count)
	{
		this.Dust(position, count, Vector3.zero, 0f);
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x0002F054 File Offset: 0x0002D254
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

	// Token: 0x060005E5 RID: 1509 RVA: 0x0002F0F0 File Offset: 0x0002D2F0
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

	// Token: 0x060005E6 RID: 1510 RVA: 0x0002F1A8 File Offset: 0x0002D3A8
	public void Bonk(Vector3 position)
	{
		if (Vector3.SqrMagnitude(MainCamera.t.position - position) < this.maxDistance * this.maxDistance && this.bonkPrefab != null)
		{
			Object.Instantiate<GameObject>(this.bonkPrefab, position, Quaternion.identity);
		}
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x0002F1FC File Offset: 0x0002D3FC
	public void Bonk(Vector3 position, BoxCollider box)
	{
		if (Vector3.SqrMagnitude(MainCamera.t.position - position) < this.maxDistance * this.maxDistance && this.bonkPrefab != null)
		{
			Object.Instantiate<GameObject>(this.bonkPrefab, position, Quaternion.identity).GetComponent<FitBounds>().Fit(box);
		}
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x00002229 File Offset: 0x00000429
	private void EmitForSystem(Vector3 position, int count, Vector3 direction, Vector3 velocity, ParticleSystem system)
	{
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x0002F258 File Offset: 0x0002D458
	public void Dig(Vector3 position)
	{
		if (Vector3.SqrMagnitude(MainCamera.t.position - position) < this.maxDistance * this.maxDistance && this.digPrefab != null)
		{
			Object.Instantiate<GameObject>(this.digPrefab, position, Quaternion.identity);
		}
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x0002F2AC File Offset: 0x0002D4AC
	public void TryToPlayHitSound(Vector3 position, Vector3 direction, float volume = 1f)
	{
		SurfaceMaterial surfaceMaterial = MaterialManager.m.SampleSurfaceMaterial(position, direction);
		if (surfaceMaterial == null)
		{
			return;
		}
		surfaceMaterial.PlayImpact(position, volume, 1f);
	}

	// Token: 0x040007E8 RID: 2024
	private static EffectsManager instance;

	// Token: 0x040007E9 RID: 2025
	public GameObject splashPrefab;

	// Token: 0x040007EA RID: 2026
	public GameObject bonkPrefab;

	// Token: 0x040007EB RID: 2027
	public ParticleSystem rippleSystem;

	// Token: 0x040007EC RID: 2028
	public ParticleSystem dustSystem;

	// Token: 0x040007ED RID: 2029
	public GameObject sweatPrefab;

	// Token: 0x040007EE RID: 2030
	public float maxDistance = 80f;

	// Token: 0x040007EF RID: 2031
	public GameObject digPrefab;

	// Token: 0x040007F0 RID: 2032
	private const float minVolume = 0.3f;

	// Token: 0x040007F1 RID: 2033
	private const float minDelay = 0.1f;

	// Token: 0x040007F2 RID: 2034
	private const float maxDelay = 0.25f;

	// Token: 0x040007F3 RID: 2035
	private float lastSplashTime = -10f;
}
