using System;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class SurfaceMaterial : ScriptableObject
{
	// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0000DA78 File Offset: 0x0000BC78
	public bool HasFootstep
	{
		get
		{
			return this.footsteps != null && this.footsteps.Length != 0;
		}
	}

	// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x000527EC File Offset: 0x000509EC
	public AudioClip Footstep
	{
		get
		{
			if (this.footsteps == null || this.footsteps.Length == 0)
			{
				return null;
			}
			int num = Mathf.FloorToInt(global::UnityEngine.Random.value * (float)this.footsteps.Length);
			while (this.footsteps.Length > 1 && num == this.lastFootstepIndex)
			{
				num = (num + 1) % this.footsteps.Length;
			}
			this.lastFootstepIndex = num;
			return this.footsteps[num];
		}
	}

	// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x0000DA8E File Offset: 0x0000BC8E
	public bool HasImpact
	{
		get
		{
			return this.impacts != null && this.impacts.Length != 0;
		}
	}

	// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x00052854 File Offset: 0x00050A54
	public AudioClip Impact
	{
		get
		{
			if (this.impacts == null || this.impacts.Length == 0)
			{
				return null;
			}
			int num = Mathf.FloorToInt(global::UnityEngine.Random.value * (float)this.impacts.Length);
			while (this.impacts.Length > 1 && num == this.lastImpactIndex)
			{
				num = (num + 1) % this.impacts.Length;
			}
			this.lastImpactIndex = num;
			return this.impacts[Mathf.FloorToInt(global::UnityEngine.Random.value * (float)this.impacts.Length)];
		}
	}

	// Token: 0x06000FD3 RID: 4051 RVA: 0x000528D0 File Offset: 0x00050AD0
	public void PlayFootstep(Vector3 position, float volumeMod = 1f, float pitchMod = 1f)
	{
		if (!this.HasFootstep)
		{
			return;
		}
		PlayAudio.p.PlayAtPoint(this.Footstep, position, PlayAudio.p.footstepMixer, volumeMod * this.footstepVolume, pitchMod * this.footstepPitch, 128);
		if (this.footstepEffect != null)
		{
			global::UnityEngine.Object.Instantiate<GameObject>(this.footstepEffect, position, Quaternion.identity);
		}
	}

	// Token: 0x06000FD4 RID: 4052 RVA: 0x00052938 File Offset: 0x00050B38
	public void PlayImpact(Vector3 position, float volumeMod = 1f, float pitchMod = 1f)
	{
		if (!this.HasImpact)
		{
			return;
		}
		PlayAudio.p.PlayAtPoint(this.Impact, position, volumeMod * this.impactVolume, pitchMod * this.impactPitch, 128);
		if (this.impactEffect != null)
		{
			global::UnityEngine.Object.Instantiate<GameObject>(this.impactEffect, position, Quaternion.identity);
		}
	}

	public AudioClip[] footsteps;

	public AudioClip[] impacts;

	public AudioClip scraping;

	public Color color;

	private int lastFootstepIndex;

	private int lastImpactIndex;

	public GameObject footstepEffect;

	public GameObject impactEffect;

	[Range(0f, 1f)]
	public float footstepVolume = 0.8f;

	[Range(0.25f, 2f)]
	public float footstepPitch = 1f;

	[Range(0f, 1f)]
	public float impactVolume = 0.8f;

	[Range(0.25f, 2f)]
	public float impactPitch = 1f;
}
