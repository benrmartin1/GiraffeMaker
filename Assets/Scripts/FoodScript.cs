using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    ParticleSystem particles;

    bool triggered = false;
    int rotateSpeed = 50;
    float timeToDie = 5f;
    // TODO remove
    public GameObject text;

    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        particles.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        // Avoid triggering multiple times on the same object
        if (triggered)
        {
            return;
        }
        triggered = true;

        // Get script from top level component
        GiraffeController controller = other.gameObject.transform.root.GetComponent<GiraffeController>();
        controller.IncrementScore();
        particles.Play();
        gameObject.GetComponent<AudioSource>().Play();
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        print(other.transform.root.name);
        if (text != null && other.transform.root.name == "Linnea")
        {
            text.SetActive(true);
        }

        StartCoroutine(DestroySelf());
    }

    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    IEnumerator DestroySelf()
    {
        for (float i = 0f; i < timeToDie; i += Time.deltaTime)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}