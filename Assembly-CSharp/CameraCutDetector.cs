using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraCutDetector : MonoBehaviour
{
	// Token: 0x06000240 RID: 576 RVA: 0x0000C340 File Offset: 0x0000A540
	private void Update()
	{
		Vector3 vector = base.transform.position;
		if (Vector3.Distance(vector, this.position) > 1f)
		{
			this.UpdateSubscribers();
		}
		this.position = vector;
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0000C37C File Offset: 0x0000A57C
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
