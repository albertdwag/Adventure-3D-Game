using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public List<UIGunUpdater> uIGunUpdaters;

    public float ammo = 5f;
    public float timeToReload = 1f;

    private float _currentShots;
    private bool _reloading = false;

    private void Awake()
    {
        GetAllUis();
    }

    protected override IEnumerator ShootCoroutine()
    {
        if (_reloading) yield break;

        while (true)
        {
            if(_currentShots < ammo)
            {
                Shoot();
                _currentShots++;
                CheckReload();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
        }
    }

    private void CheckReload()
    {
        if (_currentShots >= ammo)
        {
            StopShoot();
            StartReload();
        }
    }

    private void StartReload()
    {
        _reloading = true;
        StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {
        float time = 0;
        while (time < timeToReload)
        {
            time += Time.deltaTime;
            uIGunUpdaters.ForEach(i => i.UpdateValue(time/timeToReload));
            yield return new WaitForEndOfFrame();
        }
        _currentShots = 0;
        _reloading = false;
    }

    private void UpdateUI()
    {
        uIGunUpdaters.ForEach(i => i.UpdateValue(ammo, _currentShots));
    }

    private void GetAllUis()
    {
        uIGunUpdaters = GameObject.FindObjectsOfType<UIGunUpdater>().ToList();
    }
}
