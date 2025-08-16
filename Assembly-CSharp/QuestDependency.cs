using System;
using UnityEngine;
using UnityEngine.Events;

public class QuestDependency : MonoBehaviour
{
	// Token: 0x060001B0 RID: 432 RVA: 0x0001D49C File Offset: 0x0001B69C
	private void Start()
	{
		QuestProfile[] array = this.quests;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].onComplete.AddListener(new UnityAction(this.CheckQuests));
		}
		this.CheckQuests(true);
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x000036E1 File Offset: 0x000018E1
	private void CheckQuests()
	{
		this.CheckQuests(false);
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x0001D4E0 File Offset: 0x0001B6E0
	private void CheckQuests(bool isInitial)
	{
		bool flag = true;
		this.totalCompletedDependencies = 0;
		QuestProfile[] array = this.quests;
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].IsComplete)
			{
				flag = false;
			}
			else
			{
				this.totalCompletedDependencies++;
			}
		}
		if (flag)
		{
			array = this.quests;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].onComplete.RemoveListener(new UnityAction(this.CheckQuests));
			}
		}
		GameObject[] array2 = this.activateObjects;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].SetActive(flag);
		}
		if (!isInitial && flag)
		{
			this.onAllComplete.Invoke();
		}
	}

	public QuestProfile[] quests;

	public GameObject[] activateObjects;

	public UnityEvent onAllComplete;

	public int totalCompletedDependencies;
}
