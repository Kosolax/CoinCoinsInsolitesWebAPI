namespace CoinCoinsInsolites.Business
{
    using CoinCoinsInsolites.BusinessObject;
    using CoinCoinsInsolites.BusinessObject.Validation.Resources;
    using CoinCoinsInsolites.Entities;
    using CoinCoinsInsolites.IBusiness;
    using CoinCoinsInsolites.IDataAccess;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PlaceBusiness : IPlaceBusiness
    {
        private readonly IPlaceDataAccess dataAccess;

        private readonly IPicturePlaceBusiness picturePlaceBusiness;

        public PlaceBusiness(IPlaceDataAccess dataAccess, IPicturePlaceBusiness picturePlaceBusiness)
        {
            this.dataAccess = dataAccess;
            this.picturePlaceBusiness = picturePlaceBusiness;
        }

        public async Task<KeyValuePair<bool, Place>> Create(Place itemToCreate)
        {
            try
            {
                if (itemToCreate != null)
                {
                    PlaceEntity entity = itemToCreate.CreateEntity();
                    var result = this.picturePlaceBusiness.ValidateList(itemToCreate.ListPicturePlace, itemToCreate);

                    if (itemToCreate.ValidationService.Validate(entity) && result.Key)
                    {
                        if (entity.Id == default)
                        {
                            entity = await this.dataAccess.Create(entity);
                            var resultFromPicturePlace = await this.picturePlaceBusiness.CreateOrUpdateFromList(entity.Id, itemToCreate.ListPicturePlace);

                            if (!resultFromPicturePlace.Key)
                            {
                                await this.Delete(entity.Id);
                                itemToCreate.ValidationService.AddError(nameof(PlaceValidationResources.ListPicturePlace_GoneWrong), PlaceValidationResources.ListPicturePlace_GoneWrong);
                                return new KeyValuePair<bool, Place>(false, itemToCreate);
                            }

                            itemToCreate = await this.GetPlaceFromEntity(entity);

                            return new KeyValuePair<bool, Place>(true, itemToCreate);
                        }
                    }
                }

                return new KeyValuePair<bool, Place>(false, itemToCreate);
            }
            catch (Exception)
            {
                return new KeyValuePair<bool, Place>(false, itemToCreate);
            }
        }

        public async Task<KeyValuePair<bool, Place>> Delete(int id)
        {
            Place place = await this.Get(id);

            try
            {
                if (place != null)
                {
                    await this.picturePlaceBusiness.DeleteAllFromPlaceId(id);
                    await this.dataAccess.Delete(id);
                    return new KeyValuePair<bool, Place>(true, place);
                }

                return new KeyValuePair<bool, Place>(false, place);
            }
            catch (Exception)
            {
                return new KeyValuePair<bool, Place>(false, place);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<List<Place>> FindMostRecentPlace(int count)
        {
            List<Place> places = new List<Place>();

            try
            {
                var result = await this.dataAccess.FindMostRecentPlaceEntity(count);
                places = await this.GetPlacesFromEntity(result);

                return places;
            }
            catch (Exception)
            {
                return places;
            }
        }

        public async Task<Place> Get(int id)
        {
            Place place = null;

            try
            {
                PlaceEntity entity = await this.dataAccess.Find(id);

                if (entity != null)
                {
                    place = await this.GetPlaceFromEntity(entity);
                }

                return place;
            }
            catch (Exception)
            {
                return place;
            }
        }

        public async Task<List<Place>> List()
        {
            List<Place> places = new List<Place>();

            try
            {
                var result = await this.dataAccess.List();
                places = await this.GetPlacesFromEntity(result);

                return places;
            }
            catch (Exception)
            {
                return places;
            }
        }

        public async Task<KeyValuePair<bool, Place>> Update(int id, Place itemToUpdate)
        {
            try
            {
                if (itemToUpdate != null && id != default)
                {
                    itemToUpdate.Id = id;
                    PlaceEntity entity = itemToUpdate.CreateEntity();
                    var result = this.picturePlaceBusiness.ValidateList(itemToUpdate.ListPicturePlace, itemToUpdate);

                    if (itemToUpdate.ValidationService.Validate(entity) && result.Key)
                    {
                        if (entity.Id != default)
                        {
                            entity = await this.dataAccess.Update(entity, entity.Id);
                            var resultFromPicturePlace = await this.picturePlaceBusiness.CreateOrUpdateFromList(id, itemToUpdate.ListPicturePlace);

                            if (!resultFromPicturePlace.Key)
                            {
                                await this.Delete(entity.Id);
                                itemToUpdate.ValidationService.AddError(nameof(PlaceValidationResources.ListPicturePlace_GoneWrong), PlaceValidationResources.ListPicturePlace_GoneWrong);
                                return new KeyValuePair<bool, Place>(false, itemToUpdate);
                            }

                            itemToUpdate = await this.GetPlaceFromEntity(entity);

                            return new KeyValuePair<bool, Place>(true, itemToUpdate);
                        }
                    }

                    itemToUpdate = result.Value;
                }

                return new KeyValuePair<bool, Place>(false, itemToUpdate);
            }
            catch (Exception)
            {
                return new KeyValuePair<bool, Place>(false, itemToUpdate);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dataAccess?.Dispose();
                this.picturePlaceBusiness?.Dispose();
            }
        }

        private async Task<Place> GetPlaceFromEntity(PlaceEntity entity)
        {
            Place place = new Place(entity);
            place.ListPicturePlace = await this.picturePlaceBusiness.FindAllFromPlaceId(entity.Id);

            return place;
        }

        private async Task<List<Place>> GetPlacesFromEntity(List<PlaceEntity> placeEntities)
        {
            List<int> listId = placeEntities.Select(obj => obj.Id).Distinct().ToList();
            List<PicturePlace> picturePlacesAllClient = await this.picturePlaceBusiness.FindAllFromListId(listId);
            List<Place> places = new List<Place>();

            foreach (var placeEntity in placeEntities)
            {
                // We could have just done this :
                // places.Add(await this.GetPlaceFromEntity(placeEntity));
                // and return places;
                // But it means more time and request to the db
                // I decided that i prefer to use the CPU for that instead of DTU (calcul unit for azure db)
                List<PicturePlace> picturePlacesForThisClient = new List<PicturePlace>();

                picturePlacesAllClient.ForEach(picturePlace =>
                {
                    if (picturePlace.IdPlace == placeEntity.Id)
                    {
                        picturePlacesForThisClient.Add(picturePlace);
                    }
                });

                Place place = new Place(placeEntity);
                place.ListPicturePlace = picturePlacesForThisClient;
                places.Add(await this.GetPlaceFromEntity(placeEntity));
            }

            return places;
        }
    }
}