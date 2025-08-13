using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E4 RID: 228
public class DialogueManager : MonoBehaviour
{
	// Token: 0x17000058 RID: 88
	// (get) Token: 0x0600041F RID: 1055 RVA: 0x00005094 File Offset: 0x00003294
	public bool IsInAmbientDialogue
	{
		get
		{
			return this.bubbleCoroutine != null || this.smallPhoneCoroutine != null;
		}
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06000420 RID: 1056 RVA: 0x000050A9 File Offset: 0x000032A9
	public bool IsInImportantDialogue
	{
		get
		{
			return this.bubbleIsImportant || this.smallPhoneIsImportant || this.isInNormalDialogue;
		}
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x06000421 RID: 1057 RVA: 0x000050C3 File Offset: 0x000032C3
	public bool CanAcceptBubbleDialogue
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x06000422 RID: 1058 RVA: 0x000050C6 File Offset: 0x000032C6
	internal bool IsPlayerReady
	{
		get
		{
			return Player.movement.IsGrounded || Player.movement.IsSubmerged || Player.movement.modIgnoreReady;
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x06000423 RID: 1059 RVA: 0x000050EC File Offset: 0x000032EC
	public bool HasFocusPoint
	{
		get
		{
			return this.actorFocuses != null || Time.time - this.validCameraFocusPointTime < 0.1f;
		}
	}

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x06000424 RID: 1060 RVA: 0x00028F54 File Offset: 0x00027154
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

	// Token: 0x06000425 RID: 1061 RVA: 0x00029068 File Offset: 0x00027268
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

	// Token: 0x06000426 RID: 1062 RVA: 0x0000510B File Offset: 0x0000330B
	private void OnDisable()
	{
		this.CancelBubble();
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x00005113 File Offset: 0x00003313
	public IEnumerator LoadChunk(string chunkName, DialogueActor other, DialogueManager.DialogueBoxBackground background = DialogueManager.DialogueBoxBackground.Standard, bool cancelBubble = true)
	{
		this.temporaryActor[0] = other;
		return this.LoadChunk(chunkName, this.temporaryActor, background, cancelBubble);
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x0000512E File Offset: 0x0000332E
	public IEnumerator WaitForPlayer()
	{
		Player.movement.ClearMods();
		this.isWaitingForPlayer = true;
		yield return this.waitUntilPlayerReady;
		this.isWaitingForPlayer = false;
		yield break;
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x0000513D File Offset: 0x0000333D
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

	// Token: 0x0600042A RID: 1066 RVA: 0x000291FC File Offset: 0x000273FC
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
					Debug.LogError("Warning: Not enough actors for line " + chunk.lines[i].GetText(Language.English));
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
				yield return workingDialogueBox.Load(chunk.lines[i].GetText(Language.English), currentActor, skipWaits || !chunk.lines[i].noInput, skipWaits ? 0f : chunk.lines[i].waitTime);
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

	// Token: 0x0600042B RID: 1067 RVA: 0x00005169 File Offset: 0x00003369
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

	// Token: 0x0600042C RID: 1068 RVA: 0x0002924C File Offset: 0x0002744C
	public IEnumerator RunDialogueOptions(MultilingualString[] options)
	{
		string[] array = new string[options.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = options[i].GetText(Language.English);
		}
		return this.RunDialogueOptions(array);
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x00005178 File Offset: 0x00003378
	public IEnumerator RunDialogueOptions(string[] options)
	{
		yield return this.dialogueOptions.SetOptions(options);
		DialogueManager.optionChosen = this.dialogueOptions.selectedOption;
		this.dialogueOptions.Clear();
		yield break;
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x0000518E File Offset: 0x0000338E
	public void SetDialogueCamera(DialogueActor[] actors = null)
	{
		if (actors == null)
		{
			actors = new DialogueActor[0];
		}
		this.actorFocuses = actors;
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x000051A2 File Offset: 0x000033A2
	public void ClearDialogueCamera()
	{
		this.actorFocuses = null;
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x000051AB File Offset: 0x000033AB
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

	// Token: 0x06000431 RID: 1073 RVA: 0x000051DF File Offset: 0x000033DF
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
					Debug.LogError("Warning: Not enough actors for line " + chunk.lines[i].GetText(Language.English));
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
				yield return base.StartCoroutine(this.scrollingPhoneDisplay.DisplayTextMessage(chunk.lines[i].GetText(Language.English), character, displayNames));
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

	// Token: 0x06000432 RID: 1074 RVA: 0x00029288 File Offset: 0x00027488
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

	// Token: 0x06000433 RID: 1075 RVA: 0x000292D4 File Offset: 0x000274D4
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

	// Token: 0x06000434 RID: 1076 RVA: 0x00029328 File Offset: 0x00027528
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

	// Token: 0x06000435 RID: 1077 RVA: 0x00005213 File Offset: 0x00003413
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
					Debug.LogError("Warning: Not enough actors for line " + chunk.lines[i].GetText(Language.English));
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
				yield return this.dialogueBoxBubble.Load(chunk.lines[i].GetText(Language.English), dialogueActor, hasInput, 0f);
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

	// Token: 0x06000436 RID: 1078 RVA: 0x00005247 File Offset: 0x00003447
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

	// Token: 0x06000437 RID: 1079 RVA: 0x00005274 File Offset: 0x00003474
	public void CancelSmallPhone()
	{
		if (this.smallPhoneCoroutine != null)
		{
			base.StopCoroutine(this.smallPhoneCoroutine);
			this.smallPhoneCoroutine = null;
			this.smallPhoneIsImportant = false;
		}
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x00005298 File Offset: 0x00003498
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
					Debug.LogError("Warning: Not enough actors for line " + chunk.lines[i].GetText(Language.English));
					int num = characters.Length;
				}
				character = characters[chunk.lines[i].actorIndex - 1];
			}
			yield return base.StartCoroutine(this.smallPhoneDialogue.DisplayMessage(chunk.lines[i].GetText(Language.English), character));
			num2 = i;
		}
		this.smallPhoneDialogue.Clear();
		this.smallPhoneCoroutine = null;
		this.smallPhoneIsImportant = false;
		yield break;
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x000052BC File Offset: 0x000034BC
	private void ClearActorEmote(DialogueActor actor, bool overrideHoldEmote = false)
	{
		if (actor != null)
		{
			actor.ClearEmote(overrideHoldEmote, false);
		}
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x000052CF File Offset: 0x000034CF
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

	// Token: 0x0600043B RID: 1083 RVA: 0x000052FB File Offset: 0x000034FB
	private void SetStateAndPosition(DialogueActor actor, int state, int position)
	{
		if (actor != null)
		{
			actor.SetStateAndPosition(state, position, false, false);
		}
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x000293B0 File Offset: 0x000275B0
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

	// Token: 0x0600043D RID: 1085 RVA: 0x000293FC File Offset: 0x000275FC
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
					num += dialogueLine.english.Split(new char[] { ' ' }).Length;
				}
				foreach (MultilingualString multilingualString in dialogueChunk.mlOptions)
				{
					num += multilingualString.english.Split(new char[] { ' ' }).Length;
				}
			}
		}
		MultilingualTextDocument[] array5 = this.documents;
		for (int i = 0; i < array5.Length; i++)
		{
			foreach (DialogueChunk dialogueChunk2 in array5[i].chunks)
			{
				foreach (DialogueLine dialogueLine2 in dialogueChunk2.lines)
				{
					num += dialogueLine2.english.Split(new char[] { ' ' }).Length;
				}
				foreach (MultilingualString multilingualString2 in dialogueChunk2.mlOptions)
				{
					num += multilingualString2.english.Split(new char[] { ' ' }).Length;
				}
			}
		}
		Debug.Log("Estimated word count of " + num.ToString());
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x000295B4 File Offset: 0x000277B4
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
					string english = dialogueChunk.lines[k].english;
					for (int l = 0; l < english.Length; l++)
					{
						if (char.IsLetter(english[l]))
						{
							bool flag3 = l == 0 || char.IsWhiteSpace(english[l - 1]);
							if (!flag3 || !char.IsUpper(english[l]) || l >= english.Length - 1 || !char.IsLetter(english[l + 1]) || !char.IsUpper(english[l + 1]))
							{
								bool flag4 = flag3 && (l == 0 || (l > 4 && english[l - 2] == '.' && char.IsLetter(english[l - 3])));
								bool flag5 = char.ToUpper(english[l]) == 'I' && (l == 0 || char.IsWhiteSpace(english[l - 1])) && (l == english.Length - 1 || !char.IsLetter(english[l + 1]));
								if (flag4 || flag5)
								{
									if (char.IsUpper(english[l]))
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
						Debug.Log(string.Format("Found possible inconsistency in document \"{0}\", chunk \"{1}\", line {2:0}: \"{3}\"", new object[] { multilingualTextDocument.name, dialogueChunk.name, k, english }), multilingualTextDocument);
					}
				}
			}
		}
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x000297B0 File Offset: 0x000279B0
	[ContextMenu("Search Documents For Lil Gator")]
	public void SearchDocumentsForLilGator()
	{
		foreach (MultilingualTextDocument multilingualTextDocument in this.documents)
		{
			foreach (DialogueChunk dialogueChunk in multilingualTextDocument.chunks)
			{
				for (int k = 0; k < dialogueChunk.lines.Length; k++)
				{
					string text = dialogueChunk.lines[k].english.ToLower();
					if (text.Contains("lil gator") || text.Contains("gator"))
					{
						Debug.Log(string.Format("Found lil gator in document \"{0}\", chunk \"{1}\", line {2:0}: \"{3}\"", new object[] { multilingualTextDocument.name, dialogueChunk.name, k, text }), multilingualTextDocument);
					}
				}
			}
		}
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x00029890 File Offset: 0x00027A90
	[ContextMenu("Search Documents For Amphitheater")]
	public void SearchDocumentsForAmphitheater()
	{
		foreach (MultilingualTextDocument multilingualTextDocument in this.documents)
		{
			foreach (DialogueChunk dialogueChunk in multilingualTextDocument.chunks)
			{
				for (int k = 0; k < dialogueChunk.lines.Length; k++)
				{
					string text = dialogueChunk.lines[k].english.ToLower();
					if (text.Contains("ampetheater") || text.Contains("ampetheater") || text.Contains("amphitheater") || text.Contains("amphitheatre") || text.Contains("theatre"))
					{
						Debug.Log(string.Format("Found in document \"{0}\", chunk \"{1}\", line {2:0}: \"{3}\"", new object[] { multilingualTextDocument.name, dialogueChunk.name, k, text }), multilingualTextDocument);
					}
				}
			}
		}
	}

	// Token: 0x040005E0 RID: 1504
	public static DialogueManager d;

	// Token: 0x040005E1 RID: 1505
	public static int optionChosen;

	// Token: 0x040005E2 RID: 1506
	public DialogueBox dialogueBox;

	// Token: 0x040005E3 RID: 1507
	public DialogueOptions dialogueOptions;

	// Token: 0x040005E4 RID: 1508
	public UIPhoneDialogue phoneDialogue;

	// Token: 0x040005E5 RID: 1509
	public DialogueBox dialogueBoxBubble;

	// Token: 0x040005E6 RID: 1510
	public DialogueBox dialogueBoxBigBubble;

	// Token: 0x040005E7 RID: 1511
	public UIPhoneDialogue smallPhoneDialogue;

	// Token: 0x040005E8 RID: 1512
	public UIScrollingPhoneDisplay scrollingPhoneDisplay;

	// Token: 0x040005E9 RID: 1513
	public DialogueBox dialogueBoxWooden;

	// Token: 0x040005EA RID: 1514
	public DialogueBox dialogueBoxNarration;

	// Token: 0x040005EB RID: 1515
	[Header("Database")]
	public ScriptObject[] scripts;

	// Token: 0x040005EC RID: 1516
	public MultilingualTextDocument[] documents;

	// Token: 0x040005ED RID: 1517
	public Dictionary<string, DialogueChunk> chunkDic;

	// Token: 0x040005EE RID: 1518
	private bool advanceInput;

	// Token: 0x040005EF RID: 1519
	private bool skipInput;

	// Token: 0x040005F0 RID: 1520
	public float bubbleDelay = 3f;

	// Token: 0x040005F1 RID: 1521
	public float smallPhoneDelay = 3f;

	// Token: 0x040005F2 RID: 1522
	private WaitForSeconds waitForBubbleDelay;

	// Token: 0x040005F3 RID: 1523
	private WaitForSeconds waitForSmallPhoneDelay;

	// Token: 0x040005F4 RID: 1524
	internal WaitUntil waitUntilBubbleFinish;

	// Token: 0x040005F5 RID: 1525
	internal WaitUntil waitUntilButtonPress;

	// Token: 0x040005F6 RID: 1526
	internal WaitUntil waitUntilPlayerReady;

	// Token: 0x040005F7 RID: 1527
	private bool isInNormalDialogue;

	// Token: 0x040005F8 RID: 1528
	public bool isInBubbleDialogue;

	// Token: 0x040005F9 RID: 1529
	private IEnumerator bubbleCoroutine;

	// Token: 0x040005FA RID: 1530
	private IEnumerator smallPhoneCoroutine;

	// Token: 0x040005FB RID: 1531
	private DialogueActor[] bubbleActors;

	// Token: 0x040005FC RID: 1532
	private bool bubbleIsImportant;

	// Token: 0x040005FD RID: 1533
	private bool smallPhoneIsImportant;

	// Token: 0x040005FE RID: 1534
	public bool isWaitingForPlayer;

	// Token: 0x040005FF RID: 1535
	public static DialogueActor currentlySpeakingActor;

	// Token: 0x04000600 RID: 1536
	public static DialogueActor currentlySpeakingBubbleActor;

	// Token: 0x04000601 RID: 1537
	public static int currentlySpeakingActorIndex;

	// Token: 0x04000602 RID: 1538
	public bool cue;

	// Token: 0x04000603 RID: 1539
	public WaitUntil waitUntilCue;

	// Token: 0x04000604 RID: 1540
	private static readonly int defaultEmote;

	// Token: 0x04000605 RID: 1541
	private DialogueActor[] actorFocuses;

	// Token: 0x04000606 RID: 1542
	private Vector3 cameraFocusPoint;

	// Token: 0x04000607 RID: 1543
	private float validCameraFocusPointTime = -1f;

	// Token: 0x04000608 RID: 1544
	private DialogueActor[] temporaryActor = new DialogueActor[1];

	// Token: 0x04000609 RID: 1545
	private readonly int textingEmote = Animator.StringToHash("E_Texting");

	// Token: 0x0400060A RID: 1546
	private int stateID = Animator.StringToHash("State");

	// Token: 0x020000E5 RID: 229
	public enum DialogueBoxBackground
	{
		// Token: 0x0400060C RID: 1548
		Standard,
		// Token: 0x0400060D RID: 1549
		Wooden,
		// Token: 0x0400060E RID: 1550
		Narration,
		// Token: 0x0400060F RID: 1551
		Bubble,
		// Token: 0x04000610 RID: 1552
		BigBubble
	}
}
