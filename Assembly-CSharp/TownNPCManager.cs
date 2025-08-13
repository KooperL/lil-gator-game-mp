using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000196 RID: 406
public class TownNPCManager : MonoBehaviour
{
	// Token: 0x17000074 RID: 116
	// (get) Token: 0x06000845 RID: 2117 RVA: 0x00027701 File Offset: 0x00025901
	private bool ShowSequence
	{
		get
		{
			return Game.WorldState != WorldState.Act1;
		}
	}

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x06000846 RID: 2118 RVA: 0x0002770E File Offset: 0x0002590E
	public bool isJillQuestFinished
	{
		get
		{
			return this.jillQuest.IsComplete;
		}
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x06000847 RID: 2119 RVA: 0x0002771B File Offset: 0x0002591B
	public bool isMartinQuestFinished
	{
		get
		{
			return this.martinQuest.IsComplete;
		}
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x06000848 RID: 2120 RVA: 0x00027728 File Offset: 0x00025928
	public bool isAveryQuestFinished
	{
		get
		{
			return this.averyQuest.IsComplete;
		}
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x06000849 RID: 2121 RVA: 0x00027735 File Offset: 0x00025935
	// (set) Token: 0x0600084A RID: 2122 RVA: 0x00027742 File Offset: 0x00025942
	public TownNPCManager.TownState State
	{
		get
		{
			return (TownNPCManager.TownState)this.stateMachine.StateID;
		}
		private set
		{
			this.stateMachine.StateID = (int)value;
		}
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x00027750 File Offset: 0x00025950
	private void Awake()
	{
		TownNPCManager.t = this;
		this.FindTownActors();
		this.castle.onUpgraded.AddListener(new UnityAction(this.CheckBuildings));
		this.cathedral.onUpgraded.AddListener(new UnityAction(this.CheckBuildings));
		this.market.onUpgraded.AddListener(new UnityAction(this.CheckBuildings));
		this.juiceBar.onUpgraded.AddListener(new UnityAction(this.CheckBuildings));
		this.stateMachine.onStateChange.AddListener(new UnityAction<int>(this.OnStateProgressed));
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x000277F5 File Offset: 0x000259F5
	private void Start()
	{
		this.UpdateTownActors();
		this.UpdateState();
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x00027803 File Offset: 0x00025A03
	[ContextMenu("Find Town Actors")]
	public void FindTownActors()
	{
		this.townActors = base.transform.GetComponentsInChildren<DialogueActor>(true);
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x00027818 File Offset: 0x00025A18
	public void UnlockAllNPCs()
	{
		foreach (DialogueActor dialogueActor in this.townActors)
		{
			if (dialogueActor != null && dialogueActor.profile != null)
			{
				dialogueActor.profile.IsUnlocked = true;
			}
		}
		this.UpdateTownActors();
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x00027868 File Offset: 0x00025A68
	public void RewardNPCsSilently(CharacterProfile[] profiles)
	{
		foreach (CharacterProfile characterProfile in profiles)
		{
			if (!characterProfile.IsUnlocked)
			{
				characterProfile.IsUnlocked = true;
			}
			if (SpeedrunData.ShouldTrack && !SpeedrunData.unlockedFriends.Contains(characterProfile.id))
			{
				SpeedrunData.unlockedFriends.Add(characterProfile.id);
			}
		}
		this.UpdateTownActors();
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x000278C7 File Offset: 0x00025AC7
	public void RewardNPCs(CharacterProfile[] profiles, int count = -1)
	{
		CoroutineUtil.Start(this.RewardNPCsCoroutine(profiles, -1));
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x000278D7 File Offset: 0x00025AD7
	private IEnumerator RewardNPCsCoroutine(CharacterProfile[] profiles, int count = -1)
	{
		yield return null;
		if (Game.State != GameState.Play)
		{
			int gameStateCounter = 0;
			while (Game.State != GameState.Play && gameStateCounter < 10)
			{
				yield return null;
				if (Game.State == GameState.Play)
				{
					int i = gameStateCounter;
					gameStateCounter = i + 1;
				}
				else
				{
					gameStateCounter = 0;
				}
			}
		}
		int num = 0;
		if (count == -1)
		{
			foreach (CharacterProfile characterProfile in profiles)
			{
				if (!characterProfile.IsUnlocked)
				{
					characterProfile.IsUnlocked = true;
					num++;
				}
			}
		}
		else
		{
			num = count;
		}
		if (SpeedrunData.ShouldTrack)
		{
			foreach (CharacterProfile characterProfile2 in profiles)
			{
				if (!SpeedrunData.unlockedFriends.Contains(characterProfile2.id))
				{
					SpeedrunData.unlockedFriends.Add(characterProfile2.id);
				}
			}
		}
		if (this.ShowSequence)
		{
			this.populationResource.Amount += num;
			UIMenus.characterNotification.Load(profiles);
		}
		this.UpdateTownActors();
		yield break;
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x000278F4 File Offset: 0x00025AF4
	public void RewardAlreadyUnlockedNPCs()
	{
		List<CharacterProfile> list = new List<CharacterProfile>();
		foreach (CharacterProfile characterProfile in this.possibleUnlockedCharacters)
		{
			if (characterProfile.IsUnlocked)
			{
				list.Add(characterProfile);
			}
		}
		if (list.Count > 0)
		{
			this.populationResource.Amount += list.Count;
			UIMenus.characterNotification.Load(list.ToArray());
		}
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x00027960 File Offset: 0x00025B60
	private void UpdateTownActors()
	{
		foreach (DialogueActor dialogueActor in this.townActors)
		{
			if (dialogueActor.profile != null && !dialogueActor.ignoreUnlock)
			{
				dialogueActor.gameObject.SetActive(dialogueActor.profile.IsUnlocked);
			}
		}
		int num = 0;
		CharacterProfile[] array2 = this.allUnlockableCharacters;
		for (int i = 0; i < array2.Length; i++)
		{
			if (array2[i].IsUnlocked)
			{
				num++;
			}
		}
		if (Game.WorldState == WorldState.Act1)
		{
			GameData.g.Write("TotalPopulation", 0);
		}
		else
		{
			GameData.g.Write("TotalPopulation", num);
		}
		this.UpdateState();
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x00027A0C File Offset: 0x00025C0C
	private void UpdateState()
	{
		TownNPCManager.TownState state = this.State;
		this.tutorialHouse.SetActive(state == TownNPCManager.TownState.BuildingTutorial);
		this.castle.SetActive(state == TownNPCManager.TownState.UpgradingTown);
		this.juiceBar.SetActive(this.averyQuest.IsComplete && state == TownNPCManager.TownState.UpgradingTown);
		this.cathedral.SetActive(this.jillQuest.IsComplete && state == TownNPCManager.TownState.UpgradingTown);
		this.market.SetActive(this.martinQuest.IsComplete && state == TownNPCManager.TownState.UpgradingTown);
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x00027A9B File Offset: 0x00025C9B
	public void OnAveryComplete()
	{
		if (this.State == TownNPCManager.TownState.UpgradingTown)
		{
			base.StartCoroutine(this.juiceBar.RunUnlockSequence());
			return;
		}
		this.juiceBar.SetActive(false);
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x00027AC5 File Offset: 0x00025CC5
	public void OnMartinComplete()
	{
		if (this.State == TownNPCManager.TownState.UpgradingTown)
		{
			base.StartCoroutine(this.market.RunUnlockSequence());
			return;
		}
		this.market.SetActive(false);
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x00027AEF File Offset: 0x00025CEF
	public void OnJillComplete()
	{
		if (this.State == TownNPCManager.TownState.UpgradingTown)
		{
			base.StartCoroutine(this.cathedral.RunUnlockSequence());
			return;
		}
		this.cathedral.SetActive(false);
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x00027B19 File Offset: 0x00025D19
	private IEnumerator ShowAlreadyUnlockedBuildings()
	{
		yield return base.StartCoroutine(this.castle.RunUnlockSequence());
		if (this.jillQuest.IsComplete)
		{
			yield return base.StartCoroutine(this.cathedral.RunUnlockSequence());
		}
		if (this.averyQuest.IsComplete)
		{
			yield return base.StartCoroutine(this.juiceBar.RunUnlockSequence());
		}
		if (this.martinQuest.IsComplete)
		{
			yield return base.StartCoroutine(this.market.RunUnlockSequence());
		}
		yield break;
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x00027B28 File Offset: 0x00025D28
	private void CheckBuildings()
	{
		if (this.castle.IsUpgraded && this.cathedral.IsUpgraded && this.juiceBar.IsUpgraded && this.market.IsUpgraded)
		{
			this.ProgressState();
		}
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x00027B64 File Offset: 0x00025D64
	public void ProgressState()
	{
		this.stateMachine.ProgressState(-1);
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x00027B73 File Offset: 0x00025D73
	private void OnStateProgressed(int newState)
	{
		if (this.State == TownNPCManager.TownState.UpgradingTown)
		{
			base.StartCoroutine(this.ShowAlreadyUnlockedBuildings());
		}
	}

	// Token: 0x04000A6A RID: 2666
	public static TownNPCManager t;

	// Token: 0x04000A6B RID: 2667
	public QuestStates stateMachine;

	// Token: 0x04000A6C RID: 2668
	public ItemResource populationResource;

	// Token: 0x04000A6D RID: 2669
	public DialogueActor[] townActors;

	// Token: 0x04000A6E RID: 2670
	public CharacterProfile[] possibleUnlockedCharacters;

	// Token: 0x04000A6F RID: 2671
	public CharacterProfile[] allUnlockableCharacters;

	// Token: 0x04000A70 RID: 2672
	[Header("Buildings")]
	public QuestProfile jillQuest;

	// Token: 0x04000A71 RID: 2673
	public QuestProfile martinQuest;

	// Token: 0x04000A72 RID: 2674
	public QuestProfile averyQuest;

	// Token: 0x04000A73 RID: 2675
	public BuildingUpgradeStation castle;

	// Token: 0x04000A74 RID: 2676
	public BuildingUpgradeStation cathedral;

	// Token: 0x04000A75 RID: 2677
	public BuildingUpgradeStation market;

	// Token: 0x04000A76 RID: 2678
	public BuildingUpgradeStation juiceBar;

	// Token: 0x04000A77 RID: 2679
	public BuildingUpgradeStation tutorialHouse;

	// Token: 0x020003D3 RID: 979
	public enum TownState
	{
		// Token: 0x04001C1D RID: 7197
		Introduction,
		// Token: 0x04001C1E RID: 7198
		BuildingTutorial,
		// Token: 0x04001C1F RID: 7199
		UpgradingTown,
		// Token: 0x04001C20 RID: 7200
		TownBuilt,
		// Token: 0x04001C21 RID: 7201
		Postgame
	}
}
