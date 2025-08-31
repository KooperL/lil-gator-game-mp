using System;
using UnityEngine;

public class ActorPathFollower : MonoBehaviour, IManagedUpdate
{
	// Token: 0x060001E4 RID: 484 RVA: 0x0000A36C File Offset: 0x0000856C
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

	// Token: 0x060001E5 RID: 485 RVA: 0x0000A3EC File Offset: 0x000085EC
	private void Start()
	{
		this.scaledSpeed = this.speed / Mathf.Min(1f, base.transform.localScale.x);
		if (this.randomizeStartPosition)
		{
			this.nodePosition = Random.Range(0f, (float)(this.path.positions.Length - 1));
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

	// Token: 0x060001E6 RID: 486 RVA: 0x0000A48F File Offset: 0x0000868F
	private void InitializePosition()
	{
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x0000A494 File Offset: 0x00008694
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

	// Token: 0x060001E8 RID: 488 RVA: 0x0000A4E5 File Offset: 0x000086E5
	private void OnDisable()
	{
		if (this.isInRange)
		{
			FastUpdateManager.updateEvery1.Remove(this);
			return;
		}
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x0000A508 File Offset: 0x00008708
	private bool IsMoving()
	{
		return (!this.stopWhileTalking || !this.actor.IsInDialogue) && this.moving;
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000A528 File Offset: 0x00008728
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
			if (this.waitAtNode > 0f && Mathf.Floor(this.oldNodePosition) != Mathf.Floor(this.nodePosition) && Random.value <= this.waitAtNodeChance)
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

	// Token: 0x060001EB RID: 491 RVA: 0x0000A7D0 File Offset: 0x000089D0
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
