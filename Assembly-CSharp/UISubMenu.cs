using System;
using UnityEngine;
using UnityEngine.Events;

public class UISubMenu : MonoBehaviour
{
	// Token: 0x06000FBB RID: 4027 RVA: 0x0004B5FD File Offset: 0x000497FD
	private void OnValidate()
	{
		this.transformDepth = base.transform.GetDepth();
	}

	// Token: 0x06000FBC RID: 4028 RVA: 0x0004B610 File Offset: 0x00049810
	private void Awake()
	{
		this.checkCancel = base.GetComponent<ICheckCancel>();
	}

	// Token: 0x06000FBD RID: 4029 RVA: 0x0004B61E File Offset: 0x0004981E
	private void OnEnable()
	{
		UIRootMenu.u.AddMenu(this);
		if (!this.isActivated)
		{
			this.Activate();
		}
	}

	// Token: 0x06000FBE RID: 4030 RVA: 0x0004B639 File Offset: 0x00049839
	private void OnDisable()
	{
		UIRootMenu.u.RemoveMenu(this);
		if (this.isActivated)
		{
			this.Deactivate();
		}
	}

	// Token: 0x06000FBF RID: 4031 RVA: 0x0004B654 File Offset: 0x00049854
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

	// Token: 0x06000FC0 RID: 4032 RVA: 0x0004B68E File Offset: 0x0004988E
	public void Deactivate()
	{
		this.isActivated = false;
		this.onDeactivate.Invoke();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FC1 RID: 4033 RVA: 0x0004B6AE File Offset: 0x000498AE
	public void OnLoseProminence()
	{
		if (this.prominentObject != null)
		{
			this.prominentObject.SetActive(false);
		}
		this.onLoseProminence.Invoke();
	}

	// Token: 0x06000FC2 RID: 4034 RVA: 0x0004B6D5 File Offset: 0x000498D5
	public void OnRegainProminence()
	{
		if (this.prominentObject != null)
		{
			this.prominentObject.SetActive(true);
		}
		this.onRegainProminence.Invoke();
	}

	// Token: 0x06000FC3 RID: 4035 RVA: 0x0004B6FC File Offset: 0x000498FC
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
