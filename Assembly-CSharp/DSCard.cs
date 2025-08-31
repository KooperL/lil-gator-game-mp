using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Title Card")]
public class DSCard : DialogueSequence
{
	// Token: 0x06000466 RID: 1126 RVA: 0x00018E30 File Offset: 0x00017030
	public override void Activate()
	{
		this.titleCard.SetActive(true);
		base.Activate();
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x00018E44 File Offset: 0x00017044
	public override YieldInstruction Run()
	{
		return new WaitForSeconds(this.waitTime);
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x00018E51 File Offset: 0x00017051
	public override void Deactivate()
	{
		base.Deactivate();
		base.StartCoroutine(this.DelayedDisable());
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x00018E66 File Offset: 0x00017066
	private IEnumerator DelayedDisable()
	{
		yield return new WaitForSecondsRealtime(this.deactivateTime);
		this.titleCard.SetActive(false);
		yield break;
	}

	public GameObject titleCard;

	public float waitTime = 1f;

	public float deactivateTime = 5f;
}
