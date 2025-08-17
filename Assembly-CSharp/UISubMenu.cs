using System;
using UnityEngine;
using UnityEngine.Events;

public class UISubMenu : MonoBehaviour
{
	// Token: 0x06001347 RID: 4935 RVA: 0x00010422 File Offset: 0x0000E622
	private void OnValidate()
	{
		this.transformDepth = base.transform.GetDepth();
	}

	// Token: 0x06001348 RID: 4936 RVA: 0x00010435 File Offset: 0x0000E635
	private void Awake()
	{
		this.checkCancel = base.GetComponent<ICheckCancel>();
	}

	// Token: 0x06001349 RID: 4937 RVA: 0x00010443 File Offset: 0x0000E643
	private void OnEnable()
	{
		UIRootMenu.u.AddMenu(this);
		if (!this.isActivated)
		{
			this.Activate();
		}
	}

	// Token: 0x0600134A RID: 4938 RVA: 0x0001045E File Offset: 0x0000E65E
	private void OnDisable()
	{
		UIRootMenu.u.RemoveMenu(this);
		if (this.isActivated)
		{
			this.Deactivate();
		}
	}

	// Token: 0x0600134B RID: 4939 RVA: 0x00010479 File Offset: 0x0000E679
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

	// Token: 0x0600134C RID: 4940 RVA: 0x000104B3 File Offset: 0x0000E6B3
	public void Deactivate()
	{
		this.isActivated = false;
		this.onDeactivate.Invoke();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600134D RID: 4941 RVA: 0x000104D3 File Offset: 0x0000E6D3
	public void OnLoseProminence()
	{
		if (this.prominentObject != null)
		{
			this.prominentObject.SetActive(false);
		}
		this.onLoseProminence.Invoke();
	}

	// Token: 0x0600134E RID: 4942 RVA: 0x000104FA File Offset: 0x0000E6FA
	public void OnRegainProminence()
	{
		if (this.prominentObject != null)
		{
			this.prominentObject.SetActive(true);
		}
		this.onRegainProminence.Invoke();
	}

	// Token: 0x0600134F RID: 4943 RVA: 0x00010521 File Offset: 0x0000E721
	public void OnCancel()
	{
		if (this.deactivateOnCancel && (this.checkCancel == null || this.checkCancel.TryCancel()))
		{
			this.Deactivate();
		}
		this.onCancel.Invoke();
	}

	[ReadOnly]
	public int transformDepth;

	public UnityEvent onActivate;

	public UnityEvent onDeactivate;

	public GameObject prominentObject;

	public UnityEvent onLoseProminence;

	public UnityEvent onRegainProminence;

	public bool deactivateOnCancel = true;

	public UnityEvent onCancel;

	public bool isActivated;

	private ICheckCancel checkCancel;
}
