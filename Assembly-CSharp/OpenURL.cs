using System;
using UnityEngine;

// Token: 0x020002AC RID: 684
public class OpenURL : MonoBehaviour
{
	// Token: 0x06000E72 RID: 3698 RVA: 0x00045149 File Offset: 0x00043349
	public void ExecuteAction()
	{
		Application.OpenURL(this.url);
	}

	// Token: 0x040012C7 RID: 4807
	public string url;
}
