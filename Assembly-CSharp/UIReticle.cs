using System;
using UnityEngine;

// Token: 0x020003C4 RID: 964
public class UIReticle : MonoBehaviour
{
	// Token: 0x0600126C RID: 4716 RVA: 0x0000F96E File Offset: 0x0000DB6E
	public void OnValidate()
	{
		if (this.animator == null)
		{
			this.animator = base.GetComponent<Animator>();
		}
	}

	// Token: 0x0600126D RID: 4717 RVA: 0x0000F98A File Offset: 0x0000DB8A
	private void OnDisable()
	{
		if (this.isAiming)
		{
			this.isAiming = false;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600126E RID: 4718 RVA: 0x0000F9A7 File Offset: 0x0000DBA7
	public void StartAiming(float chargeTime)
	{
		base.gameObject.SetActive(true);
		this.isAiming = true;
		this.animator.SetFloat(this.speedID, 1f / chargeTime);
		this.animator.SetBool(this.showID, true);
	}

	// Token: 0x0600126F RID: 4719 RVA: 0x0000F9E6 File Offset: 0x0000DBE6
	public void StopAiming()
	{
		if (this.isAiming)
		{
			this.isAiming = false;
			this.animator.SetBool(this.showID, false);
		}
	}

	// Token: 0x06001270 RID: 4720 RVA: 0x0000FA09 File Offset: 0x0000DC09
	public void Update()
	{
		if (this.isAiming && !Player.itemManager.IsAiming)
		{
			this.StopAiming();
		}
	}

	// Token: 0x06001271 RID: 4721 RVA: 0x00009344 File Offset: 0x00007544
	public void Deactivate()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x040017D6 RID: 6102
	public Animator animator;

	// Token: 0x040017D7 RID: 6103
	private int speedID = Animator.StringToHash("Speed");

	// Token: 0x040017D8 RID: 6104
	private int startID = Animator.StringToHash("Start");

	// Token: 0x040017D9 RID: 6105
	private int stopID = Animator.StringToHash("Stop");

	// Token: 0x040017DA RID: 6106
	private int showID = Animator.StringToHash("Show");

	// Token: 0x040017DB RID: 6107
	public bool isAiming;
}
