using System;
using UnityEngine;

public class UIScaleFollowWithTarget : MonoBehaviour
{
	// Token: 0x06000F71 RID: 3953 RVA: 0x0004A0B1 File Offset: 0x000482B1
	private void Awake()
	{
		this.highlight = Object.FindObjectOfType<HighlightsFX>();
		this.UpdateFollowTarget();
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x0004A0C4 File Offset: 0x000482C4
	private void Update()
	{
		if (this.followTarget != this.follow.followTarget)
		{
			this.UpdateFollowTarget();
		}
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x0004A0E4 File Offset: 0x000482E4
	private void UpdateFollowTarget()
	{
		this.followTarget = this.follow.followTarget;
		Bounds bounds = new Bounds(this.followTarget.transform.position, 0.01f * Vector3.one);
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

	private HighlightsFX highlight;

	private Transform followTarget;

	public UIFollow follow;

	private Vector3 offset;
}
