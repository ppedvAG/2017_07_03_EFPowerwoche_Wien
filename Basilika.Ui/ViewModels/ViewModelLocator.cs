using Basilika.Core;
using Basilika.Data;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Basilika.Ui.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<BlutContext>();
            SimpleIoc.Default.Register<IUnitOfWork, UnitOfWork>();
           
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<BlutprobenViewModel>();
        }

        public BlutprobenViewModel Blutproben => ServiceLocator.Current.GetInstance<BlutprobenViewModel>();
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}