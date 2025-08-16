using System;
using System.IO;
using UnityEngine;

public class MultiplayerConfigLoader
{
	// Token: 0x06001E7D RID: 7805 RVA: 0x00077D18 File Offset: 0x00075F18
	public static MultiplayerConfigLoader Load(string filename = "config.ini")
	{
		string text = Path.Combine(Directory.GetParent(Application.dataPath).FullName, filename);
		Debug.Log("[ConfigINI] Full config path: " + text);
		if (!File.Exists(text))
		{
			Debug.LogWarning("[ConfigINI] config.ini not found at: " + text);
			MultiplayerConfigLoader._instance = new MultiplayerConfigLoader();
			return MultiplayerConfigLoader._instance;
		}
		MultiplayerConfigLoader multiplayerConfigLoader = new MultiplayerConfigLoader();
		foreach (string text2 in File.ReadAllLines(text))
		{
			if (!string.IsNullOrWhiteSpace(text2) && !text2.StartsWith("#"))
			{
				string[] array2 = text2.Split(new char[] { '=' }, 2);
				if (array2.Length == 2)
				{
					string text3 = array2[0].Trim();
					string text4 = array2[1].Trim();
					string text5 = text3.ToLowerInvariant();
					if (!(text5 == "session_key"))
					{
						if (!(text5 == "display_name"))
						{
							if (text5 == "server_host")
							{
								multiplayerConfigLoader.ServerHost = text4;
							}
						}
						else
						{
							multiplayerConfigLoader.DisplayName = text4;
						}
					}
					else
					{
						multiplayerConfigLoader.SessionKey = text4;
					}
				}
			}
		}
		MultiplayerConfigLoader._instance = multiplayerConfigLoader;
		Debug.Log("[ConfigINI] Config loaded successfully.");
		return MultiplayerConfigLoader._instance;
	}

	// (get) Token: 0x06001E7F RID: 7807 RVA: 0x000174D9 File Offset: 0x000156D9
	public static MultiplayerConfigLoader Instance
	{
		get
		{
			if (MultiplayerConfigLoader._instance == null)
			{
				Debug.LogWarning("[ConfigINI] Instance requested before Load()!");
				MultiplayerConfigLoader._instance = new MultiplayerConfigLoader();
			}
			return MultiplayerConfigLoader._instance;
		}
	}

	public string SessionKey;

	public string DisplayName;

	public string ServerHost;

	private static MultiplayerConfigLoader _instance;
}
