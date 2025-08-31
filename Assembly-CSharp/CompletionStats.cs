using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CompletionStats : MonoBehaviour
{
	// (get) Token: 0x06000257 RID: 599 RVA: 0x0000C7BD File Offset: 0x0000A9BD
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

	// (get) Token: 0x06000258 RID: 600 RVA: 0x0000C7DC File Offset: 0x0000A9DC
	// (set) Token: 0x06000259 RID: 601 RVA: 0x0000C7EE File Offset: 0x0000A9EE
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

	// (get) Token: 0x0600025A RID: 602 RVA: 0x0000C800 File Offset: 0x0000AA00
	// (set) Token: 0x0600025B RID: 603 RVA: 0x0000C812 File Offset: 0x0000AA12
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

	// (get) Token: 0x0600025C RID: 604 RVA: 0x0000C824 File Offset: 0x0000AA24
	// (set) Token: 0x0600025D RID: 605 RVA: 0x0000C836 File Offset: 0x0000AA36
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

	// Token: 0x0600025E RID: 606 RVA: 0x0000C848 File Offset: 0x0000AA48
	private void Awake()
	{
		this.townStates.onStateChange.AddListener(new UnityAction<int>(this.OnStateChange));
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0000C866 File Offset: 0x0000AA66
	private void OnStateChange(int newStateIndex)
	{
		if (newStateIndex == 4)
		{
			this.CheckForFullCompletion();
		}
	}

	// Token: 0x06000260 RID: 608 RVA: 0x0000C874 File Offset: 0x0000AA74
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

	// Token: 0x06000261 RID: 609 RVA: 0x0000C8DE File Offset: 0x0000AADE
	[ContextMenu("Update all")]
	private void Start()
	{
		this.UpdateObjects();
		this.UpdateCharacters(null, true);
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0000C8F0 File Offset: 0x0000AAF0
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

	// Token: 0x06000263 RID: 611 RVA: 0x0000C980 File Offset: 0x0000AB80
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

	// Token: 0x06000264 RID: 612 RVA: 0x0000CA18 File Offset: 0x0000AC18
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

	// Token: 0x06000265 RID: 613 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
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

	// Token: 0x06000266 RID: 614 RVA: 0x0000CB21 File Offset: 0x0000AD21
	public void CheckForFullCompletion()
	{
		if (CompletionStats.PercentObjects == 100 && CompletionStats.PercentCharacters == 100 && this.townStates.StateID == 4)
		{
			this.townStates.JustProgressState();
		}
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0000CB50 File Offset: 0x0000AD50
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

	// Token: 0x06000268 RID: 616 RVA: 0x0000CBB4 File Offset: 0x0000ADB4
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

	// Token: 0x06000269 RID: 617 RVA: 0x0000CCF0 File Offset: 0x0000AEF0
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

	// Token: 0x0600026A RID: 618 RVA: 0x0000CE9C File Offset: 0x0000B09C
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

	// Token: 0x0600026B RID: 619 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
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

	// Token: 0x0600026C RID: 620 RVA: 0x0000CF30 File Offset: 0x0000B130
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

	// Token: 0x0600026D RID: 621 RVA: 0x0000CF98 File Offset: 0x0000B198
	[ContextMenu("Break ALL Breakables")]
	public void BreakALLBreakables()
	{
		BreakableObject[] array = Object.FindObjectsOfType<BreakableObject>(false);
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Break(false, Vector3.zero, true);
		}
	}

	// Token: 0x0600026E RID: 622 RVA: 0x0000CFCC File Offset: 0x0000B1CC
	[ContextMenu("Collect All Rewards")]
	public void CollectAllRewards()
	{
		QuestReward[] array = Object.FindObjectsOfType<QuestReward>();
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
