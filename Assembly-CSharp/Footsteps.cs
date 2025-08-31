using System;
using UnityEngine;
using UnityEngine.Audio;

public class Footsteps : MonoBehaviour
{
	// Token: 0x060008EF RID: 2287 RVA: 0x0002AC53 File Offset: 0x00028E53
	public void ClearOverride()
	{
		this.overrideSettings = false;
		this.overrideFootstepMaterial = null;
		this.overrideHasVisualFootsteps = true;
		this.overrideHasFootstepDust = true;
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x0002AC71 File Offset: 0x00028E71
	public void DoStep(bool isLeft)
	{
		this.Step(isLeft ? this.leftFoot : this.rightFoot, isLeft);
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x0002AC8C File Offset: 0x00028E8C
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

	// Token: 0x060008F2 RID: 2290 RVA: 0x0002ADA8 File Offset: 0x00028FA8
	private void PlayFootstep(SurfaceMaterial surface, Vector3 position, bool isLeft)
	{
		float num = this.footstepVolume;
		num *= Mathf.Clamp01(Player.rigidbody.velocity.magnitude / Player.movement.Speed);
		surface.PlayFootstep(position, num, isLeft ? this.leftFootPitch : 1f);
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x0002ADFC File Offset: 0x00028FFC
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

	// Token: 0x060008F4 RID: 2292 RVA: 0x0002AE5C File Offset: 0x0002905C
	private void MakeFootstepDust(SurfaceMaterial surface, Vector3 position)
	{
		if (Player.rigidbody.velocity.sqrMagnitude < this.minDustSpeed * this.minDustSpeed)
		{
			return;
		}
		int num = Mathf.RoundToInt(Random.Range(1f, 2f));
		EffectsManager.e.Dust(position, num, base.transform.TransformVector(new Vector3(0f, 1f, -4f)), 0f);
	}

	public AudioClip[] defaultFootsteps;

	private int footstepIndex;

	public float footstepVolume = 0.1f;

	public float leftFootPitch = 0.9f;

	public Transform leftFoot;

	public Transform rightFoot;

	public LayerMask layerMask;

	private AudioSource audioSource;

	public GameObject footprint;

	public AudioMixerGroup mixerGroup;

	public float minFootstepDelay = 0.1f;

	private float lastFootstep = -1f;

	public float minDustSpeed = 4.5f;

	public bool overrideSettings;

	public SurfaceMaterial overrideFootstepMaterial;

	public bool overrideHasVisualFootsteps = true;

	public bool overrideHasFootstepDust = true;
}
