using Configs;
using Services.Api;
using Services.Data;
using UnityEngine;
using Zenject;

namespace Installers.Global
{
    public class GlobalInstaller : MonoInstaller
    {

        [SerializeField] private EndPointsConfig endPointsConfig;
        [SerializeField] private GridConfig gridConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<IApiService>().To<ApiService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DataService>().AsSingle().NonLazy();
            
            Container.Bind<EndPointsConfig>().FromInstance(endPointsConfig).AsSingle();
            Container.Bind<GridConfig>().FromInstance(gridConfig).AsSingle();
        }
    }
}