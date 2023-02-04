using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class Wall : MonoBehaviour, IInteractable
    {
        [NonSerialized]
        private int hits;

        public void Interact()
        {
            hits -= 1;

            if (hits <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
