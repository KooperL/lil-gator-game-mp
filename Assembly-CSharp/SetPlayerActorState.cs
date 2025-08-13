using System;
using UnityEngine;

// Token: 0x020000FC RID: 252
public class SetPlayerActorState : MonoBehaviour
{
	// Token: 0x060004C3 RID: 1219 RVA: 0x0002BD38 File Offset: 0x00029F38
	private void OnEnable()
	{
		Player.actor.SetStateAndPosition((int)this.state, (int)this.position, this.skipTransition, false);
		if (this.applyTransform)
		{
			IActorMover component = Player.animator.GetComponent<IActorMover>();
			if (component != null)
			{
				component.CancelMove();
			}
			MountedActor component2 = Player.animator.GetComponent<MountedActor>();
			if (component2 != null)
			{
				component2.CancelMount();
			}
			RaycastHit raycastHit;
			if (this.snapToFloor && Physics.Raycast(base.transform.position + Vector3.up, Vector3.down, ref raycastHit, 3f))
			{
				base.transform.position = raycastHit.point;
			}
			Player.movement.ApplyTransform(base.transform);
		}
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x0002BDF0 File Offset: 0x00029FF0
	[ContextMenu("Snap To Floor")]
	public void SnapToFloor()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + Vector3.up, Vector3.down, ref raycastHit, 3f, LayerMask.GetMask(new string[] { "default", "Terrain" })))
		{
			base.transform.position = raycastHit.point;
		}
	}

	// Token: 0x040006C2 RID: 1730
	public ActorPosition position;

	// Token: 0x040006C3 RID: 1731
	public ActorState state = ActorState.S_Happy;

	// Token: 0x040006C4 RID: 1732
	public bool skipTransition;

	// Token: 0x040006C5 RID: 1733
	public bool applyTransform;

	// Token: 0x040006C6 RID: 1734
	[ConditionalHide("applyTransform")]
	public bool snapToFloor;

	// Token: 0x040006C7 RID: 1735
	private int stateID = Animator.StringToHash("State");

	// Token: 0x040006C8 RID: 1736
	private int positionID = Animator.StringToHash("Position");
}
