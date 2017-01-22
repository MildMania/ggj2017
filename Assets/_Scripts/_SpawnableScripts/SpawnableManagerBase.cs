using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public abstract class SpawnableManagerBase : MonoBehaviour
{
    public List<GameObject> SpawnablePrefabList;

    public Transform SpawnableCarrier;

    public int MaxSpawnableInGame;

    public float MinSpawnInterval, MaxSpawnInterval;

    protected List<SpawnableBase> _deactiveSpawnableList;
    protected List<SpawnableBase> _activeSpawnableList;

    protected bool _canSpawnSpawnable;

    protected IEnumerator _spawnRoutine;

    protected virtual void Awake()
    {
        InitSpawnableLists();
        GenerateSpawnables();

        StartListeningEvents();
    }

    protected virtual void OnDestroy()
    {
        FinishListeningEvents();
    }

    protected virtual void StartListeningEvents()
    {
    }

    protected virtual void FinishListeningEvents()
    {
        FinishListeningGameEndingEvents();
    }

    protected virtual void FinishListeningGameEndingEvents()
    {

    }

    protected virtual void InitSpawnableLists()
    {
        _deactiveSpawnableList = new List<SpawnableBase>();
        _activeSpawnableList = new List<SpawnableBase>();
    }

    protected virtual void GenerateSpawnables()
    {
        foreach (GameObject spawnablePrefab in SpawnablePrefabList)
            for (int i = 0; i < MaxSpawnableInGame; i++)
            {
                GameObject tempSpawnable = GameObject.Instantiate(spawnablePrefab, Vector3.zero, spawnablePrefab.transform.rotation) as GameObject;

                SpawnableBase spawnableBase = tempSpawnable.GetComponent<SpawnableBase>();

                spawnableBase.InitSpawnable(this);
            }
    }

    public void StartSpawnProgress()
    {
        StopSpawnProgress();

        _spawnRoutine = SpawnProgress();
        StartCoroutine(_spawnRoutine);
    }

    public void StopSpawnProgress()
    {
        if (_spawnRoutine != null)
            StopCoroutine(_spawnRoutine);
    }

    protected virtual IEnumerator SpawnProgress()
    {
        _canSpawnSpawnable = true;

        while (_canSpawnSpawnable)
        {
            SpawnableBase newSpawnable = null;

            do
            {
                newSpawnable = SpawnSpawnable();
                yield return Utilities.WaitForEndOfFrame;
            } while (newSpawnable == null);

            float spawnInterval = Utilities.NextFloat(MinSpawnInterval, MaxSpawnInterval);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public virtual SpawnableBase SpawnSpawnable(params object[] list)
    {
        list = GetSpawnParameters(list);

        Vector3 spawnPos = GetSpawnPos();

        SpawnableBase newSpawnable = SpawnSpawnable(spawnPos, list);

        return newSpawnable;
    }

    public virtual SpawnableBase SpawnSpawnable(Vector3 spawnPos, params object[] list)
    {
        SpawnableBase targetSpawnable = GetNewSpawnable();

        if (targetSpawnable == null)
            return null;

        SpawnSpecificSpawnable(targetSpawnable, spawnPos, list);

        return targetSpawnable;
    }

    public virtual void SpawnSpecificSpawnable(SpawnableBase spawnable, Vector3 spawnPos, params object[] list)
    {
        AddToActiveList(spawnable);

        spawnable.Activate(spawnPos, list);
    }

    protected virtual object[] GetSpawnParameters(params object[] list)
    {
        return list;
    }

    protected virtual SpawnableBase GetNewSpawnable()
    {
        if (_deactiveSpawnableList.Count == 0)
            return null;

        return _deactiveSpawnableList[0];
    }

    protected virtual Vector3 GetSpawnPos()
    {
        return Vector3.zero;
    }

    protected void AddToActiveList(SpawnableBase spawnable)
    {
        _deactiveSpawnableList.Remove(spawnable);

        _activeSpawnableList.Add(spawnable);
    }

    public virtual void DeactivateAllSpawnables()
    {
        _activeSpawnableList.ForEach(val => val.Deactivate());
    }

    public virtual void SpawnableDeactivated(SpawnableBase spawnable)
    {
        AddToDeactiveList(spawnable);
    }

    protected virtual void AddToDeactiveList(SpawnableBase spawnable)
    {
        _activeSpawnableList.Remove(spawnable);

        _deactiveSpawnableList.Add(spawnable);
    }
}