# CoinCoinsInsolitesWebAPI

Voir d'abord : [La même web api mais en java et en moins bien](https://github.com/Kosolax/CoinCoins_Insolites_Web_Api_Java)

## Choix de la technologie et de l'approche

Le projet a été fait en C# ASP .NET Core avec entity framework (un orm). Pour faciliter le développement pour l'équipe, nous avons décidé de faire une approche "Code First" pour la web Api.
Cette approche va nous permettre de développer notre WEB API dans notre coin et une fois fini de juste l'envoyer. La personne qui en aura besoin aura juste à lancer un Sql et la web api. La base de données va se créer toute seule ainsi que des données.


## Fonctionnalité de la web api

Puisqu'on utilise un ORM tout ce qui est relié au problème de sécurité sont pour la plus part corrigé ! En effet l'injection SQL et ce genre de chose sont déjà géré par l'orm.

* Une architecture
* Les champs importants de la base de données sont cryptés
* Validation d'objets (pour éviter de rentrer de la mauvaise donnée en base de données)
* Seed (comme faker mais on le fait nous-mêmes dans l'api)
* CRUD disponible pour d'autres clients sur les objets User et Place (On n'avait pas forcément besoin de tout le CRUD pour l'exercice mais tant qu'à faire j'ai tout fait)
* Contrairement au java, tous les appels ici sont asynchrones

Contrairement à la web api en java il n'y a pas de TU car je n'ai pas eu le temps de les refaire.


## Architecture du projet

* Business Object : Objet métier. Par exemple en base de données un user ne possède pas de liste de photo. Pourtant dans l'application c'est le cas. Donc l'objet User métier doit posséder une liste de photo.

* Business : Logique qui permet de faire les opérations sur les objets métier. Par exemple quand on met un jour un user on doit mettre à jour ses photos au passage. Pourtant c'est une autre table dans la base de données mais d'un point de vue métier c'est ce qui est attendu. Le business sert donc à relier les objets entre eux pour une utilisation plus intuitive de l'api. 

* Entities : Objet qui se map directement sur les entités de la base de données.

* DataAccess : Logique qui permet de faire les opérations sql sur la base de donnée (elle utilise donc les entités)

* IBusiness : Pour les injections de dépendance. Elles doivent implémenter IDisposable pour fermer les ressources quand c'est possible.

* IDataAccess : Pour les injections de dépendance

* WebApi : Ici il y a les controllers et les fichiers de config. C# met à disposition IConfiguration qui permet de lire dans les fichiers de configuration. J'ai donc mis les clés de cryptage dedans. Les controllers permettent d'exposer les fonctions disponibles aux autres clients.

* WebApi.Route : Une bibliothèque de constant pour définir les routes des controllers
