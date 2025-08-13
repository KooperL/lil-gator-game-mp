using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000145 RID: 325
public class HitSquish : MonoBehaviour, IHit
{
	// Token: 0x060006A7 RID: 1703 RVA: 0x00021EDD File Offset: 0x000200DD
	public void Hit(Vector3 velocity, bool isHeavy = false)
	{
		if (this.squish == null)
		{
			this.squish = this.Squish();
			base.StartCoroutine(this.squish);
		}
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x00021F00 File Offset: 0x00020100
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

	// Token: 0x040008F7 RID: 2295
	private float squishAmount = 0.08f;

	// Token: 0x040008F8 RID: 2296
	private float squishTime = 0.15f;

	// Token: 0x040008F9 RID: 2297
	private IEnumerator squish;
}
