using System;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.Events;

public class UIRootMenu : MonoBehaviour
{
	// Token: 0x06000F5F RID: 3935 RVA: 0x00049D67 File Offset: 0x00047F67
	private void Awake()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		UIRootMenu.u = this;
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x00049D80 File Offset: 0x00047F80
	private void OnEnable()
	{
		UIRootMenu.u = this;
		if (this.rePlayer == null)
		{
			this.rePlayer = ReInput.players.GetPlayer(0);
		}
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnCancelInput), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("UICancel"));
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x00049DD4 File Offset: 0x00047FD4
	private void OnCancelInput(InputActionEventData obj)
	{
		this.OnCancel();
	}

	// Token: 0x06000F62 RID: 3938 RVA: 0x00049DDC File Offset: 0x00047FDC
	private void OnDisable()
	{
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnCancelInput));
	}

	// Token: 0x06000F63 RID: 3939 RVA: 0x00049DF8 File Offset: 0x00047FF8
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

	// Token: 0x06000F64 RID: 3940 RVA: 0x00049E90 File Offset: 0x00048090
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

	// Token: 0x06000F65 RID: 3941 RVA: 0x00049F14 File Offset: 0x00048114
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

	// Token: 0x06000F66 RID: 3942 RVA: 0x00049F6B File Offset: 0x0004816B
	public void OnLeaveRoot()
	{
		if (this.rootObject != null)
		{
			this.rootObject.SetActive(false);
		}
	}

	// Token: 0x06000F67 RID: 3943 RVA: 0x00049F87 File Offset: 0x00048187
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
