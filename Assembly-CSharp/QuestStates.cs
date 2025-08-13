using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001DB RID: 475
[AddComponentMenu("Logic/State Machine")]
public class QuestStates : MonoBehaviour
{
	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x060008CB RID: 2251 RVA: 0x000088FC File Offset: 0x00006AFC
	// (set) Token: 0x060008CC RID: 2252 RVA: 0x00008928 File Offset: 0x00006B28
	public int StateID
	{
		get
		{
			if (this.id == "")
			{
				return this.localState;
			}
			return GameData.g.ReadInt(this.id, 0);
		}
		set
		{
			this.localState = value;
			if (this.id != "")
			{
				GameData.g.Write(this.id, value);
			}
		}
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x00008954 File Offset: 0x00006B54
	[ContextMenu("Debug State")]
	public void DebugState()
	{
		Debug.Log(this.StateID);
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x00008966 File Offset: 0x00006B66
	private void OnEnable()
	{
		if (this.isShared)
		{
			QuestStates.shared.Add(this);
		}
		this.SetState(this.StateID, true, false);
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x00008989 File Offset: 0x00006B89
	private void OnDisable()
	{
		if (this.isShared)
		{
			QuestStates.shared.Remove(this);
		}
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x0000899F File Offset: 0x00006B9F
	[ContextMenu("Progress State")]
	public void JustProgressState()
	{
		this.ProgressState(-1);
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x000089A9 File Offset: 0x00006BA9
	public void ProgressToState(int nextStateID)
	{
		this.ProgressState(nextStateID);
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x000383B8 File Offset: 0x000365B8
	public YieldInstruction ProgressState(int nextStateID = -1)
	{
		if (nextStateID == -1)
		{
			nextStateID = this.StateID + 1;
		}
		if (nextStateID >= this.states.Length)
		{
			return null;
		}
		if (this.states[nextStateID].fadeOnTransition)
		{
			return CoroutineUtil.c.StartCo(this.RunFadeTransition(nextStateID));
		}
		this.SetState(nextStateID, false, false);
		return null;
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x000089B3 File Offset: 0x00006BB3
	[ContextMenu("Progress To End")]
	public void ProgressToEnd()
	{
		this.ProgressState(this.states.Length - 1);
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x000089C6 File Offset: 0x00006BC6
	private IEnumerator RunFadeTransition(int nextStateID)
	{
		Game.DialogueDepth++;
		yield return Blackout.FadeIn();
		this.SetState(nextStateID, false, false);
		yield return Blackout.FadeOut();
		Game.DialogueDepth--;
		yield break;
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x00038410 File Offset: 0x00036610
	private void SetState(int stateID, bool initial = false, bool fromShared = false)
	{
		int num = -1;
		if (!initial)
		{
			num = this.StateID;
			this.StateID = stateID;
		}
		if (num == stateID)
		{
			num = -1;
		}
		QuestStates.QuestState questState;
		GameObject[] array;
		for (int i = 0; i < this.states.Length; i++)
		{
			questState = this.states[i];
			array = questState.firstStateObjects;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].SetActive(stateID >= i);
			}
			array = questState.lastStateObjects;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].SetActive(stateID <= i);
			}
			if (i != stateID)
			{
				foreach (GameObject gameObject in questState.stateObjects)
				{
					if (!this.states[stateID].stateObjects.Contains(gameObject))
					{
						gameObject.SetActive(false);
					}
				}
				foreach (GameObject gameObject2 in questState.nonStateObjects)
				{
					if (!this.states[stateID].nonStateObjects.Contains(gameObject2))
					{
						gameObject2.SetActive(true);
					}
				}
				if (i == num && questState.onDeactivate != null)
				{
					questState.onDeactivate.Invoke();
				}
			}
		}
		questState = this.states[stateID];
		array = questState.stateObjects;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].SetActive(true);
		}
		array = questState.nonStateObjects;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].SetActive(false);
		}
		if (questState.onActivate != null)
		{
			questState.onActivate.Invoke();
		}
		if (!fromShared && !initial && questState.onProgress != null)
		{
			questState.onProgress.Invoke();
		}
		if (!fromShared && !initial && stateID == this.states.Length - 1 && this.giveRewardsOnLastState)
		{
			QuestRewards component = base.GetComponent<QuestRewards>();
			if (component != null)
			{
				component.GiveAllRewards();
			}
		}
		this.onStateChange.Invoke(stateID);
		if (this.isShared)
		{
			foreach (QuestStates questStates in QuestStates.shared)
			{
				if (questStates != this && questStates.id == this.id)
				{
					questStates.SetState(stateID, false, true);
				}
			}
		}
		if (SpeedrunData.ShouldTrack && stateID == this.states.Length - 1 && !SpeedrunData.completedQuests.Contains(this.id))
		{
			SpeedrunData.completedQuests.Add(this.id);
		}
	}

	// Token: 0x04000B6D RID: 2925
	private static List<QuestStates> shared = new List<QuestStates>();

	// Token: 0x04000B6E RID: 2926
	public QuestStates.QuestState[] states;

	// Token: 0x04000B6F RID: 2927
	[HideInInspector]
	public QuestStates.StateChangeEvent onStateChange = new QuestStates.StateChangeEvent();

	// Token: 0x04000B70 RID: 2928
	public string id;

	// Token: 0x04000B71 RID: 2929
	private int localState;

	// Token: 0x04000B72 RID: 2930
	public bool giveRewardsOnLastState;

	// Token: 0x04000B73 RID: 2931
	public bool isShared;

	// Token: 0x020001DC RID: 476
	[Serializable]
	public struct QuestState
	{
		// Token: 0x04000B74 RID: 2932
		public string name;

		// Token: 0x04000B75 RID: 2933
		public bool fadeOnTransition;

		// Token: 0x04000B76 RID: 2934
		[Tooltip("Objects active on this state")]
		public GameObject[] stateObjects;

		// Token: 0x04000B77 RID: 2935
		[Tooltip("Objects active on states other than this state")]
		public GameObject[] nonStateObjects;

		// Token: 0x04000B78 RID: 2936
		[Tooltip("Objects active on this state and future states\n(This is the FIRST state this object is active)")]
		public GameObject[] firstStateObjects;

		// Token: 0x04000B79 RID: 2937
		[Tooltip("Objects active on this state and previous states\n(This is the LAST state this object is active)")]
		public GameObject[] lastStateObjects;

		// Token: 0x04000B7A RID: 2938
		[Space]
		public UnityEvent onActivate;

		// Token: 0x04000B7B RID: 2939
		public UnityEvent onProgress;

		// Token: 0x04000B7C RID: 2940
		public UnityEvent onDeactivate;
	}

	// Token: 0x020001DD RID: 477
	[Serializable]
	public class StateChangeEvent : UnityEvent<int>
	{
	}
}
