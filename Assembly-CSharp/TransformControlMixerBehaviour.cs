using System;
using UnityEngine;
using UnityEngine.Playables;

// Token: 0x02000008 RID: 8
public class TransformControlMixerBehaviour : PlayableBehaviour
{
	// Token: 0x06000013 RID: 19 RVA: 0x00002550 File Offset: 0x00000750
	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		Transform transform = playerData as Transform;
		Vector3 vector = Vector3.zero;
		Quaternion quaternion = Quaternion.identity;
		if (!transform)
		{
			return;
		}
		int inputCount = playable.GetInputCount<Playable>();
		for (int i = 0; i < inputCount; i++)
		{
			float inputWeight = playable.GetInputWeight(i);
			TransformControlBehaviour behaviour = ((ScriptPlayable<T>)playable.GetInput(i)).GetBehaviour();
			vector += behaviour.position * inputWeight;
			if (i == 0)
			{
				quaternion = behaviour.rotation;
			}
			else
			{
				quaternion = Quaternion.RotateTowards(quaternion, behaviour.rotation, 360f * inputWeight);
			}
		}
		transform.position = vector;
		transform.rotation = quaternion;
	}
}
