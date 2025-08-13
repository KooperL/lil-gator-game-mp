using System;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class MultilingualTextDocument : ScriptableObject
{
	// Token: 0x06000031 RID: 49 RVA: 0x000022C5 File Offset: 0x000004C5
	public static void SetPlaceholder(int index, string replacementText)
	{
		MultilingualTextDocument.placeholders[index] = replacementText;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00017778 File Offset: 0x00015978
	public string FetchString(string id, Language language = Language.English)
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

	// Token: 0x06000033 RID: 51 RVA: 0x000177C8 File Offset: 0x000159C8
	public string FetchString(int id, Language language = Language.English)
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

	// Token: 0x06000034 RID: 52 RVA: 0x0001780C File Offset: 0x00015A0C
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

	// Token: 0x06000035 RID: 53 RVA: 0x00017850 File Offset: 0x00015A50
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

	// Token: 0x06000036 RID: 54 RVA: 0x00017888 File Offset: 0x00015A88
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

	// Token: 0x06000037 RID: 55 RVA: 0x000178D8 File Offset: 0x00015AD8
	internal string FindString(string text)
	{
		foreach (MultilingualString multilingualString in this.mlStrings)
		{
			if (multilingualString.english == text)
			{
				return multilingualString.idString;
			}
		}
		return "";
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00002229 File Offset: 0x00000429
	public void AddStringEntry(string entryKey, string englishText)
	{
	}

	// Token: 0x04000041 RID: 65
	public static string[] placeholders = new string[1];

	// Token: 0x04000042 RID: 66
	public string[] idStrings;

	// Token: 0x04000043 RID: 67
	public MultilingualString[] mlStrings;

	// Token: 0x04000044 RID: 68
	public string[] idChunks;

	// Token: 0x04000045 RID: 69
	public DialogueChunk[] chunks;
}
