using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002EA RID: 746
public class UIVersionNumber : MonoBehaviour
{
	// Token: 0x06000FD7 RID: 4055 RVA: 0x0004B9BC File Offset: 0x00049BBC
	private void Start()
	{
		base.GetComponent<Text>().text = "1.0.3";
	}
}
