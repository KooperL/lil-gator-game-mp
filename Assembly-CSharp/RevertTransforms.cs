using System;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class RevertTransforms : MonoBehaviour
{
	// Token: 0x0600014D RID: 333 RVA: 0x0001BA68 File Offset: 0x00019C68
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

	// Token: 0x0600014E RID: 334 RVA: 0x00003226 File Offset: 0x00001426
	private void OnDisable()
	{
		this.Revert();
	}

	// Token: 0x0600014F RID: 335 RVA: 0x0001BAE8 File Offset: 0x00019CE8
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

	// Token: 0x04000204 RID: 516
	public RevertTransforms.InitialTransform[] initialTransforms;

	// Token: 0x02000065 RID: 101
	[Serializable]
	public struct InitialTransform
	{
		// Token: 0x04000205 RID: 517
		public Transform transform;

		// Token: 0x04000206 RID: 518
		public Vector3 position;

		// Token: 0x04000207 RID: 519
		public Quaternion rotation;
	}
}
