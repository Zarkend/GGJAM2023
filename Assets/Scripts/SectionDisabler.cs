using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionDisabler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        var section = other.GetComponent<Section>();

        if (section != null)
        {
            section.Disable();
        }
    }
}
