using System;
using UnityEngine;

public class Follow : MonoBehaviour
{
	// Token: 0x06000727 RID: 1831 RVA: 0x000073D1 File Offset: 0x000055D1
	private void Start()
	{
		this.lockedPosition = base.transform.position;
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x00033CEC File Offset: 0x00031EEC
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
