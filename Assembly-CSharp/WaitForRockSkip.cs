using System;
using UnityEngine;
using UnityEngine.Events;

public class WaitForRockSkip : MonoBehaviour
{
	// Token: 0x06000932 RID: 2354 RVA: 0x00008E6A File Offset: 0x0000706A
	private void Awake()
	{
		Skipper.bestSkip = 0;
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x0003A17C File Offset: 0x0003837C
	public void OnTriggerStay(Collider other)
	{
		Skipper.isPartOfQuest = true;
		base.enabled = true;
		this.lastTriggerTime = Time.time;
		if (Skipper.bestSkip >= this.requiredSkips)
		{
			this.onSkip.Invoke();
			if (this.disableOnSkip)
			{
				base.gameObject.SetActive(false);
			}
			PlayAudio.p.PlayQuestEndSting();
			Skipper.isPartOfQuest = false;
		}
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x00008E72 File Offset: 0x00007072
	private void Update()
	{
		if (Time.time - this.lastTriggerTime > 0.25f)
		{
			Skipper.isPartOfQuest = false;
			this.onLeaveArea.Invoke();
			base.enabled = false;
		}
	}

	public int requiredSkips = 4;

	public UnityEvent onSkip;

	public bool disableOnSkip = true;

	public UnityEvent onLeaveArea;

	private float lastTriggerTime = -1f;
}
