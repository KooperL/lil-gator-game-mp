using System;
using System.IO;
using UnityEngine;

// Token: 0x020004D4 RID: 1236
public class MultiplayerConfigLoader
{
	// Token: 0x06001EE7 RID: 7911
	public static MultiplayerConfigLoader Load(string filename = "config.ini")
	{
		string path = Path.Combine(Directory.GetParent(Application.dataPath).FullName, filename);
		Debug.Log("[ConfigINI] Full config path: " + path);
		if (!File.Exists(path))
		{
			Debug.LogWarning("[ConfigINI] config.ini not found at: " + path);
			MultiplayerConfigLoader._instance = new MultiplayerConfigLoader();
			return MultiplayerConfigLoader._instance;
		}
		MultiplayerConfigLoader config = new MultiplayerConfigLoader();
		foreach (string line in File.ReadAllLines(path))
		{
			if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
			{
				string[] kv = line.Split(new char[] { '=' }, 2);
				if (kv.Length == 2)
				{
					string key = kv[0].Trim();
					string value = kv[1].Trim();
					string text = key.ToLowerInvariant();
					if (!(text == "session_key"))
					{
						if (!(text == "display_name"))
						{
							if (text == "server_host")
							{
								config.ServerHost = value;
							}
						}
						else
						{
							config.DisplayName = value;
						}
					}
					else
					{
						config.SessionKey = value;
					}
				}
			}
		}
		MultiplayerConfigLoader._instance = config;
		Debug.Log("[ConfigINI] Config loaded successfully.");
		return MultiplayerConfigLoader._instance;
	}

	// Token: 0x17000663 RID: 1635
	// (get) Token: 0x06001F35 RID: 7989
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

	// Token: 0x0400207E RID: 8318
	public string SessionKey;

	// Token: 0x0400207F RID: 8319
	public string DisplayName;

	// Token: 0x04002080 RID: 8320
	public string ServerHost;

	// Token: 0x04002081 RID: 8321
	private static MultiplayerConfigLoader _instance;
}
