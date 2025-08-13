using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000233 RID: 563
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

	// Token: 0x04000FF9 RID: 4089
	public static List<SetActorName> active = new List<SetActorName>();

	// Token: 0x04000FFA RID: 4090
	public MultilingualTextDocument document;

	// Token: 0x04000FFB RID: 4091
	public CharacterProfile character;

	// Token: 0x04000FFC RID: 4092
	[TextLookup("document")]
	public string actorName;
}
