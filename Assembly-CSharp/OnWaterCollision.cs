using System;
using UnityEngine;
using UnityEngine.Events;

public class OnWaterCollision : MonoBehaviour
{
	// Token: 0x06000A62 RID: 2658 RVA: 0x00009ECB File Offset: 0x000080CB
	private void OnTriggerEnter(Collider other)
	{
		this.WaterTrigger(other);
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x00009ECB File Offset: 0x000080CB
	private void OnTriggerStay(Collider other)
	{
		this.WaterTrigger(other);
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x0003D2BC File Offset: 0x0003B4BC
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
