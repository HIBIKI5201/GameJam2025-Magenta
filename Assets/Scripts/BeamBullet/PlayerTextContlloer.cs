using System.Collections.Generic;
using UnityEngine;

public class PlayerTextContlloer : MonoBehaviour
{
    IBulletGenerator _bulletGenerator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _bulletGenerator = new BeamBulletGenerator();

        _bulletGenerator.Update(1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
