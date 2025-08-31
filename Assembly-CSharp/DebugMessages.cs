using System;
using UnityEngine;
using UnityEngine.UI;

public class DebugMessages : MonoBehaviour
{
	// Token: 0x060002EA RID: 746 RVA: 0x0001152C File Offset: 0x0000F72C
	public static void DisplayMessage(string message)
	{
		if (DebugMessages.d == null)
		{
			return;
		}
		DebugMessages.d.Display(message);
	}

	// Token: 0x060002EB RID: 747 RVA: 0x00011547 File Offset: 0x0000F747
	private void Awake()
	{
		DebugMessages.d = this;
	}

	// Token: 0x060002EC RID: 748 RVA: 0x0001154F File Offset: 0x0000F74F
	public void Display(string message)
	{
	}

	// Token: 0x060002ED RID: 749 RVA: 0x00011551 File Offset: 0x0000F751
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
