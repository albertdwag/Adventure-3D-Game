using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GunType
{
    Gun1,
    Gun2
}

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase gun1Base;
    public Transform gun1Position;
    public GunBase gun2Base;
    public Transform gun2Position;

    private GunBase _currentGun;
    private GunType _currentGunType = GunType.Gun1;

    protected override void Init()
    {
        base.Init();

        CreateGun(_currentGunType);

        inputs.Gameplay.PrimaryWeapon.performed += ctx => SwtichPrimaryWeapon();
        inputs.Gameplay.SecondaryWeapon.performed += ctx => SwitchSecondaryWeapon();
        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
    }

    private void CreateGun(GunType gunType)
    {
        switch (gunType)
        {
            case GunType.Gun1:
                _currentGun = Instantiate(gun1Base, gun1Position);
                break;
            case GunType.Gun2:
                _currentGun = Instantiate(gun2Base, gun2Position);
                break;
        }

        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    private void SwtichPrimaryWeapon()
    {
        if (_currentGunType != GunType.Gun1)
        {
            Destroy(_currentGun.gameObject);
            _currentGunType = GunType.Gun1;
            CreateGun(_currentGunType);
        }
    }

    private void SwitchSecondaryWeapon()
    {
        if (_currentGunType != GunType.Gun2)
        {
            Destroy(_currentGun.gameObject);
            _currentGunType = GunType.Gun2;
            CreateGun(_currentGunType);
        }
    }

    private void StartShoot()
    {
        _currentGun.StartShoot();
        Debug.Log("Start Shoot");
    }

    private void CancelShoot()
    {
        _currentGun.StopShoot();
        Debug.Log("Cancel Shoot");
    }
}
