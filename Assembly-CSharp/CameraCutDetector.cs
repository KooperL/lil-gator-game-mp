using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AE RID: 174
public class CameraCutDetector : MonoBehaviour
{
	// Token: 0x0600028A RID: 650 RVA: 0x0002023C File Offset: 0x0001E43C
	private void Update()
	{
		Vector3 vector = base.transform.position;
		if (Vector3.Distance(vector, this.position) > 1f)
		{
			this.UpdateSubscribers();
		}
		this.position = vector;
	}

	// Token: 0x0600028B RID: 651 RVA: 0x00020278 File Offset: 0x0001E478
	private void UpdateSubscribers()
	{
		foreach (ICameraCut cameraCut in CameraCutDetector.subscribers)
		{
			cameraCut.OnCameraCut();
		}
	}

	// Token: 0x04000395 RID: 917
	public static List<ICameraCut> subscribers = new List<ICameraCut>();

	// Token: 0x04000396 RID: 918
	private Vector3 position;

	// Token: 0x04000397 RID: 919
	private Quaternion rotation;
}
