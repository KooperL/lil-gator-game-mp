using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000B7 RID: 183
public class PlayerActorMover : MonoBehaviour, IActorMover, ICustomPlayerMovement
{
	// Token: 0x060003F3 RID: 1011 RVA: 0x00017160 File Offset: 0x00015360
	public void MovementUpdate(Vector3 input, ref Vector3 position, ref Vector3 velocity, ref Vector3 direction, ref Vector3 up, ref float animationIndex)
	{
		if (this.pathArray == null)
		{
			base.enabled = false;
			return;
		}
		Vector3 vector = this.pathArray[this.pathArray.Length - 1];
		float num = Vector3.Distance(position, this.path[0]);
		float num2 = Vector3.Distance(position, vector);
		float num3 = Mathf.InverseLerp(0.5f, 0f, num2);
		if (this.t >= (float)(this.path.Count - 1))
		{
			position = vector;
			direction = this.markRotation * Vector3.forward;
			velocity = Vector3.zero;
			base.enabled = false;
			UnityEvent unityEvent = this.onReachMark;
			Player.movement.ClearMods();
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			this.onReachMark = new UnityEvent();
			return;
		}
		float num4 = Mathf.Max(0.05f, Mathf.Sqrt(2f * num2 * this.acc / 3f));
		this.smoothSpeed = Mathf.MoveTowards(this.smoothSpeed, this.targetSpeed, this.acc * Time.deltaTime);
		this.smoothSpeed = Mathf.Min(this.smoothSpeed, num4);
		this.t = this.MoveAlongPath(this.t, Time.deltaTime * this.smoothSpeed);
		position = iTween.PointOnPath(this.pathArray, this.t / (float)(this.pathArray.Length - 1));
		Vector3 vector2 = (position - this.lastPosition).normalized;
		this.lastPosition = position;
		velocity = this.smoothSpeed * vector2;
		if (this.snapMovementToGround)
		{
			LayerUtil.SnapToGround(ref position, 5f);
		}
		if (num3 > 0f)
		{
			vector2 = MathUtils.SlerpFlat(vector2, this.markRotation * Vector3.forward, num3, false);
		}
		if (num < 0.5f)
		{
			direction = MathUtils.SlerpFlat(direction, vector2, Mathf.InverseLerp(0f, 0.5f, num), false);
			return;
		}
		direction = vector2;
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x00017380 File Offset: 0x00015580
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

	// Token: 0x060003F5 RID: 1013 RVA: 0x00017438 File Offset: 0x00015638
	public void SetMark(Vector3[] positions, Quaternion rotation, float speed, UnityEvent onReachMark, bool skipToStart, bool disableInteraction = false, bool playFootsteps = true)
	{
		if (this.path == null)
		{
			this.path = new List<Vector3>();
		}
		this.path.Clear();
		if (!skipToStart)
		{
			this.path.Add(base.transform.position);
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
			Player.movement.SetPosition(positions[0]);
			Vector3 vector2 = iTween.PointOnPath(this.pathArray, 0.02f) - positions[0];
			Player.movement.SetRotation(Quaternion.LookRotation(vector2.Flat()));
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

	// Token: 0x060003F6 RID: 1014 RVA: 0x000175C8 File Offset: 0x000157C8
	private void StartMoving()
	{
		this.path[0] = (this.pathArray[0] = base.transform.position);
		this.t = 0f;
		this.smoothSpeed = 0f;
		this.lastPosition = base.transform.position;
		Player.movement.isModified = true;
		Player.movement.modIgnoreReady = true;
		Player.movement.modNoInteractions = true;
		Player.movement.moddedWithoutControl = true;
		Player.movement.modCustomMovement = true;
		Player.movement.modCustomMovementScript = this;
		Player.movement.modDisableCollision = true;
		Player.movement.modNoClimbing = true;
		Player.movement.modJumpRule = PlayerMovement.ModRule.Locked;
		Player.movement.modGlideRule = PlayerMovement.ModRule.Locked;
		Player.movement.modSecondaryRule = PlayerMovement.ModRule.Locked;
		Player.movement.modPrimaryRule = PlayerMovement.ModRule.Allowed;
		Player.movement.modItemRule = PlayerMovement.ModRule.Locked;
		Player.movement.modIgnoreGroundedness = true;
		Player.actor.Position = 0;
		base.enabled = true;
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x000176CE File Offset: 0x000158CE
	public void Cancel()
	{
		this.onReachMark = new UnityEvent();
		base.enabled = false;
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x000176E2 File Offset: 0x000158E2
	public void CancelMove()
	{
		if (!base.enabled)
		{
			return;
		}
		Player.movement.ClearMods();
	}

	// Token: 0x04000577 RID: 1399
	private MountedActor mountedActor;

	// Token: 0x04000578 RID: 1400
	public bool snapPathToGround = true;

	// Token: 0x04000579 RID: 1401
	public bool snapMovementToGround;

	// Token: 0x0400057A RID: 1402
	public float speed = 4f;

	// Token: 0x0400057B RID: 1403
	public float acc = 15f;

	// Token: 0x0400057C RID: 1404
	private float targetSpeed;

	// Token: 0x0400057D RID: 1405
	private float smoothSpeed;

	// Token: 0x0400057E RID: 1406
	private List<Vector3> path = new List<Vector3>();

	// Token: 0x0400057F RID: 1407
	private Vector3[] pathArray;

	// Token: 0x04000580 RID: 1408
	private Quaternion markRotation;

	// Token: 0x04000581 RID: 1409
	private float t;

	// Token: 0x04000582 RID: 1410
	private const float targetRotationLerpDistance = 0.5f;

	// Token: 0x04000583 RID: 1411
	private Vector3 lastPosition;

	// Token: 0x04000584 RID: 1412
	private UnityEvent onReachMark;

	// Token: 0x04000585 RID: 1413
	private const float speedStepProbe = 0.01f;
}
