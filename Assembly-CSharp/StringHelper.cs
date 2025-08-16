using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class StringHelper
{
	// Token: 0x060009E9 RID: 2537 RVA: 0x000098C9 File Offset: 0x00007AC9
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

	// Token: 0x060009EA RID: 2538 RVA: 0x0003BA08 File Offset: 0x00039C08
	public static string[] SplitRecords(this string input)
	{
		string[] array = new Regex("\n(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))").Split(input);
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = array[i].Trim();
		}
		return array;
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x0003BA40 File Offset: 0x00039C40
	public static string[] SplitCSV(this string input, char separatorCharacter, bool removeQuotes = true)
	{
		string[] array = new Regex(separatorCharacter.ToString() + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))").Split(input);
		if (removeQuotes)
		{
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
		}
		return array;
	}
}
