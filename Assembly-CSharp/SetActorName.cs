using System;
using System.Collections.Generic;
using UnityEngine;

public class SetActorName : MonoBehaviour
{
	// Token: 0x06000C3B RID: 3131 RVA: 0x0003AEEC File Offset: 0x000390EC
	public static void ForceRefresh()
	{
		foreach (SetActorName setActorName in SetActorName.active)
		{
			setActorName.UpdateName();
		}
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x0003AF3C File Offset: 0x0003913C
	public void OnEnable()
	{
		SetActorName.active.Add(this);
		this.UpdateName();
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x0003AF4F File Offset: 0x0003914F
	private void OnDisable()
	{
		if (SetActorName.active.Contains(this))
		{
			SetActorName.active.Remove(this);
		}
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x0003AF6A File Offset: 0x0003916A
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
