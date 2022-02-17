using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GiraffeMaker : MonoBehaviour
{

    Transform giraffe;

    Transform body;

    Transform legPivotFL;
    Transform legFL;
    Transform legPivotFR;
    Transform legFR;
    Transform legPivotBL;
    Transform legBL;
    Transform legPivotBR;
    Transform legBR;

    Transform neckPivot;
    Transform neck;

    public GameObject headPrefab;
    Transform head;
    Transform headPivot;
    Transform hornL;
    Transform hornR;

    Transform tailPivot;
    Transform tail;

    Color color;

    public Transform targetLocation;

    public static string curentGiraffeName;

    public Transform GenerateGiraffe()
    {
        // Make the empty parent object
        GameObject giraffeGameObject = new GameObject();
        giraffe = giraffeGameObject.transform;
        giraffe.tag = "Player";

        // Name it
        giraffe.name = RandomNamer.RandomName();
        curentGiraffeName = giraffe.name;

        // Base the random seed off the name, so the same name giraffe always looks the same
        // Plus 7 for a secret shift so good names get good looks :)
        Random.InitState(giraffe.name.GetHashCode() + 7);

        // Make it
        GenerateBody();
        GenerateLegs();
        GenerateNeck();
        GenerateHead();
        GenerateTail();

        // Rotate it so it the camera sees a side view instead of back
        giraffe.Rotate(0, 90, 0);

        // Move it so it starts at target transform on the y axis
        giraffe.Translate(targetLocation.position.x, (legBR.localScale.y + body.localScale.y / 2.0f) + targetLocation.position.y + targetLocation.localScale.y, 0);

        color = new Color(Random.value, Random.value, Random.value, 1.0f);
        foreach (Renderer r in giraffe.GetComponentsInChildren<Renderer>())
        {
            r.material.color = color;
        }

        GiraffeInfo gi = giraffeGameObject.AddComponent<GiraffeInfo>();
        gi.SetHeight((body.localScale.y / 2.0f + legBL.localScale.y) * 2.0f);
        gi.SetWidth(body.localScale.x / 2.0f);

        Object.DontDestroyOnLoad(giraffeGameObject);
        return giraffeGameObject.transform;
    }

    void GenerateBody()
    {
        body = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        body.name = "Body";
        body.parent = giraffe;
        Vector3 bodyScale;
        bodyScale.z = Random.Range(1.7f, 3.0f);
        bodyScale.y = Random.Range(0.8f, 1.4f);
        bodyScale.x = Random.Range(0.6f, 1.2f);
        body.localScale = bodyScale;
    }

    void GenerateLegs()
    {
        // All legs are the same size
        Vector3 legScale;
        legScale.x = Random.Range(0.1f, 0.3f);
        legScale.y = Random.Range(2.0f, 3.0f);
        legScale.z = Random.Range(0.1f, 0.3f);

        // Front left leg
        legPivotFL = new GameObject().transform;
        legPivotFL.name = "LegPivotFL";
        legPivotFL.parent = giraffe;
        legPivotFL.localPosition = new Vector3(-body.localScale.x / 2.0f, -body.localScale.y / 2.0f, body.localScale.z / 2.0f);
        legFL = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        legFL.name = "LegFL";
        legFL.parent = legPivotFL;
        legFL.localScale = legScale;
        legFL.localPosition = new Vector3(legScale.x / 2.0f, -legScale.y / 2.0f, -legScale.z / 2.0f);


        // Front right leg
        legPivotFR = new GameObject().transform;
        legPivotFR.name = "LegPivotFR";
        legPivotFR.parent = giraffe;
        legPivotFR.position = new Vector3(body.localScale.x / 2.0f, -body.localScale.y / 2.0f, body.localScale.z / 2.0f);
        legFR = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        legFR.name = "LegFR";
        legFR.parent = legPivotFR;
        legFR.localScale = legScale;
        legFR.localPosition = new Vector3(-legScale.x / 2.0f, -legScale.y / 2.0f, -legScale.z / 2.0f);


        // Back left leg
        legPivotBL = new GameObject().transform;
        legPivotBL.name = "LegPivotBL";
        legPivotBL.parent = giraffe;
        legPivotBL.position = new Vector3(-body.localScale.x / 2.0f, -body.localScale.y / 2.0f, -body.localScale.z / 2.0f); 
        legBL = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        legBL.name = "LegBL";
        legBL.parent = legPivotBL;
        legBL.localScale = legScale;
        legBL.localPosition = new Vector3(legScale.x / 2.0f, -legScale.y / 2.0f, legScale.z / 2.0f);


        // Back right leg
        legPivotBR = new GameObject().transform;
        legPivotBR.name = "LegPivotBR";
        legPivotBR.parent = giraffe;
        legPivotBR.position = new Vector3(body.localScale.x / 2.0f, -body.localScale.y / 2.0f, -body.localScale.z / 2.0f);
        legBR = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        legBR.name = "LegBR";
        legBR.parent = legPivotBR;
        legBR.localScale = legScale;
        legBR.localPosition = new Vector3(-legScale.x / 2.0f, -legScale.y / 2.0f, legScale.z / 2.0f);

    }

    void GenerateNeck()
    {
        Vector3 neckScale;
        neckScale.z = Random.Range(0.15f, 0.35f);
        neckScale.y = Random.Range(2.0f, 3.0f);
        neckScale.x = body.localScale.x / 1.8f; // about half the thickness of the body

        neckPivot = new GameObject().transform;
        neckPivot.name = "NeckPivot";
        neckPivot.parent = giraffe;
        neckPivot.localPosition = new Vector3(0, body.localScale.y / 2 - neckScale.z / 2.0f, body.localScale.z / 2.0f - neckScale.z / 2.0f);

        neck = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        neck.name = "Neck";
        neck.parent = neckPivot;
        neck.localScale = neckScale;
        neck.localPosition = new Vector3(0, neckScale.y / 2.0f - neckScale.z / 2.0f, 0);

        neckPivot.rotation = Quaternion.Euler(new Vector3(Random.Range(-2, 10), 0, 0));
    }

    void GenerateHead()
    {
        headPivot = new GameObject().transform;
        headPivot.name = "HeadPivot";
        headPivot.parent = neckPivot;
        headPivot.localPosition = new Vector3(0, neck.localScale.y - neck.localScale.z, 0);

        Vector3 headScale;
        headScale.z = Random.Range(0.5f, 1.0f);
        headScale.y = Random.Range(0.2f, 0.5f);
        headScale.x = neck.localScale.x * 1.125f; // Always slightly thicker than neck

        // Create using prefab so I don't have to do math on the horn placement
        // They just get scaled according to the head scaling
        GameObject headObject = (GameObject)Instantiate(headPrefab);
        head = headObject.transform;
        head.name = "Head";
        head.parent = headPivot;
        head.localScale = headScale;
        head.localPosition = new Vector3(0, neck.localScale.z / 2.0f, headScale.z / 2.0f - neck.localScale.z / 2.0f);

    }

    void GenerateTail()
    {
        tailPivot = new GameObject().transform;
        tailPivot.name = "TailPivot";
        tailPivot.parent = giraffe;
        tailPivot.position = new Vector3(0, body.localScale.y / 2.0f, -body.localScale.z / 2.0f);
        tail = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        tail.name = "Tail";
        tail.parent = tailPivot;

        Vector3 tailScale;
        tailScale.z = Random.Range(0.1f, 0.3f);
        tailScale.y = Random.Range(2.0f, 3.0f);
        tailScale.x = Random.Range(0.1f, 0.3f);
        tail.localScale = tailScale;
        tail.localPosition = new Vector3(0, -tailScale.y / 2.0f, tailScale.z / 2.0f);

        tailPivot.rotation = Quaternion.Euler(new Vector3(Random.Range(5, 45), 0, 0));

    }
}
