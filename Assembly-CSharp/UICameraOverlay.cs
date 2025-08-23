using System;
using UnityEngine;

public class UICameraOverlay : MonoBehaviour
{
	// Token: 0x060011F5 RID: 4597 RVA: 0x0005A080 File Offset: 0x00058280
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

	public GameObject[] rightHandObjects;

	public GameObject[] leftHandObjects;

	public GameObject[] fullCameraModeObjects;
}
