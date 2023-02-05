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

        [SerializeField]
        bool right = true;

        private void LateUpdate()
        {

            if (right)
            {
                cameraDir = Camera.main.transform.forward;
            }
            else
            {
                cameraDir = Camera.main.transform.forward * -1;
            }

            cameraDir.y = 0;
            transform.rotation = Quaternion.LookRotation(cameraDir);
        }
    }
}
