using System;
using UnityEngine;
using UnityEngine.UI;

public class UIRacePreview : MonoBehaviour
{
	// Token: 0x0600111E RID: 4382 RVA: 0x0000E965 File Offset: 0x0000CB65
	public void Load(Transform anchor, float previousBest)
	{
		this.uiFollow.followTarget = anchor;
		this.previousBestDisplay.text = UIRaceIcon.TimerFormat(previousBest);
		base.gameObject.SetActive(true);
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x000096AC File Offset: 0x000078AC
	public void Clear()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x0000E990 File Offset: 0x0000CB90
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
