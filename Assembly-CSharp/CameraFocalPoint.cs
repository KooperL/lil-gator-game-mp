using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200008A RID: 138
public class CameraFocalPoint : MonoBehaviour
{
	// Token: 0x06000245 RID: 581 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
	public static bool HasActive()
	{
		return CameraFocalPoint.all.Count != 0;
	}

	// Token: 0x06000246 RID: 582 RVA: 0x0000C3EF File Offset: 0x0000A5EF
	public static bool GetActive(out Vector3 position)
	{
		if (CameraFocalPoint.all.Count == 0)
		{
			position = Vector3.zero;
			return false;
		}
		position = CameraFocalPoint.all[0].transform.position;
		return true;
	}

	// Token: 0x06000247 RID: 583 RVA: 0x0000C428 File Offset: 0x0000A628
	private void OnEnable()
	{
		int num = -1;
		for (int i = 0; i < CameraFocalPoint.all.Count; i++)
		{
			if (CameraFocalPoint.all[i].priority < this.priority)
			{
				num = i;
				break;
			}
		}
		if (num == -1)
		{
			CameraFocalPoint.all.Add(this);
		}
		else
		{
			CameraFocalPoint.all.Insert(num, this);
		}
		if ((num == 0 || CameraFocalPoint.all.Count == 1) && PlayerOrbitCamera.active != null)
		{
			PlayerOrbitCamera.active.ForceAutoCamera();
		}
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0000C4AC File Offset: 0x0000A6AC
	private void OnDisable()
	{
		CameraFocalPoint.all.Remove(this);
	}

	// Token: 0x040002FF RID: 767
	public static List<CameraFocalPoint> all = new List<CameraFocalPoint>();

	// Token: 0x04000300 RID: 768
	public int priority;
}
