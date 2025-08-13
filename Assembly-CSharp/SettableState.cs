using System;
using UnityEngine;

// Token: 0x02000237 RID: 567
public class SettableState : MonoBehaviour
{
	// Token: 0x06000C49 RID: 3145 RVA: 0x0003B13C File Offset: 0x0003933C
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

	// Token: 0x06000C4A RID: 3146 RVA: 0x0003B174 File Offset: 0x00039374
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

	// Token: 0x04001009 RID: 4105
	public GameObject[] stateObjects;

	// Token: 0x0400100A RID: 4106
	public SettableState[] children;

	// Token: 0x0400100B RID: 4107
	public bool defaultState;

	// Token: 0x0400100C RID: 4108
	private bool hasBeenSet;
}
