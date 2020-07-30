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

    public class PicturePlaceBusiness : IPicturePlaceBusiness
    {
        private readonly IPicturePlaceDataAccess dataAccess;

        private List<PicturePlaceEntity> picturePlaceEntitiesToCreate = new List<PicturePlaceEntity>();

        private List<PicturePlaceEntity> picturePlaceEntitiesToDelete = new List<PicturePlaceEntity>();

        private List<PicturePlaceEntity> picturePlaceEntitiesToUpdate = new List<PicturePlaceEntity>();

        public PicturePlaceBusiness(IPicturePlaceDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public async Task<KeyValuePair<bool, List<PicturePlace>>> CreateOrUpdateFromList(int id, List<PicturePlace> listPicturePlace)
        {
            try
            {
                if (listPicturePlace != null && listPicturePlace.Count != 0)
                {
                    // We don't check if objects are correct because we'll do it in Place
                    await this.SetListToDeleteToCreateToUpdate(id, listPicturePlace);

                    if (this.picturePlaceEntitiesToDelete.Count != 0)
                    {
                        this.dataAccess.DeleteAllFromList(this.picturePlaceEntitiesToDelete);
                    }
                    if (this.picturePlaceEntitiesToCreate.Count != 0)
                    {
                        await this.dataAccess.CreateFromList(this.picturePlaceEntitiesToCreate);
                    }
                    if (this.picturePlaceEntitiesToUpdate.Count != 0)
                    {
                        await this.dataAccess.UpdateFromList(this.picturePlaceEntitiesToUpdate);
                    }

                    this.ResetList();
                    return new KeyValuePair<bool, List<PicturePlace>>(true, listPicturePlace);
                }

                this.ResetList();
                return new KeyValuePair<bool, List<PicturePlace>>(true, listPicturePlace);
            }
            catch (Exception)
            {
                this.ResetList();
                return new KeyValuePair<bool, List<PicturePlace>>(false, listPicturePlace);
            }
        }

        public async Task DeleteAllFromPlaceId(int id)
        {
            try
            {
                if (id != default)
                {
                    await this.dataAccess.DeleteAllFromPlaceId(id);
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

        public async Task<List<PicturePlace>> FindAllFromListId(List<int> listId)
        {
            List<PicturePlace> list = new List<PicturePlace>();
            var result = await this.dataAccess.FindAllPictureByListPlaceId(listId);

            if (result != null)
            {
                result.ForEach(picturePlaceEntities => list.Add(new PicturePlace(picturePlaceEntities)));
            }

            return list;
        }

        public async Task<List<PicturePlace>> FindAllFromPlaceId(int id)
        {
            List<PicturePlace> picturePlaces = new List<PicturePlace>();

            try
            {
                if (id != default)
                {
                    var result = await this.dataAccess.FindAllPictureByPlaceId(id);

                    if (result != null)
                    {
                        result.ForEach(picturePlaceEntities => picturePlaces.Add(new PicturePlace(picturePlaceEntities)));
                    }
                }

                return picturePlaces;
            }
            catch (Exception)
            {
                return picturePlaces;
            }
        }

        public KeyValuePair<bool, Place> ValidateList(List<PicturePlace> picturePlaces, Place place)
        {
            try
            {
                bool dataIsValid = true;

                foreach (var picturePlace in picturePlaces)
                {
                    var entity = picturePlace.CreateEntity();
                    picturePlace.ValidationService.Validate(entity);
                    if (!picturePlace.ValidationService.IsValid)
                    {
                        dataIsValid = false;

                        foreach (var dictionaryError in picturePlace.ValidationService.ModelState)
                        {
                            place.ValidationService.AddError(dictionaryError.Key, dictionaryError.Value);
                        }
                    }
                }

                return new KeyValuePair<bool, Place>(dataIsValid, place);
            }
            catch (Exception)
            {
                return new KeyValuePair<bool, Place>(false, place);
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
            this.picturePlaceEntitiesToDelete.Clear();
            this.picturePlaceEntitiesToCreate.Clear();
            this.picturePlaceEntitiesToUpdate.Clear();
        }

        private async Task SetListToDeleteToCreateToUpdate(int id, List<PicturePlace> picturePlaces)
        {
            Dictionary<int, PicturePlaceEntity> dictionary = await this.dataAccess.FindAllPictureByPlaceIdToDictionary(id);
            List<PicturePlaceEntity> picturePlaceEntities = dictionary.Values.ToList();

            picturePlaces.ForEach(picturePlace =>
            {
                if (picturePlace.Id == default)
                {
                    DateTime createdDate = DateTime.UtcNow;
                    var entity = picturePlace.CreateEntity();
                    entity.IdPlace = id;
                    entity.CreatedDate = createdDate;
                    this.picturePlaceEntitiesToCreate.Add(entity);
                }

                if (picturePlace.Id != default)
                {
                    var entity = picturePlace.CreateEntity();
                    DateTime updatedDate = DateTime.UtcNow;
                    entity.UpdatedDate = updatedDate;
                    this.picturePlaceEntitiesToUpdate.Add(entity);
                }

                if (dictionary.ContainsKey(picturePlace.Id))
                {
                    picturePlaceEntities.Remove(picturePlaceEntities.Where(entity => entity.Id == picturePlace.Id).FirstOrDefault());
                }
            });

            this.picturePlaceEntitiesToDelete = picturePlaceEntities;
        }
    }
}