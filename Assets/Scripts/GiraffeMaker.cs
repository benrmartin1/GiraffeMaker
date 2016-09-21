using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GiraffeMaker : MonoBehaviour {

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

    public Text nameText;


    // Use this for initialization
    void Start () {
        //GenerateGiraffe();
        //giraffe.Translate(Vector3.left * 4 + Vector3.up * 4);
        //GenerateGiraffe();
        //giraffe.Translate(Vector3.right * 4 + Vector3.up * 4);
        //GenerateGiraffe();
        //giraffe.Translate(Vector3.left * 4 + Vector3.down * 2);
        //GenerateGiraffe();
        //giraffe.Translate(Vector3.right * 4 + Vector3.down * 2);
        GenerateGiraffe();

    }

    void GenerateGiraffe()
    {
        // Make the empty parent object
        GameObject giraffeGameObject = new GameObject();
        giraffe = giraffeGameObject.transform;
        giraffe.tag = "Giraffe";

        // Name it
        giraffe.name = GetComponent<RandomNamer>().RandomName();
        nameText.text = giraffe.name;

        Random.InitState(giraffe.name.GetHashCode());

        // Make it
        GenerateBody();
        GenerateLegs();
        GenerateNeck();
        GenerateHead();
        GenerateTail();

        // Move it so it starts at 0 on the y axis
        giraffe.Translate(0, (legBR.localScale.y + body.localScale.y / 2.0f), 0);

        color = new Color(Random.value, Random.value, Random.value, 1.0f);
        foreach (Renderer r in giraffe.GetComponentsInChildren<Renderer>())
        {
            r.material.color = color;
        }
    }

    void GenerateBody()
    {
        body = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        body.name = "Body";
        body.parent = giraffe;
        Vector3 bodyScale;
        bodyScale.x = Random.Range(1.7f, 3.0f);
        bodyScale.y = Random.Range(0.8f, 1.4f);
        bodyScale.z = Random.Range(0.6f, 1.2f);
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
        legPivotFL.localPosition = new Vector3(-body.localScale.x/2.0f, -body.localScale.y / 2.0f, body.localScale.z / 2.0f);
        legFL = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        legFL.name = "LegFL";
        legFL.parent = legPivotFL;
        legFL.localScale = legScale;
        legFL.localPosition = new Vector3(legScale.x/2.0f, -legScale.y / 2.0f, -legScale.z / 2.0f);


        // Front right leg
        legPivotFR = new GameObject().transform;
        legPivotFR.name = "LegPivotFR";
        legPivotFR.parent = giraffe;
        legPivotFR.position = new Vector3(-body.localScale.x / 2.0f, -body.localScale.y / 2.0f, -body.localScale.z / 2.0f);
        legFR = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        legFR.name = "LegFR";
        legFR.parent = legPivotFR;
        legFR.localScale = legScale;
        legFR.localPosition = new Vector3(legScale.x / 2.0f, -legScale.y / 2.0f, legScale.z / 2.0f);


        // Back left leg
        legPivotBL = new GameObject().transform;
        legPivotBL.name = "LegPivotBL";
        legPivotBL.parent = giraffe;
        legPivotBL.position = new Vector3(body.localScale.x / 2.0f, -body.localScale.y / 2.0f, body.localScale.z / 2.0f);
        legBL = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        legBL.name = "LegBL";
        legBL.parent = legPivotBL;
        legBL.localScale = legScale;
        legBL.localPosition = new Vector3(-legScale.x / 2.0f, -legScale.y / 2.0f, -legScale.z / 2.0f);


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
        neckScale.x = Random.Range(0.15f, 0.35f);
        neckScale.y = Random.Range(2.0f, 3.0f);
        neckScale.z = body.localScale.z / 1.8f; // about half the thickness of the body

        neckPivot = new GameObject().transform;
        neckPivot.name = "NeckPivot";
        neckPivot.parent = giraffe;
        neckPivot.localPosition = new Vector3(-body.localScale.x/2.0f + neckScale.x/2.0f, body.localScale.y/2 - neckScale.x/2.0f, 0);

        neck = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        neck.name = "Neck";
        neck.parent = neckPivot;
        neck.localScale = neckScale;
        neck.localPosition = new Vector3(0, neckScale.y / 2.0f - neckScale.x / 2.0f, 0);


        neckPivot.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-5, 25)));
    }

    void GenerateHead()
    {
        headPivot = new GameObject().transform;
        headPivot.name = "HeadPivot";
        headPivot.parent = neckPivot;
        headPivot.localPosition = new Vector3(0, neck.localScale.y - neck.localScale.x, 0);

        Vector3 headScale;
        headScale.x = Random.Range(0.5f, 1.0f);
        headScale.y = Random.Range(0.2f, 0.5f);
        headScale.z = neck.localScale.z * 1.125f; // Always slightly thicker than neck

        // Create using prefab so I don't have to do math on the horn placement
        // They just get scaled according to the head scaling
        GameObject headObject = (GameObject)Instantiate(headPrefab);
        head = headObject.transform;
        head.name = "Head";
        head.parent = headPivot;
        head.localScale = headScale;
        head.localPosition = new Vector3(-headScale.x / 2.0f + neck.localScale.x / 2.0f, neck.localScale.x / 2.0f, 0);


    }

    void GenerateTail()
    {
        tailPivot = new GameObject().transform;
        tailPivot.name = "TailPivot";
        tailPivot.parent = giraffe;
        tailPivot.position = new Vector3(body.localScale.x / 2.0f, body.localScale.y / 2.0f, 0);
        tail = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        tail.name = "Tail";
        tail.parent = tailPivot;

        Vector3 tailScale;
        tailScale.x = Random.Range(0.1f, 0.3f);
        tailScale.y = Random.Range(2.0f, 3.0f);
        tailScale.z = Random.Range(0.1f, 0.3f);
        tail.localScale = tailScale;
        tail.localPosition = new Vector3(tailScale.x / 2.0f, -tailScale.y / 2.0f, 0);

        tailPivot.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(5, 45)));

    }

    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(giraffe.gameObject);
            GenerateGiraffe();
        }
	}
}
