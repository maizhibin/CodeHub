using CodeHub.Core.ViewModels.Issues;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace CodeHub.iOS.Views.Issues
{
    public class MyIssuesView : BaseIssuesView
    {
		private UISegmentedControl _viewSegment;
		private UIBarButtonItem _segmentBarButton;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			_viewSegment = new UISegmentedControl(new object[] { "Open".t(), "Closed".t(), "Custom".t() });
			_segmentBarButton = new UIBarButtonItem(_viewSegment);
            _segmentBarButton.Width = View.Frame.Width - 10f;
			ToolbarItems = new [] { new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace), _segmentBarButton, new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace) };
			var vm = (MyIssuesViewModel)ViewModel;
			vm.Bind(x => x.SelectedFilter, x =>
			{
				if (x == 2)
				{
					ShowFilterController(new CodeHub.iOS.Views.Filters.MyIssuesFilterViewController(vm.Issues));
				}

                // If there is searching going on. Finish it.
                FinishSearch();
			});

			BindCollection(vm.Issues, CreateElement);
			var set = this.CreateBindingSet<MyIssuesView, MyIssuesViewModel>();
			set.Bind(_viewSegment).To(x => x.SelectedFilter);
			set.Apply();
        }

        public override void ViewWillAppear(bool animated)
        {
            if (ToolbarItems != null)
                NavigationController.SetToolbarHidden(false, animated);
            base.ViewWillAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            if (ToolbarItems != null)
                NavigationController.SetToolbarHidden(true, animated);
        }
    }
}

