using System;
using UnityEngine;

[ExecuteInEditMode]
public class StretchFitString : MonoBehaviour
{
	// Token: 0x06000FB9 RID: 4025 RVA: 0x000526D4 File Offset: 0x000508D4
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

	// Token: 0x06000FBA RID: 4026 RVA: 0x0000D9A3 File Offset: 0x0000BBA3
	private void Start()
	{
		if (base.gameObject.isStatic)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x0005271C File Offset: 0x0005091C
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

	public Transform anchor;

	public Transform target;

	public bool attachTargetToPlayer;

	[ConditionalHide("attachTargetToPlayer", true)]
	public StretchFitString.PlayerAttachmentPoint playerAttachmentPoint;

	public enum PlayerAttachmentPoint
	{
		Hand,
		Hip
	}
}
