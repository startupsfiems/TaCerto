using System;
using System.Collections;
using System.Collections.Generic;

public class StringAux{
    public static int CreckSprite(string text, int index){
        if(text.Length >= index + 28 && text.Substring(index, 28).Equals("<size=150%><sprite=0></size>"))
            return 1;
        return -1;
    }
    public static bool CreckPalavra(string text, int index){
        if(index < 0)
            return false;
        string valor = (text.Split(new string[]{" "}, StringSplitOptions.None))[index];
        if(valor.StartsWith("<b>") && valor.EndsWith("</b>"))
            return true;
        return false;
    }  
}