using System;
using UnityEngine;
using UnityEngine.Events;

public class WaitForShieldSkip : MonoBehaviour
{
	// Token: 0x06000936 RID: 2358 RVA: 0x00008EAB File Offset: 0x000070AB
	private void OnEnable()
	{
		PlayerMovement.didShieldSkip = false;
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x00008EB3 File Offset: 0x000070B3
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

	// Token: 0x06000938 RID: 2360 RVA: 0x00008EED File Offset: 0x000070ED
	private void Update()
	{
		if (Time.time - this.lastTriggerTime > 0.25f)
		{
			this.onLeaveArea.Invoke();
			base.enabled = false;
		}
	}

	public UnityEvent onSkip;

	public bool disableOnSkip = true;

	public UnityEvent onLeaveArea;

	private float lastTriggerTime = -1f;
}
