using System;
using Xamarin.Forms;

namespace Baddy.Behaviours
{
    public class EntryCompletedBehaviour : BehaviorBase<Entry>
    {
        public static readonly BindableProperty EntryCompletedCommandProperty =
            BindableProperty.Create(
                "EntryCompletedCommand",
                typeof(Command),
                typeof(EntryCompletedBehaviour),
                null);

        public Command EntryCompletedCommand
        {
            get => (Command)GetValue(EntryCompletedCommandProperty);
            set => SetValue(EntryCompletedCommandProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Completed += OnEntryCompleted;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Completed -= OnEntryCompleted;
        }

        private void OnEntryCompleted(object sender, EventArgs e)
        {
            var newParam = new object[] {
                sender,
                e
            };

            if (EntryCompletedCommand.CanExecute(newParam))
                EntryCompletedCommand.Execute(newParam);
        }
    }
}
