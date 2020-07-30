namespace CoinCoinsInsolites.BusinessObject.Validation
{
    using CoinCoinsInsolites.BusinessObject.Validation.Resources;
    using CoinCoinsInsolites.BusinessObject.Validation.Service;
    using CoinCoinsInsolites.Entities;

    public class UserValidation : ValidationService<UserEntity>
    {
        public override bool Validate(UserEntity itemToValidate)
        {
            this.Clear();
            this.ValidateMail(itemToValidate.Mail, nameof(itemToValidate.Mail), false);
            this.ValidatePassword(itemToValidate.Password, nameof(itemToValidate.Password), false);
            this.ValidatePseudonym(itemToValidate.Pseudonym, nameof(itemToValidate.Pseudonym), false);

            return this.IsValid;
        }

        private void ValidateMail(string itemToValidate, string propertyName, bool clearDictionary)
        {
            this.ClearDictionary(clearDictionary);
            if (this.ValidateStringRequired(itemToValidate, propertyName, UserValidationResources.Mail_Required))
            {
                this.ValidateStringLength(itemToValidate, 255, propertyName, UserValidationResources.Mail_Length);
            }
        }

        private void ValidatePassword(string itemToValidate, string propertyName, bool clearDictionary)
        {
            this.ClearDictionary(clearDictionary);
            if (this.ValidateStringRequired(itemToValidate, propertyName, UserValidationResources.Password_Required))
            {
                this.ValidateStringLength(itemToValidate, 255, propertyName, UserValidationResources.Password_Length);
            }
        }

        private void ValidatePseudonym(string itemToValidate, string propertyName, bool clearDictionary)
        {
            this.ClearDictionary(clearDictionary);
            if (this.ValidateStringRequired(itemToValidate, propertyName, UserValidationResources.Pseudonym_Required))
            {
                this.ValidateStringLength(itemToValidate, 255, propertyName, UserValidationResources.Pseudonym_Length);
            }
        }
    }
}