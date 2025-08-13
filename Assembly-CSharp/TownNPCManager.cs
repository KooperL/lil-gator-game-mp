using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200020F RID: 527
public class TownNPCManager : MonoBehaviour
{
	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x060009BC RID: 2492 RVA: 0x000096D6 File Offset: 0x000078D6
	private bool ShowSequence
	{
		get
		{
			return Game.WorldState != WorldState.Act1;
		}
	}

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x060009BD RID: 2493 RVA: 0x000096E3 File Offset: 0x000078E3
	public bool isJillQuestFinished
	{
		get
		{
			return this.jillQuest.IsComplete;
		}
	}

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x060009BE RID: 2494 RVA: 0x000096F0 File Offset: 0x000078F0
	public bool isMartinQuestFinished
	{
		get
		{
			return this.martinQuest.IsComplete;
		}
	}

	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x060009BF RID: 2495 RVA: 0x000096FD File Offset: 0x000078FD
	public bool isAveryQuestFinished
	{
		get
		{
			return this.averyQuest.IsComplete;
		}
	}

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0000970A File Offset: 0x0000790A
	// (set) Token: 0x060009C1 RID: 2497 RVA: 0x00009717 File Offset: 0x00007917
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

	// Token: 0x060009C2 RID: 2498 RVA: 0x0003A548 File Offset: 0x00038748
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

	// Token: 0x060009C3 RID: 2499 RVA: 0x00009725 File Offset: 0x00007925
	private void Start()
	{
		this.UpdateTownActors();
		this.UpdateState();
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x00009733 File Offset: 0x00007933
	[ContextMenu("Find Town Actors")]
	public void FindTownActors()
	{
		this.townActors = base.transform.GetComponentsInChildren<DialogueActor>(true);
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x0003A5F0 File Offset: 0x000387F0
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

	// Token: 0x060009C6 RID: 2502 RVA: 0x0003A640 File Offset: 0x00038840
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

	// Token: 0x060009C7 RID: 2503 RVA: 0x00009747 File Offset: 0x00007947
	public void RewardNPCs(CharacterProfile[] profiles, int count = -1)
	{
		CoroutineUtil.Start(this.RewardNPCsCoroutine(profiles, -1));
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x00009757 File Offset: 0x00007957
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

	// Token: 0x060009C9 RID: 2505 RVA: 0x0003A6A0 File Offset: 0x000388A0
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

	// Token: 0x060009CA RID: 2506 RVA: 0x0003A70C File Offset: 0x0003890C
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

	// Token: 0x060009CB RID: 2507 RVA: 0x0003A7B8 File Offset: 0x000389B8
	private void UpdateState()
	{
		TownNPCManager.TownState state = this.State;
		this.tutorialHouse.SetActive(state == TownNPCManager.TownState.BuildingTutorial);
		this.castle.SetActive(state == TownNPCManager.TownState.UpgradingTown);
		this.juiceBar.SetActive(this.averyQuest.IsComplete && state == TownNPCManager.TownState.UpgradingTown);
		this.cathedral.SetActive(this.jillQuest.IsComplete && state == TownNPCManager.TownState.UpgradingTown);
		this.market.SetActive(this.martinQuest.IsComplete && state == TownNPCManager.TownState.UpgradingTown);
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x00009774 File Offset: 0x00007974
	public void OnAveryComplete()
	{
		if (this.State == TownNPCManager.TownState.UpgradingTown)
		{
			base.StartCoroutine(this.juiceBar.RunUnlockSequence());
			return;
		}
		this.juiceBar.SetActive(false);
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x0000979E File Offset: 0x0000799E
	public void OnMartinComplete()
	{
		if (this.State == TownNPCManager.TownState.UpgradingTown)
		{
			base.StartCoroutine(this.market.RunUnlockSequence());
			return;
		}
		this.market.SetActive(false);
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x000097C8 File Offset: 0x000079C8
	public void OnJillComplete()
	{
		if (this.State == TownNPCManager.TownState.UpgradingTown)
		{
			base.StartCoroutine(this.cathedral.RunUnlockSequence());
			return;
		}
		this.cathedral.SetActive(false);
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x000097F2 File Offset: 0x000079F2
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

	// Token: 0x060009D0 RID: 2512 RVA: 0x00009801 File Offset: 0x00007A01
	private void CheckBuildings()
	{
		if (this.castle.IsUpgraded && this.cathedral.IsUpgraded && this.juiceBar.IsUpgraded && this.market.IsUpgraded)
		{
			this.ProgressState();
		}
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x0000983D File Offset: 0x00007A3D
	public void ProgressState()
	{
		this.stateMachine.ProgressState(-1);
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x0000984C File Offset: 0x00007A4C
	private void OnStateProgressed(int newState)
	{
		if (this.State == TownNPCManager.TownState.UpgradingTown)
		{
			base.StartCoroutine(this.ShowAlreadyUnlockedBuildings());
		}
	}

	// Token: 0x04000C60 RID: 3168
	public static TownNPCManager t;

	// Token: 0x04000C61 RID: 3169
	public QuestStates stateMachine;

	// Token: 0x04000C62 RID: 3170
	public ItemResource populationResource;

	// Token: 0x04000C63 RID: 3171
	public DialogueActor[] townActors;

	// Token: 0x04000C64 RID: 3172
	public CharacterProfile[] possibleUnlockedCharacters;

	// Token: 0x04000C65 RID: 3173
	public CharacterProfile[] allUnlockableCharacters;

	// Token: 0x04000C66 RID: 3174
	[Header("Buildings")]
	public QuestProfile jillQuest;

	// Token: 0x04000C67 RID: 3175
	public QuestProfile martinQuest;

	// Token: 0x04000C68 RID: 3176
	public QuestProfile averyQuest;

	// Token: 0x04000C69 RID: 3177
	public BuildingUpgradeStation castle;

	// Token: 0x04000C6A RID: 3178
	public BuildingUpgradeStation cathedral;

	// Token: 0x04000C6B RID: 3179
	public BuildingUpgradeStation market;

	// Token: 0x04000C6C RID: 3180
	public BuildingUpgradeStation juiceBar;

	// Token: 0x04000C6D RID: 3181
	public BuildingUpgradeStation tutorialHouse;

	// Token: 0x02000210 RID: 528
	public enum TownState
	{
		// Token: 0x04000C6F RID: 3183
		Introduction,
		// Token: 0x04000C70 RID: 3184
		BuildingTutorial,
		// Token: 0x04000C71 RID: 3185
		UpgradingTown,
		// Token: 0x04000C72 RID: 3186
		TownBuilt,
		// Token: 0x04000C73 RID: 3187
		Postgame
	}
}
