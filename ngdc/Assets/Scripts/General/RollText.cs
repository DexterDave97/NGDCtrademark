using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollText : MonoBehaviour {
    public static bool ended;

    public static IEnumerator rollText(string sentence,Text tx)
    {
        tx.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            tx.text += letter;
            if (tx.text == sentence)
            {
                ended = true;
            }
            yield return new WaitForSeconds(0.03f);
            
        }
    }
}
