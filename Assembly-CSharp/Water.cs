using System;
using UnityEngine;

public class Water : MonoBehaviour
{
	// Token: 0x060010CE RID: 4302 RVA: 0x0000E59C File Offset: 0x0000C79C
	private void OnValidate()
	{
		if (this.collider == null)
		{
			this.collider = base.GetComponent<Collider>();
		}
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x0000E5B8 File Offset: 0x0000C7B8
	public virtual float GetWaterPlaneHeight(Vector3 referencePosition)
	{
		return base.transform.TransformPoint(Vector3.up * this.heightOffset).y;
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x0000E5DA File Offset: 0x0000C7DA
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 8)
		{
			Player.movement.WaterTrigger(this.collider, this);
		}
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x0000E5DA File Offset: 0x0000C7DA
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer == 8)
		{
			Player.movement.WaterTrigger(this.collider, this);
		}
	}

	private const int playerLayer = 8;

	public Collider collider;

	public float heightOffset;
}
