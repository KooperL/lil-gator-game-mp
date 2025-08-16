using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TownNPCManager : MonoBehaviour
{
	// (get) Token: 0x06000A04 RID: 2564 RVA: 0x000099F5 File Offset: 0x00007BF5
	private bool ShowSequence
	{
		get
		{
			return Game.WorldState != WorldState.Act1;
		}
	}

	// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00009A02 File Offset: 0x00007C02
	public bool isJillQuestFinished
	{
		get
		{
			return this.jillQuest.IsComplete;
		}
	}

	// (get) Token: 0x06000A06 RID: 2566 RVA: 0x00009A0F File Offset: 0x00007C0F
	public bool isMartinQuestFinished
	{
		get
		{
			return this.martinQuest.IsComplete;
		}
	}

	// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00009A1C File Offset: 0x00007C1C
	public bool isAveryQuestFinished
	{
		get
		{
			return this.averyQuest.IsComplete;
		}
	}

	// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00009A29 File Offset: 0x00007C29
	// (set) Token: 0x06000A09 RID: 2569 RVA: 0x00009A36 File Offset: 0x00007C36
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

	// Token: 0x06000A0A RID: 2570 RVA: 0x0003BE5C File Offset: 0x0003A05C
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

	// Token: 0x06000A0B RID: 2571 RVA: 0x00009A44 File Offset: 0x00007C44
	private void Start()
	{
		this.UpdateTownActors();
		this.UpdateState();
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x00009A52 File Offset: 0x00007C52
	[ContextMenu("Find Town Actors")]
	public void FindTownActors()
	{
		this.townActors = base.transform.GetComponentsInChildren<DialogueActor>(true);
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x0003BF04 File Offset: 0x0003A104
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

	// Token: 0x06000A0E RID: 2574 RVA: 0x0003BF54 File Offset: 0x0003A154
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

	// Token: 0x06000A0F RID: 2575 RVA: 0x00009A66 File Offset: 0x00007C66
	public void RewardNPCs(CharacterProfile[] profiles, int count = -1)
	{
		CoroutineUtil.Start(this.RewardNPCsCoroutine(profiles, -1));
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x00009A76 File Offset: 0x00007C76
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

	// Token: 0x06000A11 RID: 2577 RVA: 0x0003BFB4 File Offset: 0x0003A1B4
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

	// Token: 0x06000A12 RID: 2578 RVA: 0x0003C020 File Offset: 0x0003A220
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

	// Token: 0x06000A13 RID: 2579 RVA: 0x0003C0CC File Offset: 0x0003A2CC
	private void UpdateState()
	{
		TownNPCManager.TownState state = this.State;
		this.tutorialHouse.SetActive(state == TownNPCManager.TownState.BuildingTutorial);
		this.castle.SetActive(state == TownNPCManager.TownState.UpgradingTown);
		this.juiceBar.SetActive(this.averyQuest.IsComplete && state == TownNPCManager.TownState.UpgradingTown);
		this.cathedral.SetActive(this.jillQuest.IsComplete && state == TownNPCManager.TownState.UpgradingTown);
		this.market.SetActive(this.martinQuest.IsComplete && state == TownNPCManager.TownState.UpgradingTown);
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x00009A93 File Offset: 0x00007C93
	public void OnAveryComplete()
	{
		if (this.State == TownNPCManager.TownState.UpgradingTown)
		{
			base.StartCoroutine(this.juiceBar.RunUnlockSequence());
			return;
		}
		this.juiceBar.SetActive(false);
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x00009ABD File Offset: 0x00007CBD
	public void OnMartinComplete()
	{
		if (this.State == TownNPCManager.TownState.UpgradingTown)
		{
			base.StartCoroutine(this.market.RunUnlockSequence());
			return;
		}
		this.market.SetActive(false);
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x00009AE7 File Offset: 0x00007CE7
	public void OnJillComplete()
	{
		if (this.State == TownNPCManager.TownState.UpgradingTown)
		{
			base.StartCoroutine(this.cathedral.RunUnlockSequence());
			return;
		}
		this.cathedral.SetActive(false);
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x00009B11 File Offset: 0x00007D11
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

	// Token: 0x06000A18 RID: 2584 RVA: 0x00009B20 File Offset: 0x00007D20
	private void CheckBuildings()
	{
		if (this.castle.IsUpgraded && this.cathedral.IsUpgraded && this.juiceBar.IsUpgraded && this.market.IsUpgraded)
		{
			this.ProgressState();
		}
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x00009B5C File Offset: 0x00007D5C
	public void ProgressState()
	{
		this.stateMachine.ProgressState(-1);
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x00009B6B File Offset: 0x00007D6B
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
