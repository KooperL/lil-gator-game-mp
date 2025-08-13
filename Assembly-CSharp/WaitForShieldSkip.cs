using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000171 RID: 369
public class WaitForShieldSkip : MonoBehaviour
{
	// Token: 0x06000797 RID: 1943 RVA: 0x00025557 File Offset: 0x00023757
	private void OnEnable()
	{
		PlayerMovement.didShieldSkip = false;
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x0002555F File Offset: 0x0002375F
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

	// Token: 0x06000799 RID: 1945 RVA: 0x00025599 File Offset: 0x00023799
	private void Update()
	{
		if (Time.time - this.lastTriggerTime > 0.25f)
		{
			this.onLeaveArea.Invoke();
			base.enabled = false;
		}
	}

	// Token: 0x040009C1 RID: 2497
	public UnityEvent onSkip;

	// Token: 0x040009C2 RID: 2498
	public bool disableOnSkip = true;

	// Token: 0x040009C3 RID: 2499
	public UnityEvent onLeaveArea;

	// Token: 0x040009C4 RID: 2500
	private float lastTriggerTime = -1f;
}
