using UnityEngine;
using System.Collections;

public class GameplayCamera : MonoBehaviour
{
    GameObject player;
    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindWithTag("Giraffe");
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}