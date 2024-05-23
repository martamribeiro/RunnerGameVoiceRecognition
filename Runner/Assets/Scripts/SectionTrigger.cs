using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject[] roadSections;  // Array to hold different road sections

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            // Randomly select a road section from the array
            int randomIndex = Random.Range(0, roadSections.Length);
            GameObject selectedRoadSection = roadSections[randomIndex];

            // Instantiate the selected road section at the calculated position
            Instantiate(selectedRoadSection, new Vector3(-3.459214f-0.249607f-0.036538815f, 0,0), Quaternion.identity);
        }
    }
}
