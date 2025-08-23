using System;
using System.Collections;
using UnityEngine;

public class HitSquish : MonoBehaviour, IHit
{
	// Token: 0x06000820 RID: 2080 RVA: 0x0000803A File Offset: 0x0000623A
	public void Hit(Vector3 velocity, bool isHeavy = false)
	{
		if (this.squish == null)
		{
			this.squish = this.Squish();
			base.StartCoroutine(this.squish);
		}
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x0000805D File Offset: 0x0000625D
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

	private float squishAmount = 0.08f;

	private float squishTime = 0.15f;

	private IEnumerator squish;
}
