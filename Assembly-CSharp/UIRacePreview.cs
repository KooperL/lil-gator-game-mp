using System;
using UnityEngine;
using UnityEngine.UI;

public class UIRacePreview : MonoBehaviour
{
	// Token: 0x06000DF9 RID: 3577 RVA: 0x00043B8D File Offset: 0x00041D8D
	public void Load(Transform anchor, float previousBest)
	{
		this.uiFollow.followTarget = anchor;
		this.previousBestDisplay.text = UIRaceIcon.TimerFormat(previousBest);
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x00043BB8 File Offset: 0x00041DB8
	public void Clear()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DFB RID: 3579 RVA: 0x00043BC6 File Offset: 0x00041DC6
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
