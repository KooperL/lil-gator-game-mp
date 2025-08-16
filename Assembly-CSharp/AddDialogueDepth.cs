using System;
using UnityEngine;

public class AddDialogueDepth : MonoBehaviour
{
	// Token: 0x060003A5 RID: 933 RVA: 0x00004C9B File Offset: 0x00002E9B
	private void OnEnable()
	{
		Game.DialogueDepth++;
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00004CA9 File Offset: 0x00002EA9
	private void OnDisable()
	{
		Game.DialogueDepth--;
	}
}
