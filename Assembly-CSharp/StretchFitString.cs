using System;
using UnityEngine;

// Token: 0x02000248 RID: 584
[ExecuteInEditMode]
public class StretchFitString : MonoBehaviour
{
	// Token: 0x06000CB0 RID: 3248 RVA: 0x0003D6F0 File Offset: 0x0003B8F0
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

	// Token: 0x06000CB1 RID: 3249 RVA: 0x0003D735 File Offset: 0x0003B935
	private void Start()
	{
		if (base.gameObject.isStatic)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x0003D74C File Offset: 0x0003B94C
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

	// Token: 0x040010C5 RID: 4293
	public Transform anchor;

	// Token: 0x040010C6 RID: 4294
	public Transform target;

	// Token: 0x040010C7 RID: 4295
	public bool attachTargetToPlayer;

	// Token: 0x040010C8 RID: 4296
	[ConditionalHide("attachTargetToPlayer", true)]
	public StretchFitString.PlayerAttachmentPoint playerAttachmentPoint;

	// Token: 0x02000422 RID: 1058
	public enum PlayerAttachmentPoint
	{
		// Token: 0x04001D4A RID: 7498
		Hand,
		// Token: 0x04001D4B RID: 7499
		Hip
	}
}
