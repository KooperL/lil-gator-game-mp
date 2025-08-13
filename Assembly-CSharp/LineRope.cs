using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
public class LineRope : MonoBehaviour
{
	// Token: 0x06000126 RID: 294 RVA: 0x0001B244 File Offset: 0x00019444
	private void OnEnable()
	{
		this.lineRenderer = base.GetComponent<LineRenderer>();
		LineRope.Attachment attachment = this.attachment;
		if (attachment != LineRope.Attachment.Hand)
		{
			if (attachment == LineRope.Attachment.Hip)
			{
				this.attachmentTransform = Player.itemManager.hipAnchor;
			}
		}
		else
		{
			this.attachmentTransform = Player.itemManager.leftHandAnchor;
		}
		this.positions = new Vector3[this.lineRenderer.positionCount];
		this.lineRenderer.GetPositions(this.positions);
		this.UpdateLineRenderer();
	}

	// Token: 0x06000127 RID: 295 RVA: 0x0000300D File Offset: 0x0000120D
	private void LateUpdate()
	{
		this.UpdateLineRenderer();
	}

	// Token: 0x06000128 RID: 296 RVA: 0x0001B2C0 File Offset: 0x000194C0
	private void UpdateLineRenderer()
	{
		Vector3 position = base.transform.position;
		Vector3 position2 = this.attachmentTransform.position;
		for (int i = 0; i < this.positions.Length; i++)
		{
			float num = Mathf.InverseLerp(0f, (float)(this.positions.Length - 1), (float)i);
			this.positions[i] = Vector3.Lerp(position, position2, num);
		}
		this.lineRenderer.SetPositions(this.positions);
	}

	// Token: 0x040001B1 RID: 433
	public LineRope.Attachment attachment;

	// Token: 0x040001B2 RID: 434
	private Transform attachmentTransform;

	// Token: 0x040001B3 RID: 435
	private LineRenderer lineRenderer;

	// Token: 0x040001B4 RID: 436
	private Vector3[] positions;

	// Token: 0x02000052 RID: 82
	public enum Attachment
	{
		// Token: 0x040001B6 RID: 438
		Hand,
		// Token: 0x040001B7 RID: 439
		Hip
	}
}
