using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class StringHelper
{
	// Token: 0x06000833 RID: 2099 RVA: 0x00027310 File Offset: 0x00025510
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

	// Token: 0x06000834 RID: 2100 RVA: 0x00027320 File Offset: 0x00025520
	public static string[] SplitRecords(this string input)
	{
		string[] array = new Regex("\n(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))").Split(input);
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = array[i].Trim();
		}
		return array;
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x00027358 File Offset: 0x00025558
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
