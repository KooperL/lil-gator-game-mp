using System;
using System.Collections;
using UnityEngine;

public class IntroDialogue : MonoBehaviour
{
	// Token: 0x06000525 RID: 1317 RVA: 0x00005C18 File Offset: 0x00003E18
	private void Start()
	{
		if (GameData.g.ReadInt("GlobalGameState", 0) == 0)
		{
			base.StartCoroutine(this.RunConversation());
		}
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x00005C39 File Offset: 0x00003E39
	private IEnumerator RunConversation()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_Intro1", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		yield return base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Tutorial_IntroText", this.profiles, true, true, null));
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_Intro2", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		GameData.g.Write("GlobalGameState", 1);
		yield break;
	}

	public CharacterProfile[] profiles;

	public DialogueActor[] actors;
}
