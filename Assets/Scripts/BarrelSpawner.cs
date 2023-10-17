using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _barrelPrefab;
    [SerializeField] private float _minTime;
    [SerializeField] private float _maxTime;

    private void Start()
    {
        SpawnBarrel();
    }

    private void SpawnBarrel()
    {
        Instantiate(_barrelPrefab, transform.position, Quaternion.identity);
        Invoke(nameof(SpawnBarrel), UnityEngine.Random.Range(_minTime, _maxTime));
    }
}
