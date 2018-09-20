using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollText : MonoBehaviour {
    public IEnumerator rollText(string sentence,Text tx)
    {
        tx.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            tx.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }
}
