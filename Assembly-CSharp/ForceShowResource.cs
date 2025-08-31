using System;
using UnityEngine;

public class ForceShowResource : MonoBehaviour
{
	// Token: 0x060005D8 RID: 1496 RVA: 0x0001EE8D File Offset: 0x0001D08D
	public void OnEnable()
	{
		if (!this.ignoreIfEmpty || this.resource.HasBeenCollected())
		{
			this.resource.ForceShow = true;
		}
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x0001EEB0 File Offset: 0x0001D0B0
	public void OnDisable()
	{
		this.resource.ForceShow = false;
	}

	public bool ignoreIfEmpty;

	public ItemResource resource;
}
