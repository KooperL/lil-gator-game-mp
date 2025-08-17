using System;
using UnityEngine;

public class SetPlayerActorState : MonoBehaviour
{
	// Token: 0x060004E9 RID: 1257 RVA: 0x0002CE98 File Offset: 0x0002B098
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
			if (this.snapToFloor && Physics.Raycast(base.transform.position + Vector3.up, Vector3.down, out raycastHit, 3f))
			{
				base.transform.position = raycastHit.point;
			}
			Player.movement.ApplyTransform(base.transform);
		}
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x0002CF50 File Offset: 0x0002B150
	[ContextMenu("Snap To Floor")]
	public void SnapToFloor()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + Vector3.up, Vector3.down, out raycastHit, 3f, LayerMask.GetMask(new string[] { "default", "Terrain" })))
		{
			base.transform.position = raycastHit.point;
		}
	}

	public ActorPosition position;

	public ActorState state = ActorState.S_Happy;

	public bool skipTransition;

	public bool applyTransform;

	[ConditionalHide("applyTransform")]
	public bool snapToFloor;

	private int stateID = Animator.StringToHash("State");

	private int positionID = Animator.StringToHash("Position");
}
