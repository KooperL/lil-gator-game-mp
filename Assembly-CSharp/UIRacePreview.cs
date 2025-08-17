using System;
using UnityEngine;
using UnityEngine.UI;

public class UIRacePreview : MonoBehaviour
{
	// Token: 0x0600111D RID: 4381 RVA: 0x0000E95B File Offset: 0x0000CB5B
	public void Load(Transform anchor, float previousBest)
	{
		this.uiFollow.followTarget = anchor;
		this.previousBestDisplay.text = UIRaceIcon.TimerFormat(previousBest);
		base.gameObject.SetActive(true);
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x000096A2 File Offset: 0x000078A2
	public void Clear()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x0000E986 File Offset: 0x0000CB86
	private void Update()
	{
		if (Vector3.Distance(this.uiFollow.followTarget.position, Player.Position) > 10f)
		{
			this.Clear();
		}
	}

	public UIFollow uiFollow;

	public Text previousBestDisplay;
}
