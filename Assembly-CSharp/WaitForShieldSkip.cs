using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001E5 RID: 485
public class WaitForShieldSkip : MonoBehaviour
{
	// Token: 0x060008F5 RID: 2293 RVA: 0x00008B8F File Offset: 0x00006D8F
	private void OnEnable()
	{
		PlayerMovement.didShieldSkip = false;
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x00008B97 File Offset: 0x00006D97
	public void OnTriggerStay(Collider other)
	{
		base.enabled = true;
		this.lastTriggerTime = Time.time;
		if (PlayerMovement.didShieldSkip)
		{
			this.onSkip.Invoke();
			if (this.disableOnSkip)
			{
				base.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x00008BD1 File Offset: 0x00006DD1
	private void Update()
	{
		if (Time.time - this.lastTriggerTime > 0.25f)
		{
			this.onLeaveArea.Invoke();
			base.enabled = false;
		}
	}

	// Token: 0x04000B8F RID: 2959
	public UnityEvent onSkip;

	// Token: 0x04000B90 RID: 2960
	public bool disableOnSkip = true;

	// Token: 0x04000B91 RID: 2961
	public UnityEvent onLeaveArea;

	// Token: 0x04000B92 RID: 2962
	private float lastTriggerTime = -1f;
}
