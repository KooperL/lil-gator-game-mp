using System;
using UnityEngine;

// Token: 0x02000353 RID: 851
public class Water : MonoBehaviour
{
	// Token: 0x06001072 RID: 4210 RVA: 0x0000E229 File Offset: 0x0000C429
	private void OnValidate()
	{
		if (this.collider == null)
		{
			this.collider = base.GetComponent<Collider>();
		}
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x0000E245 File Offset: 0x0000C445
	public virtual float GetWaterPlaneHeight(Vector3 referencePosition)
	{
		return base.transform.TransformPoint(Vector3.up * this.heightOffset).y;
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x0000E267 File Offset: 0x0000C467
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 8)
		{
			Player.movement.WaterTrigger(this.collider, this);
		}
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x0000E267 File Offset: 0x0000C467
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer == 8)
		{
			Player.movement.WaterTrigger(this.collider, this);
		}
	}

	// Token: 0x04001551 RID: 5457
	private const int playerLayer = 8;

	// Token: 0x04001552 RID: 5458
	public Collider collider;

	// Token: 0x04001553 RID: 5459
	public float heightOffset;
}
