using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{

    public class Billboard : MonoBehaviour
    {
        Vector3 cameraDir;

        private void LateUpdate()
        {
            cameraDir = Camera.main.transform.forward;
            cameraDir.y = 0;

            transform.rotation = Quaternion.LookRotation(cameraDir);
        }
    }
}
