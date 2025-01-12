using System;
using Core.MVVM.ViewModelLocator.Interfaces;
using FreeView.Scripts.ViewModelLocator.Interfaces;
using FreeView.Scripts.ViewModels.Interfaces;

namespace Core.MVVM.ViewModelLocator
{
    public class ViewModelLocator : IViewModelLocator
    {
        private readonly IViewModelProvider _viewModelProvider;
        
        public ViewModelLocator(IViewModelProvider viewModelProvider)
        {
            _viewModelProvider = viewModelProvider;
        }
        
        public IBaseViewModel Load(Type viewModelType)
        {
            if (viewModelType == null)
                throw new ArgumentNullException(nameof(viewModelType));

            IBaseViewModel baseViewModel;
            try
            {
                baseViewModel = _viewModelProvider.ResolveViewModel(viewModelType);
            }
            catch (Exception)
            {
                throw new Exception($"Problem creating viewModel of type {viewModelType.Name}");
            }
            
            RunViewModelLifecycle(baseViewModel);
            return baseViewModel;
        }

        public IBaseViewModel Load<TViewModel>() where TViewModel : IBaseViewModel
        {
            IBaseViewModel baseViewModel;
            try
            {
                baseViewModel = _viewModelProvider.ResolveViewModel<TViewModel>();
            }
            catch (Exception e)
            {
                throw new Exception($"Problem creating viewModel of type {typeof(TViewModel)} {e.Message}");
            }
            
            RunViewModelLifecycle(baseViewModel);
            return baseViewModel;
        }
        
        public IBaseViewModel<TNavigationArgs> Load<TViewModel, TNavigationArgs>(TNavigationArgs args) 
            where TViewModel : IBaseViewModel<TNavigationArgs>  
        {
            IBaseViewModel<TNavigationArgs> baseViewModel;
            try
            {
                baseViewModel = _viewModelProvider.ResolveViewModel<TViewModel, TNavigationArgs>(args);
            }
            catch (Exception e)
            {
                throw new Exception($"Problem creating viewModel of type {typeof(TViewModel)} {e.Message}");
            }
            
            RunViewModelLifecycle(baseViewModel, args);
            return baseViewModel;
        }
        
        private void RunViewModelLifecycle(IBaseViewModel baseViewModel)
        {
            if (baseViewModel == null)
                throw new ArgumentNullException(nameof(baseViewModel));

            try
            {
                baseViewModel.Start();
                baseViewModel.Prepare();
                baseViewModel.Initialize();
            }
            catch (Exception)
            {
                throw new Exception($"Problem running viewModel lifecycle of type {baseViewModel.GetType().Name}");
            }
        }

        private void RunViewModelLifecycle<TNavigationArgs>(IBaseViewModel<TNavigationArgs> baseViewModel, TNavigationArgs args)  
        {
            if (baseViewModel == null)
                throw new ArgumentNullException(nameof(baseViewModel));

            try
            {
                baseViewModel.Start();
                baseViewModel.Prepare(args);
                baseViewModel.Initialize();
            }
            catch (Exception)
            {
                throw new Exception($"Problem running viewModel lifecycle of type {baseViewModel.GetType().Name}");
            }
        }
    }
}