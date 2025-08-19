using System;
using UnityEngine;

public class ActorPathFollower : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000228 RID: 552 RVA: 0x0001EAB8 File Offset: 0x0001CCB8
	private void OnValidate()
	{
		if (this.actor == null)
		{
			this.actor = base.GetComponent<DialogueActor>();
		}
		if (this.animator == null)
		{
			this.animator = base.GetComponent<Animator>();
		}
		if (!this.groundLayers.Contains(0))
		{
			this.groundLayers = LayerMask.GetMask(new string[] { "default", "Terrain", "Player Collision" });
		}
	}

	// Token: 0x06000229 RID: 553 RVA: 0x0001EB38 File Offset: 0x0001CD38
	private void Start()
	{
		this.scaledSpeed = this.speed / Mathf.Min(1f, base.transform.localScale.x);
		if (this.randomizeStartPosition)
		{
			this.nodePosition = global::UnityEngine.Random.Range(0f, (float)(this.path.positions.Length - 1));
		}
		Vector3 interpolatedPosition = this.path.GetInterpolatedPosition(this.nodePosition);
		base.transform.position = interpolatedPosition;
		if (this.useTimeStep)
		{
			this.fromPosition = (this.toPosition = interpolatedPosition);
			this.DoNewTimeStep();
			this.t = 0f;
		}
	}

	// Token: 0x0600022A RID: 554 RVA: 0x00002229 File Offset: 0x00000429
	private void InitializePosition()
	{
	}

	// Token: 0x0600022B RID: 555 RVA: 0x0001EBDC File Offset: 0x0001CDDC
	private void OnEnable()
	{
		this.isInRange = this.path.SqrDistance(MainCamera.t.position) <= 3025f;
		if (this.isInRange)
		{
			FastUpdateManager.updateEvery1.Add(this);
			return;
		}
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x0600022C RID: 556 RVA: 0x00003CAA File Offset: 0x00001EAA
	private void OnDisable()
	{
		if (this.isInRange)
		{
			FastUpdateManager.updateEvery1.Remove(this);
			return;
		}
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x0600022D RID: 557 RVA: 0x00003CCD File Offset: 0x00001ECD
	private bool IsMoving()
	{
		return (!this.stopWhileTalking || !this.actor.IsInDialogue) && this.moving;
	}

	// Token: 0x0600022E RID: 558 RVA: 0x0001EC30 File Offset: 0x0001CE30
	public void ManagedUpdate()
	{
		if (this.isInRange != this.path.SqrDistance(MainCamera.t.position) <= 3025f)
		{
			this.isInRange = !this.isInRange;
			if (this.isInRange)
			{
				FastUpdateManager.updateEveryNonFixed.Remove(this);
				FastUpdateManager.updateEvery1.Add(this);
			}
			else
			{
				FastUpdateManager.updateEveryNonFixed.Add(this);
				FastUpdateManager.updateEvery1.Remove(this);
			}
		}
		if (!this.isInRange)
		{
			return;
		}
		float num;
		if (this.IsMoving())
		{
			if (this.waitAtNodeCounter > 0f)
			{
				this.waitAtNodeCounter -= Time.deltaTime;
				num = 0f;
			}
			else
			{
				num = this.speed;
			}
		}
		else
		{
			num = 0f;
		}
		this.smoothSpeed = Mathf.MoveTowards(this.smoothSpeed, num, this.speed * 4f * Time.deltaTime);
		if (this.smoothSpeed > 0f)
		{
			Vector3 position = base.transform.position;
			Vector3 vector;
			Vector3 vector2;
			this.path.Interpolate(ref this.nodePosition, this.smoothSpeed, out vector, out vector2, false);
			if (this.snapToGround)
			{
				RaycastHit raycastHit;
				float num2;
				if ((Player.Position - base.transform.position).magnitude < 30f && Physics.Raycast(vector2 + 0.5f * Vector3.up, Vector3.down, out raycastHit, 2f, this.groundLayers))
				{
					num2 = raycastHit.point.y - vector2.y;
				}
				else
				{
					num2 = 0f;
				}
				this.smoothGroundOffset = Mathf.SmoothDamp(this.smoothGroundOffset, num2, ref this.smoothGroundVelocity, 0.2f);
				vector2.y += this.smoothGroundOffset;
			}
			base.transform.position = vector2;
			Vector3 vector3 = (vector2 - position).Flat();
			base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, Quaternion.LookRotation(vector3), 720f * Time.deltaTime);
			if (this.waitAtNode > 0f && Mathf.Floor(this.oldNodePosition) != Mathf.Floor(this.nodePosition) && global::UnityEngine.Random.value <= this.waitAtNodeChance)
			{
				this.waitAtNodeCounter = this.waitAtNode;
			}
			this.oldNodePosition = this.nodePosition;
		}
		float num3 = Mathf.InverseLerp(0f, this.speed, this.smoothSpeed);
		float num4 = num3 * this.scaledSpeed;
		this.animator.SetFloat(this.speedID, num4);
		this.animator.SetLayerWeight(1, num3);
	}

	// Token: 0x0600022F RID: 559 RVA: 0x0001EED8 File Offset: 0x0001D0D8
	private void DoNewTimeStep()
	{
		this.fromPosition = this.toPosition;
		this.path.AddDistance(ref this.nodePosition, this.speed * this.timeStep);
		this.toPosition = this.path.GetInterpolatedPosition(this.nodePosition);
		Debug.DrawLine(this.toPosition + 0.5f * Vector3.up, this.toPosition + 1.5f * Vector3.down, Color.blue, this.timeStep);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.toPosition + 0.5f * Vector3.up, Vector3.down, out raycastHit, 2f, this.groundLayers))
		{
			this.toPosition = raycastHit.point;
		}
		this.direction = this.toPosition - this.fromPosition;
	}

	private const int movementLayer = 1;

	private readonly int speedID = Animator.StringToHash("Speed");

	public DialogueActor actor;

	public Animator animator;

	public ActorPath path;

	public float speed;

	private float scaledSpeed;

	private float smoothSpeed;

	public float waitAtNode;

	public float waitAtNodeChance = 1f;

	private float waitAtNodeCounter = -1f;

	private float oldNodePosition;

	public float nodePosition;

	private const float rotationSpeed = 720f;

	public bool snapToGround = true;

	private float smoothGroundOffset;

	private float smoothGroundVelocity;

	public LayerMask groundLayers;

	private Vector3 velocity;

	public bool useTimeStep = true;

	public float timeStep = 0.25f;

	private float t = 1f;

	private Vector3 fromPosition;

	private Vector3 toPosition;

	private Vector3 direction;

	public bool moving = true;

	public bool stopWhileTalking = true;

	public bool randomizeStartPosition = true;

	private bool isInRange;

	private const float accM = 4f;
}
