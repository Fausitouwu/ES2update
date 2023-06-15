CREATE TABLE Tipo_Utilizador (
                                 id SERIAL PRIMARY KEY,
                                 tipo VARCHAR(20) CHECK (tipo IN ('Organizador', 'Participante'))
);

CREATE TABLE Utilizador (
                            id SERIAL PRIMARY KEY,
                            nome VARCHAR(255),
                            email VARCHAR(255),
                            senha VARCHAR(255),
                            idtipoUser INTEGER REFERENCES Tipo_Utilizador (id)
);


CREATE TABLE Mensagem (
                          id SERIAL PRIMARY KEY,
                          conteudo TEXT
);


CREATE TABLE MensagemUtilizador (
                                    Mensagem_ID INTEGER,
                                    Utilizador_ID INTEGER,
                                    PRIMARY KEY (Mensagem_ID, Utilizador_ID),
                                    FOREIGN KEY (Mensagem_ID) REFERENCES Mensagem (id),
                                    FOREIGN KEY (Utilizador_ID) REFERENCES Utilizador (id)
);


CREATE TABLE Eventos (
                         id SERIAL PRIMARY KEY,
                         nome VARCHAR(255),
                         data DATE,
                         hora TIME,
                         local VARCHAR(255),
                         descricao VARCHAR(255),
                         capacidade INTEGER,
                         preco DECIMAL(10, 2),
                         idUtilizador INTEGER REFERENCES Utilizador (id)
);

CREATE TABLE Atividades (
                            id SERIAL PRIMARY KEY,
                            nome VARCHAR(255),
                            data DATE,
                            hora TIME,
                            descricao VARCHAR(255),
                            idEvento INTEGER REFERENCES Eventos (id)
);

CREATE TABLE Ticket (
                        id SERIAL PRIMARY KEY,
                        tipoIngresso VARCHAR(255),
                        preco DECIMAL(10, 2),
                        quantidade INTEGER,
                        evento_id INTEGER REFERENCES Eventos (id)
);

CREATE TABLE Relatorio (
                           id SERIAL PRIMARY KEY,
                           nota VARCHAR(255),
                           id_evento INTEGER REFERENCES Eventos (id)
);

CREATE TABLE CategoriasEventos (
                                   id SERIAL PRIMARY KEY,
                                   nome VARCHAR(255)
);

CREATE TABLE EventoCategoria (
                                 Evento_ID INTEGER,
                                 Categoria_ID INTEGER,
                                 PRIMARY KEY (Evento_ID, Categoria_ID),
                                 FOREIGN KEY (Evento_ID) REFERENCES Eventos (id),
                                 FOREIGN KEY (Categoria_ID) REFERENCES CategoriasEventos (id)
);