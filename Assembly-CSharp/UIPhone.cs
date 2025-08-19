using System;
using System.Collections;
using UnityEngine;

public class UIPhone : MonoBehaviour
{
	// Token: 0x060012AC RID: 4780 RVA: 0x0000FBCA File Offset: 0x0000DDCA
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

	// Token: 0x060012AD RID: 4781 RVA: 0x0000FC00 File Offset: 0x0000DE00
	[ContextMenu("Activate")]
	public void Activate()
	{
		this.animator.SetBool(this.isRaisedID, true);
		this.rootGameObject.SetActive(true);
	}

	// Token: 0x060012AE RID: 4782 RVA: 0x0000FC20 File Offset: 0x0000DE20
	[ContextMenu("Deactivate")]
	public void Deactivate()
	{
		this.animator.SetBool(this.isRaisedID, false);
		base.StartCoroutine(this.DelayedDeactivate());
	}

	// Token: 0x060012AF RID: 4783 RVA: 0x0000FC41 File Offset: 0x0000DE41
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
