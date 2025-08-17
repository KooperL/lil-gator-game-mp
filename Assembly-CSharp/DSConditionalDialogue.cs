using System;

public class DSConditionalDialogue : DSDialogue
{
	// Token: 0x06000598 RID: 1432 RVA: 0x0002F0E4 File Offset: 0x0002D2E4
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

	public QuestProfile[] quests;

	public CharacterProfile[] characters;

	[ChunkLookup("document")]
	public string allComplete;

	[ChunkLookup("document")]
	public string someComplete;

	[ChunkLookup("document")]
	public string noneComplete;
}
