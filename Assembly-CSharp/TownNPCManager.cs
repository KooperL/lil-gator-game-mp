using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TownNPCManager : MonoBehaviour
{
	// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00009A14 File Offset: 0x00007C14
	private bool ShowSequence
	{
		get
		{
			return Game.WorldState != WorldState.Act1;
		}
	}

	// (get) Token: 0x06000A06 RID: 2566 RVA: 0x00009A21 File Offset: 0x00007C21
	public bool isJillQuestFinished
	{
		get
		{
			return this.jillQuest.IsComplete;
		}
	}

	// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00009A2E File Offset: 0x00007C2E
	public bool isMartinQuestFinished
	{
		get
		{
			return this.martinQuest.IsComplete;
		}
	}

	// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00009A3B File Offset: 0x00007C3B
	public bool isAveryQuestFinished
	{
		get
		{
			return this.averyQuest.IsComplete;
		}
	}

	// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00009A48 File Offset: 0x00007C48
	// (set) Token: 0x06000A0A RID: 2570 RVA: 0x00009A55 File Offset: 0x00007C55
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

	// Token: 0x06000A0B RID: 2571 RVA: 0x0003C2B8 File Offset: 0x0003A4B8
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

	// Token: 0x06000A0C RID: 2572 RVA: 0x00009A63 File Offset: 0x00007C63
	private void Start()
	{
		this.UpdateTownActors();
		this.UpdateState();
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x00009A71 File Offset: 0x00007C71
	[ContextMenu("Find Town Actors")]
	public void FindTownActors()
	{
		this.townActors = base.transform.GetComponentsInChildren<DialogueActor>(true);
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x0003C360 File Offset: 0x0003A560
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

	// Token: 0x06000A0F RID: 2575 RVA: 0x0003C3B0 File Offset: 0x0003A5B0
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

	// Token: 0x06000A10 RID: 2576 RVA: 0x00009A85 File Offset: 0x00007C85
	public void RewardNPCs(CharacterProfile[] profiles, int count = -1)
	{
		CoroutineUtil.Start(this.RewardNPCsCoroutine(profiles, -1));
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x00009A95 File Offset: 0x00007C95
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

	// Token: 0x06000A12 RID: 2578 RVA: 0x0003C410 File Offset: 0x0003A610
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

	// Token: 0x06000A13 RID: 2579 RVA: 0x0003C47C File Offset: 0x0003A67C
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

	// Token: 0x06000A14 RID: 2580 RVA: 0x0003C528 File Offset: 0x0003A728
	private void UpdateState()
	{
		TownNPCManager.TownState state = this.State;
		this.tutorialHouse.SetActive(state == TownNPCManager.TownState.BuildingTutorial);
		this.castle.SetActive(state == TownNPCManager.TownState.UpgradingTown);
		this.juiceBar.SetActive(this.averyQuest.IsComplete && state == TownNPCManager.TownState.UpgradingTown);
		this.cathedral.SetActive(this.jillQuest.IsComplete && state == TownNPCManager.TownState.UpgradingTown);
		this.market.SetActive(this.martinQuest.IsComplete && state == TownNPCManager.TownState.UpgradingTown);
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x00009AB2 File Offset: 0x00007CB2
	public void OnAveryComplete()
	{
		if (this.State == TownNPCManager.TownState.UpgradingTown)
		{
			base.StartCoroutine(this.juiceBar.RunUnlockSequence());
			return;
		}
		this.juiceBar.SetActive(false);
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x00009ADC File Offset: 0x00007CDC
	public void OnMartinComplete()
	{
		if (this.State == TownNPCManager.TownState.UpgradingTown)
		{
			base.StartCoroutine(this.market.RunUnlockSequence());
			return;
		}
		this.market.SetActive(false);
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x00009B06 File Offset: 0x00007D06
	public void OnJillComplete()
	{
		if (this.State == TownNPCManager.TownState.UpgradingTown)
		{
			base.StartCoroutine(this.cathedral.RunUnlockSequence());
			return;
		}
		this.cathedral.SetActive(false);
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x00009B30 File Offset: 0x00007D30
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

	// Token: 0x06000A19 RID: 2585 RVA: 0x00009B3F File Offset: 0x00007D3F
	private void CheckBuildings()
	{
		if (this.castle.IsUpgraded && this.cathedral.IsUpgraded && this.juiceBar.IsUpgraded && this.market.IsUpgraded)
		{
			this.ProgressState();
		}
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x00009B7B File Offset: 0x00007D7B
	public void ProgressState()
	{
		this.stateMachine.ProgressState(-1);
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x00009B8A File Offset: 0x00007D8A
	private void OnStateProgressed(int newState)
	{
		if (this.State == TownNPCManager.TownState.UpgradingTown)
		{
			base.StartCoroutine(this.ShowAlreadyUnlockedBuildings());
		}
	}

	public static TownNPCManager t;

	public QuestStates stateMachine;

	public ItemResource populationResource;

	public DialogueActor[] townActors;

	public CharacterProfile[] possibleUnlockedCharacters;

	public CharacterProfile[] allUnlockableCharacters;

	[Header("Buildings")]
	public QuestProfile jillQuest;

	public QuestProfile martinQuest;

	public QuestProfile averyQuest;

	public BuildingUpgradeStation castle;

	public BuildingUpgradeStation cathedral;

	public BuildingUpgradeStation market;

	public BuildingUpgradeStation juiceBar;

	public BuildingUpgradeStation tutorialHouse;

	public enum TownState
	{
		Introduction,
		BuildingTutorial,
		UpgradingTown,
		TownBuilt,
		Postgame
	}
}
