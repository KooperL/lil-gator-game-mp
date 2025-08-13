using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Token: 0x02000209 RID: 521
public static class StringHelper
{
	// Token: 0x060009A2 RID: 2466 RVA: 0x000095AA File Offset: 0x000077AA
	public static IEnumerable<string> EnumerateCSV(this string input)
	{
		Regex regex = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);
		foreach (object obj in regex.Matches(input))
		{
			Match match = (Match)obj;
			yield return match.Value.TrimStart(new char[] { ',' });
		}
		IEnumerator enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x0003A12C File Offset: 0x0003832C
	public static string[] SplitCSV(this string input, char separatorCharacter)
	{
		string[] array = new Regex(separatorCharacter.ToString() + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))").Split(input);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].StartsWith("\"") && array[i].EndsWith("\""))
			{
				array[i] = array[i].Substring(1, array[i].Length - 2);
			}
			while (array[i].Contains("\"\""))
			{
				int num = array[i].IndexOf("\"\"");
				array[i] = array[i].Remove(num, 1);
			}
		}
		return array;
	}
}
