using System;
using UnityEngine;

public class Ach_StickyComboTracker : MonoBehaviour
{
	// Token: 0x060001FB RID: 507 RVA: 0x00003A10 File Offset: 0x00001C10
	public static void Stick()
	{
		if (Ach_StickyComboTracker.instance == null)
		{
			return;
		}
		Ach_StickyComboTracker.instance.OnStick();
	}

	// Token: 0x060001FC RID: 508 RVA: 0x00003A2A File Offset: 0x00001C2A
	private void Awake()
	{
		Ach_StickyComboTracker.instance = this;
	}

	// Token: 0x060001FD RID: 509 RVA: 0x00003A32 File Offset: 0x00001C32
	private void OnDestroy()
	{
		if (Ach_StickyComboTracker.instance == this)
		{
			Ach_StickyComboTracker.instance = null;
		}
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0001E4C0 File Offset: 0x0001C6C0
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

	// Token: 0x060001FF RID: 511 RVA: 0x00003A47 File Offset: 0x00001C47
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
