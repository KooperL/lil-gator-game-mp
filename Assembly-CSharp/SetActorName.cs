using System;
using System.Collections.Generic;
using UnityEngine;

public class SetActorName : MonoBehaviour
{
	// Token: 0x06000F36 RID: 3894 RVA: 0x000500C4 File Offset: 0x0004E2C4
	public static void ForceRefresh()
	{
		foreach (SetActorName setActorName in SetActorName.active)
		{
			setActorName.UpdateName();
		}
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x0000D2BB File Offset: 0x0000B4BB
	public void OnEnable()
	{
		SetActorName.active.Add(this);
		this.UpdateName();
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x0000D2CE File Offset: 0x0000B4CE
	private void OnDisable()
	{
		if (SetActorName.active.Contains(this))
		{
			SetActorName.active.Remove(this);
		}
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x0000D2E9 File Offset: 0x0000B4E9
	private void UpdateName()
	{
		this.character.SetName(this.actorName, this.document);
	}

	public static List<SetActorName> active = new List<SetActorName>();

	public MultilingualTextDocument document;

	public CharacterProfile character;

	[TextLookup("document")]
	public string actorName;
}
