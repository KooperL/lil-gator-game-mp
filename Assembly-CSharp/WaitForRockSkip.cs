using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001E4 RID: 484
public class WaitForRockSkip : MonoBehaviour
{
	// Token: 0x060008F2 RID: 2290 RVA: 0x0003880C File Offset: 0x00036A0C
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

	// Token: 0x060008F3 RID: 2291 RVA: 0x00008B41 File Offset: 0x00006D41
	private void Update()
	{
		if (Time.time - this.lastTriggerTime > 0.25f)
		{
			Skipper.isPartOfQuest = false;
			this.onLeaveArea.Invoke();
			base.enabled = false;
		}
	}

	// Token: 0x04000B8A RID: 2954
	public int requiredSkips = 4;

	// Token: 0x04000B8B RID: 2955
	public UnityEvent onSkip;

	// Token: 0x04000B8C RID: 2956
	public bool disableOnSkip = true;

	// Token: 0x04000B8D RID: 2957
	public UnityEvent onLeaveArea;

	// Token: 0x04000B8E RID: 2958
	private float lastTriggerTime = -1f;
}
