using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000364 RID: 868
public class UIDisplay : MonoBehaviour
{
	// Token: 0x060010A7 RID: 4263 RVA: 0x0000E4E9 File Offset: 0x0000C6E9
	private void Awake()
	{
		this.waitForDelay = new WaitForSeconds(this.promptDelay);
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x0000E4FC File Offset: 0x0000C6FC
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

	// Token: 0x040015AD RID: 5549
	public float promptDelay = 1f;

	// Token: 0x040015AE RID: 5550
	private WaitForSeconds waitForDelay;

	// Token: 0x040015AF RID: 5551
	public UIButtonPrompt buttonPrompt;

	// Token: 0x040015B0 RID: 5552
	public bool deactivateAfterPrompt = true;

	// Token: 0x040015B1 RID: 5553
	public UnityEvent afterPrompt;
}
