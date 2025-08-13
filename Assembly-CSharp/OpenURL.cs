using System;
using UnityEngine;

// Token: 0x02000389 RID: 905
public class OpenURL : MonoBehaviour
{
	// Token: 0x06001142 RID: 4418 RVA: 0x0000ECA8 File Offset: 0x0000CEA8
	public void ExecuteAction()
	{
		Application.OpenURL(this.url);
	}

	// Token: 0x0400162F RID: 5679
	public string url;
}
