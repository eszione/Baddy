using System;
using Xamarin.Forms;

namespace Baddy.Behaviours
{
    public class EntryFocusedBehaviour : BehaviorBase<Entry>
    {
        public string NextFocusedElement { get; set; }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Completed += OnEntryCompleted;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.Completed -= OnEntryCompleted;
            base.OnDetachingFrom(bindable);
        }

        private void OnEntryCompleted(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NextFocusedElement))
                return;

            var parent = ((Entry)sender).Parent;
            if (parent != null)
            {
                var nextFocusElement = parent.FindByName<Entry>(NextFocusedElement);
                if (nextFocusElement != null)
                {
                    nextFocusElement.Focus();
                }
            }
        }
    }
}
