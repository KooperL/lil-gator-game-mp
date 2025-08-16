using System;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
	// Token: 0x060011A2 RID: 4514 RVA: 0x0000F07C File Offset: 0x0000D27C
	public void ExecuteAction()
	{
		Application.OpenURL(this.url);
	}

	public string url;
}
