namespace CoinCoinsInsolites.BusinessObject.Validation
{
    using CoinCoinsInsolites.BusinessObject.Validation.Resources;
    using CoinCoinsInsolites.BusinessObject.Validation.Service;
    using CoinCoinsInsolites.Entities;

    public class PlaceValidation : ValidationService<PlaceEntity>
    {
        public override bool Validate(PlaceEntity itemToValidate)
        {
            this.Clear();
            this.ValidateDescription(itemToValidate.Description, nameof(itemToValidate.Description), false);
            this.ValidateTitle(itemToValidate.Title, nameof(itemToValidate.Title), false);
            this.ValidateLongitude(itemToValidate.Longitude, nameof(itemToValidate.Longitude), false);
            this.ValidateLatitude(itemToValidate.Latitude, nameof(itemToValidate.Latitude), false);

            return this.IsValid;
        }

        private void ValidateLongitude(double itemToValidate, string propertyName, bool clearDictionary)
        {
            this.ClearDictionary(clearDictionary);
            this.ValidateDoubleRange(itemToValidate, double.MinValue, double.MaxValue, propertyName, PlaceValidationResources.Longitude_Length);
        }

        private void ValidateLatitude(double itemToValidate, string propertyName, bool clearDictionary)
        {
            this.ClearDictionary(clearDictionary);
            this.ValidateDoubleRange(itemToValidate, double.MinValue, double.MaxValue, propertyName, PlaceValidationResources.Latitude_Length);
        }

        private void ValidateDescription(string itemToValidate, string propertyName, bool clearDictionary)
        {
            this.ClearDictionary(clearDictionary);
            if (this.ValidateStringRequired(itemToValidate, propertyName, PlaceValidationResources.Description_Required))
            {
                this.ValidateStringLength(itemToValidate, 6000, propertyName, PlaceValidationResources.Description_Length);
            }
        }

        private void ValidateTitle(string itemToValidate, string propertyName, bool clearDictionary)
        {
            this.ClearDictionary(clearDictionary);
            if (this.ValidateStringRequired(itemToValidate, propertyName, PlaceValidationResources.Title_Required))
            {
                this.ValidateStringLength(itemToValidate, 1000, propertyName, PlaceValidationResources.Title_Length);
            }
        }
    }
}