using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TommyGun : Gun
{
    public override void Shoot(Transform shootPoint)
    {
        Vector3[] bulletsPosition = new Vector3[] { shootPoint.position, shootPoint.position, shootPoint.position };

        for (int i = 0; i < bulletsPosition.Length; i++)
        {
            InitializeBullet(bulletsPosition[i], i);
        }
    }

    private void InitializeBullet(Vector3 position, float changeSpeedValue)
    {
        Instantiate(Bullet, position, Quaternion.identity).ChangeSpeed(changeSpeedValue);
    }
}
