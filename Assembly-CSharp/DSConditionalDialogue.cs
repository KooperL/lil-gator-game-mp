using System;

// Token: 0x0200011B RID: 283
public class DSConditionalDialogue : DSDialogue
{
	// Token: 0x0600055E RID: 1374 RVA: 0x0002D9E8 File Offset: 0x0002BBE8
	public override void Activate()
	{
		int num = this.quests.Length + this.characters.Length;
		int num2 = 0;
		QuestProfile[] array = this.quests;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].IsComplete)
			{
				num2++;
			}
		}
		CharacterProfile[] array2 = this.characters;
		for (int i = 0; i < array2.Length; i++)
		{
			if (array2[i].IsUnlocked)
			{
				num2++;
			}
		}
		if (num2 == 0)
		{
			this.dialogue = this.noneComplete;
		}
		else if (num2 < num)
		{
			this.dialogue = this.someComplete;
		}
		else
		{
			this.dialogue = this.allComplete;
		}
		if (!string.IsNullOrEmpty(this.dialogue))
		{
			base.Activate();
		}
	}

	// Token: 0x04000760 RID: 1888
	public QuestProfile[] quests;

	// Token: 0x04000761 RID: 1889
	public CharacterProfile[] characters;

	// Token: 0x04000762 RID: 1890
	[ChunkLookup("document")]
	public string allComplete;

	// Token: 0x04000763 RID: 1891
	[ChunkLookup("document")]
	public string someComplete;

	// Token: 0x04000764 RID: 1892
	[ChunkLookup("document")]
	public string noneComplete;
}
