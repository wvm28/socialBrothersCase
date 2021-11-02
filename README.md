# socialBrothersCase

Database vernieuwen stel dat deze niet meer werkt:
1. verwijder alle bestanden in de Database folder
2. open een console in het project
3. volg de stappen in de volgende video https://youtu.be/z-Hll4Xddjs?t=248 tot 6:17
4. In het kort zijn deze stappen
5. Installeer (als dit nog niet geinstalleerd is) dotnet tool door middel van "dotnet tool update --global dotnet-ef"
6. Run vervolgens "dotnet ef migrations add Initial -o Database"
7. Nadat dit is gebeurd run "dotnet ef database update"

Onderdelen waar ik trots of minder tevreden mee ben:
De api service die ik gebruikt heb om de afstand tussen twee addressen op te vragen.
Ik ben deels blij met hoe ik dit heb gemaakt en deels ook niet. Het is een klasse die zonder veel toevoegingen in een ander project kan worden geplaatst, het enige wat er aan setup nodig is om een api key toetevoegen aan de appsettings. Alleen heb ik de bouw/benaming gebruikt van Angular waar services als dit op deze manier worden opgebouwd waardoor het waarschijnlijk niet overeenkomt met de gebruikelijke C# manieren. 

over het algemeen:
Ik heb wat lang gedaan over het maken van de een simpele CRUD api. Dit kwam omdat ik C# al een tijd niet op deze manier heb gebruikt, maar wel een API wou neerzetten die niet afhaneklijk is van Plain MYSQL commando's die in strings worden bewaard. Hierdoor koste het wat tijd om echt op gang te komen waarbij Micha een aantal hints heeft gegeven, bijvoorbeeld om Entity framework te gebruiken. 
