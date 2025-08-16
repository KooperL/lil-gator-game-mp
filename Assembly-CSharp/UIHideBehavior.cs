using System;
using UnityEngine;

public class UIHideBehavior : MonoBehaviour
{
	// Token: 0x06001220 RID: 4640 RVA: 0x0000F610 File Offset: 0x0000D810
	public virtual void Show()
	{
		this.isHiding = false;
		base.gameObject.SetActive(true);
		if (this.autoHideDelay > 0f)
		{
			this.autoHideTime = Time.time + this.autoHideDelay;
		}
	}

	// Token: 0x06001221 RID: 4641 RVA: 0x0000F644 File Offset: 0x0000D844
	public virtual void Hide()
	{
		this.isHiding = true;
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001222 RID: 4642 RVA: 0x0000F659 File Offset: 0x0000D859
	protected virtual void Update()
	{
		if (this.autoHideTime > 0f && Time.time > this.autoHideTime)
		{
			this.Hide();
		}
	}

	public bool isHiding = true;

	public float autoHideDelay = -1f;

	public float autoHideTime = -1f;
}
