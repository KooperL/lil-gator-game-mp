using System;
using UnityEngine;

public class QuestMarker : MonoBehaviour
{
	// Token: 0x06000BA0 RID: 2976 RVA: 0x00038A50 File Offset: 0x00036C50
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

	// Token: 0x06000BA1 RID: 2977 RVA: 0x00038AA4 File Offset: 0x00036CA4
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

	// Token: 0x06000BA2 RID: 2978 RVA: 0x00038AFC File Offset: 0x00036CFC
	private void OnDisable()
	{
		if (this.isUIActivated)
		{
			this.DeactivateUI();
		}
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x00038B0C File Offset: 0x00036D0C
	private void OnTriggerExit(Collider other)
	{
		if (this.isUIActivated)
		{
			this.DeactivateUI();
		}
		base.enabled = false;
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x00038B23 File Offset: 0x00036D23
	private void OnTriggerEnter(Collider other)
	{
		this.stepsSinceTriggered = 0;
		base.enabled = true;
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x00038B33 File Offset: 0x00036D33
	private void OnTriggerStay(Collider other)
	{
		this.stepsSinceTriggered = 0;
		base.enabled = true;
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x00038B44 File Offset: 0x00036D44
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

	// Token: 0x06000BA7 RID: 2983 RVA: 0x00038BB7 File Offset: 0x00036DB7
	private void DeactivateUI()
	{
		this.isUIActivated = false;
		Object.Destroy(this.currentUIElement);
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x00038BCB File Offset: 0x00036DCB
	private void ActivateUI()
	{
		this.isUIActivated = true;
		this.currentUIElement = Object.Instantiate<GameObject>(this.uiElementPrefab, this.uiElementParent);
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
