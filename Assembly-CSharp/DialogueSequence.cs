using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000132 RID: 306
public class DialogueSequence : MonoBehaviour
{
	// Token: 0x060005B4 RID: 1460 RVA: 0x0002E894 File Offset: 0x0002CA94
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

	// Token: 0x060005B5 RID: 1461 RVA: 0x0000614F File Offset: 0x0000434F
	public virtual YieldInstruction Run()
	{
		return null;
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x0002E8D8 File Offset: 0x0002CAD8
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

	// Token: 0x040007BA RID: 1978
	public UnityEvent onStart;

	// Token: 0x040007BB RID: 1979
	public GameObject[] stateObjects;

	// Token: 0x040007BC RID: 1980
	public bool fade;

	// Token: 0x040007BD RID: 1981
	public bool ignoreDialogueDepth;
}
