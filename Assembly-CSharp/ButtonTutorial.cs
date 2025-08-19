using System;
using UnityEngine;

[CreateAssetMenu]
public class ButtonTutorial : ScriptableObject
{
	// (get) Token: 0x060009B3 RID: 2483 RVA: 0x000095B5 File Offset: 0x000077B5
	public bool HasBeenPressed
	{
		get
		{
			return this.lastButtonPress > 0f;
		}
	}

	// (get) Token: 0x060009B4 RID: 2484 RVA: 0x000095C4 File Offset: 0x000077C4
	public bool HasBeenPressedRecently
	{
		get
		{
			return Time.time - this.lastButtonPress < 10f;
		}
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x000095D9 File Offset: 0x000077D9
	private void OnEnable()
	{
		this.lastButtonPress = -100f;
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x000095E6 File Offset: 0x000077E6
	public void Press()
	{
		this.lastButtonPress = Time.time;
	}

	public UIButtonObject buttonObject;

	public float lastButtonPress = -100f;

	private const float recentThreshold = 10f;
}
