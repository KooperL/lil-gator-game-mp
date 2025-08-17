using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Logic/Fork Event By State")]
public class LogicFork : MonoBehaviour
{
	// Token: 0x060008F4 RID: 2292 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDisable()
	{
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x00039578 File Offset: 0x00037778
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

	// Token: 0x060008F6 RID: 2294 RVA: 0x0003969C File Offset: 0x0003789C
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

	// Token: 0x060008F7 RID: 2295 RVA: 0x00008B68 File Offset: 0x00006D68
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

	// Token: 0x060008F8 RID: 2296 RVA: 0x00039728 File Offset: 0x00037928
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

	// Token: 0x060008F9 RID: 2297 RVA: 0x000397C8 File Offset: 0x000379C8
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

	public QuestStates stateMachine;

	public DialogueActor[] actors;

	public MultilingualTextDocument document;

	public LogicFork.StateAction[] stateActions;

	[Serializable]
	public struct StateAction
	{
		[ReadOnly]
		public string stateName;

		[Space]
		public int[] applicableStates;

		[Space]
		public bool progressState;

		[ConditionalHide("progressState", true)]
		public bool overrideNewState;

		[ConditionalHide("overrideNewState", true, ConditionalSourceField2 = "progressState")]
		public int newState;

		[ConditionalHide("progressState", true)]
		public bool progressStateFirst;

		[Space]
		public GameObject[] actionObjects;

		[Header("Dialogue Triggers")]
		[ChunkLookup("document")]
		public string dialogue;

		[ChunkLookup("document")]
		public string[] additionalDialogue;

		public bool dialogueIsBubble;

		public bool skipWaitForPlayer;

		[ReadOnly]
		public int dialogueIndex;

		public bool loopDialogue;

		public DialogueSequencer dialogueSequencer;

		[Header("Event Triggers")]
		public UnityEvent onAction;

		public UnityEvent afterAction;
	}
}
