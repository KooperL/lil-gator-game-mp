using System;
using UnityEngine;

// Token: 0x0200038A RID: 906
public class PlayNotificationSting : MonoBehaviour
{
	// Token: 0x06001144 RID: 4420 RVA: 0x0000ECB5 File Offset: 0x0000CEB5
	public void ResetSting()
	{
		this.isTriggered = false;
		base.enabled = true;
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x0000ECC5 File Offset: 0x0000CEC5
	private void OnDisable()
	{
		this.timer = 0f;
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x0000ECD2 File Offset: 0x0000CED2
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

	// Token: 0x04001630 RID: 5680
	private bool isTriggered = true;

	// Token: 0x04001631 RID: 5681
	private float timer;
}
