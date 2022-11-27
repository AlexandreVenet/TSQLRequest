using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;

namespace TSQLRequest
{
	public class Request
	{
		/// <summary>
		/// Exécuter une requête préparée avec étiquettes nommées, sans retour, pour lire des données.<br/>
		/// Exemple : GET.
		/// </summary>
		/// <param name="chaineConnexion">Chaîne de connexion.</param>
		/// <param name="requete">Chaîne de requête.</param>
		/// <param name="action">Fonction callback de traitement lors de la lecture.</param>
		/// <param name="parametres">Tableau optionnel de paramètres.</param>
		public void RequestReader(string chaineConnexion, string requete, Action<SqlDataReader> action, Parametre[] parametres = null)
		{
			try
			{
				using (SqlConnection connexion = new(chaineConnexion))
				{
					using (SqlCommand commande = new(requete, connexion))
					{
						// Ouverture de la connexion
						connexion.Open();

						// Préparation de la requête avec étiquette nommée
						if (parametres != null && parametres.Length > 0)
						{
							for (int i = 0; i < parametres.Length; i++)
							{
								commande.Parameters.Add(parametres[i].Construire());
							}
						}

						commande.Prepare();

						using (SqlDataReader reader = commande.ExecuteReader())
						{
							while (reader.Read())
							{
								action(reader);
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"\tErreur de requête\n\t{e.GetType()}\n\t{e.Message}");
			}
		}

		/// <summary>
		/// Exécuter une requête préparée avec étiquettes nommées, ayant pour retour un objet (ou null) représentant une valeur.<br/>
		/// Exemple : POST, DELETE.
		/// </summary>
		/// <param name="chaineConnexion">Chaîne de connexion.</param>
		/// <param name="requete">Chaîne de requête.</param>
		/// <param name="parametres">Tableau optionnel de paramètres.</param>
		/// <returns>Objet (ou null) représentant une valeur.</returns>
		public object RequestScalar(string chaineConnexion, string requete, Parametre[] parametres = null)
		{
			object retour = null;

			try
			{
				using (SqlConnection connexion = new(chaineConnexion))
				{
					using (SqlCommand commande = new(requete, connexion))
					{
						// Ouverture de la connexion
						connexion.Open();

						// Préparation de la requête avec étiquette nommée
						if (parametres != null && parametres.Length > 0)
						{
							for (int i = 0; i < parametres.Length; i++)
							{
								commande.Parameters.Add(parametres[i].Construire());
							}
						}

						commande.Prepare();

						retour = commande.ExecuteScalar();
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"\tErreur de requête\n\t{e.GetType()}\n\t{e.Message}");
			}

			return retour;
		}

		/// <summary>
		/// Exécuter une requête préparée avec étiquettes nommées, ayant pour retour le nombre de lignes concernéees (0 si erreur ou pas d'action).<br/>
		/// Exemple : PUT.
		/// </summary>
		/// <param name="chaineConnexion">Chaîne de connexion.</param>
		/// <param name="requete">Chaîne de requête.</param>
		/// <param name="parametres">Tableau optionnel de paramètres.</param>
		/// <returns>Nombre entier représentant le nombre de lignes concernées par la requête.</returns>
		public int RequestNonQuery(string chaineConnexion, string requete, Parametre[] parametres = null)
		{
			int retour = -1;

			try
			{
				using (SqlConnection connexion = new(chaineConnexion))
				{
					using (SqlCommand commande = new(requete, connexion))
					{
						// Ouverture de la connexion
						connexion.Open();

						// Préparation de la requête avec étiquette nommée
						if (parametres != null && parametres.Length > 0)
						{
							for (int i = 0; i < parametres.Length; i++)
							{
								commande.Parameters.Add(parametres[i].Construire());
							}
						}

						commande.Prepare();

						retour = commande.ExecuteNonQuery();
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"\tErreur de requête\n\t{e.GetType()}\n\t{e.Message}");
			}

			return retour;
		}
	}
}