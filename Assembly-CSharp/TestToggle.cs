using System;
using Rewired;
using UnityEngine;

public class TestToggle : MonoBehaviour
{
	// Token: 0x06000DA0 RID: 3488 RVA: 0x000420D8 File Offset: 0x000402D8
	private void Start()
	{
		ReInput.players.GetPlayer(0).AddInputEventDelegate(new Action<InputActionEventData>(this.OnToggle), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("DebugToggle"));
		this.SetToggledState(0);
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x00042110 File Offset: 0x00040310
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

	// Token: 0x06000DA2 RID: 3490 RVA: 0x00042144 File Offset: 0x00040344
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
