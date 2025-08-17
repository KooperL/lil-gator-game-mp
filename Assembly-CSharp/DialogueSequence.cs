using System;
using UnityEngine;
using UnityEngine.Events;

public class DialogueSequence : MonoBehaviour
{
	// Token: 0x060005EE RID: 1518 RVA: 0x0002FF90 File Offset: 0x0002E190
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

	// Token: 0x060005EF RID: 1519 RVA: 0x00006415 File Offset: 0x00004615
	public virtual YieldInstruction Run()
	{
		return null;
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x0002FFD4 File Offset: 0x0002E1D4
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
