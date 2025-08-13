using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002D5 RID: 725
public class ThrowObjectQuest : MonoBehaviour
{
	// Token: 0x1700018B RID: 395
	// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0000C95F File Offset: 0x0000AB5F
	// (set) Token: 0x06000E25 RID: 3621 RVA: 0x0000C972 File Offset: 0x0000AB72
	private int State
	{
		get
		{
			return GameData.g.ReadInt(this.id, 0);
		}
		set
		{
			GameData.g.Write(this.id, value);
		}
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x0000C985 File Offset: 0x0000AB85
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x0004B938 File Offset: 0x00049B38
	private void UpdateState()
	{
		int state = this.State;
		if (this.crier != null)
		{
			this.crier.SetActive(state == 0);
		}
		for (int i = 0; i < this.chunks.Length; i++)
		{
			if (this.chunks[i].thrownObject != null)
			{
				this.chunks[i].thrownObject.SetActive(state == i + 1 && !this.chunks[i].IsItemFetched);
			}
		}
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x0000C98D File Offset: 0x0000AB8D
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x0000C99C File Offset: 0x0000AB9C
	private IEnumerator RunConversation()
	{
		DialogueManager.d.CancelBubble();
		int state = this.State;
		if (state == this.chunks.Length && this.chunks[this.chunks.Length - 1].IsItemFetched)
		{
			if (!string.IsNullOrEmpty(this.rewardText))
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.rewardText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			this.onReward.Invoke();
			this.State = this.chunks.Length + 1;
		}
		else if (state <= this.chunks.Length)
		{
			if (state == 0 || this.chunks[state - 1].IsItemFetched)
			{
				this.State = state + 1;
				Coroutine dialogueChunk = base.StartCoroutine(DialogueManager.d.LoadChunk(this.chunks[state].prompt, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
				yield return DialogueManager.d.waitUntilCue;
				DialogueManager.d.cue = false;
				this.chunks[state].thrownObject.GetComponent<ThrownRigidbody>().PrepareToThrow();
				this.chunks[state].thrownObject.SetActive(true);
				if (this.chunks[state].cameraObject != null)
				{
					this.chunks[state].cameraObject.SetActive(true);
					yield return DialogueManager.d.waitUntilCue;
					DialogueManager.d.cue = false;
					this.chunks[state].cameraObject.SetActive(false);
				}
				yield return dialogueChunk;
				dialogueChunk = null;
			}
			else
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.chunks[state - 1].reminder, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
		}
		if (state > this.chunks.Length && !string.IsNullOrEmpty(this.afterText))
		{
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.afterText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		}
		this.UpdateState();
		yield return null;
		yield break;
	}

	// Token: 0x06000E2A RID: 3626 RVA: 0x0004B9C8 File Offset: 0x00049BC8
	public void PickedUpPencil()
	{
		int state = this.State;
		if (state == 0 || state - 1 >= this.chunks.Length)
		{
			return;
		}
		this.chunks[this.State - 1].IsItemFetched = true;
	}

	// Token: 0x04001258 RID: 4696
	public string id;

	// Token: 0x04001259 RID: 4697
	public ThrowObjectQuest.QuestChunk[] chunks;

	// Token: 0x0400125A RID: 4698
	public DialogueActor[] actors;

	// Token: 0x0400125B RID: 4699
	public GameObject crier;

	// Token: 0x0400125C RID: 4700
	public string rewardText;

	// Token: 0x0400125D RID: 4701
	public UnityEvent onReward;

	// Token: 0x0400125E RID: 4702
	public string afterText;

	// Token: 0x020002D6 RID: 726
	[Serializable]
	public struct QuestChunk
	{
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x0000C9AB File Offset: 0x0000ABAB
		// (set) Token: 0x06000E2D RID: 3629 RVA: 0x0000C9BE File Offset: 0x0000ABBE
		public bool IsItemFetched
		{
			get
			{
				return GameData.g.ReadBool(this.itemKey, false);
			}
			set
			{
				GameData.g.Write(this.itemKey, value);
			}
		}

		// Token: 0x0400125F RID: 4703
		public string prompt;

		// Token: 0x04001260 RID: 4704
		public string reminder;

		// Token: 0x04001261 RID: 4705
		public GameObject thrownObject;

		// Token: 0x04001262 RID: 4706
		public ItemObject item;

		// Token: 0x04001263 RID: 4707
		public string itemKey;

		// Token: 0x04001264 RID: 4708
		public GameObject cameraObject;
	}
}
