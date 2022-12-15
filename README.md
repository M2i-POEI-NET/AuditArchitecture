# ContosoApp

Structurer une application microservices revient a la subdiviser en plusieurs applications autonomes (chacune doit pouvoir fonctionner individuellement) et de creer une communication entre elles afin de pouvoir repondre aux differentes specifications du projet.

Contoso est un systeme example d'application ASP .NET simple et compose de 3 tables 
COURSES, STUDENTS et ENROLLEMENT pour en savoir plus, bienvouloir suivre ce lient (https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-7.0).

Dans ce fichier nous deroulerons la demarche a suivre afin de realiser ce projet suivant une architecture microservices


# Etapes de realisations du projet de demo des architectures microservices ContosoApp

1) Structure du projet.

la solution ContosoApp est contituee de 3 services (un service n'est rien d'autre qu'un projet de type API), chaque services representant chacunes des tables sus-citees.

Chaque service est constituee de 2 applications (une librairie de classe pour gerer les entites et une application API), d'une base de donnees postgres embarquee dans un conteneur docker.

La communication entre ces services se fera via un bus d'evenements RabbitMQ et les routes d'acces au microservice seront configurees via une passerelle API


2) Creation des services

Dans des dossiers, creer les projets API et librairie de classe et implementer pour chacun les CRUD a l'aide de entity framework

3) Configuration de postgres

Faire un clique droit sur l'un des projets API puis, aller sur Add>"Container Orchestrator Support"
 Un nouveau projet se cree alors a la racine "docker-compose", definir ce dernier comme etant le projet de demarrage.

dans le fichier "docker-compose.yml", ajouter les lignes suivantes

  postgres_image:
    image: postgres:latest
    ports:
      - "5432"
    restart: always
    volumes:
      - db_volume:/var/lib/postgresql/data
    environment:
      POSTGRES_USER: "postgres"
      POSTGRES_PASSWORD: "Admin1*"
      POSTGRES_DB: "courseDb"

volumes:
  db_volume:

Dans ces configurations, nous ajoutons une image postgres dans le conteneur docker ecoutant sur le port 5432, qui va redemarrer a chaque fois qu'on effectuera un build du projet, et qui a comme username "postgres", comme MDP "Admin1*" et contient une base de donnees par defaut "courseDb"




