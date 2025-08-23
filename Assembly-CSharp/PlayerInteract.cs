using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
	// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0000B664 File Offset: 0x00009864
	public static GameObject CurrentTarget
	{
		get
		{
			return PlayerInteract.p.interactionTarget.gameObject;
		}
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x0000B675 File Offset: 0x00009875
	public static void CurrentHighlightChanged()
	{
		PlayerInteract.p.UpdateInteractionRenderers();
	}

	// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0000B681 File Offset: 0x00009881
	public bool InteractionAvailable
	{
		get
		{
			return this.interactionTarget != null;
		}
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x0000B68F File Offset: 0x0000988F
	private void OnValidate()
	{
		if (this.playerOrbitCamera == null)
		{
			this.playerOrbitCamera = global::UnityEngine.Object.FindObjectOfType<PlayerOrbitCamera>();
		}
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x0000B6AA File Offset: 0x000098AA
	private void Awake()
	{
		PlayerInteract.p = this;
		if (this.dialogueActor == null)
		{
			this.dialogueActor = base.transform.parent.GetComponent<DialogueActor>();
		}
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x0000B6D6 File Offset: 0x000098D6
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

	// Token: 0x06000C49 RID: 3145 RVA: 0x0000B6FA File Offset: 0x000098FA
	private void Start()
	{
		this.highlightsFX = HighlightsFX.h;
	}

	// Token: 0x06000C4A RID: 3146 RVA: 0x0000B707 File Offset: 0x00009907
	private void OnTriggerEnter(Collider other)
	{
		this.AddInteraction(other);
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x0000B707 File Offset: 0x00009907
	private void OnTriggerStay(Collider other)
	{
		this.AddInteraction(other);
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x0000B710 File Offset: 0x00009910
	private void OnTriggerExit(Collider other)
	{
		this.RemoveInteraction(other);
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x0000B719 File Offset: 0x00009919
	private void AddInteraction(Collider target)
	{
		base.enabled = true;
		if (!this.targetList.Contains(target))
		{
			this.targetList.Add(target);
		}
		this.UpdateInteraction();
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x0000B742 File Offset: 0x00009942
	private void RemoveInteraction(Collider target)
	{
		this.targetList.Remove(target);
		if (this.interactionTarget == target && this.interactionHighlight != null)
		{
			this.ClearInteractionFocus();
		}
		this.UpdateInteraction();
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x00043F90 File Offset: 0x00042190
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

	// Token: 0x06000C50 RID: 3152 RVA: 0x0004403C File Offset: 0x0004223C
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

	// Token: 0x06000C51 RID: 3153 RVA: 0x00044254 File Offset: 0x00042454
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

	// Token: 0x06000C52 RID: 3154 RVA: 0x000442DC File Offset: 0x000424DC
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

	// Token: 0x06000C53 RID: 3155 RVA: 0x00044330 File Offset: 0x00042530
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

	// Token: 0x06000C54 RID: 3156 RVA: 0x0000B773 File Offset: 0x00009973
	public void UpdateInteractionRenderers()
	{
		this.interactionHighlight = PlayerInteract.currentHighlight.GetHighlightedRenderer();
		this.highlightsFX.SetRenderer(this.interactionHighlight, Color.white, HighlightsFX.SortingType.Overlay, false);
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x00044364 File Offset: 0x00042564
	private void SetInteractionFocus(Collider target, Renderer[] highlightRenderer)
	{
		this.interactionHighlight = highlightRenderer;
		this.highlightsFX.SetRenderer(this.interactionHighlight, Color.white, HighlightsFX.SortingType.Overlay, true);
		this.interactionTarget = target;
		this.dialogueActor.LookAt = true;
		DialogueActor dialogueActor;
		if (this.interactionTarget.TryGetComponent<DialogueActor>(out dialogueActor))
		{
			this.dialogueActor.lookAtTarget = dialogueActor.DialogueAnchor.position;
			return;
		}
		this.dialogueActor.lookAtTarget = this.interactionTarget.transform.position;
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x0000B79D File Offset: 0x0000999D
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

	// Token: 0x06000C57 RID: 3159 RVA: 0x0000B7D8 File Offset: 0x000099D8
	private void FixedUpdate()
	{
		this.UpdateInteraction();
		if (this.targetList.Count == 0)
		{
			base.enabled = false;
		}
	}

	private static PlayerInteract p;

	public static InteractionHighlight currentHighlight;

	public static GameObject interactButtonPriority;

	private float interactionTime;

	private List<Collider> targetList = new List<Collider>();

	protected Collider interactionTarget;

	private Renderer[] interactionHighlight;

	private HighlightsFX highlightsFX;

	public DialogueActor dialogueActor;

	public GameObject interactionPrompt;

	public UIFollow promptFollow;

	private bool hadInteractionTarget;

	private const int sameTargetRequirement = 4;

	private int sameTargetFrameCount;

	private Collider possibleTarget;

	public PlayerOrbitCamera playerOrbitCamera;

	public float distancePriorityWeight = 10f;

	public float playerPriorityWeight = 5f;

	public float cameraPriorityWeight = 1f;
}
