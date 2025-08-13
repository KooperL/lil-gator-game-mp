using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002A RID: 42
[AddComponentMenu("Music/MusicSystem Dynamic States")]
public class MusicSystemDynamicStates : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000095 RID: 149 RVA: 0x000194B0 File Offset: 0x000176B0
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

	// Token: 0x06000096 RID: 150 RVA: 0x00019588 File Offset: 0x00017788
	private void Awake()
	{
		MusicSystemDynamicStates.DynamicState[] array = this.dynamicStates;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Initialize(this.musicSystem);
		}
	}

	// Token: 0x06000097 RID: 151 RVA: 0x000195B8 File Offset: 0x000177B8
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

	// Token: 0x06000098 RID: 152 RVA: 0x0000285D File Offset: 0x00000A5D
	private void OnDisable()
	{
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x06000099 RID: 153 RVA: 0x0001961C File Offset: 0x0001781C
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

	// Token: 0x0600009A RID: 154 RVA: 0x00019698 File Offset: 0x00017898
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

	// Token: 0x0600009B RID: 155 RVA: 0x00019744 File Offset: 0x00017944
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

	// Token: 0x0600009C RID: 156 RVA: 0x0000286B File Offset: 0x00000A6B
	public void MarkStateEligible(int index)
	{
		this.dynamicStates[index].lastEligibleTime = Time.time;
	}

	// Token: 0x040000D6 RID: 214
	public MusicSystem musicSystem;

	// Token: 0x040000D7 RID: 215
	public bool ignoreMusicLayers;

	// Token: 0x040000D8 RID: 216
	public MusicSystemDynamicStates.DynamicState[] dynamicStates;

	// Token: 0x040000D9 RID: 217
	public int currentState;

	// Token: 0x040000DA RID: 218
	public MusicSystemDynamicStates.VariantStates currentVariant = MusicSystemDynamicStates.VariantStates.Active;

	// Token: 0x040000DB RID: 219
	private float isPlayerActiveSmooth = 1f;

	// Token: 0x040000DC RID: 220
	public bool forceUpdate;

	// Token: 0x040000DD RID: 221
	private const float stateEligibilityDelay = 1f;

	// Token: 0x0200002B RID: 43
	public enum VariantStates
	{
		// Token: 0x040000DF RID: 223
		Stationary,
		// Token: 0x040000E0 RID: 224
		Active,
		// Token: 0x040000E1 RID: 225
		Dialogue = 10
	}

	// Token: 0x0200002C RID: 44
	[Serializable]
	public class DynamicState
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002899 File Offset: 0x00000A99
		public bool IsEligible
		{
			get
			{
				return this.isLocked || this.lastEligibleTime + 1f > Time.time;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000028B8 File Offset: 0x00000AB8
		public int[] GetNeededLayers()
		{
			return this.neededLayers;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0001977C File Offset: 0x0001797C
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

		// Token: 0x040000E2 RID: 226
		public string name;

		// Token: 0x040000E3 RID: 227
		[Tooltip("Highest wins")]
		public int priority;

		// Token: 0x040000E4 RID: 228
		[ReadOnly]
		public float lastEligibleTime;

		// Token: 0x040000E5 RID: 229
		public bool isLocked;

		// Token: 0x040000E6 RID: 230
		public MusicSystemDynamicStates.DynamicState.LayerSetting[] layerSettings;

		// Token: 0x040000E7 RID: 231
		public MusicSystemDynamicStates.DynamicState.DynamicStateVariant[] variants;

		// Token: 0x040000E8 RID: 232
		private int[] neededLayers;

		// Token: 0x0200002D RID: 45
		[Serializable]
		public class LayerSetting
		{
			// Token: 0x060000A2 RID: 162 RVA: 0x00019848 File Offset: 0x00017A48
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

			// Token: 0x040000E9 RID: 233
			[MusicLayerLookup("musicSystem")]
			public string layerName;

			// Token: 0x040000EA RID: 234
			[ReadOnly]
			public int layerIndex;

			// Token: 0x040000EB RID: 235
			[Range(0f, 1f)]
			public float volume = 1f;
		}

		// Token: 0x0200002E RID: 46
		[Serializable]
		public class DynamicStateVariant
		{
			// Token: 0x040000EC RID: 236
			[HideInInspector]
			public string name;

			// Token: 0x040000ED RID: 237
			public MusicSystemDynamicStates.VariantStates variant;

			// Token: 0x040000EE RID: 238
			public MusicSystemDynamicStates.DynamicState.LayerSetting[] layerSettings;
		}
	}
}
