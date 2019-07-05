using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.script
{

    [System.Serializable]
    public class Case
    {

        public int id;
        public string case_name;
        public string case_description;
        public int model_id;
    }

    [System.Serializable]
    public class VCaseList <T>
    {
        public List<T> array;
    }
}