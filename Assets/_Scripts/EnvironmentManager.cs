using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager Instance { get; private set; }

    public EnvObjScript StartingObj;
    public List<GameObject> PrefabList;

    List<EnvObjScript> _spawnedEnvs;

    public Transform MaxFillPoint;

    void Awake()
    {
        Instance = this;

        _spawnedEnvs = new List<EnvObjScript>();
    }

    void Start()
    {
        StartCoroutine(FillArea());
    }

    IEnumerator FillArea()
    {
        if(StartingObj != null)
            _spawnedEnvs.Add(StartingObj);
        else
            SpawnAt(new Vector3(0f, 0f, 0f));

        while (_spawnedEnvs[_spawnedEnvs.Count - 1].EndTransform.position.z < MaxFillPoint.position.z)
        {
            SpawnAt(StartingObj.EndTransform.position);

            yield return null;
        }
    }

    void SpawnAt(Vector3 point)
    {
        int randIndex = UnityEngine.Random.Range(0, PrefabList.Count);

        GameObject newObj = GameObject.Instantiate(PrefabList[randIndex], transform) as GameObject;
        newObj.transform.position = point;

        _spawnedEnvs.Add(newObj.GetComponent<EnvObjScript>());
    }

    void Update()
    {
        if (_spawnedEnvs[_spawnedEnvs.Count - 1].EndTransform.position.z < MaxFillPoint.position.z)
            SpawnAt(_spawnedEnvs[_spawnedEnvs.Count - 1].EndTransform.position);
    }
}
