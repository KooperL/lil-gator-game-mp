using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActorMover : MonoBehaviour, IActorMover
{
	// Token: 0x0600038E RID: 910 RVA: 0x00004BC6 File Offset: 0x00002DC6
	private void OnValidate()
	{
		if (this.animator == null)
		{
			this.animator = base.GetComponent<Animator>();
		}
	}

	// Token: 0x0600038F RID: 911 RVA: 0x000269D0 File Offset: 0x00024BD0
	private void Awake()
	{
		if (this.animator == null)
		{
			this.animator = base.GetComponent<Animator>();
		}
		if (this.actor == null)
		{
			this.actor = base.GetComponent<DialogueActor>();
		}
		this.interactionCollider = this.actor.GetComponent<Collider>();
		base.enabled = false;
	}

	// Token: 0x06000390 RID: 912 RVA: 0x00004BE2 File Offset: 0x00002DE2
	private void OnEnable()
	{
		this.t = 0f;
		this.smoothSpeed = 0f;
		this.lastPosition = base.transform.position;
	}

	// Token: 0x06000391 RID: 913 RVA: 0x00026A2C File Offset: 0x00024C2C
	private void Update()
	{
		if (this.pathArray == null)
		{
			base.enabled = false;
			return;
		}
		Vector3 vector = base.transform.position;
		Vector3 vector2 = this.pathArray[this.pathArray.Length - 1];
		float num = Vector3.Distance(vector, this.path[0]);
		float num2 = Vector3.Distance(vector, vector2);
		float num3 = Mathf.InverseLerp(0.5f, 0f, num2);
		if (this.t >= (float)(this.path.Count - 1))
		{
			base.transform.position = vector2;
			base.transform.rotation = this.markRotation;
			this.path.Clear();
			base.enabled = false;
			this.animator.SetFloat(ActorMover.speedID, 0f);
			this.animator.SetLayerWeight(1, 0f);
			if (this.onReachMark != null)
			{
				this.onReachMark.Invoke();
			}
			if (this.interactionCollider != null)
			{
				this.interactionCollider.enabled = true;
				return;
			}
		}
		else
		{
			float num4 = Mathf.Max(0.05f, Mathf.Sqrt(2f * num2 * this.acc / 3f));
			this.smoothSpeed = Mathf.MoveTowards(this.smoothSpeed, this.targetSpeed, this.acc * Time.deltaTime);
			this.smoothSpeed = Mathf.Min(num4, this.smoothSpeed);
			this.t = this.MoveAlongPath(this.t, Time.deltaTime * this.smoothSpeed);
			vector = iTween.PointOnPath(this.pathArray, this.t / (float)(this.pathArray.Length - 1));
			Vector3 normalized = (vector - this.lastPosition).normalized;
			this.lastPosition = vector;
			Quaternion quaternion = Quaternion.LookRotation(normalized);
			if (this.snapMovementToGround)
			{
				LayerUtil.SnapToGround(ref vector, 5f);
			}
			if (num3 > 0f)
			{
				quaternion = MathUtils.SlerpFlat(quaternion, this.markRotation, num3);
			}
			base.transform.position = vector;
			if (num < 0.5f)
			{
				base.transform.rotation = MathUtils.SlerpFlat(base.transform.rotation, quaternion, Mathf.InverseLerp(0f, 0.5f, num));
			}
			else
			{
				base.transform.rotation = quaternion;
			}
			this.animator.SetFloat(ActorMover.speedID, this.smoothSpeed);
			this.animator.SetLayerWeight(1, Mathf.Min(Mathf.Clamp01(this.smoothSpeed), 1f - num3));
		}
	}

	// Token: 0x06000392 RID: 914 RVA: 0x00026CAC File Offset: 0x00024EAC
	public float MoveAlongPath(float t, float distance)
	{
		if (distance == 0f)
		{
			return t;
		}
		if (t == 0f && distance < 0f)
		{
			return t;
		}
		if (t == (float)(this.path.Count - 1) && distance > 0f)
		{
			return t;
		}
		Vector3 vector = iTween.PointOnPath(this.pathArray, t / (float)(this.pathArray.Length - 1));
		Vector3 vector2 = iTween.PointOnPath(this.pathArray, (t + Mathf.Sign(distance) * 0.01f) / (float)(this.pathArray.Length - 1));
		float num = Vector3.Distance(vector, vector2);
		if (num == 0f)
		{
			return t;
		}
		float num2 = 0.01f / num;
		return Mathf.Clamp(t + distance * num2, 0f, (float)(this.path.Count - 1));
	}

	// Token: 0x06000393 RID: 915 RVA: 0x00026D64 File Offset: 0x00024F64
	public void SetMark(Vector3[] positions, Quaternion rotation, float speed, UnityEvent onReachMark, bool skipToStart = false, bool disableInteraction = true, bool playFootsteps = true)
	{
		if (this.actor == null)
		{
			this.actor = base.GetComponent<DialogueActor>();
		}
		this.actor.ClearEmote(true, false);
		base.enabled = false;
		this.path.Clear();
		if (!skipToStart)
		{
			this.path.Add(base.transform.position);
		}
		if (disableInteraction && this.interactionCollider != null)
		{
			this.interactionCollider.enabled = false;
		}
		this.onReachMark = onReachMark;
		if (speed == 0f)
		{
			this.targetSpeed = this.speed;
		}
		else
		{
			this.targetSpeed = speed;
		}
		this.path.AddRange(positions);
		if (this.snapPathToGround)
		{
			for (int i = 0; i < this.path.Count; i++)
			{
				Vector3 vector = this.path[i];
				LayerUtil.SnapToGround(ref vector, 5f);
				this.path[i] = vector;
			}
		}
		this.markRotation = rotation;
		this.pathArray = this.path.ToArray();
		if (this.mountedActor == null)
		{
			this.mountedActor = base.GetComponent<MountedActor>();
		}
		if (skipToStart)
		{
			if (this.mountedActor != null)
			{
				this.mountedActor.CancelMount();
			}
			base.transform.position = positions[0];
			this.actor.SetStateAndPosition(-1, 0, true, true);
			this.StartMoving();
			return;
		}
		if (this.mountedActor == null)
		{
			this.StartMoving();
			return;
		}
		this.mountedActor.onGottenOut.AddListener(new UnityAction(this.StartMoving));
		this.mountedActor.GetOut();
	}

	// Token: 0x06000394 RID: 916 RVA: 0x00026F0C File Offset: 0x0002510C
	private void StartMoving()
	{
		this.path[0] = (this.pathArray[0] = base.transform.position);
		this.actor.Position = 0;
		base.enabled = true;
	}

	// Token: 0x06000395 RID: 917 RVA: 0x00026F54 File Offset: 0x00025154
	public void CancelMove()
	{
		if (!base.enabled)
		{
			return;
		}
		this.path.Clear();
		base.enabled = false;
		this.animator.SetFloat(ActorMover.speedID, 0f);
		this.animator.SetLayerWeight(1, 0f);
		this.onReachMark = new UnityEvent();
		if (this.interactionCollider != null)
		{
			this.interactionCollider.enabled = true;
		}
	}

	private DialogueActor actor;

	private MountedActor mountedActor;

	private static readonly int speedID = Animator.StringToHash("Speed");

	private const int movementLayer = 1;

	public Animator animator;

	public bool snapPathToGround = true;

	public bool snapMovementToGround;

	public float speed = 3f;

	public float acc = 15f;

	private float targetSpeed;

	private float smoothSpeed;

	private List<Vector3> path = new List<Vector3>();

	private Vector3[] pathArray;

	private Quaternion markRotation;

	private float t;

	private const float targetRotationLerpDistance = 0.5f;

	private Vector3 lastPosition;

	private UnityEvent onReachMark;

	private Collider interactionCollider;

	private const float speedStepProbe = 0.01f;
}
