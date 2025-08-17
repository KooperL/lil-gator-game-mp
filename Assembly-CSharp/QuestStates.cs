using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Logic/State Machine")]
public class QuestStates : MonoBehaviour
{
	// (get) Token: 0x0600090B RID: 2315 RVA: 0x00008C25 File Offset: 0x00006E25
	// (set) Token: 0x0600090C RID: 2316 RVA: 0x00008C51 File Offset: 0x00006E51
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

	// Token: 0x0600090D RID: 2317 RVA: 0x00008C7D File Offset: 0x00006E7D
	[ContextMenu("Debug State")]
	public void DebugState()
	{
		Debug.Log(this.StateID);
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x00008C8F File Offset: 0x00006E8F
	private void OnEnable()
	{
		if (this.isShared)
		{
			QuestStates.shared.Add(this);
		}
		this.SetState(this.StateID, true, false);
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x00008CB2 File Offset: 0x00006EB2
	private void OnDisable()
	{
		if (this.isShared)
		{
			QuestStates.shared.Remove(this);
		}
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x00008CC8 File Offset: 0x00006EC8
	[ContextMenu("Progress State")]
	public void JustProgressState()
	{
		this.ProgressState(-1);
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x00008CD2 File Offset: 0x00006ED2
	public void ProgressToState(int nextStateID)
	{
		this.ProgressState(nextStateID);
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x00039D28 File Offset: 0x00037F28
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

	// Token: 0x06000913 RID: 2323 RVA: 0x00008CDC File Offset: 0x00006EDC
	[ContextMenu("Progress To End")]
	public void ProgressToEnd()
	{
		this.ProgressState(this.states.Length - 1);
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x00008CEF File Offset: 0x00006EEF
	private IEnumerator RunFadeTransition(int nextStateID)
	{
		Game.DialogueDepth++;
		yield return Blackout.FadeIn();
		this.SetState(nextStateID, false, false);
		yield return Blackout.FadeOut();
		Game.DialogueDepth--;
		yield break;
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x00039D80 File Offset: 0x00037F80
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

	private static List<QuestStates> shared = new List<QuestStates>();

	public QuestStates.QuestState[] states;

	[HideInInspector]
	public QuestStates.StateChangeEvent onStateChange = new QuestStates.StateChangeEvent();

	public string id;

	private int localState;

	public bool giveRewardsOnLastState;

	public bool isShared;

	[Serializable]
	public struct QuestState
	{
		public string name;

		public bool fadeOnTransition;

		[Tooltip("Objects active on this state")]
		public GameObject[] stateObjects;

		[Tooltip("Objects active on states other than this state")]
		public GameObject[] nonStateObjects;

		[Tooltip("Objects active on this state and future states\n(This is the FIRST state this object is active)")]
		public GameObject[] firstStateObjects;

		[Tooltip("Objects active on this state and previous states\n(This is the LAST state this object is active)")]
		public GameObject[] lastStateObjects;

		[Space]
		public UnityEvent onActivate;

		public UnityEvent onProgress;

		public UnityEvent onDeactivate;
	}

	[Serializable]
	public class StateChangeEvent : UnityEvent<int>
	{
	}
}
