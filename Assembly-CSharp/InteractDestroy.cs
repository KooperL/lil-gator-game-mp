using System;
using UnityEngine;

public class InteractDestroy : MonoBehaviour, Interaction
{
	// Token: 0x060006B5 RID: 1717 RVA: 0x00022204 File Offset: 0x00020404
	public void Interact()
	{
		Object.Destroy(base.gameObject);
	}
}
