using System;
using UnityEngine;

// Token: 0x020002F6 RID: 758
public class SettableState : MonoBehaviour
{
	// Token: 0x06000EE8 RID: 3816 RVA: 0x0004E594 File Offset: 0x0004C794
	private void Start()
	{
		if (!this.hasBeenSet)
		{
			GameObject[] array = this.stateObjects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(this.defaultState);
			}
		}
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x0004E5CC File Offset: 0x0004C7CC
	public void SetState(bool active)
	{
		GameObject[] array = this.stateObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(active);
		}
		SettableState[] array2 = this.children;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].SetState(active);
		}
		this.hasBeenSet = true;
	}

	// Token: 0x0400130A RID: 4874
	public GameObject[] stateObjects;

	// Token: 0x0400130B RID: 4875
	public SettableState[] children;

	// Token: 0x0400130C RID: 4876
	public bool defaultState;

	// Token: 0x0400130D RID: 4877
	private bool hasBeenSet;
}
