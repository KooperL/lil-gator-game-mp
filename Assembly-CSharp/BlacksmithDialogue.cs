using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000AA RID: 170
public class BlacksmithDialogue : MonoBehaviour, Interaction
{
	// Token: 0x0600033D RID: 829 RVA: 0x00013056 File Offset: 0x00011256
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x0600033E RID: 830 RVA: 0x00013065 File Offset: 0x00011265
	private IEnumerator RunConversation()
	{
		yield return null;
		yield break;
	}

	// Token: 0x04000469 RID: 1129
	public DialogueActor[] actors;

	// Token: 0x0400046A RID: 1130
	public int neededOres = 5;

	// Token: 0x0400046B RID: 1131
	public string rewardID = "Weapon_Hero";
}
