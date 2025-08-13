using System;
using UnityEngine;

// Token: 0x0200003F RID: 63
public class LineRope : MonoBehaviour
{
	// Token: 0x06000101 RID: 257 RVA: 0x00006A04 File Offset: 0x00004C04
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

	// Token: 0x06000102 RID: 258 RVA: 0x00006A7D File Offset: 0x00004C7D
	private void LateUpdate()
	{
		this.UpdateLineRenderer();
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00006A88 File Offset: 0x00004C88
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

	// Token: 0x0400016A RID: 362
	public LineRope.Attachment attachment;

	// Token: 0x0400016B RID: 363
	private Transform attachmentTransform;

	// Token: 0x0400016C RID: 364
	private LineRenderer lineRenderer;

	// Token: 0x0400016D RID: 365
	private Vector3[] positions;

	// Token: 0x02000361 RID: 865
	public enum Attachment
	{
		// Token: 0x04001A1A RID: 6682
		Hand,
		// Token: 0x04001A1B RID: 6683
		Hip
	}
}
