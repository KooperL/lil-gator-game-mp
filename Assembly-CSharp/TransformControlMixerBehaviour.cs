using System;
using UnityEngine;
using UnityEngine.Playables;

// Token: 0x02000008 RID: 8
public class TransformControlMixerBehaviour : PlayableBehaviour
{
	// Token: 0x06000013 RID: 19 RVA: 0x0001740C File Offset: 0x0001560C
	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		Transform transform = playerData as Transform;
		Vector3 vector = Vector3.zero;
		Quaternion quaternion = Quaternion.identity;
		if (!transform)
		{
			return;
		}
		int inputCount = PlayableExtensions.GetInputCount<Playable>(playable);
		for (int i = 0; i < inputCount; i++)
		{
			float inputWeight = PlayableExtensions.GetInputWeight<Playable>(playable, i);
			TransformControlBehaviour behaviour = ((ScriptPlayable<!0>)PlayableExtensions.GetInput<Playable>(playable, i)).GetBehaviour();
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
