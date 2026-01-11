using Zenject;
using TowerDefense.Signals;

namespace TowerDefense.Gameplay.Installers
{
    public class SignalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<RewardMoneySignal>();
            Container.DeclareSignal<OnGridChanged>();
            Container.DeclareSignal<OnCurrencyChanged>();
        }
    }
}
