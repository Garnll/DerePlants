using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(Phrase_Pool))]
public class Phrase_Pool_Custom_Editor : Editor {

    private Phrase_Pool phrasePool;

    private bool[] editingActive;

    private void OnEnable()
    {
        phrasePool = (Phrase_Pool)target;
        editingActive = new bool[3];
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Numero de frases: " + (phrasePool.phrasesPositive.Length + phrasePool.phrasesNegative.Length + phrasePool.phrasesAmbiguous.Length));


        //FRASES POSITIVAS

        GUILayout.BeginVertical("Box");
        editingActive[0] = EditorGUILayout.Toggle("Activar Positivas: ", editingActive[0]);

        if (editingActive[0])
        {
            for (int i = 0; i < phrasePool.phrasesPositive.Length; i++)
            {
                GUILayout.BeginVertical("Box");

                GUILayout.BeginHorizontal();

                phrasePool.phrasesPositive[i].editThisPhrase = EditorGUILayout.Toggle("Editar Frase: ", phrasePool.phrasesPositive[i].editThisPhrase);

                GUILayout.EndHorizontal();

                if (phrasePool.phrasesPositive[i].editThisPhrase)
                {
                    phrasePool.phrasesPositive[i].myPhrase = EditorGUILayout.TextArea(phrasePool.phrasesPositive[i].myPhrase);
                    phrasePool.phrasesPositive[i].love = (int)EditorGUILayout.Slider("Love: ", phrasePool.phrasesPositive[i].love, 1, 20);
                    phrasePool.phrasesPositive[i].loveType = (Love_Type)EditorGUILayout.EnumPopup("Tipo: ", phrasePool.phrasesPositive[i].loveType);
                }
                else
                {
                    if (phrasePool.phrasesPositive[i].myPhrase == "")
                    {
                        GUILayout.Label("Esta Frase no ha sido editada");
                    }
                    else
                    {
                        GUILayout.Label(phrasePool.phrasesPositive[i].myPhrase + " / " +
                            phrasePool.phrasesPositive[i].love.ToString() + " / " +
                            phrasePool.phrasesPositive[i].loveType.ToString());
                    }
                }

                GUILayout.EndVertical();

                GUILayout.Space(5);

            }
        }
        GUILayout.EndVertical();


        //FRASES AMBIGUAS

        GUILayout.Space(10);

        GUILayout.BeginVertical("Box");
        editingActive[1] = EditorGUILayout.Toggle("Activar Ambiguas: ", editingActive[1]);

        if (editingActive[1])
        {
            for (int i = 0; i < phrasePool.phrasesAmbiguous.Length; i++)
            {
                GUILayout.BeginVertical("Box");

                GUILayout.BeginHorizontal();

                phrasePool.phrasesAmbiguous[i].editThisPhrase = EditorGUILayout.Toggle("Editar Frase: ", phrasePool.phrasesAmbiguous[i].editThisPhrase);

                GUILayout.EndHorizontal();

                if (phrasePool.phrasesAmbiguous[i].editThisPhrase)
                {
                    phrasePool.phrasesAmbiguous[i].myPhrase = EditorGUILayout.TextArea(phrasePool.phrasesAmbiguous[i].myPhrase);
                    phrasePool.phrasesAmbiguous[i].love = (int)EditorGUILayout.Slider("Love: ", phrasePool.phrasesAmbiguous[i].love, 1, 20);
                    phrasePool.phrasesAmbiguous[i].loveType = (Love_Type)EditorGUILayout.EnumPopup("Tipo: ", phrasePool.phrasesAmbiguous[i].loveType);
                }
                else
                {
                    if (phrasePool.phrasesAmbiguous[i].myPhrase == "")
                    {
                        GUILayout.Label("Esta Frase no ha sido editada");
                    }
                    else
                    {
                        GUILayout.Label(phrasePool.phrasesAmbiguous[i].myPhrase + " / " +
                            phrasePool.phrasesAmbiguous[i].love.ToString() + " / " +
                            phrasePool.phrasesAmbiguous[i].loveType.ToString());
                    }
                }

                GUILayout.EndVertical();

                GUILayout.Space(5);

            }
        }
        GUILayout.EndVertical();


        //FRASES NEGATIVAS

        GUILayout.Space(10);

        GUILayout.BeginVertical("Box");
        editingActive[2] = EditorGUILayout.Toggle("Activar Negativas: ", editingActive[2]);

        if (editingActive[2])
        {
            for (int i = 0; i < phrasePool.phrasesNegative.Length; i++)
            {
                GUILayout.BeginVertical("Box");

                GUILayout.BeginHorizontal();

                phrasePool.phrasesNegative[i].editThisPhrase = EditorGUILayout.Toggle("Editar Frase: ", phrasePool.phrasesNegative[i].editThisPhrase);

                GUILayout.EndHorizontal();

                if (phrasePool.phrasesNegative[i].editThisPhrase)
                {
                    phrasePool.phrasesNegative[i].myPhrase = EditorGUILayout.TextArea(phrasePool.phrasesNegative[i].myPhrase);
                    phrasePool.phrasesNegative[i].love = (int)EditorGUILayout.Slider("Love: ", phrasePool.phrasesNegative[i].love, 1, 20);
                    phrasePool.phrasesNegative[i].loveType = (Love_Type)EditorGUILayout.EnumPopup("Tipo: ", phrasePool.phrasesNegative[i].loveType);
                }
                else
                {
                    if (phrasePool.phrasesNegative[i].myPhrase == "")
                    {
                        GUILayout.Label("Esta Frase no ha sido editada");
                    }
                    else
                    {
                        GUILayout.Label(phrasePool.phrasesNegative[i].myPhrase + " / " +
                            phrasePool.phrasesNegative[i].love.ToString() + " / " + 
                            phrasePool.phrasesNegative[i].loveType.ToString());
                    }
                }

                GUILayout.EndVertical();

                GUILayout.Space(5);

            }
        }
        GUILayout.EndVertical();
    }
}
