using System;
using UnityEngine;

public class InteractDestroy : MonoBehaviour, Interaction
{
	// Token: 0x06000834 RID: 2100 RVA: 0x000049DF File Offset: 0x00002BDF
	public void Interact()
	{
		global::UnityEngine.Object.Destroy(base.gameObject);
	}
}
