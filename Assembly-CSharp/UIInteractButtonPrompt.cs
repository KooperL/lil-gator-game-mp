using System;
using Rewired;
using RewiredConsts;
using UnityEngine;

public class UIInteractButtonPrompt : MonoBehaviour
{
	// Token: 0x06001233 RID: 4659 RVA: 0x0000F760 File Offset: 0x0000D960
	private void Awake()
	{
		this.buttonDisplay = base.GetComponent<UIButtonDisplay>();
		this.buttonDisplay.updateAutomatically = false;
	}

	// Token: 0x06001234 RID: 4660 RVA: 0x0005B120 File Offset: 0x00059320
	private void OnEnable()
	{
		if (UIInteractButtonPrompt.showPrompt)
		{
			switch (PlayerInput.interactMapping)
			{
			case PlayerInput.InteractMapping.Self:
				this.buttonDisplay.action = this.interact;
				break;
			case PlayerInput.InteractMapping.Primary:
				this.buttonDisplay.action = this.primary;
				break;
			case PlayerInput.InteractMapping.Jump:
				this.buttonDisplay.action = this.jump;
				break;
			}
			this.buttonDisplay.enabled = true;
			this.buttonDisplay.UpdateButtonDisplay();
			return;
		}
		this.buttonDisplay.enabled = false;
		this.buttonDisplay.ClearButtonDisplay();
	}

	public static bool showPrompt = true;

	private UIButtonDisplay buttonDisplay;

	[ActionIdProperty(typeof(global::RewiredConsts.Action))]
	public int interact;

	[ActionIdProperty(typeof(global::RewiredConsts.Action))]
	public int primary;

	[ActionIdProperty(typeof(global::RewiredConsts.Action))]
	public int jump;
}
