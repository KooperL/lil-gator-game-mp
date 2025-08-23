using System;
using UnityEngine;

public class ButtonTutorialUI : MonoBehaviour
{
	// Token: 0x060009B9 RID: 2489 RVA: 0x0003B90C File Offset: 0x00039B0C
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

	// Token: 0x060009BA RID: 2490 RVA: 0x00009606 File Offset: 0x00007806
	public void Hide()
	{
		base.gameObject.SetActive(false);
		base.enabled = false;
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x0003B98C File Offset: 0x00039B8C
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

	public ButtonTutorial buttonTutorial;

	public UIHideBehavior visualUI;

	public float hasBeenPressedDelay = 10f;

	public float hasntBeenPressedDelay = 1f;

	public bool allowRecentlyPressed;

	private float showTime = -1f;
}
