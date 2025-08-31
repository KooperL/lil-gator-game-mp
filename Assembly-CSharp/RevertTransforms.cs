using System;
using UnityEngine;

public class RevertTransforms : MonoBehaviour
{
	// Token: 0x06000128 RID: 296 RVA: 0x00007470 File Offset: 0x00005670
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

	// Token: 0x06000129 RID: 297 RVA: 0x000074F0 File Offset: 0x000056F0
	private void OnDisable()
	{
		this.Revert();
	}

	// Token: 0x0600012A RID: 298 RVA: 0x000074F8 File Offset: 0x000056F8
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
