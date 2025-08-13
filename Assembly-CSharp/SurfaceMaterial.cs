using System;
using UnityEngine;

// Token: 0x02000314 RID: 788
[CreateAssetMenu]
[Serializable]
public class SurfaceMaterial : ScriptableObject
{
	// Token: 0x170001C2 RID: 450
	// (get) Token: 0x06000F73 RID: 3955 RVA: 0x0000D6C6 File Offset: 0x0000B8C6
	public bool HasFootstep
	{
		get
		{
			return this.footsteps != null && this.footsteps.Length != 0;
		}
	}

	// Token: 0x170001C3 RID: 451
	// (get) Token: 0x06000F74 RID: 3956 RVA: 0x000508EC File Offset: 0x0004EAEC
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

	// Token: 0x170001C4 RID: 452
	// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0000D6DC File Offset: 0x0000B8DC
	public bool HasImpact
	{
		get
		{
			return this.impacts != null && this.impacts.Length != 0;
		}
	}

	// Token: 0x170001C5 RID: 453
	// (get) Token: 0x06000F76 RID: 3958 RVA: 0x00050954 File Offset: 0x0004EB54
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

	// Token: 0x06000F77 RID: 3959 RVA: 0x000509D0 File Offset: 0x0004EBD0
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

	// Token: 0x06000F78 RID: 3960 RVA: 0x00050A38 File Offset: 0x0004EC38
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

	// Token: 0x040013FC RID: 5116
	public AudioClip[] footsteps;

	// Token: 0x040013FD RID: 5117
	public AudioClip[] impacts;

	// Token: 0x040013FE RID: 5118
	public AudioClip scraping;

	// Token: 0x040013FF RID: 5119
	public Color color;

	// Token: 0x04001400 RID: 5120
	private int lastFootstepIndex;

	// Token: 0x04001401 RID: 5121
	private int lastImpactIndex;

	// Token: 0x04001402 RID: 5122
	public GameObject footstepEffect;

	// Token: 0x04001403 RID: 5123
	public GameObject impactEffect;

	// Token: 0x04001404 RID: 5124
	[Range(0f, 1f)]
	public float footstepVolume = 0.8f;

	// Token: 0x04001405 RID: 5125
	[Range(0.25f, 2f)]
	public float footstepPitch = 1f;

	// Token: 0x04001406 RID: 5126
	[Range(0f, 1f)]
	public float impactVolume = 0.8f;

	// Token: 0x04001407 RID: 5127
	[Range(0.25f, 2f)]
	public float impactPitch = 1f;
}
