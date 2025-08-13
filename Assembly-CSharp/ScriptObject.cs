using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class ScriptObject : ScriptableObject
{
	// Token: 0x06000024 RID: 36 RVA: 0x00002768 File Offset: 0x00000968
	private string GetCleanString(string source)
	{
		source = source.Replace("\r\n", " ");
		source = source.Replace("\n", " ");
		source = source.Replace(Environment.NewLine, " ");
		source = source.Replace("  ", " ");
		source = source.Replace("\"", "\"\"");
		if (source.Contains("\"") || source.Contains(","))
		{
			source = string.Format("\"{0}\"", source);
		}
		return source;
	}

	// Token: 0x0400001E RID: 30
	public string text;

	// Token: 0x0400001F RID: 31
	public DialogueChunk[] chunks;

	// Token: 0x04000020 RID: 32
	private string[] emoteDatabase = new string[] { "E_Talk", "E_RaiseArms", "E_Shrug", "E_ShowItem", "E_Point", "E_Shy", "E_Think", "E_ArmsCrossed" };
}
