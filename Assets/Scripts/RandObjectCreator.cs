using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using MathNet.Numerics.Distributions;
using System.Diagnostics;
public class RandObjectCreator : MonoBehaviour
{
    public GameObject[] myObjects;
    public float lamdaSphere = 0.01f;

    void Start()
    {
        InvokeRepeating("repetitor", 0.5f, 2.0f);
    }
    void Update()
    {
    }

    public void repetitor()
    {
        List<float> lamdas = new List<float>{lamdaSphere};
        for (int i = 0; i < lamdas.Count; i++)
        {
            generateObject(lamdas[i], i);
        }
    }
    public void generateObject(float lamda, int index)
    {
        if (myObjects[index] != null)
        {
            Vector3 randomCreatePosition = new Vector3((transform.position.x + Random.Range(-0.003f, 0.003f)), transform.position.y, (transform.position.z + Random.Range(-0.003f, 0.003f))); // to define where the objects are generated (x, y, z) coordinates.
            Instantiate(myObjects[index], randomCreatePosition, Quaternion.identity);
        }

    }

    private int randPoisson(float lamda)
    {
        float k = Poisson.Sample(lamda*0.025);
        return (int)k;
    }

}
