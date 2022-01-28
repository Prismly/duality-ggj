using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    GameObject dua, lity, ball;

    public enum PlayerCharacter
    {
        DUA,
        LITY,
        BALL
    }

    bool isDua = true;

    //public void TransformCharacter(Player oldCharacter)
    //{
    //    GameObject transformed;

    //    if (oldCharacter.characterIdentity == PlayerCharacter.DUA)
    //        transformed = Instantiate(dua);
    //    else if (newCharacter == PlayerCharacter.LITY)
    //        transformed = Instantiate(lity);
    //    else
    //        transformed = Instantiate(ball);

    //    transformed.transform.position = oldCharacter.transform.position;
    //    transform.GetComponent<Rigidbody2D>().velocity = oldCharacter.GetComponent<Rigidbody2D>().velocity;
    //    Destroy(oldCharacter);
    //}
}
