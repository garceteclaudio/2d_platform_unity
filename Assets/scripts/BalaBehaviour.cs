using System;
using UnityEngine;

public class BalaBehaviour : MonoBehaviour
{

    public float velocidad = 3f;


    void Start()
    {
        //acceder al componente de gameobject de activar
        Debug.Log(gameObject.activeSelf);
    }
    void Update()
    {
        //Vector3 movement = new Vector3(velocidad * Time.deltaTime, 0, 0);
        //gameObject.transform.position += movement;

        //gameObject.transform.position += Vector3.right * velocidad * Time.deltaTime;

        //gameObject.transform.position = 
        //new Vector3(gameObject.transform.position.x + velocidad * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
    }


}