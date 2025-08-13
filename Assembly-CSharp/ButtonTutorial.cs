using System;
using UnityEngine;

// Token: 0x02000186 RID: 390
[CreateAssetMenu]
public class ButtonTutorial : ScriptableObject
{
	// Token: 0x1700006F RID: 111
	// (get) Token: 0x06000803 RID: 2051 RVA: 0x00026B32 File Offset: 0x00024D32
	public bool HasBeenPressed
	{
		get
		{
			return this.lastButtonPress > 0f;
		}
	}

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x06000804 RID: 2052 RVA: 0x00026B41 File Offset: 0x00024D41
	public bool HasBeenPressedRecently
	{
		get
		{
			return Time.time - this.lastButtonPress < 10f;
		}
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x00026B56 File Offset: 0x00024D56
	private void OnEnable()
	{
		this.lastButtonPress = -100f;
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x00026B63 File Offset: 0x00024D63
	public void Press()
	{
		this.lastButtonPress = Time.time;
	}

	// Token: 0x04000A32 RID: 2610
	public UIButtonObject buttonObject;

	// Token: 0x04000A33 RID: 2611
	public float lastButtonPress = -100f;

	// Token: 0x04000A34 RID: 2612
	private const float recentThreshold = 10f;
}
