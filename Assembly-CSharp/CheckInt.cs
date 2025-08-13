using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002C3 RID: 707
public class CheckInt : MonoBehaviour
{
	// Token: 0x17000180 RID: 384
	// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0000C6E9 File Offset: 0x0000A8E9
	private int State
	{
		get
		{
			return GameData.g.ReadInt(this.id, 0);
		}
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x0000C6FC File Offset: 0x0000A8FC
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000DDC RID: 3548 RVA: 0x0000C70B File Offset: 0x0000A90B
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

	// Token: 0x04001215 RID: 4629
	public string id;

	// Token: 0x04001216 RID: 4630
	public int checkAgainst;

	// Token: 0x04001217 RID: 4631
	public string ifBelowText;

	// Token: 0x04001218 RID: 4632
	public UnityEvent ifBelow;

	// Token: 0x04001219 RID: 4633
	public string ifEqualText;

	// Token: 0x0400121A RID: 4634
	public UnityEvent ifEqual;

	// Token: 0x0400121B RID: 4635
	public string ifAboveText;

	// Token: 0x0400121C RID: 4636
	public UnityEvent ifAbove;

	// Token: 0x0400121D RID: 4637
	public DialogueActor[] actors;
}
