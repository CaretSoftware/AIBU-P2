using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private LayerMask dialogueOption;
    [SerializeField] private LayerMask objectThatDoSomething;

    private GameObject clickedObject;
    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        

        if (Physics.Raycast(ray, out hit, 100, dialogueOption))
        {
            clickedObject = hit.collider.gameObject;
            //clickedObject.GetComponent<SCRIPT>().DoSomething();

        }

        if (Physics.Raycast(ray, out hit, 100, objectThatDoSomething))
        {
            //hit.collider.gameObject.getComponent<SCRIPT>().DoSomething();
        }
    }
}
