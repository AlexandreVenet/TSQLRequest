using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQLRequest
{
	/// <summary>
	/// Type permettant de créer un SqlParameter pour une requête préparée avec étiquette de remplacement.
	/// </summary>
	public class Parametre
	{
		#region Champs

		/// <summary>
		/// Préfixe de l'étiquette nommée.
		/// </summary>
		private char _prefixe = '@';

		/// <summary>
		/// Masque complet (préfixe + nom).
		/// </summary>
		private string _masque;

		/// <summary>
		/// Type de la donnée en base de données.
		/// </summary>
		private SqlDbType _sqlDbType;

		/// <summary>
		/// Valeur de la donnée.
		/// </summary>
		private object _valeur;

		/// <summary>
		/// Si type numérique, nombre total de digits (precision).
		/// </summary>
		private byte _nbreDigits;

		/// <summary>
		/// Si type numérique, nombre de digits après la virgule (scale).
		/// </summary>
		private byte _nbreDecimales;

		/// <summary>
		/// Si donnée de longueur variable (ex : VARCHAR), alors la taille de la donnée.
		/// </summary>
		private int _taille;

		#endregion



		#region Constructeurs

		/// <summary>
		/// Constructeur vide.
		/// </summary>
		public Parametre() { }

		/// <summary>
		/// Définir un paramètre général.
		/// </summary>
		/// <param name="masque">Nom de l'étiquette nommée.</param>
		/// <param name="sqlDbType">Le type de la donnée en base de données.</param>
		/// <param name="valeur">La valeur de la donnée.</param>
		public Parametre(string masque, SqlDbType sqlDbType, object valeur)
		{
			_masque = _prefixe + masque;
			_valeur = valeur;
			_sqlDbType = sqlDbType;

			// Si le type est de longueur variable (cas des strings) alors spécifier la longueur sur le nombre de caractères
			if (_sqlDbType == SqlDbType.VarChar || _sqlDbType == SqlDbType.NVarChar || _sqlDbType == SqlDbType.Text || _sqlDbType == SqlDbType.NText)
			{
				_taille = _valeur.ToString().Length;
			}
		}

		/// <summary>
		/// Définir pour valeur numérique.
		/// </summary>
		/// <param name="masque">Nom de l'étiquette nommée.</param>
		/// <param name="sqlDbType">Le type de la donnée en base de données.</param>
		/// <param name="valeur">La valeur de la donnée.</param>
		/// <param name="nbreDigits">Nombre total de digits.</param>
		/// <param name="nbreDecimales">Nombre de digits après la virgule.</param>
		public Parametre(string masque, SqlDbType sqlDbType, object valeur, byte nbreDigits, byte nbreDecimales)
		{
			_masque = _prefixe + masque;
			_valeur = valeur;
			_sqlDbType = sqlDbType;
			_nbreDigits = nbreDigits;
			_nbreDecimales = nbreDecimales;
		}

		#endregion



		#region Fonctions

		/// <summary>
		/// Construire le paramètre à partir des données de cet objet.
		/// </summary>
		/// <returns></returns>
		public SqlParameter Construire()
		{
			SqlParameter parametre = new();

			parametre.ParameterName = _masque;
			parametre.SqlDbType = _sqlDbType;
			parametre.Value = _valeur;

			// Selon le type (compléter au besoin)
			switch (_sqlDbType)
			{
				case SqlDbType.NText:
				case SqlDbType.NVarChar:
				case SqlDbType.Text:
				case SqlDbType.VarChar:
					parametre.Size = _taille;
					break;
				case SqlDbType.Float:
				case SqlDbType.Decimal:
				case SqlDbType.Money:
				case SqlDbType.SmallMoney:
				case SqlDbType.Real:
					parametre.Precision = _nbreDigits;
					parametre.Scale = _nbreDecimales;
					break;
					/*case SqlDbType.BigInt:
						break;
					case SqlDbType.Binary:
						break;
					case SqlDbType.Bit:
						break;
					case SqlDbType.Char:
						break;
					case SqlDbType.DateTime:
						break;
					case SqlDbType.Image:
						break;
					case SqlDbType.Int:
						break;
					case SqlDbType.NChar:
						break;
					case SqlDbType.UniqueIdentifier:
						break;
					case SqlDbType.SmallDateTime:
						break;
					case SqlDbType.SmallInt:
						break;
					case SqlDbType.Timestamp:
						break;
					case SqlDbType.TinyInt:
						break;
					case SqlDbType.VarBinary:
						break;
					case SqlDbType.Variant:
						break;
					case SqlDbType.Xml:
						break;
					case SqlDbType.Udt:
						break;
					case SqlDbType.Structured:
						break;
					case SqlDbType.Date:
						break;
					case SqlDbType.Time:
						break;
					case SqlDbType.DateTime2:
						break;
					case SqlDbType.DateTimeOffset:
						break;
					default:
						break;*/
			}

			return parametre;
		}

		#endregion
	}
}
