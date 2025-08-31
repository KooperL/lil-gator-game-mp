using System;
using UnityEngine;
using UnityEngine.Events;

public class OnWaterCollision : MonoBehaviour
{
	// Token: 0x06000896 RID: 2198 RVA: 0x0002894D File Offset: 0x00026B4D
	private void OnTriggerEnter(Collider other)
	{
		this.WaterTrigger(other);
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x00028956 File Offset: 0x00026B56
	private void OnTriggerStay(Collider other)
	{
		this.WaterTrigger(other);
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x00028960 File Offset: 0x00026B60
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
