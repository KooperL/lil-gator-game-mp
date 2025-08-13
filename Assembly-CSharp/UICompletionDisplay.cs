using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002BF RID: 703
public class UICompletionDisplay : MonoBehaviour
{
	// Token: 0x06000EC7 RID: 3783 RVA: 0x00046A32 File Offset: 0x00044C32
	private void OnEnable()
	{
		this.UpdateDisplay();
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x00046A3C File Offset: 0x00044C3C
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

	// Token: 0x06000EC9 RID: 3785 RVA: 0x00046A98 File Offset: 0x00044C98
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

	// Token: 0x0400133E RID: 4926
	public GameObject objectPercentObject;

	// Token: 0x0400133F RID: 4927
	public GameObject characterPercentObject;

	// Token: 0x04001340 RID: 4928
	public UIItemResource objectResource;

	// Token: 0x04001341 RID: 4929
	public UIItemResource characterResource;

	// Token: 0x04001342 RID: 4930
	public Text objectPercentDisplay;

	// Token: 0x04001343 RID: 4931
	public Text characterPercentDisplay;

	// Token: 0x04001344 RID: 4932
	public QuestStates playgroundStates;
}
