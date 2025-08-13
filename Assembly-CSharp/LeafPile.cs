using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000056 RID: 86
public class LeafPile : PersistentObject
{
	// Token: 0x06000131 RID: 305 RVA: 0x000030AE File Offset: 0x000012AE
	private void Start()
	{
		this.displacementPerHealth = this.maxDisplacement / (float)this.health;
		if (this.hiddenObject)
		{
			this.hiddenObject.SetActive(false);
		}
	}

	// Token: 0x06000132 RID: 306 RVA: 0x0001B338 File Offset: 0x00019538
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

	// Token: 0x040001C3 RID: 451
	public int health = 3;

	// Token: 0x040001C4 RID: 452
	public float maxDisplacement = 0.5f;

	// Token: 0x040001C5 RID: 453
	private float displacementPerHealth;

	// Token: 0x040001C6 RID: 454
	public GameObject hiddenObject;

	// Token: 0x040001C7 RID: 455
	public UnityEvent onClear;

	// Token: 0x040001C8 RID: 456
	public GameObject splashPrefab;
}
