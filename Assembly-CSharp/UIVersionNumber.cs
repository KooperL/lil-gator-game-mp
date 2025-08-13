using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003DB RID: 987
public class UIVersionNumber : MonoBehaviour
{
	// Token: 0x06001303 RID: 4867 RVA: 0x00010240 File Offset: 0x0000E440
	private void Start()
	{
		base.GetComponent<Text>().text = "1.0.2";
	}
}
