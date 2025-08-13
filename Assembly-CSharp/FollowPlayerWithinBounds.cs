using System;
using UnityEngine;

// Token: 0x02000170 RID: 368
public class FollowPlayerWithinBounds : MonoBehaviour
{
	// Token: 0x060006EF RID: 1775 RVA: 0x00007122 File Offset: 0x00005322
	private void OnEnable()
	{
		this.UpdatePosition();
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x00007122 File Offset: 0x00005322
	private void LateUpdate()
	{
		this.UpdatePosition();
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x0003256C File Offset: 0x0003076C
	private void UpdatePosition()
	{
		Vector3 vector = Player.Position;
		vector = this.boundsCollider.ClosestPoint(vector);
		base.transform.position = vector + this.offset;
	}

	// Token: 0x0400094E RID: 2382
	public Collider boundsCollider;

	// Token: 0x0400094F RID: 2383
	public Vector3 offset = Vector3.up;
}
