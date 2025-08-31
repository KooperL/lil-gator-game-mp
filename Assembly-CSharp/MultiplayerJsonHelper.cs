using System;
using UnityEngine;

public static class MultiplayerJsonHelper
{
	public static T[] FromJson<T>(string json)
	{
		return JsonUtility.FromJson<MultiplayerJsonHelper.Wrapper<T>>(json).playerStates;
	}

	public static string ToJson<T>(T[] array)
	{
		return JsonUtility.ToJson(new MultiplayerJsonHelper.Wrapper<T>
		{
			playerStates = array
		});
	}

	public static string ToJson<T>(T[] array, bool prettyPrint)
	{
		return JsonUtility.ToJson(new MultiplayerJsonHelper.Wrapper<T>
		{
			playerStates = array
		}, prettyPrint);
	}

	[Serializable]
	private class Wrapper<T>
	{
		public T[] playerStates;
	}
}
