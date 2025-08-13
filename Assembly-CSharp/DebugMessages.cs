using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C3 RID: 195
public class DebugMessages : MonoBehaviour
{
	// Token: 0x0600032D RID: 813 RVA: 0x000047AA File Offset: 0x000029AA
	public static void DisplayMessage(string message)
	{
		if (DebugMessages.d == null)
		{
			return;
		}
		DebugMessages.d.Display(message);
	}

	// Token: 0x0600032E RID: 814 RVA: 0x000047C5 File Offset: 0x000029C5
	private void Awake()
	{
		DebugMessages.d = this;
	}

	// Token: 0x0600032F RID: 815 RVA: 0x00002229 File Offset: 0x00000429
	public void Display(string message)
	{
	}

	// Token: 0x06000330 RID: 816 RVA: 0x000047CD File Offset: 0x000029CD
	private void Update()
	{
		if (Time.time > this.disableTime)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x040004A1 RID: 1185
	public static DebugMessages d;

	// Token: 0x040004A2 RID: 1186
	public Text text;

	// Token: 0x040004A3 RID: 1187
	private float disableTime = -10f;
}
