using System;
using UnityEngine;

public class GenericPath : MonoBehaviour
{
	// (get) Token: 0x06000622 RID: 1570 RVA: 0x0001FF34 File Offset: 0x0001E134
	public Bounds Bounds
	{
		get
		{
			if (!this.generatedBounds)
			{
				this.GenerateBounds();
			}
			return this.bounds;
		}
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0001FF4C File Offset: 0x0001E14C
	private void GenerateBounds()
	{
		this.generatedBounds = true;
		this.bounds = new Bounds(this.positions[0], Vector3.one);
		for (int i = 1; i < this.positions.Length; i++)
		{
			this.bounds.Encapsulate(this.positions[i]);
		}
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x0001FFA8 File Offset: 0x0001E1A8
	public float SqrDistance(Vector3 point)
	{
		return this.Bounds.SqrDistance(base.transform.InverseTransformPoint(point));
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x0001FFCF File Offset: 0x0001E1CF
	public virtual void UpdatePath()
	{
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x0001FFD1 File Offset: 0x0001E1D1
	protected Vector3 GetPosition(int i)
	{
		return base.transform.TransformPoint(this.GetPositionLocal(i));
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x0001FFE5 File Offset: 0x0001E1E5
	protected Vector3 GetPositionLocal(int i)
	{
		i = Mathf.Clamp(i, 0, this.positions.Length - 1);
		return this.positions[i];
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x00020006 File Offset: 0x0001E206
	public Vector3 GetPosition(float t)
	{
		return base.transform.TransformPoint(this.GetPositionLocal(t));
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x0002001A File Offset: 0x0001E21A
	public Vector3 GetPositionLocal(float t)
	{
		t = Mathf.Clamp(t / (float)(this.positions.Length - 1), 1E-08f, 1f);
		return iTween.PointOnPath(this.positions, t);
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x00020048 File Offset: 0x0001E248
	[ContextMenu("Snap to ground")]
	public void SnapToGround()
	{
		for (int i = 0; i < this.positions.Length; i++)
		{
			Vector3 vector = base.transform.TransformPoint(this.positions[i]);
			if (LayerUtil.SnapToGround(ref vector, 5f))
			{
				this.positions[i] = base.transform.InverseTransformPoint(vector);
			}
		}
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x000200A8 File Offset: 0x0001E2A8
	public int GetClosest(Vector3 position)
	{
		position = base.transform.InverseTransformPoint(position);
		int num = 0;
		float num2 = Vector3.SqrMagnitude(position - this.GetPositionLocal(0));
		for (int i = 1; i < this.positions.Length; i++)
		{
			float num3 = Vector3.SqrMagnitude(position - this.GetPositionLocal(i));
			if (num3 < num2)
			{
				num2 = num3;
				num = i;
			}
		}
		return num;
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x00020107 File Offset: 0x0001E307
	public float GetClosestInterpolated(Vector3 position)
	{
		return this.GetClosestInterpolated(position, this.GetClosest(position));
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x00020118 File Offset: 0x0001E318
	public float GetClosestInterpolated(Vector3 position, int closest)
	{
		int num;
		Vector3 vector;
		Vector3 vector2;
		if (closest == 0)
		{
			num = 0;
			int num2 = 1;
			vector = this.GetPosition(num);
			vector2 = this.GetPosition(num2);
		}
		else if (closest == this.positions.Length - 1)
		{
			num = this.positions.Length - 2;
			int num2 = this.positions.Length - 1;
			vector = this.GetPosition(num);
			vector2 = this.GetPosition(num2);
		}
		else
		{
			int num3 = closest - 1;
			int num4 = closest + 1;
			Vector3 position2 = this.GetPosition(num3);
			Vector3 position3 = this.GetPosition(num4);
			if (Vector3.SqrMagnitude(position - position3) > Vector3.SqrMagnitude(position - position2))
			{
				num = num3;
				vector = position2;
				vector2 = this.GetPosition(closest);
			}
			else
			{
				num = closest;
				vector = this.GetPosition(closest);
				vector2 = position3;
			}
		}
		Vector3 vector3 = position - vector;
		Vector3 vector4 = vector2 - vector;
		float num5 = Mathf.Clamp01(Vector3.Dot(vector4, vector3) / (vector4.magnitude * vector4.magnitude));
		return (float)num + num5;
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x00020209 File Offset: 0x0001E409
	public Vector3 GetDirection(int closest)
	{
		return this.GetDirection((float)closest);
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x00020214 File Offset: 0x0001E414
	public Vector3 GetDirection(float interpolatedPosition)
	{
		return (this.GetPosition(interpolatedPosition + 0.01f) - this.GetPosition(interpolatedPosition - 0.01f)).normalized;
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x00020248 File Offset: 0x0001E448
	public Vector3 ClosestPointOnPath(Vector3 point)
	{
		return this.GetPosition(this.GetClosestInterpolated(point));
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x00020257 File Offset: 0x0001E457
	public float DistanceToPath(Vector3 point)
	{
		return Vector3.Distance(this.ClosestPointOnPath(point), point);
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x00020268 File Offset: 0x0001E468
	public float MoveAlongPath(float t, float distance)
	{
		if (distance == 0f)
		{
			return t;
		}
		if (t == 0f && distance < 0f)
		{
			return t;
		}
		if (t == (float)(this.positions.Length - 1) && distance > 0f)
		{
			return t;
		}
		Vector3 position = this.GetPosition(t);
		Vector3 position2 = this.GetPosition(t + Mathf.Sign(distance) * 0.01f);
		float num = Vector3.Distance(position, position2);
		if (num == 0f)
		{
			return t;
		}
		float num2 = 0.01f / num;
		return Mathf.Clamp(t + distance * num2, 0f, (float)(this.positions.Length - 1));
	}

	public Vector3[] positions;

	private bool generatedBounds;

	private Bounds bounds;

	private const float speedStepProbe = 0.01f;
}
