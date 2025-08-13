using System;
using UnityEngine;

// Token: 0x020001FD RID: 509
public class ButtonTutorialUI : MonoBehaviour
{
	// Token: 0x06000972 RID: 2418 RVA: 0x00039C38 File Offset: 0x00037E38
	public void Activate()
	{
		if (this.allowRecentlyPressed && this.buttonTutorial.HasBeenPressedRecently)
		{
			return;
		}
		base.enabled = true;
		base.gameObject.SetActive(true);
		if (this.visualUI != null)
		{
			this.visualUI.Hide();
		}
		this.showTime = Time.time + (this.buttonTutorial.HasBeenPressed ? this.hasBeenPressedDelay : this.hasntBeenPressedDelay);
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x0000929E File Offset: 0x0000749E
	public void Hide()
	{
		base.gameObject.SetActive(false);
		base.enabled = false;
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x00039CB0 File Offset: 0x00037EB0
	private void Update()
	{
		if (Game.HasControl)
		{
			if (this.visualUI != null && this.visualUI.isHiding && Time.time >= this.showTime && Game.HasControl)
			{
				this.visualUI.Show();
			}
			if (this.buttonTutorial.HasBeenPressedRecently)
			{
				if (this.visualUI != null)
				{
					this.visualUI.Hide();
				}
				base.enabled = false;
				return;
			}
		}
		else if (!this.visualUI.isHiding)
		{
			this.visualUI.Hide();
		}
	}

	// Token: 0x04000C1E RID: 3102
	public ButtonTutorial buttonTutorial;

	// Token: 0x04000C1F RID: 3103
	public UIHideBehavior visualUI;

	// Token: 0x04000C20 RID: 3104
	public float hasBeenPressedDelay = 10f;

	// Token: 0x04000C21 RID: 3105
	public float hasntBeenPressedDelay = 1f;

	// Token: 0x04000C22 RID: 3106
	public bool allowRecentlyPressed;

	// Token: 0x04000C23 RID: 3107
	private float showTime = -1f;
}
