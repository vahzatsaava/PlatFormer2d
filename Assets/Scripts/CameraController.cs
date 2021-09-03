using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] private Transform player;
    private Vector3 pos;

    private void Start()
    {
        pos = transform.position - player.position;
            
    }
    void Awake()
    {//проверка на нахождение игрокаж
        if (!player)
            player = FindObjectOfType<Hero>().transform;

    }

    
    void FixedUpdate()
    {
        transform.position = player.position + pos;
        
    }
}
