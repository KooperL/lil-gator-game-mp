using System;
using UnityEngine;

public class ScriptObject : ScriptableObject
{
	// Token: 0x06000024 RID: 36 RVA: 0x00017BAC File Offset: 0x00015DAC
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

	public string text;

	public DialogueChunk[] chunks;

	private string[] emoteDatabase = new string[] { "E_Talk", "E_RaiseArms", "E_Shrug", "E_ShowItem", "E_Point", "E_Shy", "E_Think", "E_ArmsCrossed" };
}
