using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MapTileController : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Camera camera = collision.GetComponent<Camera>();
        if (camera == null)
            return;

        Vector3 dir = camera.transform.position - transform.position;

        float dirX = dir.x < 0 ? -1 : 1;
        float dirY = dir.y < 0 ? -1 : 1;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            transform.Translate(Vector3.right * dirX * 200);
        }
        else
        {
            transform.Translate(Vector3.up * dirY * 200);
        }
    }
}
