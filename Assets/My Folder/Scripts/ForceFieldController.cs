using UnityEngine;

public class ForceFieldController : MonoBehaviour
{
    
    public Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.SetFloat("_SwitchText", 0);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            mat.SetFloat("_SwitchText", 1);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            mat.SetFloat("_SwitchText", 0);
        }
    }
}
