using System;
using UnityEngine.Serialization;

// Token: 0x02000011 RID: 17
[Serializable]
public class DialogueChunk
{
	// Token: 0x0400002B RID: 43
	[FormerlySerializedAs("idString")]
	public string name;

	// Token: 0x0400002C RID: 44
	public int id;

	// Token: 0x0400002D RID: 45
	public DialogueLine[] lines;

	// Token: 0x0400002E RID: 46
	public string[] options;

	// Token: 0x0400002F RID: 47
	public MultilingualString[] mlOptions;
}
