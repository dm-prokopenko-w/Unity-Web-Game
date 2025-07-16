using Controllers.Loading;
using Controllers.SceneTransition;
using UnityEngine;
using View.Loading;
using View.Transition;
using Zenject;

namespace Installers.Loading
{
	public class LoadingSceneInstaller : MonoInstaller
	{

		[SerializeField] private LoadingView loadingView;
		[SerializeField] private SceneTransitionView transitionView;
	
		public override void InstallBindings()
		{
			Container.Bind<ISceneTransitionController>()
				.To<SceneTransitionController>()
				.AsSingle()
				.WithArguments(transitionView)
				.NonLazy();

			Container.BindInterfacesAndSelfTo<LoadingController>()
				.AsSingle()
				.WithArguments(loadingView);
		}
	}
}
