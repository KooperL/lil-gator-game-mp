using System;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.Events;

public class UIRootMenu : MonoBehaviour
{
	// Token: 0x060012D4 RID: 4820 RVA: 0x0000FE25 File Offset: 0x0000E025
	private void Awake()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		UIRootMenu.u = this;
	}

	// Token: 0x060012D5 RID: 4821 RVA: 0x0005D4FC File Offset: 0x0005B6FC
	private void OnEnable()
	{
		UIRootMenu.u = this;
		if (this.rePlayer == null)
		{
			this.rePlayer = ReInput.players.GetPlayer(0);
		}
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnCancelInput), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("UICancel"));
	}

	// Token: 0x060012D6 RID: 4822 RVA: 0x0000FE3E File Offset: 0x0000E03E
	private void OnCancelInput(InputActionEventData obj)
	{
		this.OnCancel();
	}

	// Token: 0x060012D7 RID: 4823 RVA: 0x0000FE46 File Offset: 0x0000E046
	private void OnDisable()
	{
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnCancelInput));
	}

	// Token: 0x060012D8 RID: 4824 RVA: 0x0005D550 File Offset: 0x0005B750
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

	// Token: 0x060012D9 RID: 4825 RVA: 0x0005D5E8 File Offset: 0x0005B7E8
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

	// Token: 0x060012DA RID: 4826 RVA: 0x0005D66C File Offset: 0x0005B86C
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

	// Token: 0x060012DB RID: 4827 RVA: 0x0000FE5F File Offset: 0x0000E05F
	public void OnLeaveRoot()
	{
		if (this.rootObject != null)
		{
			this.rootObject.SetActive(false);
		}
	}

	// Token: 0x060012DC RID: 4828 RVA: 0x0000FE7B File Offset: 0x0000E07B
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
