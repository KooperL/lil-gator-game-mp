using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestTrackerTask : MonoBehaviour
{
	// Token: 0x06001115 RID: 4373 RVA: 0x0000E8F8 File Offset: 0x0000CAF8
	public void Load(QuestProfile.QuestTask task)
	{
		if (this.taskText != null)
		{
			this.taskText.text = task.GetTaskText();
		}
	}

	public Text taskText;
}
