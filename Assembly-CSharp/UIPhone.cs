using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003BD RID: 957
public class UIPhone : MonoBehaviour
{
	// Token: 0x0600124C RID: 4684 RVA: 0x0000F7CA File Offset: 0x0000D9CA
	private void OnValidate()
	{
		if (this.animator == null)
		{
			this.animator = base.GetComponent<Animator>();
		}
		if (this.rootGameObject == null)
		{
			this.rootGameObject = base.gameObject;
		}
	}

	// Token: 0x0600124D RID: 4685 RVA: 0x0000F800 File Offset: 0x0000DA00
	[ContextMenu("Activate")]
	public void Activate()
	{
		this.animator.SetBool(this.isRaisedID, true);
		this.rootGameObject.SetActive(true);
	}

	// Token: 0x0600124E RID: 4686 RVA: 0x0000F820 File Offset: 0x0000DA20
	[ContextMenu("Deactivate")]
	public void Deactivate()
	{
		this.animator.SetBool(this.isRaisedID, false);
		base.StartCoroutine(this.DelayedDeactivate());
	}

	// Token: 0x0600124F RID: 4687 RVA: 0x0000F841 File Offset: 0x0000DA41
	private IEnumerator DelayedDeactivate()
	{
		yield return new WaitForSeconds(this.deactivateDelay);
		this.rootGameObject.SetActive(false);
		yield break;
	}

	// Token: 0x040017AF RID: 6063
	public Animator animator;

	// Token: 0x040017B0 RID: 6064
	private int isRaisedID = Animator.StringToHash("IsRaised");

	// Token: 0x040017B1 RID: 6065
	public float deactivateDelay = 1f;

	// Token: 0x040017B2 RID: 6066
	public GameObject rootGameObject;
}
