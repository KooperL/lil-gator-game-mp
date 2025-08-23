using System;
using UnityEngine;

public static class MathUtils
{
	// Token: 0x06000A43 RID: 2627 RVA: 0x00009D41 File Offset: 0x00007F41
	public static Quaternion ShortestRotation(Quaternion a, Quaternion b)
	{
		if (Quaternion.Dot(a, b) < 0f)
		{
			return a * Quaternion.Inverse(MathUtils.Multiply(b, -1f));
		}
		return a * Quaternion.Inverse(b);
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x00009D74 File Offset: 0x00007F74
	public static Quaternion Multiply(Quaternion input, float scalar)
	{
		return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x00009D9B File Offset: 0x00007F9B
	public static float SmoothDampAngleAcc(float current, float target, ref float currentVelocity, float smoothTime, float acceleration)
	{
		return MathUtils.SmoothDampAngleAcc(current, target, ref currentVelocity, smoothTime, acceleration, Time.deltaTime);
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x0003CBFC File Offset: 0x0003ADFC
	public static float SmoothDampAngleAcc(float current, float target, ref float currentVelocity, float smoothTime, float acceleration, float deltaTime)
	{
		float num = currentVelocity;
		float num2 = Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime);
		if (Mathf.Abs(currentVelocity - num) > deltaTime * acceleration)
		{
			currentVelocity = Mathf.MoveTowards(num, currentVelocity, deltaTime * acceleration);
			num2 = current + deltaTime * currentVelocity;
		}
		return num2;
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x0003CC40 File Offset: 0x0003AE40
	public static Vector3 SmoothDampAcc(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float acceleration)
	{
		Vector3 vector = currentVelocity;
		Vector3 vector2 = Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime);
		if (Vector3.Magnitude(currentVelocity - vector) > Time.deltaTime * acceleration)
		{
			currentVelocity = Vector3.MoveTowards(vector, currentVelocity, Time.deltaTime * acceleration);
			vector2 = current + Time.deltaTime * currentVelocity;
		}
		return vector2;
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x00009DAD File Offset: 0x00007FAD
	public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
	{
		target = current + Mathf.DeltaAngle(current, target);
		return MathUtils.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x0003CCAC File Offset: 0x0003AEAC
	public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
	{
		smoothTime = Mathf.Max(0.0001f, smoothTime);
		float num = 2f / smoothTime;
		float num2 = num * deltaTime;
		float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
		float num4 = current - target;
		float num5 = maxSpeed * smoothTime;
		num4 = Mathf.Clamp(num4, -num5, num5);
		float num6 = (currentVelocity + num * num4) * deltaTime;
		currentVelocity = (currentVelocity - num * num6) * num3;
		return current - num4 + (num4 + num6) * num3;
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x0003CD30 File Offset: 0x0003AF30
	public static Vector3 SlerpFlat(Vector3 from, Vector3 to, float t, bool perfectMagnitudes = false)
	{
		from.Normalize();
		to.Normalize();
		Vector3 vector = Quaternion.AngleAxis(0.5f * Vector3.SignedAngle(from, to, Vector3.up), Vector3.up) * from;
		vector.y = Mathf.Lerp(from.y, to.y, 0.5f);
		if (t < 0.5f)
		{
			return Vector3.Slerp(from, vector, t * 2f);
		}
		return Vector3.Slerp(vector, to, t * 2f - 1f);
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x0003CDB8 File Offset: 0x0003AFB8
	public static Quaternion SlerpFlat(Quaternion from, Quaternion to, float t)
	{
		Quaternion quaternion = Quaternion.Slerp(from, to, 0.5f);
		Vector3 vector = quaternion * Vector3.forward;
		vector.y = Mathf.Lerp((from * Vector3.forward).y, (to * Vector3.forward).y, 0.5f);
		quaternion = Quaternion.LookRotation(vector);
		if (t < 0.5f)
		{
			return Quaternion.Slerp(from, quaternion, t * 2f);
		}
		return Quaternion.Slerp(quaternion, to, t * 2f - 1f);
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x00009DC7 File Offset: 0x00007FC7
	public static Vector3 ClosestAlongLine(Vector3 point, Vector3 a, Vector3 b)
	{
		return Vector3.Lerp(a, b, MathUtils.InverseLerp(point, a, b));
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x0003CE44 File Offset: 0x0003B044
	public static float InverseLerp(Vector3 point, Vector3 a, Vector3 b)
	{
		Vector3 vector = point - a;
		Vector3 vector2 = b - a;
		return Mathf.Clamp01(Vector3.Dot(vector2, vector) / (vector2.magnitude * vector2.magnitude));
	}
}
