using System;
using UnityEngine;

// Token: 0x020002C5 RID: 709
public class UIHideBehavior : MonoBehaviour
{
	// Token: 0x06000EE8 RID: 3816 RVA: 0x00047A2E File Offset: 0x00045C2E
	public virtual void Show()
	{
		this.isHiding = false;
		base.gameObject.SetActive(true);
		if (this.autoHideDelay > 0f)
		{
			this.autoHideTime = Time.time + this.autoHideDelay;
		}
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x00047A62 File Offset: 0x00045C62
	public virtual void Hide()
	{
		this.isHiding = true;
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000EEA RID: 3818 RVA: 0x00047A77 File Offset: 0x00045C77
	protected virtual void Update()
	{
		if (this.autoHideTime > 0f && Time.time > this.autoHideTime)
		{
			this.Hide();
		}
	}

	// Token: 0x0400137E RID: 4990
	public bool isHiding = true;

	// Token: 0x0400137F RID: 4991
	public float autoHideDelay = -1f;

	// Token: 0x04001380 RID: 4992
	public float autoHideTime = -1f;
}
