﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ApplicationProject.Views;
using ApplicationProject.Views.DatedPageView;
using System.Globalization;

namespace ApplicationProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewPresenter
    {
        public IBaseView PresentedView { get; protected set; }
        public Overlay Overlay { get; }

        public MainWindow()
        {
            InitializeComponent();
            Overlay = new Overlay(OverlayLayer);

            UserControls.InterPageView.InterPageView view = new()
            {
                AnalysisButtonName = "Анализ",
                PlanButtonName = "План",
                AccountName = "Аккаунт"
            };
            Present(view);
            UserControls.DatedPageView.DatedPageView aview = new()
            {
                PageNameTextKey = "Анализ"
            };
            aview.DateRangeTypes.Add(new DateRangeType { DisplayName = "Месяц", Type = DateRangeType.RangeType.MONTH });
            aview.DateRangeTypes.Add(new DateRangeType { DisplayName = "Год", Type = DateRangeType.RangeType.YEAR });
            view.PageViewPresenter.Present(aview);
        }

        public void OnCultureChanged(CultureInfo culture)
        {
            PresentedView?.OnCultureChanged(culture);
        }

        public bool Present(IBaseView view)
        {
            if(view == null)
                throw new ArgumentNullException(nameof(view));
            else if(!view.IsPresentable || !(view is UserControl))
                return false;

            PresentedView?.Hide();
            if(PresentedView is ISupportOverlay overlay)
            {
                overlay.ClearOverlay();
                overlay.Overlay = null;
            }

            PresentedView = view;
            ActiveView.Content = view as UserControl;

            PresentedView?.Show();
            if(PresentedView is ISupportOverlay overlay2)
                overlay2.Overlay = Overlay;

            return true;
        }
    }
}