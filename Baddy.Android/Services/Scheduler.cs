﻿using Android.Content;
using Baddy.Constants;
using Baddy.Droid.Helpers;
using Baddy.Enums;
using Baddy.Helpers;
using Baddy.Interfaces;
using Baddy.Models;
using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baddy.Android.Services
{
    [BroadcastReceiver]
    public class Scheduler : BroadcastReceiver
    {
        private readonly IStorageService _storageService;
        private readonly IAuthService _authService;
        private readonly IBookingService _bookingService;
        private readonly IEmailService _emailService;
        private readonly IAppContext _appContext;

        public Scheduler()
        {
            _storageService = ServiceLocator.Current.GetInstance<IStorageService>();
            _authService = ServiceLocator.Current.GetInstance<IAuthService>();
            _bookingService = ServiceLocator.Current.GetInstance<IBookingService>();
            _emailService = ServiceLocator.Current.GetInstance<IEmailService>();
            _appContext = ServiceLocator.Current.GetInstance<IAppContext>();
        }

        public override async void OnReceive(Context context, Intent intent)
        {
            var currentDateTime = DateTime.Now;

            var authorized = await HandleLogin();
            if (authorized)
                await HandleBooking(currentDateTime);

            var scheduleDay = _storageService.ReadKey<Days>(ScheduleConstants.ScheduleDay);
            var scheduleTime = _storageService.ReadKey<TimeSpan>(ScheduleConstants.ScheduleTime);

            var nextScheduleDateTime = DateTimeHelper.NextScheduledDate(currentDateTime, scheduleDay, scheduleTime);

            SchedulerHelper.StartScheduler(context, nextScheduleDateTime);

            SendEmail("Next scheduled booking", $"Your next booking is scheduled to run on: {nextScheduleDateTime.ToString(DateConstants.LongDateTimeFormat)}");
        }

        private async Task<bool> HandleLogin()
        {
            var cardNumber = _storageService.ReadKey<string>(PropertyConstants.CardNumber);
            var pinNumber = _storageService.ReadKey<string>(PropertyConstants.PinNumber);

            if (string.IsNullOrWhiteSpace(cardNumber) || string.IsNullOrWhiteSpace(pinNumber))
                return false;

            var loginResult = await _authService.Login(cardNumber, pinNumber);
            if (string.IsNullOrWhiteSpace(loginResult?.Token))
                return false;

            return await _authService.Authorize(loginResult.Token);
        }

        private async Task HandleBooking(DateTime currentDateTime)
        {
            var bookingTime = _storageService.ReadKey<TimeSpan>(ScheduleConstants.BookingTime);
            var duration = _storageService.ReadKey<int>(ScheduleConstants.BookingDuration);
            var court = _storageService.ReadKey<int>(ScheduleConstants.Court);

            if (duration > 0 && court > 0)
            {
                while(true)
                {
                    try
                    {
                        var bookingDate = currentDateTime.Date.AddDays(14) + bookingTime;
                        var bookingConfirmed = await _bookingService.Create(new List<CreateBookingInfo>
                        {
                            new CreateBookingInfo
                            {
                                Date = bookingDate,
                                Court = court,
                                Duration = duration
                            }
                        });
                        var booking = bookingConfirmed?.Bookings?.FirstOrDefault();
                        if (booking != null)
                        {
                            SendEmail(
                                "Booking confirmed",
                                $"Your booking was confirmed for {bookingDate.ToString(DateConstants.LongDateTimeFormat)}" +
                                $"\nCourt {court}, {duration} minutes\n\n" +
                                $"Current time is: {DateTime.Now.ToString(DateConstants.VeryLongDateTimeFormat)}"
                            );
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        SendEmail("Booking failed", $"An error occurred during your booking: {ex.Message}");
                    }
                }
            }
        }

        private void SendEmail(string subject, string body)
        {
            _emailService.SendEmail(_appContext.Profile.FirstName, _appContext.Profile.Email, subject, body);
        }
    }
}