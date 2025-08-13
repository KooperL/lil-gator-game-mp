using System;
using UnityEngine;

// Token: 0x0200039D RID: 925
public class UICameraOverlay : MonoBehaviour
{
	// Token: 0x06001194 RID: 4500 RVA: 0x00057DF4 File Offset: 0x00055FF4
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

	// Token: 0x040016AA RID: 5802
	public GameObject[] rightHandObjects;

	// Token: 0x040016AB RID: 5803
	public GameObject[] leftHandObjects;

	// Token: 0x040016AC RID: 5804
	public GameObject[] fullCameraModeObjects;
}
