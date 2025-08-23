using System;
using UnityEngine;

public class ProximityEnabled : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000D4A RID: 3402 RVA: 0x0000C362 File Offset: 0x0000A562
	private void OnValidate()
	{
		if (this.collider == null)
		{
			this.collider = base.GetComponent<Collider>();
		}
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x00002B40 File Offset: 0x00000D40
	private void OnEnable()
	{
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x000028C1 File Offset: 0x00000AC1
	private void OnDisable()
	{
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x0000C37E File Offset: 0x0000A57E
	public void OnTriggerEnter(Collider other)
	{
		this.stepsSinceProximity = 0;
		if (!base.enabled)
		{
			this.OnProximityEnter();
		}
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x0004ACEC File Offset: 0x00048EEC
	public void ManagedUpdate()
	{
		Vector3 position = Player.Position;
		if ((this.collider.ClosestPoint(position) - position).sqrMagnitude > 1f)
		{
			this.stepsSinceProximity++;
		}
		else
		{
			this.stepsSinceProximity = 0;
		}
		if (this.stepsSinceProximity > 5)
		{
			this.OnProximityExit();
		}
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x0004AD48 File Offset: 0x00048F48
	private void OnProximityEnter()
	{
		base.enabled = true;
		Renderer[] array = this.renderers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = true;
		}
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x0004AD7C File Offset: 0x00048F7C
	private void OnProximityExit()
	{
		base.enabled = false;
		Renderer[] array = this.renderers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = false;
		}
	}

	public Renderer[] renderers;

	public Collider collider;

	private int stepsSinceProximity;
}
