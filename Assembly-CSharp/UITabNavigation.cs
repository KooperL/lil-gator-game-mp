using System;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003D8 RID: 984
public class UITabNavigation : MonoBehaviour
{
	// Token: 0x060012F1 RID: 4849 RVA: 0x00010163 File Offset: 0x0000E363
	private void OnValidate()
	{
		if (this.tabs == null || this.tabs.Length < base.transform.childCount)
		{
			this.tabs = base.GetComponentsInChildren<Toggle>();
		}
	}

	// Token: 0x060012F2 RID: 4850 RVA: 0x0005CD6C File Offset: 0x0005AF6C
	private void OnEnable()
	{
		if (this.rePlayer == null)
		{
			this.rePlayer = ReInput.players.GetPlayer(0);
		}
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnTabRight), 0, 3, ReInput.mapping.GetActionId("UITabRight"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnTabLeft), 0, 3, ReInput.mapping.GetActionId("UITabLeft"));
	}

	// Token: 0x060012F3 RID: 4851 RVA: 0x0001018E File Offset: 0x0000E38E
	private void OnDisable()
	{
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnTabRight));
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnTabLeft));
	}

	// Token: 0x060012F4 RID: 4852 RVA: 0x000101BE File Offset: 0x0000E3BE
	private void OnTabLeft(InputActionEventData obj)
	{
		if (Time.time - UITabNavigation.bufferTime < 0.1f)
		{
			return;
		}
		this.OnTabLeft();
	}

	// Token: 0x060012F5 RID: 4853 RVA: 0x0005CDE4 File Offset: 0x0005AFE4
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

	// Token: 0x060012F6 RID: 4854 RVA: 0x000101D9 File Offset: 0x0000E3D9
	private void OnTabRight(InputActionEventData obj)
	{
		if (Time.time - UITabNavigation.bufferTime < 0.1f)
		{
			return;
		}
		this.OnTabRight();
	}

	// Token: 0x060012F7 RID: 4855 RVA: 0x0005CE1C File Offset: 0x0005B01C
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

	// Token: 0x060012F8 RID: 4856 RVA: 0x0005CE50 File Offset: 0x0005B050
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

	// Token: 0x0400186D RID: 6253
	public static float bufferTime = -1f;

	// Token: 0x0400186E RID: 6254
	public Toggle[] tabs;

	// Token: 0x0400186F RID: 6255
	private Player rePlayer;
}
