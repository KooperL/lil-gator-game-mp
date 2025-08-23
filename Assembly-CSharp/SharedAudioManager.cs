using System;
using System.Collections.Generic;
using UnityEngine;

public class SharedAudioManager : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000242 RID: 578 RVA: 0x00003DD0 File Offset: 0x00001FD0
	private void OnEnable()
	{
		SharedAudioManager.nearbyAudioSources = new List<ISharedAudioSource>();
		this.audioListener = global::UnityEngine.Object.FindObjectOfType<AudioListener>();
		this.activeSources = new Dictionary<SharedAudioProfile, SharedAudioManager.ActiveAudioSource>();
		FastUpdateManager.updateEvery4.Add(this);
	}

	// Token: 0x06000243 RID: 579 RVA: 0x000026CE File Offset: 0x000008CE
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
	}

	// Token: 0x06000244 RID: 580 RVA: 0x0001F4A4 File Offset: 0x0001D6A4
	public void ManagedUpdate()
	{
		Vector3 position = base.transform.position;
		this.outOfRangeList.Clear();
		foreach (SharedAudioManager.ActiveAudioSource activeAudioSource in this.activeSources.Values)
		{
			activeAudioSource.hasSource = false;
			activeAudioSource.directions.Clear();
			activeAudioSource.strengths.Clear();
		}
		foreach (ISharedAudioSource sharedAudioSource in SharedAudioManager.nearbyAudioSources)
		{
			if (sharedAudioSource == null)
			{
				this.outOfRangeList.Add(sharedAudioSource);
			}
			else
			{
				SharedAudioProfile sharedAudioProfile;
				Vector3 vector;
				float num;
				sharedAudioSource.GetAudioData(position, out sharedAudioProfile, out vector, out num);
				if (num == 0f)
				{
					this.outOfRangeList.Add(sharedAudioSource);
				}
				else
				{
					if (!this.activeSources.ContainsKey(sharedAudioProfile))
					{
						this.AddActiveSource(sharedAudioProfile);
					}
					this.activeSources[sharedAudioProfile].hasSource = true;
					this.activeSources[sharedAudioProfile].directions.Add(vector);
					this.activeSources[sharedAudioProfile].strengths.Add(num);
				}
			}
		}
		foreach (ISharedAudioSource sharedAudioSource2 in this.outOfRangeList)
		{
			SharedAudioManager.nearbyAudioSources.Remove(sharedAudioSource2);
			if (sharedAudioSource2 != null)
			{
				sharedAudioSource2.WasRemoved();
			}
		}
		this.activeSourcesToRemove.Clear();
		foreach (KeyValuePair<SharedAudioProfile, SharedAudioManager.ActiveAudioSource> keyValuePair in this.activeSources)
		{
			if (keyValuePair.Key != null && keyValuePair.Value != null && keyValuePair.Value.audioSource != null && (keyValuePair.Value.hasSource || keyValuePair.Value.audioSource.volume > 0f))
			{
				Vector3 vector2 = Vector3.zero;
				float num2 = 0f;
				float num3 = 0f;
				for (int i = 0; i < keyValuePair.Value.directions.Count; i++)
				{
					vector2 += keyValuePair.Value.strengths[i] * keyValuePair.Value.directions[i];
					num2 += keyValuePair.Value.strengths[i];
					num3 = Mathf.Max(num3, keyValuePair.Value.strengths[i]);
				}
				if (num2 > 0f)
				{
					vector2 /= num2;
				}
				float num4 = 1f - vector2.magnitude;
				keyValuePair.Value.audioSource.volume = Mathf.MoveTowards(keyValuePair.Value.audioSource.volume, keyValuePair.Key.volume * Mathf.Pow(num3, 3f), 1.5f * Time.deltaTime * 4f);
				keyValuePair.Value.audioSource.spatialBlend = Mathf.Max(keyValuePair.Key.spacialBlend * (1f - num3), num4);
				keyValuePair.Value.transform.position = base.transform.position + 5f * vector2;
			}
			else
			{
				this.activeSourcesToRemove.Add(keyValuePair.Key);
			}
		}
		foreach (SharedAudioProfile sharedAudioProfile2 in this.activeSourcesToRemove)
		{
			global::UnityEngine.Object.Destroy(this.activeSources[sharedAudioProfile2].gameObject);
			this.activeSources.Remove(sharedAudioProfile2);
		}
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0001F91C File Offset: 0x0001DB1C
	private void AddActiveSource(SharedAudioProfile profile)
	{
		SharedAudioManager.ActiveAudioSource activeAudioSource = new SharedAudioManager.ActiveAudioSource();
		activeAudioSource.gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.audioSourcePrefab);
		activeAudioSource.gameObject.name = string.Format("Shared Audio Source ({0})", profile.name);
		activeAudioSource.transform = activeAudioSource.gameObject.transform;
		activeAudioSource.audioSource = activeAudioSource.gameObject.GetComponent<AudioSource>();
		activeAudioSource.directions = new List<Vector3>();
		activeAudioSource.strengths = new List<float>();
		activeAudioSource.audioSource.clip = profile.audioClip;
		activeAudioSource.audioSource.pitch = profile.pitch;
		activeAudioSource.audioSource.volume = 0f;
		activeAudioSource.audioSource.Play();
		this.activeSources.Add(profile, activeAudioSource);
	}

	public static List<ISharedAudioSource> nearbyAudioSources = new List<ISharedAudioSource>();

	public GameObject audioSourcePrefab;

	private Dictionary<SharedAudioProfile, SharedAudioManager.ActiveAudioSource> activeSources;

	private AudioListener audioListener;

	private List<ISharedAudioSource> outOfRangeList = new List<ISharedAudioSource>();

	private List<SharedAudioProfile> activeSourcesToRemove = new List<SharedAudioProfile>();

	private class ActiveAudioSource
	{
		public GameObject gameObject;

		public Transform transform;

		public AudioSource audioSource;

		public bool hasSource;

		public List<Vector3> directions;

		public List<float> strengths;
	}
}
