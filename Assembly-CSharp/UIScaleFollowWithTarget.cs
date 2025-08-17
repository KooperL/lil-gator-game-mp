using System;
using UnityEngine;

public class UIScaleFollowWithTarget : MonoBehaviour
{
	// Token: 0x060012E5 RID: 4837 RVA: 0x0000FF34 File Offset: 0x0000E134
	private void Awake()
	{
		this.highlight = global::UnityEngine.Object.FindObjectOfType<HighlightsFX>();
		this.UpdateFollowTarget();
	}

	// Token: 0x060012E6 RID: 4838 RVA: 0x0000FF47 File Offset: 0x0000E147
	private void Update()
	{
		if (this.followTarget != this.follow.followTarget)
		{
			this.UpdateFollowTarget();
		}
	}

	// Token: 0x060012E7 RID: 4839 RVA: 0x0005D458 File Offset: 0x0005B658
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
