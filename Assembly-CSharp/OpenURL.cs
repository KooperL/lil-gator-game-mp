using System;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
	// Token: 0x06000E72 RID: 3698 RVA: 0x00045149 File Offset: 0x00043349
	public void ExecuteAction()
	{
		Application.OpenURL(this.url);
	}

	public string url;
}
