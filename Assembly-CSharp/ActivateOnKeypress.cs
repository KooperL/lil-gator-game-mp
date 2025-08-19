using System;
using Cinemachine;
using UnityEngine;

public class ActivateOnKeypress : MonoBehaviour
{
	// Token: 0x0600015B RID: 347 RVA: 0x000032D1 File Offset: 0x000014D1
	private void Start()
	{
		this.vcam = base.GetComponent<CinemachineVirtualCameraBase>();
	}

	// Token: 0x0600015C RID: 348 RVA: 0x0001C360 File Offset: 0x0001A560
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
