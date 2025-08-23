using System;
using UnityEngine;
using UnityEngine.UI;

public class UICompletionDisplay : MonoBehaviour
{
	// Token: 0x06001200 RID: 4608 RVA: 0x0000F4BA File Offset: 0x0000D6BA
	private void OnEnable()
	{
		this.UpdateDisplay();
	}

	// Token: 0x06001201 RID: 4609 RVA: 0x0005A204 File Offset: 0x00058404
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

	// Token: 0x06001202 RID: 4610 RVA: 0x0005A260 File Offset: 0x00058460
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
