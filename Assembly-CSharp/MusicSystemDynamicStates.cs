using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Music/MusicSystem Dynamic States")]
public class MusicSystemDynamicStates : MonoBehaviour, IManagedUpdate
{
	// Token: 0x0600009D RID: 157 RVA: 0x00019CEC File Offset: 0x00017EEC
	private void OnValidate()
	{
		if (this.musicSystem == null)
		{
			this.musicSystem = base.GetComponent<MusicSystem>();
		}
		if (this.dynamicStates != null && this.dynamicStates.Length != 0)
		{
			this.currentState = Mathf.Clamp(this.currentState, 0, this.dynamicStates.Length);
			for (int i = 0; i < this.dynamicStates.Length; i++)
			{
				if (this.dynamicStates[i].variants != null && this.dynamicStates[i].variants.Length != 0)
				{
					for (int j = 0; j < this.dynamicStates[i].variants.Length; j++)
					{
						this.dynamicStates[i].variants[j].name = this.dynamicStates[i].variants[j].variant.ToString();
					}
				}
			}
		}
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00019DC4 File Offset: 0x00017FC4
	private void Awake()
	{
		MusicSystemDynamicStates.DynamicState[] array = this.dynamicStates;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Initialize(this.musicSystem);
		}
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00019DF4 File Offset: 0x00017FF4
	private void OnEnable()
	{
		this.isPlayerActiveSmooth = 1f;
		this.currentVariant = MusicSystemDynamicStates.VariantStates.Dialogue;
		for (int i = 0; i < this.dynamicStates.Length; i++)
		{
			this.dynamicStates[i].lastEligibleTime = -100f;
		}
		this.UpdateDynamicState(this.currentState, this.currentVariant);
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x000028C1 File Offset: 0x00000AC1
	private void OnDisable()
	{
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00019E58 File Offset: 0x00018058
	public void ManagedUpdate()
	{
		MusicSystemDynamicStates.VariantStates variantStates = this.currentVariant;
		int num = this.currentState;
		int num2 = -100;
		for (int i = 0; i < this.dynamicStates.Length; i++)
		{
			MusicSystemDynamicStates.DynamicState dynamicState = this.dynamicStates[i];
			if (dynamicState.IsEligible && dynamicState.priority >= num2)
			{
				num2 = dynamicState.priority;
				num = i;
			}
		}
		if (this.forceUpdate || num != this.currentState || variantStates != this.currentVariant)
		{
			this.UpdateDynamicState(num, variantStates);
		}
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00019ED4 File Offset: 0x000180D4
	private void UpdateDynamicState(int newState, MusicSystemDynamicStates.VariantStates newVariant)
	{
		this.currentState = newState;
		this.currentVariant = newVariant;
		if (this.ignoreMusicLayers)
		{
			return;
		}
		this.musicSystem.ClearLayerWeights();
		MusicSystemDynamicStates.DynamicState dynamicState = this.dynamicStates[this.currentState];
		MusicSystemDynamicStates.DynamicState.LayerSetting[] array = dynamicState.layerSettings;
		foreach (MusicSystemDynamicStates.DynamicState.DynamicStateVariant dynamicStateVariant in dynamicState.variants)
		{
			if (dynamicStateVariant.variant == this.currentVariant)
			{
				array = dynamicStateVariant.layerSettings;
				break;
			}
		}
		foreach (MusicSystemDynamicStates.DynamicState.LayerSetting layerSetting in array)
		{
			this.musicSystem.layers[layerSetting.layerIndex].weight = layerSetting.volume;
		}
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x00019F80 File Offset: 0x00018180
	public int GetStateIndex(string stateName)
	{
		for (int i = 0; i < this.dynamicStates.Length; i++)
		{
			if (this.dynamicStates[i].name == stateName)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x000028CF File Offset: 0x00000ACF
	public void MarkStateEligible(int index)
	{
		this.dynamicStates[index].lastEligibleTime = Time.time;
	}

	public MusicSystem musicSystem;

	public bool ignoreMusicLayers;

	public MusicSystemDynamicStates.DynamicState[] dynamicStates;

	public int currentState;

	public MusicSystemDynamicStates.VariantStates currentVariant = MusicSystemDynamicStates.VariantStates.Active;

	private float isPlayerActiveSmooth = 1f;

	public bool forceUpdate;

	private const float stateEligibilityDelay = 1f;

	public enum VariantStates
	{
		Stationary,
		Active,
		Dialogue = 10
	}

	[Serializable]
	public class DynamicState
	{
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000028FD File Offset: 0x00000AFD
		public bool IsEligible
		{
			get
			{
				return this.isLocked || this.lastEligibleTime + 1f > Time.time;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000291C File Offset: 0x00000B1C
		public int[] GetNeededLayers()
		{
			return this.neededLayers;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00019FB8 File Offset: 0x000181B8
		public void Initialize(MusicSystem musicSystem)
		{
			List<int> list = new List<int>();
			foreach (MusicSystemDynamicStates.DynamicState.LayerSetting layerSetting in this.layerSettings)
			{
				layerSetting.FindLayerIndex(musicSystem);
				if (layerSetting.layerIndex != -1 && !list.Contains(layerSetting.layerIndex))
				{
					list.Add(layerSetting.layerIndex);
				}
			}
			MusicSystemDynamicStates.DynamicState.DynamicStateVariant[] array2 = this.variants;
			for (int i = 0; i < array2.Length; i++)
			{
				foreach (MusicSystemDynamicStates.DynamicState.LayerSetting layerSetting2 in array2[i].layerSettings)
				{
					layerSetting2.FindLayerIndex(musicSystem);
					if (layerSetting2.layerIndex != -1 && !list.Contains(layerSetting2.layerIndex))
					{
						list.Add(layerSetting2.layerIndex);
					}
				}
			}
			this.neededLayers = list.ToArray();
		}

		public string name;

		[Tooltip("Highest wins")]
		public int priority;

		[ReadOnly]
		public float lastEligibleTime;

		public bool isLocked;

		public MusicSystemDynamicStates.DynamicState.LayerSetting[] layerSettings;

		public MusicSystemDynamicStates.DynamicState.DynamicStateVariant[] variants;

		private int[] neededLayers;

		[Serializable]
		public class LayerSetting
		{
			// Token: 0x060000AA RID: 170 RVA: 0x0001A084 File Offset: 0x00018284
			public void FindLayerIndex(MusicSystem musicSystem)
			{
				for (int i = 0; i < musicSystem.layers.Length; i++)
				{
					if (musicSystem.layers[i].name == this.layerName)
					{
						this.layerIndex = i;
						return;
					}
				}
				this.layerIndex = -1;
			}

			[MusicLayerLookup("musicSystem")]
			public string layerName;

			[ReadOnly]
			public int layerIndex;

			[Range(0f, 1f)]
			public float volume = 1f;
		}

		[Serializable]
		public class DynamicStateVariant
		{
			[HideInInspector]
			public string name;

			public MusicSystemDynamicStates.VariantStates variant;

			public MusicSystemDynamicStates.DynamicState.LayerSetting[] layerSettings;
		}
	}
}
