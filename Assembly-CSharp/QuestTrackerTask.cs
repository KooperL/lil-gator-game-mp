using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000368 RID: 872
public class QuestTrackerTask : MonoBehaviour
{
	// Token: 0x060010B9 RID: 4281 RVA: 0x0000E585 File Offset: 0x0000C785
	public void Load(QuestProfile.QuestTask task)
	{
		if (this.taskText != null)
		{
			this.taskText.text = task.GetTaskText();
		}
	}

	// Token: 0x040015C4 RID: 5572
	public Text taskText;
}
