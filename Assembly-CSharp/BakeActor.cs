using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Actors/Posed Actor")]
public class BakeActor : MonoBehaviour
{
	// Token: 0x06000216 RID: 534 RVA: 0x0000B7F9 File Offset: 0x000099F9
	private void OnValidate()
	{
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0000B7FC File Offset: 0x000099FC
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

	// Token: 0x06000218 RID: 536 RVA: 0x0000B910 File Offset: 0x00009B10
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

	// Token: 0x06000219 RID: 537 RVA: 0x0000BA2E File Offset: 0x00009C2E
	private void ActorEnabled()
	{
		if (this.actor.lookAtAmount > 0f || this.actor.shoulderLookAmount > 0f)
		{
			base.enabled = true;
			this.SetBakeState(false);
		}
	}

	// Token: 0x0600021A RID: 538 RVA: 0x0000BA62 File Offset: 0x00009C62
	private void ActorEnteredDialogue()
	{
		base.enabled = true;
		this.SetBakeState(false);
	}

	// Token: 0x0600021B RID: 539 RVA: 0x0000BA72 File Offset: 0x00009C72
	private void Update()
	{
		if (!this.actor.enabled)
		{
			this.SetBakeState(true);
			base.enabled = false;
		}
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0000BA8F File Offset: 0x00009C8F
	[ContextMenu("PreBake Mesh")]
	public void PreBakeMesh()
	{
	}

	// Token: 0x0600021D RID: 541 RVA: 0x0000BA94 File Offset: 0x00009C94
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

	// Token: 0x0600021E RID: 542 RVA: 0x0000BB4D File Offset: 0x00009D4D
	private void BakeMesh()
	{
		if (this.bakedMesh == null)
		{
			this.bakedMesh = new Mesh();
		}
		this.skinnedMeshRenderer.BakeMesh(this.bakedMesh, false);
		this.bakedMeshFilter.mesh = this.bakedMesh;
	}

	// Token: 0x0600021F RID: 543 RVA: 0x0000BB8C File Offset: 0x00009D8C
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

	public bool bakeStatic = true;

	[ConditionalHide("bakeStatic", true)]
	public bool alwaysStatic;

	[Header("References")]
	public DialogueActor actor;

	public Animator animator;

	public Transform armatureRoot;

	[ReadOnly]
	public Transform[] armature;

	public SkinnedMeshRenderer skinnedMeshRenderer;

	public InteractionHighlightGeneric highlight;

	public int highlightIndex = -1;

	public WobbleBrain wobbleBrain;

	public FadeRenderer fadeRenderer;

	public GameObject proximityTrigger;

	public bool disableProximityTrigger = true;

	[ReadOnly]
	public bool isPreBaked;

	[ReadOnly]
	public Mesh bakedMesh;

	[ReadOnly]
	public MeshRenderer bakedMeshRenderer;

	[ReadOnly]
	public MeshFilter bakedMeshFilter;

	private float headRotationMod;

	private float jawRotationMod;
}
