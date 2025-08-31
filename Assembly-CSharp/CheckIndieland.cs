using System;
using UnityEngine;

public class CheckIndieland : MonoBehaviour
{
	// Token: 0x06000139 RID: 313 RVA: 0x00007B07 File Offset: 0x00005D07
	private void Awake()
	{
		if (false != this.showIfIndieland)
		{
			base.gameObject.SetActive(false);
		}
	}

	public bool showIfIndieland = true;
}
