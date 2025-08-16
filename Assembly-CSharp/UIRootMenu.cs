using System;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.Events;

public class UIRootMenu : MonoBehaviour
{
	// Token: 0x060012D3 RID: 4819 RVA: 0x0000FE06 File Offset: 0x0000E006
	private void Awake()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		UIRootMenu.u = this;
	}

	// Token: 0x060012D4 RID: 4820 RVA: 0x0005D0A0 File Offset: 0x0005B2A0
	private void OnEnable()
	{
		UIRootMenu.u = this;
		if (this.rePlayer == null)
		{
			this.rePlayer = ReInput.players.GetPlayer(0);
		}
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnCancelInput), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("UICancel"));
	}

	// Token: 0x060012D5 RID: 4821 RVA: 0x0000FE1F File Offset: 0x0000E01F
	private void OnCancelInput(InputActionEventData obj)
	{
		this.OnCancel();
	}

	// Token: 0x060012D6 RID: 4822 RVA: 0x0000FE27 File Offset: 0x0000E027
	private void OnDisable()
	{
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnCancelInput));
	}

	// Token: 0x060012D7 RID: 4823 RVA: 0x0005D0F4 File Offset: 0x0005B2F4
	public void AddMenu(UISubMenu newMenu)
	{
		if (this.menuStack.Count == 0)
		{
			this.OnLeaveRoot();
		}
		int transformDepth = newMenu.transformDepth;
		for (int i = 0; i < this.menuStack.Count; i++)
		{
			if (transformDepth < this.menuStack[i].transformDepth)
			{
				this.menuStack.Insert(i, newMenu);
				return;
			}
		}
		if (this.menuStack.Count > 0)
		{
			this.menuStack[this.menuStack.Count - 1].OnLoseProminence();
		}
		this.menuStack.Add(newMenu);
	}

	// Token: 0x060012D8 RID: 4824 RVA: 0x0005D18C File Offset: 0x0005B38C
	public void RemoveMenu(UISubMenu oldMenu)
	{
		if (this.menuStack.Contains(oldMenu))
		{
			if (this.menuStack.Count > 1 && this.menuStack.IndexOf(oldMenu) == this.menuStack.Count - 1)
			{
				this.menuStack[this.menuStack.Count - 2].OnRegainProminence();
			}
			this.menuStack.Remove(oldMenu);
			if (this.menuStack.Count == 0)
			{
				this.OnReturnToRoot();
			}
		}
	}

	// Token: 0x060012D9 RID: 4825 RVA: 0x0005D210 File Offset: 0x0005B410
	public void OnCancel()
	{
		if (UIRootMenu.lastInputTime == Time.time)
		{
			return;
		}
		if (this.menuStack != null && this.menuStack.Count > 0)
		{
			UIRootMenu.lastInputTime = Time.time;
			this.menuStack[this.menuStack.Count - 1].OnCancel();
		}
	}

	// Token: 0x060012DA RID: 4826 RVA: 0x0000FE40 File Offset: 0x0000E040
	public void OnLeaveRoot()
	{
		if (this.rootObject != null)
		{
			this.rootObject.SetActive(false);
		}
	}

	// Token: 0x060012DB RID: 4827 RVA: 0x0000FE5C File Offset: 0x0000E05C
	public void OnReturnToRoot()
	{
		if (this.rootObject != null)
		{
			this.rootObject.SetActive(true);
		}
		this.onReturnToRoot.Invoke();
	}

	public static UIRootMenu u;

	public static float lastInputTime = -1f;

	public GameObject rootObject;

	public UnityEvent onReturnToRoot;

	public List<UISubMenu> menuStack = new List<UISubMenu>();

	private global::Rewired.Player rePlayer;
}
