//using UnityEngine;
//using System.Collections;

//public class GameplayCamera : MonoBehaviour
//{
//    GameObject player;
//    private Vector3 offset;

//    void Start()
//    {
//        player = GameObject.FindWithTag("Giraffe");
//        offset = transform.position - player.transform.position;
//    }

//    void LateUpdate()
//    {
//        transform.position = player.transform.position + offset;
//    }
//}

using UnityEngine;
using System.Collections;

public class GameplayCamera : MonoBehaviour
{
    GameObject player;
    public float distance;
    public float speed;

    private bool z;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

    }

    void LateUpdate()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);

        Vector3 behind = player.transform.position - new Vector3(player.transform.forward.x * distance, player.transform.forward.y - 1.0f, player.transform.forward.z * distance);

        if (!z && Input.GetKeyDown(KeyCode.Z))
        {
            z = true;
        }
        else if (z)
        {
            transform.LookAt(player.transform);

            transform.position = Vector3.MoveTowards(transform.position, behind, 30 * Time.deltaTime);

            if (transform.position == behind)
            {
                z = false;
            }
        }
        else
        {
            if (dist < distance)
            {
                transform.LookAt(player.transform);
            }
            else if (dist > distance * 2)
            {
                transform.LookAt(player.transform);

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 1.0f, player.transform.position.z - distance), speed * 3 * Time.deltaTime);
            }
            else if (dist < distance * 2 && dist > distance)
            {
                transform.LookAt(player.transform);

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 1.0f, player.transform.position.z - distance), speed * Time.deltaTime);
            }
        }
    }
}