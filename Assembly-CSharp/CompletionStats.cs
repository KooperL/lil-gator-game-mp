using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CompletionStats : MonoBehaviour
{
	// (get) Token: 0x060002AE RID: 686 RVA: 0x00004351 File Offset: 0x00002551
	public static CompletionStats c
	{
		get
		{
			if (CompletionStats.instance == null)
			{
				CompletionStats.instance = global::UnityEngine.Object.FindObjectOfType<CompletionStats>(true);
			}
			return CompletionStats.instance;
		}
	}

	// (get) Token: 0x060002AF RID: 687 RVA: 0x00004370 File Offset: 0x00002570
	// (set) Token: 0x060002B0 RID: 688 RVA: 0x00004382 File Offset: 0x00002582
	public static int PercentObjects
	{
		get
		{
			return GameData.g.ReadInt("Completion_Objects", 0);
		}
		set
		{
			GameData.g.Write("Completion_Objects", value);
		}
	}

	// (get) Token: 0x060002B1 RID: 689 RVA: 0x00004394 File Offset: 0x00002594
	// (set) Token: 0x060002B2 RID: 690 RVA: 0x000043A6 File Offset: 0x000025A6
	public static int PercentCharacters
	{
		get
		{
			return GameData.g.ReadInt("Completion_Characters", 0);
		}
		set
		{
			GameData.g.Write("Completion_Characters", value);
		}
	}

	// (get) Token: 0x060002B3 RID: 691 RVA: 0x000043B8 File Offset: 0x000025B8
	// (set) Token: 0x060002B4 RID: 692 RVA: 0x000043CA File Offset: 0x000025CA
	public static int PercentItems
	{
		get
		{
			return GameData.g.ReadInt("Completion_Items", 0);
		}
		set
		{
			GameData.g.Write("Completion_Items", value);
		}
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x000043DC File Offset: 0x000025DC
	private void Awake()
	{
		this.townStates.onStateChange.AddListener(new UnityAction<int>(this.OnStateChange));
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x000043FA File Offset: 0x000025FA
	private void OnStateChange(int newStateIndex)
	{
		if (newStateIndex == 4)
		{
			this.CheckForFullCompletion();
		}
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x00020FAC File Offset: 0x0001F1AC
	public void OnEnable()
	{
		CompletionStats.instance = this;
		PersistentObject[] array = this.completionObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].onSaveTrue.AddListener(new UnityAction(this.UpdateObjects));
		}
		CharacterProfile[] array2 = this.completionCharacters;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].OnChange += this.UpdateCharacters;
		}
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x00004406 File Offset: 0x00002606
	[ContextMenu("Update all")]
	private void Start()
	{
		this.UpdateObjects();
		this.UpdateCharacters(null, true);
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x00021018 File Offset: 0x0001F218
	private void OnDisable()
	{
		if (CompletionStats.instance == this)
		{
			CompletionStats.instance = null;
		}
		foreach (PersistentObject persistentObject in this.completionObjects)
		{
			if (persistentObject != null)
			{
				persistentObject.onSaveTrue.RemoveListener(new UnityAction(this.UpdateObjects));
			}
		}
		foreach (CharacterProfile characterProfile in this.completionCharacters)
		{
			if (characterProfile != null)
			{
				characterProfile.OnChange -= this.UpdateCharacters;
			}
		}
	}

	// Token: 0x060002BA RID: 698 RVA: 0x000210A8 File Offset: 0x0001F2A8
	private void UpdateObjects()
	{
		if (CompletionStats.PercentObjects == 100)
		{
			this.objectsAchievement.UnlockAchievement();
			return;
		}
		int num = 0;
		PersistentObject[] array = this.completionObjects;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].PersistentState)
			{
				num++;
			}
		}
		int num2 = Mathf.RoundToInt(100f * (float)num / (float)this.completionObjects.Length);
		if (num2 == 100 && num != this.completionObjects.Length)
		{
			num2 = 99;
		}
		this.completeObjectsDialogue.SetActive(num2 == 100);
		CompletionStats.PercentObjects = num2;
		if (num2 == 100)
		{
			this.objectsAchievement.UnlockAchievement();
		}
	}

	// Token: 0x060002BB RID: 699 RVA: 0x00021140 File Offset: 0x0001F340
	private void UpdateCharacters(object sender, bool wasUnlocked)
	{
		if (!wasUnlocked)
		{
			return;
		}
		if (CompletionStats.PercentCharacters == 100)
		{
			this.friendsAchievement.UnlockAchievement();
			return;
		}
		int num = 0;
		CharacterProfile[] array = this.completionCharacters;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].IsUnlocked)
			{
				num++;
			}
		}
		int num2 = Mathf.RoundToInt(100f * (float)num / (float)this.completionCharacters.Length);
		if (num2 == 100 && num != this.completionCharacters.Length)
		{
			num2 = 99;
		}
		this.completeCharactersDialogue.SetActive(num2 == 100);
		CompletionStats.PercentCharacters = num2;
		if (num2 == 100)
		{
			this.friendsAchievement.UnlockAchievement();
		}
	}

	// Token: 0x060002BC RID: 700 RVA: 0x000211DC File Offset: 0x0001F3DC
	public void UpdateItems()
	{
		if (CompletionStats.PercentItems == 100)
		{
			return;
		}
		int num = 0;
		ItemObject[] array = this.completionItems;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].IsUnlocked)
			{
				num++;
			}
		}
		int num2 = Mathf.RoundToInt(100f * (float)num / (float)this.completionItems.Length);
		if (num2 == 100 && num != this.completionItems.Length)
		{
			num2 = 99;
		}
		CompletionStats.PercentItems = num2;
	}

	// Token: 0x060002BD RID: 701 RVA: 0x00004416 File Offset: 0x00002616
	public void CheckForFullCompletion()
	{
		if (CompletionStats.PercentObjects == 100 && CompletionStats.PercentCharacters == 100 && this.townStates.StateID == 4)
		{
			this.townStates.JustProgressState();
		}
	}

	// Token: 0x060002BE RID: 702 RVA: 0x0002124C File Offset: 0x0001F44C
	[ContextMenu("Count")]
	public void CountPersistentObjects()
	{
		int num = 0;
		int num2 = 0;
		foreach (PersistentObject persistentObject in this.completionObjects)
		{
			if (persistentObject is BreakableObject)
			{
				num2++;
			}
			if (persistentObject is TimedChallenge)
			{
				num++;
			}
		}
		Debug.Log("Timed Challenges: " + num.ToString() + "  Breakables: " + num2.ToString());
	}

	// Token: 0x060002BF RID: 703 RVA: 0x000212B0 File Offset: 0x0001F4B0
	[ContextMenu("Find Non Quest Objects")]
	public void FindNonQuestObjects()
	{
		List<PersistentObject> list = new List<PersistentObject>();
		foreach (PersistentObject persistentObject in global::UnityEngine.Object.FindObjectsOfType<PersistentObject>(false))
		{
			if (persistentObject.isPersistent && !(persistentObject.GetComponentInParent<QuestStates>() != null) && !(persistentObject.GetComponentInParent<FetchQuest>() != null) && !(persistentObject.GetComponentInParent<DestroyQuest>() != null) && !(persistentObject.GetComponentInParent<MultiDestroyQuest>() != null) && !(persistentObject.GetComponentInParent<QuestReward>() != null) && !(persistentObject.GetComponentInParent<QuestRelated>() != null) && (!(persistentObject.GetComponentInParent<TimedChallenge>() != null) || !(persistentObject.GetComponent<TimedChallenge>() == null)) && (!(persistentObject.transform.parent != null) || !(persistentObject.transform.parent.parent != null) || !(persistentObject.transform.parent.parent.gameObject.name == "Flashback Region")) && !(persistentObject.gameObject.name == "Retainer Pickup"))
			{
				list.Add(persistentObject);
			}
		}
		this.completionObjects = list.ToArray();
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x000213EC File Offset: 0x0001F5EC
	[ContextMenu("Find Completion Actors")]
	public void FindCompletionActors()
	{
		List<DialogueActor> list = new List<DialogueActor>();
		foreach (DialogueActor dialogueActor in global::UnityEngine.Object.FindObjectsOfType<DialogueActor>(false))
		{
			if (!list.Contains(dialogueActor) && !dialogueActor.isPlayer && !dialogueActor.ignoreUnlock && dialogueActor.showNpcMarker && !(dialogueActor.profile == null) && !(dialogueActor.profile.id == "") && !dialogueActor.profile.startsUnlocked)
			{
				Transform parent = dialogueActor.transform.parent;
				if ((!(dialogueActor.transform.parent != null) || !(dialogueActor.transform.parent.parent != null) || !(dialogueActor.transform.parent.parent == base.transform.parent)) && (!(dialogueActor.transform.parent != null) || !(dialogueActor.transform.parent.parent != null) || !(dialogueActor.transform.parent.parent.parent != null) || !(dialogueActor.transform.parent.parent.parent == base.transform.parent)) && (!(dialogueActor.transform.parent != null) || !(dialogueActor.transform.parent.gameObject.name == "Other Characters")))
				{
					list.Add(dialogueActor);
				}
			}
		}
		this.completionActors = list.ToArray();
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x00021598 File Offset: 0x0001F798
	[ContextMenu("Get Profiles From Actors")]
	public void GetProfilesFromActors()
	{
		List<CharacterProfile> list = new List<CharacterProfile>();
		foreach (DialogueActor dialogueActor in this.completionActors)
		{
			if (!list.Contains(dialogueActor.profile))
			{
				list.Add(dialogueActor.profile);
			}
		}
		this.completionCharacters = list.ToArray();
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x000215EC File Offset: 0x0001F7EC
	[ContextMenu("Break All Breakables")]
	public void BreakAllBreakables()
	{
		foreach (PersistentObject persistentObject in this.completionObjects)
		{
			if (persistentObject is BreakableObject)
			{
				(persistentObject as BreakableObject).Break(false, Vector3.zero, true);
			}
		}
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x0002162C File Offset: 0x0001F82C
	[ContextMenu("Break Most Breakables")]
	public void BreakMostBreakables()
	{
		base.gameObject.SetActive(true);
		int num = 0;
		foreach (PersistentObject persistentObject in this.completionObjects)
		{
			if (!persistentObject.PersistentState)
			{
				if (persistentObject is BreakableObject)
				{
					if (num < 1)
					{
						num++;
					}
					else
					{
						(persistentObject as BreakableObject).Break(false, Vector3.zero, true);
					}
				}
				else
				{
					persistentObject.SaveTrue();
				}
			}
		}
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x00021694 File Offset: 0x0001F894
	[ContextMenu("Break ALL Breakables")]
	public void BreakALLBreakables()
	{
		BreakableObject[] array = global::UnityEngine.Object.FindObjectsOfType<BreakableObject>(false);
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Break(false, Vector3.zero, true);
		}
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x000216C8 File Offset: 0x0001F8C8
	[ContextMenu("Collect All Rewards")]
	public void CollectAllRewards()
	{
		QuestReward[] array = global::UnityEngine.Object.FindObjectsOfType<QuestReward>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].GiveReward();
		}
	}

	private static CompletionStats instance;

	public PersistentObject[] completionObjects;

	public CharacterProfile[] completionCharacters;

	public DialogueActor[] completionActors;

	public ItemObject[] completionItems;

	private const string percentObjectKey = "Completion_Objects";

	private const string percentCharactersKey = "Completion_Characters";

	private const string percentItemsKey = "Completion_Items";

	public GameObject completeObjectsDialogue;

	public GameObject completeCharactersDialogue;

	public QuestStates townStates;

	public Achievement objectsAchievement;

	public Achievement friendsAchievement;
}
