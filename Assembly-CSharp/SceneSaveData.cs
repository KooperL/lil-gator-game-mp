using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002F0 RID: 752
public class SceneSaveData : MonoBehaviour
{
	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0000CED2 File Offset: 0x0000B0D2
	// (set) Token: 0x06000ECF RID: 3791 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
	public static SceneSaveData s
	{
		get
		{
			if (SceneSaveData.instance == null)
			{
				SceneSaveData.instance = Object.FindObjectOfType<SceneSaveData>();
			}
			return SceneSaveData.instance;
		}
		set
		{
			SceneSaveData.instance = value;
		}
	}

	// Token: 0x06000ED0 RID: 3792 RVA: 0x0000CEF8 File Offset: 0x0000B0F8
	private void OnEnable()
	{
		SceneSaveData.s = this;
	}

	// Token: 0x06000ED1 RID: 3793 RVA: 0x0000CF00 File Offset: 0x0000B100
	private void OnDisable()
	{
		if (SceneSaveData.s == this)
		{
			SceneSaveData.s = null;
		}
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x0000CF15 File Offset: 0x0000B115
	private void Start()
	{
		this.AssignIDs();
		this.Load();
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x0004E1B4 File Offset: 0x0004C3B4
	private void Load()
	{
		if (GameData.g.gameSaveData == null || this.isLoaded)
		{
			return;
		}
		this.objectStates = GameData.g.gameSaveData.objectStates;
		if (this.objectStates.Length != this.persistentObjects.Length)
		{
			bool[] array = new bool[this.persistentObjects.Length];
			Array.ConstrainedCopy(this.objectStates, 0, array, 0, Mathf.Min(this.objectStates.Length, array.Length));
			this.objectStates = (GameData.g.gameSaveData.objectStates = array);
		}
		this.LoadPersistentObjects();
		this.isLoaded = true;
	}

	// Token: 0x06000ED4 RID: 3796 RVA: 0x0004E250 File Offset: 0x0004C450
	public void LoadPersistentObjects()
	{
		for (int i = 0; i < this.persistentObjects.Length; i++)
		{
			if (this.persistentObjects[i] != null)
			{
				if (this.objectStates.Length > i)
				{
					this.persistentObjects[i].Load(this.objectStates[i]);
					this.persistentObjects[i].isLoaded = true;
				}
				else
				{
					this.persistentObjects[i].Load(false);
				}
			}
		}
	}

	// Token: 0x06000ED5 RID: 3797 RVA: 0x0004E2C0 File Offset: 0x0004C4C0
	public void SavePersistentObject(int id, bool state)
	{
		if (this.objectStates.Length <= id)
		{
			bool[] array = new bool[id + 1];
			this.objectStates.CopyTo(array, 0);
			this.objectStates = (GameData.g.gameSaveData.objectStates = array);
		}
		this.objectStates[id] = state;
	}

	// Token: 0x06000ED6 RID: 3798 RVA: 0x0000CF23 File Offset: 0x0000B123
	public bool GetPersistentObject(int id)
	{
		if (!this.isLoaded)
		{
			this.Load();
		}
		return this.objectStates != null && this.objectStates.Length > id && this.objectStates[id];
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x0004E310 File Offset: 0x0004C510
	public bool HasPersistentObject(PersistentObject o)
	{
		if (this.persistentObjects != null)
		{
			for (int i = 0; i < this.persistentObjects.Length; i++)
			{
				if (this.persistentObjects[i] == o)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x0004E34C File Offset: 0x0004C54C
	public void RegisterPersistentObject(PersistentObject o)
	{
		if (Application.isPlaying)
		{
			Debug.LogError("Do not invoke in play mode!");
			return;
		}
		if (this.HasPersistentObject(o))
		{
			return;
		}
		if (this.persistentObjects == null)
		{
			this.persistentObjects = new PersistentObject[] { o };
			return;
		}
		Array.Resize<PersistentObject>(ref this.persistentObjects, this.persistentObjects.Length + 1);
		this.persistentObjects[this.persistentObjects.Length - 1] = o;
		o.id = this.persistentObjects.Length - 1;
	}

	// Token: 0x06000ED9 RID: 3801 RVA: 0x0004E3C8 File Offset: 0x0004C5C8
	[ContextMenu("Remove Null Objects")]
	public void RemoveNullObjects()
	{
		List<PersistentObject> list = new List<PersistentObject>(this.persistentObjects);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] == null)
			{
				list.RemoveAt(i);
				i--;
			}
		}
		this.persistentObjects = list.ToArray();
	}

	// Token: 0x06000EDA RID: 3802 RVA: 0x0000CF52 File Offset: 0x0000B152
	[ContextMenu("Find Persistent Objects")]
	public void FindPersistentObjects()
	{
		this.AssignIDs();
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x0004E418 File Offset: 0x0004C618
	[ContextMenu("Assign IDs")]
	public void AssignIDs()
	{
		for (int i = 0; i < this.persistentObjects.Length; i++)
		{
			if (this.persistentObjects[i] != null)
			{
				this.persistentObjects[i].id = i;
			}
		}
	}

	// Token: 0x040012F0 RID: 4848
	private static SceneSaveData instance;

	// Token: 0x040012F1 RID: 4849
	private bool isLoaded;

	// Token: 0x040012F2 RID: 4850
	public PersistentObject[] persistentObjects;

	// Token: 0x040012F3 RID: 4851
	private bool[] objectStates;

	// Token: 0x040012F4 RID: 4852
	private const bool reuseExistingSlots = false;
}
