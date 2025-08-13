using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001A8 RID: 424
public class HitSquish : MonoBehaviour, IHit
{
	// Token: 0x060007DF RID: 2015 RVA: 0x00007D2B File Offset: 0x00005F2B
	public void Hit(Vector3 velocity, bool isHeavy = false)
	{
		if (this.squish == null)
		{
			this.squish = this.Squish();
			base.StartCoroutine(this.squish);
		}
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x00007D4E File Offset: 0x00005F4E
	private IEnumerator Squish()
	{
		float time = 0f;
		while (time < this.squishTime)
		{
			time += Time.deltaTime;
			float num = 1f - 2f * Mathf.Abs(0.5f * this.squishTime - time) / this.squishTime;
			base.transform.localScale = new Vector3(1f + num * this.squishAmount, 1f - num * this.squishAmount, 1f + num * this.squishAmount);
			yield return null;
		}
		this.squish = null;
		yield break;
	}

	// Token: 0x04000A7B RID: 2683
	private float squishAmount = 0.08f;

	// Token: 0x04000A7C RID: 2684
	private float squishTime = 0.15f;

	// Token: 0x04000A7D RID: 2685
	private IEnumerator squish;
}
