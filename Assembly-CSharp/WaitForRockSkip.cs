using System;
using UnityEngine;
using UnityEngine.Events;

public class WaitForRockSkip : MonoBehaviour
{
	// Token: 0x06000932 RID: 2354 RVA: 0x00008E74 File Offset: 0x00007074
	private void Awake()
	{
		Skipper.bestSkip = 0;
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x0003A158 File Offset: 0x00038358
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

	// Token: 0x06000934 RID: 2356 RVA: 0x00008E7C File Offset: 0x0000707C
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
