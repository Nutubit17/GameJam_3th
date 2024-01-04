using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public static ParticleManager Instance;
    
    [System.Serializable]
    public struct NameAndGameObject
    {
        public string name;
        public GameObject prefab;
    }

    public List<NameAndGameObject> prefabs;
    
    private void Awake()
    {
        Instance = this;
    }

    public void MakeParticle(Vector3 position, string name = "", float destroyTime = 2f, Transform trm = null)
    { 
        var particles = prefabs.Where(x => x.name == name).ToArray();

        if (particles.Length >= 1)
        {
            GameObject obj = Instantiate
                (
                    particles[0].prefab,
                    position,
                    quaternion.identity,
                    trm
                );
            
            Destroy(obj, destroyTime);
        }

    }




}
