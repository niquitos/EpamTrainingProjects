using Prism.Commands;
using Prism.Regions;
using SqlProject.CoreLibrary.Mvvm;
using SqlProject.CoreLibrary.Regions;

namespace SqlTestingProject.ViewModels
{
    public class MainWindowViewModel : RegionViewModelBase
    {
        public string Title { get; set; } = "Sql Testing Project";
        private int _viewsIndex = 0;
        private string[] _viewsNames = { "FirstTestView", "SecondTestView" };

        private string _nextModuleName = "Next Module";
        public string NextModuleName
        {
            get { return _nextModuleName; }
            set { _ = SetProperty(ref _nextModuleName, value); }
        }

        private string _prevModuleName = "Prev Module";
        

        public string PrevModuleName
        {
            get { return _prevModuleName; }
            set { _ = SetProperty(ref _prevModuleName, value); }
        }

        #region Commands
        private DelegateCommand? _moveToNextModule;
        public DelegateCommand MoveToNextModule => _moveToNextModule ??= new DelegateCommand(ExecuteMoveToNextModule);

        private DelegateCommand? _moveToPrevModule;
        public DelegateCommand MoveToPrevModule => _moveToPrevModule ??= new DelegateCommand(ExecuteMoveToPrevModule);
        #endregion

        public MainWindowViewModel(IRegionManager regionManager) : base(regionManager)
        { 
            
        }

        private void ExecuteMoveToNextModule()
        {
            if (_viewsIndex < _viewsNames.Length - 1)
            {
                _viewsIndex++; RegionManager.RequestNavigate(RegionNames.ContentRegion, _viewsNames[_viewsIndex]);
            }
        }
        private void ExecuteMoveToPrevModule()
        {
            if (_viewsIndex > 0)
            {
                _viewsIndex--; RegionManager.RequestNavigate(RegionNames.ContentRegion, _viewsNames[_viewsIndex]);
            }
        }
    }
}
