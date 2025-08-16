using System;
using UnityEngine;
using UnityEngine.UI;

public class DebugMessages : MonoBehaviour
{
	// Token: 0x06000353 RID: 851 RVA: 0x0000498E File Offset: 0x00002B8E
	public static void DisplayMessage(string message)
	{
		if (DebugMessages.d == null)
		{
			return;
		}
		DebugMessages.d.Display(message);
	}

	// Token: 0x06000354 RID: 852 RVA: 0x000049A9 File Offset: 0x00002BA9
	private void Awake()
	{
		DebugMessages.d = this;
	}

	// Token: 0x06000355 RID: 853 RVA: 0x00002229 File Offset: 0x00000429
	public void Display(string message)
	{
	}

	// Token: 0x06000356 RID: 854 RVA: 0x000049B1 File Offset: 0x00002BB1
	private void Update()
	{
		if (Time.time > this.disableTime)
		{
			base.gameObject.SetActive(false);
		}
	}

	public static DebugMessages d;

	public Text text;

	private float disableTime = -10f;
}
