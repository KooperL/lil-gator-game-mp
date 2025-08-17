using System;
using UnityEngine;

public class LinkActiveObject : MonoBehaviour
{
	// Token: 0x060008C4 RID: 2244 RVA: 0x00008965 File Offset: 0x00006B65
	private void OnEnable()
	{
		if (this != null && this.linkedObject != null)
		{
			this.linkedObject.SetActive(true);
		}
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x0000898A File Offset: 0x00006B8A
	private void OnDisable()
	{
		if (this != null && this.linkedObject != null)
		{
			this.linkedObject.SetActive(false);
		}
	}

	public GameObject linkedObject;
}
