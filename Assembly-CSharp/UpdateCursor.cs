using System;
using Rewired;
using UnityEngine;

public class UpdateCursor : MonoBehaviour
{
	// Token: 0x06000D0D RID: 3341 RVA: 0x00049B88 File Offset: 0x00047D88
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

	// Token: 0x06000D0E RID: 3342 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDestroy()
	{
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x0000C19C File Offset: 0x0000A39C
	private void Update()
	{
		this.UpdateCursorState();
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0000C1A4 File Offset: 0x0000A3A4
	private void OnApplicationFocus(bool focus)
	{
		this.isApplicationFocused = focus;
		this.UpdateCursorState();
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x0000C1B3 File Offset: 0x0000A3B3
	private void OnApplicationPause(bool pause)
	{
		this.isApplicationPaused = pause;
		this.UpdateCursorState();
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x0000C1C2 File Offset: 0x0000A3C2
	private void OnApplicationQuit()
	{
		this.SetCursorState(false);
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x00049C14 File Offset: 0x00047E14
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

	// Token: 0x06000D14 RID: 3348 RVA: 0x0000C1CB File Offset: 0x0000A3CB
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

	// Token: 0x06000D15 RID: 3349 RVA: 0x0000C1FD File Offset: 0x0000A3FD
	private void OnMouseClick(InputActionEventData obj)
	{
		if (UpdateCursor.isCurrentlyLocked && !this.isActuallyLocked)
		{
			this.isActuallyLocked = true;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	public static bool isCurrentlyLocked;

	private bool isActuallyLocked;

	private bool isApplicationFocused;

	private bool isApplicationPaused;

	private global::Rewired.Player rePlayer;
}
