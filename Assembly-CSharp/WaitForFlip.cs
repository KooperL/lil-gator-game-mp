using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001E3 RID: 483
public class WaitForFlip : MonoBehaviour
{
	// Token: 0x060008EF RID: 2287 RVA: 0x000387B0 File Offset: 0x000369B0
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

	// Token: 0x060008F0 RID: 2288 RVA: 0x00008B00 File Offset: 0x00006D00
	private void Update()
	{
		if (Time.time - this.lastTriggerTime > 0.25f)
		{
			this.onLeaveArea.Invoke();
			base.enabled = false;
		}
	}

	// Token: 0x04000B86 RID: 2950
	public UnityEvent onFlip;

	// Token: 0x04000B87 RID: 2951
	public bool disableOnFlip = true;

	// Token: 0x04000B88 RID: 2952
	public UnityEvent onLeaveArea;

	// Token: 0x04000B89 RID: 2953
	private float lastTriggerTime = -1f;
}
