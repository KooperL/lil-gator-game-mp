using System;
using UnityEngine;

public class CopyBoneTransforms : MonoBehaviour
{
	// Token: 0x0600001D RID: 29 RVA: 0x000179DC File Offset: 0x00015BDC
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

	public GameObject source;
}
