CREATE DATABASE IF NOT EXISTS recette_ado;
USE recette_ado;

--Désactivation temporaire des contraintes
SET FOREIGN_KEY_CHECKS = 0;

--Suppression des tables si elles existent
DROP TABLE IF EXISTS recette_ingredient;
DROP TABLE IF EXISTS etape;
DROP TABLE IF EXISTS commentaire;
DROP TABLE IF EXISTS recette;
DROP TABLE IF EXISTS ingredient;
DROP TABLE IF EXISTS categorie;

SET FOREIGN_KEY_CHECKS = 1;

--Tables
CREATE TABLE categorie (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nom VARCHAR(50) NOT NULL
);

CREATE TABLE recette (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nom VARCHAR(100) NOT NULL,
    temps_prep INT NOT NULL,
    temps_cuisson INT NOT NULL,
    difficulte VARCHAR(20) NOT NULL,
    categorie_id INT,
    FOREIGN KEY (categorie_id) REFERENCES categorie(id)
);

CREATE TABLE ingredient (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nom VARCHAR(50) NOT NULL
);

CREATE TABLE recette_ingredient (
    recette_id INT NOT NULL,
    ingredient_id INT NOT NULL,
    PRIMARY KEY (recette_id, ingredient_id),
    FOREIGN KEY (recette_id) REFERENCES recette(id) ON DELETE CASCADE,
    FOREIGN KEY (ingredient_id) REFERENCES ingredient(id) ON DELETE CASCADE
);

CREATE TABLE etape (
    id INT AUTO_INCREMENT PRIMARY KEY,
    description TEXT NOT NULL,
    recette_id INT NOT NULL,
    FOREIGN KEY (recette_id) REFERENCES recette(id) ON DELETE CASCADE
);

CREATE TABLE commentaire (
    id INT AUTO_INCREMENT PRIMARY KEY,
    description TEXT NOT NULL,
    recette_id INT NOT NULL,
    FOREIGN KEY (recette_id) REFERENCES recette(id) ON DELETE CASCADE
);

--Données de base

INSERT INTO categorie (nom) VALUES
('Entrée'),
('Plat'),
('Dessert');

INSERT INTO ingredient (nom) VALUES
('Oeuf'),
('Farine'),
('Lait'),
('Sucre'),
('Beurre'),
('Poulet'),
('Riz'),
('Tomate');

INSERT INTO recette (nom, temps_prep, temps_cuisson, difficulte, categorie_id) VALUES
('Crêpes', 10, 15, 'Facile', 3),
('Poulet au riz', 20, 40, 'Moyenne', 2);

INSERT INTO recette_ingredient (recette_id, ingredient_id) VALUES
(1, 1),
(1, 2),
(1, 3),
(1, 4),
(2, 6),
(2, 7),
(2, 8);

INSERT INTO etape (description, recette_id) VALUES
('Mélanger les ingrédients', 1),
('Cuire à la poêle', 1),
('Cuire le poulet', 2),
('Ajouter le riz', 2);

INSERT INTO commentaire (description, recette_id) VALUES
('Très simple et rapide', 1),
('Bon plat pour toute la famille', 2);