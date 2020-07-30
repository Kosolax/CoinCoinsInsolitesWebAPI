namespace CoinCoinsInsolites.DataAccess.Seed
{
    using CoinCoinsInsolites.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PlaceSeed : IContextSeed
    {
        public PlaceSeed(CoinCoinsContext context)
        {
            this.Context = context;
        }

        public CoinCoinsContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.Places.Any() && !isProduction)
            {
                DateTime createdDate = DateTime.UtcNow;
                List<PlaceEntity> places = new List<PlaceEntity>
                {
                    new PlaceEntity
                    {
                        Id = 1,
                        Description = "Cet alignement est constitué de trois files de menhirs. Deux files sont côte à côte et sont orienté est/ouest. La troisième file est disposée 40 m plus à l'ouest. Elle est orienté nord-est/sud-ouest donc quasiment perpendiculaire aux deux autres. Cette file est longue de 12 m et est constituée d'une demi-douzaine de menhirs. L'orientation des trois files fait qu'en les prolongeant elles se croisent en un point unique.",
                        CreatedDate = createdDate,
                        Latitude = 47.764877,
                        Longitude = -1.974038,
                        Title = "L'alignement de Cojoux",
                    },
                    new PlaceEntity
                    {
                        Id = 2,
                        Description = "Le plus grand cairn d'Europe a failli disparaitre en 1955 sous les coups de boutoir des pelleteuses. Sans la perspicacité de Fanch Gourvil, journaliste à Morlaix, le Parthénon mégalithique, comme l'appelait André Malraux, ne pourrait plus nous émerveiller.",
                        CreatedDate = createdDate,
                        Latitude = 48.667770,
                        Longitude = -3.858109,
                        Title = "Le cairn de Barnenez",
                    },
                    new PlaceEntity
                    {
                        Id = 3,
                        Description = "Le porche de la grotte de Gournier s'ouvre à 572 m d'altitude au pied des falaises de Presles à proximité de la grotte touristique de Choranche. Le porche est occupé par un lac qui constitue une des exsurgences du massif des Coulmes et la rivière souterraine qu'abrite la grotte est considérée parmi les spéléologues comme l'une des plus belles des Alpes. La rivière n'est cependant pas accessible au commun des mortels.",
                        CreatedDate = createdDate,
                        Latitude = 45.072347,
                        Longitude = 45.072347,
                        Title = "Le lac souterrain de Gournier",
                    },
                };

                await this.Context.Places.AddRangeAsync(places);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}