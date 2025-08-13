using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000170 RID: 368
public class WaitForRockSkip : MonoBehaviour
{
	// Token: 0x06000793 RID: 1939 RVA: 0x0002549E File Offset: 0x0002369E
	private void Awake()
	{
		Skipper.bestSkip = 0;
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x000254A8 File Offset: 0x000236A8
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

	// Token: 0x06000795 RID: 1941 RVA: 0x00025509 File Offset: 0x00023709
	private void Update()
	{
		if (Time.time - this.lastTriggerTime > 0.25f)
		{
			Skipper.isPartOfQuest = false;
			this.onLeaveArea.Invoke();
			base.enabled = false;
		}
	}

	// Token: 0x040009BC RID: 2492
	public int requiredSkips = 4;

	// Token: 0x040009BD RID: 2493
	public UnityEvent onSkip;

	// Token: 0x040009BE RID: 2494
	public bool disableOnSkip = true;

	// Token: 0x040009BF RID: 2495
	public UnityEvent onLeaveArea;

	// Token: 0x040009C0 RID: 2496
	private float lastTriggerTime = -1f;
}
