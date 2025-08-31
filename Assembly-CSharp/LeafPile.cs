using System;
using UnityEngine;
using UnityEngine.Events;

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

	public int health = 3;

	public float maxDisplacement = 0.5f;

	private float displacementPerHealth;

	public GameObject hiddenObject;

	public UnityEvent onClear;

	public GameObject splashPrefab;
}
