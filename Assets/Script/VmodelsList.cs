using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.script
{
    public class VmodelsList : MonoBehaviour {
        public List<Vmodel> models;

        public Vmodel getById(int i)
        {
            foreach (var item in models)
            {
                if (item.model_id == i)
                {
                    return item;
                }
            }
            return null;
        }
    }

}