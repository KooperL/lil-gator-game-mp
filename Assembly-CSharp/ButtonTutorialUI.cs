using System;
using UnityEngine;

// Token: 0x02000187 RID: 391
public class ButtonTutorialUI : MonoBehaviour
{
	// Token: 0x06000808 RID: 2056 RVA: 0x00026B84 File Offset: 0x00024D84
	public void Activate()
	{
		if (this.allowRecentlyPressed && this.buttonTutorial.HasBeenPressedRecently)
		{
			return;
		}
		if (Game.IsNewGamePlus)
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

	// Token: 0x06000809 RID: 2057 RVA: 0x00026C02 File Offset: 0x00024E02
	public void Hide()
	{
		base.gameObject.SetActive(false);
		base.enabled = false;
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x00026C18 File Offset: 0x00024E18
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

	// Token: 0x04000A35 RID: 2613
	public ButtonTutorial buttonTutorial;

	// Token: 0x04000A36 RID: 2614
	public UIHideBehavior visualUI;

	// Token: 0x04000A37 RID: 2615
	public float hasBeenPressedDelay = 10f;

	// Token: 0x04000A38 RID: 2616
	public float hasntBeenPressedDelay = 1f;

	// Token: 0x04000A39 RID: 2617
	public bool allowRecentlyPressed;

	// Token: 0x04000A3A RID: 2618
	private float showTime = -1f;
}
