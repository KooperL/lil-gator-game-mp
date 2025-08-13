using System;
using UnityEngine;

// Token: 0x020002F1 RID: 753
public class SetActorName : MonoBehaviour
{
	// Token: 0x06000EDD RID: 3805 RVA: 0x0000CF5A File Offset: 0x0000B15A
	public void OnEnable()
	{
		this.UpdateName();
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x0000CF62 File Offset: 0x0000B162
	private void UpdateName()
	{
		this.character.SetName(this.actorName, this.document);
	}

	// Token: 0x040012F5 RID: 4853
	public MultilingualTextDocument document;

	// Token: 0x040012F6 RID: 4854
	public CharacterProfile character;

	// Token: 0x040012F7 RID: 4855
	[TextLookup("document")]
	public string actorName;
}
