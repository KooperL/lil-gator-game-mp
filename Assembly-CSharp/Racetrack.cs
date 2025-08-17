using System;
using UnityEngine;

public class Racetrack : TimedChallenge
{
	// Token: 0x06000857 RID: 2135 RVA: 0x00008356 File Offset: 0x00006556
	protected override void LoadState()
	{
		base.LoadState();
		this.finishRaceObject.SetActive(false);
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x0000836A File Offset: 0x0000656A
	public override void StartRace()
	{
		base.StartRace();
		if (this.isRacing)
		{
			this.finishRaceObject.SetActive(true);
		}
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x00008386 File Offset: 0x00006586
	public override void ClearRace()
	{
		base.ClearRace();
		this.finishRaceObject.SetActive(false);
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x0000839A File Offset: 0x0000659A
	protected override void DoParticleEffects()
	{
		base.DoParticleEffects();
		EffectsManager.e.Dust(this.finishRaceObject.transform.position, 15);
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x000083BE File Offset: 0x000065BE
	protected override Transform RaceGoal()
	{
		return this.finishRaceObject.transform;
	}

	public GameObject finishRaceObject;
}
