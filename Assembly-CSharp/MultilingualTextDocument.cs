using System;
using UnityEngine;

public class MultilingualTextDocument : ScriptableObject
{
	// Token: 0x06000035 RID: 53 RVA: 0x000022F7 File Offset: 0x000004F7
	public static void SetPlaceholder(int index, string replacementText)
	{
		MultilingualTextDocument.placeholders[index] = replacementText;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00017F8C File Offset: 0x0001618C
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

	// Token: 0x06000037 RID: 55 RVA: 0x00017FDC File Offset: 0x000161DC
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

	// Token: 0x06000038 RID: 56 RVA: 0x00018020 File Offset: 0x00016220
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

	// Token: 0x06000039 RID: 57 RVA: 0x00018064 File Offset: 0x00016264
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

	// Token: 0x0600003A RID: 58 RVA: 0x0001809C File Offset: 0x0001629C
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

	// Token: 0x0600003B RID: 59 RVA: 0x000180EC File Offset: 0x000162EC
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

	// Token: 0x0600003C RID: 60 RVA: 0x00002229 File Offset: 0x00000429
	public void AddStringEntry(string entryKey, string englishText)
	{
	}

	public static string[] placeholders = new string[1];

	public const int currentPlatform = 0;

	public string[] idStrings;

	public MultilingualString[] mlStrings;

	public string[] idChunks;

	public DialogueChunk[] chunks;
}
