using System;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x0200022C RID: 556
public class Footsteps : MonoBehaviour
{
	// Token: 0x06000A76 RID: 2678 RVA: 0x0000A0E4 File Offset: 0x000082E4
	public void ClearOverride()
	{
		this.overrideSettings = false;
		this.overrideFootstepMaterial = null;
		this.overrideHasVisualFootsteps = true;
		this.overrideHasFootstepDust = true;
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0000A102 File Offset: 0x00008302
	public void DoStep(bool isLeft)
	{
		this.Step(isLeft ? this.leftFoot : this.rightFoot, isLeft);
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x0003D250 File Offset: 0x0003B450
	private void Step(Transform footTransform, bool isLeft)
	{
		if (Time.time - this.minFootstepDelay < this.lastFootstep)
		{
			return;
		}
		this.lastFootstep = Time.time;
		Vector3 position = footTransform.position;
		SurfaceMaterial surfaceMaterial;
		RaycastHit raycastHit;
		if (this.overrideSettings)
		{
			if (this.overrideFootstepMaterial != null)
			{
				this.PlayFootstep(this.overrideFootstepMaterial, position, isLeft);
			}
			if (this.overrideHasVisualFootsteps)
			{
				this.MakeFootprint(this.overrideFootstepMaterial, position, footTransform.rotation);
			}
			if (this.overrideHasFootstepDust)
			{
				this.MakeFootstepDust(this.overrideFootstepMaterial, position);
				return;
			}
		}
		else if (MaterialManager.m.SampleSurfaceMaterial(position, Vector3.down, out surfaceMaterial, out raycastHit))
		{
			this.PlayFootstep(surfaceMaterial, position, isLeft);
			this.MakeFootprint(surfaceMaterial, raycastHit.point + 0.02f * raycastHit.normal, Quaternion.FromToRotation(footTransform.rotation * Vector3.forward, raycastHit.normal) * footTransform.rotation);
			this.MakeFootstepDust(surfaceMaterial, raycastHit.point + 0.02f * raycastHit.normal);
		}
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x0003D36C File Offset: 0x0003B56C
	private void PlayFootstep(SurfaceMaterial surface, Vector3 position, bool isLeft)
	{
		float num = this.footstepVolume;
		num *= Mathf.Clamp01(Player.rigidbody.velocity.magnitude / Player.movement.Speed);
		surface.PlayFootstep(position, num, isLeft ? this.leftFootPitch : 1f);
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x0003D3C0 File Offset: 0x0003B5C0
	private void MakeFootprint(SurfaceMaterial surface, Vector3 position, Quaternion rotation)
	{
		Color color = Color.white;
		if (surface != null)
		{
			color = surface.color;
		}
		if (color.a > 0f)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.footprint);
			gameObject.transform.position = position;
			gameObject.transform.rotation = rotation;
			gameObject.GetComponent<SpriteRenderer>().color = color;
		}
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x0003D420 File Offset: 0x0003B620
	private void MakeFootstepDust(SurfaceMaterial surface, Vector3 position)
	{
		if (Player.rigidbody.velocity.sqrMagnitude < this.minDustSpeed * this.minDustSpeed)
		{
			return;
		}
		int num = Mathf.RoundToInt(Random.Range(1f, 2f));
		EffectsManager.e.Dust(position, num, base.transform.TransformVector(new Vector3(0f, 1f, -4f)), 0f);
	}

	// Token: 0x04000D32 RID: 3378
	public AudioClip[] defaultFootsteps;

	// Token: 0x04000D33 RID: 3379
	private int footstepIndex;

	// Token: 0x04000D34 RID: 3380
	public float footstepVolume = 0.1f;

	// Token: 0x04000D35 RID: 3381
	public float leftFootPitch = 0.9f;

	// Token: 0x04000D36 RID: 3382
	public Transform leftFoot;

	// Token: 0x04000D37 RID: 3383
	public Transform rightFoot;

	// Token: 0x04000D38 RID: 3384
	public LayerMask layerMask;

	// Token: 0x04000D39 RID: 3385
	private AudioSource audioSource;

	// Token: 0x04000D3A RID: 3386
	public GameObject footprint;

	// Token: 0x04000D3B RID: 3387
	public AudioMixerGroup mixerGroup;

	// Token: 0x04000D3C RID: 3388
	public float minFootstepDelay = 0.1f;

	// Token: 0x04000D3D RID: 3389
	private float lastFootstep = -1f;

	// Token: 0x04000D3E RID: 3390
	public float minDustSpeed = 4.5f;

	// Token: 0x04000D3F RID: 3391
	public bool overrideSettings;

	// Token: 0x04000D40 RID: 3392
	public SurfaceMaterial overrideFootstepMaterial;

	// Token: 0x04000D41 RID: 3393
	public bool overrideHasVisualFootsteps = true;

	// Token: 0x04000D42 RID: 3394
	public bool overrideHasFootstepDust = true;
}
