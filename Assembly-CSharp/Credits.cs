using System;
using UnityEngine;
using UnityEngine.Events;

public class Credits : MonoBehaviour
{
	// Token: 0x0600001F RID: 31 RVA: 0x0000271B File Offset: 0x0000091B
	private void OnEnable()
	{
		Game.DialogueDepth++;
		this.beforeCredits.Invoke();
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002734 File Offset: 0x00000934
	public void ReachedEndPicture()
	{
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002736 File Offset: 0x00000936
	public void TransitionToScene()
	{
		this.afterCredits.Invoke();
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002743 File Offset: 0x00000943
	public void HideCanvas()
	{
		Game.DialogueDepth--;
		base.gameObject.SetActive(false);
	}

	public UnityEvent beforeCredits;

	public UnityEvent afterCredits;
}
