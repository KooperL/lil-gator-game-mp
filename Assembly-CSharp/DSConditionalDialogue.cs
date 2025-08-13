using System;

// Token: 0x020000D1 RID: 209
public class DSConditionalDialogue : DSDialogue
{
	// Token: 0x06000476 RID: 1142 RVA: 0x00018F94 File Offset: 0x00017194
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

	// Token: 0x0400063E RID: 1598
	public QuestProfile[] quests;

	// Token: 0x0400063F RID: 1599
	public CharacterProfile[] characters;

	// Token: 0x04000640 RID: 1600
	[ChunkLookup("document")]
	public string allComplete;

	// Token: 0x04000641 RID: 1601
	[ChunkLookup("document")]
	public string someComplete;

	// Token: 0x04000642 RID: 1602
	[ChunkLookup("document")]
	public string noneComplete;
}
