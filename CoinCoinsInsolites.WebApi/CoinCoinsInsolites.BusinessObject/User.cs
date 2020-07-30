namespace CoinCoinsInsolites.BusinessObject
{
    using CoinCoinsInsolites.BusinessObject.Validation;
    using CoinCoinsInsolites.Entities;
    using System.Collections.Generic;

    public class User : BaseBusinessObject<UserEntity>
    {
        public User()
        {
            this.ListPictureUser = new List<PictureUser>();
            this.ValidationService = new UserValidation();
        }

        public User(UserEntity entity)
            : base(entity)
        {
            this.ListPictureUser = new List<PictureUser>();
            this.ValidationService = new UserValidation();
            this.Id = entity.Id;
            this.Mail = entity.Mail;
            this.Password = entity.Password;
            this.Pseudonym = entity.Pseudonym;
        }

        public int Id { get; set; }

        public List<PictureUser> ListPictureUser { get; set; }

        public string Mail { get; set; }

        public string Password { get; set; }

        public string Pseudonym { get; set; }

        public override UserEntity CreateEntity()
        {
            return new UserEntity
            {
                Id = this.Id,
                Mail = this.Mail,
                CreatedDate = this.CreatedDate,
                UpdatedDate = this.UpdatedDate,
                Password = this.Password,
                Pseudonym = this.Pseudonym,
            };
        }
    }
}