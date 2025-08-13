using System;
using UnityEngine;

// Token: 0x0200030C RID: 780
[ExecuteInEditMode]
public class StretchFitString : MonoBehaviour
{
	// Token: 0x06000F5C RID: 3932 RVA: 0x000504E8 File Offset: 0x0004E6E8
	private void OnEnable()
	{
		if (this.attachTargetToPlayer)
		{
			StretchFitString.PlayerAttachmentPoint playerAttachmentPoint = this.playerAttachmentPoint;
			if (playerAttachmentPoint == StretchFitString.PlayerAttachmentPoint.Hand)
			{
				this.target = Player.itemManager.leftHandAnchor;
				return;
			}
			if (playerAttachmentPoint != StretchFitString.PlayerAttachmentPoint.Hip)
			{
				return;
			}
			this.target = Player.itemManager.hipAnchor;
		}
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x0000D5F1 File Offset: 0x0000B7F1
	private void Start()
	{
		if (base.gameObject.isStatic)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000F5E RID: 3934 RVA: 0x00050530 File Offset: 0x0004E730
	private void LateUpdate()
	{
		if (this.anchor != null && this.target != null)
		{
			Vector3 position = this.anchor.position;
			Vector3 position2 = this.target.position;
			base.transform.position = Vector3.Lerp(position, position2, 0.5f);
			Vector3 vector = position2 - position;
			base.transform.rotation = Quaternion.FromToRotation(Vector3.forward, vector);
			base.transform.localScale = new Vector3(1f, 1f, base.transform.parent.InverseTransformVector(vector).magnitude);
		}
	}

	// Token: 0x040013DB RID: 5083
	public Transform anchor;

	// Token: 0x040013DC RID: 5084
	public Transform target;

	// Token: 0x040013DD RID: 5085
	public bool attachTargetToPlayer;

	// Token: 0x040013DE RID: 5086
	[ConditionalHide("attachTargetToPlayer", true)]
	public StretchFitString.PlayerAttachmentPoint playerAttachmentPoint;

	// Token: 0x0200030D RID: 781
	public enum PlayerAttachmentPoint
	{
		// Token: 0x040013E0 RID: 5088
		Hand,
		// Token: 0x040013E1 RID: 5089
		Hip
	}
}
