using System;
using UnityEngine;

// Token: 0x020003C7 RID: 967
public class UIScaleFollowWithTarget : MonoBehaviour
{
	// Token: 0x06001285 RID: 4741 RVA: 0x0000FB3E File Offset: 0x0000DD3E
	private void Awake()
	{
		this.highlight = Object.FindObjectOfType<HighlightsFX>();
		this.UpdateFollowTarget();
	}

	// Token: 0x06001286 RID: 4742 RVA: 0x0000FB51 File Offset: 0x0000DD51
	private void Update()
	{
		if (this.followTarget != this.follow.followTarget)
		{
			this.UpdateFollowTarget();
		}
	}

	// Token: 0x06001287 RID: 4743 RVA: 0x0005B4A8 File Offset: 0x000596A8
	private void UpdateFollowTarget()
	{
		this.followTarget = this.follow.followTarget;
		Bounds bounds;
		bounds..ctor(this.followTarget.transform.position, 0.01f * Vector3.one);
		foreach (Renderer renderer in this.highlight.objectRenderers)
		{
			Bounds bounds2 = renderer.bounds;
			if (renderer is SkinnedMeshRenderer)
			{
				bounds2.extents = new Vector3(0.5f * bounds2.extents.x, bounds2.extents.y, 0.5f * bounds2.extents.z);
			}
			bounds.Encapsulate(bounds2);
		}
		bounds.center - this.followTarget.position;
		this.follow.offset = bounds.center - this.followTarget.position;
		Vector3 extents = bounds.extents;
		this.follow.localOffset = new Vector2(Mathf.Max(extents.x, extents.z), -extents.y);
	}

	// Token: 0x040017E4 RID: 6116
	private HighlightsFX highlight;

	// Token: 0x040017E5 RID: 6117
	private Transform followTarget;

	// Token: 0x040017E6 RID: 6118
	public UIFollow follow;

	// Token: 0x040017E7 RID: 6119
	private Vector3 offset;
}
