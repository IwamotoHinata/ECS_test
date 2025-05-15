using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(SimulationSystemGroup))]
[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
public partial class EarthquakeTriggerSystem : SystemBase
{
    private Entity _earthquakeSystem;
    private EarthquakeInput _earthquakeInput;

    /*
    protected override void OnUpdate()
    {
        
        _earthquakeSystem = SystemAPI.GetSingletonEntity<EarthquakeInput>();
        _earthquakeInput = SystemAPI.GetComponent<EarthquakeInput>(_earthquakeSystem);
        Debug.Log("ë„ì¸äÆóπ");
        
        Debug.Log(_earthquakeInput.EarthquakeTrigger);

        if (_earthquakeInput.EarthquakeTrigger)
        {
            var singletonEntity = SystemAPI.GetSingletonEntity<EarthquakeState>();
            EarthquakeState EarthquakeState = SystemAPI.GetComponent<EarthquakeState>(singletonEntity);
            Debug.Log(EarthquakeState.Active);
            //ínêkÇ™î≠ê∂ÇµÇƒÇ¢Ç»Ç¢éûÇÃÇ›
            if (!EarthquakeState.Active)
            {
                EntityManager.SetComponentData(singletonEntity, new EarthquakeState
                {
                    Active = true,
                    Timer = 90f
                });

                UnityEngine.Debug.Log("ínêkî≠ê∂ÅI");
            }
        }
    }
    */
    protected override void OnCreate()
    {
        RequireForUpdate<HostPlayerTag>();
    }



    protected override void OnUpdate()
    {
        /*
        foreach (var (input, entity) in SystemAPI.Query<EarthquakeInput>().WithEntityAccess())
        {
            if (input.EarthquakeTrigger)
            {
                var singletonEntity = SystemAPI.GetSingletonEntity<EarthquakeState>();
                EarthquakeState EarthquakeState = SystemAPI.GetComponent<EarthquakeState>(singletonEntity);
                //ínêkÇ™î≠ê∂ÇµÇƒÇ¢Ç»Ç¢éûÇÃÇ›
                if (!EarthquakeState.Active)
                {
                    EntityManager.SetComponentData(singletonEntity, new EarthquakeState
                    {
                        Active = true,
                        Timer = 90f
                    });

                    UnityEngine.Debug.Log("ínêkî≠ê∂ÅI");
                }
            }
        }
        */
    }

}
