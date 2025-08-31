using System;
using Cinemachine;
using UnityEngine;

public class ActivateOnKeypress : MonoBehaviour
{
	// Token: 0x0600012E RID: 302 RVA: 0x00007597 File Offset: 0x00005797
	private void Start()
	{
		this.vcam = base.GetComponent<CinemachineVirtualCameraBase>();
	}

	// Token: 0x0600012F RID: 303 RVA: 0x000075A8 File Offset: 0x000057A8
	private void Update()
	{
		if (this.vcam != null)
		{
			if (Input.GetKey(this.ActivationKey))
			{
				if (!this.boosted)
				{
					this.vcam.Priority += this.PriorityBoostAmount;
					this.boosted = true;
				}
			}
			else if (this.boosted)
			{
				this.vcam.Priority -= this.PriorityBoostAmount;
				this.boosted = false;
			}
		}
		if (this.Reticle != null)
		{
			this.Reticle.SetActive(this.boosted);
		}
	}

	public KeyCode ActivationKey = KeyCode.LeftControl;

	public int PriorityBoostAmount = 10;

	public GameObject Reticle;

	private CinemachineVirtualCameraBase vcam;

	private bool boosted;
}
