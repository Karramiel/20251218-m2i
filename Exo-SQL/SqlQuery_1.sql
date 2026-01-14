DROP DATABASE tabletoptreasures_database ;

/* === Étape 1 : Création de la Base de Données et des Tables === */

CREATE DATABASE IF NOT EXISTS tabletoptreasures_database;

USE tabletoptreasures_database;

CREATE TABLE IF NOT EXISTS Clients (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Nom VARCHAR(50) NOT NULL,
    Prenom VARCHAR(50) NOT NULL,
    Adresse_mail VARCHAR(100) NOT NULL,
    Adresse_de_livraison VARCHAR(200),
    Telephone VARCHAR(20)
);
CREATE TABLE IF NOT EXISTS Categories (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Nom VARCHAR(50) NOT NULL
);  

CREATE TABLE IF NOT EXISTS Jeux (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Nom VARCHAR(50) NOT NULL,
    Description TEXT NOT NULL,
    Prix DECIMAL(10, 2) NOT NULL,
    ID_Categorie INT DEFAULT 0,
    FOREIGN KEY (ID_Categorie) REFERENCES Categories(ID)
);



CREATE TABLE IF NOT EXISTS Commandes (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    ID_Client INT NOT NULL,
    FOREIGN KEY (ID_Client) REFERENCES Clients(ID),
    ID_Jeux INT NOT NULL,
    FOREIGN KEY (ID_Jeux) REFERENCES Jeux(ID),
    Date_de_commande DATETIME DEFAULT CURRENT_TIMESTAMP,
    Adresse_de_livraison VARCHAR(200),
    Statut VARCHAR(20) DEFAULT 'En cours'
);

/* === Étape 2 : Opérations en DML === */

INSERT INTO Categories (Nom) VALUES
('Stratégie'),
('Familial'),
('Aventure');

INSERT INTO Jeux (Nom, Description, Prix, ID_Categorie) VALUES
('Catan', 'Jeu de stratégie et de développement de colonies', 30, 1),
('Dixit', 'Jeu d''association d''images', 25, 2),
('Les Aventuriers', 'Jeu de plateau d''aventure', 40, 3),
('Carcassonne', 'Jeu de placement de tuiles', 28, 1),
('Codenames', 'Jeu de mots et d''indices', 20, 2),
('Pandemic', 'Jeu de coopération pour sauver le monde', 35, 3),
('7 Wonders', 'Jeu de cartes et de civilisations', 29, 1),
('Splendor', 'Jeu de développement économique', 27, 2),
('Horreur à Arkham', 'Jeu d''enquête et d''horreur', 45, 3),
('Risk', 'Jeu de conquête mondiale', 22, 1),
('Citadelles', 'Jeu de rôles et de bluff', 23, 2),
('Terraforming Mars', 'Jeu de stratégie de colonisation de Mars', 55, 3),
('Small World', 'Jeu de civilisations fantastiques', 32, 1),
('7 Wonders Duel', 'Jeu de cartes pour 2 joueurs', 26, 2),
('Horreur à l''Outreterre', 'Jeu d''aventure horrifique', 38, 3);

INSERT INTO Clients (Nom, Prenom, Adresse_mail, Adresse_de_livraison, Telephone) VALUES
('Dubois', 'Marie', 'marie.dubois@example.com', '123 Rue de la Libération, Ville', '+1234567890'),
('Lefebvre', 'Thomas', 'thomas.lefebvre@example.com', '456 Avenue des Roses, Ville', '+9876543210'),
('Martinez', 'Léa', 'lea.martinez@example.com', '789 Boulevard de la Paix, Ville', '+2345678901'),
('Dupuis', 'Antoine', 'antoine.dupuis@example.com', '567 Avenue de la Liberté, Ville', '+3456789012'),
('Morin', 'Camille', 'camille.morin@example.com', '890 Rue de l''Avenir, Ville', '+4567890123'),
('Girard', 'Lucas', 'lucas.girard@example.com', '234 Avenue des Champs, Ville', '+5678901234'),
('Petit', 'Emma', 'emma.petit@example.com', '123 Rue des Étoiles, Ville', '+6789012345'),
('Sanchez', 'Gabriel', 'gabriel.sanchez@example.com', '345 Boulevard du Bonheur, Ville', '+7890123456'),
('Rossi', 'Clara', 'clara.rossi@example.com', '678 Avenue de la Joie, Ville', '+8901234567'),
('Lemoine', 'Hugo', 'hugo.lemoine@example.com', '456 Rue de la Nature, Ville', '+9012345678'),
('Moreau', 'Eva', 'eva.moreau@example.com', '789 Avenue de la Créativité, Ville', '+1234567890'),
('Fournier', 'Noah', 'noah.fournier@example.com', '234 Rue de la Découverte, Ville', '+2345678901'),
('Leroy', 'Léa', 'lea.leroy@example.com', '567 Avenue de l''Imagination, Ville', '+3456789012'),
('Robin', 'Lucas', 'lucas.robin@example.com', '890 Rue de la Création, Ville', '+4567890123'),
('Marchand', 'Anna', 'anna.marchand@example.com', '123 Boulevard de l''Innovation, Ville', '+5678901234');

INSERT INTO Commandes (ID_Client,ID_Jeux, Date_de_commande, Adresse_de_livraison, Statut) VALUES
(1, 1, '2023-10-01 10:00:00', '123 Rue de la Libération, Ville', 'En cours'),
(2, 2, '2023-10-02 11:30:00', '456 Avenue des Roses, Ville', 'Expédiée'),
(4, 2, '2023-10-03 11:30:00', '456 Avenue des Roses, Ville', 'Expédiée'),
(5, 3, '2023-10-03 14:15:00', '789 Boulevard de la Paix, Ville', 'Livrée');


UPDATE Jeux
SET Prix = 35
WHERE ID = 3;



DELETE FROM Jeux
WHERE ID = 4;

/* === Étape 3 : Requêtes SQL simples === */
/* - Table "Categories" :*/

/* xxx */
SELECT DISTINCT Nom 
FROM Categories;

/* xxx */
SELECT Nom 
FROM Categories 
WHERE Nom LIKE 'A%' OR Nom LIKE 'S%';

/* xxx */
SELECT Nom 
FROM Categories 
WHERE ID BETWEEN 2 AND 5;

/* xxx */
SELECT COUNT(DISTINCT Nom) AS Nombre_de_Categories 
FROM Categories;

/* xxx */
SELECT Nom, LENGTH(Nom) AS Longueur 
FROM Categories 
ORDER BY LENGTH(Nom) DESC
LIMIT 1;




/* xxx */
SELECT c.Nom AS Categorie, COUNT(j.ID) AS NombreJeux
FROM Categories c
LEFT JOIN Jeux j ON c.ID = j.ID_Categorie
GROUP BY c.ID, c.Nom;

/* xxx */
SELECT DISTINCT Nom 
FROM Categories
ORDER BY Nom DESC;


/* - Table "Jeux" :*/

/* 1. Sélectionnez tous les noms de jeux distincts. */
SELECT DISTINCT Nom
FROM Jeux;

/* 2. Montrez les jeux avec un prix entre 25 et 40. */
SELECT Nom, Prix
FROM Jeux 
WHERE Prix BETWEEN 25 AND 40;

/* 3. Quels jeux appartiennent à la catégorie avec l'ID 3 ? */
SELECT Nom
FROM Jeux 
WHERE ID_Categorie  = 3;

/* 4. Combien de jeux ont une description contenant le mot "aventure" ? */

SELECT COUNT(*) AS Nb_Jeux_Avec_Aventure
FROM Jeux
WHERE Description LIKE '%aventure%';

/* 5. Quel est le jeu le moins cher ? */
SELECT Nom,Prix
FROM Jeux 
ORDER BY Prix ASC
LIMIT 1;

/* 6. Montrez la somme totale des prix de tous les jeux. */
SELECT SUM(Prix) AS Prix_Total
From Jeux;

/* Affichez les jeux triés par ordre alphabétique des noms en limitant les résultats à
5. */

SELECT DISTINCT Nom 
FROM Jeux
ORDER BY Nom ASC
LIMIT 5;



/* - Table "Client" */

/* 1. Sélectionnez tous les prénoms des clients distincts. */
SELECT DISTINCT Prenom
FROM Clients;

/* 2. Montrez les clients dont l'adresse contient "Rue" et dont le numéro de téléphone commence par "+1". */
SELECT *
FROM Clients
WHERE Adresse_de_livraison LIKE '%Rue%' AND Telephone LIKE '+1%';

/* 3. Quels clients ont un nom commençant par "M" ou "R" ?  */
SELECT *
FROM Clients
WHERE Nom LIKE 'M%' OR Nom LIKE 'R%';

/* 4. Combien de clients ont une adresse e-mail valide (contenant "@") ?  */
SELECT COUNT(*) AS Nb_Clients_Email_Valide
FROM Clients
WHERE Adresse_mail LIKE '%@%.%' 
AND (Adresse_mail  LIKE '%.com' OR Adresse_mail LIKE '%.fr');

/* 5. Quel est le prénom le plus court parmi les clients ? */
SELECT Prenom
FROM Clients
ORDER BY LENGTH(Prenom) ASC
LIMIT 1;    

/* 6. Montrez le nombre total de clients enregistrés. */
SELECT COUNT(*) AS Nb_Clients_Enregistres
FROM Clients;

/* 7. Affichez les clients triés par ordre alphabétique des noms de famille, mais en excluant les premiers 3. */
SELECT *
FROM Clients
ORDER BY Nom ASC
LIMIT 3;


/* === Étape 4 : Requêtes SQL avancées === */

/* 1. Sélectionnez les noms des clients, noms de jeux et date de commande pour
chaque commande passée.*/

SELECT 
    cl.Nom AS Nom_Client, 
    j.Nom AS Nom_Jeu,
    co.Date_de_commande
FROM Commandes co
JOIN Clients cl ON co.ID_Client = cl.ID
JOIN Jeux j ON co.ID_Jeux = j.ID;


/* 2. Sélectionnez les noms des clients et le montant total dépensé par chaque client.
Triez les résultats par montant total décroissant.*/
SELECT 
    cl.Nom AS Nom_Client,
    SUM(j.Prix) AS Montant_Total
FROM Commandes co
JOIN Clients cl ON co.ID_Client = cl.ID
JOIN Jeux j ON co.ID_Jeux = j.ID
GROUP BY cl.ID, cl.Nom
ORDER BY Montant_Total DESC;

/* 3. Sélectionnez les noms des jeux, noms de catégories et prix de chaque jeu.*/
SELECT 
    j.Nom AS Nom_Jeu, 
    c.Nom AS Nom_Categorie,
    j.Prix
FROM Jeux j
JOIN Categories c ON j.ID_Categorie = c.ID;


/* 4. Sélectionnez les noms des clients, date de commande et noms de jeux pour
toutes les commandes passées.*/
SELECT 
    cl.Nom AS Nom_Client,
    co.Date_de_commande,
    j.Nom AS Nom_Jeu
FROM Commandes co
JOIN Clients cl ON co.ID_Client = cl.ID
JOIN Jeux j ON co.ID_Jeux = j.ID
JOIN Categories c ON j.ID_Categorie = c.ID; 


/* 5. Sélectionnez les noms des clients, nombre total de commandes par client et
montant total dépensé par client. Incluez uniquement les clients ayant effectué
au moins une commande.*/

SELECT 
    cl.Nom AS Nom_Client, 
    COUNT(co.ID) AS Nb_de_Commandes,
    SUM(j.Prix) AS Montant_Total
FROM Commandes co
JOIN Clients cl ON co.ID_Client = cl.ID
JOIN Jeux j ON co.ID_Jeux = j.ID
GROUP BY cl.ID, cl.Nom
HAVING COUNT(co.ID) > 0;
