using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CheckInt : MonoBehaviour
{
	// (get) Token: 0x06000B91 RID: 2961 RVA: 0x00038895 File Offset: 0x00036A95
	private int State
	{
		get
		{
			return GameData.g.ReadInt(this.id, 0);
		}
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x000388A8 File Offset: 0x00036AA8
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x000388B7 File Offset: 0x00036AB7
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
