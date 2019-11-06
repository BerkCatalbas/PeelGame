using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peel : MonoBehaviour
{
    public ParticleSystem part;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Peels(int index)
    {
        Destroy(this.gameObject.GetComponent<MeshCollider>());
        Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
        int[] oldtri = mesh.triangles;
        int[] newtri = new int[mesh.triangles.Length - 3];

        int i = 0, j = 0;

        while(j<mesh.triangles.Length)
        {
            if(j !=index*3)
            {
                newtri[i++] = oldtri[j++];
                newtri[i++] = oldtri[j++];
                newtri[i++] = oldtri[j++];
            }
            else
            {
                j += 3;
            }
        }
        transform.GetComponent<MeshFilter>().mesh.triangles = newtri;
        this.gameObject.AddComponent<MeshCollider>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit,1000.0f))
            {
                Debug.Log(hit.triangleIndex);
                Peels(hit.triangleIndex);
                part.transform.position = ray.GetPoint(10);
                part.transform.position = new Vector3(part.transform.position.x, part.transform.position.y, 3f);
                part.Play();
            }
        }
        transform.Rotate(0, -Time.deltaTime * 50, 0);
    }
}
