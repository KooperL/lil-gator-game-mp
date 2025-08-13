using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000C1 RID: 193
public class IntroDialogue : MonoBehaviour
{
	// Token: 0x06000433 RID: 1075 RVA: 0x0001845E File Offset: 0x0001665E
	private void Start()
	{
		if (GameData.g.ReadInt("GlobalGameState", 0) == 0)
		{
			base.StartCoroutine(this.RunConversation());
		}
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x0001847F File Offset: 0x0001667F
	private IEnumerator RunConversation()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_Intro1", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		yield return base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Tutorial_IntroText", this.profiles, true, true, null));
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_Intro2", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		GameData.g.Write("GlobalGameState", 1);
		yield break;
	}

	// Token: 0x040005EB RID: 1515
	public CharacterProfile[] profiles;

	// Token: 0x040005EC RID: 1516
	public DialogueActor[] actors;
}
