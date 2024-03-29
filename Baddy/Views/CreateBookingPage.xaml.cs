﻿using Baddy.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;
using CommonServiceLocator;

namespace Baddy.Views
{
    [DesignTimeVisible(false)]
    public partial class CreateBookingPage : ContentPage
    {
        public CreateBookingPage()
        {
            InitializeComponent();

            BindingContext = ServiceLocator.Current.GetInstance<CreateBookingViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<object>(this, "NavigateAway", (sender) =>
            {
                ((CreateBookingViewModel)BindingContext).NavigateAway = true;
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Send<object>(this, "NavigateAway");
            MessagingCenter.Unsubscribe<object>(this, "NavigateAway");
        }
    }
}