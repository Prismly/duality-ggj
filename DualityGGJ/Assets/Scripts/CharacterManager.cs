using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    static GameObject dua, lity, ball;

    static bool isDua = true;
    static bool isBall = false;

    public static void TransformCharacter(Player oldCharacter)
    {
        GameObject transformed;

        if(isBall)
        {
            if(isDua)
            {
                //Currently a ball, to transform into Dua
                transformed = Instantiate(dua);
            }
            else
            {
                //Currently a ball, to transform into Lity
                transformed = Instantiate(lity);
            }
        }
        else
        {
            //Currently Dua or Lity, turn into a ball either way (and make note of who will come out)
            transformed = Instantiate(ball);
            isDua = !isDua;
        }

        transformed.transform.position = oldCharacter.transform.position;
        transformed.GetComponent<Rigidbody2D>().velocity = oldCharacter.GetComponent<Rigidbody2D>().velocity;
        Destroy(oldCharacter);
    }
}
