using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Music/MusicSystem States")]
public class MusicSystemStates : MonoBehaviour
{
	// Token: 0x060000AD RID: 173 RVA: 0x00002937 File Offset: 0x00000B37
	private void OnValidate()
	{
		this.musicSystem = base.GetComponent<MusicSystem>();
		this.dynamicStates = base.GetComponent<MusicSystemDynamicStates>();
	}

	// Token: 0x060000AE RID: 174 RVA: 0x0001A0F4 File Offset: 0x000182F4
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

	// Token: 0x060000AF RID: 175 RVA: 0x00002951 File Offset: 0x00000B51
	private void OnEnable()
	{
		if (!MusicSystemStates.current.Contains(this))
		{
			MusicSystemStates.current.Add(this);
		}
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x0000296B File Offset: 0x00000B6B
	private void OnDestroy()
	{
		if (MusicSystemStates.current.Contains(this))
		{
			MusicSystemStates.current.Remove(this);
		}
	}

	public static List<MusicSystemStates> current = new List<MusicSystemStates>();

	public MusicSystem musicSystem;

	public MusicSystemDynamicStates dynamicStates;

	public MusicSystemStates.State[] states;

	[Serializable]
	public struct State
	{
		public string name;

		[MusicStateLookup("musicStates")]
		public string[] applicableStates;

		public MusicSystemStates.State.Layer[] layers;

		public bool isFallback;

		[Serializable]
		public class Layer
		{
			[MusicLayerLookup("musicSystem")]
			public string layerName;

			[Range(0f, 1f)]
			public float volume = 1f;
		}
	}
}
