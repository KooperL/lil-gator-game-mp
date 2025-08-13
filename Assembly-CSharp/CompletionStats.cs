using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000B4 RID: 180
public class CompletionStats : MonoBehaviour
{
	// Token: 0x1700002F RID: 47
	// (get) Token: 0x060002A1 RID: 673 RVA: 0x00004265 File Offset: 0x00002465
	public static CompletionStats c
	{
		get
		{
			if (CompletionStats.instance == null)
			{
				CompletionStats.instance = Object.FindObjectOfType<CompletionStats>(true);
			}
			return CompletionStats.instance;
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x060002A2 RID: 674 RVA: 0x00004284 File Offset: 0x00002484
	// (set) Token: 0x060002A3 RID: 675 RVA: 0x00004296 File Offset: 0x00002496
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

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x060002A4 RID: 676 RVA: 0x000042A8 File Offset: 0x000024A8
	// (set) Token: 0x060002A5 RID: 677 RVA: 0x000042BA File Offset: 0x000024BA
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

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060002A6 RID: 678 RVA: 0x000042CC File Offset: 0x000024CC
	// (set) Token: 0x060002A7 RID: 679 RVA: 0x000042DE File Offset: 0x000024DE
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

	// Token: 0x060002A8 RID: 680 RVA: 0x000042F0 File Offset: 0x000024F0
	private void Awake()
	{
		this.townStates.onStateChange.AddListener(new UnityAction<int>(this.OnStateChange));
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0000430E File Offset: 0x0000250E
	private void OnStateChange(int newStateIndex)
	{
		if (newStateIndex == 4)
		{
			this.CheckForFullCompletion();
		}
	}

	// Token: 0x060002AA RID: 682 RVA: 0x00020578 File Offset: 0x0001E778
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

	// Token: 0x060002AB RID: 683 RVA: 0x0000431A File Offset: 0x0000251A
	[ContextMenu("Update all")]
	private void Start()
	{
		this.UpdateObjects();
		this.UpdateCharacters(null, true);
	}

	// Token: 0x060002AC RID: 684 RVA: 0x000205E4 File Offset: 0x0001E7E4
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

	// Token: 0x060002AD RID: 685 RVA: 0x00020674 File Offset: 0x0001E874
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

	// Token: 0x060002AE RID: 686 RVA: 0x0002070C File Offset: 0x0001E90C
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

	// Token: 0x060002AF RID: 687 RVA: 0x000207A8 File Offset: 0x0001E9A8
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

	// Token: 0x060002B0 RID: 688 RVA: 0x0000432A File Offset: 0x0000252A
	public void CheckForFullCompletion()
	{
		if (CompletionStats.PercentObjects == 100 && CompletionStats.PercentCharacters == 100 && this.townStates.StateID == 4)
		{
			this.townStates.JustProgressState();
		}
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x00020818 File Offset: 0x0001EA18
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

	// Token: 0x060002B2 RID: 690 RVA: 0x0002087C File Offset: 0x0001EA7C
	[ContextMenu("Find Non Quest Objects")]
	public void FindNonQuestObjects()
	{
		List<PersistentObject> list = new List<PersistentObject>();
		foreach (PersistentObject persistentObject in Object.FindObjectsOfType<PersistentObject>(false))
		{
			if (persistentObject.isPersistent && !(persistentObject.GetComponentInParent<QuestStates>() != null) && !(persistentObject.GetComponentInParent<FetchQuest>() != null) && !(persistentObject.GetComponentInParent<DestroyQuest>() != null) && !(persistentObject.GetComponentInParent<MultiDestroyQuest>() != null) && !(persistentObject.GetComponentInParent<QuestReward>() != null) && !(persistentObject.GetComponentInParent<QuestRelated>() != null) && (!(persistentObject.GetComponentInParent<TimedChallenge>() != null) || !(persistentObject.GetComponent<TimedChallenge>() == null)) && (!(persistentObject.transform.parent != null) || !(persistentObject.transform.parent.parent != null) || !(persistentObject.transform.parent.parent.gameObject.name == "Flashback Region")) && !(persistentObject.gameObject.name == "Retainer Pickup"))
			{
				list.Add(persistentObject);
			}
		}
		this.completionObjects = list.ToArray();
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x000209B8 File Offset: 0x0001EBB8
	[ContextMenu("Find Completion Actors")]
	public void FindCompletionActors()
	{
		List<DialogueActor> list = new List<DialogueActor>();
		foreach (DialogueActor dialogueActor in Object.FindObjectsOfType<DialogueActor>(false))
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

	// Token: 0x060002B4 RID: 692 RVA: 0x00020B64 File Offset: 0x0001ED64
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

	// Token: 0x060002B5 RID: 693 RVA: 0x00020BB8 File Offset: 0x0001EDB8
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

	// Token: 0x060002B6 RID: 694 RVA: 0x00020BF8 File Offset: 0x0001EDF8
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

	// Token: 0x060002B7 RID: 695 RVA: 0x00020C60 File Offset: 0x0001EE60
	[ContextMenu("Break ALL Breakables")]
	public void BreakALLBreakables()
	{
		BreakableObject[] array = Object.FindObjectsOfType<BreakableObject>(false);
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Break(false, Vector3.zero, true);
		}
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x00020C94 File Offset: 0x0001EE94
	[ContextMenu("Collect All Rewards")]
	public void CollectAllRewards()
	{
		QuestReward[] array = Object.FindObjectsOfType<QuestReward>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].GiveReward();
		}
	}

	// Token: 0x040003A7 RID: 935
	private static CompletionStats instance;

	// Token: 0x040003A8 RID: 936
	public PersistentObject[] completionObjects;

	// Token: 0x040003A9 RID: 937
	public CharacterProfile[] completionCharacters;

	// Token: 0x040003AA RID: 938
	public DialogueActor[] completionActors;

	// Token: 0x040003AB RID: 939
	public ItemObject[] completionItems;

	// Token: 0x040003AC RID: 940
	private const string percentObjectKey = "Completion_Objects";

	// Token: 0x040003AD RID: 941
	private const string percentCharactersKey = "Completion_Characters";

	// Token: 0x040003AE RID: 942
	private const string percentItemsKey = "Completion_Items";

	// Token: 0x040003AF RID: 943
	public GameObject completeObjectsDialogue;

	// Token: 0x040003B0 RID: 944
	public GameObject completeCharactersDialogue;

	// Token: 0x040003B1 RID: 945
	public QuestStates townStates;

	// Token: 0x040003B2 RID: 946
	public Achievement objectsAchievement;

	// Token: 0x040003B3 RID: 947
	public Achievement friendsAchievement;
}
