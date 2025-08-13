using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200028E RID: 654
public class QuestTrackerTask : MonoBehaviour
{
	// Token: 0x06000DF0 RID: 3568 RVA: 0x000438E6 File Offset: 0x00041AE6
	public void Load(QuestProfile.QuestTask task)
	{
		if (this.taskText != null)
		{
			this.taskText.text = task.GetTaskText();
		}
	}

	// Token: 0x04001264 RID: 4708
	public Text taskText;
}
