namespace CoinCoinsInsolites.Business
{
    using CoinCoinsInsolites.BusinessObject;
    using CoinCoinsInsolites.BusinessObject.Validation;
    using CoinCoinsInsolites.BusinessObject.Validation.Resources;
    using CoinCoinsInsolites.Entities;
    using CoinCoinsInsolites.IBusiness;
    using CoinCoinsInsolites.IDataAccess;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserBusiness : IUserBusiness
    {
        private readonly IUserDataAccess dataAccess;

        private readonly IPictureUserBusiness pictureUserBusiness;

        public UserBusiness(IUserDataAccess dataAccess, IPictureUserBusiness pictureUserBusiness)
        {
            this.dataAccess = dataAccess;
            this.pictureUserBusiness = pictureUserBusiness;
        }

        // Todo remove that and make a real authent with token etc ...
        public async Task<KeyValuePair<bool, User>> Authenticate(string mail, string password)
        {
            try
            {
                if (!string.IsNullOrEmpty(mail) && !string.IsNullOrEmpty(password))
                {
                    var userEntity = await this.dataAccess.FindByMailAndPassword(mail, password);
                    var user = new User(userEntity);

                    return new KeyValuePair<bool, User>(true, user);
                }

                return new KeyValuePair<bool, User>(false, null);
            }
            catch (Exception)
            {
                return new KeyValuePair<bool, User>(false, null);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dataAccess?.Dispose();
                this.pictureUserBusiness?.Dispose();
            }
        }

        public async Task<KeyValuePair<bool, User>> Create(User itemToCreate)
        {
            try
            {
                if (itemToCreate != null)
                {
                    UserEntity entity = itemToCreate.CreateEntity();
                    var userEntityThatAlreadyHaveThisMail = await this.dataAccess.FindByMail(entity.Mail);

                    // We check if someone dosn't already have this mail
                    if (userEntityThatAlreadyHaveThisMail != null)
                    {
                        itemToCreate.ValidationService.AddError(nameof(UserValidationResources.Mail_Unique), UserValidationResources.Mail_Unique);
                        return new KeyValuePair<bool, User>(false, itemToCreate);
                    }

                    // When we create a user he can't already have photos, that's why we don't create his photo in the same time
                    if (itemToCreate.ValidationService.Validate(entity) && userEntityThatAlreadyHaveThisMail == null)
                    {
                        if (entity.Id == default)
                        {
                            entity = await this.dataAccess.Create(entity);
                            itemToCreate = await this.GetUserFromEntity(entity);

                            return new KeyValuePair<bool, User>(true, itemToCreate);
                        }
                    }
                }

                return new KeyValuePair<bool, User>(false, itemToCreate);
            }
            catch (Exception)
            {
                return new KeyValuePair<bool, User>(false, itemToCreate);
            }
        }

        public async Task<KeyValuePair<bool, User>> Delete(int id)
        {
            User user = await this.Get(id);

            try
            {
                if (user != null)
                {
                    await this.pictureUserBusiness.DeleteAllFromUserId(id);
                    await this.dataAccess.Delete(id);
                    return new KeyValuePair<bool, User>(true, user);
                }

                return new KeyValuePair<bool, User>(false, user);
            }
            catch (Exception)
            {
                return new KeyValuePair<bool, User>(false, user);
            }
        }

        public async Task<User> Get(int id)
        {
            User user = null;

            try
            {
                UserEntity entity = await this.dataAccess.Find(id);

                if (entity != null)
                {
                    user = await this.GetUserFromEntity(entity);
                }

                return user;
            }
            catch (Exception)
            {
                return user;
            }
        }

        public async Task<List<User>> List()
        {
            List<User> users = new List<User>();

            try
            {
                var result = await this.dataAccess.List();
                users = await this.GetUsersFromEntity(result);

                return users;
            }
            catch (Exception)
            {
                return users;
            }
        }

        public async Task<KeyValuePair<bool, User>> Update(int id, User itemToUpdate)
        {
            try
            {
                if (itemToUpdate != null && id != default)
                {
                    // Check if the user want to modify his mail and if he want to modifiy his mail we check if it's not already used
                    User user = await this.Get(id);
                    if (user.Mail != itemToUpdate.Mail)
                    {
                        UserEntity userEntityThatAlreadyHaveThisMail = await this.dataAccess.FindByMail(itemToUpdate.Mail);

                        if (userEntityThatAlreadyHaveThisMail != null)
                        {
                            itemToUpdate.ValidationService.AddError(nameof(UserValidationResources.Mail_Unique), UserValidationResources.Mail_Unique);
                            return new KeyValuePair<bool, User>(false, itemToUpdate);
                        }
                    }

                    itemToUpdate.Id = id;
                    UserEntity entity = itemToUpdate.CreateEntity();
                    var result = this.pictureUserBusiness.ValidateList(itemToUpdate.ListPictureUser, itemToUpdate);

                    if (itemToUpdate.ValidationService.Validate(entity) && result.Key)
                    {
                        if (entity.Id != default)
                        {
                            entity = await this.dataAccess.Update(entity, entity.Id);
                            var resultFromPictureUser = await this.pictureUserBusiness.CreateOrUpdateFromList(id, itemToUpdate.ListPictureUser);

                            if (!resultFromPictureUser.Key)
                            {
                                await this.Delete(entity.Id);
                                itemToUpdate.ValidationService.AddError(nameof(UserValidationResources.ListPictureUser_GoneWrong), UserValidationResources.ListPictureUser_GoneWrong);
                                return new KeyValuePair<bool, User>(false, itemToUpdate);
                            }

                            itemToUpdate = await this.GetUserFromEntity(entity);

                            return new KeyValuePair<bool, User>(true, itemToUpdate);
                        }
                    }

                    itemToUpdate = result.Value;
                }

                return new KeyValuePair<bool, User>(false, itemToUpdate);
            }
            catch (Exception)
            {
                return new KeyValuePair<bool, User>(false, itemToUpdate);
            }
        }

        private async Task<User> GetUserFromEntity(UserEntity entity)
        {
            User user = new User(entity);
            user.ListPictureUser = await this.pictureUserBusiness.FindAllFromUserId(entity.Id);

            return user;
        }

        private async Task<List<User>> GetUsersFromEntity(List<UserEntity> userEntities)
        {
            List<int> listId = userEntities.Select(obj => obj.Id).Distinct().ToList();
            List<PictureUser> pictureUsersAllClient = await this.pictureUserBusiness.FindAllFromListId(listId);
            List<User> users = new List<User>();

            foreach (var userEntity in userEntities)
            {
                // We could have just done this :
                // users.Add(await this.GetUserFromEntity(userEntity));
                // and return users;
                // But it means more time and request to the db
                // I decided that i prefer to use the CPU for that instead of DTU (calcul unit for azure db)
                List<PictureUser> pictureUsersForThisClient = new List<PictureUser>();

                pictureUsersAllClient.ForEach(pictureUser =>
                {
                    if (pictureUser.IdUser == userEntity.Id)
                    {
                        pictureUsersForThisClient.Add(pictureUser);
                    }
                });

                User user = new User(userEntity);
                user.ListPictureUser = pictureUsersForThisClient;
                users.Add(await this.GetUserFromEntity(userEntity));
            }

            return users;
        }
    }
}