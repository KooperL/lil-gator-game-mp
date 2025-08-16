using System;
using UnityEngine;

public class SettableState : MonoBehaviour
{
	// Token: 0x06000F44 RID: 3908 RVA: 0x00050250 File Offset: 0x0004E450
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

	// Token: 0x06000F45 RID: 3909 RVA: 0x00050288 File Offset: 0x0004E488
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

	public GameObject[] stateObjects;

	public SettableState[] children;

	public bool defaultState;

	private bool hasBeenSet;
}
