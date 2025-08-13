using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001D6 RID: 470
[AddComponentMenu("Logic/Fork Event By State")]
public class LogicFork : MonoBehaviour
{
	// Token: 0x060008B4 RID: 2228 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDisable()
	{
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x00037C08 File Offset: 0x00035E08
	protected virtual void OnValidate()
	{
		if (this.stateMachine == null)
		{
			this.stateMachine = base.transform.GetComponentUpHeirarchy<QuestStates>();
		}
		if (this.stateMachine != null && this.stateActions != null)
		{
			int num = 0;
			while (num < this.stateActions.Length && num < this.stateMachine.states.Length)
			{
				if (this.stateActions[num].applicableStates.Length == 0)
				{
					this.stateActions[num].stateName = this.stateMachine.states[num].name;
				}
				else
				{
					this.stateActions[num].applicableStates[0] = Mathf.Clamp(this.stateActions[num].applicableStates[0], 0, this.stateMachine.states.Length - 1);
					this.stateActions[num].stateName = this.stateMachine.states[this.stateActions[num].applicableStates[0]].name;
				}
				num++;
			}
		}
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x00037D2C File Offset: 0x00035F2C
	public void Action()
	{
		int stateID = this.stateMachine.StateID;
		for (int i = 0; i < this.stateActions.Length; i++)
		{
			LogicFork.StateAction stateAction = this.stateActions[i];
			bool flag = false;
			if (stateAction.applicableStates.Length == 0)
			{
				if (stateID == i)
				{
					flag = true;
				}
			}
			else
			{
				int[] applicableStates = stateAction.applicableStates;
				for (int j = 0; j < applicableStates.Length; j++)
				{
					if (applicableStates[j] == stateID)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				CoroutineUtil.c.StartCo(this.RunExecuteAction(stateAction, i));
			}
		}
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0000883F File Offset: 0x00006A3F
	private IEnumerator RunExecuteAction(LogicFork.StateAction action, int actionIndex)
	{
		action.onAction.Invoke();
		if (action.actionObjects != null)
		{
			GameObject[] array = action.actionObjects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(true);
			}
		}
		if (action.progressState && action.progressStateFirst)
		{
			if (action.overrideNewState)
			{
				this.stateMachine.ProgressState(action.newState);
			}
			else
			{
				this.stateMachine.JustProgressState();
			}
		}
		if ((!string.IsNullOrEmpty(action.dialogue) && this.document.HasChunk(action.dialogue)) || action.additionalDialogue.Length != 0 || action.dialogueSequencer != null)
		{
			if (!action.dialogueIsBubble)
			{
				Game.DialogueDepth++;
			}
			if (action.additionalDialogue.Length != 0)
			{
				int index = action.dialogueIndex;
				int totalDialogueCount = action.additionalDialogue.Length;
				int i;
				if (!string.IsNullOrEmpty(action.dialogue) || action.dialogueSequencer != null)
				{
					i = index;
					index = i - 1;
					i = totalDialogueCount;
					totalDialogueCount = i + 1;
				}
				if (index == -1)
				{
					if (!string.IsNullOrEmpty(action.dialogue))
					{
						yield return this.RunDialogueChunk(action.dialogue, action.dialogueIsBubble, action.skipWaitForPlayer);
					}
				}
				else if (!string.IsNullOrEmpty(action.additionalDialogue[index]))
				{
					yield return this.RunDialogueChunk(action.additionalDialogue[index], action.dialogueIsBubble, action.skipWaitForPlayer);
				}
				i = index;
				index = i + 1;
				if (!string.IsNullOrEmpty(action.dialogue) || action.dialogueSequencer != null)
				{
					i = index;
					index = i + 1;
				}
				if (index >= totalDialogueCount)
				{
					if (action.loopDialogue)
					{
						index = 0;
					}
					else
					{
						index = totalDialogueCount - 1;
					}
				}
				this.stateActions[actionIndex].dialogueIndex = index;
			}
			else if (!string.IsNullOrEmpty(action.dialogue))
			{
				yield return this.RunDialogueChunk(action.dialogue, action.dialogueIsBubble, action.skipWaitForPlayer);
			}
			if (action.dialogueSequencer != null)
			{
				yield return action.dialogueSequencer.StartSequence();
			}
			if (!action.dialogueIsBubble)
			{
				Game.DialogueDepth--;
			}
		}
		action.afterAction.Invoke();
		if (action.actionObjects != null)
		{
			GameObject[] array = action.actionObjects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(false);
			}
		}
		if (action.progressState && !action.progressStateFirst)
		{
			if (action.overrideNewState)
			{
				this.stateMachine.ProgressState(action.newState);
			}
			else
			{
				this.stateMachine.JustProgressState();
			}
		}
		yield return null;
		yield break;
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x00037DB8 File Offset: 0x00035FB8
	private YieldInstruction RunDialogueChunk(string dialogue, bool isBubble = false, bool skipWaitForPlayer = false)
	{
		if (this.document == null)
		{
			if (isBubble)
			{
				return DialogueManager.d.Bubble(dialogue, this.actors, 0f, false, true, true);
			}
			return CoroutineUtil.Start(DialogueManager.d.LoadChunk(dialogue, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		}
		else
		{
			if (isBubble)
			{
				return DialogueManager.d.Bubble(this.document.FetchChunk(dialogue), this.actors, 0f, false, false, true);
			}
			return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(dialogue), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, skipWaitForPlayer));
		}
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x00037E58 File Offset: 0x00036058
	public void ProgressIndex()
	{
		int stateID = this.stateMachine.StateID;
		for (int i = 0; i < this.stateActions.Length; i++)
		{
			LogicFork.StateAction stateAction = this.stateActions[i];
			bool flag = false;
			if (stateAction.applicableStates.Length == 0)
			{
				if (stateID == i)
				{
					flag = true;
				}
			}
			else
			{
				int[] applicableStates = stateAction.applicableStates;
				for (int j = 0; j < applicableStates.Length; j++)
				{
					if (applicableStates[j] == stateID)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				LogicFork.StateAction[] array = this.stateActions;
				int num = i;
				array[num].dialogueIndex = array[num].dialogueIndex + 1;
			}
		}
	}

	// Token: 0x04000B4A RID: 2890
	public QuestStates stateMachine;

	// Token: 0x04000B4B RID: 2891
	public DialogueActor[] actors;

	// Token: 0x04000B4C RID: 2892
	public MultilingualTextDocument document;

	// Token: 0x04000B4D RID: 2893
	public LogicFork.StateAction[] stateActions;

	// Token: 0x020001D7 RID: 471
	[Serializable]
	public struct StateAction
	{
		// Token: 0x04000B4E RID: 2894
		[ReadOnly]
		public string stateName;

		// Token: 0x04000B4F RID: 2895
		[Space]
		public int[] applicableStates;

		// Token: 0x04000B50 RID: 2896
		[Space]
		public bool progressState;

		// Token: 0x04000B51 RID: 2897
		[ConditionalHide("progressState", true)]
		public bool overrideNewState;

		// Token: 0x04000B52 RID: 2898
		[ConditionalHide("overrideNewState", true, ConditionalSourceField2 = "progressState")]
		public int newState;

		// Token: 0x04000B53 RID: 2899
		[ConditionalHide("progressState", true)]
		public bool progressStateFirst;

		// Token: 0x04000B54 RID: 2900
		[Space]
		public GameObject[] actionObjects;

		// Token: 0x04000B55 RID: 2901
		[Header("Dialogue Triggers")]
		[ChunkLookup("document")]
		public string dialogue;

		// Token: 0x04000B56 RID: 2902
		[ChunkLookup("document")]
		public string[] additionalDialogue;

		// Token: 0x04000B57 RID: 2903
		public bool dialogueIsBubble;

		// Token: 0x04000B58 RID: 2904
		public bool skipWaitForPlayer;

		// Token: 0x04000B59 RID: 2905
		[ReadOnly]
		public int dialogueIndex;

		// Token: 0x04000B5A RID: 2906
		public bool loopDialogue;

		// Token: 0x04000B5B RID: 2907
		public DialogueSequencer dialogueSequencer;

		// Token: 0x04000B5C RID: 2908
		[Header("Event Triggers")]
		public UnityEvent onAction;

		// Token: 0x04000B5D RID: 2909
		public UnityEvent afterAction;
	}
}
