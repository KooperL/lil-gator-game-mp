using System;
using UnityEngine;

public class Water : MonoBehaviour
{
	// Token: 0x060010CD RID: 4301 RVA: 0x0000E592 File Offset: 0x0000C792
	private void OnValidate()
	{
		if (this.collider == null)
		{
			this.collider = base.GetComponent<Collider>();
		}
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x0000E5AE File Offset: 0x0000C7AE
	public virtual float GetWaterPlaneHeight(Vector3 referencePosition)
	{
		return base.transform.TransformPoint(Vector3.up * this.heightOffset).y;
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x0000E5D0 File Offset: 0x0000C7D0
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 8)
		{
			Player.movement.WaterTrigger(this.collider, this);
		}
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x0000E5D0 File Offset: 0x0000C7D0
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
