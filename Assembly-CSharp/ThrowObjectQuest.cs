using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ThrowObjectQuest : MonoBehaviour
{
	// (get) Token: 0x06000E70 RID: 3696 RVA: 0x0000CC67 File Offset: 0x0000AE67
	// (set) Token: 0x06000E71 RID: 3697 RVA: 0x0000CC7A File Offset: 0x0000AE7A
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

	// Token: 0x06000E72 RID: 3698 RVA: 0x0000CC8D File Offset: 0x0000AE8D
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000E73 RID: 3699 RVA: 0x0004D4C0 File Offset: 0x0004B6C0
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

	// Token: 0x06000E74 RID: 3700 RVA: 0x0000CC95 File Offset: 0x0000AE95
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
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

	// Token: 0x06000E76 RID: 3702 RVA: 0x0004D550 File Offset: 0x0004B750
	public void PickedUpPencil()
	{
		int state = this.State;
		if (state == 0 || state - 1 >= this.chunks.Length)
		{
			return;
		}
		this.chunks[this.State - 1].IsItemFetched = true;
	}

	public string id;

	public ThrowObjectQuest.QuestChunk[] chunks;

	public DialogueActor[] actors;

	public GameObject crier;

	public string rewardText;

	public UnityEvent onReward;

	public string afterText;

	[Serializable]
	public struct QuestChunk
	{
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x0000CCB3 File Offset: 0x0000AEB3
		// (set) Token: 0x06000E79 RID: 3705 RVA: 0x0000CCC6 File Offset: 0x0000AEC6
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

		public string prompt;

		public string reminder;

		public GameObject thrownObject;

		public ItemObject item;

		public string itemKey;

		public GameObject cameraObject;
	}
}
