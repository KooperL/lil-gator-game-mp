using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

// Token: 0x02000025 RID: 37
[AddComponentMenu("Music/MusicSystem")]
public class MusicSystem : MonoBehaviour
{
	// Token: 0x17000002 RID: 2
	// (get) Token: 0x0600007E RID: 126 RVA: 0x00003DB3 File Offset: 0x00001FB3
	public bool IsEligible
	{
		get
		{
			return this.isLocked || this.eligibleTime + 0.5f > Time.time;
		}
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00003DD2 File Offset: 0x00001FD2
	private void OnValidate()
	{
		this.loopEndTime = 60.0 / (double)this.bpm * (double)this.loopEndBeats;
		this.loopSkipTime = 60.0 / (double)this.bpm * (double)this.loopStartBeats;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00003E12 File Offset: 0x00002012
	private void Awake()
	{
		base.transform.parent = null;
		Object.DontDestroyOnLoad(base.gameObject);
		this.nativeScene = SceneManager.GetActiveScene();
		SceneManager.sceneUnloaded += this.OnSceneUnload;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00003E47 File Offset: 0x00002047
	private void Start()
	{
		this.nativeScene = SceneManager.GetActiveScene();
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00003E54 File Offset: 0x00002054
	private void OnSceneUnload(Scene unloadedScene)
	{
		if (unloadedScene == this.nativeScene)
		{
			this.isUnloading = true;
		}
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00003E6C File Offset: 0x0000206C
	private void OnEnable()
	{
		if (this.oldSources == null)
		{
			this.oldSources = new List<AudioSource>();
		}
		this.loopEndTime = 60.0 / (double)this.bpm * (double)this.loopEndBeats;
		this.loopStartTime = (double)(Time.time + 0.25f);
		this.masterWeight = (this.masterWeightSmooth = 0f);
		if (!MusicSystem.current.Contains(this))
		{
			MusicSystem.current.Add(this);
		}
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00003EE9 File Offset: 0x000020E9
	private void OnDestroy()
	{
		if (MusicSystem.current.Contains(this))
		{
			MusicSystem.current.Remove(this);
		}
		SceneManager.sceneUnloaded -= this.OnSceneUnload;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00003F18 File Offset: 0x00002118
	public void CueStartPlaying(float cueDelay)
	{
		if (this.isPlaying)
		{
			return;
		}
		this.isPlaying = true;
		this.isReadyToPlay = true;
		this.rightBeforePlaying.Invoke();
		for (int i = 0; i < this.layers.Length; i++)
		{
			this.layers[i].smoothWeight = this.layers[i].GetWeight();
			this.layers[i].weightVelocity = 0f;
			if (this.layers[i].smoothWeight > 0f && !this.layers[i].IsReadyToPlay())
			{
				this.isReadyToPlay = false;
			}
		}
		double dspTime = AudioSettings.dspTime;
		this.masterWeightSmooth = this.masterWeight;
		this.loopStartTime = dspTime + (double)cueDelay;
		this.nextLoopTime = this.loopStartTime + this.loopEndTime;
		this.isFirstLoop = true;
		for (int j = 0; j < this.layers.Length; j++)
		{
			this.layers[j].smoothWeight = this.layers[j].GetWeight();
			this.layers[j].weightVelocity = 0f;
			if (this.layers[j].smoothWeight > 0f)
			{
				if (this.stopClipsAtEndOfLoop)
				{
					this.layers[j].StartMusic(this.musicSourcePrefab, base.transform, this.loopStartTime, this.loopEndTime);
				}
				else
				{
					this.layers[j].StartMusic(this.musicSourcePrefab, base.transform, this.loopStartTime, 0.0);
				}
			}
		}
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00004098 File Offset: 0x00002298
	public void StartPlaying()
	{
		if (this.isPlaying)
		{
			return;
		}
		this.isPlaying = true;
		this.isReadyToPlay = true;
		this.rightBeforePlaying.Invoke();
		for (int i = 0; i < this.layers.Length; i++)
		{
			this.layers[i].smoothWeight = this.layers[i].GetWeight();
			this.layers[i].weightVelocity = 0f;
			if (this.layers[i].smoothWeight > 0f && !this.layers[i].IsReadyToPlay())
			{
				this.isReadyToPlay = false;
			}
		}
		if (this.isReadyToPlay)
		{
			this.StartPlayingNow();
		}
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00004140 File Offset: 0x00002340
	private void StartPlayingNow()
	{
		double dspTime = AudioSettings.dspTime;
		this.masterWeightSmooth = this.masterWeight;
		this.loopStartTime = dspTime + 0.25;
		this.nextLoopTime = this.loopStartTime + this.loopEndTime;
		this.isFirstLoop = true;
		for (int i = 0; i < this.layers.Length; i++)
		{
			this.layers[i].smoothWeight = this.layers[i].GetWeight();
			this.layers[i].weightVelocity = 0f;
			if (this.layers[i].smoothWeight > 0f)
			{
				if (this.stopClipsAtEndOfLoop)
				{
					this.layers[i].StartMusic(this.musicSourcePrefab, base.transform, this.loopStartTime, this.loopEndTime);
				}
				else
				{
					this.layers[i].StartMusic(this.musicSourcePrefab, base.transform, this.loopStartTime, 0.0);
				}
			}
		}
	}

	// Token: 0x06000088 RID: 136 RVA: 0x0000423C File Offset: 0x0000243C
	private void TryToUnload()
	{
		if (this.masterWeightSmooth > 1E-05f)
		{
			return;
		}
		foreach (MusicSystem.MusicLayer musicLayer in this.layers)
		{
			if (musicLayer.layerSource != null && musicLayer.layerSource.volume > 1E-05f)
			{
				return;
			}
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000089 RID: 137 RVA: 0x0000429C File Offset: 0x0000249C
	private void Update()
	{
		float num = Mathf.Min(Time.unscaledDeltaTime, 0.05f);
		if (this.isUnloading)
		{
			this.masterWeight = 0f;
			this.TryToUnload();
		}
		double dspTime = AudioSettings.dspTime;
		for (int i = 0; i < this.oldSources.Count; i++)
		{
			if (this.oldSources[i] == null || !this.oldSources[i].isPlaying)
			{
				if (this.oldSources[i] != null)
				{
					Object.Destroy(this.oldSources[i].gameObject);
				}
				this.oldSources.RemoveAt(i);
				i--;
			}
		}
		if (this.masterWeightSmooth > 1E-05f || this.masterWeight > 1E-05f)
		{
			this.masterWeightSmooth = Mathf.MoveTowards(this.masterWeightSmooth, this.masterWeight, 0.2f * num / (this.masterWeightSmoothTimeMultiplier * this.masterWeightSmoothTime));
			if (this.masterWeight == 0f && this.masterWeightSmooth < 1E-05f)
			{
				this.masterWeightSmooth = 0f;
				this.masterWeightVelocity = 0f;
			}
			if (!this.isPlaying && this.masterWeight > 0f && this.masterWeightSmooth > this.masterWeight * 0.5f && dspTime > this.loopStartTime)
			{
				this.StartPlaying();
			}
		}
		if (!this.isPlaying)
		{
			return;
		}
		if (!this.isReadyToPlay)
		{
			this.isReadyToPlay = true;
			for (int j = 0; j < this.layers.Length; j++)
			{
				if (this.layers[j].smoothWeight > 0f && !this.layers[j].IsReadyToPlay())
				{
					this.isReadyToPlay = false;
				}
			}
			if (!this.isReadyToPlay)
			{
				return;
			}
			this.StartPlayingNow();
		}
		if (dspTime > this.nextLoopTime)
		{
			this.loopStartTime = this.nextLoopTime;
			this.nextLoopTime += this.loopEndTime - this.loopSkipTime;
			this.isFirstLoop = false;
			for (int k = 0; k < this.layers.Length; k++)
			{
				if (this.layers[k].layerSource != null)
				{
					this.oldSources.Add(this.layers[k].layerSource);
				}
				if (this.layers[k].cuedLayerSource != null)
				{
					this.layers[k].layerSource = this.layers[k].cuedLayerSource;
					this.layers[k].cuedLayerSource = null;
				}
			}
		}
		bool flag = false;
		foreach (MusicSystem.MusicLayer musicLayer in this.layers)
		{
			if ((musicLayer.weight > 0f || musicLayer.smoothWeight > 0f) && !musicLayer.IsReadyToPlay())
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			MusicSystem.MusicLayer[] array = this.layers;
			int l = 0;
			while (l < array.Length && array[l].IsReadyToPlay())
			{
				l++;
			}
		}
		for (int m = 0; m < this.layers.Length; m++)
		{
			if (this.layers[m].isPlaying && !flag)
			{
				this.layers[m].smoothWeight = Mathf.MoveTowards(this.layers[m].smoothWeight, this.layers[m].GetWeight(), num / this.weightSmoothTime);
			}
			if (!this.layers[m].isPlaying && dspTime > this.layers[m].scheduledTime && this.layers[m].layerSource != null)
			{
				this.layers[m].isPlaying = true;
			}
			if (this.layers[m].smoothWeight > 5E-06f || this.layers[m].weight > 0f)
			{
				if (this.layers[m].IsReadyToPlay())
				{
					if (dspTime + 0.25 > this.nextLoopTime)
					{
						if (this.layers[m].cuedLayerSource == null)
						{
							if (this.stopClipsAtEndOfLoop)
							{
								this.layers[m].CueMusic(this.musicSourcePrefab, base.transform, this.nextLoopTime, this.loopSkipTime, this.loopEndTime);
							}
							else
							{
								this.layers[m].CueMusic(this.musicSourcePrefab, base.transform, this.nextLoopTime, this.loopSkipTime, 0.0);
							}
						}
					}
					else if (this.allowClipsAddedMidloop && this.layers[m].layerSource == null && this.layers[m].audioClip.loadState == AudioDataLoadState.Loaded)
					{
						this.layers[m].layerSource = Object.Instantiate<GameObject>(this.musicSourcePrefab, base.transform).GetComponent<AudioSource>();
						Object.DontDestroyOnLoad(this.layers[m].layerSource.gameObject);
						this.layers[m].layerSource.clip = this.layers[m].audioClip;
						double num2 = dspTime + 0.25 - this.loopStartTime;
						if (!this.isFirstLoop)
						{
							num2 += this.loopSkipTime;
						}
						this.layers[m].layerSource.timeSamples = (int)(num2 * (double)this.layers[m].audioClip.frequency);
						this.layers[m].layerSource.PlayScheduled(dspTime + 0.25);
						this.layers[m].scheduledTime = dspTime + 0.25;
					}
					float num3 = this.masterWeightSmooth * this.layers[m].smoothWeight * this.systemVolume;
					if (this.layers[m].layerSource != null)
					{
						this.layers[m].layerSource.volume = num3;
					}
					if (this.layers[m].cuedLayerSource != null)
					{
						this.layers[m].cuedLayerSource.volume = num3;
					}
				}
			}
			else
			{
				this.layers[m].ClearSource();
				if (this.layers[m].cuedLayerSource != null)
				{
					Object.Destroy(this.layers[m].cuedLayerSource.gameObject);
				}
				this.layers[m].isPlaying = false;
			}
		}
		if (this.masterWeightSmooth == 0f && this.masterWeight == 0f)
		{
			for (int n = 0; n < this.layers.Length; n++)
			{
				this.layers[n].ClearSource();
				this.layers[n].Unload();
				if (this.layers[n].cuedLayerSource != null)
				{
					Object.Destroy(this.layers[n].cuedLayerSource.gameObject);
				}
			}
			this.isPlaying = false;
		}
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00004998 File Offset: 0x00002B98
	public float GetCurrentBeatT()
	{
		double dspTime = AudioSettings.dspTime;
		if (!this.isPlaying || dspTime < this.loopStartTime)
		{
			return -1f;
		}
		return (float)((dspTime - this.loopStartTime) / (60.0 / (double)this.bpm) % 1.0);
	}

	// Token: 0x0600008B RID: 139 RVA: 0x000049E8 File Offset: 0x00002BE8
	public bool PrepareStateChange(int[] neededLayers)
	{
		bool flag = true;
		foreach (int num in neededLayers)
		{
			if (this.layers[num].IsReadyToPlay())
			{
				flag = false;
			}
		}
		return flag;
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00004A20 File Offset: 0x00002C20
	public void ClearLayerWeights()
	{
		for (int i = 0; i < this.layers.Length; i++)
		{
			this.layers[i].weight = 0f;
		}
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00004A52 File Offset: 0x00002C52
	public void MarkEligible()
	{
		this.eligibleTime = Time.time;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00004A60 File Offset: 0x00002C60
	public void ChangeLoop(int newStartBeats, int newEndBeats, MusicSystem.LoopChangeBehavior changeBehavior)
	{
		double num = 60.0 / (double)this.bpm * (double)newEndBeats;
		double num2 = 60.0 / (double)this.bpm * (double)newStartBeats;
		double dspTime = AudioSettings.dspTime;
		double num3 = this.nextLoopTime;
		switch (changeBehavior)
		{
		case MusicSystem.LoopChangeBehavior.Immediate:
			num3 = dspTime + 0.25;
			break;
		case MusicSystem.LoopChangeBehavior.EndOfMeasure:
		{
			double num4 = 60.0 / (double)this.bpm * (double)this.beatsPerMeasure;
			double num5 = (dspTime - this.loopStartTime) % num4;
			num3 = dspTime + (num4 - num5);
			break;
		}
		case MusicSystem.LoopChangeBehavior.Continue:
			num3 = this.nextLoopTime - this.loopEndTime + num;
			break;
		}
		this.loopEndBeats = newEndBeats;
		this.loopEndTime = num;
		this.loopStartBeats = newStartBeats;
		this.loopSkipTime = num2;
		foreach (MusicSystem.MusicLayer musicLayer in this.layers)
		{
			if (musicLayer.cuedLayerSource != null)
			{
				musicLayer.CueMusic(this.musicSourcePrefab, base.transform, num3, this.loopStartTime, this.loopEndTime);
			}
		}
		if (Math.Abs(this.nextLoopTime - num3) > 0.01)
		{
			this.nextLoopTime = num3;
			MusicSystem.MusicLayer[] array = this.layers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].UpdateEndTime(this.nextLoopTime);
			}
		}
	}

	// Token: 0x040000A4 RID: 164
	public static List<MusicSystem> current = new List<MusicSystem>();

	// Token: 0x040000A5 RID: 165
	public int priority;

	// Token: 0x040000A6 RID: 166
	public float eligibleTime = -100f;

	// Token: 0x040000A7 RID: 167
	public bool isLocked;

	// Token: 0x040000A8 RID: 168
	private Scene nativeScene;

	// Token: 0x040000A9 RID: 169
	[ReadOnly]
	public bool isUnloading;

	// Token: 0x040000AA RID: 170
	public MusicSystem.MusicLayer[] layers;

	// Token: 0x040000AB RID: 171
	public GameObject musicSourcePrefab;

	// Token: 0x040000AC RID: 172
	public float bpm = 160f;

	// Token: 0x040000AD RID: 173
	public int beatsPerMeasure = 4;

	// Token: 0x040000AE RID: 174
	[FormerlySerializedAs("totalBeats")]
	public int loopEndBeats = 64;

	// Token: 0x040000AF RID: 175
	[FormerlySerializedAs("loopSkipBeats")]
	public int loopStartBeats;

	// Token: 0x040000B0 RID: 176
	[ReadOnly]
	public double loopEndTime;

	// Token: 0x040000B1 RID: 177
	[ReadOnly]
	public double loopSkipTime;

	// Token: 0x040000B2 RID: 178
	public bool stopClipsAtEndOfLoop;

	// Token: 0x040000B3 RID: 179
	public float beatSyncMultiplier = 1f;

	// Token: 0x040000B4 RID: 180
	private bool isFirstLoop;

	// Token: 0x040000B5 RID: 181
	private double loopStartTime;

	// Token: 0x040000B6 RID: 182
	private double nextLoopTime;

	// Token: 0x040000B7 RID: 183
	public bool isPlaying;

	// Token: 0x040000B8 RID: 184
	[Range(0f, 1f)]
	public float systemVolume = 1f;

	// Token: 0x040000B9 RID: 185
	[Range(0f, 1f)]
	public float masterWeight;

	// Token: 0x040000BA RID: 186
	[ReadOnly]
	public float masterWeightSmooth;

	// Token: 0x040000BB RID: 187
	[ReadOnly]
	public float masterWeightVelocity;

	// Token: 0x040000BC RID: 188
	[Range(0.0001f, 5f)]
	public float weightSmoothTime = 1f;

	// Token: 0x040000BD RID: 189
	[Range(0.0001f, 5f)]
	public float masterWeightSmoothTime = 1f;

	// Token: 0x040000BE RID: 190
	public float masterWeightSmoothTimeMultiplier = 1f;

	// Token: 0x040000BF RID: 191
	public List<AudioSource> oldSources;

	// Token: 0x040000C0 RID: 192
	private const float cueTime = 0.25f;

	// Token: 0x040000C1 RID: 193
	public bool allowClipsAddedMidloop = true;

	// Token: 0x040000C2 RID: 194
	public bool isSilence;

	// Token: 0x040000C3 RID: 195
	public UnityEvent rightBeforePlaying;

	// Token: 0x040000C4 RID: 196
	private bool isReadyToPlay;

	// Token: 0x040000C5 RID: 197
	private const float volumeCutoff = 1E-05f;

	// Token: 0x02000352 RID: 850
	[Serializable]
	public class MusicLayer
	{
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x00066718 File Offset: 0x00064918
		private static bool CanLoad
		{
			get
			{
				return !MusicSystem.MusicLayer.isSomethingLoading;
			}
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00066722 File Offset: 0x00064922
		public float GetWeight()
		{
			return this.weight;
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0006672A File Offset: 0x0006492A
		public void ClearSource()
		{
			if (this.layerSource != null)
			{
				Object.Destroy(this.layerSource.gameObject);
			}
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x0006674C File Offset: 0x0006494C
		public void Unload()
		{
			if (this.isLoadingAudioClip)
			{
				MusicSystem.MusicLayer.isSomethingLoading = false;
			}
			if (this.isLoadingAudioClip || this.isLoadDependent)
			{
				this.audioClip.UnloadAudioData();
			}
			if (this.isAudioClipAddressable)
			{
				this.audioClip = null;
				this.addressableClip.ReleaseAsset();
			}
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x000667A0 File Offset: 0x000649A0
		public void StartMusic(GameObject musicSourcePrefab, Transform parent, double cuedTime, double endTime = 0.0)
		{
			this.layerSource = Object.Instantiate<GameObject>(musicSourcePrefab, parent).GetComponent<AudioSource>();
			Object.DontDestroyOnLoad(this.layerSource.gameObject);
			this.layerSource.clip = this.audioClip;
			this.layerSource.PlayScheduled(cuedTime);
			if (endTime != 0.0)
			{
				this.layerSource.SetScheduledEndTime(cuedTime + endTime + 0.05);
			}
			this.isPlaying = true;
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x0006681C File Offset: 0x00064A1C
		public void CueMusic(GameObject musicSourcePrefab, Transform parent, double cuedTime, double startTime = 0.0, double endTime = 0.0)
		{
			if (this.cuedLayerSource == null)
			{
				this.cuedLayerSource = Object.Instantiate<GameObject>(musicSourcePrefab, parent).GetComponent<AudioSource>();
				Object.DontDestroyOnLoad(this.cuedLayerSource.gameObject);
				this.cuedLayerSource.clip = this.audioClip;
				this.cuedLayerSource.timeSamples = (int)(startTime * (double)this.audioClip.frequency);
				this.cuedLayerSource.PlayScheduled(cuedTime);
				if (endTime != 0.0)
				{
					this.cuedLayerSource.SetScheduledEndTime(cuedTime + (endTime - startTime) + 0.05);
					return;
				}
			}
			else
			{
				this.cuedLayerSource.timeSamples = (int)(startTime * (double)this.audioClip.frequency);
				this.cuedLayerSource.SetScheduledStartTime(cuedTime);
				if (endTime != 0.0)
				{
					this.cuedLayerSource.SetScheduledEndTime(cuedTime + (endTime - startTime) + 0.05);
				}
			}
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x0006690E File Offset: 0x00064B0E
		public void UpdateEndTime(double absoluteEndTime)
		{
			this.layerSource.SetScheduledEndTime(absoluteEndTime + 0.05);
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x00066928 File Offset: 0x00064B28
		public bool IsReadyToPlay()
		{
			if (this.audioClip == null && !this.isLoadingAsset && this.addressableClip.RuntimeKeyIsValid())
			{
				this.isAudioClipAddressable = true;
				this.isLoadingAsset = true;
				this.addressableClip.LoadAssetAsync<AudioClip>().Completed += this.OnLoadFinished;
			}
			if (this.isLoadingAsset)
			{
				return false;
			}
			if (this.audioClip == null)
			{
				return true;
			}
			switch (this.audioClip.loadState)
			{
			case AudioDataLoadState.Unloaded:
				if (MusicSystem.MusicLayer.CanLoad)
				{
					MusicSystem.MusicLayer.isSomethingLoading = true;
					this.isLoadingAudioClip = true;
					this.isLoadDependent = true;
					this.audioClip.LoadAudioData();
				}
				return false;
			case AudioDataLoadState.Loading:
				return false;
			case AudioDataLoadState.Loaded:
				if (this.isLoadingAudioClip)
				{
					this.isLoadingAudioClip = false;
					MusicSystem.MusicLayer.isSomethingLoading = false;
				}
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x00066A02 File Offset: 0x00064C02
		private void OnLoadFinished(AsyncOperationHandle<AudioClip> obj)
		{
			if (this.isLoadingAsset)
			{
				this.isLoadingAsset = false;
				this.audioClip = obj.Result;
				return;
			}
			this.addressableClip.ReleaseAsset();
		}

		// Token: 0x040019CC RID: 6604
		private static bool isSomethingLoading;

		// Token: 0x040019CD RID: 6605
		private const bool allowMultipleLoading = false;

		// Token: 0x040019CE RID: 6606
		public string name;

		// Token: 0x040019CF RID: 6607
		[ReadOnly]
		public bool isPlaying;

		// Token: 0x040019D0 RID: 6608
		public AudioClip audioClip;

		// Token: 0x040019D1 RID: 6609
		public AssetReferenceT<AudioClip> addressableClip;

		// Token: 0x040019D2 RID: 6610
		[Range(0f, 1f)]
		public float weight;

		// Token: 0x040019D3 RID: 6611
		[HideInInspector]
		public float smoothWeight;

		// Token: 0x040019D4 RID: 6612
		[HideInInspector]
		public float weightVelocity;

		// Token: 0x040019D5 RID: 6613
		[HideInInspector]
		public AudioSource layerSource;

		// Token: 0x040019D6 RID: 6614
		[HideInInspector]
		public AudioSource cuedLayerSource;

		// Token: 0x040019D7 RID: 6615
		[HideInInspector]
		public double scheduledTime;

		// Token: 0x040019D8 RID: 6616
		[ReadOnly]
		public bool isLoadDependent;

		// Token: 0x040019D9 RID: 6617
		private const double endTimeBuffer = 0.05;

		// Token: 0x040019DA RID: 6618
		private bool isLoadingAudioClip;

		// Token: 0x040019DB RID: 6619
		private bool isAudioClipAddressable;

		// Token: 0x040019DC RID: 6620
		private bool isLoadingAsset;
	}

	// Token: 0x02000353 RID: 851
	public enum LoadingBehavior
	{
		// Token: 0x040019DE RID: 6622
		LoadAll,
		// Token: 0x040019DF RID: 6623
		LoadNeededFirst,
		// Token: 0x040019E0 RID: 6624
		LoadOnlyNeeded,
		// Token: 0x040019E1 RID: 6625
		PlayAsLoaded
	}

	// Token: 0x02000354 RID: 852
	public enum UnloadingBehavior
	{
		// Token: 0x040019E3 RID: 6627
		DontUnload,
		// Token: 0x040019E4 RID: 6628
		UnloadNotNeeded
	}

	// Token: 0x02000355 RID: 853
	public enum LoopChangeBehavior
	{
		// Token: 0x040019E6 RID: 6630
		Immediate,
		// Token: 0x040019E7 RID: 6631
		EndOfLoop,
		// Token: 0x040019E8 RID: 6632
		EndOfMeasure,
		// Token: 0x040019E9 RID: 6633
		Continue
	}
}
