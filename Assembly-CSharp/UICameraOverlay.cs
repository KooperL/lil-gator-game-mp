using System;
using UnityEngine;

// Token: 0x020002BB RID: 699
public class UICameraOverlay : MonoBehaviour
{
	// Token: 0x06000EBC RID: 3772 RVA: 0x00046844 File Offset: 0x00044A44
	public void SetState(ItemCamera.CameraMode cameraMode, bool isRightHand)
	{
		if (base.gameObject == null)
		{
			return;
		}
		GameObject[] array = this.fullCameraModeObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(cameraMode == ItemCamera.CameraMode.Forward || cameraMode == ItemCamera.CameraMode.Backward);
		}
		array = this.leftHandObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(!isRightHand);
		}
		array = this.rightHandObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(isRightHand);
		}
		base.gameObject.SetActive(cameraMode != ItemCamera.CameraMode.Off);
	}

	// Token: 0x04001334 RID: 4916
	public GameObject[] rightHandObjects;

	// Token: 0x04001335 RID: 4917
	public GameObject[] leftHandObjects;

	// Token: 0x04001336 RID: 4918
	public GameObject[] fullCameraModeObjects;
}
