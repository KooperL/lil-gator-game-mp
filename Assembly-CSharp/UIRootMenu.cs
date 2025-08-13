using System;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020003C5 RID: 965
public class UIRootMenu : MonoBehaviour
{
	// Token: 0x06001273 RID: 4723 RVA: 0x0000FA25 File Offset: 0x0000DC25
	private void Awake()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		UIRootMenu.u = this;
	}

	// Token: 0x06001274 RID: 4724 RVA: 0x0005B284 File Offset: 0x00059484
	private void OnEnable()
	{
		UIRootMenu.u = this;
		if (this.rePlayer == null)
		{
			this.rePlayer = ReInput.players.GetPlayer(0);
		}
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnCancelInput), 0, 3, ReInput.mapping.GetActionId("UICancel"));
	}

	// Token: 0x06001275 RID: 4725 RVA: 0x0000FA3E File Offset: 0x0000DC3E
	private void OnCancelInput(InputActionEventData obj)
	{
		this.OnCancel();
	}

	// Token: 0x06001276 RID: 4726 RVA: 0x0000FA46 File Offset: 0x0000DC46
	private void OnDisable()
	{
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnCancelInput));
	}

	// Token: 0x06001277 RID: 4727 RVA: 0x0005B2D8 File Offset: 0x000594D8
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

	// Token: 0x06001278 RID: 4728 RVA: 0x0005B370 File Offset: 0x00059570
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

	// Token: 0x06001279 RID: 4729 RVA: 0x0005B3F4 File Offset: 0x000595F4
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

	// Token: 0x0600127A RID: 4730 RVA: 0x0000FA5F File Offset: 0x0000DC5F
	public void OnLeaveRoot()
	{
		if (this.rootObject != null)
		{
			this.rootObject.SetActive(false);
		}
	}

	// Token: 0x0600127B RID: 4731 RVA: 0x0000FA7B File Offset: 0x0000DC7B
	public void OnReturnToRoot()
	{
		if (this.rootObject != null)
		{
			this.rootObject.SetActive(true);
		}
		this.onReturnToRoot.Invoke();
	}

	// Token: 0x040017DC RID: 6108
	public static UIRootMenu u;

	// Token: 0x040017DD RID: 6109
	public static float lastInputTime = -1f;

	// Token: 0x040017DE RID: 6110
	public GameObject rootObject;

	// Token: 0x040017DF RID: 6111
	public UnityEvent onReturnToRoot;

	// Token: 0x040017E0 RID: 6112
	public List<UISubMenu> menuStack = new List<UISubMenu>();

	// Token: 0x040017E1 RID: 6113
	private Player rePlayer;
}
