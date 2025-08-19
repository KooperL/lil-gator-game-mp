using System;
using Rewired;
using UnityEngine;

public class TestToggle : MonoBehaviour
{
	// Token: 0x060010B7 RID: 4279 RVA: 0x0000E4BC File Offset: 0x0000C6BC
	private void Start()
	{
		ReInput.players.GetPlayer(0).AddInputEventDelegate(new Action<InputActionEventData>(this.OnToggle), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("DebugToggle"));
		this.SetToggledState(0);
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x000563E4 File Offset: 0x000545E4
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

	// Token: 0x060010B9 RID: 4281 RVA: 0x00056418 File Offset: 0x00054618
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

	public GameObject[] toggleObjects;

	private int index;
}
