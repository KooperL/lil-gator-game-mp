using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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

	public float promptDelay = 1f;

	private WaitForSeconds waitForDelay;

	public UIButtonPrompt buttonPrompt;

	public bool deactivateAfterPrompt = true;

	public UnityEvent afterPrompt;
}
