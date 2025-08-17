using System;
using UnityEngine;
using UnityEngine.Events;

public class OnWaterCollision : MonoBehaviour
{
	// Token: 0x06000A61 RID: 2657 RVA: 0x00009EC1 File Offset: 0x000080C1
	private void OnTriggerEnter(Collider other)
	{
		this.WaterTrigger(other);
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x00009EC1 File Offset: 0x000080C1
	private void OnTriggerStay(Collider other)
	{
		this.WaterTrigger(other);
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x0003CFF4 File Offset: 0x0003B1F4
	private void WaterTrigger(Collider collider)
	{
		if (collider.gameObject.layer != 4)
		{
			return;
		}
		Water component = collider.GetComponent<Water>();
		if (component == null)
		{
			return;
		}
		if (component.GetWaterPlaneHeight(base.transform.position) > base.transform.position.y)
		{
			return;
		}
		this.onWaterCollision.Invoke();
	}

	private const int waterLayer = 4;

	public UnityEvent onWaterCollision;
}
