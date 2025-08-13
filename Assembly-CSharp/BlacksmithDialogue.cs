using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000D4 RID: 212
public class BlacksmithDialogue : MonoBehaviour, Interaction
{
	// Token: 0x06000382 RID: 898 RVA: 0x00004AD3 File Offset: 0x00002CD3
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000383 RID: 899 RVA: 0x00004AE2 File Offset: 0x00002CE2
	private IEnumerator RunConversation()
	{
		yield return null;
		yield break;
	}

	// Token: 0x04000507 RID: 1287
	public DialogueActor[] actors;

	// Token: 0x04000508 RID: 1288
	public int neededOres = 5;

	// Token: 0x04000509 RID: 1289
	public string rewardID = "Weapon_Hero";
}
