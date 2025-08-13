using System;
using UnityEngine;

// Token: 0x02000158 RID: 344
public class LinkActiveObject : MonoBehaviour
{
	// Token: 0x0600073A RID: 1850 RVA: 0x000242DC File Offset: 0x000224DC
	private void OnEnable()
	{
		if (this != null && this.linkedObject != null)
		{
			this.linkedObject.SetActive(true);
		}
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x00024301 File Offset: 0x00022501
	private void OnDisable()
	{
		if (this != null && this.linkedObject != null)
		{
			this.linkedObject.SetActive(false);
		}
	}

	// Token: 0x0400097E RID: 2430
	public GameObject linkedObject;
}
