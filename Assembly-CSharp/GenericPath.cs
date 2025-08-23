using System;
using UnityEngine;

public class GenericPath : MonoBehaviour
{
	// (get) Token: 0x06000788 RID: 1928 RVA: 0x00007891 File Offset: 0x00005A91
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

	// Token: 0x06000789 RID: 1929 RVA: 0x00035698 File Offset: 0x00033898
	private void GenerateBounds()
	{
		this.generatedBounds = true;
		this.bounds = new Bounds(this.positions[0], Vector3.one);
		for (int i = 1; i < this.positions.Length; i++)
		{
			this.bounds.Encapsulate(this.positions[i]);
		}
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x000356F4 File Offset: 0x000338F4
	public float SqrDistance(Vector3 point)
	{
		return this.Bounds.SqrDistance(base.transform.InverseTransformPoint(point));
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void UpdatePath()
	{
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x000078A7 File Offset: 0x00005AA7
	protected Vector3 GetPosition(int i)
	{
		return base.transform.TransformPoint(this.GetPositionLocal(i));
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x000078BB File Offset: 0x00005ABB
	protected Vector3 GetPositionLocal(int i)
	{
		i = Mathf.Clamp(i, 0, this.positions.Length - 1);
		return this.positions[i];
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x000078DC File Offset: 0x00005ADC
	public Vector3 GetPosition(float t)
	{
		return base.transform.TransformPoint(this.GetPositionLocal(t));
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x000078F0 File Offset: 0x00005AF0
	public Vector3 GetPositionLocal(float t)
	{
		t = Mathf.Clamp(t / (float)(this.positions.Length - 1), 1E-08f, 1f);
		return iTween.PointOnPath(this.positions, t);
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x0003571C File Offset: 0x0003391C
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

	// Token: 0x06000791 RID: 1937 RVA: 0x0003577C File Offset: 0x0003397C
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

	// Token: 0x06000792 RID: 1938 RVA: 0x0000791C File Offset: 0x00005B1C
	public float GetClosestInterpolated(Vector3 position)
	{
		return this.GetClosestInterpolated(position, this.GetClosest(position));
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x000357DC File Offset: 0x000339DC
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

	// Token: 0x06000794 RID: 1940 RVA: 0x0000792C File Offset: 0x00005B2C
	public Vector3 GetDirection(int closest)
	{
		return this.GetDirection((float)closest);
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x000358D0 File Offset: 0x00033AD0
	public Vector3 GetDirection(float interpolatedPosition)
	{
		return (this.GetPosition(interpolatedPosition + 0.01f) - this.GetPosition(interpolatedPosition - 0.01f)).normalized;
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00007936 File Offset: 0x00005B36
	public Vector3 ClosestPointOnPath(Vector3 point)
	{
		return this.GetPosition(this.GetClosestInterpolated(point));
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x00007945 File Offset: 0x00005B45
	public float DistanceToPath(Vector3 point)
	{
		return Vector3.Distance(this.ClosestPointOnPath(point), point);
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x00035904 File Offset: 0x00033B04
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
