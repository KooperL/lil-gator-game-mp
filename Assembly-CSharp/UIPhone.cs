using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002D4 RID: 724
public class UIPhone : MonoBehaviour
{
	// Token: 0x06000F4A RID: 3914 RVA: 0x000499D6 File Offset: 0x00047BD6
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

	// Token: 0x06000F4B RID: 3915 RVA: 0x00049A0C File Offset: 0x00047C0C
	[ContextMenu("Activate")]
	public void Activate()
	{
		this.animator.SetBool(this.isRaisedID, true);
		this.rootGameObject.SetActive(true);
	}

	// Token: 0x06000F4C RID: 3916 RVA: 0x00049A2C File Offset: 0x00047C2C
	[ContextMenu("Deactivate")]
	public void Deactivate()
	{
		this.animator.SetBool(this.isRaisedID, false);
		base.StartCoroutine(this.DelayedDeactivate());
	}

	// Token: 0x06000F4D RID: 3917 RVA: 0x00049A4D File Offset: 0x00047C4D
	private IEnumerator DelayedDeactivate()
	{
		yield return new WaitForSeconds(this.deactivateDelay);
		this.rootGameObject.SetActive(false);
		yield break;
	}

	// Token: 0x0400141D RID: 5149
	public Animator animator;

	// Token: 0x0400141E RID: 5150
	private int isRaisedID = Animator.StringToHash("IsRaised");

	// Token: 0x0400141F RID: 5151
	public float deactivateDelay = 1f;

	// Token: 0x04001420 RID: 5152
	public GameObject rootGameObject;
}
