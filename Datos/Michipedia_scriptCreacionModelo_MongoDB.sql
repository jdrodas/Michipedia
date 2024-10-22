-- Scripts de clase - Octubre 15 de 2024
-- Curso de Tópicos Avanzados de base de datos - UPB 202420
-- Juan Dario Rodas - juand.rodasm@upb.edu.co

-- Proyecto: Michipedia - Enciclopedia de Gatos
-- Motor de Base de datos: MongoDB - 7.x

-- ***********************************
-- Abastecimiento de imagen en Docker
-- ***********************************
 
-- Descargar la imagen -- https://hub.docker.com/_/mongo
docker pull mongo:latest

-- Crear el contenedor
docker run --name mongodb-michis -e “MONGO_INITDB_ROOT_USERNAME=mongoadmin” -e MONGO_INITDB_ROOT_PASSWORD=unaClav3 -p 27017:27017 -d mongo:latest

-- ****************************************
-- Creación de base de datos y usuarios
-- ****************************************

-- Con usuario mongoadmin:

-- crear la base de datos
use michis_db;

-- #########################################################
-- PENDIENTE: Crear el usuario con privilegios limitados
-- #########################################################

-- Creamos las collecciones ... usando un json schema para validación

db.createCollection("paises",{
        validator: {
            $jsonSchema: {
                bsonType: 'object',
                title: 'Los paises de origen de las razas',
                required: [
                    '_id',
                    'nombre',
                    'continente'
                ],
                properties: {
                    _id: {
                        bsonType: 'objectId'
                    },
                    nombre: {
                        bsonType: 'string',
                        description: "'nombre' Debe ser una cadena de caracteres y no puede ser nulo"
                    },
                    continente: {
                        bsonType: 'string',
                        description: "'continente' Debe ser una cadena de caracteres y no puede ser nulo"
                    }
                },
                additionalProperties: false
            }
        }
    } 
);

db.createCollection("razas",{
        validator: {
            $jsonSchema: {
                bsonType: 'object',
                title: 'Las razas de los gatos incluidas en esta enciclopedia',
                required: [
                    '_id',
                    'nombre',
                    'descripcion',
                    'pais'
                ],
                properties: {
                    _id: {
                        bsonType: 'objectId'
                    },
                    nombre: {
                        bsonType: 'string',
                        description: "'nombre' Debe ser una cadena de caracteres y no puede ser nulo"
                    },
                    descripcion: {
                        bsonType: 'string',
                        description: "'descripcion' Debe ser una cadena de caracteres y no puede ser nulo"
                    },
                    pais: {
                        bsonType: 'string',
                        description: "'pais' Debe ser una cadena de caracteres y no puede ser nulo"
                    }
                },
                additionalProperties: false
            }
        }
    } 
);


db.createCollection("caracteristicas",{
    validator: 
        {
        $jsonSchema: {
                bsonType: 'object',
                title: 'Las características de las razas',
                required: [
                    '_id',
                    'nombre',
                    'descripcion'
                ],
                properties: {
                    _id: {
                        bsonType: 'objectId'
                    },
                        nombre: {
                        bsonType: 'string',
                        description: '\'nombre\' Debe ser una cadena de caracteres y no puede ser nulo'
                    },
                        descripcion: {
                        bsonType: 'string',
                        description: '\'descripcion\' Debe ser una cadena de caracteres y no puede ser nulo'
                    }
                },
                additionalProperties: false
            }
        }
    } 
);


db.createCollection("comportamientos",{
    validator: 
        {
            $jsonSchema: {
                bsonType: 'object',
                title: 'Los comportamientos de las razas',
                required: [
                    '_id',
                    'nombre',
                    'descripcion'
                ],
                properties: {
                    _id: {
                        bsonType: 'objectId'
                    },
                    nombre: {
                        bsonType: 'string',
                        description: '\'nombre\' Debe ser una cadena de caracteres y no puede ser nulo'
                    },
                    descripcion: {
                        bsonType: 'string',
                        description: '\'descripcion\' Debe ser una cadena de caracteres y no puede ser nulo'
                    }
                },
                additionalProperties: false
            }
        }
    } 
);


db.createCollection("caracteristicas_razas",{
    validator: 
        {
            $jsonSchema: {
                bsonType: 'object',
                title: 'Las caracteristicas asociadas a las razas de los gatos',
                required: [
                    '_id',
                    'raza_id',
                    'caracteristica_id',
                    'valoracion'
                ],
                properties: {
                    _id: {
                        bsonType: 'objectId'
                    },
                    raza_id: {
                        bsonType: 'objectId',
                        description: "'raza_id' no puede ser nulo"
                    },
                    caracteristica_id: {
                        bsonType: 'objectId',
                        description: "'caracteristica_id' no puede ser nulo"
                    },
                    valoracion: {
                        bsonType: 'string',
                        description: "'valoracion' Debe ser una cadena de caracteres y no puede ser nulo"
                    }
                },
                additionalProperties: false
            }
        }
    } 
);        