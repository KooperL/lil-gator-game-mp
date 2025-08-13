using System;
using UnityEngine;

// Token: 0x0200006C RID: 108
public class CheckIndieland : MonoBehaviour
{
	// Token: 0x0600015E RID: 350 RVA: 0x000032FC File Offset: 0x000014FC
	private void Awake()
	{
		if (false != this.showIfIndieland)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0400021D RID: 541
	public bool showIfIndieland = true;
}
