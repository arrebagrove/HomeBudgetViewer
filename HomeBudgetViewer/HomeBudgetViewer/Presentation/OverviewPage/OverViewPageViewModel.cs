﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Database.Engine.Repository.Base;
using HomeBudgetViewer.Database.Engine.Restrictions.Currency;
using HomeBudgetViewer.Database.Engine.Restrictions.ItemType;
using HomeBudgetViewer.Messages;
using HomeBudgetViewer.Models.Enum;
using HomeBudgetViewer.Services.SettingService;

namespace HomeBudgetViewer.Presentation.OverviewPage
{
    public class OverviewPageViewModel : AppViewModelBase
    {
        private ObservableCollection<BudgetItem> _currentItems;
        private RelayCommand<object> _switchMonthCommand;
        private RelayCommand<object> _switchItemType;
        private BudgetItem _selectedBudgetItem;
        private ItemType _budgetItemType;
        public int CurrentMonthIndex { get; private set; }
        public DateTime CurrentDateTime { get; private set; }

        public OverviewPageViewModel()
        {
            this.CurrentDateTime = DateTime.Now.Date;
            this.CurrentMonthIndex = 0;
        }

        public BudgetItem SelectedBudgetItem
        {
            get
            {
                return _selectedBudgetItem;
            }
            set
            {
                _selectedBudgetItem = value;
                this.OnSelectedBudgetItemChanged();
            }
        }

        private void OnSelectedBudgetItemChanged()
        {
            if (this.SelectedBudgetItem == null)
                return;
            this.NavigationService.Navigate(typeof(BudgetItemPage.BudgetItemPage),true);
            MessengerInstance.Send<IsModifyingStateToBudgetItemMessage>(new IsModifyingStateToBudgetItemMessage(this.SelectedBudgetItem));
        }
        public string CurrentDateHeader
        {
            get { return $"{this.GetLocalizedMonth(this.CurrentDateTime.Month)} {this.CurrentDateTime.Year}"; }
        }

        public string CurrentCurrencyString
        {
            get
            {
                var currencySymbol =
                    CurrencyModel.PossibleCurrencies.FirstOrDefault(
                        c => c.CurrencyName == SettingsService.Instance.CurrentUser.Currency).CurrencySymbol;
                return currencySymbol ?? "$";
            }
        }

        public ItemType BudgetItemType
        {
            get { return _budgetItemType; }
            set
            {
                if (_budgetItemType == value)
                    return;

                _budgetItemType = value;
                RaisePropertyChanged();
            }
        }

        private void GetData()
        {
            using (var unitOfWork = new UnitOfWork(new BudgetContext()))
            {
                List<BudgetItem> items=null;
                switch (this.BudgetItemType)
                {
                    case ItemType.Expense:
                        items = unitOfWork.BudgetItems.GetAllExpensesOfUserByDate(
                            SettingsService.Instance.CurrentUser.Id,
                            this.CurrentDateTime);
                        break;
                    case ItemType.Income:
                        items = unitOfWork.BudgetItems.GetAllIncomesOfUserByDate(
                            SettingsService.Instance.CurrentUser.Id,
                            this.CurrentDateTime);
                        break;
                }

                CurrentItems = new ObservableCollection<BudgetItem>(items);
            }
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            this.GetData();
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public ObservableCollection<BudgetItem> CurrentItems
        {
            get { return _currentItems; }
            set
            {
                _currentItems = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand<object> SwitchMonthCommand
        {
            get
            {
                return _switchMonthCommand ?? (_switchMonthCommand = new RelayCommand<object>(param =>
                {
                    if (param != null)
                    {
                        var direction = (SwitchingMonthDirection) param;
                        switch (direction)
                        {
                            case SwitchingMonthDirection.Next:
                                this.CurrentMonthIndex++;
                                break;
                            case SwitchingMonthDirection.Previous:
                                this.CurrentMonthIndex--;
                                break;
                        }
                        this.CurrentDateTime = DateTime.Now.Date.AddMonths(this.CurrentMonthIndex);
                        this.RaisePropertyChanged("CurrentDateHeader");
                        this.GetData();
                    }
                }));
            }
        }

        public RelayCommand<object> SwitchItemType
        {
            get
            {
                return _switchItemType ?? (_switchItemType = new RelayCommand<object>(param =>
                {
                    if (param != null)
                    {
                        var itemType = (ItemType)param;
                        this.BudgetItemType = itemType;
                        GetData();
                    }
                }));
            }
        }
        

    }
}
