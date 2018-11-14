--
-- Plik wygenerowany przez SQLiteStudio v3.2.1 dnia śr. lis 14 14:57:28 2018
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
    nazwa STRING PRIMARY KEY
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
    slowa_kluczowe STRING
);


-- Tabela: Wydawcy
DROP TABLE IF EXISTS Wydawcy;

CREATE TABLE Wydawcy (
    nazwa STRING PRIMARY KEY
);


COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
