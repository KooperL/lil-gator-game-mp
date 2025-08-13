using System;
using UnityEngine;

// Token: 0x0200037B RID: 891
public class RenameCharacter : MonoBehaviour
{
	// Token: 0x060010FD RID: 4349 RVA: 0x0000E9ED File Offset: 0x0000CBED
	public void DoTheThing()
	{
		UIMenus.u.CloseMenus();
		UINameInput.ShowNameInputPrompt();
	}
}
