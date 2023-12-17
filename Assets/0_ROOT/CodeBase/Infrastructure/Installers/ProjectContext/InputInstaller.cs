using System;
using Infrastructure.InputHandlers;
using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;
using Application = UnityEngine.Application;

namespace Infrastructure.Installers.ProjectContext
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private GameInputHandlersCollection m_gameInputHandlersCollection;
        
        public override void InstallBindings()
        {
            switch (SystemInfo.deviceType)
            {
                case DeviceType.Desktop:
                    if (Application.isEditor)
                    {
                        Container.Bind<IGameInputService>().
                            To<EditorGameInputService>()
                            .FromInstance(new EditorGameInputService(m_gameInputHandlersCollection))
                            .AsSingle()
                            .NonLazy();
                        break;
                    }
                    
                    Container.Bind<IGameInputService>().
                        To<StandaloneGameInputService>()
                        .FromInstance(new StandaloneGameInputService(m_gameInputHandlersCollection))
                        .AsSingle()
                        .NonLazy();
                    break;
                
                case DeviceType.Handheld:
                    Container.Bind<IGameInputService>().
                        To<MobileGameInputService>()
                        .FromInstance(new MobileGameInputService(m_gameInputHandlersCollection))
                        .AsSingle()
                        .NonLazy();
                    break;
                
                default:
                    throw new SystemException("Device is not supported!");
            }
        }
    }
}