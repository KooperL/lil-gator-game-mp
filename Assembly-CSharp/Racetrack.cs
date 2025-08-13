using System;
using UnityEngine;

// Token: 0x020001B2 RID: 434
public class Racetrack : TimedChallenge
{
	// Token: 0x06000817 RID: 2071 RVA: 0x00008047 File Offset: 0x00006247
	protected override void LoadState()
	{
		base.LoadState();
		this.finishRaceObject.SetActive(false);
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x0000805B File Offset: 0x0000625B
	public override void StartRace()
	{
		base.StartRace();
		if (this.isRacing)
		{
			this.finishRaceObject.SetActive(true);
		}
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x00008077 File Offset: 0x00006277
	public override void ClearRace()
	{
		base.ClearRace();
		this.finishRaceObject.SetActive(false);
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x0000808B File Offset: 0x0000628B
	protected override void DoParticleEffects()
	{
		base.DoParticleEffects();
		EffectsManager.e.Dust(this.finishRaceObject.transform.position, 15);
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x000080AF File Offset: 0x000062AF
	protected override Transform RaceGoal()
	{
		return this.finishRaceObject.transform;
	}

	// Token: 0x04000AC2 RID: 2754
	public GameObject finishRaceObject;
}
