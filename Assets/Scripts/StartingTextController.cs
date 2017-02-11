using UnityEngine;
using System.Collections;

public class StartingTextController : MonoBehaviour {

    // The amount of time in seconds it takes for the text to move and shrink
    float totalTime = 0.3f;
    // The amount the text moves down towards the bottom of the screen
    float totalDistance = 250f;
    // Scale from 0 to 1 of how much the text will be shrunk.
    // 0.5 means the final text will be hal the size of the original.
    float totalShrinkAmount = 0.5f;

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
            scale.x -= totalShrinkAmount * (Time.deltaTime / totalTime);
            scale.y -= totalShrinkAmount * (Time.deltaTime / totalTime);
            transform.localScale = scale;

            yield return null;
        }
        //transform.gameObject.SetActive(false);

    }


}
