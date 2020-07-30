namespace CoinCoinsInsolites.BusinessObject.Validation.Service
{
    using System.Collections.Generic;

    public abstract class ValidationService<T> : IValidationService<T>
    {
        public ValidationService()
        {
            this.ModelState = new Dictionary<string, string>();
            this.IsValid = true;
        }

        protected bool ValidateDoubleRange(double itemToValidate, double minValue, double maxValue, string propertyName, string resource)
        {
            if (itemToValidate < minValue || itemToValidate > maxValue)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateStringLength(string itemToValidate, ushort length, string propertyName, string resource)
        {
            if (!string.IsNullOrEmpty(itemToValidate) && itemToValidate.Length > length)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateStringRequired(string itemToValidate, string propertyName, string resource)
        {
            if (string.IsNullOrEmpty(itemToValidate))
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        public bool IsValid { get; set; }

        protected void ClearDictionary(bool clear)
        {
            if (clear)
            {
                this.Clear();
            }
        }

        public Dictionary<string, string> ModelState { get; set; }

        public void AddError(string key, string errorMessage)
        {
            if (!this.ModelState.ContainsKey(key))
            {
                this.ModelState.Add(key, errorMessage);
            }
            else
            {
                this.ModelState[key] = errorMessage;
            }

            this.IsValid = this.ModelState.Count == 0;
        }

        public void Clear()
        {
            try
            {
                this.ModelState.Clear();
            }
            catch
            {
                this.ModelState = new Dictionary<string, string>();
            }

            this.IsValid = this.ModelState.Count == 0;
        }

        public abstract bool Validate(T itemToValidate);
    }
}