using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
	// (get) Token: 0x06000A41 RID: 2625 RVA: 0x000307D9 File Offset: 0x0002E9D9
	public static GameObject CurrentTarget
	{
		get
		{
			return PlayerInteract.p.interactionTarget.gameObject;
		}
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x000307EA File Offset: 0x0002E9EA
	public static void CurrentHighlightChanged()
	{
		PlayerInteract.p.UpdateInteractionRenderers();
	}

	// (get) Token: 0x06000A43 RID: 2627 RVA: 0x000307F6 File Offset: 0x0002E9F6
	public bool InteractionAvailable
	{
		get
		{
			return this.interactionTarget != null;
		}
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x00030804 File Offset: 0x0002EA04
	private void OnValidate()
	{
		if (this.playerOrbitCamera == null)
		{
			this.playerOrbitCamera = Object.FindObjectOfType<PlayerOrbitCamera>();
		}
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x0003081F File Offset: 0x0002EA1F
	private void Awake()
	{
		PlayerInteract.p = this;
		if (this.dialogueActor == null)
		{
			this.dialogueActor = base.transform.parent.GetComponent<DialogueActor>();
		}
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x0003084B File Offset: 0x0002EA4B
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

	// Token: 0x06000A47 RID: 2631 RVA: 0x0003086F File Offset: 0x0002EA6F
	private void Start()
	{
		this.highlightsFX = HighlightsFX.h;
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x0003087C File Offset: 0x0002EA7C
	private void OnTriggerEnter(Collider other)
	{
		this.AddInteraction(other);
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x00030885 File Offset: 0x0002EA85
	private void OnTriggerStay(Collider other)
	{
		this.AddInteraction(other);
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x0003088E File Offset: 0x0002EA8E
	private void OnTriggerExit(Collider other)
	{
		this.RemoveInteraction(other);
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x00030897 File Offset: 0x0002EA97
	private void AddInteraction(Collider target)
	{
		base.enabled = true;
		if (!this.targetList.Contains(target))
		{
			this.targetList.Add(target);
		}
		this.UpdateInteraction();
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x000308C0 File Offset: 0x0002EAC0
	private void RemoveInteraction(Collider target)
	{
		this.targetList.Remove(target);
		if (this.interactionTarget == target && this.interactionHighlight != null)
		{
			this.ClearInteractionFocus();
		}
		this.UpdateInteraction();
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x000308F4 File Offset: 0x0002EAF4
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

	// Token: 0x06000A4E RID: 2638 RVA: 0x000309A0 File Offset: 0x0002EBA0
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

	// Token: 0x06000A4F RID: 2639 RVA: 0x00030BB8 File Offset: 0x0002EDB8
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

	// Token: 0x06000A50 RID: 2640 RVA: 0x00030C40 File Offset: 0x0002EE40
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

	// Token: 0x06000A51 RID: 2641 RVA: 0x00030C94 File Offset: 0x0002EE94
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

	// Token: 0x06000A52 RID: 2642 RVA: 0x00030CC5 File Offset: 0x0002EEC5
	public void UpdateInteractionRenderers()
	{
		this.interactionHighlight = PlayerInteract.currentHighlight.GetHighlightedRenderer();
		this.highlightsFX.SetRenderer(this.interactionHighlight, Color.white, HighlightsFX.SortingType.Overlay, false);
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x00030CF0 File Offset: 0x0002EEF0
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

	// Token: 0x06000A54 RID: 2644 RVA: 0x00030D70 File Offset: 0x0002EF70
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

	// Token: 0x06000A55 RID: 2645 RVA: 0x00030DAB File Offset: 0x0002EFAB
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
