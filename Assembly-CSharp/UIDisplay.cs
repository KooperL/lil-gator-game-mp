using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200028B RID: 651
public class UIDisplay : MonoBehaviour
{
	// Token: 0x06000DE4 RID: 3556 RVA: 0x00043504 File Offset: 0x00041704
	private void Awake()
	{
		this.waitForDelay = new WaitForSeconds(this.promptDelay);
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x00043517 File Offset: 0x00041717
	public IEnumerator RunDisplay()
	{
		this.buttonPrompt.gameObject.SetActive(false);
		base.gameObject.SetActive(true);
		yield return this.waitForDelay;
		this.buttonPrompt.gameObject.SetActive(true);
		yield return this.buttonPrompt.waitUntilTriggered;
		this.buttonPrompt.gameObject.SetActive(false);
		if (this.deactivateAfterPrompt)
		{
			base.gameObject.SetActive(false);
		}
		this.afterPrompt.Invoke();
		yield break;
	}

	// Token: 0x04001250 RID: 4688
	public float promptDelay = 1f;

	// Token: 0x04001251 RID: 4689
	private WaitForSeconds waitForDelay;

	// Token: 0x04001252 RID: 4690
	public UIButtonPrompt buttonPrompt;

	// Token: 0x04001253 RID: 4691
	public bool deactivateAfterPrompt = true;

	// Token: 0x04001254 RID: 4692
	public UnityEvent afterPrompt;
}
