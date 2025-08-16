using System;
using UnityEngine;

public class PlayNotificationSting : MonoBehaviour
{
	// Token: 0x060011A4 RID: 4516 RVA: 0x0000F089 File Offset: 0x0000D289
	public void ResetSting()
	{
		this.isTriggered = false;
		base.enabled = true;
	}

	// Token: 0x060011A5 RID: 4517 RVA: 0x0000F099 File Offset: 0x0000D299
	private void OnDisable()
	{
		this.timer = 0f;
	}

	// Token: 0x060011A6 RID: 4518 RVA: 0x0000F0A6 File Offset: 0x0000D2A6
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer > 0.1f)
		{
			this.isTriggered = true;
			PlayAudio.p.PlayQuestEndSting();
			base.enabled = false;
		}
	}

	private bool isTriggered = true;

	private float timer;
}
