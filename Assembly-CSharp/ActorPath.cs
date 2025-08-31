using System;
using UnityEngine;

public class ActorPath : GenericPath
{
	// Token: 0x060001DF RID: 479 RVA: 0x0000A0EC File Offset: 0x000082EC
	public void Interpolate(ref float nodePosition, float speed, out Vector3 velocity, out Vector3 position, bool getAccurateDirection = false)
	{
		float num = Mathf.Ceil(nodePosition + 1E-05f);
		float num2 = speed * Time.deltaTime;
		Vector3 vector = this.DeltaBetweenNodes(Mathf.FloorToInt(nodePosition));
		float magnitude = vector.magnitude;
		Vector3 vector2 = vector / magnitude;
		velocity = speed * vector2;
		nodePosition += num2 / magnitude;
		if (nodePosition >= num)
		{
			if (num < (float)(this.positions.Length - 1))
			{
				nodePosition = num;
			}
			else if (this.connectEnds)
			{
				if (num < (float)this.positions.Length)
				{
					nodePosition = num;
				}
				else
				{
					nodePosition = 0f;
				}
			}
			else if (num < (float)(2 * (this.positions.Length - 1)))
			{
				nodePosition = num;
			}
			else
			{
				nodePosition = 0f;
			}
		}
		position = this.GetInterpolatedPosition(nodePosition);
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x0000A1AC File Offset: 0x000083AC
	public void AddDistance(ref float nodePosition, float distance)
	{
		float num = Mathf.Ceil(nodePosition + 1E-05f);
		Vector3 vector = this.DeltaBetweenNodes(Mathf.FloorToInt(nodePosition));
		float magnitude = vector.magnitude;
		vector / magnitude;
		nodePosition += distance / magnitude;
		bool flag = false;
		if (this.connectEnds)
		{
			if (nodePosition >= (float)this.positions.Length)
			{
				nodePosition -= (float)this.positions.Length;
				flag = true;
			}
		}
		else if (nodePosition >= (float)(2 * (this.positions.Length - 1)))
		{
			nodePosition -= Mathf.Floor(nodePosition);
			flag = true;
		}
		if (nodePosition > num || flag)
		{
			distance = (nodePosition - Mathf.Floor(nodePosition)) * magnitude;
			nodePosition = Mathf.Floor(nodePosition);
			this.AddDistance(ref nodePosition, distance);
		}
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x0000A260 File Offset: 0x00008460
	private Vector3 DeltaBetweenNodes(int node)
	{
		if (node < this.positions.Length - 1)
		{
			return base.GetPosition(node + 1) - base.GetPosition(node);
		}
		if (this.connectEnds)
		{
			return base.GetPosition(0) - base.GetPosition(node);
		}
		node = 2 * (this.positions.Length - 1) - node;
		return base.GetPosition(node - 1) - base.GetPosition(node);
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0000A2D4 File Offset: 0x000084D4
	public Vector3 GetInterpolatedPosition(float nodePosition)
	{
		int num = Mathf.FloorToInt(nodePosition);
		float num2 = nodePosition - (float)num;
		if (num < this.positions.Length - 1)
		{
			return base.transform.TransformPoint(iTween.PointOnPath(this.positions, nodePosition / (float)(this.positions.Length - 1)));
		}
		if (this.connectEnds)
		{
			return Vector3.Lerp(base.GetPosition(num), base.GetPosition(0), num2);
		}
		return base.transform.TransformPoint(iTween.PointOnPath(this.positions, 2f - nodePosition / (float)(this.positions.Length - 1)));
	}

	public bool connectEnds;
}
