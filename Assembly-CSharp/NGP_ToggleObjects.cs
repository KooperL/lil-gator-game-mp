using System;
using UnityEngine;

public class NGP_ToggleObjects : MonoBehaviour
{
	// Token: 0x0600099C RID: 2460 RVA: 0x0003AE5C File Offset: 0x0003905C
	private void Start()
	{
		if (Game.IsNewGamePlus)
		{
			GameObject[] array = this.newGamePlusObjects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(true);
			}
			array = this.nonNewGamePlusObjects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(false);
			}
		}
	}

	[Tooltip("Objects will be ENABLED if NG+")]
	public GameObject[] newGamePlusObjects;

	[Tooltip("Objects will be DISABLED if NG+")]
	public GameObject[] nonNewGamePlusObjects;
}
