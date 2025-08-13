using System;
using UnityEngine;

// Token: 0x020001A4 RID: 420
public static class PhysUtil
{
	// Token: 0x0600089A RID: 2202 RVA: 0x000289C4 File Offset: 0x00026BC4
	private static float SolveTimeQuadratic(float a, float b, float c)
	{
		if (b * b - 4f * a * c < 0f)
		{
			return 0f;
		}
		float num = (-b + Mathf.Sqrt(b * b - 4f * a * c)) / (2f * a);
		float num2 = (-b - Mathf.Sqrt(b * b - 4f * a * c)) / (2f * a);
		if (num >= 0f && (num < num2 || num2 < 0f))
		{
			return num;
		}
		if (num2 >= 0f)
		{
			return num2;
		}
		return 0f;
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x00028A4C File Offset: 0x00026C4C
	public static float SolveTimeUntilLanding(float velocity, float deltaY)
	{
		return PhysUtil.SolveTimeQuadratic(0.5f * Physics.gravity.y, velocity, deltaY);
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x00028A68 File Offset: 0x00026C68
	public static float SolveProjectileAngle(Vector2 delta, float speed, float gravityFactor = 1f)
	{
		float num = -1f * gravityFactor * Physics.gravity.y;
		float num2 = Mathf.Pow(speed, 4f) - num * (num * delta.x * delta.x + 2f * delta.y * speed * speed);
		if (num2 < 0f)
		{
			return 0f;
		}
		float num3 = Mathf.Atan((speed * speed + Mathf.Sqrt(num2)) / (num * delta.x));
		float num4 = Mathf.Atan((speed * speed - Mathf.Sqrt(num2)) / (num * delta.x));
		if (Mathf.Abs(num3) < Mathf.Abs(num4))
		{
			return num3;
		}
		return num4;
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x00028B08 File Offset: 0x00026D08
	public static bool SolveProjectileVelocity(Vector3 delta, float speed, out Vector3 velocity, float gravityFactor = 1f)
	{
		velocity = Vector3.zero;
		Vector3 vector = new Vector3(delta.x, 0f, delta.z);
		Vector3 normalized = vector.normalized;
		float num = PhysUtil.SolveProjectileAngle(new Vector2(vector.magnitude, delta.y), speed, gravityFactor);
		if (num == 0f)
		{
			return false;
		}
		Vector3 vector2;
		if (num > 0f)
		{
			vector2 = Vector3.RotateTowards(normalized, Vector3.up, num, 1f);
		}
		else
		{
			vector2 = Vector3.RotateTowards(normalized, Vector3.down, -num, 1f);
		}
		velocity = speed * vector2;
		return true;
	}
}
