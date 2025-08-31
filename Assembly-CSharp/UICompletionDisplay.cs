using System;
using UnityEngine;
using UnityEngine.UI;

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

	public GameObject objectPercentObject;

	public GameObject characterPercentObject;

	public UIItemResource objectResource;

	public UIItemResource characterResource;

	public Text objectPercentDisplay;

	public Text characterPercentDisplay;

	public QuestStates playgroundStates;
}
