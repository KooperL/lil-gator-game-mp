using System;
using System.Collections.Generic;
using UnityEngine;

public class SetActorName : MonoBehaviour
{
	// Token: 0x06000F37 RID: 3895 RVA: 0x00050520 File Offset: 0x0004E720
	public static void ForceRefresh()
	{
		foreach (SetActorName setActorName in SetActorName.active)
		{
			setActorName.UpdateName();
		}
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x0000D2DA File Offset: 0x0000B4DA
	public void OnEnable()
	{
		SetActorName.active.Add(this);
		this.UpdateName();
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x0000D2ED File Offset: 0x0000B4ED
	private void OnDisable()
	{
		if (SetActorName.active.Contains(this))
		{
			SetActorName.active.Remove(this);
		}
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x0000D308 File Offset: 0x0000B508
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
