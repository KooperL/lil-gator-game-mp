using System;
using UnityEngine.Serialization;

// Token: 0x02000012 RID: 18
[Serializable]
public class DialogueChunk
{
	// Token: 0x04000035 RID: 53
	[FormerlySerializedAs("idString")]
	public string name;

	// Token: 0x04000036 RID: 54
	public int id;

	// Token: 0x04000037 RID: 55
	public DialogueLine[] lines;

	// Token: 0x04000038 RID: 56
	public string[] options;

	// Token: 0x04000039 RID: 57
	public MultilingualString[] mlOptions;
}
