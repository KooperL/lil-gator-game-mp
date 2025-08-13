using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200016F RID: 367
public class WaitForFlip : MonoBehaviour
{
	// Token: 0x06000790 RID: 1936 RVA: 0x00025404 File Offset: 0x00023604
	public void OnTriggerStay(Collider other)
	{
		base.enabled = true;
		this.lastTriggerTime = Time.time;
		if (Player.ragdollController.transform.up.y < 0f)
		{
			this.onFlip.Invoke();
			if (this.disableOnFlip)
			{
				base.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x0002545D File Offset: 0x0002365D
	private void Update()
	{
		if (Time.time - this.lastTriggerTime > 0.25f)
		{
			this.onLeaveArea.Invoke();
			base.enabled = false;
		}
	}

	// Token: 0x040009B8 RID: 2488
	public UnityEvent onFlip;

	// Token: 0x040009B9 RID: 2489
	public bool disableOnFlip = true;

	// Token: 0x040009BA RID: 2490
	public UnityEvent onLeaveArea;

	// Token: 0x040009BB RID: 2491
	private float lastTriggerTime = -1f;
}
