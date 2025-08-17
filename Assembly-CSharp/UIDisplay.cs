using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UIDisplay : MonoBehaviour
{
	// Token: 0x06001102 RID: 4354 RVA: 0x0000E852 File Offset: 0x0000CA52
	private void Awake()
	{
		this.waitForDelay = new WaitForSeconds(this.promptDelay);
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x0000E865 File Offset: 0x0000CA65
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
