using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000043 RID: 67
public class LeafPile : PersistentObject
{
	// Token: 0x0600010C RID: 268 RVA: 0x00006BA8 File Offset: 0x00004DA8
	private void Start()
	{
		this.displacementPerHealth = this.maxDisplacement / (float)this.health;
		if (this.hiddenObject)
		{
			this.hiddenObject.SetActive(false);
		}
	}

	// Token: 0x0600010D RID: 269 RVA: 0x00006BD8 File Offset: 0x00004DD8
	private void OnTriggerEnter(Collider other)
	{
		this.health--;
		if (this.splashPrefab)
		{
			Object.Instantiate<GameObject>(this.splashPrefab, base.transform.position, base.transform.rotation);
		}
		if (this.health > 0)
		{
			Vector3 position = base.transform.position;
			position.y -= this.displacementPerHealth;
			base.transform.position = position;
			return;
		}
		if (this.onClear != null)
		{
			this.onClear.Invoke();
		}
		if (this.hiddenObject)
		{
			this.hiddenObject.SetActive(true);
		}
		this.SaveTrue();
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000179 RID: 377
	public int health = 3;

	// Token: 0x0400017A RID: 378
	public float maxDisplacement = 0.5f;

	// Token: 0x0400017B RID: 379
	private float displacementPerHealth;

	// Token: 0x0400017C RID: 380
	public GameObject hiddenObject;

	// Token: 0x0400017D RID: 381
	public UnityEvent onClear;

	// Token: 0x0400017E RID: 382
	public GameObject splashPrefab;
}
