using System;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class MultilingualTextDocument : ScriptableObject
{
	// Token: 0x06000035 RID: 53 RVA: 0x00002BFF File Offset: 0x00000DFF
	public static void SetPlaceholder(int index, string replacementText)
	{
		MultilingualTextDocument.placeholders[index] = replacementText;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00002C0C File Offset: 0x00000E0C
	public string FetchString(string id, Language language = Language.Auto)
	{
		if (string.IsNullOrEmpty(id))
		{
			return "";
		}
		foreach (MultilingualString multilingualString in this.mlStrings)
		{
			if (multilingualString.idString == id)
			{
				return multilingualString.GetText(language);
			}
		}
		return id;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002C5C File Offset: 0x00000E5C
	public string FetchString(int id, Language language = Language.Auto)
	{
		foreach (MultilingualString multilingualString in this.mlStrings)
		{
			if (multilingualString.id == id)
			{
				return multilingualString.GetText(language);
			}
		}
		return "No string found";
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00002CA0 File Offset: 0x00000EA0
	public bool HasString(string id)
	{
		if (string.IsNullOrEmpty(id))
		{
			return false;
		}
		MultilingualString[] array = this.mlStrings;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].idString == id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00002CE4 File Offset: 0x00000EE4
	public bool HasChunk(string id)
	{
		int num = Animator.StringToHash(id);
		DialogueChunk[] array = this.chunks;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].id == num)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00002D1C File Offset: 0x00000F1C
	public DialogueChunk FetchChunk(string id)
	{
		int num = Animator.StringToHash(id);
		foreach (DialogueChunk dialogueChunk in this.chunks)
		{
			if (dialogueChunk.id == num)
			{
				return dialogueChunk;
			}
		}
		Debug.LogError("No chunk of id " + id + " found");
		return null;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00002D6C File Offset: 0x00000F6C
	internal string FindString(string text)
	{
		foreach (MultilingualString multilingualString in this.mlStrings)
		{
			if (multilingualString.english[0] == text)
			{
				return multilingualString.idString;
			}
		}
		return "";
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00002DB2 File Offset: 0x00000FB2
	public void AddStringEntry(string entryKey, string englishText)
	{
	}

	// Token: 0x04000052 RID: 82
	public static string[] placeholders = new string[1];

	// Token: 0x04000053 RID: 83
	public const int currentPlatform = 0;

	// Token: 0x04000054 RID: 84
	public string[] idStrings;

	// Token: 0x04000055 RID: 85
	public MultilingualString[] mlStrings;

	// Token: 0x04000056 RID: 86
	public string[] idChunks;

	// Token: 0x04000057 RID: 87
	public DialogueChunk[] chunks;
}
