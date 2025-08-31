using System;
using UnityEngine;

public class Water : MonoBehaviour
{
	// Token: 0x06000DB6 RID: 3510 RVA: 0x00042723 File Offset: 0x00040923
	private void OnValidate()
	{
		if (this.collider == null)
		{
			this.collider = base.GetComponent<Collider>();
		}
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x0004273F File Offset: 0x0004093F
	public virtual float GetWaterPlaneHeight(Vector3 referencePosition)
	{
		return base.transform.TransformPoint(Vector3.up * this.heightOffset).y;
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x00042761 File Offset: 0x00040961
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 8)
		{
			Player.movement.WaterTrigger(this.collider, this);
		}
	}

	// Token: 0x06000DB9 RID: 3513 RVA: 0x00042782 File Offset: 0x00040982
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
