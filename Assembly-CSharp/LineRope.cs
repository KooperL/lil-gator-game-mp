using System;
using UnityEngine;

public class LineRope : MonoBehaviour
{
	// Token: 0x0600012E RID: 302 RVA: 0x0001BA34 File Offset: 0x00019C34
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

	// Token: 0x0600012F RID: 303 RVA: 0x000030B0 File Offset: 0x000012B0
	private void LateUpdate()
	{
		this.UpdateLineRenderer();
	}

	// Token: 0x06000130 RID: 304 RVA: 0x0001BAB0 File Offset: 0x00019CB0
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

	public LineRope.Attachment attachment;

	private Transform attachmentTransform;

	private LineRenderer lineRenderer;

	private Vector3[] positions;

	public enum Attachment
	{
		Hand,
		Hip
	}
}
