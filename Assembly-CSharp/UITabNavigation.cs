using System;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class UITabNavigation : MonoBehaviour
{
	// Token: 0x06001351 RID: 4945 RVA: 0x0001056A File Offset: 0x0000E76A
	private void OnValidate()
	{
		if (this.tabs == null || this.tabs.Length < base.transform.childCount)
		{
			this.tabs = base.GetComponentsInChildren<Toggle>();
		}
	}

	// Token: 0x06001352 RID: 4946 RVA: 0x0005ED70 File Offset: 0x0005CF70
	private void OnEnable()
	{
		if (this.rePlayer == null)
		{
			this.rePlayer = ReInput.players.GetPlayer(0);
		}
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnTabRight), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("UITabRight"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnTabLeft), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("UITabLeft"));
	}

	// Token: 0x06001353 RID: 4947 RVA: 0x00010595 File Offset: 0x0000E795
	private void OnDisable()
	{
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnTabRight));
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnTabLeft));
	}

	// Token: 0x06001354 RID: 4948 RVA: 0x000105C5 File Offset: 0x0000E7C5
	private void OnTabLeft(InputActionEventData obj)
	{
		if (Time.time - UITabNavigation.bufferTime < 0.1f)
		{
			return;
		}
		this.OnTabLeft();
	}

	// Token: 0x06001355 RID: 4949 RVA: 0x0005EDE8 File Offset: 0x0005CFE8
	private void OnTabLeft()
	{
		int num = this.FindCurrentlySelectedTab();
		num--;
		if (num < 0)
		{
			num = this.tabs.Length - 1;
		}
		this.tabs[num].isOn = true;
	}

	// Token: 0x06001356 RID: 4950 RVA: 0x000105E0 File Offset: 0x0000E7E0
	private void OnTabRight(InputActionEventData obj)
	{
		if (Time.time - UITabNavigation.bufferTime < 0.1f)
		{
			return;
		}
		this.OnTabRight();
	}

	// Token: 0x06001357 RID: 4951 RVA: 0x0005EE20 File Offset: 0x0005D020
	private void OnTabRight()
	{
		int num = this.FindCurrentlySelectedTab();
		num++;
		if (num >= this.tabs.Length)
		{
			num = 0;
		}
		this.tabs[num].isOn = true;
	}

	// Token: 0x06001358 RID: 4952 RVA: 0x0005EE54 File Offset: 0x0005D054
	private int FindCurrentlySelectedTab()
	{
		for (int i = 0; i < this.tabs.Length; i++)
		{
			if (this.tabs[i].isOn)
			{
				return i;
			}
		}
		return 0;
	}

	public static float bufferTime = -1f;

	public Toggle[] tabs;

	private global::Rewired.Player rePlayer;
}
