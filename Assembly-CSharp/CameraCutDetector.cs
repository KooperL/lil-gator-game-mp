using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraCutDetector : MonoBehaviour
{
	// Token: 0x06000297 RID: 663 RVA: 0x00020C94 File Offset: 0x0001EE94
	private void Update()
	{
		Vector3 vector = base.transform.position;
		if (Vector3.Distance(vector, this.position) > 1f)
		{
			this.UpdateSubscribers();
		}
		this.position = vector;
	}

	// Token: 0x06000298 RID: 664 RVA: 0x00020CD0 File Offset: 0x0001EED0
	private void UpdateSubscribers()
	{
		foreach (ICameraCut cameraCut in CameraCutDetector.subscribers)
		{
			cameraCut.OnCameraCut();
		}
	}

	public static List<ICameraCut> subscribers = new List<ICameraCut>();

	private Vector3 position;

	private Quaternion rotation;
}
