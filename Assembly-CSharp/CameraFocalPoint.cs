using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocalPoint : MonoBehaviour
{
	// Token: 0x0600029C RID: 668 RVA: 0x00004240 File Offset: 0x00002440
	public static bool HasActive()
	{
		return CameraFocalPoint.all.Count != 0;
	}

	// Token: 0x0600029D RID: 669 RVA: 0x0000424F File Offset: 0x0000244F
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

	// Token: 0x0600029E RID: 670 RVA: 0x00020D34 File Offset: 0x0001EF34
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

	// Token: 0x0600029F RID: 671 RVA: 0x00004286 File Offset: 0x00002486
	private void OnDisable()
	{
		CameraFocalPoint.all.Remove(this);
	}

	public static List<CameraFocalPoint> all = new List<CameraFocalPoint>();

	public int priority;
}
