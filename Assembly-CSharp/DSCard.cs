using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Title Card")]
public class DSCard : DialogueSequence
{
	// Token: 0x0600057C RID: 1404 RVA: 0x00005F73 File Offset: 0x00004173
	public override void Activate()
	{
		this.titleCard.SetActive(true);
		base.Activate();
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x00005F87 File Offset: 0x00004187
	public override YieldInstruction Run()
	{
		return new WaitForSeconds(this.waitTime);
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x00005F94 File Offset: 0x00004194
	public override void Deactivate()
	{
		base.Deactivate();
		base.StartCoroutine(this.DelayedDisable());
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x00005FA9 File Offset: 0x000041A9
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
