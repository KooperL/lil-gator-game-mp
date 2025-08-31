using System;
using System.Collections;
using UnityEngine;

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

	public DialogueActor[] actors;

	public int neededOres = 5;

	public string rewardID = "Weapon_Hero";
}
