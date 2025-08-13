using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000112 RID: 274
[AddComponentMenu("Dialogue Sequence/Title Card")]
public class DSCard : DialogueSequence
{
	// Token: 0x06000542 RID: 1346 RVA: 0x00005CAD File Offset: 0x00003EAD
	public override void Activate()
	{
		this.titleCard.SetActive(true);
		base.Activate();
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x00005CC1 File Offset: 0x00003EC1
	public override YieldInstruction Run()
	{
		return new WaitForSeconds(this.waitTime);
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00005CCE File Offset: 0x00003ECE
	public override void Deactivate()
	{
		base.Deactivate();
		base.StartCoroutine(this.DelayedDisable());
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00005CE3 File Offset: 0x00003EE3
	private IEnumerator DelayedDisable()
	{
		yield return new WaitForSecondsRealtime(this.deactivateTime);
		this.titleCard.SetActive(false);
		yield break;
	}

	// Token: 0x04000743 RID: 1859
	public GameObject titleCard;

	// Token: 0x04000744 RID: 1860
	public float waitTime = 1f;

	// Token: 0x04000745 RID: 1861
	public float deactivateTime = 5f;
}
