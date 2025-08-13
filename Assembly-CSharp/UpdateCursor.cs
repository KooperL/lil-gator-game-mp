using System;
using Rewired;
using UnityEngine;

// Token: 0x020001FA RID: 506
public class UpdateCursor : MonoBehaviour
{
	// Token: 0x06000B02 RID: 2818 RVA: 0x00037170 File Offset: 0x00035370
	private void Start()
	{
		this.isApplicationFocused = true;
		this.isApplicationPaused = false;
		if (this.rePlayer == null)
		{
			this.rePlayer = ReInput.players.GetPlayer(0);
		}
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMouseClick), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("OnMouseClick"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMouseClick), UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, ReInput.mapping.GetActionId("OnMouseClick"));
		this.UpdateCursorState();
	}

	// Token: 0x06000B03 RID: 2819 RVA: 0x000371FA File Offset: 0x000353FA
	private void OnDestroy()
	{
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x000371FC File Offset: 0x000353FC
	private void Update()
	{
		this.UpdateCursorState();
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x00037204 File Offset: 0x00035404
	private void OnApplicationFocus(bool focus)
	{
		this.isApplicationFocused = focus;
		this.UpdateCursorState();
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x00037213 File Offset: 0x00035413
	private void OnApplicationPause(bool pause)
	{
		this.isApplicationPaused = pause;
		this.UpdateCursorState();
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x00037222 File Offset: 0x00035422
	private void OnApplicationQuit()
	{
		this.SetCursorState(false);
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x0003722C File Offset: 0x0003542C
	private void UpdateCursorState()
	{
		bool flag = true;
		if (UIMouseUnlock.unlockDepth > 0)
		{
			flag = false;
		}
		if (!this.isApplicationFocused || this.isApplicationPaused)
		{
			flag = false;
		}
		this.SetCursorState(flag);
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x0003725E File Offset: 0x0003545E
	private void SetCursorState(bool isLocked)
	{
		UpdateCursor.isCurrentlyLocked = isLocked;
		if (!isLocked)
		{
			this.isActuallyLocked = false;
		}
		Cursor.lockState = (isLocked ? (this.isActuallyLocked ? CursorLockMode.Locked : CursorLockMode.Confined) : CursorLockMode.None);
		Cursor.visible = !isLocked;
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x00037290 File Offset: 0x00035490
	private void OnMouseClick(InputActionEventData obj)
	{
		if (UpdateCursor.isCurrentlyLocked && !this.isActuallyLocked)
		{
			this.isActuallyLocked = true;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	// Token: 0x04000EB0 RID: 3760
	public static bool isCurrentlyLocked;

	// Token: 0x04000EB1 RID: 3761
	private bool isActuallyLocked;

	// Token: 0x04000EB2 RID: 3762
	private bool isApplicationFocused;

	// Token: 0x04000EB3 RID: 3763
	private bool isApplicationPaused;

	// Token: 0x04000EB4 RID: 3764
	private global::Rewired.Player rePlayer;
}
