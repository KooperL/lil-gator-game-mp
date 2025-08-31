using System;
using UnityEngine;

public class ProximityEnabled : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000B32 RID: 2866 RVA: 0x00037DC7 File Offset: 0x00035FC7
	private void OnValidate()
	{
		if (this.collider == null)
		{
			this.collider = base.GetComponent<Collider>();
		}
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x00037DE3 File Offset: 0x00035FE3
	private void OnEnable()
	{
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x00037DF0 File Offset: 0x00035FF0
	private void OnDisable()
	{
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x00037DFE File Offset: 0x00035FFE
	public void OnTriggerEnter(Collider other)
	{
		this.stepsSinceProximity = 0;
		if (!base.enabled)
		{
			this.OnProximityEnter();
		}
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x00037E18 File Offset: 0x00036018
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

	// Token: 0x06000B37 RID: 2871 RVA: 0x00037E74 File Offset: 0x00036074
	private void OnProximityEnter()
	{
		base.enabled = true;
		Renderer[] array = this.renderers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = true;
		}
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x00037EA8 File Offset: 0x000360A8
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
