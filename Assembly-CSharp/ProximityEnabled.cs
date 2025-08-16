using System;
using UnityEngine;

public class ProximityEnabled : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000D49 RID: 3401 RVA: 0x0000C343 File Offset: 0x0000A543
	private void OnValidate()
	{
		if (this.collider == null)
		{
			this.collider = base.GetComponent<Collider>();
		}
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x00002B40 File Offset: 0x00000D40
	private void OnEnable()
	{
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x000028C1 File Offset: 0x00000AC1
	private void OnDisable()
	{
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x0000C35F File Offset: 0x0000A55F
	public void OnTriggerEnter(Collider other)
	{
		this.stepsSinceProximity = 0;
		if (!base.enabled)
		{
			this.OnProximityEnter();
		}
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x0004A890 File Offset: 0x00048A90
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

	// Token: 0x06000D4E RID: 3406 RVA: 0x0004A8EC File Offset: 0x00048AEC
	private void OnProximityEnter()
	{
		base.enabled = true;
		Renderer[] array = this.renderers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = true;
		}
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x0004A920 File Offset: 0x00048B20
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
