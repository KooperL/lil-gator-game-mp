using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003A1 RID: 929
public class UICompletionDisplay : MonoBehaviour
{
	// Token: 0x0600119F RID: 4511 RVA: 0x0000F0C7 File Offset: 0x0000D2C7
	private void OnEnable()
	{
		this.UpdateDisplay();
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x00057F78 File Offset: 0x00056178
	private void OnDisable()
	{
		if (this.objectPercentObject != null)
		{
			this.objectPercentObject.SetActive(false);
		}
		if (this.characterPercentObject != null)
		{
			this.characterPercentObject.SetActive(false);
		}
		this.objectResource.skipDelta = false;
		this.characterResource.skipDelta = false;
	}

	// Token: 0x060011A1 RID: 4513 RVA: 0x00057FD4 File Offset: 0x000561D4
	public void UpdateDisplay()
	{
		bool flag = this.playgroundStates.StateID >= 4;
		this.objectPercentObject.SetActive(flag);
		this.characterPercentObject.SetActive(flag);
		if (flag)
		{
			this.objectResource.skipDelta = true;
			this.characterResource.skipDelta = true;
			this.objectPercentDisplay.text = string.Format("{0}%", CompletionStats.PercentObjects);
			this.characterPercentDisplay.text = string.Format("{0}%", CompletionStats.PercentCharacters);
		}
	}

	// Token: 0x040016B4 RID: 5812
	public GameObject objectPercentObject;

	// Token: 0x040016B5 RID: 5813
	public GameObject characterPercentObject;

	// Token: 0x040016B6 RID: 5814
	public UIItemResource objectResource;

	// Token: 0x040016B7 RID: 5815
	public UIItemResource characterResource;

	// Token: 0x040016B8 RID: 5816
	public Text objectPercentDisplay;

	// Token: 0x040016B9 RID: 5817
	public Text characterPercentDisplay;

	// Token: 0x040016BA RID: 5818
	public QuestStates playgroundStates;
}
