using System;
using UnityEngine;

// Token: 0x0200016E RID: 366
public class Follow : MonoBehaviour
{
	// Token: 0x060006E9 RID: 1769 RVA: 0x000070D0 File Offset: 0x000052D0
	private void Start()
	{
		this.lockedPosition = base.transform.position;
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x00032460 File Offset: 0x00030660
	private void LateUpdate()
	{
		Vector3 vector = base.transform.position;
		if (this.lockToTarget)
		{
			vector = this.target.TransformPoint(this.targetLocalPosition);
		}
		else
		{
			Vector3 vector2 = this.target.TransformPoint(this.targetLocalPosition) - vector;
			float magnitude = vector2.magnitude;
			if (magnitude <= this.allowance)
			{
				return;
			}
			vector += vector2.normalized * (magnitude - this.allowance) * Time.deltaTime * this.speedMultiplier;
		}
		if (this.clampMaxDistance)
		{
			vector = Vector3.MoveTowards(this.lockedPosition, vector, this.maxDistance);
		}
		if (this.lockToGrid)
		{
			for (int i = 0; i < 3; i++)
			{
				vector[i] = Mathf.Round(vector[i] / this.gridSize) * this.gridSize;
			}
		}
		if (this.lockY)
		{
			vector.y = this.lockedPosition.y;
		}
		base.transform.position = vector;
	}

	// Token: 0x04000942 RID: 2370
	public Transform target;

	// Token: 0x04000943 RID: 2371
	public Vector3 targetLocalPosition = Vector3.zero;

	// Token: 0x04000944 RID: 2372
	[ConditionalHide("lockToTarget", true, Inverse = true)]
	public float allowance = 1.5f;

	// Token: 0x04000945 RID: 2373
	[ConditionalHide("lockToTarget", true, Inverse = true)]
	public float speedMultiplier = 1f;

	// Token: 0x04000946 RID: 2374
	private Vector3 velocity;

	// Token: 0x04000947 RID: 2375
	public bool lockToTarget;

	// Token: 0x04000948 RID: 2376
	public bool lockY;

	// Token: 0x04000949 RID: 2377
	private Vector3 lockedPosition;

	// Token: 0x0400094A RID: 2378
	public bool lockToGrid;

	// Token: 0x0400094B RID: 2379
	[ConditionalHide("lockToGrid", true)]
	public float gridSize = 0.625f;

	// Token: 0x0400094C RID: 2380
	public bool clampMaxDistance;

	// Token: 0x0400094D RID: 2381
	[ConditionalHide("clampMaxDistance", true)]
	public float maxDistance = 500f;
}
