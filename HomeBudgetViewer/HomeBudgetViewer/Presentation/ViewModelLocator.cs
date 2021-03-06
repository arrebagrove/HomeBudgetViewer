﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using HomeBudgetViewer.Presentation.AboutPage;
using HomeBudgetViewer.Presentation.BudgetItemPage;
using HomeBudgetViewer.Presentation.BudgetItemPage.CategorySelectionPage;
using HomeBudgetViewer.Presentation.MainPage;
using HomeBudgetViewer.Presentation.OverviewPage;
using HomeBudgetViewer.Presentation.SettingsPage;
using HomeBudgetViewer.Presentation.SettingsPage.Tabs.UserProfiles.UserSelectionPage;
using HomeBudgetViewer.Presentation.Statistics;
using HomeBudgetViewer.Presentation.SummaryPage;
using Microsoft.Practices.ServiceLocation;

namespace HomeBudgetViewer.Presentation
{
    public class ViewModelLocator
    {
        private static ViewModelLocator _instance;

        public static ViewModelLocator Instance
        {
            get { return _instance ?? (_instance = new ViewModelLocator()); }
        }

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(()=>SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
            {

            }
            else
            {
               
            }

            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<SettingsPageViewModel>();
            SimpleIoc.Default.Register<AboutPageViewModel>();
            SimpleIoc.Default.Register<UserSelectionPageVIewModel>();
            SimpleIoc.Default.Register<BudgetItemPageViewModel>();
            SimpleIoc.Default.Register<CategorySelectionPageViewModel>();
            SimpleIoc.Default.Register<OverviewPageViewModel>();
            SimpleIoc.Default.Register<SummaryPageViewModel>();
            SimpleIoc.Default.Register<StatisticsPageViewModel>();
        }

        public StatisticsPageViewModel StatisticsPageViewModel
        {
            get { return ServiceLocator.Current.GetInstance<StatisticsPageViewModel>(); }
        }

        public SummaryPageViewModel SummaryPageViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SummaryPageViewModel>(); }
        }

        public OverviewPageViewModel OverviewPageViewModel
        {
            get { return ServiceLocator.Current.GetInstance<OverviewPageViewModel>(); }
        }

        public CategorySelectionPageViewModel CategorySelectionPageViewModel
        {
            get { return ServiceLocator.Current.GetInstance<CategorySelectionPageViewModel>(); }
        }

        public BudgetItemPageViewModel BudgetItemPageViewModel
        {
            get { return ServiceLocator.Current.GetInstance<BudgetItemPageViewModel>(); }
        }

        public UserSelectionPageVIewModel UserSelectionPageVIewModel
        {
            get { return ServiceLocator.Current.GetInstance<UserSelectionPageVIewModel>(); }
        }

        public AboutPageViewModel AboutPageViewModel
        {
            get { return ServiceLocator.Current.GetInstance<AboutPageViewModel>(); }
        }

        public MainPageViewModel MainPageViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MainPageViewModel>(); }
        }

        public SettingsPageViewModel SettingsPageViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SettingsPageViewModel>(); }
        }

    }
}
