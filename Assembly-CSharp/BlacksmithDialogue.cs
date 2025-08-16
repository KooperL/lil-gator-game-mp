using System;
using System.Collections;
using UnityEngine;

public class BlacksmithDialogue : MonoBehaviour, Interaction
{
	// Token: 0x060003A8 RID: 936 RVA: 0x00004CB7 File Offset: 0x00002EB7
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x00004CC6 File Offset: 0x00002EC6
	private IEnumerator RunConversation()
	{
		yield return null;
		yield break;
	}

	public DialogueActor[] actors;

	public int neededOres = 5;

	public string rewardID = "Weapon_Hero";
}
