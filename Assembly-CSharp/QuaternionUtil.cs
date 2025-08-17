using System;
using UnityEngine;

public static class QuaternionUtil
{
	// Token: 0x06000A41 RID: 2625 RVA: 0x0003C7DC File Offset: 0x0003A9DC
	public static Quaternion SmoothDamp(Quaternion rot, Quaternion target, ref Quaternion deriv, float time)
	{
		if (Time.deltaTime < Mathf.Epsilon)
		{
			return rot;
		}
		float num = ((Quaternion.Dot(rot, target) > 0f) ? 1f : (-1f));
		target.x *= num;
		target.y *= num;
		target.z *= num;
		target.w *= num;
		Vector4 normalized = new Vector4(Mathf.SmoothDamp(rot.x, target.x, ref deriv.x, time), Mathf.SmoothDamp(rot.y, target.y, ref deriv.y, time), Mathf.SmoothDamp(rot.z, target.z, ref deriv.z, time), Mathf.SmoothDamp(rot.w, target.w, ref deriv.w, time)).normalized;
		Vector4 vector = Vector4.Project(new Vector4(deriv.x, deriv.y, deriv.z, deriv.w), normalized);
		deriv.x -= vector.x;
		deriv.y -= vector.y;
		deriv.z -= vector.z;
		deriv.w -= vector.w;
		return new Quaternion(normalized.x, normalized.y, normalized.z, normalized.w);
	}
}
