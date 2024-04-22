using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;

    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        if (Target == null)
        {
            GameObject go = GameObject.Find("Player");

            if (go == null)
                return;

            Target = go;
        }

        transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, -10);
    }
}
