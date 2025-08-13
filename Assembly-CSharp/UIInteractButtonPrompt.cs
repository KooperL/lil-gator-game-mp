using System;
using Rewired;
using RewiredConsts;
using UnityEngine;

// Token: 0x020002CA RID: 714
public class UIInteractButtonPrompt : MonoBehaviour
{
	// Token: 0x06000EFB RID: 3835 RVA: 0x00047F26 File Offset: 0x00046126
	private void Awake()
	{
		this.buttonDisplay = base.GetComponent<UIButtonDisplay>();
		this.buttonDisplay.updateAutomatically = false;
	}

	// Token: 0x06000EFC RID: 3836 RVA: 0x00047F40 File Offset: 0x00046140
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

	// Token: 0x04001395 RID: 5013
	public static bool showPrompt = true;

	// Token: 0x04001396 RID: 5014
	private UIButtonDisplay buttonDisplay;

	// Token: 0x04001397 RID: 5015
	[ActionIdProperty(typeof(Action))]
	public int interact;

	// Token: 0x04001398 RID: 5016
	[ActionIdProperty(typeof(Action))]
	public int primary;

	// Token: 0x04001399 RID: 5017
	[ActionIdProperty(typeof(Action))]
	public int jump;
}
