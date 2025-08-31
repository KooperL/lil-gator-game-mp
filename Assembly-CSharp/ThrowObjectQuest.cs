using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ThrowObjectQuest : MonoBehaviour
{
	// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00038FAD File Offset: 0x000371AD
	// (set) Token: 0x06000BCA RID: 3018 RVA: 0x00038FC0 File Offset: 0x000371C0
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

	// Token: 0x06000BCB RID: 3019 RVA: 0x00038FD3 File Offset: 0x000371D3
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x00038FDC File Offset: 0x000371DC
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

	// Token: 0x06000BCD RID: 3021 RVA: 0x0003906C File Offset: 0x0003726C
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x0003907B File Offset: 0x0003727B
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

	// Token: 0x06000BCF RID: 3023 RVA: 0x0003908C File Offset: 0x0003728C
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
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x00071465 File Offset: 0x0006F665
		// (set) Token: 0x06001A9B RID: 6811 RVA: 0x00071478 File Offset: 0x0006F678
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
