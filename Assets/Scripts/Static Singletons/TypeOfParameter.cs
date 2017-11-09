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
        single,
        network,
        local

    }

    public Parameter currentPlayType = Parameter.single;
}
