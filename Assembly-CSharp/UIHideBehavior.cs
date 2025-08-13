using System;
using UnityEngine;

// Token: 0x020003A7 RID: 935
public class UIHideBehavior : MonoBehaviour
{
	// Token: 0x060011C0 RID: 4544 RVA: 0x0000F23C File Offset: 0x0000D43C
	public virtual void Show()
	{
		this.isHiding = false;
		base.gameObject.SetActive(true);
		if (this.autoHideDelay > 0f)
		{
			this.autoHideTime = Time.time + this.autoHideDelay;
		}
	}

	// Token: 0x060011C1 RID: 4545 RVA: 0x0000F270 File Offset: 0x0000D470
	public virtual void Hide()
	{
		this.isHiding = true;
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011C2 RID: 4546 RVA: 0x0000F285 File Offset: 0x0000D485
	protected virtual void Update()
	{
		if (this.autoHideTime > 0f && Time.time > this.autoHideTime)
		{
			this.Hide();
		}
	}

	// Token: 0x040016F4 RID: 5876
	public bool isHiding = true;

	// Token: 0x040016F5 RID: 5877
	public float autoHideDelay = -1f;

	// Token: 0x040016F6 RID: 5878
	public float autoHideTime = -1f;
}
