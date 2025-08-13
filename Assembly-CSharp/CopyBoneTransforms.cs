using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class CopyBoneTransforms : MonoBehaviour
{
	// Token: 0x0600001D RID: 29 RVA: 0x000026C0 File Offset: 0x000008C0
	[ContextMenu("Apply Transforms From Source")]
	public void ApplyTransformsFromSource()
	{
		foreach (Transform transform in this.source.GetComponentsInChildren<Transform>())
		{
			Transform transform2 = base.transform.FindDeepChild(transform.gameObject.name);
			if (transform2 != null)
			{
				transform2.ApplyTransformLocal(transform);
			}
		}
	}

	// Token: 0x0400001B RID: 27
	public GameObject source;
}
