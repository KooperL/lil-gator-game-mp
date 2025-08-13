using System;
using UnityEngine;

// Token: 0x020000D3 RID: 211
public class AddDialogueDepth : MonoBehaviour
{
	// Token: 0x0600037F RID: 895 RVA: 0x00004AB7 File Offset: 0x00002CB7
	private void OnEnable()
	{
		Game.DialogueDepth++;
	}

	// Token: 0x06000380 RID: 896 RVA: 0x00004AC5 File Offset: 0x00002CC5
	private void OnDisable()
	{
		Game.DialogueDepth--;
	}
}
