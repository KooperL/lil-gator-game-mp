using System;
using UnityEngine;
using UnityEngine.Events;

public class LeafPile : PersistentObject
{
	// Token: 0x06000139 RID: 313 RVA: 0x00003151 File Offset: 0x00001351
	private void Start()
	{
		this.displacementPerHealth = this.maxDisplacement / (float)this.health;
		if (this.hiddenObject)
		{
			this.hiddenObject.SetActive(false);
		}
	}

	// Token: 0x0600013A RID: 314 RVA: 0x0001BB4C File Offset: 0x00019D4C
	private void OnTriggerEnter(Collider other)
	{
		this.health--;
		if (this.splashPrefab)
		{
			global::UnityEngine.Object.Instantiate<GameObject>(this.splashPrefab, base.transform.position, base.transform.rotation);
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
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	public int health = 3;

	public float maxDisplacement = 0.5f;

	private float displacementPerHealth;

	public GameObject hiddenObject;

	public UnityEvent onClear;

	public GameObject splashPrefab;
}
