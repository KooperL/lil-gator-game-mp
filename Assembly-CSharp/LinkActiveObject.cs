using System;
using UnityEngine;

// Token: 0x020001C1 RID: 449
public class LinkActiveObject : MonoBehaviour
{
	// Token: 0x06000884 RID: 2180 RVA: 0x00008648 File Offset: 0x00006848
	private void OnEnable()
	{
		if (this != null && this.linkedObject != null)
		{
			this.linkedObject.SetActive(true);
		}
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x0000866D File Offset: 0x0000686D
	private void OnDisable()
	{
		if (this != null && this.linkedObject != null)
		{
			this.linkedObject.SetActive(false);
		}
	}

	// Token: 0x04000B1C RID: 2844
	public GameObject linkedObject;
}
