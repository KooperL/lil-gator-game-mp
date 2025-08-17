using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestTrackerTask : MonoBehaviour
{
	// Token: 0x06001114 RID: 4372 RVA: 0x0000E8EE File Offset: 0x0000CAEE
	public void Load(QuestProfile.QuestTask task)
	{
		if (this.taskText != null)
		{
			this.taskText.text = task.GetTaskText();
		}
	}

	public Text taskText;
}
