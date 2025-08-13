using System;
using UnityEngine;

// Token: 0x02000090 RID: 144
public class Ach_StickyComboTracker : MonoBehaviour
{
	// Token: 0x060001EE RID: 494 RVA: 0x00003924 File Offset: 0x00001B24
	public static void Stick()
	{
		if (Ach_StickyComboTracker.instance == null)
		{
			return;
		}
		Ach_StickyComboTracker.instance.OnStick();
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0000393E File Offset: 0x00001B3E
	private void Awake()
	{
		Ach_StickyComboTracker.instance = this;
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x00003946 File Offset: 0x00001B46
	private void OnDestroy()
	{
		if (Ach_StickyComboTracker.instance == this)
		{
			Ach_StickyComboTracker.instance = null;
		}
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x0001DAA0 File Offset: 0x0001BCA0
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

	// Token: 0x060001F2 RID: 498 RVA: 0x0000395B File Offset: 0x00001B5B
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

	// Token: 0x040002DD RID: 733
	public static Ach_StickyComboTracker instance;

	// Token: 0x040002DE RID: 734
	public Achievement achievement;

	// Token: 0x040002DF RID: 735
	public int requiredCombo = 10;

	// Token: 0x040002E0 RID: 736
	public int stickCombo;

	// Token: 0x040002E1 RID: 737
	private int invalidCounter;
}
