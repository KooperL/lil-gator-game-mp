using System;
using UnityEngine;
using UnityEngine.Events;

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

	public int requiredSkips = 4;

	public UnityEvent onSkip;

	public bool disableOnSkip = true;

	public UnityEvent onLeaveArea;

	private float lastTriggerTime = -1f;
}
