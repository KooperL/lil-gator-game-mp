using System;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
	// (get) Token: 0x06000616 RID: 1558 RVA: 0x0000659F File Offset: 0x0000479F
	public static EffectsManager e
	{
		get
		{
			if (EffectsManager.instance == null)
			{
				EffectsManager.instance = global::UnityEngine.Object.FindObjectOfType<EffectsManager>();
			}
			return EffectsManager.instance;
		}
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x000065BD File Offset: 0x000047BD
	private void Awake()
	{
		EffectsManager.instance = this;
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x000065BD File Offset: 0x000047BD
	private void OnEnable()
	{
		EffectsManager.instance = this;
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x000065C5 File Offset: 0x000047C5
	private float ModifyVolume(float volume, float lastSoundTime)
	{
		return volume * Mathf.Lerp(0.3f, 1f, Mathf.InverseLerp(0.1f, 0.25f, Time.time - lastSoundTime));
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x00030628 File Offset: 0x0002E828
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
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.splashPrefab, position, Quaternion.identity);
			if (volume != 1f)
			{
				AudioSourceVariance component = gameObject.GetComponent<AudioSourceVariance>();
				component.maxVolume *= volume;
				component.minVolume *= volume;
			}
		}
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x000065EE File Offset: 0x000047EE
	public void Ripple(Vector3 position, int count)
	{
		this.Ripple(position, count, Vector3.zero);
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x000306C8 File Offset: 0x0002E8C8
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

	// Token: 0x0600061D RID: 1565 RVA: 0x000065FD File Offset: 0x000047FD
	public void Dust(Vector3 position, int count)
	{
		this.Dust(position, count, Vector3.zero, 0f);
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x00030764 File Offset: 0x0002E964
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

	// Token: 0x0600061F RID: 1567 RVA: 0x00030800 File Offset: 0x0002EA00
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
			emitParams.velocity = global::UnityEngine.Random.Range(8f, 10f) * Vector3.ProjectOnPlane(global::UnityEngine.Random.onUnitSphere, normal).normalized + 2f * global::UnityEngine.Random.value * normal;
			this.dustSystem.Emit(emitParams, 1);
		}
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x000308B8 File Offset: 0x0002EAB8
	public void Bonk(Vector3 position)
	{
		if (Vector3.SqrMagnitude(MainCamera.t.position - position) < this.maxDistance * this.maxDistance && this.bonkPrefab != null)
		{
			global::UnityEngine.Object.Instantiate<GameObject>(this.bonkPrefab, position, Quaternion.identity);
		}
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x0003090C File Offset: 0x0002EB0C
	public void Bonk(Vector3 position, BoxCollider box)
	{
		if (Vector3.SqrMagnitude(MainCamera.t.position - position) < this.maxDistance * this.maxDistance && this.bonkPrefab != null)
		{
			global::UnityEngine.Object.Instantiate<GameObject>(this.bonkPrefab, position, Quaternion.identity).GetComponent<FitBounds>().Fit(box);
		}
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x00002229 File Offset: 0x00000429
	private void EmitForSystem(Vector3 position, int count, Vector3 direction, Vector3 velocity, ParticleSystem system)
	{
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x00030968 File Offset: 0x0002EB68
	public void Dig(Vector3 position)
	{
		if (Vector3.SqrMagnitude(MainCamera.t.position - position) < this.maxDistance * this.maxDistance && this.digPrefab != null)
		{
			global::UnityEngine.Object.Instantiate<GameObject>(this.digPrefab, position, Quaternion.identity);
		}
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x000309BC File Offset: 0x0002EBBC
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
