using System;
using System.Collections;
using UnityEngine;

public class UIPhone : MonoBehaviour
{
	// Token: 0x060012AC RID: 4780 RVA: 0x0000FBAB File Offset: 0x0000DDAB
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

	// Token: 0x060012AD RID: 4781 RVA: 0x0000FBE1 File Offset: 0x0000DDE1
	[ContextMenu("Activate")]
	public void Activate()
	{
		this.animator.SetBool(this.isRaisedID, true);
		this.rootGameObject.SetActive(true);
	}

	// Token: 0x060012AE RID: 4782 RVA: 0x0000FC01 File Offset: 0x0000DE01
	[ContextMenu("Deactivate")]
	public void Deactivate()
	{
		this.animator.SetBool(this.isRaisedID, false);
		base.StartCoroutine(this.DelayedDeactivate());
	}

	// Token: 0x060012AF RID: 4783 RVA: 0x0000FC22 File Offset: 0x0000DE22
	private IEnumerator DelayedDeactivate()
	{
		yield return new WaitForSeconds(this.deactivateDelay);
		this.rootGameObject.SetActive(false);
		yield break;
	}

	public Animator animator;

	private int isRaisedID = Animator.StringToHash("IsRaised");

	public float deactivateDelay = 1f;

	public GameObject rootGameObject;
}
