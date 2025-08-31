using System;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class UITabNavigation : MonoBehaviour
{
	// Token: 0x06000FC5 RID: 4037 RVA: 0x0004B73B File Offset: 0x0004993B
	private void OnValidate()
	{
		if (this.tabs == null || this.tabs.Length < base.transform.childCount)
		{
			this.tabs = base.GetComponentsInChildren<Toggle>();
		}
	}

	// Token: 0x06000FC6 RID: 4038 RVA: 0x0004B768 File Offset: 0x00049968
	private void OnEnable()
	{
		if (this.rePlayer == null)
		{
			this.rePlayer = ReInput.players.GetPlayer(0);
		}
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnTabRight), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("UITabRight"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnTabLeft), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("UITabLeft"));
	}

	// Token: 0x06000FC7 RID: 4039 RVA: 0x0004B7DE File Offset: 0x000499DE
	private void OnDisable()
	{
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnTabRight));
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnTabLeft));
	}

	// Token: 0x06000FC8 RID: 4040 RVA: 0x0004B80E File Offset: 0x00049A0E
	private void OnTabLeft(InputActionEventData obj)
	{
		if (Time.time - UITabNavigation.bufferTime < 0.1f)
		{
			return;
		}
		this.OnTabLeft();
	}

	// Token: 0x06000FC9 RID: 4041 RVA: 0x0004B82C File Offset: 0x00049A2C
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

	// Token: 0x06000FCA RID: 4042 RVA: 0x0004B861 File Offset: 0x00049A61
	private void OnTabRight(InputActionEventData obj)
	{
		if (Time.time - UITabNavigation.bufferTime < 0.1f)
		{
			return;
		}
		this.OnTabRight();
	}

	// Token: 0x06000FCB RID: 4043 RVA: 0x0004B87C File Offset: 0x00049A7C
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

	// Token: 0x06000FCC RID: 4044 RVA: 0x0004B8B0 File Offset: 0x00049AB0
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
