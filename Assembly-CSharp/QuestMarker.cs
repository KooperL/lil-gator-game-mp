using System;
using UnityEngine;

public class QuestMarker : MonoBehaviour
{
	// Token: 0x06000E41 RID: 3649 RVA: 0x0004CFB4 File Offset: 0x0004B1B4
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

	// Token: 0x06000E42 RID: 3650 RVA: 0x0004D008 File Offset: 0x0004B208
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

	// Token: 0x06000E43 RID: 3651 RVA: 0x0000CAD5 File Offset: 0x0000ACD5
	private void OnDisable()
	{
		if (this.isUIActivated)
		{
			this.DeactivateUI();
		}
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x0000CAE5 File Offset: 0x0000ACE5
	private void OnTriggerExit(Collider other)
	{
		if (this.isUIActivated)
		{
			this.DeactivateUI();
		}
		base.enabled = false;
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x0000CAFC File Offset: 0x0000ACFC
	private void OnTriggerEnter(Collider other)
	{
		this.stepsSinceTriggered = 0;
		base.enabled = true;
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x0000CAFC File Offset: 0x0000ACFC
	private void OnTriggerStay(Collider other)
	{
		this.stepsSinceTriggered = 0;
		base.enabled = true;
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x0004D060 File Offset: 0x0004B260
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

	// Token: 0x06000E48 RID: 3656 RVA: 0x0000CB0C File Offset: 0x0000AD0C
	private void DeactivateUI()
	{
		this.isUIActivated = false;
		global::UnityEngine.Object.Destroy(this.currentUIElement);
	}

	// Token: 0x06000E49 RID: 3657 RVA: 0x0000CB20 File Offset: 0x0000AD20
	private void ActivateUI()
	{
		this.isUIActivated = true;
		this.currentUIElement = global::UnityEngine.Object.Instantiate<GameObject>(this.uiElementPrefab, this.uiElementParent);
		this.currentUIElement.GetComponent<UIFollow>().followTarget = base.transform;
	}

	public static Transform globalUiElementParent;

	public DialogueActor attachedActor;

	public GameObject uiElementPrefab;

	public Transform uiElementParent;

	public GameObject currentUIElement;

	private bool isUIActivated;

	private int stepsSinceTriggered;
}
