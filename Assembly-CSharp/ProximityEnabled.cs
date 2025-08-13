using System;
using UnityEngine;

// Token: 0x0200029D RID: 669
public class ProximityEnabled : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000CFD RID: 3325 RVA: 0x0000C050 File Offset: 0x0000A250
	private void OnValidate()
	{
		if (this.collider == null)
		{
			this.collider = base.GetComponent<Collider>();
		}
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x00002ADC File Offset: 0x00000CDC
	private void OnEnable()
	{
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x06000CFF RID: 3327 RVA: 0x0000285D File Offset: 0x00000A5D
	private void OnDisable()
	{
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x0000C06C File Offset: 0x0000A26C
	public void OnTriggerEnter(Collider other)
	{
		this.stepsSinceProximity = 0;
		if (!base.enabled)
		{
			this.OnProximityEnter();
		}
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x00048E9C File Offset: 0x0004709C
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

	// Token: 0x06000D02 RID: 3330 RVA: 0x00048EF8 File Offset: 0x000470F8
	private void OnProximityEnter()
	{
		base.enabled = true;
		Renderer[] array = this.renderers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = true;
		}
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x00048F2C File Offset: 0x0004712C
	private void OnProximityExit()
	{
		base.enabled = false;
		Renderer[] array = this.renderers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = false;
		}
	}

	// Token: 0x0400115D RID: 4445
	public Renderer[] renderers;

	// Token: 0x0400115E RID: 4446
	public Collider collider;

	// Token: 0x0400115F RID: 4447
	private int stepsSinceProximity;
}
