using System;
using Rewired;
using UnityEngine;

// Token: 0x0200034C RID: 844
public class TestToggle : MonoBehaviour
{
	// Token: 0x0600105C RID: 4188 RVA: 0x0000E149 File Offset: 0x0000C349
	private void Start()
	{
		ReInput.players.GetPlayer(0).AddInputEventDelegate(new Action<InputActionEventData>(this.OnToggle), 0, 3, ReInput.mapping.GetActionId("DebugToggle"));
		this.SetToggledState(0);
	}

	// Token: 0x0600105D RID: 4189 RVA: 0x000544E4 File Offset: 0x000526E4
	private void OnToggle(InputActionEventData obj)
	{
		if (!Game.HasControl)
		{
			return;
		}
		int num = this.index + 1;
		if (num >= this.toggleObjects.Length)
		{
			num = 0;
		}
		this.SetToggledState(num);
	}

	// Token: 0x0600105E RID: 4190 RVA: 0x00054518 File Offset: 0x00052718
	private void SetToggledState(int newIndex)
	{
		this.index = newIndex;
		for (int i = 0; i < this.toggleObjects.Length; i++)
		{
			if (this.toggleObjects[i] != null)
			{
				this.toggleObjects[i].SetActive(this.index == i);
			}
		}
	}

	// Token: 0x04001537 RID: 5431
	public GameObject[] toggleObjects;

	// Token: 0x04001538 RID: 5432
	private int index;
}
