using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200036A RID: 874
public class UIRacePreview : MonoBehaviour
{
	// Token: 0x060010C2 RID: 4290 RVA: 0x0000E5F2 File Offset: 0x0000C7F2
	public void Load(Transform anchor, float previousBest)
	{
		this.uiFollow.followTarget = anchor;
		this.previousBestDisplay.text = UIRaceIcon.TimerFormat(previousBest);
		base.gameObject.SetActive(true);
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x00009344 File Offset: 0x00007544
	public void Clear()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060010C4 RID: 4292 RVA: 0x0000E61D File Offset: 0x0000C81D
	private void Update()
	{
		if (Vector3.Distance(this.uiFollow.followTarget.position, Player.Position) > 10f)
		{
			this.Clear();
		}
	}

	// Token: 0x040015D2 RID: 5586
	public UIFollow uiFollow;

	// Token: 0x040015D3 RID: 5587
	public Text previousBestDisplay;
}
