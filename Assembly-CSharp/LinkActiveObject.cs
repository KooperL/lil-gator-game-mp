using System;
using UnityEngine;

public class LinkActiveObject : MonoBehaviour
{
	// Token: 0x060008C5 RID: 2245 RVA: 0x0000896F File Offset: 0x00006B6F
	private void OnEnable()
	{
		if (this != null && this.linkedObject != null)
		{
			this.linkedObject.SetActive(true);
		}
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x00008994 File Offset: 0x00006B94
	private void OnDisable()
	{
		if (this != null && this.linkedObject != null)
		{
			this.linkedObject.SetActive(false);
		}
	}

	public GameObject linkedObject;
}
