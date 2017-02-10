using UnityEngine;
using System.Collections;

public class StartingTextController : MonoBehaviour {


    float totalTime = 0.3f;
    float totalDistance = 250f;

    public IEnumerator Shrink()
    {
        float currentTime = 0;
        while(currentTime < totalTime)
        {
            currentTime += Time.deltaTime;
            Vector3 pos = transform.position;
            pos.y -= totalDistance * (Time.deltaTime / totalTime);
            transform.position = pos;

            Vector3 scale = transform.localScale;
            scale.x -= 0.5f * (Time.deltaTime / totalTime);
            scale.y -= 0.5f * (Time.deltaTime / totalTime);
            transform.localScale = scale;

            yield return null;
        }
        //transform.gameObject.SetActive(false);

    }


}
