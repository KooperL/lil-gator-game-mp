using System;
using Rewired;
using RewiredConsts;
using UnityEngine;

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

	public static bool showPrompt = true;

	private UIButtonDisplay buttonDisplay;

	[ActionIdProperty(typeof(Action))]
	public int interact;

	[ActionIdProperty(typeof(Action))]
	public int primary;

	[ActionIdProperty(typeof(Action))]
	public int jump;
}
