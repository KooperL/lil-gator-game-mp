using System;
using Rewired;
using UnityEngine;

public class UpdateCursor : MonoBehaviour
{
	// Token: 0x06000D0D RID: 3341 RVA: 0x00049D1C File Offset: 0x00047F1C
	private void Start()
	{
		this.isApplicationFocused = true;
		this.isApplicationPaused = false;
		if (this.rePlayer == null)
		{
			this.rePlayer = ReInput.players.GetPlayer(0);
		}
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMouseClick), 0, 3, ReInput.mapping.GetActionId("OnMouseClick"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMouseClick), 0, 4, ReInput.mapping.GetActionId("OnMouseClick"));
		this.UpdateCursorState();
	}

	// Token: 0x06000D0E RID: 3342 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDestroy()
	{
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x0000C1B1 File Offset: 0x0000A3B1
	private void Update()
	{
		this.UpdateCursorState();
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0000C1B9 File Offset: 0x0000A3B9
	private void OnApplicationFocus(bool focus)
	{
		this.isApplicationFocused = focus;
		this.UpdateCursorState();
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x0000C1C8 File Offset: 0x0000A3C8
	private void OnApplicationPause(bool pause)
	{
		this.isApplicationPaused = pause;
		this.UpdateCursorState();
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x0000C1D7 File Offset: 0x0000A3D7
	private void OnApplicationQuit()
	{
		this.SetCursorState(false);
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x00049DA8 File Offset: 0x00047FA8
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

	// Token: 0x06000D14 RID: 3348 RVA: 0x0000C1E0 File Offset: 0x0000A3E0
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

	// Token: 0x06000D15 RID: 3349 RVA: 0x0000C212 File Offset: 0x0000A412
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

	private Player rePlayer;
}
