using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000167 RID: 359
[AddComponentMenu("Logic/Fork Event By State")]
public class LogicFork : MonoBehaviour
{
	// Token: 0x06000762 RID: 1890 RVA: 0x00024A6F File Offset: 0x00022C6F
	private void OnDisable()
	{
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x00024A74 File Offset: 0x00022C74
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

	// Token: 0x06000764 RID: 1892 RVA: 0x00024B98 File Offset: 0x00022D98
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

	// Token: 0x06000765 RID: 1893 RVA: 0x00024C21 File Offset: 0x00022E21
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

	// Token: 0x06000766 RID: 1894 RVA: 0x00024C40 File Offset: 0x00022E40
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

	// Token: 0x06000767 RID: 1895 RVA: 0x00024CE0 File Offset: 0x00022EE0
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

	// Token: 0x040009A0 RID: 2464
	public QuestStates stateMachine;

	// Token: 0x040009A1 RID: 2465
	public DialogueActor[] actors;

	// Token: 0x040009A2 RID: 2466
	public MultilingualTextDocument document;

	// Token: 0x040009A3 RID: 2467
	public LogicFork.StateAction[] stateActions;

	// Token: 0x020003C6 RID: 966
	[Serializable]
	public struct StateAction
	{
		// Token: 0x04001BC7 RID: 7111
		[ReadOnly]
		public string stateName;

		// Token: 0x04001BC8 RID: 7112
		[Space]
		public int[] applicableStates;

		// Token: 0x04001BC9 RID: 7113
		[Space]
		public bool progressState;

		// Token: 0x04001BCA RID: 7114
		[ConditionalHide("progressState", true)]
		public bool overrideNewState;

		// Token: 0x04001BCB RID: 7115
		[ConditionalHide("overrideNewState", true, ConditionalSourceField2 = "progressState")]
		public int newState;

		// Token: 0x04001BCC RID: 7116
		[ConditionalHide("progressState", true)]
		public bool progressStateFirst;

		// Token: 0x04001BCD RID: 7117
		[Space]
		public GameObject[] actionObjects;

		// Token: 0x04001BCE RID: 7118
		[Header("Dialogue Triggers")]
		[ChunkLookup("document")]
		public string dialogue;

		// Token: 0x04001BCF RID: 7119
		[ChunkLookup("document")]
		public string[] additionalDialogue;

		// Token: 0x04001BD0 RID: 7120
		public bool dialogueIsBubble;

		// Token: 0x04001BD1 RID: 7121
		public bool skipWaitForPlayer;

		// Token: 0x04001BD2 RID: 7122
		[ReadOnly]
		public int dialogueIndex;

		// Token: 0x04001BD3 RID: 7123
		public bool loopDialogue;

		// Token: 0x04001BD4 RID: 7124
		public DialogueSequencer dialogueSequencer;

		// Token: 0x04001BD5 RID: 7125
		[Header("Event Triggers")]
		public UnityEvent onAction;

		// Token: 0x04001BD6 RID: 7126
		public UnityEvent afterAction;
	}
}
