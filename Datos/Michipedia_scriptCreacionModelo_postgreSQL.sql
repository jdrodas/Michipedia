-- Scripts de clase - Septiembre 5 de 2024 
-- Curso de Tópicos Avanzados de base de datos - UPB 202420
-- Juan Dario Rodas - juand.rodasm@upb.edu.co

-- Proyecto: Michipedia - Enciclopedia de Gatos
-- Motor de Base de datos: PostgreSQL 16.x

-- ***********************************
-- Abastecimiento de imagen en Docker
-- ***********************************
 
-- Descargar la imagen
docker pull postgres:latest

-- Crear el contenedor
docker run --name postgres-Michis -e POSTGRES_PASSWORD=unaClav3 -d -p 5432:5432 postgres:latest

-- ****************************************
-- Creación de base de datos y usuarios
-- ****************************************

-- Con usuario Postgres:

-- crear el esquema la base de datos
create database michis_db;

-- Conectarse a la base de datos
\c michis_db;

-- Creamos un esquema para almacenar todo el modelo de datos del dominio
create schema core;

-- crear el usuario con el que se implementará la creación del modelo
create user michi_app with encrypted password 'unaClav3';

-- asignación de privilegios para el usuario
grant connect on database michis_db to michi_app;
grant create on database michis_db to michi_app;
grant create, usage on schema core to michi_app;
alter user michi_app set search_path to core;

-- crear el usuario con el que se conectará la aplicación
create user michi_usr with encrypted password 'unaClav3';

-- asignación de privilegios para el usuario
grant connect on database michis_db to michi_usr;
grant usage on schema core to michi_usr;
alter default privileges for user michi_app in schema core grant insert, update, delete, select on tables to michi_usr;
alter default privileges for user michi_app in schema core grant execute on routines TO michi_usr;
alter user michi_usr set search_path to core;