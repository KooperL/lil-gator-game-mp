using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000027 RID: 39
[AddComponentMenu("Music/MusicSystem States")]
public class MusicSystemStates : MonoBehaviour
{
	// Token: 0x0600009A RID: 154 RVA: 0x00004F4E File Offset: 0x0000314E
	private void OnValidate()
	{
		this.musicSystem = base.GetComponent<MusicSystem>();
		this.dynamicStates = base.GetComponent<MusicSystemDynamicStates>();
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00004F68 File Offset: 0x00003168
	public void SetState(string stateName)
	{
		if (this.dynamicStates != null)
		{
			this.dynamicStates.enabled = stateName == "Dynamic";
			if (this.dynamicStates.enabled)
			{
				return;
			}
		}
		int num = 0;
		int num2 = -1;
		for (int i = 0; i < this.states.Length; i++)
		{
			if (this.states[i].isFallback)
			{
				num = i;
			}
			if (this.states[i].name == stateName || this.states[i].applicableStates.Contains(stateName))
			{
				num2 = i;
				break;
			}
		}
		if (num2 == -1)
		{
			num2 = num;
		}
		MusicSystemStates.State state = this.states[num2];
		this.musicSystem.ClearLayerWeights();
		for (int j = 0; j < this.musicSystem.layers.Length; j++)
		{
			for (int k = 0; k < state.layers.Length; k++)
			{
				if (this.musicSystem.layers[j].name == state.layers[k].layerName)
				{
					this.musicSystem.layers[j].weight = state.layers[k].volume;
				}
			}
		}
	}

	// Token: 0x0600009C RID: 156 RVA: 0x000050A3 File Offset: 0x000032A3
	private void OnEnable()
	{
		if (!MusicSystemStates.current.Contains(this))
		{
			MusicSystemStates.current.Add(this);
		}
	}

	// Token: 0x0600009D RID: 157 RVA: 0x000050BD File Offset: 0x000032BD
	private void OnDestroy()
	{
		if (MusicSystemStates.current.Contains(this))
		{
			MusicSystemStates.current.Remove(this);
		}
	}

	// Token: 0x040000CE RID: 206
	public static List<MusicSystemStates> current = new List<MusicSystemStates>();

	// Token: 0x040000CF RID: 207
	public MusicSystem musicSystem;

	// Token: 0x040000D0 RID: 208
	public MusicSystemDynamicStates dynamicStates;

	// Token: 0x040000D1 RID: 209
	public MusicSystemStates.State[] states;

	// Token: 0x02000358 RID: 856
	[Serializable]
	public struct State
	{
		// Token: 0x040019F5 RID: 6645
		public string name;

		// Token: 0x040019F6 RID: 6646
		[MusicStateLookup("musicStates")]
		public string[] applicableStates;

		// Token: 0x040019F7 RID: 6647
		public MusicSystemStates.State.Layer[] layers;

		// Token: 0x040019F8 RID: 6648
		public bool isFallback;

		// Token: 0x020004BB RID: 1211
		[Serializable]
		public class Layer
		{
			// Token: 0x04001FA6 RID: 8102
			[MusicLayerLookup("musicSystem")]
			public string layerName;

			// Token: 0x04001FA7 RID: 8103
			[Range(0f, 1f)]
			public float volume = 1f;
		}
	}
}
