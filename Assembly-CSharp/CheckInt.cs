using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CheckInt : MonoBehaviour
{
	// (get) Token: 0x06000E27 RID: 3623 RVA: 0x0000C9FB File Offset: 0x0000ABFB
	private int State
	{
		get
		{
			return GameData.g.ReadInt(this.id, 0);
		}
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x0000CA0E File Offset: 0x0000AC0E
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x0000CA1D File Offset: 0x0000AC1D
	private IEnumerator RunConversation()
	{
		int state = this.State;
		if (state < this.checkAgainst)
		{
			if (!string.IsNullOrEmpty(this.ifBelowText))
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.ifBelowText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			this.ifBelow.Invoke();
		}
		else if (state == this.checkAgainst)
		{
			if (!string.IsNullOrEmpty(this.ifEqualText))
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.ifEqualText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			this.ifEqual.Invoke();
		}
		else
		{
			if (!string.IsNullOrEmpty(this.ifAboveText))
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.ifAboveText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			this.ifAbove.Invoke();
		}
		yield return null;
		yield break;
	}

	public string id;

	public int checkAgainst;

	public string ifBelowText;

	public UnityEvent ifBelow;

	public string ifEqualText;

	public UnityEvent ifEqual;

	public string ifAboveText;

	public UnityEvent ifAbove;

	public DialogueActor[] actors;
}
