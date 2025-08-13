using System;
using UnityEngine;

// Token: 0x0200024F RID: 591
[CreateAssetMenu]
[Serializable]
public class SurfaceMaterial : ScriptableObject
{
	// Token: 0x170000DB RID: 219
	// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0003DBE6 File Offset: 0x0003BDE6
	public bool HasFootstep
	{
		get
		{
			return this.footsteps != null && this.footsteps.Length != 0;
		}
	}

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0003DBFC File Offset: 0x0003BDFC
	public AudioClip Footstep
	{
		get
		{
			if (this.footsteps == null || this.footsteps.Length == 0)
			{
				return null;
			}
			int num = Mathf.FloorToInt(Random.value * (float)this.footsteps.Length);
			while (this.footsteps.Length > 1 && num == this.lastFootstepIndex)
			{
				num = (num + 1) % this.footsteps.Length;
			}
			this.lastFootstepIndex = num;
			return this.footsteps[num];
		}
	}

	// Token: 0x170000DD RID: 221
	// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0003DC63 File Offset: 0x0003BE63
	public bool HasImpact
	{
		get
		{
			return this.impacts != null && this.impacts.Length != 0;
		}
	}

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0003DC7C File Offset: 0x0003BE7C
	public AudioClip Impact
	{
		get
		{
			if (this.impacts == null || this.impacts.Length == 0)
			{
				return null;
			}
			int num = Mathf.FloorToInt(Random.value * (float)this.impacts.Length);
			while (this.impacts.Length > 1 && num == this.lastImpactIndex)
			{
				num = (num + 1) % this.impacts.Length;
			}
			this.lastImpactIndex = num;
			return this.impacts[Mathf.FloorToInt(Random.value * (float)this.impacts.Length)];
		}
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x0003DCF8 File Offset: 0x0003BEF8
	public void PlayFootstep(Vector3 position, float volumeMod = 1f, float pitchMod = 1f)
	{
		if (!this.HasFootstep)
		{
			return;
		}
		PlayAudio.p.PlayAtPoint(this.Footstep, position, PlayAudio.p.footstepMixer, volumeMod * this.footstepVolume, pitchMod * this.footstepPitch, 128);
		if (this.footstepEffect != null)
		{
			Object.Instantiate<GameObject>(this.footstepEffect, position, Quaternion.identity);
		}
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x0003DD60 File Offset: 0x0003BF60
	public void PlayImpact(Vector3 position, float volumeMod = 1f, float pitchMod = 1f)
	{
		if (!this.HasImpact)
		{
			return;
		}
		PlayAudio.p.PlayAtPoint(this.Impact, position, volumeMod * this.impactVolume, pitchMod * this.impactPitch, 128);
		if (this.impactEffect != null)
		{
			Object.Instantiate<GameObject>(this.impactEffect, position, Quaternion.identity);
		}
	}

	// Token: 0x040010E3 RID: 4323
	public AudioClip[] footsteps;

	// Token: 0x040010E4 RID: 4324
	public AudioClip[] impacts;

	// Token: 0x040010E5 RID: 4325
	public AudioClip scraping;

	// Token: 0x040010E6 RID: 4326
	public Color color;

	// Token: 0x040010E7 RID: 4327
	private int lastFootstepIndex;

	// Token: 0x040010E8 RID: 4328
	private int lastImpactIndex;

	// Token: 0x040010E9 RID: 4329
	public GameObject footstepEffect;

	// Token: 0x040010EA RID: 4330
	public GameObject impactEffect;

	// Token: 0x040010EB RID: 4331
	[Range(0f, 1f)]
	public float footstepVolume = 0.8f;

	// Token: 0x040010EC RID: 4332
	[Range(0.25f, 2f)]
	public float footstepPitch = 1f;

	// Token: 0x040010ED RID: 4333
	[Range(0f, 1f)]
	public float impactVolume = 0.8f;

	// Token: 0x040010EE RID: 4334
	[Range(0.25f, 2f)]
	public float impactPitch = 1f;
}
