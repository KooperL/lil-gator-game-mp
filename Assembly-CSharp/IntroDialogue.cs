using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000104 RID: 260
public class IntroDialogue : MonoBehaviour
{
	// Token: 0x060004F8 RID: 1272 RVA: 0x000059A3 File Offset: 0x00003BA3
	private void Start()
	{
		if (GameData.g.ReadInt("GlobalGameState", 0) == 0)
		{
			base.StartCoroutine(this.RunConversation());
		}
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x000059C4 File Offset: 0x00003BC4
	private IEnumerator RunConversation()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_Intro1", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		yield return base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Tutorial_IntroText", this.profiles, true, true, null));
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_Intro2", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		GameData.g.Write("GlobalGameState", 1);
		yield break;
	}

	// Token: 0x040006FB RID: 1787
	public CharacterProfile[] profiles;

	// Token: 0x040006FC RID: 1788
	public DialogueActor[] actors;
}
