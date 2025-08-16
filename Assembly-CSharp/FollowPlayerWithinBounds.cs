using System;
using UnityEngine;

public class FollowPlayerWithinBounds : MonoBehaviour
{
	// Token: 0x0600072D RID: 1837 RVA: 0x00007423 File Offset: 0x00005623
	private void OnEnable()
	{
		this.UpdatePosition();
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x00007423 File Offset: 0x00005623
	private void LateUpdate()
	{
		this.UpdatePosition();
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x00033C68 File Offset: 0x00031E68
	private void UpdatePosition()
	{
		Vector3 vector = Player.Position;
		vector = this.boundsCollider.ClosestPoint(vector);
		base.transform.position = vector + this.offset;
	}

	public Collider boundsCollider;

	public Vector3 offset = Vector3.up;
}
