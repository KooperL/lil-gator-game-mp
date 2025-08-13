using System;
using Rewired;
using UnityEngine;

// Token: 0x0200028B RID: 651
public class UpdateCursor : MonoBehaviour
{
	// Token: 0x06000CC1 RID: 3265 RVA: 0x00048194 File Offset: 0x00046394
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

	// Token: 0x06000CC2 RID: 3266 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDestroy()
	{
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x0000BEA9 File Offset: 0x0000A0A9
	private void Update()
	{
		this.UpdateCursorState();
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x0000BEB1 File Offset: 0x0000A0B1
	private void OnApplicationFocus(bool focus)
	{
		this.isApplicationFocused = focus;
		this.UpdateCursorState();
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
	private void OnApplicationPause(bool pause)
	{
		this.isApplicationPaused = pause;
		this.UpdateCursorState();
	}

	// Token: 0x06000CC6 RID: 3270 RVA: 0x0000BECF File Offset: 0x0000A0CF
	private void OnApplicationQuit()
	{
		this.SetCursorState(false);
	}

	// Token: 0x06000CC7 RID: 3271 RVA: 0x00048220 File Offset: 0x00046420
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

	// Token: 0x06000CC8 RID: 3272 RVA: 0x0000BED8 File Offset: 0x0000A0D8
	private void SetCursorState(bool isLocked)
	{
		UpdateCursor.isCurrentlyLocked = isLocked;
		if (!isLocked)
		{
			this.isActuallyLocked = false;
		}
		Cursor.lockState = (isLocked ? (this.isActuallyLocked ? 1 : 2) : 0);
		Cursor.visible = !isLocked;
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x0000BF0A File Offset: 0x0000A10A
	private void OnMouseClick(InputActionEventData obj)
	{
		if (UpdateCursor.isCurrentlyLocked && !this.isActuallyLocked)
		{
			this.isActuallyLocked = true;
			Cursor.lockState = 1;
		}
	}

	// Token: 0x0400110B RID: 4363
	public static bool isCurrentlyLocked;

	// Token: 0x0400110C RID: 4364
	private bool isActuallyLocked;

	// Token: 0x0400110D RID: 4365
	private bool isApplicationFocused;

	// Token: 0x0400110E RID: 4366
	private bool isApplicationPaused;

	// Token: 0x0400110F RID: 4367
	private Player rePlayer;
}
