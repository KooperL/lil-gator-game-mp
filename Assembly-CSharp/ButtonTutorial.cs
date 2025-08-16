using System;
using UnityEngine;

[CreateAssetMenu]
public class ButtonTutorial : ScriptableObject
{
	// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00009596 File Offset: 0x00007796
	public bool HasBeenPressed
	{
		get
		{
			return this.lastButtonPress > 0f;
		}
	}

	// (get) Token: 0x060009B4 RID: 2484 RVA: 0x000095A5 File Offset: 0x000077A5
	public bool HasBeenPressedRecently
	{
		get
		{
			return Time.time - this.lastButtonPress < 10f;
		}
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x000095BA File Offset: 0x000077BA
	private void OnEnable()
	{
		this.lastButtonPress = -100f;
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x000095C7 File Offset: 0x000077C7
	public void Press()
	{
		this.lastButtonPress = Time.time;
	}

	public UIButtonObject buttonObject;

	public float lastButtonPress = -100f;

	private const float recentThreshold = 10f;
}
