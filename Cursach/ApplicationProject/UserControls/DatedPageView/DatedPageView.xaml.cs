﻿using System;
using System.Globalization;
using System.ComponentModel;

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

using System.Windows;
using System.Windows.Controls;

using ApplicationProjectViews;
using ApplicationProjectViews.DatedPageView;

namespace ApplicationProject.UserControls.DatedPageView
{
    /// <summary>
    /// Interaction logic for AnalysisPageView.xaml
    /// </summary>
    public partial class DatedPageView : UserControl, IDatedPageView, ISupportOverlay, INotifyPropertyChanged, IViewPresenter, ICultureDependentData
    {
        protected static readonly Point CalendarOffset = new(0, 0);

        public DatedPageView()
        {
            m_PageNameTextKey = "";

            InitializeComponent();

            DateRangeTypes = new ObservableCollection<DateRangeType>();
            ((ObservableCollection<DateRangeType>)DateRangeTypes).CollectionChanged += AnalysisPage_DateRangeTypesChanged;
            DateRangeTypeSelector.ItemsSource = DateRangeTypes;

            DateRangeSelectorRoot = new Viewbox
            {
                Child = new RangeSelectorCalendar()
            };

            DateRangeSelectorCalendar = (RangeSelectorCalendar)DateRangeSelectorRoot.Child;
            DateRangeSelectorCalendar.SelectionTarget = RangeSelectorCalendar.RangeSelectorCalendarMode.Month;
            DateRangeSelectorCalendar.SelectionChanged += DaterRangeSelector_SelectionChanged;
            CurrentCulture = null;
        }

        protected Viewbox DateRangeSelectorRoot { get; }
        protected RangeSelectorCalendar DateRangeSelectorCalendar { get; }
        private CultureInfo m_CurrentCulture;
        protected CultureInfo CurrentCulture
        {
            get => m_CurrentCulture;
            set
            {
                m_CurrentCulture = value ?? System.Threading.Thread.CurrentThread.CurrentUICulture ?? CultureInfo.CurrentUICulture ?? CultureInfo.CurrentCulture ?? CultureInfo.InvariantCulture;
                DateRangeSelectorCalendar.CurrentCulture = m_CurrentCulture;
                RefreshLocalization();
            }
        }


        #region IViewPresenter
        public IBaseView PresentedView { get; private set; }

        public bool Present(IBaseView view)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));
            else if (!(view is UserControl && view.Show()))
                return false;

            if (PresentedView is ISupportOverlay overlay)
            {
                overlay.ClearOverlay();
                overlay.Overlay = null;
            }

            PresentedView = view;
            ActiveView.Content = view as UserControl;

            if (PresentedView is ISupportOverlay overlay2)
                overlay2.Overlay = Overlay;
            if (PresentedView is ICultureDependentData cultureDependent)
                cultureDependent.OnCultureChanged(CurrentCulture);

            PresentedView.OnShown();

            return true;
        }
        #endregion

        #region ICultureDependentData
        public void OnCultureChanged(CultureInfo newCulture)
        {
            CurrentCulture = newCulture;
        }
        #endregion

        #region IBaseView
        public bool Show()
        {
            ShowPreview?.Invoke(this, EventArgs.Empty);

            return DateRangeTypes.Count > 0 &&
                   PageNameText?.Length > 0 &&
                   PresentedView != null &&
                   PresentedView.Show();
        }

        public void OnShown()
        {
            if (PresentedView is ISupportOverlay overlay)
                overlay.Overlay = Overlay;

            if (PresentedView is ICultureDependentData cultureDependent)
                cultureDependent.OnCultureChanged(CurrentCulture);

            PresentedView.OnShown();
        }

        public void DispatchUpdate(ViewUpdate action)
        {
            Dispatcher.Invoke(() => action(this));
        }

        public event EventHandler ShowPreview;
        #endregion

        #region IDatedPageView
        public string DateRangeText => ConvertToDateRangeDisplay(SelectedDateRange);

        public string PageNameTextKey
        {
            get => m_PageNameTextKey;
            set
            {
                m_PageNameTextKey = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PageNameText)));
            }
        }
        private string m_PageNameTextKey;
        public string PageNameText => GetLocalizedString(PageNameTextKey);

        public event DateRangeTypeSelectedEventHandler DateRangeTypeSelected;
        public event EventHandler SelectedDateRangeChanged;
        public event EventHandler NextDateRangeSelected;
        public event EventHandler PreviousDateRangeSelected;

        public DateRange SelectedDateRange
        {
            get => m_SelectedDateRange;
            set
            {
                m_SelectedDateRange = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DateRangeText)));
            }
        }
        private DateRange m_SelectedDateRange;

        public ICollection<DateRangeType> DateRangeTypes { get; }

        public DateRange? DateRangeBounds
        {
            get => new(DateRangeSelectorCalendar.LowerBoundary.Value, DateRangeSelectorCalendar.UpperBoundary.Value);
            set
            {
                DateRangeSelectorCalendar.LowerBoundary = value.HasValue ? value.Value.Start : null;
                DateRangeSelectorCalendar.UpperBoundary = value.HasValue ? value.Value.End : null;
            }
        }

        public DateRangeType SelectedRangeType
        {
            get => m_SelectedRangeType;
            set
            {
                m_SelectedRangeType = value;
                DateRangeSelectorCalendar.SelectionTarget = m_SelectedRangeType switch
                {
                    DateRangeType.MONTH => RangeSelectorCalendar.RangeSelectorCalendarMode.Month,
                    DateRangeType.YEAR => RangeSelectorCalendar.RangeSelectorCalendarMode.Year,
                    _ => throw new ArgumentOutOfRangeException(nameof(SelectedRangeType)),
                };
            }
        }
        private DateRangeType m_SelectedRangeType;
        #endregion

        #region ISupportOverlay
        private Overlay m_Overlay;
        public Overlay Overlay
        {
            get => m_Overlay;
            set
            {
                m_Overlay = value;

                if (m_Overlay != null)
                    m_Overlay.BackgroundClick += Overlay_Click;
            }
        }

        public void ClearOverlay()
        {
            if (Overlay.Visible)
                Overlay.RemoveElement(DateRangeSelectorRoot);

            Overlay.BackgroundClick -= Overlay_Click;
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        public void RefreshLocalization()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PageNameText)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DateRangeText)));
        }

        private string GetLocalizedString(string key)
        {
            return ApplicationProject.Resources.Locale.ResourceManager.GetString(key, CurrentCulture);
        }

        protected string ConvertToDateRangeDisplay(DateRange range)
        {
            return m_SelectedRangeType switch
            {
                DateRangeType.MONTH => range.Start.ToString("MMMM yyyy", CurrentCulture),
                DateRangeType.YEAR => range.Start.ToString("yyyy", CurrentCulture),
                _ => "ERROR"
            };
        }
        #endregion

        #region Handled events
        private void DateRangeSelector_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource == DateRangeSelector)
            {
                Overlay.AddElement(DateRangeSelectorRoot);
                DateRangeSelectorRoot.Height = DateRangeSelectorRoot.Width = DateRangeSelector.ActualWidth;
                Overlay.MoveElement(DateRangeSelectorRoot, DateRangeSelector, new Point(CalendarOffset.X, CalendarOffset.Y + DateRangeSelector.ActualHeight));
                Overlay.Visible = DateRangeSelector.IsChecked ?? false;
                DateRangeSelectorRoot.Visibility = (DateRangeSelector.IsChecked ?? false) ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private void ButtonPreviousDateRange_Click(object sender, RoutedEventArgs e)
        {
            PreviousDateRangeSelected?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonNextDateRange_Click(object sender, RoutedEventArgs e)
        {
            NextDateRangeSelected?.Invoke(this, EventArgs.Empty);
        }

        private void Overlay_Click(object sender, EventArgs e)
        {
            Overlay.Visible = false;
            DateRangeSelector.IsChecked = false;
            m_Overlay.RemoveElement(DateRangeSelectorRoot);
        }

        private void CurrentPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Overlay.Visible)
            {
                DateRangeSelectorRoot.Height = DateRangeSelectorRoot.Width = DateRangeSelector.ActualWidth;
                Overlay.MoveElement(DateRangeSelectorRoot, DateRangeSelector, new Point(CalendarOffset.X, CalendarOffset.Y + DateRangeSelector.ActualHeight));
            }
        }

        private void DateRangeTypeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateRangeTypeSelected?.Invoke(this, new DateRangeTypeSelectedEventArgs((DateRangeType)DateRangeTypeSelector.SelectedItem));
        }

        private void AnalysisPage_DateRangeTypesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (DateRangeTypes.Count == 1)
                DateRangeTypeSelector.SelectedIndex = 0;
        }

        private void DaterRangeSelector_SelectionChanged(object sender, EventArgs e)
        {
            if (sender == DateRangeSelectorCalendar)
            {
                IEnumerator<RangeSelectorCalendar.DateRange> enumerator = DateRangeSelectorCalendar.SelectedRanges.GetEnumerator();
                if (enumerator.MoveNext())
                    SelectedDateRange = new DateRange(enumerator.Current.Start, enumerator.Current.End);

                Overlay_Click(this, EventArgs.Empty);

                SelectedDateRangeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}
