using System;
using UnityEngine;

public class Racetrack : TimedChallenge
{
	// Token: 0x06000857 RID: 2135 RVA: 0x00008341 File Offset: 0x00006541
	protected override void LoadState()
	{
		base.LoadState();
		this.finishRaceObject.SetActive(false);
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x00008355 File Offset: 0x00006555
	public override void StartRace()
	{
		base.StartRace();
		if (this.isRacing)
		{
			this.finishRaceObject.SetActive(true);
		}
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x00008371 File Offset: 0x00006571
	public override void ClearRace()
	{
		base.ClearRace();
		this.finishRaceObject.SetActive(false);
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x00008385 File Offset: 0x00006585
	protected override void DoParticleEffects()
	{
		base.DoParticleEffects();
		EffectsManager.e.Dust(this.finishRaceObject.transform.position, 15);
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x000083A9 File Offset: 0x000065A9
	protected override Transform RaceGoal()
	{
		return this.finishRaceObject.transform;
	}

	public GameObject finishRaceObject;
}
