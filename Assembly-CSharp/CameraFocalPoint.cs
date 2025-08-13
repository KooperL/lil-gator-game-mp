using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B0 RID: 176
public class CameraFocalPoint : MonoBehaviour
{
	// Token: 0x0600028F RID: 655 RVA: 0x00004154 File Offset: 0x00002354
	public static bool HasActive()
	{
		return CameraFocalPoint.all.Count != 0;
	}

	// Token: 0x06000290 RID: 656 RVA: 0x00004163 File Offset: 0x00002363
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

	// Token: 0x06000291 RID: 657 RVA: 0x000202C8 File Offset: 0x0001E4C8
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

	// Token: 0x06000292 RID: 658 RVA: 0x0000419A File Offset: 0x0000239A
	private void OnDisable()
	{
		CameraFocalPoint.all.Remove(this);
	}

	// Token: 0x04000398 RID: 920
	public static List<CameraFocalPoint> all = new List<CameraFocalPoint>();

	// Token: 0x04000399 RID: 921
	public int priority;
}
