--
-- Plik wygenerowany przez SQLiteStudio v3.2.1 dnia śr. lis 28 12:28:30 2018
--
-- Użyte kodowanie tekstu: UTF-8
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Tabela: Autorzy
DROP TABLE IF EXISTS Autorzy;

CREATE TABLE Autorzy (
    id_autora INTEGER PRIMARY KEY AUTOINCREMENT
                      NOT NULL,
    imie      STRING
);


-- Tabela: Kategorie
DROP TABLE IF EXISTS Kategorie;

CREATE TABLE Kategorie (
    nazwa        STRING,
    id_kategorii INTEGER PRIMARY KEY AUTOINCREMENT
                         NOT NULL
);


-- Tabela: Podkategorie
DROP TABLE IF EXISTS Podkategorie;

CREATE TABLE Podkategorie (
    id_kategorii    INTEGER REFERENCES Kategorie (id_kategorii) 
                            NOT NULL,
    id_podkategorii INTEGER REFERENCES Kategorie (id_kategorii) 
                            NOT NULL,
    PRIMARY KEY (
        id_kategorii,
        id_podkategorii
    )
);


-- Tabela: Prace
DROP TABLE IF EXISTS Prace;

CREATE TABLE Prace (
    id_pracy       INTEGER PRIMARY KEY AUTOINCREMENT
                           NOT NULL,
    tytul          STRING,
    jezyk          STRING,
    rodzaj         STRING,
    rok_publikacji INT,
    slowa_kluczowe STRING,
    id_wydawcy     INTEGER REFERENCES Wydawcy (id_wydawcy) 
);


-- Tabela: Prace-Autorzy
DROP TABLE IF EXISTS [Prace-Autorzy];

CREATE TABLE [Prace-Autorzy] (
    id_pracy  INTEGER REFERENCES Prace (id_pracy) 
                      NOT NULL,
    id_autora INTEGER REFERENCES Autorzy (id_autora) 
                      NOT NULL,
    PRIMARY KEY (
        id_pracy,
        id_autora
    )
);


-- Tabela: Prace-Kategorie
DROP TABLE IF EXISTS [Prace-Kategorie];

CREATE TABLE [Prace-Kategorie] (
    id_pracy     INTEGER REFERENCES Prace (id_pracy) 
                         NOT NULL,
    id_kategorii INTEGER REFERENCES Kategorie (id_kategorii) 
                         NOT NULL,
    PRIMARY KEY (
        id_pracy,
        id_kategorii
    )
);


-- Tabela: Wydawcy
DROP TABLE IF EXISTS Wydawcy;

CREATE TABLE Wydawcy (
    nazwa      STRING,
    id_wydawcy INTEGER PRIMARY KEY AUTOINCREMENT
                       NOT NULL
);


COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
