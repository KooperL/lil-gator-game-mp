using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Logic/State Machine")]
public class QuestStates : MonoBehaviour
{
	// (get) Token: 0x06000773 RID: 1907 RVA: 0x00024E8F File Offset: 0x0002308F
	// (set) Token: 0x06000774 RID: 1908 RVA: 0x00024EBB File Offset: 0x000230BB
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

	// Token: 0x06000775 RID: 1909 RVA: 0x00024EE7 File Offset: 0x000230E7
	[ContextMenu("Debug State")]
	public void DebugState()
	{
		Debug.Log(this.StateID);
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x00024EF9 File Offset: 0x000230F9
	private void OnEnable()
	{
		if (this.isShared)
		{
			QuestStates.shared.Add(this);
		}
		this.SetState(this.StateID, true, false);
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x00024F1C File Offset: 0x0002311C
	private void OnDisable()
	{
		if (this.isShared)
		{
			QuestStates.shared.Remove(this);
		}
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x00024F32 File Offset: 0x00023132
	[ContextMenu("Progress State")]
	public void JustProgressState()
	{
		this.ProgressState(-1);
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x00024F3C File Offset: 0x0002313C
	public void ProgressToState(int nextStateID)
	{
		this.ProgressState(nextStateID);
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x00024F48 File Offset: 0x00023148
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

	// Token: 0x0600077B RID: 1915 RVA: 0x00024F9F File Offset: 0x0002319F
	[ContextMenu("Progress To End")]
	public void ProgressToEnd()
	{
		this.ProgressState(this.states.Length - 1);
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x00024FB2 File Offset: 0x000231B2
	private IEnumerator RunFadeTransition(int nextStateID)
	{
		Game.DialogueDepth++;
		yield return Blackout.FadeIn();
		this.SetState(nextStateID, false, false);
		yield return Blackout.FadeOut();
		Game.DialogueDepth--;
		yield break;
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x00024FC8 File Offset: 0x000231C8
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
