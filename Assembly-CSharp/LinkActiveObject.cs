using System;
using UnityEngine;

public class LinkActiveObject : MonoBehaviour
{
	// Token: 0x060008C4 RID: 2244 RVA: 0x00008950 File Offset: 0x00006B50
	private void OnEnable()
	{
		if (this != null && this.linkedObject != null)
		{
			this.linkedObject.SetActive(true);
		}
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x00008975 File Offset: 0x00006B75
	private void OnDisable()
	{
		if (this != null && this.linkedObject != null)
		{
			this.linkedObject.SetActive(false);
		}
	}

	public GameObject linkedObject;
}
