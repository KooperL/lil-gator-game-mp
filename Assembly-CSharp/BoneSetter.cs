using System;
using UnityEngine;

public class BoneSetter : MonoBehaviour
{
	// Token: 0x06000226 RID: 550 RVA: 0x0000BD38 File Offset: 0x00009F38
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

	// Token: 0x06000227 RID: 551 RVA: 0x0000BD8C File Offset: 0x00009F8C
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
