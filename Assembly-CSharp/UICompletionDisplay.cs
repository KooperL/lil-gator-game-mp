using System;
using UnityEngine;
using UnityEngine.UI;

public class UICompletionDisplay : MonoBehaviour
{
	// Token: 0x060011FF RID: 4607 RVA: 0x0000F4B0 File Offset: 0x0000D6B0
	private void OnEnable()
	{
		this.UpdateDisplay();
	}

	// Token: 0x06001200 RID: 4608 RVA: 0x00059F3C File Offset: 0x0005813C
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

	// Token: 0x06001201 RID: 4609 RVA: 0x00059F98 File Offset: 0x00058198
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
