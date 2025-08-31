using System;
using UnityEngine;

public class PlayNotificationSting : MonoBehaviour
{
	// Token: 0x06000E74 RID: 3700 RVA: 0x0004515E File Offset: 0x0004335E
	public void ResetSting()
	{
		this.isTriggered = false;
		base.enabled = true;
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x0004516E File Offset: 0x0004336E
	private void OnDisable()
	{
		this.timer = 0f;
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x0004517B File Offset: 0x0004337B
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
