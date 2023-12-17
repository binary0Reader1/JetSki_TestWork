using Canvas;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.ProjectContext
{
    public class CanvasInstaller : MonoInstaller
    {
        [SerializeField] private TouchPanel m_touchPanel;

        public override void InstallBindings() => 
            BindMobileInputUI();

        private void BindMobileInputUI()
        {
            Container.Bind<TouchPanel>()
                .FromInstance(m_touchPanel)
                .AsSingle()
                .NonLazy();
        }
    }
}