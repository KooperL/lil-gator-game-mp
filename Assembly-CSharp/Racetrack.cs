using System;
using UnityEngine;

public class Racetrack : TimedChallenge
{
	// Token: 0x060006D9 RID: 1753 RVA: 0x00022B58 File Offset: 0x00020D58
	protected override void LoadState()
	{
		base.LoadState();
		this.finishRaceObject.SetActive(false);
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x00022B6C File Offset: 0x00020D6C
	public override void StartRace()
	{
		base.StartRace();
		if (this.isRacing)
		{
			this.finishRaceObject.SetActive(true);
		}
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x00022B88 File Offset: 0x00020D88
	public override void ClearRace()
	{
		base.ClearRace();
		this.finishRaceObject.SetActive(false);
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x00022B9C File Offset: 0x00020D9C
	protected override void DoParticleEffects()
	{
		base.DoParticleEffects();
		EffectsManager.e.Dust(this.finishRaceObject.transform.position, 15);
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x00022BC0 File Offset: 0x00020DC0
	protected override Transform RaceGoal()
	{
		return this.finishRaceObject.transform;
	}

	public GameObject finishRaceObject;
}
