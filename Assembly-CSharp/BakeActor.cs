using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000A3 RID: 163
[AddComponentMenu("Actors/Posed Actor")]
public class BakeActor : MonoBehaviour
{
	// Token: 0x0600024E RID: 590 RVA: 0x00002229 File Offset: 0x00000429
	private void OnValidate()
	{
	}

	// Token: 0x0600024F RID: 591 RVA: 0x0001F228 File Offset: 0x0001D428
	public void UpdateArmature()
	{
		if (this.armatureRoot == null)
		{
			return;
		}
		List<Transform> list = new List<Transform>(this.armatureRoot.GetComponentsInChildren<Transform>());
		List<Transform> list2 = new List<Transform>();
		foreach (Transform transform in list)
		{
			if (transform == this.armatureRoot || transform.gameObject.name.StartsWith("ShoulderConnector") || transform.gameObject.name == "Right Eye" || transform.gameObject.name == "Left Eye")
			{
				list2.Add(transform);
			}
		}
		foreach (Transform transform2 in list2)
		{
			list.Remove(transform2);
		}
		this.armature = list.ToArray();
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0001F33C File Offset: 0x0001D53C
	private void Start()
	{
		if (this.disableProximityTrigger && this.proximityTrigger != null)
		{
			this.proximityTrigger.SetActive(false);
		}
		if (this.bakeStatic)
		{
			this.headRotationMod = this.actor.headRotationMod;
			this.jawRotationMod = this.actor.jawRotationMod;
			if (this.bakedMeshRenderer == null)
			{
				this.GenerateBakedObject();
			}
			if (!this.isPreBaked)
			{
				this.BakeMesh();
			}
			if (!this.alwaysStatic)
			{
				this.actor.onEnable.AddListener(new UnityAction(this.ActorEnabled));
				this.actor.onEnterDialogue.AddListener(new UnityAction(this.ActorEnteredDialogue));
			}
			else
			{
				this.actor.lookAtAmount = 0f;
				this.actor.shoulderLookAmount = 0f;
				this.actor.headRotationMod = 0f;
				this.actor.jawRotationMod = 0f;
			}
		}
		if (this.alwaysStatic)
		{
			this.SetBakeState(true);
			base.enabled = false;
			return;
		}
		this.SetBakeState(false);
	}

	// Token: 0x06000251 RID: 593 RVA: 0x00003E9E File Offset: 0x0000209E
	private void ActorEnabled()
	{
		if (this.actor.lookAtAmount > 0f || this.actor.shoulderLookAmount > 0f)
		{
			base.enabled = true;
			this.SetBakeState(false);
		}
	}

	// Token: 0x06000252 RID: 594 RVA: 0x00003ED2 File Offset: 0x000020D2
	private void ActorEnteredDialogue()
	{
		base.enabled = true;
		this.SetBakeState(false);
	}

	// Token: 0x06000253 RID: 595 RVA: 0x00003EE2 File Offset: 0x000020E2
	private void Update()
	{
		if (!this.actor.enabled)
		{
			this.SetBakeState(true);
			base.enabled = false;
		}
	}

	// Token: 0x06000254 RID: 596 RVA: 0x00002229 File Offset: 0x00000429
	[ContextMenu("PreBake Mesh")]
	public void PreBakeMesh()
	{
	}

	// Token: 0x06000255 RID: 597 RVA: 0x0001F45C File Offset: 0x0001D65C
	private void GenerateBakedObject()
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "Baked Actor";
		gameObject.transform.parent = this.skinnedMeshRenderer.transform.parent;
		gameObject.transform.ApplyTransformLocal(this.skinnedMeshRenderer.transform);
		gameObject.layer = 18;
		this.bakedMeshRenderer = gameObject.AddComponent<MeshRenderer>();
		this.bakedMeshFilter = gameObject.AddComponent<MeshFilter>();
		this.bakedMeshRenderer.sharedMaterials = this.skinnedMeshRenderer.sharedMaterials;
		this.bakedMeshRenderer.shadowCastingMode = this.skinnedMeshRenderer.shadowCastingMode;
		this.bakedMeshRenderer.receiveShadows = this.skinnedMeshRenderer.receiveShadows;
		gameObject.SetActive(false);
	}

	// Token: 0x06000256 RID: 598 RVA: 0x00003EFF File Offset: 0x000020FF
	private void BakeMesh()
	{
		if (this.bakedMesh == null)
		{
			this.bakedMesh = new Mesh();
		}
		this.skinnedMeshRenderer.BakeMesh(this.bakedMesh, false);
		this.bakedMeshFilter.mesh = this.bakedMesh;
	}

	// Token: 0x06000257 RID: 599 RVA: 0x0001F518 File Offset: 0x0001D718
	private void SetBakeState(bool isBaked)
	{
		this.actor.jawRotationMod = (isBaked ? 0f : this.jawRotationMod);
		this.actor.headRotationMod = (isBaked ? 0f : this.headRotationMod);
		this.bakedMeshRenderer.gameObject.SetActive(isBaked);
		this.skinnedMeshRenderer.gameObject.SetActive(!isBaked);
		if (this.highlight != null)
		{
			this.highlight.highlightedRenderers[this.highlightIndex] = (isBaked ? this.bakedMeshRenderer : this.skinnedMeshRenderer);
			this.highlight.RenderersChanged();
		}
		if (this.wobbleBrain != null)
		{
			this.wobbleBrain.enabled = !isBaked;
		}
		if (this.fadeRenderer != null)
		{
			this.fadeRenderer.mainRenderer = (isBaked ? this.bakedMeshRenderer : this.skinnedMeshRenderer);
			this.fadeRenderer.UpdateFade();
		}
	}

	// Token: 0x04000347 RID: 839
	public bool bakeStatic = true;

	// Token: 0x04000348 RID: 840
	[ConditionalHide("bakeStatic", true)]
	public bool alwaysStatic;

	// Token: 0x04000349 RID: 841
	[Header("References")]
	public DialogueActor actor;

	// Token: 0x0400034A RID: 842
	public Animator animator;

	// Token: 0x0400034B RID: 843
	public Transform armatureRoot;

	// Token: 0x0400034C RID: 844
	[ReadOnly]
	public Transform[] armature;

	// Token: 0x0400034D RID: 845
	public SkinnedMeshRenderer skinnedMeshRenderer;

	// Token: 0x0400034E RID: 846
	public InteractionHighlightGeneric highlight;

	// Token: 0x0400034F RID: 847
	public int highlightIndex = -1;

	// Token: 0x04000350 RID: 848
	public WobbleBrain wobbleBrain;

	// Token: 0x04000351 RID: 849
	public FadeRenderer fadeRenderer;

	// Token: 0x04000352 RID: 850
	public GameObject proximityTrigger;

	// Token: 0x04000353 RID: 851
	public bool disableProximityTrigger = true;

	// Token: 0x04000354 RID: 852
	[ReadOnly]
	public bool isPreBaked;

	// Token: 0x04000355 RID: 853
	[ReadOnly]
	public Mesh bakedMesh;

	// Token: 0x04000356 RID: 854
	[ReadOnly]
	public MeshRenderer bakedMeshRenderer;

	// Token: 0x04000357 RID: 855
	[ReadOnly]
	public MeshFilter bakedMeshFilter;

	// Token: 0x04000358 RID: 856
	private float headRotationMod;

	// Token: 0x04000359 RID: 857
	private float jawRotationMod;
}
