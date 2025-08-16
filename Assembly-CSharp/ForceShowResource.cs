using System;
using UnityEngine;

public class ForceShowResource : MonoBehaviour
{
	// Token: 0x0600073C RID: 1852 RVA: 0x000074BE File Offset: 0x000056BE
	public void OnEnable()
	{
		if (!this.ignoreIfEmpty || this.resource.HasBeenCollected())
		{
			this.resource.ForceShow = true;
		}
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x000074E1 File Offset: 0x000056E1
	public void OnDisable()
	{
		this.resource.ForceShow = false;
	}

	public bool ignoreIfEmpty;

	public ItemResource resource;
}
