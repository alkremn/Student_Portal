using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Student_Portal.Behaviors
{
    class IntValidationBehavior : Behavior<Entry>
    {
        private const int MAX_NUMBER = 10;

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += Entry_TextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= Entry_TextChanged;
            base.OnDetachingFrom(entry);
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isValid = int.TryParse(e.NewTextValue, out int result) || e.NewTextValue.Length <= MAX_NUMBER;

            Entry entry = sender as Entry;
            entry.TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}
