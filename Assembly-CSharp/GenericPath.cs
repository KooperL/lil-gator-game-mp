using System;
using UnityEngine;

// Token: 0x02000184 RID: 388
public class GenericPath : MonoBehaviour
{
	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x06000747 RID: 1863 RVA: 0x00007582 File Offset: 0x00005782
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

	// Token: 0x06000748 RID: 1864 RVA: 0x00033A68 File Offset: 0x00031C68
	private void GenerateBounds()
	{
		this.generatedBounds = true;
		this.bounds = new Bounds(this.positions[0], Vector3.one);
		for (int i = 1; i < this.positions.Length; i++)
		{
			this.bounds.Encapsulate(this.positions[i]);
		}
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x00033AC4 File Offset: 0x00031CC4
	public float SqrDistance(Vector3 point)
	{
		return this.Bounds.SqrDistance(base.transform.InverseTransformPoint(point));
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void UpdatePath()
	{
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x00007598 File Offset: 0x00005798
	protected Vector3 GetPosition(int i)
	{
		return base.transform.TransformPoint(this.GetPositionLocal(i));
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x000075AC File Offset: 0x000057AC
	protected Vector3 GetPositionLocal(int i)
	{
		i = Mathf.Clamp(i, 0, this.positions.Length - 1);
		return this.positions[i];
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x000075CD File Offset: 0x000057CD
	public Vector3 GetPosition(float t)
	{
		return base.transform.TransformPoint(this.GetPositionLocal(t));
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x000075E1 File Offset: 0x000057E1
	public Vector3 GetPositionLocal(float t)
	{
		t = Mathf.Clamp(t / (float)(this.positions.Length - 1), 1E-08f, 1f);
		return iTween.PointOnPath(this.positions, t);
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x00033AEC File Offset: 0x00031CEC
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

	// Token: 0x06000750 RID: 1872 RVA: 0x00033B4C File Offset: 0x00031D4C
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

	// Token: 0x06000751 RID: 1873 RVA: 0x0000760D File Offset: 0x0000580D
	public float GetClosestInterpolated(Vector3 position)
	{
		return this.GetClosestInterpolated(position, this.GetClosest(position));
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x00033BAC File Offset: 0x00031DAC
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

	// Token: 0x06000753 RID: 1875 RVA: 0x0000761D File Offset: 0x0000581D
	public Vector3 GetDirection(int closest)
	{
		return this.GetDirection((float)closest);
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x00033CA0 File Offset: 0x00031EA0
	public Vector3 GetDirection(float interpolatedPosition)
	{
		return (this.GetPosition(interpolatedPosition + 0.01f) - this.GetPosition(interpolatedPosition - 0.01f)).normalized;
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x00007627 File Offset: 0x00005827
	public Vector3 ClosestPointOnPath(Vector3 point)
	{
		return this.GetPosition(this.GetClosestInterpolated(point));
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x00007636 File Offset: 0x00005836
	public float DistanceToPath(Vector3 point)
	{
		return Vector3.Distance(this.ClosestPointOnPath(point), point);
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x00033CD4 File Offset: 0x00031ED4
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

	// Token: 0x040009C5 RID: 2501
	public Vector3[] positions;

	// Token: 0x040009C6 RID: 2502
	private bool generatedBounds;

	// Token: 0x040009C7 RID: 2503
	private Bounds bounds;

	// Token: 0x040009C8 RID: 2504
	private const float speedStepProbe = 0.01f;
}
