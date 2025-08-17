using System;
using System.Collections.Generic;
using UnityEngine;

public class SetActorName : MonoBehaviour
{
	// Token: 0x06000F36 RID: 3894 RVA: 0x00050258 File Offset: 0x0004E458
	public static void ForceRefresh()
	{
		foreach (SetActorName setActorName in SetActorName.active)
		{
			setActorName.UpdateName();
		}
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x0000D2D0 File Offset: 0x0000B4D0
	public void OnEnable()
	{
		SetActorName.active.Add(this);
		this.UpdateName();
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x0000D2E3 File Offset: 0x0000B4E3
	private void OnDisable()
	{
		if (SetActorName.active.Contains(this))
		{
			SetActorName.active.Remove(this);
		}
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x0000D2FE File Offset: 0x0000B4FE
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
