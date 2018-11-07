create database meta;

using meta;

create table Autorzy (id_autora int NOT NULL AUTO_INCREMENT, imie varchar(100));

create table Kategorie (nazwa varchar(100));

create table Wydawcy (nazwa varchar(100));

create table Prace (id_pracy NOT NULL AUTO_INCREMENT, tytul varchar(255), jezyk varchar(100), rodzaj enum('artyku³','czasopismo'), rok_publikacji int, slowa_kluczowe varchar(100));