using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B0 RID: 176
public class DialogueManager : MonoBehaviour
{
	// Token: 0x17000031 RID: 49
	// (get) Token: 0x060003A8 RID: 936 RVA: 0x00015AAA File Offset: 0x00013CAA
	public bool IsInAmbientDialogue
	{
		get
		{
			return this.bubbleCoroutine != null || this.smallPhoneCoroutine != null;
		}
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060003A9 RID: 937 RVA: 0x00015ABF File Offset: 0x00013CBF
	public bool IsInImportantDialogue
	{
		get
		{
			return this.bubbleIsImportant || this.smallPhoneIsImportant || this.isInNormalDialogue;
		}
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060003AA RID: 938 RVA: 0x00015AD9 File Offset: 0x00013CD9
	public bool CanAcceptBubbleDialogue
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x060003AB RID: 939 RVA: 0x00015ADC File Offset: 0x00013CDC
	internal bool IsPlayerReady
	{
		get
		{
			return Player.movement.IsGrounded || Player.movement.IsSubmerged || Player.movement.modIgnoreReady;
		}
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x060003AC RID: 940 RVA: 0x00015B02 File Offset: 0x00013D02
	public bool HasFocusPoint
	{
		get
		{
			return this.actorFocuses != null || Time.time - this.validCameraFocusPointTime < 0.1f;
		}
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x060003AD RID: 941 RVA: 0x00015B24 File Offset: 0x00013D24
	public Vector3 DialogueFocusPoint
	{
		get
		{
			if (this.actorFocuses == null)
			{
				return this.cameraFocusPoint;
			}
			this.validCameraFocusPointTime = Time.time;
			Vector3 focusPosition = DialogueActor.playerActor.FocusPosition;
			this.cameraFocusPoint = focusPosition;
			int num = 1;
			if (this.actorFocuses != null && this.actorFocuses.Length != 0)
			{
				foreach (DialogueActor dialogueActor in this.actorFocuses)
				{
					if (dialogueActor.gameObject.activeSelf)
					{
						this.cameraFocusPoint += dialogueActor.FocusPosition;
						num++;
					}
				}
			}
			this.cameraFocusPoint /= (float)num;
			this.cameraFocusPoint = Vector3.MoveTowards(focusPosition, this.cameraFocusPoint, 1f);
			float num2 = Mathf.InverseLerp(0.5f, 1f, PlayerOrbitCamera.active.mostRecentDistanceMultiplier);
			float num3 = Mathf.Lerp(0f, -0.5f, num2);
			this.cameraFocusPoint += new Vector3(0f, num3, 0f);
			return this.cameraFocusPoint;
		}
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00015C38 File Offset: 0x00013E38
	private void OnEnable()
	{
		DialogueManager.d = this;
		this.chunkDic = new Dictionary<string, DialogueChunk>();
		for (int i = 0; i < this.scripts.Length; i++)
		{
			for (int j = 0; j < this.scripts[i].chunks.Length; j++)
			{
				if (!this.chunkDic.ContainsKey(this.scripts[i].chunks[j].name))
				{
					this.chunkDic.Add(this.scripts[i].chunks[j].name, this.scripts[i].chunks[j]);
				}
			}
		}
		for (int k = 0; k < this.documents.Length; k++)
		{
			for (int l = 0; l < this.documents[k].chunks.Length; l++)
			{
				if (!this.chunkDic.ContainsKey(this.documents[k].chunks[l].name))
				{
					this.chunkDic.Add(this.documents[k].chunks[l].name, this.documents[k].chunks[l]);
				}
			}
		}
		this.waitUntilButtonPress = new WaitUntil(() => this.advanceInput || this.skipInput);
		this.waitForBubbleDelay = new WaitForSeconds(this.bubbleDelay);
		this.waitForSmallPhoneDelay = new WaitForSeconds(this.smallPhoneDelay);
		this.waitUntilBubbleFinish = new WaitUntil(() => !this.dialogueBoxBubble.isTypingText);
		this.waitUntilPlayerReady = new WaitUntil(() => this.IsPlayerReady);
		this.waitUntilCue = new WaitUntil(() => this.cue);
	}

	// Token: 0x060003AF RID: 943 RVA: 0x00015DCC File Offset: 0x00013FCC
	private void OnDisable()
	{
		this.CancelBubble();
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x00015DD4 File Offset: 0x00013FD4
	public IEnumerator LoadChunk(string chunkName, DialogueActor other, DialogueManager.DialogueBoxBackground background = DialogueManager.DialogueBoxBackground.Standard, bool cancelBubble = true)
	{
		this.temporaryActor[0] = other;
		return this.LoadChunk(chunkName, this.temporaryActor, background, cancelBubble);
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x00015DEF File Offset: 0x00013FEF
	public IEnumerator WaitForPlayer()
	{
		Player.movement.ClearMods();
		this.isWaitingForPlayer = true;
		yield return this.waitUntilPlayerReady;
		this.isWaitingForPlayer = false;
		yield break;
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x00015DFE File Offset: 0x00013FFE
	public IEnumerator LoadChunk(string chunkName, DialogueActor[] actors = null, DialogueManager.DialogueBoxBackground background = DialogueManager.DialogueBoxBackground.Standard, bool cancelBubble = true)
	{
		if (!this.chunkDic.ContainsKey(chunkName))
		{
			Debug.LogError("Invalid dialogue chunk: " + chunkName);
			yield break;
		}
		yield return base.StartCoroutine(this.LoadChunk(this.chunkDic[chunkName], actors, background, cancelBubble, true, false, false));
		yield break;
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00015E2C File Offset: 0x0001402C
	public IEnumerator LoadChunk(DialogueChunk chunk, DialogueActor[] actors = null, DialogueManager.DialogueBoxBackground background = DialogueManager.DialogueBoxBackground.Standard, bool cancelBubble = true, bool setDialogueCamera = true, bool skipWaits = false, bool skipWaitForPlayer = false)
	{
		this.isInNormalDialogue = true;
		if (!Game.ignoreDialogueDepth && !this.IsPlayerReady)
		{
			if (skipWaitForPlayer)
			{
				base.StartCoroutine(this.WaitForPlayer());
			}
			else
			{
				yield return base.StartCoroutine(this.WaitForPlayer());
			}
		}
		if (cancelBubble)
		{
			this.CancelBubble();
		}
		this.advanceInput = false;
		DialogueActor player = DialogueActor.playerActor;
		this.cue = false;
		DialogueManager.optionChosen = -1;
		Game.DialogueDepth++;
		if (!Game.ignoreDialogueDepth && setDialogueCamera)
		{
			this.SetDialogueCamera(actors);
		}
		player.IsInStandardDialogue = true;
		int j;
		if (actors != null)
		{
			for (j = 0; j < actors.Length; j++)
			{
				actors[j].IsInStandardDialogue = true;
			}
		}
		DialogueActor currentActor = null;
		DialogueActor previousActor = null;
		DialogueBox workingDialogueBox = this.dialogueBox;
		DialogueActor primaryLookTarget = null;
		DialogueActor secondaryLookTarget = null;
		if (DialogueDocumentInfo.d != null)
		{
			DialogueDocumentInfo.d.LoadDocumentInfo(chunk);
		}
		switch (background)
		{
		case DialogueManager.DialogueBoxBackground.Wooden:
			if (this.dialogueBoxWooden != null)
			{
				workingDialogueBox = this.dialogueBoxWooden;
			}
			break;
		case DialogueManager.DialogueBoxBackground.Narration:
		case DialogueManager.DialogueBoxBackground.Bubble:
			if (this.dialogueBoxBubble != null)
			{
				workingDialogueBox = this.dialogueBoxBubble;
			}
			break;
		case DialogueManager.DialogueBoxBackground.BigBubble:
			if (this.dialogueBoxBigBubble != null)
			{
				workingDialogueBox = this.dialogueBoxBigBubble;
			}
			break;
		}
		for (int i = 0; i < chunk.lines.Length; i = j + 1)
		{
			if (chunk.lines[i].actorIndex == 0 || actors == null)
			{
				currentActor = player;
			}
			else
			{
				if (chunk.lines[i].actorIndex - 1 >= actors.Length)
				{
					Debug.LogError("Warning: Not enough actors for line " + chunk.lines[i].GetText(Language.Auto));
					int num = actors.Length;
				}
				currentActor = actors[chunk.lines[i].actorIndex - 1];
			}
			DialogueManager.currentlySpeakingActor = currentActor;
			DialogueManager.currentlySpeakingActorIndex = chunk.lines[i].actorIndex;
			if (currentActor != previousActor && previousActor != null)
			{
				this.ClearActorEmote(previousActor, false);
			}
			int emote = chunk.lines[i].emote;
			this.SetActorEmote(currentActor, emote);
			this.SetStateAndPosition(currentActor, chunk.lines[i].state, chunk.lines[i].position);
			if (chunk.lines[i].holdEmote && currentActor != null)
			{
				currentActor.HoldEmote();
			}
			if (chunk.lines[i].cue)
			{
				this.cue = true;
			}
			previousActor = currentActor;
			if (chunk.lines[i].HasText() && actors != null && actors.Length != 0)
			{
				if (primaryLookTarget != currentActor)
				{
					secondaryLookTarget = primaryLookTarget;
					primaryLookTarget = currentActor;
				}
				if (secondaryLookTarget == null && primaryLookTarget != null)
				{
					if (currentActor != null && currentActor != primaryLookTarget)
					{
						secondaryLookTarget = currentActor;
					}
					else if (previousActor != null && previousActor != primaryLookTarget)
					{
						secondaryLookTarget = previousActor;
					}
					else if (primaryLookTarget != player)
					{
						secondaryLookTarget = player;
					}
					else
					{
						secondaryLookTarget = actors[0];
					}
				}
			}
			if (chunk.lines[i].look != null && chunk.lines[i].look.Length != 0 && (chunk.lines[i].HasText() || chunk.lines[i].waitTime != 0f) && actors != null && actors.Length != 0)
			{
				this.SetLookTarget(player, chunk.lines[i].look[0], primaryLookTarget, secondaryLookTarget, player, actors, i == 0);
				for (int k = 0; k < actors.Length; k++)
				{
					this.SetLookTarget(actors[k], chunk.lines[i].look[k + 1], primaryLookTarget, secondaryLookTarget, player, actors, i == 0);
				}
			}
			if (chunk.lines[i].HasText())
			{
				yield return workingDialogueBox.Load(chunk.lines[i].GetText(Language.Auto), currentActor, skipWaits || !chunk.lines[i].noInput, skipWaits ? 0f : chunk.lines[i].waitTime);
			}
			else if (chunk.lines[i].waitTime != 0f && !skipWaits)
			{
				workingDialogueBox.Clear(false);
				yield return new WaitForSeconds(chunk.lines[i].waitTime);
			}
			DialogueManager.currentlySpeakingActor = null;
			j = i;
		}
		if (chunk.options.Length != 0)
		{
			yield return base.StartCoroutine(this.RunDialogueOptions(chunk.mlOptions));
		}
		workingDialogueBox.Clear(true);
		this.ClearActorEmote(currentActor, false);
		if (actors != null && actors.Length != 0)
		{
			for (int l = 0; l < actors.Length; l++)
			{
				actors[l].LookAtDialogue = false;
			}
		}
		player.LookAtDialogue = false;
		player.IsInStandardDialogue = false;
		if (actors != null)
		{
			for (j = 0; j < actors.Length; j++)
			{
				actors[j].IsInStandardDialogue = false;
			}
		}
		Game.DialogueDepth--;
		this.isInNormalDialogue = false;
		if (!Game.ignoreDialogueDepth && setDialogueCamera)
		{
			this.ClearDialogueCamera();
		}
		yield break;
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x00015E7B File Offset: 0x0001407B
	private IEnumerator ClearDialogueDelayed(DialogueBox dialogueBox)
	{
		yield return null;
		yield return null;
		if (!this.isInNormalDialogue)
		{
			this.actorFocuses = null;
		}
		yield break;
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00015E8C File Offset: 0x0001408C
	public IEnumerator RunDialogueOptions(MultilingualString[] options)
	{
		string[] array = new string[options.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = options[i].GetText(Language.Auto);
		}
		return this.RunDialogueOptions(array);
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00015EC8 File Offset: 0x000140C8
	public IEnumerator RunDialogueOptions(string[] options)
	{
		yield return this.dialogueOptions.SetOptions(options);
		DialogueManager.optionChosen = this.dialogueOptions.selectedOption;
		this.dialogueOptions.Clear();
		yield break;
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x00015EDE File Offset: 0x000140DE
	public void SetDialogueCamera(DialogueActor[] actors = null)
	{
		if (actors == null)
		{
			actors = new DialogueActor[0];
		}
		this.actorFocuses = actors;
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x00015EF2 File Offset: 0x000140F2
	public void ClearDialogueCamera()
	{
		this.actorFocuses = null;
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x00015EFB File Offset: 0x000140FB
	public IEnumerator LoadChunkPhone(string chunkName, CharacterProfile[] characters, bool clearAfter = true, bool displayNames = true, Sprite[] images = null)
	{
		if (!this.chunkDic.ContainsKey(chunkName))
		{
			Debug.LogError("Invalid dialogue chunk: " + chunkName);
			yield break;
		}
		yield return base.StartCoroutine(this.LoadChunkPhone(this.chunkDic[chunkName], characters, clearAfter, displayNames, images));
		yield break;
	}

	// Token: 0x060003BA RID: 954 RVA: 0x00015F2F File Offset: 0x0001412F
	public IEnumerator LoadChunkPhone(DialogueChunk chunk, CharacterProfile[] characters, bool clearAfter = true, bool displayNames = true, Sprite[] images = null)
	{
		this.isInNormalDialogue = true;
		if (!this.IsPlayerReady)
		{
			Player.movement.ClearMods();
			this.isWaitingForPlayer = true;
			yield return this.waitUntilPlayerReady;
			this.isWaitingForPlayer = false;
		}
		this.advanceInput = false;
		DialogueManager.optionChosen = -1;
		Game.DialogueDepth++;
		DialogueActor playerActor = DialogueActor.playerActor;
		CharacterProfile player = playerActor.profile;
		Player.itemManager.EquipPhone();
		playerActor.SetEmote(this.textingEmote, false);
		if (DialogueDocumentInfo.d != null)
		{
			DialogueDocumentInfo.d.LoadDocumentInfo(chunk);
		}
		CharacterProfile character = null;
		int num2;
		for (int i = 0; i < chunk.lines.Length; i = num2 + 1)
		{
			if (chunk.lines[i].actorIndex == -1)
			{
				character = null;
			}
			else if (chunk.lines[i].actorIndex == 0 || characters == null)
			{
				character = player;
			}
			else
			{
				if (chunk.lines[i].actorIndex - 1 >= characters.Length)
				{
					Debug.LogError("Warning: Not enough actors for line " + chunk.lines[i].GetText(Language.Auto));
					int num = characters.Length;
				}
				character = characters[chunk.lines[i].actorIndex - 1];
			}
			if (chunk.lines[i].cue)
			{
				this.cue = true;
			}
			if (chunk.lines[i].state != -1 && images != null && images.Length > chunk.lines[i].state)
			{
				yield return base.StartCoroutine(this.scrollingPhoneDisplay.DisplayImage(images[chunk.lines[i].state], character, displayNames, false));
			}
			if (chunk.lines[i].HasText())
			{
				yield return base.StartCoroutine(this.scrollingPhoneDisplay.DisplayTextMessage(chunk.lines[i].GetText(Language.Auto), character, displayNames));
			}
			num2 = i;
		}
		if (clearAfter)
		{
			this.scrollingPhoneDisplay.ClearPhone();
		}
		playerActor.ClearEmote(false, false);
		Game.DialogueDepth--;
		this.isInNormalDialogue = false;
		yield break;
	}

	// Token: 0x060003BB RID: 955 RVA: 0x00015F64 File Offset: 0x00014164
	public YieldInstruction Bubble(string chunkName, DialogueActor[] actors = null, float delay = 0f, bool isImportant = false, bool hasInput = true, bool canInterrupt = true)
	{
		if (!this.chunkDic.ContainsKey(chunkName))
		{
			Debug.LogError("Invalid dialogue chunk: " + chunkName);
			return null;
		}
		DialogueChunk dialogueChunk = this.chunkDic[chunkName];
		return this.Bubble(dialogueChunk, actors, delay, isImportant, hasInput, canInterrupt);
	}

	// Token: 0x060003BC RID: 956 RVA: 0x00015FB0 File Offset: 0x000141B0
	public YieldInstruction Bubble(DialogueChunk chunk, DialogueActor[] actors = null, float delay = 0f, bool isImportant = false, bool hasInput = false, bool canInterrupt = true)
	{
		if (this.bubbleCoroutine != null && canInterrupt)
		{
			this.CancelBubble();
		}
		if (this.bubbleCoroutine == null)
		{
			this.bubbleActors = actors;
			this.bubbleCoroutine = this.LoadChunkBubble(chunk, actors, delay, isImportant, hasInput);
			return base.StartCoroutine(this.bubbleCoroutine);
		}
		return null;
	}

	// Token: 0x060003BD RID: 957 RVA: 0x00016004 File Offset: 0x00014204
	public void CancelBubble()
	{
		if (this.bubbleCoroutine != null)
		{
			base.StopCoroutine(this.bubbleCoroutine);
			this.bubbleCoroutine = null;
			if (this.dialogueBoxBubble != null)
			{
				this.dialogueBoxBubble.Clear(false);
			}
			this.bubbleIsImportant = false;
			this.isInBubbleDialogue = false;
			if (this.bubbleActors != null)
			{
				foreach (DialogueActor dialogueActor in this.bubbleActors)
				{
					if (dialogueActor != null)
					{
						dialogueActor.IsInBubbleDialogue = false;
					}
				}
			}
			this.bubbleActors = null;
		}
	}

	// Token: 0x060003BE RID: 958 RVA: 0x0001608C File Offset: 0x0001428C
	private IEnumerator LoadChunkBubble(DialogueChunk chunk, DialogueActor[] actors, float delay, bool isImportant, bool hasInput = true)
	{
		this.bubbleIsImportant = isImportant;
		this.isInBubbleDialogue = true;
		DialogueActor player = DialogueActor.playerActor;
		player.IsInBubbleDialogue = true;
		int j;
		if (actors != null)
		{
			for (j = 0; j < actors.Length; j++)
			{
				actors[j].IsInBubbleDialogue = true;
			}
		}
		DialogueActor dialogueActor = player;
		DialogueActor previousActor = dialogueActor;
		bool doingAnimation = false;
		if (DialogueDocumentInfo.d != null)
		{
			DialogueDocumentInfo.d.LoadDocumentInfo(chunk);
		}
		if (delay > 0f)
		{
			yield return new WaitForSeconds(delay);
			if (this.isInNormalDialogue && !isImportant)
			{
				yield break;
			}
		}
		for (int i = 0; i < chunk.lines.Length; i = j + 1)
		{
			if (chunk.lines[i].actorIndex == 0 || actors == null)
			{
				dialogueActor = player;
			}
			else
			{
				if (chunk.lines[i].actorIndex - 1 >= actors.Length)
				{
					Debug.LogError("Warning: Not enough actors for line " + chunk.lines[i].GetText(Language.Auto));
					int num = actors.Length;
				}
				dialogueActor = actors[chunk.lines[i].actorIndex - 1];
			}
			DialogueManager.currentlySpeakingBubbleActor = dialogueActor;
			if (previousActor != dialogueActor && doingAnimation && previousActor != null)
			{
				this.ClearActorEmote(previousActor, false);
			}
			int emote = chunk.lines[i].emote;
			if (emote == DialogueManager.defaultEmote || emote == 0)
			{
				if (previousActor == dialogueActor && doingAnimation && dialogueActor != null)
				{
					this.ClearActorEmote(dialogueActor, false);
				}
				doingAnimation = false;
			}
			else
			{
				this.SetActorEmote(dialogueActor, emote);
				doingAnimation = true;
			}
			if (chunk.lines[i].holdEmote && dialogueActor != null)
			{
				dialogueActor.HoldEmote();
			}
			this.SetStateAndPosition(dialogueActor, chunk.lines[i].state, chunk.lines[i].position);
			if (chunk.lines[i].cue)
			{
				this.cue = true;
			}
			previousActor = dialogueActor;
			if (chunk.lines[i].HasText())
			{
				yield return this.dialogueBoxBubble.Load(chunk.lines[i].GetText(Language.Auto), dialogueActor, hasInput, 0f);
			}
			DialogueManager.currentlySpeakingBubbleActor = null;
			j = i;
		}
		if (doingAnimation)
		{
			this.ClearActorEmote(previousActor, false);
		}
		player.IsInBubbleDialogue = false;
		if (actors != null)
		{
			for (j = 0; j < actors.Length; j++)
			{
				actors[j].IsInBubbleDialogue = false;
			}
		}
		this.dialogueBoxBubble.Clear(false);
		this.isInBubbleDialogue = false;
		this.bubbleCoroutine = null;
		this.bubbleIsImportant = false;
		yield break;
	}

	// Token: 0x060003BF RID: 959 RVA: 0x000160C0 File Offset: 0x000142C0
	public YieldInstruction SmallPhone(string chunkName, CharacterProfile[] characters, bool isImportant = false)
	{
		this.CancelSmallPhone();
		if (this.smallPhoneCoroutine == null)
		{
			this.smallPhoneCoroutine = this.LoadChunkSmallPhone(chunkName, characters, isImportant);
			return base.StartCoroutine(this.smallPhoneCoroutine);
		}
		return null;
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x000160ED File Offset: 0x000142ED
	public void CancelSmallPhone()
	{
		if (this.smallPhoneCoroutine != null)
		{
			base.StopCoroutine(this.smallPhoneCoroutine);
			this.smallPhoneCoroutine = null;
			this.smallPhoneIsImportant = false;
		}
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00016111 File Offset: 0x00014311
	private IEnumerator LoadChunkSmallPhone(string chunkName, CharacterProfile[] characters, bool isImportant)
	{
		if (!this.chunkDic.ContainsKey(chunkName))
		{
			Debug.LogError("Invalid dialogue chunk: " + chunkName);
			yield break;
		}
		this.smallPhoneIsImportant = isImportant;
		DialogueActor playerActor = DialogueActor.playerActor;
		CharacterProfile player = playerActor.profile;
		DialogueChunk chunk = this.chunkDic[chunkName];
		CharacterProfile character = null;
		int num2;
		for (int i = 0; i < chunk.lines.Length; i = num2 + 1)
		{
			if (chunk.lines[i].actorIndex == 0 || characters == null)
			{
				character = player;
			}
			else
			{
				if (chunk.lines[i].actorIndex - 1 >= characters.Length)
				{
					Debug.LogError("Warning: Not enough actors for line " + chunk.lines[i].GetText(Language.Auto));
					int num = characters.Length;
				}
				character = characters[chunk.lines[i].actorIndex - 1];
			}
			yield return base.StartCoroutine(this.smallPhoneDialogue.DisplayMessage(chunk.lines[i].GetText(Language.Auto), character));
			num2 = i;
		}
		this.smallPhoneDialogue.Clear();
		this.smallPhoneCoroutine = null;
		this.smallPhoneIsImportant = false;
		yield break;
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00016135 File Offset: 0x00014335
	private void ClearActorEmote(DialogueActor actor, bool overrideHoldEmote = false)
	{
		if (actor != null)
		{
			actor.ClearEmote(overrideHoldEmote, false);
		}
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x00016148 File Offset: 0x00014348
	private void SetActorEmote(DialogueActor actor, int emoteHash = 0)
	{
		if (actor != null)
		{
			if (emoteHash == 0)
			{
				actor.ClearEmote(false, false);
				return;
			}
			if (emoteHash == -1)
			{
				actor.ClearEmote(true, false);
				return;
			}
			actor.SetEmote(emoteHash, false);
		}
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x00016174 File Offset: 0x00014374
	private void SetStateAndPosition(DialogueActor actor, int state, int position)
	{
		if (actor != null)
		{
			actor.SetStateAndPosition(state, position, false, false);
		}
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x0001618C File Offset: 0x0001438C
	private void SetLookTarget(DialogueActor actor, int lookIndex, DialogueActor primaryLookTarget, DialogueActor secondaryLookTarget, DialogueActor player, DialogueActor[] actors, bool isFirstLine)
	{
		DialogueActor dialogueActor = null;
		if (lookIndex == -2)
		{
			dialogueActor = null;
		}
		else if (lookIndex == -1)
		{
			dialogueActor = ((actor != primaryLookTarget) ? primaryLookTarget : secondaryLookTarget);
		}
		else if (lookIndex == 0)
		{
			dialogueActor = player;
		}
		else if (lookIndex <= actors.Length)
		{
			dialogueActor = actors[lookIndex - 1];
		}
		actor.SetActorLookTarget(dialogueActor, isFirstLine);
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x000161D8 File Offset: 0x000143D8
	[ContextMenu("Estimate Word Count")]
	public void EstimateWordCount()
	{
		int num = 0;
		ScriptObject[] array = this.scripts;
		for (int i = 0; i < array.Length; i++)
		{
			foreach (DialogueChunk dialogueChunk in array[i].chunks)
			{
				foreach (DialogueLine dialogueLine in dialogueChunk.lines)
				{
					num += dialogueLine.english[0].Split(new char[] { ' ' }).Length;
				}
				foreach (MultilingualString multilingualString in dialogueChunk.mlOptions)
				{
					num += multilingualString.english[0].Split(new char[] { ' ' }).Length;
				}
			}
		}
		List<int> list = new List<int>();
		List<string> list2 = new List<string>();
		foreach (MultilingualTextDocument multilingualTextDocument in this.documents)
		{
			int num2 = 0;
			foreach (DialogueChunk dialogueChunk2 in multilingualTextDocument.chunks)
			{
				foreach (DialogueLine dialogueLine2 in dialogueChunk2.lines)
				{
					if (!string.IsNullOrEmpty(dialogueLine2.english[0]))
					{
						num2 += dialogueLine2.english[0].Split(new char[] { ' ' }).Length;
					}
				}
				foreach (MultilingualString multilingualString2 in dialogueChunk2.mlOptions)
				{
					if (!string.IsNullOrEmpty(multilingualString2.english[0]))
					{
						num2 += multilingualString2.english[0].Split(new char[] { ' ' }).Length;
					}
				}
			}
			foreach (MultilingualString multilingualString3 in multilingualTextDocument.mlStrings)
			{
				if (!string.IsNullOrEmpty(multilingualString3.english[0]))
				{
					num2 += multilingualString3.english[0].Split(new char[] { ' ' }).Length;
				}
			}
			int num3 = 0;
			while (num3 < list.Count && list[num3] < num2)
			{
				num3++;
			}
			list.Insert(num3, num2);
			list2.Insert(num3, multilingualTextDocument.name);
			num += num2;
		}
		string text = "Estimated word count of " + num.ToString();
		for (int l = 0; l < list.Count; l++)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				list2[l],
				": ",
				list[l].ToString()
			});
		}
		Debug.Log(text);
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x000164D0 File Offset: 0x000146D0
	[ContextMenu("Search Documents For Capitals")]
	public void SearchDocumentsForCapitals()
	{
		foreach (MultilingualTextDocument multilingualTextDocument in this.documents)
		{
			foreach (DialogueChunk dialogueChunk in multilingualTextDocument.chunks)
			{
				for (int k = 0; k < dialogueChunk.lines.Length; k++)
				{
					bool flag = false;
					bool flag2 = false;
					string text = dialogueChunk.lines[k].english[0];
					for (int l = 0; l < text.Length; l++)
					{
						if (char.IsLetter(text[l]))
						{
							bool flag3 = l == 0 || char.IsWhiteSpace(text[l - 1]);
							if (!flag3 || !char.IsUpper(text[l]) || l >= text.Length - 1 || !char.IsLetter(text[l + 1]) || !char.IsUpper(text[l + 1]))
							{
								bool flag4 = flag3 && (l == 0 || (l > 4 && text[l - 2] == '.' && char.IsLetter(text[l - 3])));
								bool flag5 = char.ToUpper(text[l]) == 'I' && (l == 0 || char.IsWhiteSpace(text[l - 1])) && (l == text.Length - 1 || !char.IsLetter(text[l + 1]));
								if (flag4 || flag5)
								{
									if (char.IsUpper(text[l]))
									{
										flag = true;
									}
									else
									{
										flag2 = true;
									}
								}
							}
						}
					}
					if (flag && flag2)
					{
						Debug.Log(string.Format("Found possible inconsistency in document \"{0}\", chunk \"{1}\", line {2:0}: \"{3}\"", new object[] { multilingualTextDocument.name, dialogueChunk.name, k, text }), multilingualTextDocument);
					}
				}
			}
		}
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x000166D0 File Offset: 0x000148D0
	[ContextMenu("Search Documents For Lil Gator")]
	public void SearchDocumentsForLilGator()
	{
		foreach (MultilingualTextDocument multilingualTextDocument in this.documents)
		{
			foreach (DialogueChunk dialogueChunk in multilingualTextDocument.chunks)
			{
				for (int k = 0; k < dialogueChunk.lines.Length; k++)
				{
					string text = dialogueChunk.lines[k].english[0].ToLower();
					if (text.Contains("lil gator") || text.Contains("gator"))
					{
						Debug.Log(string.Format("Found lil gator in document \"{0}\", chunk \"{1}\", line {2:0}: \"{3}\"", new object[] { multilingualTextDocument.name, dialogueChunk.name, k, text }), multilingualTextDocument);
					}
				}
			}
		}
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x000167B0 File Offset: 0x000149B0
	[ContextMenu("Search Documents For Amphitheater")]
	public void SearchDocumentsForAmphitheater()
	{
		foreach (MultilingualTextDocument multilingualTextDocument in this.documents)
		{
			foreach (DialogueChunk dialogueChunk in multilingualTextDocument.chunks)
			{
				for (int k = 0; k < dialogueChunk.lines.Length; k++)
				{
					string text = dialogueChunk.lines[k].english[0].ToLower();
					if (text.Contains("ampetheater") || text.Contains("ampetheater") || text.Contains("amphitheater") || text.Contains("amphitheatre") || text.Contains("theatre"))
					{
						Debug.Log(string.Format("Found in document \"{0}\", chunk \"{1}\", line {2:0}: \"{3}\"", new object[] { multilingualTextDocument.name, dialogueChunk.name, k, text }), multilingualTextDocument);
					}
				}
			}
		}
	}

	// Token: 0x04000523 RID: 1315
	public static DialogueManager d;

	// Token: 0x04000524 RID: 1316
	public static int optionChosen;

	// Token: 0x04000525 RID: 1317
	public DialogueBox dialogueBox;

	// Token: 0x04000526 RID: 1318
	public DialogueOptions dialogueOptions;

	// Token: 0x04000527 RID: 1319
	public UIPhoneDialogue phoneDialogue;

	// Token: 0x04000528 RID: 1320
	public DialogueBox dialogueBoxBubble;

	// Token: 0x04000529 RID: 1321
	public DialogueBox dialogueBoxBigBubble;

	// Token: 0x0400052A RID: 1322
	public UIPhoneDialogue smallPhoneDialogue;

	// Token: 0x0400052B RID: 1323
	public UIScrollingPhoneDisplay scrollingPhoneDisplay;

	// Token: 0x0400052C RID: 1324
	public DialogueBox dialogueBoxWooden;

	// Token: 0x0400052D RID: 1325
	public DialogueBox dialogueBoxNarration;

	// Token: 0x0400052E RID: 1326
	[Header("Database")]
	public ScriptObject[] scripts;

	// Token: 0x0400052F RID: 1327
	public MultilingualTextDocument[] documents;

	// Token: 0x04000530 RID: 1328
	public Dictionary<string, DialogueChunk> chunkDic;

	// Token: 0x04000531 RID: 1329
	private bool advanceInput;

	// Token: 0x04000532 RID: 1330
	private bool skipInput;

	// Token: 0x04000533 RID: 1331
	public float bubbleDelay = 3f;

	// Token: 0x04000534 RID: 1332
	public float smallPhoneDelay = 3f;

	// Token: 0x04000535 RID: 1333
	private WaitForSeconds waitForBubbleDelay;

	// Token: 0x04000536 RID: 1334
	private WaitForSeconds waitForSmallPhoneDelay;

	// Token: 0x04000537 RID: 1335
	internal WaitUntil waitUntilBubbleFinish;

	// Token: 0x04000538 RID: 1336
	internal WaitUntil waitUntilButtonPress;

	// Token: 0x04000539 RID: 1337
	internal WaitUntil waitUntilPlayerReady;

	// Token: 0x0400053A RID: 1338
	private bool isInNormalDialogue;

	// Token: 0x0400053B RID: 1339
	public bool isInBubbleDialogue;

	// Token: 0x0400053C RID: 1340
	private IEnumerator bubbleCoroutine;

	// Token: 0x0400053D RID: 1341
	private IEnumerator smallPhoneCoroutine;

	// Token: 0x0400053E RID: 1342
	private DialogueActor[] bubbleActors;

	// Token: 0x0400053F RID: 1343
	private bool bubbleIsImportant;

	// Token: 0x04000540 RID: 1344
	private bool smallPhoneIsImportant;

	// Token: 0x04000541 RID: 1345
	public bool isWaitingForPlayer;

	// Token: 0x04000542 RID: 1346
	public static DialogueActor currentlySpeakingActor;

	// Token: 0x04000543 RID: 1347
	public static DialogueActor currentlySpeakingBubbleActor;

	// Token: 0x04000544 RID: 1348
	public static int currentlySpeakingActorIndex;

	// Token: 0x04000545 RID: 1349
	public bool cue;

	// Token: 0x04000546 RID: 1350
	public WaitUntil waitUntilCue;

	// Token: 0x04000547 RID: 1351
	private static readonly int defaultEmote;

	// Token: 0x04000548 RID: 1352
	private DialogueActor[] actorFocuses;

	// Token: 0x04000549 RID: 1353
	private Vector3 cameraFocusPoint;

	// Token: 0x0400054A RID: 1354
	private float validCameraFocusPointTime = -1f;

	// Token: 0x0400054B RID: 1355
	private DialogueActor[] temporaryActor = new DialogueActor[1];

	// Token: 0x0400054C RID: 1356
	private readonly int textingEmote = Animator.StringToHash("E_Texting");

	// Token: 0x0400054D RID: 1357
	private int stateID = Animator.StringToHash("State");

	// Token: 0x0200038A RID: 906
	public enum DialogueBoxBackground
	{
		// Token: 0x04001ABE RID: 6846
		Standard,
		// Token: 0x04001ABF RID: 6847
		Wooden,
		// Token: 0x04001AC0 RID: 6848
		Narration,
		// Token: 0x04001AC1 RID: 6849
		Bubble,
		// Token: 0x04001AC2 RID: 6850
		BigBubble
	}
}
