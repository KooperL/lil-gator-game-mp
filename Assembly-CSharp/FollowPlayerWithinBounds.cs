using System;
using UnityEngine;

public class FollowPlayerWithinBounds : MonoBehaviour
{
	// Token: 0x060005C9 RID: 1481 RVA: 0x0001E538 File Offset: 0x0001C738
	private void OnEnable()
	{
		this.UpdatePosition();
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x0001E540 File Offset: 0x0001C740
	private void LateUpdate()
	{
		this.UpdatePosition();
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x0001E548 File Offset: 0x0001C748
	private void UpdatePosition()
	{
		Vector3 vector = Player.Position;
		vector = this.boundsCollider.ClosestPoint(vector);
		base.transform.position = vector + this.offset;
	}

	public Collider boundsCollider;

	public Vector3 offset = Vector3.up;
}
