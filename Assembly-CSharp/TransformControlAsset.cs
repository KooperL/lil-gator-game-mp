using System;
using UnityEngine;
using UnityEngine.Playables;

public class TransformControlAsset : PlayableAsset
{
	// Token: 0x06000011 RID: 17 RVA: 0x00002531 File Offset: 0x00000731
	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<TransformControlBehaviour>.Create(graph, this.template, 0);
	}

	public TransformControlBehaviour template;
}
