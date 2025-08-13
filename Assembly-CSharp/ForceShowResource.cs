using System;
using UnityEngine;

// Token: 0x02000173 RID: 371
public class ForceShowResource : MonoBehaviour
{
	// Token: 0x060006FE RID: 1790 RVA: 0x000071BD File Offset: 0x000053BD
	public void OnEnable()
	{
		if (!this.ignoreIfEmpty || this.resource.HasBeenCollected())
		{
			this.resource.ForceShow = true;
		}
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x000071E0 File Offset: 0x000053E0
	public void OnDisable()
	{
		this.resource.ForceShow = false;
	}

	// Token: 0x04000968 RID: 2408
	public bool ignoreIfEmpty;

	// Token: 0x04000969 RID: 2409
	public ItemResource resource;
}
