using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 90f; // You can adjust this in the Inspector

    void Update()
    {
        // Rotate the coin around the Y axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
