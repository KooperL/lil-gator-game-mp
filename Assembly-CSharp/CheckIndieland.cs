using System;
using UnityEngine;

public class CheckIndieland : MonoBehaviour
{
	// Token: 0x06000166 RID: 358 RVA: 0x0000339F File Offset: 0x0000159F
	private void Awake()
	{
		if (false != this.showIfIndieland)
		{
			base.gameObject.SetActive(false);
		}
	}

	public bool showIfIndieland = true;
}
