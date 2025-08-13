using System;
using Rewired;
using RewiredConsts;
using UnityEngine;

// Token: 0x020003AC RID: 940
public class UIInteractButtonPrompt : MonoBehaviour
{
	// Token: 0x060011D3 RID: 4563 RVA: 0x0000F36D File Offset: 0x0000D56D
	private void Awake()
	{
		this.buttonDisplay = base.GetComponent<UIButtonDisplay>();
		this.buttonDisplay.updateAutomatically = false;
	}

	// Token: 0x060011D4 RID: 4564 RVA: 0x00059180 File Offset: 0x00057380
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

	// Token: 0x0400170B RID: 5899
	public static bool showPrompt = true;

	// Token: 0x0400170C RID: 5900
	private UIButtonDisplay buttonDisplay;

	// Token: 0x0400170D RID: 5901
	[ActionIdProperty(typeof(global::RewiredConsts.Action))]
	public int interact;

	// Token: 0x0400170E RID: 5902
	[ActionIdProperty(typeof(global::RewiredConsts.Action))]
	public int primary;

	// Token: 0x0400170F RID: 5903
	[ActionIdProperty(typeof(global::RewiredConsts.Action))]
	public int jump;
}
