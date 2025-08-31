using System;
using UnityEngine;

public class Ach_StickyComboTracker : MonoBehaviour
{
	// Token: 0x060001B8 RID: 440 RVA: 0x00009AC6 File Offset: 0x00007CC6
	public static void Stick()
	{
		if (Ach_StickyComboTracker.instance == null)
		{
			return;
		}
		Ach_StickyComboTracker.instance.OnStick();
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x00009AE0 File Offset: 0x00007CE0
	private void Awake()
	{
		Ach_StickyComboTracker.instance = this;
	}

	// Token: 0x060001BA RID: 442 RVA: 0x00009AE8 File Offset: 0x00007CE8
	private void OnDestroy()
	{
		if (Ach_StickyComboTracker.instance == this)
		{
			Ach_StickyComboTracker.instance = null;
		}
	}

	// Token: 0x060001BB RID: 443 RVA: 0x00009B00 File Offset: 0x00007D00
	private void FixedUpdate()
	{
		if ((Player.movement.IsGrounded && Player.movement.stepsSinceLastClimbing > 10) || Player.movement.IsSwimming || (Player.movement.HasGroundContact && Player.movement.isSledding))
		{
			this.invalidCounter++;
		}
		else
		{
			this.invalidCounter = 0;
		}
		if (this.invalidCounter > 10)
		{
			this.stickCombo = 0;
			base.enabled = false;
		}
	}

	// Token: 0x060001BC RID: 444 RVA: 0x00009B7A File Offset: 0x00007D7A
	public void OnStick()
	{
		this.invalidCounter = 0;
		this.stickCombo++;
		base.enabled = true;
		if (this.stickCombo >= this.requiredCombo)
		{
			this.achievement.UnlockAchievement();
		}
	}

	public static Ach_StickyComboTracker instance;

	public Achievement achievement;

	public int requiredCombo = 10;

	public int stickCombo;

	private int invalidCounter;
}
