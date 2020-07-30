namespace CoinCoinsInsolites.Business
{
    using CoinCoinsInsolites.BusinessObject;
    using CoinCoinsInsolites.Entities;
    using CoinCoinsInsolites.IBusiness;
    using CoinCoinsInsolites.IDataAccess;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PictureUserBusiness : IPictureUserBusiness
    {
        private readonly IPictureUserDataAccess dataAccess;

        private List<PictureUserEntity> pictureUserEntitiesToCreate = new List<PictureUserEntity>();

        private List<PictureUserEntity> pictureUserEntitiesToDelete = new List<PictureUserEntity>();

        private List<PictureUserEntity> pictureUserEntitiesToUpdate = new List<PictureUserEntity>();

        public PictureUserBusiness(IPictureUserDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public async Task<KeyValuePair<bool, List<PictureUser>>> CreateOrUpdateFromList(int id, List<PictureUser> listPictureUser)
        {
            try
            {
                if (listPictureUser != null && listPictureUser.Count != 0)
                {
                    // We don't check if objects are correct because we'll do it in User
                    await this.SetListToDeleteToCreateToUpdate(id, listPictureUser);

                    if (this.pictureUserEntitiesToDelete.Count != 0)
                    {
                        this.dataAccess.DeleteAllFromList(this.pictureUserEntitiesToDelete);
                    }
                    if (this.pictureUserEntitiesToCreate.Count != 0)
                    {
                        await this.dataAccess.CreateFromList(this.pictureUserEntitiesToCreate);
                    }
                    if (this.pictureUserEntitiesToUpdate.Count != 0)
                    {
                        await this.dataAccess.UpdateFromList(this.pictureUserEntitiesToUpdate);
                    }

                    this.ResetList();
                    return new KeyValuePair<bool, List<PictureUser>>(true, listPictureUser);
                }

                this.ResetList();
                return new KeyValuePair<bool, List<PictureUser>>(true, listPictureUser);
            }
            catch (Exception)
            {
                this.ResetList();
                return new KeyValuePair<bool, List<PictureUser>>(false, listPictureUser);
            }
        }

        public async Task DeleteAllFromUserId(int id)
        {
            try
            {
                if (id != default)
                {
                    await this.dataAccess.DeleteAllFromUserId(id);
                }
            }
            catch (Exception)
            {
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<List<PictureUser>> FindAllFromListId(List<int> listId)
        {
            List<PictureUser> list = new List<PictureUser>();
            var result = await this.dataAccess.FindAllPictureByListUserId(listId);

            if (result != null)
            {
                result.ForEach(pictureUserEntities => list.Add(new PictureUser(pictureUserEntities)));
            }

            return list;
        }

        public async Task<List<PictureUser>> FindAllFromUserId(int id)
        {
            List<PictureUser> pictureUsers = new List<PictureUser>();

            try
            {
                if (id != default)
                {
                    var result = await this.dataAccess.FindAllPictureByUserId(id);

                    if (result != null)
                    {
                        result.ForEach(pictureUserEntities => pictureUsers.Add(new PictureUser(pictureUserEntities)));
                    }
                }

                return pictureUsers;
            }
            catch (Exception)
            {
                return pictureUsers;
            }
        }

        public KeyValuePair<bool, User> ValidateList(List<PictureUser> pictureUsers, User user)
        {
            try
            {
                bool dataIsValid = true;

                foreach (var pictureUser in pictureUsers)
                {
                    var entity = pictureUser.CreateEntity();
                    pictureUser.ValidationService.Validate(entity);
                    if (!pictureUser.ValidationService.IsValid)
                    {
                        dataIsValid = false;

                        foreach (var dictionaryError in pictureUser.ValidationService.ModelState)
                        {
                            user.ValidationService.AddError(dictionaryError.Key, dictionaryError.Value);
                        }
                    }
                }

                return new KeyValuePair<bool, User>(dataIsValid, user);
            }
            catch (Exception)
            {
                return new KeyValuePair<bool, User>(false, user);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dataAccess?.Dispose();
            }
        }

        private void ResetList()
        {
            this.pictureUserEntitiesToDelete.Clear();
            this.pictureUserEntitiesToCreate.Clear();
            this.pictureUserEntitiesToUpdate.Clear();
        }

        private async Task SetListToDeleteToCreateToUpdate(int id, List<PictureUser> pictureUsers)
        {
            Dictionary<int, PictureUserEntity> dictionary = await this.dataAccess.FindAllPictureByUserIdToDictionary(id);
            List<PictureUserEntity> pictureUserEntities = dictionary.Values.ToList();

            pictureUsers.ForEach(pictureUser =>
            {
                if (pictureUser.Id == default)
                {
                    DateTime createdDate = DateTime.UtcNow;
                    var entity = pictureUser.CreateEntity();
                    entity.IdUser = id;
                    entity.CreatedDate = createdDate;
                    this.pictureUserEntitiesToCreate.Add(entity);
                }

                if (pictureUser.Id != default)
                {
                    var entity = pictureUser.CreateEntity();
                    DateTime updatedDate = DateTime.UtcNow;
                    entity.UpdatedDate = updatedDate;
                    this.pictureUserEntitiesToUpdate.Add(entity);
                }

                if (dictionary.ContainsKey(pictureUser.Id))
                {
                    pictureUserEntities.Remove(pictureUserEntities.Where(entity => entity.Id == pictureUser.Id).FirstOrDefault());
                }
            });

            this.pictureUserEntitiesToDelete = pictureUserEntities;
        }
    }
}