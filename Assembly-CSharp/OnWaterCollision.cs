using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200021D RID: 541
public class OnWaterCollision : MonoBehaviour
{
	// Token: 0x06000A17 RID: 2583 RVA: 0x00009B8D File Offset: 0x00007D8D
	private void OnTriggerEnter(Collider other)
	{
		this.WaterTrigger(other);
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x00009B8D File Offset: 0x00007D8D
	private void OnTriggerStay(Collider other)
	{
		this.WaterTrigger(other);
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x0003B548 File Offset: 0x00039748
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

	// Token: 0x04000C9E RID: 3230
	private const int waterLayer = 4;

	// Token: 0x04000C9F RID: 3231
	public UnityEvent onWaterCollision;
}
