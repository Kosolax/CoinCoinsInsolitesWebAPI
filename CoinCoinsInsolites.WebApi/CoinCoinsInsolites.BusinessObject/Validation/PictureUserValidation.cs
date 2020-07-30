namespace CoinCoinsInsolites.BusinessObject.Validation
{
    using CoinCoinsInsolites.BusinessObject.Validation.Resources;
    using CoinCoinsInsolites.BusinessObject.Validation.Service;
    using CoinCoinsInsolites.Entities;

    public class PictureUserValidation : ValidationService<PictureUserEntity>
    {
        public override bool Validate(PictureUserEntity itemToValidate)
        {
            this.Clear();
            this.ValidateLongitude(itemToValidate.Longitude, nameof(itemToValidate.Longitude), false);
            this.ValidateLatitude(itemToValidate.Latitude, nameof(itemToValidate.Latitude), false);

            return this.IsValid;
        }

        private void ValidateLatitude(double itemToValidate, string propertyName, bool clearDictionary)
        {
            this.ClearDictionary(clearDictionary);

            // We use PlaceValidationResources here because we would do the same and i don't want to duplicate things
            this.ValidateDoubleRange(itemToValidate, double.MinValue, double.MaxValue, propertyName, PlaceValidationResources.Latitude_Length);
        }

        private void ValidateLongitude(double itemToValidate, string propertyName, bool clearDictionary)
        {
            this.ClearDictionary(clearDictionary);

            // We use PlaceValidationResources here because we would do the same and i don't want to duplicate things
            this.ValidateDoubleRange(itemToValidate, double.MinValue, double.MaxValue, propertyName, PlaceValidationResources.Longitude_Length);
        }
    }
}