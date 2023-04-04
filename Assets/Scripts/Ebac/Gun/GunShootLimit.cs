using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public float ammo = 5f;
    public float timeToReload = 1f;

    private float _currentShots;
    private bool _reloading = false;

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
            yield return new WaitForEndOfFrame();
        }
        _currentShots = 0;
        _reloading = false;
    }
}
