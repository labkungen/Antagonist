using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocus : MonoBehaviour
{
    private GameObject focusObject;
    private Vector3 focusPosition;
    private GameManager gameManager;
    private bool isFocusingOnObject;
    [SerializeField] float cameraRotationSpeed;
    [SerializeField] float cameraMoveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (focusObject != null && isFocusingOnObject)
        {
           // transform.position = focusObject.transform.position;
            transform.LookAt(focusObject.transform.position);
        }

        float horisontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        //Rotate view
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -cameraRotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, cameraRotationSpeed * Time.deltaTime);
        }


        //Move
        transform.Translate(Vector3.forward * verticalInput * cameraMoveSpeed * Time.deltaTime) ;
        transform.Translate(Vector3.right * horisontalInput * cameraMoveSpeed * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveFocus(gameManager.LocationOfPlayer());
            isFocusingOnObject = !isFocusingOnObject;
        }


        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            gameManager.LocationOfPlayer().GetComponent<Anthill>().ChangePercentageSoldiers(1);
            
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            gameManager.LocationOfPlayer().GetComponent<Anthill>().ChangePercentageSoldiers(-1);
        }

    }

    //POLYMORPHISM
    private void MoveFocus(GameObject targetObject)
    {
        focusObject = targetObject;
    }

    private void MoveFocus(Vector3 focusPosition)
    {
        

    }
}
