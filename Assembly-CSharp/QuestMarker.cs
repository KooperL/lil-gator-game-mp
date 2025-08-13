using System;
using UnityEngine;

// Token: 0x020002C8 RID: 712
public class QuestMarker : MonoBehaviour
{
	// Token: 0x06000DF5 RID: 3573 RVA: 0x0004B42C File Offset: 0x0004962C
	private void OnValidate()
	{
		if (this.attachedActor == null)
		{
			this.attachedActor = base.GetComponentInParent<DialogueActor>();
		}
		if (this.uiElementParent == null)
		{
			GameObject gameObject = GameObject.Find("QuestMarkers");
			this.uiElementParent = ((gameObject != null) ? gameObject.transform : null);
		}
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x0004B480 File Offset: 0x00049680
	private void Awake()
	{
		if (this.uiElementParent == null)
		{
			if (QuestMarker.globalUiElementParent == null)
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag("QuestMarkers");
				QuestMarker.globalUiElementParent = (this.uiElementParent = ((gameObject != null) ? gameObject.transform : null));
				return;
			}
			this.uiElementParent = QuestMarker.globalUiElementParent;
		}
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x0000C7CD File Offset: 0x0000A9CD
	private void OnDisable()
	{
		if (this.isUIActivated)
		{
			this.DeactivateUI();
		}
	}

	// Token: 0x06000DF8 RID: 3576 RVA: 0x0000C7DD File Offset: 0x0000A9DD
	private void OnTriggerExit(Collider other)
	{
		if (this.isUIActivated)
		{
			this.DeactivateUI();
		}
		base.enabled = false;
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x0000C7F4 File Offset: 0x0000A9F4
	private void OnTriggerEnter(Collider other)
	{
		this.stepsSinceTriggered = 0;
		base.enabled = true;
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x0000C7F4 File Offset: 0x0000A9F4
	private void OnTriggerStay(Collider other)
	{
		this.stepsSinceTriggered = 0;
		base.enabled = true;
	}

	// Token: 0x06000DFB RID: 3579 RVA: 0x0004B4D8 File Offset: 0x000496D8
	private void FixedUpdate()
	{
		this.stepsSinceTriggered++;
		bool flag = true;
		if (this.stepsSinceTriggered > 5)
		{
			flag = false;
		}
		if (this.attachedActor != null && this.attachedActor.IsInDialogue)
		{
			flag = false;
		}
		if (!Game.HasControl)
		{
			flag = false;
		}
		if (this.isUIActivated && !flag)
		{
			this.DeactivateUI();
			return;
		}
		if (!this.isUIActivated && flag)
		{
			this.ActivateUI();
		}
	}

	// Token: 0x06000DFC RID: 3580 RVA: 0x0000C804 File Offset: 0x0000AA04
	private void DeactivateUI()
	{
		this.isUIActivated = false;
		Object.Destroy(this.currentUIElement);
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x0000C818 File Offset: 0x0000AA18
	private void ActivateUI()
	{
		this.isUIActivated = true;
		this.currentUIElement = Object.Instantiate<GameObject>(this.uiElementPrefab, this.uiElementParent);
		this.currentUIElement.GetComponent<UIFollow>().followTarget = base.transform;
	}

	// Token: 0x04001230 RID: 4656
	public static Transform globalUiElementParent;

	// Token: 0x04001231 RID: 4657
	public DialogueActor attachedActor;

	// Token: 0x04001232 RID: 4658
	public GameObject uiElementPrefab;

	// Token: 0x04001233 RID: 4659
	public Transform uiElementParent;

	// Token: 0x04001234 RID: 4660
	public GameObject currentUIElement;

	// Token: 0x04001235 RID: 4661
	private bool isUIActivated;

	// Token: 0x04001236 RID: 4662
	private int stepsSinceTriggered;
}
