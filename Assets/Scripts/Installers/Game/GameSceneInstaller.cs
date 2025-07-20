using Controllers.SceneTransition;
using UnityEngine;
using UnityEngine.Serialization;
using View.Transition;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private SettingsView settingsView;
    [SerializeField] private GameView gameView;
    [SerializeField] private SceneTransitionView transitionView;

    public override void InstallBindings()
    {
        Container.Bind<ISceneTransitionController>()
            .To<SceneTransitionController>()
            .AsSingle()
            .WithArguments(transitionView)
            .NonLazy();

        Container.BindInterfacesAndSelfTo<GameController>()
            .AsSingle()
            .WithArguments(settingsView, gameView);
        
        Container.Bind<IGameBuilder>()
            .To<GameBuilder>()
            .AsSingle()
            //.WithArguments(settingsView, gameView)
            .NonLazy();
    }
}
