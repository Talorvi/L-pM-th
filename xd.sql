--
-- Plik wygenerowany przez SQLiteStudio v3.2.1 dnia œr. lis 21 12:49:52 2018
--
-- U¿yte kodowanie tekstu: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Tabela: Autorzy
CREATE TABLE Autorzy (
    id_autora INTEGER PRIMARY KEY AUTOINCREMENT
                      NOT NULL,
    imie      STRING
);


-- Tabela: Kategorie
CREATE TABLE Kategorie (
    nazwa        STRING,
    id_kategorii INTEGER PRIMARY KEY AUTOINCREMENT
                         NOT NULL
);


-- Tabela: Prace
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
CREATE TABLE [Prace-Autorzy] (
    id_pracy  INTEGER REFERENCES Prace (id_pracy) 
                      NOT NULL,
    id_autora INTEGER REFERENCES Autorzy (id_autora) 
                      NOT NULL
);


-- Tabela: Prace-Kategorie
CREATE TABLE [Prace-Kategorie] (
    id_pracy     INTEGER REFERENCES Prace (id_pracy) 
                         NOT NULL,
    id_kategorii INTEGER REFERENCES Kategorie (id_kategorii) 
                         NOT NULL
);


-- Tabela: Wydawcy
CREATE TABLE Wydawcy (
    nazwa      STRING,
    id_wydawcy INTEGER PRIMARY KEY AUTOINCREMENT
                       NOT NULL
);


COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
