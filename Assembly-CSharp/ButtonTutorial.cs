using System;
using UnityEngine;

[CreateAssetMenu]
public class ButtonTutorial : ScriptableObject
{
	// (get) Token: 0x06000803 RID: 2051 RVA: 0x00026B32 File Offset: 0x00024D32
	public bool HasBeenPressed
	{
		get
		{
			return this.lastButtonPress > 0f;
		}
	}

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

	public UIButtonObject buttonObject;

	public float lastButtonPress = -100f;

	private const float recentThreshold = 10f;
}
