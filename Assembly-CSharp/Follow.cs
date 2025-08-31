using System;
using UnityEngine;

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

	public Transform target;

	public Vector3 targetLocalPosition = Vector3.zero;

	[ConditionalHide("lockToTarget", true, Inverse = true)]
	public float allowance = 1.5f;

	[ConditionalHide("lockToTarget", true, Inverse = true)]
	public float speedMultiplier = 1f;

	private Vector3 velocity;

	public bool lockToTarget;

	public bool lockY;

	private Vector3 lockedPosition;

	public bool lockToGrid;

	[ConditionalHide("lockToGrid", true)]
	public float gridSize = 0.625f;

	public bool clampMaxDistance;

	[ConditionalHide("clampMaxDistance", true)]
	public float maxDistance = 500f;
}
