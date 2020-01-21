using System;
using Xamarin.Forms;

namespace sudoku.Behaviors
{
    /// <summary>
    /// Validates an Input Entry Value
    ///
    /// Code from Suthahar J - https://www.c-sharpcorner.com/article/input-validation-in-xamarin-forms-behaviors/
    /// </summary>
    public class NumberValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            int result;
            bool isValid = false;

            try {
                result = Int32.Parse(args.NewTextValue);

                if (result > 0 && result <= 9) {
                    isValid = true;
                }
            } catch (Exception e) {
                isValid = false;
            }

            
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}
