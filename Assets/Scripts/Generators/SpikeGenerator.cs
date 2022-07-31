using UnityEngine;


namespace GDRTest.Generators { 
    public class SpikeGenerator : Generator<Spike>
    {
        protected override void Awake()
        {
            base.Awake();
            _tag = "Spike";
        }

        protected override GameObject SpawnRandomGameObject()
        {
            _id = Random.Range(0, _prefabs.Length - 1);

            var position = _screen.GetRandomPosition();
            while(!_screen.CheckFreeSpace(position))
            {
                position = _screen.GetRandomPosition();
            }

            var obj = CreateObjectById(
                _id,
                position,
                Quaternion.identity,
                _parent
            );

            obj.tag = _tag;
            return obj.gameObject;
        }
    }
}
