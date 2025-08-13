using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000064 RID: 100
public class QuestDependency : MonoBehaviour
{
	// Token: 0x06000177 RID: 375 RVA: 0x0000899C File Offset: 0x00006B9C
	private void Start()
	{
		QuestProfile[] array = this.quests;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].onComplete.AddListener(new UnityAction(this.CheckQuests));
		}
		this.CheckQuests(true);
	}

	// Token: 0x06000178 RID: 376 RVA: 0x000089DE File Offset: 0x00006BDE
	private void CheckQuests()
	{
		this.CheckQuests(false);
	}

	// Token: 0x06000179 RID: 377 RVA: 0x000089E8 File Offset: 0x00006BE8
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

	// Token: 0x04000220 RID: 544
	public QuestProfile[] quests;

	// Token: 0x04000221 RID: 545
	public GameObject[] activateObjects;

	// Token: 0x04000222 RID: 546
	public UnityEvent onAllComplete;

	// Token: 0x04000223 RID: 547
	public int totalCompletedDependencies;
}
