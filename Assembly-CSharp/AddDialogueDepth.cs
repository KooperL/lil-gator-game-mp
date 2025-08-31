using System;
using UnityEngine;

public class AddDialogueDepth : MonoBehaviour
{
	// Token: 0x0600033A RID: 826 RVA: 0x00013032 File Offset: 0x00011232
	private void OnEnable()
	{
		Game.DialogueDepth++;
	}

	// Token: 0x0600033B RID: 827 RVA: 0x00013040 File Offset: 0x00011240
	private void OnDisable()
	{
		Game.DialogueDepth--;
	}
}
