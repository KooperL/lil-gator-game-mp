using System;
using System.IO;
using UnityEngine;

// Token: 0x020004D4 RID: 1236
public class MultiplayerConfigLoader
{
	// Token: 0x17000647 RID: 1607
	// (get) Token: 0x06001EE1 RID: 7905
	// (set) Token: 0x06001EE2 RID: 7906
	public string SessionKey { get; private set; } = "";

	// Token: 0x17000648 RID: 1608
	// (get) Token: 0x06001EE3 RID: 7907
	// (set) Token: 0x06001EE4 RID: 7908
	public string DisplayName { get; private set; } = "";

	// Token: 0x17000649 RID: 1609
	// (get) Token: 0x06001EE5 RID: 7909
	// (set) Token: 0x06001EE6 RID: 7910
	public string ServerHost { get; private set; } = "";

	// Token: 0x06001EE7 RID: 7911
	public static MultiplayerConfigLoader Load(string filename = "config.ini")
	{
		string path = Path.Combine(Directory.GetParent(Application.dataPath).FullName, filename);
		Debug.Log("[ConfigINI] Full config path: " + path);
		if (!File.Exists(path))
		{
			Debug.LogWarning("[ConfigINI] config.ini not found at: " + path);
			return new MultiplayerConfigLoader();
		}
		MultiplayerConfigLoader config = new MultiplayerConfigLoader();
		string[] array = File.ReadAllLines(path);
		for (int i = 0; i < array.Length; i++)
		{
			string line = array[i].Trim();
			if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#") && !line.StartsWith(";"))
			{
				string[] parts = line.Split(new char[] { '=' }, 2);
				if (parts.Length == 2)
				{
					string key = parts[0].Trim().ToLower();
					string value = parts[1].Trim();
					if (!(key == "session_key"))
					{
						if (!(key == "display_name"))
						{
							if (key == "server_host")
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
		Debug.Log("[ConfigINI] Loaded config successfully.");
		return config;
	}
}
