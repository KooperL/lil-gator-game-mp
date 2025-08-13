using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000080 RID: 128
public class QuestDependency : MonoBehaviour
{
	// Token: 0x060001A3 RID: 419 RVA: 0x0001CBD8 File Offset: 0x0001ADD8
	private void Start()
	{
		QuestProfile[] array = this.quests;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].onComplete.AddListener(new UnityAction(this.CheckQuests));
		}
		this.CheckQuests(true);
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x000035F7 File Offset: 0x000017F7
	private void CheckQuests()
	{
		this.CheckQuests(false);
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x0001CC1C File Offset: 0x0001AE1C
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

	// Token: 0x04000292 RID: 658
	public QuestProfile[] quests;

	// Token: 0x04000293 RID: 659
	public GameObject[] activateObjects;

	// Token: 0x04000294 RID: 660
	public UnityEvent onAllComplete;

	// Token: 0x04000295 RID: 661
	public int totalCompletedDependencies;
}
