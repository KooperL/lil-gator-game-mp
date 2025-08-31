using System;
using UnityEngine;
using UnityEngine.Events;

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

	public UnityEvent onFlip;

	public bool disableOnFlip = true;

	public UnityEvent onLeaveArea;

	private float lastTriggerTime = -1f;
}
