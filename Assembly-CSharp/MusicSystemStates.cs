using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002F RID: 47
[AddComponentMenu("Music/MusicSystem States")]
public class MusicSystemStates : MonoBehaviour
{
	// Token: 0x060000A5 RID: 165 RVA: 0x000028D3 File Offset: 0x00000AD3
	private void OnValidate()
	{
		this.musicSystem = base.GetComponent<MusicSystem>();
		this.dynamicStates = base.GetComponent<MusicSystemDynamicStates>();
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00019894 File Offset: 0x00017A94
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

	// Token: 0x060000A7 RID: 167 RVA: 0x000028ED File Offset: 0x00000AED
	private void OnEnable()
	{
		if (!MusicSystemStates.current.Contains(this))
		{
			MusicSystemStates.current.Add(this);
		}
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00002907 File Offset: 0x00000B07
	private void OnDestroy()
	{
		if (MusicSystemStates.current.Contains(this))
		{
			MusicSystemStates.current.Remove(this);
		}
	}

	// Token: 0x040000EF RID: 239
	public static List<MusicSystemStates> current = new List<MusicSystemStates>();

	// Token: 0x040000F0 RID: 240
	public MusicSystem musicSystem;

	// Token: 0x040000F1 RID: 241
	public MusicSystemDynamicStates dynamicStates;

	// Token: 0x040000F2 RID: 242
	public MusicSystemStates.State[] states;

	// Token: 0x02000030 RID: 48
	[Serializable]
	public struct State
	{
		// Token: 0x040000F3 RID: 243
		public string name;

		// Token: 0x040000F4 RID: 244
		[MusicStateLookup("musicStates")]
		public string[] applicableStates;

		// Token: 0x040000F5 RID: 245
		public MusicSystemStates.State.Layer[] layers;

		// Token: 0x040000F6 RID: 246
		public bool isFallback;

		// Token: 0x02000031 RID: 49
		[Serializable]
		public class Layer
		{
			// Token: 0x040000F7 RID: 247
			[MusicLayerLookup("musicSystem")]
			public string layerName;

			// Token: 0x040000F8 RID: 248
			[Range(0f, 1f)]
			public float volume = 1f;
		}
	}
}
