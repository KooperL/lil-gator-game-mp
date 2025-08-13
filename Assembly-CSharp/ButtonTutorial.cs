using System;
using UnityEngine;

// Token: 0x020001FC RID: 508
[CreateAssetMenu]
public class ButtonTutorial : ScriptableObject
{
	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x0600096D RID: 2413 RVA: 0x0000924D File Offset: 0x0000744D
	public bool HasBeenPressed
	{
		get
		{
			return this.lastButtonPress > 0f;
		}
	}

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x0600096E RID: 2414 RVA: 0x0000925C File Offset: 0x0000745C
	public bool HasBeenPressedRecently
	{
		get
		{
			return Time.time - this.lastButtonPress < 10f;
		}
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x00009271 File Offset: 0x00007471
	private void OnEnable()
	{
		this.lastButtonPress = -100f;
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x0000927E File Offset: 0x0000747E
	public void Press()
	{
		this.lastButtonPress = Time.time;
	}

	// Token: 0x04000C1B RID: 3099
	public UIButtonObject buttonObject;

	// Token: 0x04000C1C RID: 3100
	public float lastButtonPress = -100f;

	// Token: 0x04000C1D RID: 3101
	private const float recentThreshold = 10f;
}
