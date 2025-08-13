using System;
using UnityEngine;

// Token: 0x02000053 RID: 83
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

	// Token: 0x040001B9 RID: 441
	public bool showIfIndieland = true;
}
