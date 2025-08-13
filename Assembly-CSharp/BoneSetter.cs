using System;
using UnityEngine;

// Token: 0x020000A7 RID: 167
public class BoneSetter : MonoBehaviour
{
	// Token: 0x0600025E RID: 606 RVA: 0x0001F644 File Offset: 0x0001D844
	[ContextMenu("GetBoneNames()")]
	public void GetBoneNames()
	{
		SkinnedMeshRenderer component = base.GetComponent<SkinnedMeshRenderer>();
		if (component == null)
		{
			return;
		}
		Transform[] array = component.bones;
		this.boneNames = new string[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			this.boneNames[i] = array[i].name;
		}
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0001F698 File Offset: 0x0001D898
	[ContextMenu("SetBones()")]
	public void SetBones()
	{
		if (this.skeletonRoot == null)
		{
			Debug.LogError("Root object is not set!");
			return;
		}
		this.bones = new Transform[this.boneNames.Length];
		for (int i = 0; i < this.boneNames.Length; i++)
		{
			this.bones[i] = this.skeletonRoot.FindDeepChild(this.boneNames[i]);
		}
		SkinnedMeshRenderer component = base.GetComponent<SkinnedMeshRenderer>();
		component.bones = this.bones;
		component.rootBone = this.skeletonRoot;
	}

	// Token: 0x04000360 RID: 864
	public Transform skeletonRoot;

	// Token: 0x04000361 RID: 865
	[SerializeField]
	private string[] boneNames;

	// Token: 0x04000362 RID: 866
	[SerializeField]
	private Transform[] bones;
}
