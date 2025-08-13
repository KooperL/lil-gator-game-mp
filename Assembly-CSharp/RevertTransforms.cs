using System;
using UnityEngine;

// Token: 0x0200004C RID: 76
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

	// Token: 0x040001A3 RID: 419
	public RevertTransforms.InitialTransform[] initialTransforms;

	// Token: 0x02000367 RID: 871
	[Serializable]
	public struct InitialTransform
	{
		// Token: 0x04001A33 RID: 6707
		public Transform transform;

		// Token: 0x04001A34 RID: 6708
		public Vector3 position;

		// Token: 0x04001A35 RID: 6709
		public Quaternion rotation;
	}
}
