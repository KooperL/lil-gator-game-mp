using System;
using UnityEngine;

public class UIReticle : MonoBehaviour
{
	// Token: 0x060012CC RID: 4812 RVA: 0x0000FD4F File Offset: 0x0000DF4F
	public void OnValidate()
	{
		if (this.animator == null)
		{
			this.animator = base.GetComponent<Animator>();
		}
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x0000FD6B File Offset: 0x0000DF6B
	private void OnDisable()
	{
		if (this.isAiming)
		{
			this.isAiming = false;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060012CE RID: 4814 RVA: 0x0000FD88 File Offset: 0x0000DF88
	public void StartAiming(float chargeTime)
	{
		base.gameObject.SetActive(true);
		this.isAiming = true;
		this.animator.SetFloat(this.speedID, 1f / chargeTime);
		this.animator.SetBool(this.showID, true);
	}

	// Token: 0x060012CF RID: 4815 RVA: 0x0000FDC7 File Offset: 0x0000DFC7
	public void StopAiming()
	{
		if (this.isAiming)
		{
			this.isAiming = false;
			this.animator.SetBool(this.showID, false);
		}
	}

	// Token: 0x060012D0 RID: 4816 RVA: 0x0000FDEA File Offset: 0x0000DFEA
	public void Update()
	{
		if (this.isAiming && !Player.itemManager.IsAiming)
		{
			this.StopAiming();
		}
	}

	// Token: 0x060012D1 RID: 4817 RVA: 0x0000968D File Offset: 0x0000788D
	public void Deactivate()
	{
		base.gameObject.SetActive(false);
	}

	public Animator animator;

	private int speedID = Animator.StringToHash("Speed");

	private int startID = Animator.StringToHash("Start");

	private int stopID = Animator.StringToHash("Stop");

	private int showID = Animator.StringToHash("Show");

	public bool isAiming;
}
