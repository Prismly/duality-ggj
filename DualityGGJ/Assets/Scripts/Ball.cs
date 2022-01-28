using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Player
{
    [SerializeField]
    GameObject duaForm, lityForm;
    public bool isDua;

    public override void TransformCheck()
    {
        if (Input.GetKeyUp(transformKey))
        {
            GameObject newForm = isDua ? Instantiate(duaForm) : Instantiate(lityForm);
            newForm.transform.position = transform.position;
            newForm.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
            cameraController.player = newForm.transform;
            Destroy(gameObject);
        }
    }
}
