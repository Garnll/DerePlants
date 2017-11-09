using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOfParameter  {

    private static TypeOfParameter instance = null;

    public static TypeOfParameter Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TypeOfParameter();
            }
            return instance;
        }

    }

    public enum Parameter
    {
        network,
        local,
        single
    }

    public Parameter currentPlayType;
}
