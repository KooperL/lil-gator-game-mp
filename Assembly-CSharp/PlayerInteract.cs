using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000271 RID: 625
public class PlayerInteract : MonoBehaviour
{
	// Token: 0x1700011A RID: 282
	// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0000B352 File Offset: 0x00009552
	public static GameObject CurrentTarget
	{
		get
		{
			return PlayerInteract.p.interactionTarget.gameObject;
		}
	}

	// Token: 0x06000BF7 RID: 3063 RVA: 0x0000B363 File Offset: 0x00009563
	public static void CurrentHighlightChanged()
	{
		PlayerInteract.p.UpdateInteractionRenderers();
	}

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x0000B36F File Offset: 0x0000956F
	public bool InteractionAvailable
	{
		get
		{
			return this.interactionTarget != null;
		}
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x0000B37D File Offset: 0x0000957D
	private void OnValidate()
	{
		if (this.playerOrbitCamera == null)
		{
			this.playerOrbitCamera = Object.FindObjectOfType<PlayerOrbitCamera>();
		}
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x0000B398 File Offset: 0x00009598
	private void Awake()
	{
		PlayerInteract.p = this;
		if (this.dialogueActor == null)
		{
			this.dialogueActor = base.transform.parent.GetComponent<DialogueActor>();
		}
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x0000B3C4 File Offset: 0x000095C4
	private void OnDisable()
	{
		if (this == null)
		{
			return;
		}
		this.targetList = new List<Collider>();
		this.interactionTarget = null;
		this.UpdateInteraction();
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x0000B3E8 File Offset: 0x000095E8
	private void Start()
	{
		this.highlightsFX = HighlightsFX.h;
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x0000B3F5 File Offset: 0x000095F5
	private void OnTriggerEnter(Collider other)
	{
		this.AddInteraction(other);
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x0000B3F5 File Offset: 0x000095F5
	private void OnTriggerStay(Collider other)
	{
		this.AddInteraction(other);
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x0000B3FE File Offset: 0x000095FE
	private void OnTriggerExit(Collider other)
	{
		this.RemoveInteraction(other);
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x0000B407 File Offset: 0x00009607
	private void AddInteraction(Collider target)
	{
		base.enabled = true;
		if (!this.targetList.Contains(target))
		{
			this.targetList.Add(target);
		}
		this.UpdateInteraction();
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x0000B430 File Offset: 0x00009630
	private void RemoveInteraction(Collider target)
	{
		this.targetList.Remove(target);
		if (this.interactionTarget == target && this.interactionHighlight != null)
		{
			this.ClearInteractionFocus();
		}
		this.UpdateInteraction();
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x00042140 File Offset: 0x00040340
	private void CleanTargets()
	{
		for (int i = 0; i < this.targetList.Count; i++)
		{
			if (this.targetList[i] == null || !this.targetList[i].gameObject.activeInHierarchy || !this.targetList[i].enabled)
			{
				if (this.interactionTarget == this.targetList[i] && this.interactionHighlight != null)
				{
					this.ClearInteractionFocus();
				}
				this.targetList.Remove(this.targetList[i]);
				i--;
			}
		}
	}

	// Token: 0x06000C03 RID: 3075 RVA: 0x000421EC File Offset: 0x000403EC
	private void UpdateInteraction()
	{
		if (PlayerInteract.interactButtonPriority != null && !PlayerInteract.interactButtonPriority.activeSelf)
		{
			PlayerInteract.interactButtonPriority = null;
		}
		if (Game.HasControl && !DialogueManager.d.IsInImportantDialogue && (Player.movement.IsGrounded || Player.movement.IsSubmerged) && PlayerInteract.interactButtonPriority == null && !Player.movement.modNoInteractions && PlayerOrbitCamera.active.cameraMode == PlayerOrbitCamera.CameraMode.Off)
		{
			this.CleanTargets();
			if (this.targetList.Count > 0)
			{
				Collider collider = null;
				Renderer[] array = null;
				float num = 9999f;
				for (int i = 0; i < this.targetList.Count; i++)
				{
					float priority = this.GetPriority(this.targetList[i].transform.position);
					Renderer[] array2;
					if (priority < num && this.IsInteractionValid(this.targetList[i], out array2))
					{
						num = priority;
						array = array2;
						collider = this.targetList[i];
					}
				}
				this.interactionTime = Time.fixedTime;
				if (collider != null && collider != this.interactionTarget)
				{
					this.interactionTarget = null;
					if (this.possibleTarget == collider)
					{
						this.sameTargetFrameCount++;
						if (this.sameTargetFrameCount > 4)
						{
							this.SetInteractionFocus(collider, array);
						}
					}
					else
					{
						this.sameTargetFrameCount = 0;
						this.possibleTarget = collider;
					}
				}
			}
		}
		else
		{
			this.interactionTarget = null;
		}
		if (this.promptFollow != null)
		{
			if (this.interactionTarget != null)
			{
				this.promptFollow.followTarget = this.interactionTarget.transform;
			}
			else
			{
				this.promptFollow.followTarget = null;
			}
		}
		if (this.interactionPrompt != null)
		{
			this.interactionPrompt.SetActive(this.interactionTarget != null && this.hadInteractionTarget);
		}
		if (this.interactionTarget == null)
		{
			this.ClearInteractionFocus();
		}
		this.hadInteractionTarget = this.interactionTarget != null;
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x00042404 File Offset: 0x00040604
	private float GetPriority(Vector3 position)
	{
		Vector3 vector = base.transform.InverseTransformPoint(position);
		float num = this.distancePriorityWeight * vector.magnitude;
		float num2 = this.playerPriorityWeight * Mathf.Abs(vector.x);
		float num3 = 0f;
		if (!this.playerOrbitCamera.IsAutoCameraActive)
		{
			Vector3 vector2 = position - base.transform.position;
			num3 = this.cameraPriorityWeight * Vector3.Angle(vector2.Flat(), MainCamera.t.forward.Flat());
		}
		return num + num2 + num3;
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x0004248C File Offset: 0x0004068C
	public void Interact()
	{
		if (this.interactionTarget == null)
		{
			Debug.LogError("No interaction target object");
			return;
		}
		Interaction component = this.interactionTarget.GetComponent<Interaction>();
		if (component == null)
		{
			Debug.LogError("No interaction target component");
		}
		else
		{
			component.Interact();
		}
		this.RemoveInteraction(this.interactionTarget);
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x000424E0 File Offset: 0x000406E0
	private bool IsInteractionValid(Collider target, out Renderer[] highlightRenderer)
	{
		InteractionHighlight component = target.GetComponent<InteractionHighlight>();
		highlightRenderer = null;
		if (component == null)
		{
			return false;
		}
		highlightRenderer = component.GetHighlightedRenderer();
		if (highlightRenderer == null)
		{
			return false;
		}
		PlayerInteract.currentHighlight = component;
		return true;
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x0000B461 File Offset: 0x00009661
	public void UpdateInteractionRenderers()
	{
		this.interactionHighlight = PlayerInteract.currentHighlight.GetHighlightedRenderer();
		this.highlightsFX.SetRenderer(this.interactionHighlight, Color.white, HighlightsFX.SortingType.Overlay, false);
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x00042514 File Offset: 0x00040714
	private void SetInteractionFocus(Collider target, Renderer[] highlightRenderer)
	{
		this.interactionHighlight = highlightRenderer;
		this.highlightsFX.SetRenderer(this.interactionHighlight, Color.white, HighlightsFX.SortingType.Overlay, true);
		this.interactionTarget = target;
		this.dialogueActor.LookAt = true;
		DialogueActor dialogueActor;
		if (this.interactionTarget.TryGetComponent<DialogueActor>(ref dialogueActor))
		{
			this.dialogueActor.lookAtTarget = dialogueActor.DialogueAnchor.position;
			return;
		}
		this.dialogueActor.lookAtTarget = this.interactionTarget.transform.position;
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x0000B48B File Offset: 0x0000968B
	private void ClearInteractionFocus()
	{
		if (this.highlightsFX != null)
		{
			this.highlightsFX.ClearRenderer();
		}
		this.interactionHighlight = null;
		this.interactionTarget = null;
		PlayerInteract.currentHighlight = null;
		this.dialogueActor.LookAt = false;
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x0000B4C6 File Offset: 0x000096C6
	private void FixedUpdate()
	{
		this.UpdateInteraction();
		if (this.targetList.Count == 0)
		{
			base.enabled = false;
		}
	}

	// Token: 0x04000F45 RID: 3909
	private static PlayerInteract p;

	// Token: 0x04000F46 RID: 3910
	public static InteractionHighlight currentHighlight;

	// Token: 0x04000F47 RID: 3911
	public static GameObject interactButtonPriority;

	// Token: 0x04000F48 RID: 3912
	private float interactionTime;

	// Token: 0x04000F49 RID: 3913
	private List<Collider> targetList = new List<Collider>();

	// Token: 0x04000F4A RID: 3914
	protected Collider interactionTarget;

	// Token: 0x04000F4B RID: 3915
	private Renderer[] interactionHighlight;

	// Token: 0x04000F4C RID: 3916
	private HighlightsFX highlightsFX;

	// Token: 0x04000F4D RID: 3917
	public DialogueActor dialogueActor;

	// Token: 0x04000F4E RID: 3918
	public GameObject interactionPrompt;

	// Token: 0x04000F4F RID: 3919
	public UIFollow promptFollow;

	// Token: 0x04000F50 RID: 3920
	private bool hadInteractionTarget;

	// Token: 0x04000F51 RID: 3921
	private const int sameTargetRequirement = 4;

	// Token: 0x04000F52 RID: 3922
	private int sameTargetFrameCount;

	// Token: 0x04000F53 RID: 3923
	private Collider possibleTarget;

	// Token: 0x04000F54 RID: 3924
	public PlayerOrbitCamera playerOrbitCamera;

	// Token: 0x04000F55 RID: 3925
	public float distancePriorityWeight = 10f;

	// Token: 0x04000F56 RID: 3926
	public float playerPriorityWeight = 5f;

	// Token: 0x04000F57 RID: 3927
	public float cameraPriorityWeight = 1f;
}
