using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000216 RID: 534
public class CheckInt : MonoBehaviour
{
	// Token: 0x170000BE RID: 190
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

	// Token: 0x04000F59 RID: 3929
	public string id;

	// Token: 0x04000F5A RID: 3930
	public int checkAgainst;

	// Token: 0x04000F5B RID: 3931
	public string ifBelowText;

	// Token: 0x04000F5C RID: 3932
	public UnityEvent ifBelow;

	// Token: 0x04000F5D RID: 3933
	public string ifEqualText;

	// Token: 0x04000F5E RID: 3934
	public UnityEvent ifEqual;

	// Token: 0x04000F5F RID: 3935
	public string ifAboveText;

	// Token: 0x04000F60 RID: 3936
	public UnityEvent ifAbove;

	// Token: 0x04000F61 RID: 3937
	public DialogueActor[] actors;
}
