using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickableObject : MonoBehaviour
{
    public UnityEvent whatTheDogDoing;

    private void Update()
    {
        // Check for a mouse click
        if (Input.GetMouseButtonDown(0)) // 0 corresponds to the left mouse button
        {
            // Perform a raycast to see if the mouse click hits any object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the clicked object is this GameObject
                if (hit.collider.gameObject == gameObject)
                {
                    // Perform the desired action when the object is clicked
                    OnObjectClicked();
                }
            }
        }
    }

    private void OnObjectClicked()
    {
        whatTheDogDoing.Invoke();
    }
}
