using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UIDisplay : MonoBehaviour
{
	// Token: 0x06001103 RID: 4355 RVA: 0x0000E85C File Offset: 0x0000CA5C
	private void Awake()
	{
		this.waitForDelay = new WaitForSeconds(this.promptDelay);
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x0000E86F File Offset: 0x0000CA6F
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
