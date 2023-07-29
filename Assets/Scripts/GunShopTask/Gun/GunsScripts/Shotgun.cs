using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public override void Shoot(Transform shootPoint)
    {
        float bulletsSpacing = 0.09383f;

        Vector3[] bulletsPosition = new Vector3[] { new Vector3(shootPoint.position.x + bulletsSpacing, shootPoint.position.y, 0f), 
            shootPoint.position };

        for (int i = 0; i < bulletsPosition.Length; i++)
        {
            InitializeBullet(bulletsPosition[i]);
        }
    }

    private void InitializeBullet(Vector3 position)
    {
        Instantiate(Bullet, position, Quaternion.identity);
    }
}
