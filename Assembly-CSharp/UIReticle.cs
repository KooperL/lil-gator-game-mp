using System;
using UnityEngine;

public class UIReticle : MonoBehaviour
{
	// Token: 0x06000F58 RID: 3928 RVA: 0x00049C4D File Offset: 0x00047E4D
	public void OnValidate()
	{
		if (this.animator == null)
		{
			this.animator = base.GetComponent<Animator>();
		}
	}

	// Token: 0x06000F59 RID: 3929 RVA: 0x00049C69 File Offset: 0x00047E69
	private void OnDisable()
	{
		if (this.isAiming)
		{
			this.isAiming = false;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000F5A RID: 3930 RVA: 0x00049C86 File Offset: 0x00047E86
	public void StartAiming(float chargeTime)
	{
		base.gameObject.SetActive(true);
		this.isAiming = true;
		this.animator.SetFloat(this.speedID, 1f / chargeTime);
		this.animator.SetBool(this.showID, true);
	}

	// Token: 0x06000F5B RID: 3931 RVA: 0x00049CC5 File Offset: 0x00047EC5
	public void StopAiming()
	{
		if (this.isAiming)
		{
			this.isAiming = false;
			this.animator.SetBool(this.showID, false);
		}
	}

	// Token: 0x06000F5C RID: 3932 RVA: 0x00049CE8 File Offset: 0x00047EE8
	public void Update()
	{
		if (this.isAiming && !Player.itemManager.IsAiming)
		{
			this.StopAiming();
		}
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x00049D04 File Offset: 0x00047F04
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
