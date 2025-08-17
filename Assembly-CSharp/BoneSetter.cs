using System;
using UnityEngine;

public class BoneSetter : MonoBehaviour
{
	// Token: 0x0600026B RID: 619 RVA: 0x00020088 File Offset: 0x0001E288
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

	// Token: 0x0600026C RID: 620 RVA: 0x000200DC File Offset: 0x0001E2DC
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

	public Transform skeletonRoot;

	[SerializeField]
	private string[] boneNames;

	[SerializeField]
	private Transform[] bones;
}
