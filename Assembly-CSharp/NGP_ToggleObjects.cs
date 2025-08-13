using System;
using UnityEngine;

// Token: 0x02000184 RID: 388
public class NGP_ToggleObjects : MonoBehaviour
{
	// Token: 0x060007F8 RID: 2040 RVA: 0x00026834 File Offset: 0x00024A34
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

	// Token: 0x04000A29 RID: 2601
	[Tooltip("Objects will be ENABLED if NG+")]
	public GameObject[] newGamePlusObjects;

	// Token: 0x04000A2A RID: 2602
	[Tooltip("Objects will be DISABLED if NG+")]
	public GameObject[] nonNewGamePlusObjects;
}
