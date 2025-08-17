using System;
using UnityEngine;
using UnityEngine.Events;

public class WaitForFlip : MonoBehaviour
{
	// Token: 0x0600092F RID: 2351 RVA: 0x0003A120 File Offset: 0x00038320
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

	// Token: 0x06000930 RID: 2352 RVA: 0x00008E29 File Offset: 0x00007029
	private void Update()
	{
		if (Time.time - this.lastTriggerTime > 0.25f)
		{
			this.onLeaveArea.Invoke();
			base.enabled = false;
		}
	}

	public UnityEvent onFlip;

	public bool disableOnFlip = true;

	public UnityEvent onLeaveArea;

	private float lastTriggerTime = -1f;
}
