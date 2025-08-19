using System;
using System.IO;
using UnityEngine;

public class MultiplayerConfigLoader
{
	public static MultiplayerConfigLoader Load(string filename = "lggmp_config.ini")
	{
		string text = Path.Combine(Directory.GetParent(Application.dataPath).FullName, filename);
		Debug.Log("[LGG-MP] Full config path: " + text);
		if (!File.Exists(text))
		{
			Debug.LogWarning("[LGG-MP] lggmp_config.ini not found at: " + text);
			MultiplayerConfigLoader._instance = new MultiplayerConfigLoader();
			return MultiplayerConfigLoader._instance;
		}
		MultiplayerConfigLoader multiplayerConfigLoader = new MultiplayerConfigLoader();
		multiplayerConfigLoader.ConfigFileFound = true;
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
								multiplayerConfigLoader.ServerHostPresent = true;
							}
						}
						else
						{
							multiplayerConfigLoader.DisplayName = text4;
							multiplayerConfigLoader.DisplayNamePresent = true;
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
		Debug.Log("[LGG-MP] Config loaded successfully.");
		return MultiplayerConfigLoader._instance;
	}

	// (get) Token: 0x06001E72 RID: 7794
	public static MultiplayerConfigLoader Instance
	{
		get
		{
			if (MultiplayerConfigLoader._instance == null)
			{
				Debug.LogWarning("[LGG-MP] Instance requested before Load()!");
				MultiplayerConfigLoader._instance = new MultiplayerConfigLoader();
			}
			return MultiplayerConfigLoader._instance;
		}
	}

	public string SessionKey;

	public string DisplayName;

	public string ServerHost;

	private static MultiplayerConfigLoader _instance;

	public bool DisplayNamePresent;

	public bool ServerHostPresent;

	public bool ConfigFileFound;

	public string logTemp = "";
}
