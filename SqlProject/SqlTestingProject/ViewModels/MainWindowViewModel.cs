using Prism.Commands;
using Prism.Regions;
using SqlProject.CoreLibrary.Mvvm;
using SqlProject.CoreLibrary.Regions;

namespace SqlTestingProject.ViewModels
{
    public class MainWindowViewModel : RegionViewModelBase
    {
        public string Title { get; set; } = "Sql Testing Project";

        private string[] _viewsNames = { "FirstTestView", "SecondTestView", "ThirdTestView" };

        #region Raising properties
        private int _viewsIndex;
        public int ViewsIndex
        {
            get => _viewsIndex;
            set
            {
                _ = SetProperty(ref _viewsIndex, value);
                MoveToNextModule.RaiseCanExecuteChanged();
                MoveToPrevModule.RaiseCanExecuteChanged();
            }
        }

        private string _nextModuleName = "Next Module";
        public string NextModuleName
        {
            get => _nextModuleName;
            set => _ = SetProperty(ref _nextModuleName, value);
        }

        private string _prevModuleName = "Prev Module";
        public string PrevModuleName
        {
            get { return _prevModuleName; }
            set { _ = SetProperty(ref _prevModuleName, value); }
        }
        #endregion

        #region Commands
        private DelegateCommand? _moveToNextModule;
        public DelegateCommand MoveToNextModule => _moveToNextModule ??= new DelegateCommand(ExecuteMoveToNextModule, CanExecuteMoveToNextModule);

        private DelegateCommand? _moveToPrevModule;
        public DelegateCommand MoveToPrevModule => _moveToPrevModule ??= new DelegateCommand(ExecuteMoveToPrevModule, CanExecuteMoveToPrevModule);
        #endregion

        public MainWindowViewModel(IRegionManager regionManager) : base(regionManager)
        {

        }

        #region Methods
        private void ExecuteMoveToNextModule()
        {
            ViewsIndex++;
            RegionManager.RequestNavigate(RegionNames.ContentRegion, _viewsNames[ViewsIndex]);
        }

        private bool CanExecuteMoveToNextModule()
        {
            return ViewsIndex < _viewsNames.Length - 1;
        }
        private void ExecuteMoveToPrevModule()
        {
            ViewsIndex--;
            RegionManager.RequestNavigate(RegionNames.ContentRegion, _viewsNames[ViewsIndex]);
        }
        private bool CanExecuteMoveToPrevModule()
        {
            return ViewsIndex > 0;
        } 
        #endregion
    }
}
