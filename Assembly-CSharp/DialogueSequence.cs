using System;
using UnityEngine;
using UnityEngine.Events;

public class DialogueSequence : MonoBehaviour
{
	// Token: 0x060004B1 RID: 1201 RVA: 0x00019DAC File Offset: 0x00017FAC
	public virtual void Activate()
	{
		this.onStart.Invoke();
		GameObject[] array = this.stateObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(true);
		}
		if (this.ignoreDialogueDepth)
		{
			Game.ignoreDialogueDepth = true;
		}
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x00019DF0 File Offset: 0x00017FF0
	public virtual YieldInstruction Run()
	{
		return null;
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x00019DF4 File Offset: 0x00017FF4
	public virtual void Deactivate()
	{
		GameObject[] array = this.stateObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(false);
		}
		if (this.ignoreDialogueDepth)
		{
			Game.ignoreDialogueDepth = false;
		}
	}

	public UnityEvent onStart;

	public GameObject[] stateObjects;

	public bool fade;

	public bool ignoreDialogueDepth;
}
