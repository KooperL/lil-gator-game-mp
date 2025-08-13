using System;
using UnityEngine;

// Token: 0x02000116 RID: 278
public class Follow : MonoBehaviour
{
	// Token: 0x060005C3 RID: 1475 RVA: 0x0001E3D0 File Offset: 0x0001C5D0
	private void Start()
	{
		this.lockedPosition = base.transform.position;
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x0001E3E4 File Offset: 0x0001C5E4
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

	// Token: 0x040007E6 RID: 2022
	public Transform target;

	// Token: 0x040007E7 RID: 2023
	public Vector3 targetLocalPosition = Vector3.zero;

	// Token: 0x040007E8 RID: 2024
	[ConditionalHide("lockToTarget", true, Inverse = true)]
	public float allowance = 1.5f;

	// Token: 0x040007E9 RID: 2025
	[ConditionalHide("lockToTarget", true, Inverse = true)]
	public float speedMultiplier = 1f;

	// Token: 0x040007EA RID: 2026
	private Vector3 velocity;

	// Token: 0x040007EB RID: 2027
	public bool lockToTarget;

	// Token: 0x040007EC RID: 2028
	public bool lockY;

	// Token: 0x040007ED RID: 2029
	private Vector3 lockedPosition;

	// Token: 0x040007EE RID: 2030
	public bool lockToGrid;

	// Token: 0x040007EF RID: 2031
	[ConditionalHide("lockToGrid", true)]
	public float gridSize = 0.625f;

	// Token: 0x040007F0 RID: 2032
	public bool clampMaxDistance;

	// Token: 0x040007F1 RID: 2033
	[ConditionalHide("clampMaxDistance", true)]
	public float maxDistance = 500f;
}
