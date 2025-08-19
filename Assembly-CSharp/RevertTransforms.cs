using System;
using UnityEngine;

public class RevertTransforms : MonoBehaviour
{
	// Token: 0x06000155 RID: 341 RVA: 0x0001C258 File Offset: 0x0001A458
	[ContextMenu("Get Initial Transform")]
	public void GetInitialTransforms()
	{
		Transform[] componentsInChildren = base.transform.GetComponentsInChildren<Transform>();
		this.initialTransforms = new RevertTransforms.InitialTransform[componentsInChildren.Length];
		for (int i = 0; i < this.initialTransforms.Length; i++)
		{
			this.initialTransforms[i].transform = componentsInChildren[i];
			this.initialTransforms[i].position = componentsInChildren[i].localPosition;
			this.initialTransforms[i].rotation = componentsInChildren[i].localRotation;
		}
	}

	// Token: 0x06000156 RID: 342 RVA: 0x000032C9 File Offset: 0x000014C9
	private void OnDisable()
	{
		this.Revert();
	}

	// Token: 0x06000157 RID: 343 RVA: 0x0001C2D8 File Offset: 0x0001A4D8
	[ContextMenu("Revert Transforms")]
	public void Revert()
	{
		for (int i = 0; i < this.initialTransforms.Length; i++)
		{
			if (this.initialTransforms[i].transform != null)
			{
				this.initialTransforms[i].transform.localPosition = this.initialTransforms[i].position;
				this.initialTransforms[i].transform.localRotation = this.initialTransforms[i].rotation;
			}
		}
	}

	public RevertTransforms.InitialTransform[] initialTransforms;

	[Serializable]
	public struct InitialTransform
	{
		public Transform transform;

		public Vector3 position;

		public Quaternion rotation;
	}
}
