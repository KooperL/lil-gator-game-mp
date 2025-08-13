using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

// Token: 0x02000005 RID: 5
[TrackClipType(typeof(TransformControlAsset))]
[TrackBindingType(typeof(Transform))]
public class TransformControlTrack : TrackAsset
{
	// Token: 0x0600000E RID: 14 RVA: 0x00002121 File Offset: 0x00000321
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
	{
		return ScriptPlayable<TransformControlMixerBehaviour>.Create(graph, inputCount);
	}
}
