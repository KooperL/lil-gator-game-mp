using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020003D7 RID: 983
public class UISubMenu : MonoBehaviour
{
	// Token: 0x060012E7 RID: 4839 RVA: 0x00010025 File Offset: 0x0000E225
	private void OnValidate()
	{
		this.transformDepth = base.transform.GetDepth();
	}

	// Token: 0x060012E8 RID: 4840 RVA: 0x00010038 File Offset: 0x0000E238
	private void Awake()
	{
		this.checkCancel = base.GetComponent<ICheckCancel>();
	}

	// Token: 0x060012E9 RID: 4841 RVA: 0x00010046 File Offset: 0x0000E246
	private void OnEnable()
	{
		UIRootMenu.u.AddMenu(this);
		if (!this.isActivated)
		{
			this.Activate();
		}
	}

	// Token: 0x060012EA RID: 4842 RVA: 0x00010061 File Offset: 0x0000E261
	private void OnDisable()
	{
		UIRootMenu.u.RemoveMenu(this);
		if (this.isActivated)
		{
			this.Deactivate();
		}
	}

	// Token: 0x060012EB RID: 4843 RVA: 0x0001007C File Offset: 0x0000E27C
	public void Activate()
	{
		this.isActivated = true;
		if (this.prominentObject != null)
		{
			this.prominentObject.SetActive(true);
		}
		base.gameObject.SetActive(true);
		this.onActivate.Invoke();
	}

	// Token: 0x060012EC RID: 4844 RVA: 0x000100B6 File Offset: 0x0000E2B6
	public void Deactivate()
	{
		this.isActivated = false;
		this.onDeactivate.Invoke();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060012ED RID: 4845 RVA: 0x000100D6 File Offset: 0x0000E2D6
	public void OnLoseProminence()
	{
		if (this.prominentObject != null)
		{
			this.prominentObject.SetActive(false);
		}
		this.onLoseProminence.Invoke();
	}

	// Token: 0x060012EE RID: 4846 RVA: 0x000100FD File Offset: 0x0000E2FD
	public void OnRegainProminence()
	{
		if (this.prominentObject != null)
		{
			this.prominentObject.SetActive(true);
		}
		this.onRegainProminence.Invoke();
	}

	// Token: 0x060012EF RID: 4847 RVA: 0x00010124 File Offset: 0x0000E324
	public void OnCancel()
	{
		if (this.deactivateOnCancel && (this.checkCancel == null || this.checkCancel.TryCancel()))
		{
			this.Deactivate();
		}
		this.onCancel.Invoke();
	}

	// Token: 0x04001863 RID: 6243
	[ReadOnly]
	public int transformDepth;

	// Token: 0x04001864 RID: 6244
	public UnityEvent onActivate;

	// Token: 0x04001865 RID: 6245
	public UnityEvent onDeactivate;

	// Token: 0x04001866 RID: 6246
	public GameObject prominentObject;

	// Token: 0x04001867 RID: 6247
	public UnityEvent onLoseProminence;

	// Token: 0x04001868 RID: 6248
	public UnityEvent onRegainProminence;

	// Token: 0x04001869 RID: 6249
	public bool deactivateOnCancel = true;

	// Token: 0x0400186A RID: 6250
	public UnityEvent onCancel;

	// Token: 0x0400186B RID: 6251
	public bool isActivated;

	// Token: 0x0400186C RID: 6252
	private ICheckCancel checkCancel;
}
