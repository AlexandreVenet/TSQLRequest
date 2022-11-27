# TSQLRequest

## Présentation 

Programme proposant des fonctions factorisées de requêtes Transact-SQL préparées (paramètres nommés par étiquettes). 

Fichiers prêts à l'emploi :
- fichier `.dll` du programe,
- fichier `.nupkg` de *package* NuGet.

Le programme fournit trois méthodes :
- `RequestReader()` : lire des données,
- `RequestScalar()` : retourne un objet représentant une valeur,
- `RequestNonQuery()` : retourne le nombre de lignes concernées.

Le programme fournit aussi un type `Parametre` pour préparer la requête.

## Exemple

Déclaration de l'espace de nom : 
```
using TSQLRequest;
```

Instruction pour une requête de verbe HTTP `GET` :
```
new Request().RequestReader(chaineDeConnexion, "SELECT * FROM MaTable;", (reader) =>
{
	int id = reader.GetInt32(0);
	string colonne1 = reader.GetString(1);
	DateTime colonne2 = reader.GetDateTime(2);
	Console.WriteLine($"\t{id}. {colonne1}, {colonne2}");
});
```
